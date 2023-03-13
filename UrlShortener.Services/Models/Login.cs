using System.ComponentModel.DataAnnotations;

namespace URL_Shortener.Models
{
    public class LoginRequest
    {
        [Required(ErrorMessage = "User Name is required")]
        public string? Username { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string? Password { get; set; }
    }
    public class LoginResponse : Response
    {
        public string Token { get; set; }
        public DateTime Expiration { get; set; }
    }
}
