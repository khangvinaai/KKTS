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
    public class HT_NhomTaiKhoanController : Controller
    {

        private KSTNEntities db = new KSTNEntities();
        private UserInfo user = new UserInfo();

        public JsonResult GetNhomTaiKhoan()
        {
            var role = user.GetRole();

            if (role == "ADMIN" || role == "NDDTTT")
            {
                var data = db.HT_NhomTaiKhoan.Select(_ => new { MaNhomTaiKhoan = _.MaNhomTaiKhoan, TenNhomTaiKhoan = _.TenNhomTaiKhoan, MaTaiKhoan = _.MaTaiKhoan });
                return Json(data, JsonRequestBehavior.AllowGet);
            }
            else if(role == "NQTCSBN" || role == "NDDCSBN"){
                var data = db.HT_NhomTaiKhoan.Where(_ => _.MaTaiKhoan != "ADMIN" && _.MaTaiKhoan != "NDDTTT").Select(_ => new { MaNhomTaiKhoan = _.MaNhomTaiKhoan, TenNhomTaiKhoan = _.TenNhomTaiKhoan, MaTaiKhoan = _.MaTaiKhoan });
                return Json(data, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var data = db.HT_NhomTaiKhoan.Where(_ => _.MaTaiKhoan == "CBKKTSTN").Select(_ => new { MaNhomTaiKhoan = _.MaNhomTaiKhoan, TenNhomTaiKhoan = _.TenNhomTaiKhoan, MaTaiKhoan = _.MaTaiKhoan });
                return Json(data, JsonRequestBehavior.AllowGet);
            }
        }
        public string getuser()
        {
            HttpCookie authCookie = System.Web.HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName];
            if (authCookie != null)
            {
                if (!string.IsNullOrEmpty(authCookie.Value))
                {
                    FormsAuthenticationTicket authTicket = FormsAuthentication.Decrypt(authCookie.Value);

                    string str = authTicket.UserData;
                    string[] subs = str.Split(',');
                    return subs[1];
                }
            }
            return "";
        }


        // GET: HT_NhomTaiKhoan
        public JsonResult LoadData()
        {
            var draw = Request.Form.GetValues("draw").FirstOrDefault();
            var start = Request.Form.GetValues("start").FirstOrDefault();
            var length = Request.Form.GetValues("length").FirstOrDefault();
            int pageSize = length != null ? Convert.ToInt32(length) : 0;
            int skip = start != null ? Convert.ToInt32(start) : 0;
            int recordsTotal = 0;
            var TenNhomTaiKhoan = Request.Form.GetValues("columns[1][search][value]").FirstOrDefault();
            var data = (from tk in db.HT_NhomTaiKhoan
                        orderby tk.NgayTao
                        select new { tk.MaTaiKhoan, tk.MaNhomTaiKhoan, tk.TenNhomTaiKhoan, tk.Note, tk.TrangThai, tk.NgaySua, tk.NguoiSua, tk.NguoiTao, tk.NgayTao }
                        ).ToList();
          

            recordsTotal = data.Count();
            var data1 = data.Skip(skip).Take(pageSize).ToList();
            return Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data1 }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Index()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult Create([Bind(Include = "MaNhomTaiKhoan,TenNhomTaiKhoan,MaTaiKhoan,Note,TrangThai,NguoiTao,NgayTao,NguoiSua,NgaySua")] HT_NhomTaiKhoan hT_NhomTaiKhoan)
        {
            hT_NhomTaiKhoan.NgaySua = DateTime.Now;
            hT_NhomTaiKhoan.NgayTao = DateTime.Now;
            hT_NhomTaiKhoan.NguoiTao = getuser();
            hT_NhomTaiKhoan.NguoiSua = getuser();

            if(hT_NhomTaiKhoan.TrangThai == null)
            {
                hT_NhomTaiKhoan.TrangThai = false;
            }
            
            db.HT_NhomTaiKhoan.Add(hT_NhomTaiKhoan);
            db.SaveChanges();


            var menu = db.HT_Menu.Select(_ => _.MenuCode).ToList();
            var chucnang = db.HT_ChucNang.Select(_ => _.ChucNangCode).ToList();
            var CTHTPQ = new HT_ChiTietPhanQuyen();

            foreach (var y in menu)
            {
                foreach (var z in chucnang)
                {
                    CTHTPQ.MaTaiKhoan = hT_NhomTaiKhoan.MaTaiKhoan;
                    CTHTPQ.MenuCode = y;
                    CTHTPQ.ChucNangCode = z;
                    CTHTPQ.TrangThai = false;
                    db.HT_ChiTietPhanQuyen.Add(CTHTPQ);
                    db.SaveChanges();
                }
            }

            return Json(hT_NhomTaiKhoan.TenNhomTaiKhoan, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetSuaNhomTaiKhoan(int id)
        {
            var data = (from tk in db.HT_NhomTaiKhoan where tk.MaNhomTaiKhoan == id

                        select new { tk.MaTaiKhoan, tk.MaNhomTaiKhoan, tk.TenNhomTaiKhoan, tk.Note, tk.TrangThai, tk.NgaySua, tk.NguoiSua, tk.NguoiTao, tk.NgayTao }).Single();



            return Json(data, JsonRequestBehavior.AllowGet);
        }
        
        [HttpPost]
        public ActionResult Edit([Bind(Include = "MaNhomTaiKhoan,TenNhomTaiKhoan,MaTaiKhoan,Note,TrangThai,NguoiTao,NgayTao,NguoiSua,NgaySua")] HT_NhomTaiKhoan hT_NhomTaiKhoan)
        {
            var IDuser = hT_NhomTaiKhoan.MaNhomTaiKhoan;

            var tk = getuser();

            hT_NhomTaiKhoan.NgaySua = DateTime.Now;
            hT_NhomTaiKhoan.NguoiSua = tk;

            if (hT_NhomTaiKhoan.TrangThai == null)
            {
                hT_NhomTaiKhoan.TrangThai = false;
            }

            string sqlconnectStr = ConfigurationManager.ConnectionStrings["ASPNETConnectionString"].ToString();
            var cnn = new SqlConnection(sqlconnectStr);
            cnn.Open();

            var sql = $" UPDATE dbo.HT_NhomTaiKhoan SET  TenNhomTaiKhoan = @TenNhomTaiKhoan, MaTaiKhoan = @MaTaiKhoan,  Note = @Note, NgaySua = @NgaySua, NguoiSua = @NguoiSua, TrangThai = @TrangThai WHERE MaNhomTaiKhoan =" + IDuser;

            var command_ins = new SqlCommand(sql, cnn);
            command_ins.Parameters.Add(new SqlParameter("TenNhomTaiKhoan", hT_NhomTaiKhoan.TenNhomTaiKhoan));
            command_ins.Parameters.Add(new SqlParameter("MaTaiKhoan", hT_NhomTaiKhoan.MaTaiKhoan));
            command_ins.Parameters.Add(new SqlParameter("Note", hT_NhomTaiKhoan.Note));
            command_ins.Parameters.Add(new SqlParameter("NgaySua", hT_NhomTaiKhoan.NgaySua));
            command_ins.Parameters.Add(new SqlParameter("NguoiSua", hT_NhomTaiKhoan.NguoiSua));
            command_ins.Parameters.Add(new SqlParameter("TrangThai", hT_NhomTaiKhoan.TrangThai));

            command_ins.ExecuteNonQuery();

            cnn.Close();
            return Json(true, JsonRequestBehavior.AllowGet);
        }

       
        // POST: HT_NhomTaiKhoan/Delete/5
        [HttpPost, ActionName("Delete")]

        public ActionResult DeleteConfirmed(int id)
        {
            HT_NhomTaiKhoan hT_NhomTaiKhoan = db.HT_NhomTaiKhoan.Find(id);
            var data = new
            {
                id = hT_NhomTaiKhoan.MaNhomTaiKhoan,
                TenNhomTaiKhoan = hT_NhomTaiKhoan.TenNhomTaiKhoan,
            };

            db.HT_NhomTaiKhoan.Remove(hT_NhomTaiKhoan);
            db.SaveChanges();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public JsonResult CheckNhomTaiKhoan(string TenNhomTaiKhoan)
        {

            var flag = db.HT_NhomTaiKhoan.Count(x => x.TenNhomTaiKhoan == TenNhomTaiKhoan);

            if (flag > 0)
            {

                return Json(false, JsonRequestBehavior.AllowGet);
            }

            return Json(true, JsonRequestBehavior.AllowGet);
        }


        public JsonResult CheckTenNhomTaiKhoanEdit(string TenNhomTaiKhoan, string TenNhomTaiKhoanEdit)
        {

            var tenNhomTaiKhoanEdit = TenNhomTaiKhoanEdit;
            // int maCamEdit = -1;//nt32.Parse(maCamEdit);
            //  var maCamEdit = Int32.Parse(ViewBag.MaCamEdit);


            if (TenNhomTaiKhoan == TenNhomTaiKhoanEdit)
            {
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            var tk = db.HT_NhomTaiKhoan.Where(_ => _.TenNhomTaiKhoan == TenNhomTaiKhoan);

            if (tk.Count() > 0)
            {

                return Json(false, JsonRequestBehavior.AllowGet);
            }

            return Json(true, JsonRequestBehavior.AllowGet);
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
