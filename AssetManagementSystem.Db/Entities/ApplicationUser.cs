using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace AssetManagementSystem.Db.Entities
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        // Extended properties
        [Required]
        [MaxLength(100)]
        public string FirstName { get; set; } = null!;
        [MaxLength(100)]
        public string? LastName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public DateTime? LastLogin { get; set; }
        public bool IsActive { get; set; }
        //Audit Columns
        public DateTime? CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }
        // Navigation property for one-to-many relationsip
        public virtual List<Address>? Addresses { get; set; }
    }
}
