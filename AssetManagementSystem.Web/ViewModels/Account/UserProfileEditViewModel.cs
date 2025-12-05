using System.ComponentModel.DataAnnotations;

namespace AssetManagementSystem.Web.ViewModels.Account
{
    public class UserProfileEditViewModel
    {
        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; } = string.Empty;

        [Display(Name = "Phone Number")]
        [Phone]
        public string? PhoneNumber { get; set; }

        [Display(Name = "Date of Birth")]
        [DataType(DataType.Date)]
        public DateTime? DateOfBirth { get; set; }

        [Display(Name = "Username")]
        public string? Username { get; set; } // (Readonly)

        [Display(Name = "Email")]
        public string? Email { get; set; } // (Readonly)
    }
}
