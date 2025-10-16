using System.ComponentModel.DataAnnotations;

namespace AssetManagementSystem.Web.Models.Assets
{
    public class AssetCreateViewModel
    {
        [Required]
        [StringLength(20)]
        public string Code { get; set; } = string.Empty;

        [Required]
        [StringLength(200)]
        public string Name { get; set; } = string.Empty;

        [StringLength(200)]
        public string? Description { get; set; }

        [Required]
        [StringLength(20)]
        public string Category { get; set; } = string.Empty;

        [Required]
        [StringLength(20)]
        public string Department { get; set; } = string.Empty;

        [Required]
        [StringLength(20)]
        public string Location { get; set; } = string.Empty;

        [StringLength(200)]
        public string? Note { get; set; }

        [Required]
        [Display(Name = "Registered Date")]
        [DataType(DataType.Date)]
        public DateTime DateRegister { get; set; } = DateTime.UtcNow;

        [Display(Name = "Active")]
        public bool IsActive { get; set; } = true;
    }
}
