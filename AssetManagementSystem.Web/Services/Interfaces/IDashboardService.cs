using AssetManagementSystem.Web.ViewModels;

namespace AssetManagementSystem.Web.Services.Interfaces
{
    public interface IDashboardService
    {
        Task<DashboardViewModel> GetDashboardDataAsync();
    }
}
