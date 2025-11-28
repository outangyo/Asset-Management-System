using System.ComponentModel.DataAnnotations;

namespace AssetManagementSystem.Web.ViewModels.Locations
{
    public class LocationCreateViewModel
    {
        public Guid Id { get; set; }

        [Required]
        [MaxLength(100)]
        [Display(Name = "Location Name")]
        public string Name { get; set; } = string.Empty;

        [MaxLength(500)]
        [Display(Name = "Address / Building")]
        public string? Address { get; set; }

        public bool IsActive { get; set; } = true;
    }
}
