using System.ComponentModel.DataAnnotations;

namespace AssetManagementSystem.Web.ViewModels.Suppliers
{
    public class SupplierEditViewModel
    {
        // เราต้องมี Id เสมอ เพื่อรู้ว่ากำลังแก้ตัวไหน
        public Guid Id { get; set; }

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
        public bool IsActive { get; set; }

        [MaxLength(2000)]
        public string? Notes { get; set; }

        // เราต้องมี DateAdded ด้วย เพื่อไม่ให้ค่านี้หายไปตอน Save
        // เราจะใช้ [ReadOnly] หรือซ่อนไว้ในฟอร์ม
        public DateTime DateAdded { get; set; }
    }
}
