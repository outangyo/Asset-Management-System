using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AssetManagementSystem.Db.Entities
{
    public class Address
    {
        public Guid Id { get; set; }

        [Required]
        public Guid UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        public virtual ApplicationUser User { get; set; } = null!;

        [Required]
        [MaxLength(500)] // ที่อยู่บรรทัด 1
        public string Street { get; set; } = string.Empty;

        [Required]
        [MaxLength(100)] // ตำบล/อำเภอ
        public string City { get; set; } = string.Empty;

        [Required]
        [MaxLength(100)] // จังหวัด
        public string State { get; set; } = string.Empty;

        [Required]
        [MaxLength(10)] // รหัสไปรษณีย์
        public string PostalCode { get; set; } = string.Empty;

        [Required]
        [MaxLength(100)]
        public string Country { get; set; } = string.Empty;
        public bool IsActive { get; set; } = true;

        //Audit Columns
        public DateTime? CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }
    }
}
