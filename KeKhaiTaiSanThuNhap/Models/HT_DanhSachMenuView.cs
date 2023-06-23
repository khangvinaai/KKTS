using System.Collections.Generic;

namespace KeKhaiTaiSanThuNhap.Models
{
    public class HT_DanhSachMenuView
    {
        public string KeyMenu { get; set; }
        public string MenuName { get; set; }
        public string Title { get; set; }
        public string MenuNameAlias { get; set; }
        public string MenuNameContent { get; set; }
        public int? MenuType { get; set; }
        public string Module { get; set; }
        public string PathIcon { get; set; }
        public bool? IsBold { get; set; }
        public bool? IsVisible { get; set; }
        public int? IsGroup { get; set; }
        public string SorOrder { get; set; }
    }
}