using System.ComponentModel.DataAnnotations;

namespace AssetManagementSystem.Web.ViewModels
{
    public class ResendConfirmationEmailViewModel
    {
        [Required(ErrorMessage = "Email Id is Required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; } = null!;
    }
}
