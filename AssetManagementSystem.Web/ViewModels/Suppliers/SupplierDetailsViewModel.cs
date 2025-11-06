namespace AssetManagementSystem.Web.ViewModels.Suppliers
{
    public class SupplierDetailsViewModel
    {
        public Guid Id { get; set; }
        public string SupplierCode { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string? ContactName { get; set; }
        public string? ContactEmail { get; set; }
        public string? ContactPhone { get; set; }
        public string? WebsiteURL { get; set; }
        public string? TaxId { get; set; }
        public bool IsActive { get; set; }
        public DateTime DateAdded { get; set; }
        public string? Notes { get; set; }
    }
}
