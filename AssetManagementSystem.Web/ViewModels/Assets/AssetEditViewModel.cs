using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace AssetManagementSystem.Web.ViewModels.Assets
{
    public class AssetEditViewModel
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        [StringLength(20)]
        public string Code { get; set; } = string.Empty;

        [Required]
        [StringLength(200)]
        public string Name { get; set; } = string.Empty;

        [StringLength(200)]
        public string? Description { get; set; }

        // --- 1. Category ---
        [Required]
        [Display(Name = "Category")]
        public Guid CategoryId { get; set; } // เปลี่ยนเป็น Guid

        [ValidateNever]
        public IEnumerable<SelectListItem> Categories { get; set; } = Enumerable.Empty<SelectListItem>();

        // --- 2. Department ---
        [Required]
        [Display(Name = "Department")]
        public Guid DepartmentId { get; set; } // เปลี่ยนเป็น Guid

        [ValidateNever]
        public IEnumerable<SelectListItem> Departments { get; set; } = Enumerable.Empty<SelectListItem>();

        // --- 3. Location ---
        [Required]
        [Display(Name = "Location")]
        public Guid LocationId { get; set; } // เปลี่ยนเป็น Guid

        [ValidateNever]
        public IEnumerable<SelectListItem> Locations { get; set; } = Enumerable.Empty<SelectListItem>();

        [StringLength(200)]
        public string? Note { get; set; }

        // ข้อมูลเพิ่มเติมที่ดึงมาจาก User ที่สร้าง Asset
        [Display(Name = "Created By")]
        public string? CreatedByUserName { get; set; }

        [Required]
        [Display(Name = "Registered Date")]
        [DataType(DataType.Date)]
        public DateTime DateRegister { get; set; }

        [Display(Name = "Active")]
        public bool IsActive { get; set; }
    }
}
