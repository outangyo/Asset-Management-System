using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssetManagementSystem.Db.Entities
{
    public class Asset
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }   // asset name
        public string Description { get; set; }  // details about asset
        public string Category { get; set; }     // e.g., IT, Furniture, Vehicle
        public string Location { get; set; }     // where the asset is kept
        public string Department { get; set; }   // which department uses it

    }
}
