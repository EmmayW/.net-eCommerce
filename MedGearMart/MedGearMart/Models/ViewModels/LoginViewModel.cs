using System.ComponentModel.DataAnnotations;

namespace MedGearMart.Models.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "UserName is required.")]
        public string? Username { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [DataType(DataType.Password)]
        public string? Password { get; set; }

        [Display(Name = "Remember Me")]
        public bool RememberMe { get; set; }
    }
}
