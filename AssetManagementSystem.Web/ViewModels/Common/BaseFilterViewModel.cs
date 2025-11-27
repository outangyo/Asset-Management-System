namespace AssetManagementSystem.Web.ViewModels.Common
{
    public class BaseFilterViewModel
    {
        // ค่าเริ่มต้น: หน้า 1, แสดง 10 รายการต่อหน้า
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}
