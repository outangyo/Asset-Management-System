namespace AssetManagementSystem.Web.ViewModels.Locations
{
    public class LocationListItemViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Address { get; set; }
        public bool IsActive { get; set; }
    }
}
