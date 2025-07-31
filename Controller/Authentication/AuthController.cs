using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OfficeProject.Servicess;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using OfficeProject.Models.DTO;
using OfficeProject.Models.Entities;
using System.Text.Json;
using OfficeProject.Authentication;
using System.Data;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OfficeProject.Data;
namespace OfficeProject.Controller.Authentication
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IMapper Mapper;   
        private readonly IUserService _userService;
        private readonly ITokenService _tokenService;
        private readonly IPasswordService _passwordService;
        private readonly IConfiguration _configuration;

        public AuthController(
            IUserService userService,
            ITokenService tokenService,
            IPasswordService passwordService,
            IConfiguration configuration,
            IMapper mapper)
        {
            this._userService = userService;
            this._tokenService = tokenService;
            this._passwordService = passwordService;
            this._configuration = configuration;
            this.Mapper = mapper;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
                       if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (await _userService.GetUserByUsername(model.UserName) != null)
                return Conflict("Username already exists");

            var user = new Users
            {
                UserName = model.UserName,
                UserEmail = model.UserEmail,
                UserContact = model.UserContact,
                UserPassword = _passwordService.HashPassword(model.UserPassword),
                Role = model.Role,
                PreeAssignUserRole=model.PreeAssignUserRole,
                CompanyName = model.CompanyName,
                Address = model.Address,
                UserDesignation = model.UserDesignation,
                JoiningDate = model.JoiningDate,
                IsActive = true


            };

            try
            {
                await _userService.AddUserAsync(user);

                // Generate claims identical to login
                var claims = new List<Claim>
        {
            new(ClaimTypes.Name, user.UserName),
            new(ClaimTypes.Role, user.Role.ToString()),
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new(JwtRegisteredClaimNames.Email, user.UserEmail),
            new("Contact", user.UserContact)
        };

                var token = GenerateJwtToken(claims);
                var refreshToken = _tokenService.GenerateRefreshToken();

                await _userService.UpdateRefreshToken(user.UserId, refreshToken);

                return Ok(new AuthResponse
                {
                    AccessToken = token,
                    RefreshToken = refreshToken,
                    ExpiresIn = 43200,
                    UserInfo = new UserInfoDTO
                    {
                        UserId = user.UserId,
                        UserName = user.UserName,
                        UserEmail = user.UserEmail,
                        Role = user.Role
                    }
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Registration failed: {ex.Message}");
            }
        }


        [HttpPost("login")]
        public async Task<ActionResult<AuthResponse>> Login([FromBody] LoginModel login)
        {
            if (login == null)
                return BadRequest("Login data is required");

            var user = await _userService.GetUserByUsername(login.UserName);

            if (user == null || !_passwordService.VerifyPassword(login.UserPassword,user.UserPassword))
                return Unauthorized("Invalid credentials");

            if (!user.IsActive)
                return Unauthorized("Account disabled");

            var claims = GenerateUserClaims(Mapper.Map<Users>(user));
            var token = GenerateJwtToken(claims); // Returns JwtSecurityToken
            var refreshToken = _tokenService.GenerateRefreshToken();
            await _userService.UpdateRefreshToken(Mapper.Map<Users>(user).UserId, refreshToken);
            return Ok(new AuthResponse
            {
                AccessToken = token,
                RefreshToken = refreshToken,
                ExpiresIn = (int)TimeSpan.FromHours(12).TotalSeconds,
                UserInfo = new UserInfoDTO
                {
                    UserId = user.UserId,
                    UserName = user.UserName,
                    UserEmail = user.UserEmail,
                    Role = (Models.Enums.UserRole)user.Role!
                }
            });
        }

        [HttpPost("refresh")]
        public async Task<ActionResult<AuthResponse>> Refresh([FromBody] RefreshTokenModel model)
        {
            try
            {
                // 1. Validate input
                if (model?.AccessToken == null || model.RefreshToken == null)
                    return BadRequest(new { Message = "Tokens are required" });

                // 2. Get principal from expired token
                var principal = _tokenService.GetPrincipalFromExpiredToken(model.AccessToken);
                if (principal?.Identity?.Name == null)
                    return BadRequest(new { Message = "Invalid token" });

                // 3. Get user from database
                var user = await _userService.GetUserByUsername(principal.Identity.Name);
                if (user == null)
                    return Unauthorized(new { Message = "User not found" });

                // 4. Validate refresh token
                if (Mapper.Map<Users>(user).RefreshToken != model.RefreshToken || Mapper.Map<Users>(user).RefreshTokenExpiry <= DateTime.UtcNow)
                    return Unauthorized(new { Message = "Invalid refresh token" });

                // 5. Generate new tokens (with null checks)
                var newAccessToken = GenerateJwtToken(principal.Claims);
                if (string.IsNullOrEmpty(newAccessToken))
                    return StatusCode(500, new { Message = "Failed to generate access token" });

                var newRefreshToken = _tokenService.GenerateRefreshToken();
                if (string.IsNullOrEmpty(newRefreshToken))
                    return StatusCode(500, new { Message = "Failed to generate refresh token" });

                // 6. Update refresh token in database
                await _userService.UpdateRefreshToken(Mapper.Map<Users>(user).UserId, newRefreshToken);

                return Ok(new AuthResponse
                {
                    AccessToken = newAccessToken,
                    RefreshToken = newRefreshToken,
                    ExpiresIn = (int)TimeSpan.FromHours(12).TotalSeconds
                });
            }
            catch (SecurityTokenException ex)
            {
                return Unauthorized(new { Message = "Invalid token", Details = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An error occurred during token refresh", Details = ex.Message });
            }
        }


        private string GenerateJwtToken(IEnumerable<Claim> claims)
        {
            var secret = _configuration["JwtSettings:Secret"];
            var issuer = _configuration["JwtSettings:Issuer"];
            var audience = _configuration["JwtSettings:Audience"];

            //var secret = Environment.GetEnvironmentVariable("JWT_SECRET");
            //var issuer = Environment.GetEnvironmentVariable("JWT_ISSUER");
            //var audience = Environment.GetEnvironmentVariable("JWT_AUDIENCE");

            if (string.IsNullOrEmpty(secret) || string.IsNullOrEmpty(issuer) || string.IsNullOrEmpty(audience))
                throw new InvalidOperationException("JWT configuration is missing in environment variables.");

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));
            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                expires: DateTime.UtcNow.AddHours(12),
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }


        private List<Claim> GenerateUserClaims(Users user)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));

            return new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserId.ToString()),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Role, user.Role.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.UserEmail),
                new Claim("Contact", user.UserContact),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };
        }

        [Authorize]
        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            var username = User.Identity?.Name;
            if (username != null)
            {
                await _userService.RevokeRefreshToken(username);
            }
            return Ok("Logged out successfully");
        }
    }
   

}