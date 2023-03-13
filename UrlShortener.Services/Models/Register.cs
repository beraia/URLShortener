using System.ComponentModel.DataAnnotations;

namespace URL_Shortener.Models
{
    public class RegisterRequest
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
    }
    public class RegisterResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }
    }
}
