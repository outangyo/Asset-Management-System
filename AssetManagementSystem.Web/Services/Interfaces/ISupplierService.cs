using AssetManagementSystem.Web.ViewModels.Assets;
using AssetManagementSystem.Web.ViewModels.Suppliers;
using Microsoft.AspNetCore.Identity;

namespace AssetManagementSystem.Web.Services.Interfaces
{
    public interface ISupplierService
    {
        // GetSuppliersAsync ไม่ได้ใช้ Generic Repository เพราะมีการกรองข้อมูลและการแบ่งหน้า
        Task<SupplierIndexViewModel> GetSuppliersAsync(SupplierListFilterViewModel filter);
        Task<(IdentityResult result, Guid newId)> CreateAsync(SupplierCreateViewModel model);
        Task<SupplierDetailsViewModel?> GetDetailsAsync(Guid id);
        Task<SupplierEditViewModel?> GetForEditAsync(Guid id);
        Task<IdentityResult> UpdateAsync(SupplierEditViewModel model);
        Task<IdentityResult> DeleteAsync(Guid id);
    }
}
