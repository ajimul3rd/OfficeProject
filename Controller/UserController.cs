using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OfficeProject.Models.DTO;
using OfficeProject.Models.Entities;
using OfficeProject.Servicess;

namespace OfficeProject.Controllers
{
  
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService userService;

        public UserController(IUserService userService)
        {
            this.userService = userService;
        }

        // ✅ Get all users

      
        [HttpGet]
        [Authorize(Policy = "AdminOrManager")]
        public async Task<ActionResult<List<UserDTO>>> GetAllUsersDTO()
        {
            var users = await userService.GetAllUsersDTOAsync();
            return Ok(users);
        }

        // ✅ Get user by ID
        [HttpGet("{id}")]
        public async Task<ActionResult<UserDTO>> GetUserDTOById(int id)
        {
            //Console.WriteLine("UpdateUser");
            var user = await userService.GetUserDTOById(id);
            if (user == null) return NotFound();
            return Ok(user);
        }

        // ✅ Get user by username
        [HttpGet("by-username/{username}")]
        public async Task<ActionResult<UserDTO>> GetUserDTOByUsername(string username)
        {
            var user = await userService.GetUserDTOByUsername(username);
            if (user == null) return NotFound();
            return Ok(user);
        }

        // ✅ Add user
        [HttpPost]
        [Authorize(Policy = "AdminOrManager")]
        public async Task<IActionResult> AddUser([FromBody] Users user)
        {
            await userService.AddUserAsync(user);
            return CreatedAtAction(nameof(GetUserDTOById), new { id = user.UserId }, user);
        }

        // ✅ Update user
        [HttpPut("{id}")]
        [Authorize(Policy = "AdminOrManager")]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] UserDTO user)
        {
            if (id != user.UserId) return BadRequest("ID mismatch");
            await userService.UpdateUserAsync(user);
            return NoContent();

        }

        // ✅ Update Refresh Token
        [HttpPost("update-refresh-token")]
        public async Task<IActionResult> UpdateRefreshToken(int userId, [FromBody] string refreshToken)
        {
            await userService.UpdateRefreshToken(userId, refreshToken);
            return Ok();
        }

        // ✅ Revoke Refresh Token
        [HttpPost("revoke-refresh-token")]
        public async Task<IActionResult> RevokeRefreshToken([FromBody] string username)
        {
            await userService.RevokeRefreshToken(username);
            return Ok();
        }

        [AllowAnonymous]
        [HttpGet("check-header")]
        public IActionResult CheckAuthorizationHeader()
        {
            var authHeader = Request.Headers["Authorization"].ToString();

            if (string.IsNullOrEmpty(authHeader))
            {
                return BadRequest("Authorization header not found.");
            }

            return Ok($"Authorization Header: {authHeader}");
        }

        [HttpPost("add-user-with-client")]
        public async Task<IActionResult> AddUserWithClient([FromBody] UserWithClientsDto dto)
        {
            var result = await userService.GetAllUsersDTOClientAsync(dto);
            if (result == null) return BadRequest("Failed to add user");
            return Ok(result);
        }


        [HttpGet("current")]
        public async Task<ActionResult<UserDTO>> GetCurrentLoggedUserAsync()
        {
            try
            {
                var user = await userService.GetCurrentLoggedUserAsync();

                return Ok(user);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while retrieving the user.", error = ex.Message });
            }
        }        
        [HttpGet("pree-assign-user")]
        public async Task<ActionResult<UserDTO>> GetPreeAssignUsersAsync()
        {
            try
            {
                var user = await userService.GetPreeAssignUsersAsync();

                return Ok(user);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while retrieving the user.", error = ex.Message });
            }
        }


    }
}
