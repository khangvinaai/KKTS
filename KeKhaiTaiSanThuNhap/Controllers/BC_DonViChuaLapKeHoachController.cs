using KeKhaiTaiSanThuNhap.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KeKhaiTaiSanThuNhap.Controllers
{
    [Authorize(Roles = "ADMIN")]
    public class BC_DonViChuaLapKeHoachController : Controller
    {
        private KSTNEntities db = new KSTNEntities();
        private UserInfo user = new UserInfo();
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult LoadDataCoQuan()
        {
            var draw = Request.Form.GetValues("draw").FirstOrDefault();
            var start = Request.Form.GetValues("start").FirstOrDefault();
            var length = Request.Form.GetValues("length").FirstOrDefault();
            var NamKeHoach = Request.Form.GetValues("columns[0][search][value]").FirstOrDefault();

            int pageSize = length != null ? Convert.ToInt32(length) : 0;
            int skip = start != null ? Convert.ToInt32(start) : 0;
            int recordsTotal = 0;
            var CoQuanDaLapKeHoach = db.NV_LapKeHoachKeKhai.Where(_ => _.KeHoachNam == DateTime.Now.Year && _.TrangThai == true).Select(_ => _.Ma_CoQuan_DonVi);
            if (!string.IsNullOrEmpty(NamKeHoach))
            {
                var Nam = Int32.Parse(NamKeHoach);
                CoQuanDaLapKeHoach = db.NV_LapKeHoachKeKhai.Where(_ => _.KeHoachNam == Nam && _.TrangThai == true).Select(_ => _.Ma_CoQuan_DonVi);
            }

            var data = (from cq in db.DM_CoQuanDonVi
                        join lcq in db.DM_Loai_CoQuan_DonVi on cq.MaLoai_CoQuan_DonVi equals lcq.Ma_Loai_CQDV
                        where cq.MaLoai_CoQuan_DonVi != 35 && !CoQuanDaLapKeHoach.Contains(cq.Ma_CoQuan_DonVi)
                        orderby lcq.Ten_Loai_CQDV ascending, cq.Ten ascending
                        select new { cq.Ma_CoQuan_DonVi, cq.Ten, cq.MaLoai_CoQuan_DonVi, lcq.Ten_Loai_CQDV, SoLuong = db.NV_LapKeHoachKeKhai.Where(_ => _.Ma_CoQuan_DonVi == cq.Ma_CoQuan_DonVi && _.TrangThai == true).Count() }).ToList();

            recordsTotal = data.Count();
            var data1 = data.Skip(skip).Take(pageSize).ToList();
            return Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data1 }, JsonRequestBehavior.AllowGet);
        }
    }
}