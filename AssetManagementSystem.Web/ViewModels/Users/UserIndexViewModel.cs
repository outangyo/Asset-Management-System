using AssetManagementSystem.Web.ViewModels.Shared;

namespace AssetManagementSystem.Web.ViewModels.Users
{
    public class UserIndexViewModel
    {
        public PagedResult<UserListItemViewModel> PagedUsers { get; set; }

        public UserListFilterViewModel Filter { get; set; }
    }
}
