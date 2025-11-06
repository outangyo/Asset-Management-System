namespace AssetManagementSystem.Web.ViewModels.Suppliers
{
    public class SupplierListItemViewModel
    {
        public Guid Id { get; set; }
        public string SupplierCode { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string? ContactName { get; set; }
        public string? ContactEmail { get; set; }
        public string? ContactPhone { get; set; }
        public bool IsActive { get; set; }
    }
}
