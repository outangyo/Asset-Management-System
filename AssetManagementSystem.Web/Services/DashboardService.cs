using AssetManagementSystem.Db.Data;
using AssetManagementSystem.Web.ViewModels;

using Microsoft.EntityFrameworkCore;

namespace AssetManagementSystem.Web.Services
{
    public class DashboardService : IDashboardService
    {
        private readonly ApplicationDbContext _context;

        public DashboardService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<DashboardViewModel> GetDashboardDataAsync()
        {
            // ใช้ CountAsync() เพื่อประสิทธิภาพสูงสุด (มันจะสร้าง SQL SELECT COUNT(*) ให้)
            // ไม่ต้องดึงข้อมูลทั้งหมดมานับใน Memory
            var model = new DashboardViewModel
            {
                TotalAssets = await _context.Assets.CountAsync(),
                TotalSuppliers = await _context.Suppliers.CountAsync(),
                TotalUsers = await _context.Users.CountAsync(),
                TotalRoles = await _context.Roles.CountAsync()
            };

            return model;
        }
    }
}
