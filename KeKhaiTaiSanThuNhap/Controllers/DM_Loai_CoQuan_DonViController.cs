using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using KeKhaiTaiSanThuNhap.Hubs;
using KeKhaiTaiSanThuNhap.Models;

namespace KeKhaiTaiSanThuNhap.Controllers
{
    [Authorize(Roles = "ADMIN")]
    public class DM_Loai_CoQuan_DonViController : Controller
    {
        private KSTNEntities db = new KSTNEntities();
        private UserInfo user = new UserInfo();
        public ActionResult Index()
        {
            if (user.CheckQuyen("DM_LoaiCoQuan", "Xem"))
            {
                return new HttpStatusCodeResult(404, "Not found");
            }
            return View();
        }

        [HttpPost]
        public JsonResult Create([Bind(Include = "Ma_Loai_CQDV,Ten_Loai_CQDV")] DM_Loai_CoQuan_DonVi dM_Loai_CoQuan_DonVi)
        {
            if (user.CheckQuyen("DM_LoaiCoQuan", "Them"))
            {
                return Json("Không Có Quyền Truy Cập", JsonRequestBehavior.AllowGet);
            }

            db.DM_Loai_CoQuan_DonVi.Add(dM_Loai_CoQuan_DonVi);
            db.SaveChanges();
            MyHub.ReloadData();
            return Json(dM_Loai_CoQuan_DonVi.Ten_Loai_CQDV, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult LoadData()
        {

            if (user.CheckQuyen("DM_LoaiCoQuan", "Xem"))
            {
                return Json("Không Có Quyền Truy Cập", JsonRequestBehavior.AllowGet);
            }

            var draw = Request.Form.GetValues("draw").FirstOrDefault();
            var start = Request.Form.GetValues("start").FirstOrDefault();
            var length = Request.Form.GetValues("length").FirstOrDefault();
            var Ten_Loai_CQDV = Request.Form.GetValues("columns[1][search][value]").FirstOrDefault();
            
            int pageSize = length != null ? Convert.ToInt32(length) : 0;
            int skip = start != null ? Convert.ToInt32(start) : 0;
            int recordsTotal = 0;

            var data = (from cqdv in db.DM_Loai_CoQuan_DonVi

                        select new { cqdv.Ten_Loai_CQDV, cqdv.Ma_Loai_CQDV }).ToList();

            if (!string.IsNullOrEmpty(Ten_Loai_CQDV))
            {
                data = data.Where(a => a.Ten_Loai_CQDV.ToUpper().Contains(Ten_Loai_CQDV.ToUpper())).ToList();
            }
            recordsTotal = data.Count();
            var data1 = data.Skip(skip).Take(pageSize).ToList();
            return Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data1 }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Edit([Bind(Include = "Ma_Loai_CQDV,Ten_Loai_CQDV")] DM_Loai_CoQuan_DonVi dM_Loai_CoQuan_DonVi)
        {
            if (user.CheckQuyen("DM_LoaiCoQuan", "Sua"))
            {
                return Json("Không Có Quyền Truy Cập", JsonRequestBehavior.AllowGet);
            }

            if (ModelState.IsValid)
            {
                db.Entry(dM_Loai_CoQuan_DonVi).State = EntityState.Modified;
                db.SaveChanges();
                MyHub.ReloadData();
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            return Json(false, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetLoaiCoQuan(int id)
        {
            var data = (from cv in db.DM_Loai_CoQuan_DonVi
                        where cv.Ma_Loai_CQDV == id
                        select new { cv.Ma_Loai_CQDV, cv.Ten_Loai_CQDV }).Single();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetLoaiCoQuanDonVi()
        {
            var data = db.DM_Loai_CoQuan_DonVi.Select(_ => new { MaLoaiCoQuan = _.Ma_Loai_CQDV, TenLoaiCoQuan = _.Ten_Loai_CQDV }).ToList();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [HttpPost, ActionName("Delete")]
        public JsonResult DeleteConfirmed(int id)
        {
            if (user.CheckQuyen("DM_LoaiCoQuan", "Xoa"))
            {
                return Json("Không Có Quyền Truy Cập", JsonRequestBehavior.AllowGet);
            }

            DM_Loai_CoQuan_DonVi dM_Loai_CoQuan_DonVi = db.DM_Loai_CoQuan_DonVi.Find(id);
            var data = new
            {
                id = dM_Loai_CoQuan_DonVi.Ma_Loai_CQDV,
                Ten = dM_Loai_CoQuan_DonVi.Ten_Loai_CQDV
            };
            db.DM_Loai_CoQuan_DonVi.Remove(dM_Loai_CoQuan_DonVi);
            db.SaveChanges();
            MyHub.ReloadData();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
    }
}
