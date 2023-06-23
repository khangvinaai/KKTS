using KeKhaiTaiSanThuNhap.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace KeKhaiTaiSanThuNhap.Controllers
{
    [Authorize(Roles = "ADMIN")]
    public class HT_ThongBaoController : Controller
    {

        private KSTNEntities db = new KSTNEntities();

        public int getuser()
        {
            HttpCookie authCookie = System.Web.HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName];
            if (authCookie != null)
            {
                if (!string.IsNullOrEmpty(authCookie.Value))
                {
                    FormsAuthenticationTicket authTicket = FormsAuthentication.Decrypt(authCookie.Value);

                    string str = authTicket.UserData;
                    string[] subs = str.Split(',');
                    return Int32.Parse(subs[0]);
                }
            }
            return 0;
        }

        // GET: HT_ThongBao
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetThongBao()
        {
            int user = getuser();
            var data1 = (from tb in db.HT_ThongBao
                         where tb.NguoiNhan == user && tb.TrangThai == false
                         join cb in db.DM_CanBo on tb.NguoiGui equals cb.Ma_CanBo
                         join cq in db.DM_CoQuanDonVi on cb.Ma_CoQuan_DonVi equals cq.Ma_CoQuan_DonVi
                         join cv in db.DM_ChucVu_ChucDanh on cb.Ma_ChucVu_ChucDanh equals cv.Ma_ChucVu_ChucDanh
                         orderby tb.ThoiGian descending
                         select new {tb = tb, tennguoigui = cb.HoTen ,cq.Ten, cv.Ten_ChucVu_ChucDanh});

            var data2 = (from tb in db.HT_ThongBao
                         where tb.NguoiNhan == user && tb.TrangThai == true
                         join cb in db.DM_CanBo on tb.NguoiGui equals cb.Ma_CanBo
                         join cq in db.DM_CoQuanDonVi on cb.Ma_CoQuan_DonVi equals cq.Ma_CoQuan_DonVi
                         join cv in db.DM_ChucVu_ChucDanh on cb.Ma_ChucVu_ChucDanh equals cv.Ma_ChucVu_ChucDanh
                         orderby tb.ThoiGian descending
                         select new { tb = tb, tennguoigui = cb.HoTen, cq.Ten, cv.Ten_ChucVu_ChucDanh });

            var data3 = new
            {
                chuaxem = data2,
                daxem = data1
            };

            return Json(data3, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult AllSeen()
        {
            int user = getuser();
            var data1 = (from tb in db.HT_ThongBao
                         where tb.NguoiNhan == user && tb.TrangThai == true
                         join cb in db.DM_CanBo on tb.NguoiGui equals cb.Ma_CanBo
                         orderby tb.ThoiGian descending
                         select new { tb = tb, tennguoigui = cb.HoTen });


            string sqlconnectStr = ConfigurationManager.ConnectionStrings["ASPNETConnectionString"].ToString();
            var cnn = new SqlConnection(sqlconnectStr);
            cnn.Open();

            foreach (var item in data1)
            {
                var sql = $"UPDATE dbo.HT_ThongBao SET TrangThai = 0 WHERE ID = @ID";

                var command_ins = new SqlCommand(sql, cnn);
                command_ins.Parameters.Add(new SqlParameter("ID", item.tb.ID));
                command_ins.ExecuteNonQuery();
            }
            cnn.Close();

            return Json(true, JsonRequestBehavior.AllowGet);
        }
    }
}