using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssetManagementSystem.Db.Entities
{
    public class Asset
    {
        public Guid Id { get; set; }
        public string Code { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; } // อนุญาตให้เป็น null ได้
        public string Category { get; set; } = string.Empty;
        public string Department { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
        public string? Note { get; set; } // อนุญาตให้เป็น null ได้
        public Guid UserId { get; set; } // Foreign key ไปยัง ApplicationUser
        public DateTime DateRegister { get; set; }
        public bool IsActive { get; set; }
    }
}
