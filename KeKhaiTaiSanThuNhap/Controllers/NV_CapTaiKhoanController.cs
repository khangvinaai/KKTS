using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using KeKhaiTaiSanThuNhap.Models;


namespace KeKhaiTaiSanThuNhap.Controllers
{
    [Authorize(Roles = "ADMIN")]
    public class NV_CapTaiKhoanController : Controller
    {
        private KSTNEntities db = new KSTNEntities();

        public int getUserID()
        {
            var MaCanBo = 0;
            try
            {
                HttpCookie authCookie = System.Web.HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName];
                if (authCookie != null)
                {
                    if (!string.IsNullOrEmpty(authCookie.Value))
                    {
                        FormsAuthenticationTicket authTicket = FormsAuthentication.Decrypt(authCookie.Value);

                        string str = authTicket.UserData;
                        string[] subs = str.Split(',');
                        MaCanBo = Int32.Parse(subs[0]);

                    }
                }

                return MaCanBo;
            }
            catch
            {
                return MaCanBo;
            }
        }
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult LoadData()
        {
            var useID = getUserID();
            var MaCQ = db.DM_CanBo.Find(useID).Ma_CoQuan_DonVi;

            var draw = Request.Form.GetValues("draw").FirstOrDefault();
            var start = Request.Form.GetValues("start").FirstOrDefault();
            var length = Request.Form.GetValues("length").FirstOrDefault();
            int pageSize = length != null ? Convert.ToInt32(length) : 0;
            int skip = start != null ? Convert.ToInt32(start) : 0;
            int recordsTotal = 0;

            var data = (from ctk in db.NV_CapTaiKhoan
                        join cb in db.DM_CanBo on ctk.NguoiCap equals cb.Ma_CanBo
                        join cq in db.DM_CoQuanDonVi on cb.Ma_CoQuan_DonVi equals cq.Ma_CoQuan_DonVi
                        orderby ctk.NgayCap descending
                        where cb.Ma_CanBo == useID
                        select new {ctk.ID, cb.HoTen, cq.Ten , NgayCap = ctk.NgayCap, FileCap = ctk.FileCap}).ToList();

            recordsTotal = data.Count();
            var data1 = data.Skip(skip).Take(pageSize).ToList();
            return Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data1 }, JsonRequestBehavior.AllowGet);  
        }

        public FileResult Download(int id)
        {
            var CTT = db.NV_CapTaiKhoan.Single(_ => _.ID == id);
            var url = Path.Combine(Server.MapPath("~/Content/uploads"), CTT.FileCap);
            byte[] fileBytes = System.IO.File.ReadAllBytes(url);
            string fileName = CTT.FileCap;
            return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
        }
    }
}

   
