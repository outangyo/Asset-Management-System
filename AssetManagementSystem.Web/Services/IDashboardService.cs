using AssetManagementSystem.Web.ViewModels;

namespace AssetManagementSystem.Web.Services
{
    public interface IDashboardService
    {
        Task<DashboardViewModel> GetDashboardDataAsync();
    }
}
