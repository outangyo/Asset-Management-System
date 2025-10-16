using AssetManagementSystem.Db.Data;
using Microsoft.AspNetCore.Mvc;

namespace AssetManagementSystem.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DataSeedingController : ControllerBase
    {
        private readonly IServiceProvider _services;
        public DataSeedingController(IServiceProvider services)
        {
            _services = services;
        }

        [HttpPost("seed-dummy-users")]
        public async Task<IActionResult> SeedDummyUsers()
        {
            await IdentityUserSeeder.SeedUsersAsync(_services);
            return Ok("Dummy users have been seeded successfully.");
        }

        [HttpPost("seed-dummy-assets")]
        public async Task<IActionResult> SeedDummyAssets()
        {
            await AssetSeeder.SeedAssetsAsync(_services);
            return Ok("Dummy assets have been seeded successfully.");
        }
    }
}