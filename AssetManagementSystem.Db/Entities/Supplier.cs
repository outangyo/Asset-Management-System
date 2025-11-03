using System.ComponentModel.DataAnnotations;

namespace AssetManagementSystem.Db.Entities
{
    public class Supplier
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string SupplierCode { get; set; } = string.Empty;

        [Required]
        [MaxLength(250)]
        public string Name { get; set; } = string.Empty;

        [MaxLength(200)] // ชื่อผู้ติดต่อ
        public string? ContactName { get; set; }

        [MaxLength(254)]
        [EmailAddress]
        public string? ContactEmail { get; set; }

        [MaxLength(50)]
        public string? ContactPhone { get; set; }

        [MaxLength(500)]
        public string? WebsiteURL { get; set; }

        [MaxLength(50)]
        public string? TaxId { get; set; }

        public bool IsActive { get; set; } = true;
        public DateTime DateAdded { get; set; }

        [MaxLength(2000)]
        public string? Notes { get; set; }

        // --- Relationships ---

        // ความสัมพันธ์แบบ 1-to-Many: Supplier 1 ราย สามารถมี Asset หลายชิ้น
        // (บรรทัดนี้ "ต้องเก็บไว้" มันคือตัวเชื่อมไปหา Asset)
        public virtual ICollection<Asset> SuppliedAssets { get; set; } = new List<Asset>();
    }
}
