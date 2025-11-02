using AssetManagementSystem.Web.ViewModels.Users;

namespace AssetManagementSystem.Web.ViewModels
{
    public class UserIndexViewModel
    {
        public PagedResult<UserListItemViewModel> PagedUsers { get; set; }

        public UserListFilterViewModel Filter { get; set; }
    }
}
