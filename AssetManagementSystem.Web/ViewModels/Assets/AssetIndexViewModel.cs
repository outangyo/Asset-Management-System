namespace AssetManagementSystem.Web.ViewModels.Assets
{
    public class AssetIndexViewModel
    {
        public PagedResult<AssetListItemViewModel> PagedAssets { get; set; }
        public AssetListFilterViewModel Filter { get; set; }
    }
}
