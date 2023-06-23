using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using KeKhaiTaiSanThuNhap.Models;

namespace KeKhaiTaiSanThuNhap.Controllers
{
    [Authorize(Roles = "ADMIN")]
    public class HT_ChiTietPhanQuyenController : Controller
    {
        private KSTNEntities db = new KSTNEntities();

        // GET: HT_ChiTietPhanQuyen
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create()
        {
            if (ModelState.IsValid)
            {
                var nhomtaikhoan = db.HT_NhomTaiKhoan.Select(_ => _.MaTaiKhoan).ToList();
                var menu = db.HT_Menu.Select(_ => _.MenuCode).ToList();
                var chucnang = db.HT_ChucNang.Select(_ => _.ChucNangCode).ToList();
                var CTHTPQ = new HT_ChiTietPhanQuyen();
                foreach(var x in nhomtaikhoan)
                {
                    foreach(var y in menu)
                    {
                        foreach (var z in chucnang)
                        {
                            CTHTPQ.MaTaiKhoan = x;
                            CTHTPQ.MenuCode = y;
                            CTHTPQ.ChucNangCode = z;
                            CTHTPQ.TrangThai = false;
                            db.HT_ChiTietPhanQuyen.Add(CTHTPQ);
                            db.SaveChanges();
                        }
                    }
                }
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            return Json(false, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Edit(List<int> ID, string NhomTaiKhoan)
        {
            if (ModelState.IsValid)
            {
                var deleNhomTaiKhoan = db.HT_ChiTietPhanQuyen.Where(_ => _.MaTaiKhoan == NhomTaiKhoan).ToList();
                foreach (var i in deleNhomTaiKhoan)
                {
                    i.TrangThai = false;
                    db.Entry(i).State = EntityState.Modified;
                    db.SaveChanges();
                }
                foreach (var i in ID)
                {
                    var HT_PhanQuyen = db.HT_ChiTietPhanQuyen.Find(i);
                    if (HT_PhanQuyen != null)
                    {
                        HT_PhanQuyen.TrangThai = true;
                    }
                    db.Entry(HT_PhanQuyen).State = EntityState.Modified;
                    db.SaveChanges();
                }
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            return Json(false, JsonRequestBehavior.AllowGet);
        }


        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            HT_ChiTietPhanQuyen hT_ChiTietPhanQuyen = db.HT_ChiTietPhanQuyen.Find(id);
            db.HT_ChiTietPhanQuyen.Remove(hT_ChiTietPhanQuyen);
            db.SaveChanges();
            return RedirectToAction("Index");
        }



        public ActionResult GetSuaChiTietPhanQuyen(int id)
        {
            var data = db.HT_ChiTietPhanQuyen.Find(id);
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetPhanQuyenByMenuCode(string MenuCode, string MaNhomQuyen)
        {
            var data = db.HT_ChiTietPhanQuyen.Where(_ => _.MenuCode == MenuCode && _.MaTaiKhoan == MaNhomQuyen).ToList();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetChiTietPhanQuyen()
        {
            var data = db.HT_ChiTietPhanQuyen.Select(_ => new { _.ID, _.MaTaiKhoan, _.ChucNangCode, _.MenuCode, _.TrangThai }).ToList();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetNameMenu(string MenuCode)
        {
            var data = db.HT_Menu.Where(_ => _.MenuCode == MenuCode).FirstOrDefault();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public ActionResult LoadData()
        {
                var data = (from ctpq in db.HT_ChiTietPhanQuyen
                            join cn in db.HT_ChucNang on ctpq.ChucNangCode equals cn.ChucNangCode
                            join ntk in db.HT_NhomTaiKhoan on ctpq.MaTaiKhoan equals ntk.MaTaiKhoan
                            join mn in db.HT_Menu on ctpq.MenuCode equals mn.MenuCode
                            orderby mn.ID,cn.ID
                            select new { ctpq.ID, ctpq.MaTaiKhoan, ctpq.ChucNangCode, ctpq.MenuCode, ctpq.TrangThai, cn.TenChucNang, mn.TenMenu }).ToList();
                return Json( data  , JsonRequestBehavior.AllowGet);

           
        }

        
    }
}
