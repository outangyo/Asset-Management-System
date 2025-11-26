using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace AssetManagementSystem.Web.ViewModels.Assets
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

        // --- 1. Category (เปลี่ยนจาก string เป็น Guid) ---
        [Required(ErrorMessage = "Please select a category")]
        [Display(Name = "Category")]
        public Guid CategoryId { get; set; }

        // ตัวแปรสำหรับส่ง List ไปให้ Dropdown ในหน้า View
        [ValidateNever] // บอกว่าไม่ต้อง Validate ตัว List นี้ตอน Submit Form
        public IEnumerable<SelectListItem> Categories { get; set; } = Enumerable.Empty<SelectListItem>();

        // --- 2. Department (เปลี่ยนจาก string เป็น Guid) ---
        [Required(ErrorMessage = "Please select a department")]
        [Display(Name = "Department")]
        public Guid DepartmentId { get; set; }

        [ValidateNever]
        public IEnumerable<SelectListItem> Departments { get; set; } = Enumerable.Empty<SelectListItem>();

        // --- 3. Location (เปลี่ยนจาก string เป็น Guid) ---
        [Required(ErrorMessage = "Please select a location")]
        [Display(Name = "Location")]
        public Guid LocationId { get; set; }

        [ValidateNever]
        public IEnumerable<SelectListItem> Locations { get; set; } = Enumerable.Empty<SelectListItem>();

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
