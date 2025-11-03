using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace AssetManagementSystem.Db.Entities
{
    public class ApplicationRole : IdentityRole<Guid>
    {
        // Extended property
        [MaxLength(500)]
        public string? Description { get; set; }
        public bool IsActive { get; set; }
        //Audit Columns
        public DateTime? CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }
    }
}
