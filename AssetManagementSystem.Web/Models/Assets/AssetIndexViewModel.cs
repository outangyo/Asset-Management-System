namespace AssetManagementSystem.Web.Models.Assets
{
    public class AssetIndexViewModel
    {
        public PagedResult<AssetListItemViewModel> PagedAssets { get; set; }
        public AssetListFilterViewModel Filter { get; set; }
    }
}
