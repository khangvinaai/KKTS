//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace KeKhaiTaiSanThuNhap.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class NV_BaoCaoTienDo
    {
        public int ID { get; set; }
        public Nullable<int> ID_CoQuan { get; set; }
        public string NoiDung { get; set; }
        public string TenFile { get; set; }
        public string TenBaoCao { get; set; }
        public Nullable<int> NamBaoCao { get; set; }
        public Nullable<int> LoaiKeHoachKeKhai { get; set; }
        public Nullable<bool> TrangThai { get; set; }
    }
}
