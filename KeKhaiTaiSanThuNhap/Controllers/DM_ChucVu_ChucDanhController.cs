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
using KeKhaiTaiSanThuNhap.Models;

namespace KeKhaiTaiSanThuNhap.Controllers
{
    [Authorize(Roles = "ADMIN")]
    public class DM_ChucVu_ChucDanhController : Controller
    {
        private KSTNEntities db = new KSTNEntities();
        private UserInfo user = new UserInfo();
        public JsonResult GetChucVu()
        {
            var data = db.DM_ChucVu_ChucDanh.Select(_ => new { MaChucVu = _.Ma_ChucVu_ChucDanh, TenChucVu = _.Ten_ChucVu_ChucDanh });
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        // GET: DM_ChucVu_ChucDanh
        public ActionResult Index()
        {
            if (user.CheckQuyen("DM_ChucVu", "Xem"))
            {
                return new HttpStatusCodeResult(404, "Not found");
            }
            return View();
        }

        [HttpPost]
        public JsonResult Create([Bind(Include = "Ma_ChucVu_ChucDanh,Ten_ChucVu_ChucDanh")] DM_ChucVu_ChucDanh dM_ChucVu_ChucDanh)
        {
            db.DM_ChucVu_ChucDanh.Add(dM_ChucVu_ChucDanh);
            
            db.SaveChanges();
            return Json(dM_ChucVu_ChucDanh, JsonRequestBehavior.AllowGet);
        }

     
        public JsonResult LoadData()
        {
            var draw = Request.Form.GetValues("draw").FirstOrDefault();
            var start = Request.Form.GetValues("start").FirstOrDefault();
            var length = Request.Form.GetValues("length").FirstOrDefault();
           
            var Ten_ChucVu_ChucDanh = Request.Form.GetValues("columns[1][search][value]").FirstOrDefault();
            int pageSize = length != null ? Convert.ToInt32(length) : 0;
            int skip = start != null ? Convert.ToInt32(start) : 0;
            int recordsTotal = 0;

            var data = (from cv in db.DM_ChucVu_ChucDanh
                        
                        select new { cv.Ma_ChucVu_ChucDanh, cv.Ten_ChucVu_ChucDanh }).ToList();

            try
            {
                var ordercolumn = Request.Form.GetValues("order[0][column]").FirstOrDefault();
                var sortColumn = Request.Form.GetValues("columns[" + ordercolumn + "][data]").FirstOrDefault();
                var sortColumnDir = Request.Form.GetValues("order[0][dir]").FirstOrDefault();

                if (!string.IsNullOrEmpty(Ten_ChucVu_ChucDanh))
                {
                    data = data.Where(a => a.Ten_ChucVu_ChucDanh.ToUpper().Contains(Ten_ChucVu_ChucDanh.ToUpper())).ToList();
                }

                if (sortColumnDir == "asc")
                {
                    if (sortColumn == "Ten_ChucVu_ChucDanh")
                    {
                        data = data.OrderBy(_ => _.Ten_ChucVu_ChucDanh).ToList();
                    }
                }
                else
                {
                    if (sortColumn == "Ten_ChucVu_ChucDanh")
                    {
                        data = data.OrderByDescending(_ => _.Ten_ChucVu_ChucDanh).ToList();
                    }

                }
            }
            catch { }

            
            recordsTotal = data.Count();
            var data1 = data.Skip(skip).Take(pageSize).ToList();
            return Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data1 }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Edit([Bind(Include = "Ma_ChucVu_ChucDanh,Ten_ChucVu_ChucDanh")] DM_ChucVu_ChucDanh dM_ChucVu_ChucDanh)
        {
            if (user.CheckQuyen("DM_ChucVu", "Sua"))
            {
                return Json("Không Có Quyền Truy Cập", JsonRequestBehavior.AllowGet);
            }

            if (ModelState.IsValid)
            {
                db.Entry(dM_ChucVu_ChucDanh).State = EntityState.Modified;
                db.SaveChanges();
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            return Json(false, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetSuaChucVu(int id)
        {
            var data = (from cv in db.DM_ChucVu_ChucDanh
                        where cv.Ma_ChucVu_ChucDanh == id
                        select new { cv.Ma_ChucVu_ChucDanh, cv.Ten_ChucVu_ChucDanh }).Single();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        
        [HttpPost, ActionName("Delete")]
        public JsonResult DeleteConfirmed(int id)
        {

            if (user.CheckQuyen("DM_ChucVu", "Xoa"))
            {
                return Json("Không Có Quyền Truy Cập", JsonRequestBehavior.AllowGet);
            }

            DM_ChucVu_ChucDanh dM_ChucVu_ChucDanh = db.DM_ChucVu_ChucDanh.Find(id);
            var data = new
            {
                id = dM_ChucVu_ChucDanh.Ma_ChucVu_ChucDanh,
                Ten = dM_ChucVu_ChucDanh.Ten_ChucVu_ChucDanh
            };
            db.DM_ChucVu_ChucDanh.Remove(dM_ChucVu_ChucDanh);
            db.SaveChanges();

            return Json(data, JsonRequestBehavior.AllowGet);
        }

    }
}
