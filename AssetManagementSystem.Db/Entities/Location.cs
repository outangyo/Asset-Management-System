using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssetManagementSystem.Db.Entities
{
    public class Location
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty; // เช่น Building A

        [MaxLength(500)]
        public string? Address { get; set; }

        public bool IsActive { get; set; } = true;
    }
}
