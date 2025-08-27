using System.ComponentModel.DataAnnotations;

namespace AssetManagementSystem.Web.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Email Id is Required")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password Id is Required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }
}
