using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace KeKhaiTaiSanThuNhap
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "DM_Loai_KeKhai",
                url: "loai-ke-khai/{action}/{id}",
                defaults: new { controller = "DM_Loai_KeKhai", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
              name: "DM_Loai_CoQuan_DonVi",
              url: "loai-co-quan-don-vi/{action}/{id}",
              defaults: new { controller = "DM_Loai_CoQuan_DonVi", action = "Index", id = UrlParameter.Optional }
          );

            routes.MapRoute(
               name: "DM_CoQuanDonVi",
               url: "co-quan-don-vi/{action}/{id}",
               defaults: new { controller = "DM_CoQuanDonVi", action = "Index", id = UrlParameter.Optional }
           );

            routes.MapRoute(
             name: "DM_ChucVu_ChucDanh",
             url: "chuc-vu/{action}/{id}",
             defaults: new { controller = "DM_ChucVu_ChucDanh", action = "Index", id = UrlParameter.Optional }
         );

            routes.MapRoute(
          name: "DM_CanBo_Edit1",
          url: "cap-nhat-thong-tin/{id}",
          defaults: new { controller = "DM_CanBo", action = "Edit", id = UrlParameter.Optional }
      );

            routes.MapRoute(
           name: "DM_CanBo_Edit",
           url: "can-bo/cap-nhat/{id}",
           defaults: new { controller = "DM_CanBo", action = "Edit", id = UrlParameter.Optional }
       );
            routes.MapRoute(
             name: "DM_CanBo",
             url: "can-bo/{action}/{id}",
             defaults: new { controller = "DM_CanBo", action = "Index", id = UrlParameter.Optional }
         );

            routes.MapRoute(
           name: "NV_LapKeHoachKeKhai",
           url: "lap-ke-hoach-ke-khai/{action}/{id}",
           defaults: new { controller = "NV_LapKeHoachKeKhai", action = "Index", id = UrlParameter.Optional }
       );

            routes.MapRoute(
            name: "NV_DanhSachCanBoKeKhai_ThemCanBo",
            url: "danh-sach-can-bo-ke-khai/lap-danh-sach/{id}",
            defaults: new { controller = "NV_DanhSachCanBoKeKhai", action = "ThemChiTiet_CanBoKeKhai", id = UrlParameter.Optional }
        );

            routes.MapRoute(
           name: "NV_DanhSachCanBoKeKhai_BanIn",
           url: "danh-sach-can-bo-ke-khai/ban-in/{id}",
           defaults: new { controller = "NV_DanhSachCanBoKeKhai", action = "BanIn", id = UrlParameter.Optional }
       );

            routes.MapRoute(
            name: "NV_DanhSachCanBoKeKhai",
            url: "danh-sach-can-bo-ke-khai/{action}/{id}",
            defaults: new { controller = "NV_DanhSachCanBoKeKhai", action = "Index", id = UrlParameter.Optional }
        );


            routes.MapRoute(
          name: "NV_BaoCaoTienDo_BanIn",
          url: "bao-cao-tien-do/ban-in/{id}",
          defaults: new { controller = "NV_BaoCaoTienDo", action = "BanIn", id = UrlParameter.Optional }
      );


            routes.MapRoute(
        name: "NV_BaoCaoTienDo_ThemBaoCao",
url: "bao-cao-ket-qua-ke-khai/them-bao-cao/{id}",
        defaults: new { controller = "NV_BaoCaoTienDo", action = "ThemBaoCao", id = UrlParameter.Optional }
    );
            routes.MapRoute(
          name: "NV_BaoCaoTienDo_XemBaoCao",
          url: "bao-cao-ket-qua-ke-khai/xem-bao-cao/{id}",
          defaults: new { controller = "NV_BaoCaoTienDo", action = "XemBaoCao", id = UrlParameter.Optional }
      );

            routes.MapRoute(
         name: "NV_BaoCaoTienDo_XemChiTiet",
         url: "ket-qua-ke-khai/xem-chi-tiet/{id}",
         defaults: new { controller = "NV_BaoCaoTienDo", action = "XemChiTiet", id = UrlParameter.Optional }
     );

            routes.MapRoute(
             name: "NV_BaoCaoTienDo_XemCoQuan",
             url: "ket-qua-ke-khai/xem-co-quan/{id}",
             defaults: new { controller = "NV_BaoCaoTienDo", action = "XemCoQuan", id = UrlParameter.Optional }
         );
            routes.MapRoute(
             name: "NV_BaoCaoTienDo_ThemBienBan",
             url: "bao-cao-ket-qua-ke-khai/them-bien-ban/{id}",
             defaults: new { controller = "NV_BaoCaoTienDo", action = "ThemBienBan", id = UrlParameter.Optional }
         );



            routes.MapRoute(
               name: "NV_BaoCaoTienDo",
               url: "ket-qua-ke-khai/{action}/{id}",
               defaults: new { controller = "NV_BaoCaoTienDo", action = "Index", id = UrlParameter.Optional }
           );

            routes.MapRoute(
              name: "HT_ChiTietPhanQuyen",
              url: "he-thong-phan-quyen/{action}/{id}",
              defaults: new { controller = "HT_ChiTietPhanQuyen", action = "Index", id = UrlParameter.Optional }
          );


            routes.MapRoute(
                name: "NV_KeKhai_TSTN_Lap",
                url: "ke-khai-tai-san/lap-ban-ke-khai/{id}",
                defaults: new { controller = "NV_KeKhai_TSTN", action = "LapBanKeKhai", id = UrlParameter.Optional }
            );

            routes.MapRoute(
       name: "NV_KeKhai_TSTN_BanIn",
       url: "ke-khai-tai-san/ban-in/{id}",
       defaults: new { controller = "NV_KeKhai_TSTN", action = "BanIn", id = UrlParameter.Optional }

   );

            routes.MapRoute(
          name: "NV_KeKhai_TSTN_Edit",
          url: "ke-khai-tai-san/cap-nhat/{id}",
          defaults: new { controller = "NV_KeKhai_TSTN", action = "Edit", id = UrlParameter.Optional }

      );

            routes.MapRoute(
           name: "NV_BaoCaoKetQuaKeKhai_Inbaocao",
           url: "bao-cao-ket-qua-ke-khai/ban-in/{id}",
           defaults: new { controller = "NV_BaoCaoKetQuaKeKhai", action = "BanIn", id = UrlParameter.Optional }
        );
            routes.MapRoute(
          name: "NV_BaoCaoKetQuaKeKhai_BaoCao",
          url: "bao-cao-ket-qua-ke-khai/bao-cao/{id}",
          defaults: new { controller = "NV_BaoCaoKetQuaKeKhai", action = "BaoCao", id = UrlParameter.Optional }
       );

            routes.MapRoute(
            name: "NV_BaoCaoKetQuaKeKhai_Index",
            url: "bao-cao-ket-qua-ke-khai/{action}/{id}",
            defaults: new { controller = "NV_BaoCaoKetQuaKeKhai", action = "Index", id = UrlParameter.Optional }
        );



            routes.MapRoute(
            name: "NV_KeKhai_TSTN",
            url: "ke-khai-tai-san/{action}/{id}",
            defaults: new { controller = "NV_KeKhai_TSTN", action = "Index", id = UrlParameter.Optional }

        );

            routes.MapRoute(
              name: "HT_NhomTaiKhoan",
              url: "nhom-tai-khoan/{action}/{id}",
              defaults: new { controller = "HT_NhomTaiKhoan", action = "Index", id = UrlParameter.Optional }

          );


            routes.MapRoute(
              name: "NV_LapKeHoachXacMinh_LapKeHoach",
    url: "lap-ke-hoach-xac-minh/lap-ke-hoach/{id}",
              defaults: new { controller = "NV_LapKeHoachXacMinh", action = "LapKeHoach", id = UrlParameter.Optional }

          );
            routes.MapRoute(
     name: "NV_LapKeHoachXacMinh_XemChiTiet",
     url: "lap-ke-hoach-xac-minh/chi-tiet/{id}",
     defaults: new { controller = "NV_LapKeHoachXacMinh", action = "XemChiTiet", id = UrlParameter.Optional }
 );

            routes.MapRoute(
      name: "NV_LapKeHoachXacMinh_BanIn",
      url: "lap-ke-hoach-xac-minh/ban-in/{id}",
      defaults: new { controller = "NV_LapKeHoachXacMinh", action = "BanIn", id = UrlParameter.Optional }
  );

            routes.MapRoute(
          name: "NV_LapKeHoachXacMinh",
          url: "lap-ke-hoach-xac-minh/{action}/{id}",
          defaults: new { controller = "NV_LapKeHoachXacMinh", action = "Index", id = UrlParameter.Optional }

      );




            routes.MapRoute(
         name: "NV_DanhSachBanKeKhai_CanBoDuocXacMinh_BaoCao",
         url: "can-bo-duoc-xac-minh/bao-cao-ket-qua/{id}",
         defaults: new { controller = "NV_DanhSachBanKeKhai", action = "BaoCaoKetQuaXacMinh", id = UrlParameter.Optional });

            routes.MapRoute(
         name: "NV_DanhSachBanKeKhai_CanBoDuocXacMinh",
         url: "ke-hoach-xac-minh/co-quan-duoc-xac-minh/can-bo-duoc-xac-minh/{id}",
         defaults: new { controller = "NV_DanhSachBanKeKhai", action = "DanhSachCanBoDuocXacMinh", id = UrlParameter.Optional });


            routes.MapRoute(
           name: "NV_DanhSachBanKeKhai_CoQuanDuocXacMinh",
           url: "ke-hoach-xac-minh/co-quan-duoc-xac-minh/{id}",
           defaults: new { controller = "NV_DanhSachBanKeKhai", action = "DanhSachCoQuan", id = UrlParameter.Optional }

       );

            routes.MapRoute(
          name: "NV_DanhSachBanKeKhai",
          url: "ke-hoach-xac-minh/{action}/{id}",
          defaults: new { controller = "NV_DanhSachBanKeKhai", action = "Index", id = UrlParameter.Optional }

      );

            routes.MapRoute(
          name: "NV_BaoCaoKetQua_BaoCao",
          url: "bao-cao-ket-qua-xac-minh/bao-cao/{id}",
          defaults: new { controller = "NV_BaoCaoKetQua", action = "BaoCao", id = UrlParameter.Optional }

      );

            routes.MapRoute(
          name: "NV_BaoCaoKetQua",
          url: "bao-cao-ket-qua-xac-minh/{action}/{id}",
          defaults: new { controller = "NV_BaoCaoKetQua", action = "Index", id = UrlParameter.Optional }

      );

            routes.MapRoute(
          name: "BC_KeHoachKeKhai_DanhSachKeHoach_XemChiTiet",
          url: "bao-cao-ke-hoach-ke-khai/ke-hoach-ke-khai/xem-chi-tiet/{id}",
          defaults: new { controller = "BC_KeHoachThanhTra", action = "DanhSachKeHoachKeKhaiCoQuan", id = UrlParameter.Optional }

      );

            routes.MapRoute(
           name: "BC_KeHoachKeKhai_DanhSachKeHoach",
           url: "bao-cao-ke-hoach-ke-khai/ke-hoach-ke-khai/{id}",
           defaults: new { controller = "BC_KeHoachThanhTra", action = "DanhSachKeHoachKeKhaiCoQuan_ChiTiet", id = UrlParameter.Optional }

       );
         

            routes.MapRoute(
              name: "BC_KeHoachThanhTra",
              url: "bao-cao-ke-hoach-ke-khai/{action}/{id}",
              defaults: new { controller = "BC_KeHoachThanhTra", action = "Index", id = UrlParameter.Optional }
          );
        

            routes.MapRoute(
             name: "BC_DonViChuaLapKeHoach",
             url: "bao-cao-don-vi-chua-lap-ke-hoach/{action}/{id}",
             defaults: new { controller = "BC_DonViChuaLapKeHoach", action = "Index", id = UrlParameter.Optional }

         );

            routes.MapRoute(
           name: "NV_XacMinhTaiSanThuNhap",
           url: "xac-minh-tai-san-thu-nhap/{action}/{id}",
           defaults: new { controller = "NV_XacMinhTaiSanThuNhap", action = "Index", id = UrlParameter.Optional }

       );


        //danh sách cán bộ xác minh
        routes.MapRoute(
            name: "NV_LapKeHoachXacMinh_DanhSachCanBoXacMinh_chitiet",
            url: "danh-sach-can-bo-xac-minh/lap-danh-sach/{id}",
            defaults: new { controller = "NV_LapKeHoachXacMinh_DanhSachCanBoXacMinh", action = "LapDanhSach", id = UrlParameter.Optional }
        );
        routes.MapRoute(
            name: "NV_LapKeHoachXacMinh_indanhsach",
            url: "danh-sach-can-bo-xac-minh/ban-in/{id}",
            defaults: new { controller = "NV_LapKeHoachXacMinh_DanhSachCanBoXacMinh", action = "BanIn", id = UrlParameter.Optional }
        );

        routes.MapRoute(
            name: "NV_LapKeHoachXacMinh_DanhSachCanBoXacMinh",
            url: "danh-sach-can-bo-xac-minh/{action}/{id}",
            defaults: new { controller = "NV_LapKeHoachXacMinh_DanhSachCanBoXacMinh", action = "Index", id = UrlParameter.Optional }
        );
    

        routes.MapRoute(
            name: "Default",
            url: "{controller}/{action}/{id}",
            defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
        );


        }
    }
}
