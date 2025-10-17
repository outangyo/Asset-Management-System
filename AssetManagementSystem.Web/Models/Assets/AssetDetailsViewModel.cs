using AssetManagementSystem.Db.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AssetManagementSystem.Web.Models.Assets
{
    public class AssetDetailsViewModel
    {
        public Guid Id { get; set; }
        public string Code { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string Category { get; set; } = string.Empty;
        public string Department { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
        public string? Note { get; set; }
        public DateTime DateRegister { get; set; }
        public bool IsActive { get; set; }

        // ข้อมูลเพิ่มเติมที่ดึงมาจาก User ที่สร้าง Asset
        [Display(Name = "Created By")]
        public string? CreatedByUserName { get; set; }
    }
}
