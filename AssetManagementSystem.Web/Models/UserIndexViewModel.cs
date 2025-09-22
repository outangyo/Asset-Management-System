using AssetManagementSystem.Web.Models.Users;

namespace AssetManagementSystem.Web.Models
{
    public class UserIndexViewModel
    {
        public PagedResult<UserListItemViewModel> PagedUsers { get; set; }

        public UserListFilterViewModel Filter { get; set; }
    }
}
