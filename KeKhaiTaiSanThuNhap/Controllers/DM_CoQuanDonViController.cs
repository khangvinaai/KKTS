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
using System.Web.Security;
using KeKhaiTaiSanThuNhap.Models;

namespace KeKhaiTaiSanThuNhap.Controllers
{
    [Authorize(Roles = "ADMIN")]
    public class DM_CoQuanDonViController : Controller
    {
        private KSTNEntities db = new KSTNEntities();
        private UserInfo user = new UserInfo();

        public JsonResult GetCoQuan()
        {

            var role = user.GetRole();
            var MaCoQuan = user.GetUserCoQuan();

            if(role == "ADMIN")
            {
                var data = db.DM_CoQuanDonVi.Select(_ => new { MaCoQuan = _.Ma_CoQuan_DonVi, TenCoQuan = _.Ten });
                return Json(data, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var data = db.DM_CoQuanDonVi.Where(_ => _.Ma_CoQuan_DonVi == MaCoQuan).Select(_ => new { MaCoQuan = _.Ma_CoQuan_DonVi, TenCoQuan = _.Ten });
                return Json(data, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult Index()
        {
            if (user.CheckQuyen("DM_CoQuan", "Xem"))
            {
                return new HttpStatusCodeResult(404, "Not found");
            }
            return View();
        }

        [HttpPost]
        public JsonResult Create([Bind(Include = "Ma_CoQuan_DonVi,Ten,MaLoai_CoQuan_DonVi")] DM_CoQuanDonVi dM_CoQuanDonVi)
        {
            if (user.CheckQuyen("DM_CoQuan", "Them"))
            {
                return Json("Không Có Quyền Truy Cập", JsonRequestBehavior.AllowGet);
            }

            db.DM_CoQuanDonVi.Add(dM_CoQuanDonVi);
            db.SaveChanges();
            return Json(dM_CoQuanDonVi.Ten, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Edit([Bind(Include = "Ma_CoQuan_DonVi,Ten,MaLoai_CoQuan_DonVi")] DM_CoQuanDonVi dM_CoQuanDonVi)
        {

            if (user.CheckQuyen("DM_CoQuan", "Sua"))
            {
                return Json("Không Có Quyền Truy Cập", JsonRequestBehavior.AllowGet);
            }

            if (ModelState.IsValid)
            {
                db.Entry(dM_CoQuanDonVi).State = EntityState.Modified;
                db.SaveChanges();
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            return Json(false, JsonRequestBehavior.AllowGet);
        }

        [HttpPost, ActionName("Delete")]
        public JsonResult DeleteConfirmed(int id)
        {
            if (user.CheckQuyen("DM_CoQuan", "Xoa"))
            {
                return Json("Không Có Quyền Truy Cập", JsonRequestBehavior.AllowGet);
            }

            DM_CoQuanDonVi dM_CoQuanDonVi = db.DM_CoQuanDonVi.Find(id);
            var data = new
            {
                id = dM_CoQuanDonVi.Ma_CoQuan_DonVi,
                Ten = dM_CoQuanDonVi.Ten
            };
            db.DM_CoQuanDonVi.Remove(dM_CoQuanDonVi);
            db.SaveChanges();

            return Json(data, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]

        public JsonResult LoadData()
        {
            var draw = Request.Form.GetValues("draw").FirstOrDefault();
            var start = Request.Form.GetValues("start").FirstOrDefault();
            var length = Request.Form.GetValues("length").FirstOrDefault();
            var Ten = Request.Form.GetValues("columns[1][search][value]").FirstOrDefault();

            int pageSize = length != null ? Convert.ToInt32(length) : 0;
            int skip = start != null ? Convert.ToInt32(start) : 0;
            int recordsTotal = 0;

            var data = (from cq in db.DM_CoQuanDonVi
                        join lcq in db.DM_Loai_CoQuan_DonVi on cq.MaLoai_CoQuan_DonVi equals lcq.Ma_Loai_CQDV
                        orderby lcq.Ten_Loai_CQDV ascending, cq.Ten ascending
                        where lcq.Ma_Loai_CQDV != 35
                        select new { cq.Ma_CoQuan_DonVi, cq.Ten, cq.MaLoai_CoQuan_DonVi, lcq.Ten_Loai_CQDV }).ToList();


            if (!string.IsNullOrEmpty(Ten))
            {
                data = data.Where(a => a.Ten.ToUpper().Contains(Ten.ToUpper()) || a.Ten_Loai_CQDV.ToUpper().Contains(Ten.ToUpper())).ToList();
            }

            recordsTotal = data.Count();
            var data1 = data.Skip(skip).Take(pageSize).ToList();
            return Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data1}, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetSuaCoQuanDonVi(int id)
        {
            var data = (from cq in db.DM_CoQuanDonVi
                        where cq.Ma_CoQuan_DonVi == id
                        join lcq in db.DM_Loai_CoQuan_DonVi on cq.MaLoai_CoQuan_DonVi equals lcq.Ma_Loai_CQDV
                        select new { cq.Ma_CoQuan_DonVi, cq.Ten, cq.MaLoai_CoQuan_DonVi, lcq.Ten_Loai_CQDV }).Single();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetCoQuanKeHoach()
        {
            var data = db.DM_CoQuanDonVi.Select(_ => new {MaCoQuan = _.Ma_CoQuan_DonVi, TenCoQuan = _.Ten }).ToList();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetThongTinCoQuanKeHoach(int id)
        {
            var data = db.DM_CoQuanDonVi.Where(_ => _.Ma_CoQuan_DonVi == id).ToList();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetCoQuanTheoID(int? id)
        {
            var data = db.DM_CoQuanDonVi.Where(_ => _.Ma_CoQuan_DonVi == id).Select(_ => new { TenCoQuan = _.Ten }).Single();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetCoQuanSearch()
        {

            var role = user.GetRole();
            var cq = user.GetUserCoQuan();
            var data = db.DM_CoQuanDonVi.Select(_ => new { TenCoQuan = _.Ten, Ma_CoQuan_DonVi = _.Ma_CoQuan_DonVi }).ToList();
            if (role == "ADMIN" || role == "NDDTTT")
            {
                data = data.Where(_ => _.Ma_CoQuan_DonVi != 138).ToList();
            }
            else
            {
                data = data.Where(_ => _.Ma_CoQuan_DonVi == cq).ToList();
            }
                
            return Json(data, JsonRequestBehavior.AllowGet);
        }
    }
}
