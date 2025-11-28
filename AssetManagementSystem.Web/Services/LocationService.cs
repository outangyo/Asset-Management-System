using AssetManagementSystem.Core.Repositories;
using AssetManagementSystem.Db.Data;
using AssetManagementSystem.Db.Entities;
using AssetManagementSystem.Web.Services.Interfaces;
using AssetManagementSystem.Web.ViewModels.Locations;
using AssetManagementSystem.Web.ViewModels.Shared;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace AssetManagementSystem.Web.Services
{
    public class LocationService : ILocationService
    {
        private readonly ApplicationDbContext _context;
        private readonly IRepository<Location> _locationRepo;

        public LocationService(ApplicationDbContext context, IRepository<Location> locationRepo)
        {
            _context = context;
            _locationRepo = locationRepo;
        }

        // --- 1. Get List ---
        public async Task<LocationIndexViewModel> GetLocationsAsync(LocationListFilterViewModel filter)
        {
            var query = _context.Locations.AsNoTracking();

            // Search by Name or Address
            if (!string.IsNullOrWhiteSpace(filter.Search))
            {
                var searchTerm = filter.Search.Trim().ToLower();
                query = query.Where(l => l.Name.ToLower().Contains(searchTerm)
                                      || (l.Address != null && l.Address.ToLower().Contains(searchTerm)));
            }

            if (filter.IsActive.HasValue)
            {
                query = query.Where(l => l.IsActive == filter.IsActive.Value);
            }

            var totalCount = await query.CountAsync();

            var items = await query
                .OrderBy(l => l.Name)
                .Skip((filter.PageNumber - 1) * filter.PageSize)
                .Take(filter.PageSize)
                .Select(l => new LocationListItemViewModel
                {
                    Id = l.Id,
                    Name = l.Name,
                    Address = l.Address,
                    IsActive = l.IsActive
                })
                .ToListAsync();

            return new LocationIndexViewModel
            {
                Filter = filter,
                PagedLocations = new PagedResult<LocationListItemViewModel>
                {
                    Items = items,
                    TotalCount = totalCount,
                    PageNumber = filter.PageNumber,
                    PageSize = filter.PageSize
                }
            };
        }

        // --- 2. Get For Edit ---
        public async Task<LocationCreateViewModel?> GetForEditAsync(Guid id)
        {
            var location = await _locationRepo.GetByIdAsync(id);
            if (location == null) return null;

            return new LocationCreateViewModel
            {
                Id = location.Id,
                Name = location.Name,
                Address = location.Address,
                IsActive = location.IsActive
            };
        }

        // --- 3. Create ---
        public async Task<IdentityResult> CreateAsync(LocationCreateViewModel model)
        {
            var location = new Location
            {
                Id = Guid.NewGuid(),
                Name = model.Name,
                Address = model.Address,
                IsActive = model.IsActive
            };

            try
            {
                await _locationRepo.CreateAsync(location);
                await _context.SaveChangesAsync();
                return IdentityResult.Success;
            }
            catch (Exception ex)
            {
                return IdentityResult.Failed(new IdentityError { Description = ex.Message });
            }
        }

        // --- 4. Update ---
        public async Task<IdentityResult> UpdateAsync(LocationCreateViewModel model)
        {
            var location = await _locationRepo.GetByIdAsync(model.Id);
            if (location == null) return IdentityResult.Failed(new IdentityError { Description = "Location not found" });

            location.Name = model.Name;
            location.Address = model.Address;
            location.IsActive = model.IsActive;

            try
            {
                _locationRepo.Update(location);
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
            var location = await _locationRepo.GetByIdAsync(id);
            if (location == null) return IdentityResult.Failed(new IdentityError { Description = "Location not found" });

            try
            {
                _locationRepo.Delete(location);
                await _context.SaveChangesAsync();
                return IdentityResult.Success;
            }
            catch (Exception ex)
            {
                return IdentityResult.Failed(new IdentityError { Description = "Cannot delete location because it is being used by assets." });
            }
        }
    }
}