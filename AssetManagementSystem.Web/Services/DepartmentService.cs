using AssetManagementSystem.Core.Repositories;
using AssetManagementSystem.Db.Data;
using AssetManagementSystem.Db.Entities;
using AssetManagementSystem.Web.Services.Interfaces;
using AssetManagementSystem.Web.ViewModels.Departments;
using AssetManagementSystem.Web.ViewModels.Shared;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace AssetManagementSystem.Web.Services
{
    public class DepartmentService : IDepartmentService
    {
        private readonly ApplicationDbContext _context;
        private readonly IRepository<Department> _departmentRepo;

        public DepartmentService(ApplicationDbContext context, IRepository<Department> departmentRepo)
        {
            _context = context;
            _departmentRepo = departmentRepo;
        }

        // --- 1. Get List ---
        public async Task<DepartmentIndexViewModel> GetDepartmentsAsync(DepartmentListFilterViewModel filter)
        {
            var query = _context.Departments.AsNoTracking();

            if (!string.IsNullOrWhiteSpace(filter.Search))
            {
                var searchTerm = filter.Search.Trim().ToLower();
                query = query.Where(d => d.Name.ToLower().Contains(searchTerm)
                                      || (d.Code != null && d.Code.ToLower().Contains(searchTerm)));
            }

            if (filter.IsActive.HasValue)
            {
                query = query.Where(d => d.IsActive == filter.IsActive.Value);
            }

            var totalCount = await query.CountAsync();

            var items = await query
                .OrderBy(d => d.Name)
                .Skip((filter.PageNumber - 1) * filter.PageSize)
                .Take(filter.PageSize)
                .Select(d => new DepartmentListItemViewModel
                {
                    Id = d.Id,
                    Name = d.Name,
                    Code = d.Code,
                    IsActive = d.IsActive
                })
                .ToListAsync();

            return new DepartmentIndexViewModel
            {
                Filter = filter,
                PagedDepartments = new PagedResult<DepartmentListItemViewModel>
                {
                    Items = items,
                    TotalCount = totalCount,
                    PageNumber = filter.PageNumber,
                    PageSize = filter.PageSize
                }
            };
        }

        // --- 2. Get For Edit ---
        public async Task<DepartmentCreateViewModel?> GetForEditAsync(Guid id)
        {
            var department = await _departmentRepo.GetByIdAsync(id);
            if (department == null) return null;

            return new DepartmentCreateViewModel
            {
                Id = department.Id,
                Name = department.Name,
                Code = department.Code,
                IsActive = department.IsActive
            };
        }

        // --- 3. Create ---
        public async Task<IdentityResult> CreateAsync(DepartmentCreateViewModel model)
        {
            var department = new Department
            {
                Id = Guid.NewGuid(),
                Name = model.Name,
                Code = model.Code?.Trim().ToUpper(),
                IsActive = model.IsActive
            };

            // เช็คก่อนว่า Code นี้มีคนใช้หรือยัง (ถ้า Code ไม่ว่าง)
            if (!string.IsNullOrEmpty(model.Code))
            {
                bool isDuplicate = await _context.Departments
                    .AnyAsync(d => d.Code == model.Code);

                if (isDuplicate)
                {
                    return IdentityResult.Failed(new IdentityError
                    {
                        Description = $"Department Code '{model.Code}' is already taken."
                    });
                }
            }

            try
            {
                await _departmentRepo.CreateAsync(department);
                await _context.SaveChangesAsync();
                return IdentityResult.Success;
            }
            catch (Exception ex)
            {
                return IdentityResult.Failed(new IdentityError { Description = ex.Message });
            }
        }

        // --- 4. Update ---
        public async Task<IdentityResult> UpdateAsync(DepartmentCreateViewModel model)
        {
            var department = await _departmentRepo.GetByIdAsync(model.Id);
            if (department == null) return IdentityResult.Failed(new IdentityError { Description = "Department not found" });

            department.Name = model.Name;
            department.Code = model.Code;
            department.IsActive = model.IsActive;

            try
            {
                _departmentRepo.Update(department);
                await _context.SaveChangesAsync();
                return IdentityResult.Success;
            }
            catch (Exception ex)
            {
                return IdentityResult.Failed(new IdentityError { Description = ex.Message });
            }
        }

        // --- 5. Delete ---
        public async Task<IdentityResult> DeleteAsync(Guid id)
        {
            var department = await _departmentRepo.GetByIdAsync(id);
            if (department == null) return IdentityResult.Failed(new IdentityError { Description = "Department not found" });

            try
            {
                _departmentRepo.Delete(department);
                await _context.SaveChangesAsync();
                return IdentityResult.Success;
            }
            catch (Exception ex)
            {
                // จัดการกรณีที่มี Asset ผูกอยู่แล้วลบไม่ได้
                return IdentityResult.Failed(new IdentityError { Description = "Cannot delete department because it is being used." });
            }
        }
    }
}