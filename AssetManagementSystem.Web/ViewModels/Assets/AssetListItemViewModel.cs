using System.ComponentModel.DataAnnotations;

namespace AssetManagementSystem.Web.ViewModels.Assets
{
    public class AssetListItemViewModel
    {
        public Guid Id { get; set; }

        [Display(Name = "Asset Code")]
        public string Code { get; set; } = string.Empty;

        [Display(Name = "Asset Name")]
        public string Name { get; set; } = string.Empty;

        public string Category { get; set; } = string.Empty;
        public string Department { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;

        [Display(Name = "Registered Date")]
        public DateTime DateRegister { get; set; }

        [Display(Name = "Status")]
        public bool IsActive { get; set; }
    }
}
