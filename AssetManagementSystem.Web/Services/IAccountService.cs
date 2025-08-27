using AssetManagementSystem.Web.Models;
using Microsoft.AspNetCore.Identity;

namespace AssetManagementSystem.Web.Services
{
    public interface IAccountService
    {
        Task<IdentityResult> RegisterUserAsync(RegisterViewModel model);
        Task<IdentityResult> ConfirmEmailAsync(Guid userId, string token);
        Task<SignInResult> LoginUserAsync(LoginViewModel model);
        Task LogoutUserAsync();
        Task SendEmailConfirmationAsync(string email);
        Task<ProfileViewModel> GetUserProfileByEmailAsync(string email);
    }
}
