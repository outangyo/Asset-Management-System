using AssetManagementSystem.Db.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace AssetManagementSystem.Db.Data
{
    public static class AssetSeeder
    {
        public static async Task SeedAssetsAsync(IServiceProvider services)
        {
            using var scope = services.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            // ดึง User ที่มีอยู่มาใช้ (เพื่อกำหนด UserId)
            // เราจะสุ่มเอา User คนแรกที่เป็น Admin มาเป็นเจ้าของ Asset ทั้งหมด
            var adminUser = await context.Users
                .FirstOrDefaultAsync(u => u.UserName.StartsWith("pranav"));

            if (adminUser == null)
            {
                // ถ้าไม่มี Admin User, ก็ไม่สามารถสร้าง Asset ได้
                Console.WriteLine("Admin user not found, skipping asset seeding.");
                return;
            }

            // ตรวจสอบว่ามี Asset อยู่แล้วหรือยัง
            if (await context.Assets.AnyAsync())
            {
                return; // ถ้ามีอยู่แล้ว ก็ไม่ต้องทำอะไร
            }

            // สร้างข้อมูล Asset ตัวอย่าง
            var assets = new List<Asset>
            {
                // --- IT Assets (Active) ---
                new Asset
                {
                    Id = Guid.NewGuid(), Code = "LAP-DELL-01", Name = "Dell Latitude 5420 Laptop",
                    Category = "IT", Department = "Marketing", Location = "Building A, Floor 2",
                    DateRegister = DateTime.UtcNow.AddMonths(-6), IsActive = true, UserId = adminUser.Id
                },
                new Asset
                {
                    Id = Guid.NewGuid(), Code = "MON-HP-01", Name = "HP EliteDisplay 24 inch Monitor",
                    Category = "IT", Department = "HR", Location = "Building B, Floor 1",
                    DateRegister = DateTime.UtcNow.AddMonths(-3), IsActive = true, UserId = adminUser.Id
                },
                new Asset
                {
                    Id = Guid.NewGuid(), Code = "KB-LOGI-01", Name = "Logitech MX Keys Keyboard",
                    Category = "IT", Department = "Development", Location = "Building A, Floor 3",
                    DateRegister = DateTime.UtcNow.AddMonths(-2), IsActive = true, UserId = adminUser.Id
                },
                new Asset
                {
                    Id = Guid.NewGuid(), Code = "PRN-BRO-01", Name = "Brother HL-L2350DW Printer",
                    Category = "IT", Department = "Finance", Location = "Building A, Floor 1",
                    DateRegister = DateTime.UtcNow.AddYears(-1), IsActive = true, UserId = adminUser.Id
                },

                // --- Furniture Assets (Active) ---
                new Asset
                {
                    Id = Guid.NewGuid(), Code = "CHR-ERG-01", Name = "Ergonomic Office Chair",
                    Category = "Furniture", Department = "HR", Location = "Common Area",
                    DateRegister = DateTime.UtcNow.AddYears(-2), IsActive = true, UserId = adminUser.Id
                },
                new Asset
                {
                    Id = Guid.NewGuid(), Code = "DSK-STD-01", Name = "Standing Desk, Electric",
                    Category = "Furniture", Department = "Development", Location = "Building A, Floor 3",
                    DateRegister = DateTime.UtcNow.AddMonths(-10), IsActive = true, UserId = adminUser.Id
                },
                new Asset
                {
                    Id = Guid.NewGuid(), Code = "CAB-FIL-01", Name = "Filing Cabinet, 3-Drawer",
                    Category = "Furniture", Department = "Finance", Location = "Building A, Floor 1",
                    DateRegister = DateTime.UtcNow.AddYears(-3), IsActive = true, UserId = adminUser.Id
                },

                // --- IT Assets (Inactive) ---
                new Asset
                {
                    Id = Guid.NewGuid(), Code = "LAP-LEN-02", Name = "Lenovo ThinkPad T480 (Broken Screen)",
                    Category = "IT", Department = "Storage", Location = "IT Storage Room",
                    DateRegister = DateTime.UtcNow.AddYears(-2), IsActive = false, UserId = adminUser.Id
                },
                new Asset
                {
                    Id = Guid.NewGuid(), Code = "MON-DELL-02", Name = "Dell UltraSharp U2718Q (Flickering)",
                    Category = "IT", Department = "Storage", Location = "IT Storage Room",
                    DateRegister = DateTime.UtcNow.AddMonths(-18), IsActive = false, UserId = adminUser.Id
                },

                // --- Vehicle Assets ---
                new Asset
                {
                    Id = Guid.NewGuid(), Code = "VEH-TOY-01", Name = "Toyota Camry 2022 (Company Car)",
                    Category = "Vehicle", Department = "Sales", Location = "Parking Lot A",
                    DateRegister = DateTime.UtcNow.AddYears(-1), IsActive = true, UserId = adminUser.Id
                },
                new Asset
                {
                    Id = Guid.NewGuid(), Code = "VEH-FOR-01", Name = "Ford Transit Van (Delivery)",
                    Category = "Vehicle", Department = "Logistics", Location = "Warehouse",
                    DateRegister = DateTime.UtcNow.AddYears(-2), IsActive = true, UserId = adminUser.Id
                },

                // --- Miscellaneous Assets ---
                new Asset
                {
                    Id = Guid.NewGuid(), Code = "PROJ-EPS-01", Name = "Epson Projector Model X",
                    Category = "Office Equipment", Department = "Meeting Room 1", Location = "Building A, Floor 1",
                    DateRegister = DateTime.UtcNow.AddMonths(-8), IsActive = true, UserId = adminUser.Id
                },
                new Asset
                {
                    Id = Guid.NewGuid(), Code = "CAM-CAN-01", Name = "Canon EOS R6 Camera",
                    Category = "Media", Department = "Marketing", Location = "Marketing Dept.",
                    DateRegister = DateTime.UtcNow.AddMonths(-5), IsActive = true, UserId = adminUser.Id
                },
                new Asset
                {
                    Id = Guid.NewGuid(), Code = "WBC-LOGI-01", Name = "Logitech C920 Webcam (Old model)",
                    Category = "IT", Department = "Storage", Location = "IT Storage Room",
                    DateRegister = DateTime.UtcNow.AddYears(-3), IsActive = false, UserId = adminUser.Id
                },
                new Asset
                {
                    Id = Guid.NewGuid(), Code = "DSK-IKEA-02", Name = "IKEA Linnmon Desk (Scratched)",
                    Category = "Furniture", Department = "Storage", Location = "Basement",
                    DateRegister = DateTime.UtcNow.AddYears(-4), IsActive = false, UserId = adminUser.Id
                }
            };

            await context.Assets.AddRangeAsync(assets);
            await context.SaveChangesAsync();
        }
    }
}
