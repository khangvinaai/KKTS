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
    public class HT_TaiKhoanController : Controller
    {
        private KSTNEntities db = new KSTNEntities();

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
        public ActionResult Index()
        {
            return View(db.HT_TaiKhoan.ToList());
        }

        public JsonResult LoadData()
        {
            var draw = Request.Form.GetValues("draw").FirstOrDefault();
            var start = Request.Form.GetValues("start").FirstOrDefault();
            var length = Request.Form.GetValues("length").FirstOrDefault();
            int pageSize = length != null ? Convert.ToInt32(length) : 0;
            int skip = start != null ? Convert.ToInt32(start) : 0;
            int recordsTotal = 0;
            var HoTen = Request.Form.GetValues("columns[1][search][value]").FirstOrDefault();

            var country = Request.Form.GetValues("columns[3][search][value]").FirstOrDefault();
            var data = (from tk in db.HT_TaiKhoan
                        join cb in db.DM_CanBo on tk.Ma_CanBo equals cb.Ma_CanBo
                        join ntk in db.HT_NhomTaiKhoan on tk.MaNhomTaiKhoan equals ntk.MaNhomTaiKhoan
                        select new { tk.MaTaiKhoan, cb.HoTen, tk.TenTaiKhoan, tk.MatKhau, ntk.TenNhomTaiKhoan, tk.NgaySua, tk.NguoiSua, tk.NguoiTao, tk.NgayTao }).ToList();
            if (!string.IsNullOrEmpty(HoTen))
            {
                data = data.Where(a => a.HoTen.ToUpper().Contains(HoTen.ToUpper())).ToList();
            }
            var ordercolumn = Request.Form.GetValues("order[0][column]").FirstOrDefault();
            var sortColumn = Request.Form.GetValues("columns[" + ordercolumn + "][data]").FirstOrDefault();
            var sortColumnDir = Request.Form.GetValues("order[0][dir]").FirstOrDefault();
            recordsTotal = data.Count();
            var data1 = data.Skip(skip).Take(pageSize).ToList();
            return Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data1 }, JsonRequestBehavior.AllowGet);
        }
     
        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult Create([Bind(Include = "ID,MaTaiKhoan,Ma_CanBo,TenTaiKhoan,MatKhau,MaNhomTaiKhoan,TenNhomTaiKhoan,TrangThai,NguoiTao,NgayTao,NguoiSua,NgaySua")] HT_TaiKhoan hT_TaiKhoan)
        {
            hT_TaiKhoan.NgaySua = DateTime.Now;
            hT_TaiKhoan.NgayTao = DateTime.Now;
            hT_TaiKhoan.NguoiTao = getuser();
            hT_TaiKhoan.NguoiSua = getuser();
            hT_TaiKhoan.CheckPass = false;
            db.HT_TaiKhoan.Add(hT_TaiKhoan);
            db.SaveChanges();
            return Json(hT_TaiKhoan.TenTaiKhoan, JsonRequestBehavior.AllowGet);
        }
     
        public JsonResult GetSuaTaiKhoan(int id)
        {
            var matk = id.ToString();
            var data = (from tk in db.HT_TaiKhoan
                        join cb in db.DM_CanBo on tk.Ma_CanBo equals cb.Ma_CanBo
                        where tk.MaTaiKhoan == matk
                        select new {MaTaiKhoan = tk.ID, TenTaiKhoan = tk.TenTaiKhoan, MaCoQuan = cb.Ma_CoQuan_DonVi, MaCanBo = cb.Ma_CanBo, MaNhomTaiKhoan = tk.MaNhomTaiKhoan }).Single();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        // POST: HT_TaiKhoan/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,MaTaiKhoan,Ma_CanBo,TenTaiKhoan,MatKhau,MaNhomTaiKhoan,TenNhomTaiKhoan,TrangThai,NguoiTao,NgayTao,NguoiSua,NgaySua")] HT_TaiKhoan hT_TaiKhoan)
        {
            var IDuser = hT_TaiKhoan.ID;
            
            var tk = getuser();

            hT_TaiKhoan.NgaySua = DateTime.Now;
            hT_TaiKhoan.NguoiSua = tk;
            string sqlconnectStr = ConfigurationManager.ConnectionStrings["ASPNETConnectionString"].ToString();
            var cnn = new SqlConnection(sqlconnectStr);
            cnn.Open();

            var sql = $" UPDATE dbo.HT_TaiKhoan SET  TenTaiKhoan = @TenTaiKhoan, MatKhau = @MatKhau,  NgaySua = @NgaySua, NguoiSua = @NguoiSua, MaNhomTaiKhoan=@MaNhomTaiKhoan WHERE ID =" + IDuser;

            var command_ins = new SqlCommand(sql, cnn);
            command_ins.Parameters.Add(new SqlParameter("TenTaiKhoan", hT_TaiKhoan.TenTaiKhoan));
            command_ins.Parameters.Add(new SqlParameter("MatKhau", hT_TaiKhoan.MatKhau));
            command_ins.Parameters.Add(new SqlParameter("NgaySua", hT_TaiKhoan.NgaySua));
            command_ins.Parameters.Add(new SqlParameter("NguoiSua", hT_TaiKhoan.NguoiSua));
            command_ins.Parameters.Add(new SqlParameter("MaNhomTaiKhoan", hT_TaiKhoan.MaNhomTaiKhoan));




            command_ins.ExecuteNonQuery();

            cnn.Close();
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        // GET: HT_TaiKhoan/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HT_TaiKhoan hT_TaiKhoan = db.HT_TaiKhoan.Find(id);
            if (hT_TaiKhoan == null)
            {
                return HttpNotFound();
            }
            return View(hT_TaiKhoan);
        }

        // POST: HT_TaiKhoan/Delete/5
        [HttpPost, ActionName("Delete")]
  
        public ActionResult DeleteConfirmed(int id)
        {
            HT_TaiKhoan hT_TaiKhoan = db.HT_TaiKhoan.Find(id);
            var data = new
            {
                id = hT_TaiKhoan.ID,
                TenTaiKhoan = hT_TaiKhoan.TenTaiKhoan,
            };

            db.HT_TaiKhoan.Remove(hT_TaiKhoan);
            db.SaveChanges();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public JsonResult CheckTaiKhoan(string TenTaiKhoan)
        {

            var flag = db.HT_TaiKhoan.Count(x => x.TenTaiKhoan == TenTaiKhoan);

            if (flag > 0)
            {

                return Json(false, JsonRequestBehavior.AllowGet);
            }

            return Json(true, JsonRequestBehavior.AllowGet);
        }

        public JsonResult CheckTaiKhoan1(string TenTaiKhoan, int Ma_CanBo)
        {

            var flag = (from cb in db.DM_CanBo
                        join tk in db.HT_TaiKhoan on cb.Ma_CanBo equals tk.Ma_CanBo
                        where tk.TenTaiKhoan == TenTaiKhoan && cb.Ma_CanBo != Ma_CanBo
                        select tk.TenTaiKhoan).Count();

            if (flag > 0)
            {

                return Json(false, JsonRequestBehavior.AllowGet);
            }

            return Json(true, JsonRequestBehavior.AllowGet);
        }


        public JsonResult CheckMatKhau(string MatKhau1)
        {
            var idCanBo = 0;
            HttpCookie authCookie = System.Web.HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName];
            if (authCookie != null)
            {
                if (!string.IsNullOrEmpty(authCookie.Value))
                {
                    FormsAuthenticationTicket authTicket = FormsAuthentication.Decrypt(authCookie.Value);
                    string str = authTicket.UserData;
                    string[] subs = str.Split(',');
                    idCanBo = Int32.Parse(subs[0]);
                }
            }

            var taikhoan = db.HT_TaiKhoan.Where(_ => _.Ma_CanBo == idCanBo).FirstOrDefault();

            if(taikhoan.MatKhau == MatKhau1)
            {
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }

        }
        public JsonResult DoiMatKhau(string MatKhau1, string MatKhauMoi, string ReMatKhauMoi)
        {
            var idCanBo = 0;
            HttpCookie authCookie = System.Web.HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName];
            if (authCookie != null)
            {
                if (!string.IsNullOrEmpty(authCookie.Value))
                {
                    FormsAuthenticationTicket authTicket = FormsAuthentication.Decrypt(authCookie.Value);
                    string str = authTicket.UserData;
                    string[] subs = str.Split(',');
                    idCanBo = Int32.Parse(subs[0]);
                }
            }

            var taikhoan = db.HT_TaiKhoan.Where(_ => _.Ma_CanBo == idCanBo).FirstOrDefault();

            if (taikhoan.MatKhau == MatKhau1)
            {
                if(MatKhauMoi == ReMatKhauMoi)
                {
                    taikhoan.MatKhau = MatKhauMoi;
                    taikhoan.CheckPass = true;
                    db.Entry(taikhoan).State = EntityState.Modified;
                    db.SaveChanges();
                    return Json(true, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(false, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }

        }




        public JsonResult CheckTenTaiKhoanEdit(string TenTaiKhoan, string TenTaiKhoanEdit)
        {

            var tenTaiKhoanEdit = TenTaiKhoanEdit;
            // int maCamEdit = -1;//nt32.Parse(maCamEdit);
            //  var maCamEdit = Int32.Parse(ViewBag.MaCamEdit);

            if (TenTaiKhoan == TenTaiKhoanEdit)
            {
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            var tk = db.HT_TaiKhoan.Where(_ => _.TenTaiKhoan == TenTaiKhoan);

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

        //Hoàng chỉnh sửa ngày 24/2/2022
        //Cấp tài khoản tự động cho cán bộ
        [HttpGet]
        public JsonResult GetCanBoChuaCoTaiKhoan()
        {
            var draw = Request.Form.GetValues("draw").FirstOrDefault();
            var start = Request.Form.GetValues("start").FirstOrDefault();
            var length = Request.Form.GetValues("length").FirstOrDefault();
            int pageSize = length != null ? Convert.ToInt32(length) : 0;
            int skip = start != null ? Convert.ToInt32(start) : 0;
            int recordsTotal = 0;

            var data = (from tk in db.HT_TaiKhoan
                        join cb in db.DM_CanBo on tk.Ma_CanBo equals cb.Ma_CanBo
                        join ntk in db.HT_NhomTaiKhoan on tk.MaNhomTaiKhoan equals ntk.MaNhomTaiKhoan
                        select new { tk.MaTaiKhoan, cb.HoTen, tk.TenTaiKhoan, tk.MatKhau, ntk.TenNhomTaiKhoan, tk.NgaySua, tk.NguoiSua, tk.NguoiTao, tk.NgayTao }).ToList();


            recordsTotal = data.Count();
            var data1 = data.Skip(skip).Take(pageSize).ToList();
            return Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data1 }, JsonRequestBehavior.AllowGet);
        }

    }
}
