using AssetManagementSystem.Web.ViewModels.Suppliers;

namespace AssetManagementSystem.Web.Services
{
    public interface ISupplierService
    {
        Task<SupplierIndexViewModel> GetSuppliersAsync(SupplierListFilterViewModel filter);
    }
}
