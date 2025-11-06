using AssetManagementSystem.Web.ViewModels.Assets;
using AssetManagementSystem.Web.ViewModels.Suppliers;
using Microsoft.AspNetCore.Identity;

namespace AssetManagementSystem.Web.Services
{
    public interface ISupplierService
    {
        Task<SupplierIndexViewModel> GetSuppliersAsync(SupplierListFilterViewModel filter);
        Task<SupplierDetailsViewModel?> GetDetailsAsync(Guid id);
        Task<SupplierEditViewModel?> GetForEditAsync(Guid id);
        Task<IdentityResult> UpdateAsync(SupplierEditViewModel model);
    }
}
