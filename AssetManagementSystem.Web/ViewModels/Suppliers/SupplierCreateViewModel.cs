using System.ComponentModel.DataAnnotations;

namespace AssetManagementSystem.Web.ViewModels.Suppliers
{
    public class SupplierCreateViewModel
    {
        [Required]
        [MaxLength(50)]
        [Display(Name = "Supplier Code")]
        public string SupplierCode { get; set; } = string.Empty;

        [Required]
        [MaxLength(250)]
        public string Name { get; set; } = string.Empty;

        [MaxLength(200)]
        [Display(Name = "Contact Name")]
        public string? ContactName { get; set; }

        [MaxLength(254)]
        [EmailAddress]
        [Display(Name = "Contact Email")]
        public string? ContactEmail { get; set; }

        [MaxLength(50)]
        [Display(Name = "Contact Phone")]
        public string? ContactPhone { get; set; }

        [MaxLength(500)]
        [Display(Name = "Website URL")]
        public string? WebsiteURL { get; set; }

        [MaxLength(50)]
        [Display(Name = "Tax ID")]
        public string? TaxId { get; set; }

        [Display(Name = "Is Active?")]
        public bool IsActive { get; set; } = true; // ตั้งค่าเริ่มต้นให้ Active

        [MaxLength(2000)]
        public string? Notes { get; set; }
    }
}
