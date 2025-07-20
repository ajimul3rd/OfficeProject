using OfficeProject.Models.DTO;

namespace OfficeProject.Authentication
{
    public class AuthResponse
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public int ExpiresIn { get; set; }
        public UserInfoDTO UserInfo { get; set; }
    }
}
