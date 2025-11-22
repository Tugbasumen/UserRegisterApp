using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace UserRegisterApp.Models
{
    public class UserViewModel
    {
  
        [StringLength(50, ErrorMessage = "İsim en fazla 50 karakter olabilir.")]
        public string Name { get; set; }


        [EmailAddress(ErrorMessage = "Geçerli bir email giriniz.")]
        public string Email { get; set; }

   
        [MinLength(6, ErrorMessage = "Parola en az 6 karakter olmalıdır.")]
        public string Password { get; set; }

        public IFormFile ProfileImage { get; set; }
    }
}
