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

            // 1. หา Admin User
            var adminUser = await context.Users.FirstOrDefaultAsync(u => u.UserName.StartsWith("pranav"));
            if (adminUser == null)
            {
                Console.WriteLine("Admin user not found, skipping asset seeding.");
                return;
            }

            // 2. เช็คว่า Asset มีหรือยัง (ถ้ามีแล้วก็จบ)
            if (await context.Assets.AnyAsync())
            {
                return;
            }

            // ==========================================
            // PHASE 1: Seed Master Data (Category, Dept, Location)
            // ==========================================

            // --- 3. Seed Categories ---
            if (!await context.Categories.AnyAsync())
            {
                var categories = new List<Category>
                {
                    new Category { Name = "IT", Description = "Computers, Peripherals" },
                    new Category { Name = "Furniture", Description = "Desks, Chairs" },
                    new Category { Name = "Vehicle", Description = "Cars, Vans" },
                    new Category { Name = "Office Equipment", Description = "Projectors, etc." },
                    new Category { Name = "Media", Description = "Cameras, Audio" }
                };
                await context.Categories.AddRangeAsync(categories);
            }

            // --- 4. Seed Departments ---
            if (!await context.Departments.AnyAsync())
            {
                var departments = new List<Department>
                {
                    new Department { Name = "Marketing", Code = "MKT" },
                    new Department { Name = "HR", Code = "HR" },
                    new Department { Name = "Development", Code = "DEV" },
                    new Department { Name = "Finance", Code = "FIN" },
                    new Department { Name = "Storage", Code = "STR" },
                    new Department { Name = "Sales", Code = "SLE" },
                    new Department { Name = "Logistics", Code = "LOG" },
                    new Department { Name = "Meeting Room 1", Code = "MR1" } // หรือจะมองเป็นแผนกกลาง
                };
                await context.Departments.AddRangeAsync(departments);
            }

            // --- 5. Seed Locations ---
            if (!await context.Locations.AnyAsync())
            {
                var locations = new List<Location>
                {
                    new Location { Name = "Building A, Floor 2" },
                    new Location { Name = "Building A, Floor 3" },
                    new Location { Name = "Building A, Floor 1" },
                    new Location { Name = "Building B, Floor 1" },
                    new Location { Name = "Common Area" },
                    new Location { Name = "IT Storage Room" },
                    new Location { Name = "Parking Lot A" },
                    new Location { Name = "Warehouse" },
                    new Location { Name = "Marketing Dept." },
                    new Location { Name = "Basement" }
                };
                await context.Locations.AddRangeAsync(locations);
            }

            // *** สำคัญ: ต้อง Save ก่อน เพื่อให้ได้ ID ของ Master Data มาใช้ ***
            await context.SaveChangesAsync();

            // ==========================================
            // PHASE 2: Load Master Data to Memory (เพื่อเอา ID)
            // ==========================================

            // ดึงข้อมูลกลับมาใส่ Dictionary เพื่อให้ค้นหา ID ได้ง่ายๆ จากชื่อ
            var cats = await context.Categories.ToDictionaryAsync(c => c.Name, c => c.Id);
            var depts = await context.Departments.ToDictionaryAsync(d => d.Name, d => d.Id);
            var locs = await context.Locations.ToDictionaryAsync(l => l.Name, l => l.Id);

            // ==========================================
            // PHASE 3: Seed Assets (เชื่อมโยง ID)
            // ==========================================

            var assets = new List<Asset>
            {
                // --- IT Assets ---
                new Asset
                {
                    Id = Guid.NewGuid(), Code = "LAP-DELL-01", Name = "Dell Latitude 5420 Laptop",
                    CategoryId = cats["IT"], DepartmentId = depts["Marketing"], LocationId = locs["Building A, Floor 2"],
                    DateRegister = DateTime.UtcNow.AddMonths(-6), IsActive = true, UserId = adminUser.Id
                },
                new Asset
                {
                    Id = Guid.NewGuid(), Code = "MON-HP-01", Name = "HP EliteDisplay 24 inch Monitor",
                    CategoryId = cats["IT"], DepartmentId = depts["HR"], LocationId = locs["Building B, Floor 1"],
                    DateRegister = DateTime.UtcNow.AddMonths(-3), IsActive = true, UserId = adminUser.Id
                },
                new Asset
                {
                    Id = Guid.NewGuid(), Code = "KB-LOGI-01", Name = "Logitech MX Keys Keyboard",
                    CategoryId = cats["IT"], DepartmentId = depts["Development"], LocationId = locs["Building A, Floor 3"],
                    DateRegister = DateTime.UtcNow.AddMonths(-2), IsActive = true, UserId = adminUser.Id
                },

                // --- Furniture Assets ---
                new Asset
                {
                    Id = Guid.NewGuid(), Code = "CHR-ERG-01", Name = "Ergonomic Office Chair",
                    CategoryId = cats["Furniture"], DepartmentId = depts["HR"], LocationId = locs["Common Area"],
                    DateRegister = DateTime.UtcNow.AddYears(-2), IsActive = true, UserId = adminUser.Id
                },
                new Asset
                {
                    Id = Guid.NewGuid(), Code = "DSK-STD-01", Name = "Standing Desk, Electric",
                    CategoryId = cats["Furniture"], DepartmentId = depts["Development"], LocationId = locs["Building A, Floor 3"],
                    DateRegister = DateTime.UtcNow.AddMonths(-10), IsActive = true, UserId = adminUser.Id
                },

                // --- Vehicle ---
                new Asset
                {
                    Id = Guid.NewGuid(), Code = "VEH-TOY-01", Name = "Toyota Camry 2022",
                    CategoryId = cats["Vehicle"], DepartmentId = depts["Sales"], LocationId = locs["Parking Lot A"],
                    DateRegister = DateTime.UtcNow.AddYears(-1), IsActive = true, UserId = adminUser.Id
                },

                // --- Inactive / Broken ---
                new Asset
                {
                    Id = Guid.NewGuid(), Code = "LAP-LEN-02", Name = "Lenovo ThinkPad (Broken)",
                    CategoryId = cats["IT"], DepartmentId = depts["Storage"], LocationId = locs["IT Storage Room"],
                    DateRegister = DateTime.UtcNow.AddYears(-2), IsActive = false, UserId = adminUser.Id
                }
            };

            await context.Assets.AddRangeAsync(assets);
            await context.SaveChangesAsync();
        }
    }
}