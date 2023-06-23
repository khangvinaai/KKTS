using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using KeKhaiTaiSanThuNhap.Models;
namespace KeKhaiTaiSanThuNhap.Controllers
{
    [Authorize(Roles = "ADMIN")]
    public class SharedController : Controller
    {
        // GET: Shared
        private KSTNEntities db = new KSTNEntities();
        private UserInfo user = new UserInfo();

        [ChildActionOnly]
        public ActionResult RenderUserInfo()
        {
            int userID = user.GetUser();
            var thongbao = db.HT_ThongBao.Where(_ => _.NguoiNhan == userID && _.TrangThai == true).Count();
            ViewBag.DoiMatKhau = (db.HT_TaiKhoan.Single(_ => _.Ma_CanBo == userID).CheckPass == false );
            ViewBag.thongbao = thongbao;

            ViewBag.Id = user.GetUser();
            var MaChucDanh = db.DM_CanBo.Find(userID).Ma_ChucVu_ChucDanh;
            var TenChucDanh = db.DM_ChucVu_ChucDanh.FirstOrDefault(_ => _.Ma_ChucVu_ChucDanh == MaChucDanh).Ten_ChucVu_ChucDanh;
            ViewBag.Role = user.GetRole();
            ViewBag.TenUser = db.DM_CanBo.Find(userID).HoTen;
            return PartialView("_LayoutUserInfo");
        }


        [ChildActionOnly]
        public ActionResult RenderMenu()
        {
            string userRole = user.GetRole();
            ViewBag.GetRole = user.GetRole();
            ViewBag.ListQuyen = db.HT_ChiTietPhanQuyen.Where(_ => _.MaTaiKhoan == userRole && _.TrangThai == true).Select(_ => _.MenuCode + "_" + _.ChucNangCode).ToList();
            return PartialView("TreeviewLeftMenu");
        }

        public JsonResult GetCanBo(int? id)
        {
            var TenCanBo = db.DM_CanBo.Find(id).HoTen;
            return Json(TenCanBo, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetCoQuan(int? id)
        {
            var TenCoQuan = db.DM_CoQuanDonVi.Find(id).Ten;
            return Json(TenCoQuan, JsonRequestBehavior.AllowGet);
        }

    }
}
