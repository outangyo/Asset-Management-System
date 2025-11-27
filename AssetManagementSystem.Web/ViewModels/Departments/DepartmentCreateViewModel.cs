using System.ComponentModel.DataAnnotations;

namespace AssetManagementSystem.Web.ViewModels.Departments
{
    public class DepartmentCreateViewModel
    {
        public Guid Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        [MaxLength(3)]
        public string? Code { get; set; }

        public bool IsActive { get; set; } = true;
    }
}
