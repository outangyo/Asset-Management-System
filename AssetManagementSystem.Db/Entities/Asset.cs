using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AssetManagementSystem.Db.Entities
{
    public class Asset
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Code { get; set; } = string.Empty;

        [Required]
        [MaxLength(200)]
        public string Name { get; set; } = string.Empty;

        [MaxLength(1000)]
        public string? Description { get; set; }

        [Required]
        [MaxLength(100)]
        public string Category { get; set; } = string.Empty;

        [Required]
        [MaxLength(100)]
        public string Department { get; set; } = string.Empty;

        [Required]
        [MaxLength(100)]
        public string Location { get; set; } = string.Empty;

        [MaxLength(500)]
        public string? Note { get; set; }

        public DateTime DateRegister { get; set; }
        public bool IsActive { get; set; }

        // --- Foreign Keys & Relationships ---

        // 1. ความสัมพันธ์กับ User (ผู้รับผิดชอบ/ผู้ลงทะเบียน)
        public Guid UserId { get; set; }

        [ForeignKey("UserId")]
        public virtual ApplicationUser User { get; set; } = null!;

        // 2. ความสัมพันธ์กับ Supplier (ผู้ขาย) - (ที่เพิ่มใหม่)
        // เราใช้ Guid? (Nullable) เผื่อว่า Asset นี้ไม่มีผู้ขาย
        public Guid? SupplierId { get; set; }

        [ForeignKey("SupplierId")]
        public virtual Supplier? Supplier { get; set; } // เป็น Nullable ให้สอดคล้องกัน
    }
}
