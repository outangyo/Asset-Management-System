using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssetManagementSystem.Db.Entities
{
    public class ApplicationRole : IdentityRole<Guid>
    {
        // Extended property
        public string? Description { get; set; }
        public bool IsActive { get; set; }
        //Audit Columns
        public DateTime? CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }
    }
}
