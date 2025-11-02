using Microsoft.AspNetCore.Identity;

namespace AssetManagementSystem.Db.Entities
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        // Extended properties
        public string FirstName { get; set; } = null!;
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
