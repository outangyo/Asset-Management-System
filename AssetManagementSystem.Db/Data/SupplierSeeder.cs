using AssetManagementSystem.Db.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssetManagementSystem.Db.Data
{
    public static class SupplierSeeder
    {
        public static async Task SeedSuppliersAsync(IServiceProvider services)
        {
            using var scope = services.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            // ตรวจสอบว่ามี Supplier อยู่แล้วหรือยัง
            if (await context.Suppliers.AnyAsync())
            {
                return; // ถ้ามีอยู่แล้ว ก็ไม่ต้องทำอะไร
            }

            // สร้างข้อมูล Supplier ตัวอย่าง 15 รายการ
            var suppliers = new List<Supplier>
            {
                // --- IT Hardware Suppliers (Active) ---
                new Supplier
                {
                    Id = Guid.NewGuid(), SupplierCode = "SUP-DELL", Name = "Dell Technologies Inc.",
                    ContactName = "John Smith", ContactEmail = "sales@dell.com", ContactPhone = "1-800-BUY-DELL",
                    WebsiteURL = "https://www.dell.com", TaxId = "DELL-12345", IsActive = true,
                    DateAdded = DateTime.UtcNow.AddYears(-2), Notes = "Primary hardware vendor."
                },
                new Supplier
                {
                    Id = Guid.NewGuid(), SupplierCode = "SUP-HP", Name = "HP Inc.",
                    ContactName = "Jane Doe", ContactEmail = "support@hp.com", ContactPhone = "1-800-HP-SUPPORT",
                    WebsiteURL = "https://www.hp.com", TaxId = "HP-67890", IsActive = true,
                    DateAdded = DateTime.UtcNow.AddYears(-3), Notes = "For printers and peripherals."
                },
                new Supplier
                {
                    Id = Guid.NewGuid(), SupplierCode = "SUP-LOGI", Name = "Logitech International S.A.",
                    ContactName = "Mike Johnson", ContactEmail = "b2b@logitech.com", ContactPhone = "1-555-LOGITECH",
                    WebsiteURL = "https://www.logitech.com", TaxId = "LOGI-11223", IsActive = true,
                    DateAdded = DateTime.UtcNow.AddMonths(-18),
                },
                
                // --- Furniture Suppliers (Active) ---
                new Supplier
                {
                    Id = Guid.NewGuid(), SupplierCode = "SUP-IKEA", Name = "IKEA Business",
                    ContactName = "Anna Svensson", ContactEmail = "business@ikea.com", ContactPhone = "1-555-IKEA-BIZ",
                    WebsiteURL = "https://www.ikea.com/business", TaxId = "IKEA-44556", IsActive = true,
                    DateAdded = DateTime.UtcNow.AddYears(-1), Notes = "For office desks and common area furniture."
                },
                new Supplier
                {
                    Id = Guid.NewGuid(), SupplierCode = "SUP-STEEL", Name = "Steelcase Inc.",
                    ContactName = "Robert Brown", ContactEmail = "sales@steelcase.com", ContactPhone = "1-555-STEEL",
                    WebsiteURL = "https://www.steelcase.com", TaxId = "STCS-77889", IsActive = true,
                    DateAdded = DateTime.UtcNow.AddYears(-2), Notes = "Ergonomic chairs."
                },

                // --- Office Supplies (Active) ---
                new Supplier
                {
                    Id = Guid.NewGuid(), SupplierCode = "SUP-STPL", Name = "Staples Advantage",
                    ContactName = "Emily Davis", ContactEmail = "advantage@staples.com", ContactPhone = "1-888-STAPLES",
                    WebsiteURL = "https://www.staplesadvantage.com", TaxId = "STPL-99001", IsActive = true,
                    DateAdded = DateTime.UtcNow.AddMonths(-10), Notes = "General office supplies."
                },

                // --- Software Suppliers (Active) ---
                new Supplier
                {
                    Id = Guid.NewGuid(), SupplierCode = "SUP-MSFT", Name = "Microsoft Corporation",
                    ContactName = "Licensing Team", ContactEmail = "licensing@microsoft.com", ContactPhone = "1-800-MICROSOFT",
                    WebsiteURL = "https://www.microsoft.com", TaxId = "MSFT-22334", IsActive = true,
                    DateAdded = DateTime.UtcNow.AddYears(-5), Notes = "Software licensing (Office 365, Windows)."
                },
                new Supplier
                {
                    Id = Guid.NewGuid(), SupplierCode = "SUP-ADOBE", Name = "Adobe Inc.",
                    ContactName = "Creative Sales", ContactEmail = "sales@adobe.com", ContactPhone = "1-800-ADOBE",
                    WebsiteURL = "https://www.adobe.com", TaxId = "ADBE-55667", IsActive = true,
                    DateAdded = DateTime.UtcNow.AddYears(-4), Notes = "Creative Cloud licenses."
                },

                // --- Service Providers (Active) ---
                new Supplier
                {
                    Id = Guid.NewGuid(), SupplierCode = "SVC-CLEAN", Name = "Pro Cleaners Ltd.",
                    ContactName = "Maria Garcia", ContactEmail = "maria@procleaners.com", ContactPhone = "1-555-CLEAN-UP",
                    TaxId = "CLEAN-88990", IsActive = true,
                    DateAdded = DateTime.UtcNow.AddMonths(-24), Notes = "Weekly office cleaning services."
                },
                new Supplier
                {
                    Id = Guid.NewGuid(), SupplierCode = "SVC-MAINT", Name = "Building Maintenance Group",
                    ContactName = "Tom Wilson", ContactEmail = "tom@bmg.com", ContactPhone = "1-555-FIX-IT",
                    TaxId = "BMG-12121", IsActive = true,
                    DateAdded = DateTime.UtcNow.AddMonths(-30), Notes = "HVAC and electrical maintenance."
                },
                new Supplier
                {
                    Id = Guid.NewGuid(), SupplierCode = "SVC-CAFE", Name = "The Corner Cafe (Catering)",
                    ContactName = "Sarah Chen", ContactEmail = "catering@cornercafe.com", ContactPhone = "1-555-CAFE-1",
                    TaxId = "CAFE-78787", IsActive = true,
                    DateAdded = DateTime.UtcNow.AddMonths(-6), Notes = "Catering for team meetings."
                },
                new Supplier
                {
                    Id = Guid.NewGuid(), SupplierCode = "SVC-CONSULT", Name = "Global IT Consultants",
                    ContactName = "David Lee", ContactEmail = "david.lee@gitc.com", ContactPhone = "1-555-CONSULT",
                    WebsiteURL = "https://www.gitc.com", TaxId = "GITC-10101", IsActive = true,
                    DateAdded = DateTime.UtcNow.AddMonths(-8), Notes = "IT strategy consulting."
                },

                // --- Vehicle/Fleet Supplier (Active) ---
                new Supplier
                {
                    Id = Guid.NewGuid(), SupplierCode = "SUP-TOYO", Name = "Toyota Fleet",
                    ContactName = "Fleet Sales Dept", ContactEmail = "fleet@toyota.com", ContactPhone = "1-800-TOY-FLEET",
                    WebsiteURL = "https://www.toyotafleet.com", TaxId = "TOYO-34343", IsActive = true,
                    DateAdded = DateTime.UtcNow.AddYears(-1), Notes = "For company vehicles."
                },
                
                // --- Inactive Suppliers ---
                new Supplier
                {
                    Id = Guid.NewGuid(), SupplierCode = "SUP-OLDIT", Name = "Old Tech Supplies",
                    ContactName = "Bob Smith", ContactEmail = "bob@oldtech.com", ContactPhone = "1-555-OLD-TECH",
                    TaxId = "OLD-56565", IsActive = false, // Inactive
                    DateAdded = DateTime.UtcNow.AddYears(-5), Notes = "No longer in business. Do not use."
                },
                new Supplier
                {
                    Id = Guid.NewGuid(), SupplierCode = "SUP-REMOVED", Name = "A1 Office Furniture (Bankrupt)",
                    ContactName = "N/A", ContactEmail = "info@a1.com", ContactPhone = "N/A",
                    TaxId = "A1-45678", IsActive = false, // Inactive
                    DateAdded = DateTime.UtcNow.AddYears(-4), Notes = "Company went bankrupt."
                }
            };

            await context.Suppliers.AddRangeAsync(suppliers);
            await context.SaveChangesAsync();
        }
    }
}
