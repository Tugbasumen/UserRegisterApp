using Microsoft.AspNetCore.Http;

namespace UserRegisterApp.Models
{
    public class UserViewModel
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public IFormFile ProfileImage { get; set; }
    }
}
