namespace AssetManagementSystem.Web.ViewModels.Departments
{
    public class DepartmentListItemViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Code { get; set; }
        public bool IsActive { get; set; }
    }
}
