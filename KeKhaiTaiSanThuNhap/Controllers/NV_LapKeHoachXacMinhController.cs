using iText.Html2pdf;
using iText.Html2pdf.Resolver.Font;
using KeKhaiTaiSanThuNhap.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace KeKhaiTaiSanThuNhap.Controllers
{
    [Authorize(Roles = "ADMIN")]
    public class NV_LapKeHoachXacMinhController : Controller
    {
        private KSTNEntities db = new KSTNEntities();
        private UserInfo user = new UserInfo();
   
        public ActionResult Index()
        {
            return View();
        }


        public JsonResult LoadData()
        {
            var draw = Request.Form.GetValues("draw").FirstOrDefault();
            var start = Request.Form.GetValues("start").FirstOrDefault();
            var length = Request.Form.GetValues("length").FirstOrDefault();
            var search = Request.Form.GetValues("columns[0][search][value]").FirstOrDefault();

            int pageSize = length != null ? Convert.ToInt32(length) : 0;
            int skip = start != null ? Convert.ToInt32(start) : 0;
            int recordsTotal = 0;

            var data = (from lkhxm in db.NV_LapKeHoachXacMinh_DanhSachCanBoXacMinh
                        where lkhxm.TrangThai == true && lkhxm.FileDinhKem != ""
                        orderby lkhxm.KeHoachNam descending
                        select new { lkhxm.ID_DanhSach, lkhxm.TenDanhSach, lkhxm.KeHoachNam, lkhxm.NgayLapDanhSach, lkhxm.FileDinhKem, lkhxm.TrangThai, TrangThaiKH = db.NV_LapKeHoachXacMinh.Where(_ => _.ID_DanhSachCanBo == lkhxm.ID_DanhSach).Count(),
                                     TrangThaiDS = (db.NV_LapKeHoachXacMinh.Where(_ => _.ID_DanhSachCanBo == lkhxm.ID_DanhSach).Count() == 0)?false: db.NV_LapKeHoachXacMinh.FirstOrDefault(_ => _.ID_DanhSachCanBo == lkhxm.ID_DanhSach).TrangThai_ChinhSua
                        }).ToList();

            if (!string.IsNullOrEmpty(search))  
            {
                data = data.Where(a => a.TenDanhSach.ToUpper().Contains(search.ToUpper())).ToList();
            }


            recordsTotal = data.Count();
            var data1 = data.Skip(skip).Take(pageSize).ToList();
            return Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data1 }, JsonRequestBehavior.AllowGet);

        }

        public JsonResult LoadDataKeHoachXacMinh_DanhSachCanBoXacMinh()
        {
            var draw = Request.Form.GetValues("draw").FirstOrDefault();
            var start = Request.Form.GetValues("start").FirstOrDefault();
            var length = Request.Form.GetValues("length").FirstOrDefault();
            var Ten = Request.Form.GetValues("columns[0][search][value]").FirstOrDefault();

            int pageSize = length != null ? Convert.ToInt32(length) : 0;
            int skip = start != null ? Convert.ToInt32(start) : 0;
            int recordsTotal = 0;

            var data = (from lkhxm in db.NV_LapKeHoachXacMinh_DanhSachCanBoXacMinh
                        select new { lkhxm.ID_DanhSach, lkhxm.KeHoachNam, lkhxm.NgayLapDanhSach, lkhxm.FileDinhKem, lkhxm.TrangThai }).ToList();
            recordsTotal = data.Count();
            var data1 = data.Skip(skip).Take(pageSize).ToList();
            return Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data1 }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetKeHoachXacMinh(int? ID_DanhSach)
        {
            var data = db.NV_LapKeHoachXacMinh.Where(_ => _.ID_DanhSachCanBo == ID_DanhSach).First();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult HoanThanhKeHoach(int? id)
        {
            var data = db.NV_LapKeHoachXacMinh.Where(_ => _.ID_DanhSachCanBo == id).First();

            if (data.FileKeHoach == "")
            {
                return Json(new { status = "warning", title = "Cảnh Báo", message = "File Kế Hoạch Chưa Được Đính Kèm. Vui Lòng Kiểm Tra Lại!" });
            }

            data.TrangThai_ChinhSua = true;
            db.Entry(data).State = EntityState.Modified;
            db.SaveChanges();

            return Json(new { status = "success", title = "Thành Công", message = "Kế Hoạch Được Lập Đã Hoàn Thành!" });
        }

        public JsonResult Create([Bind(Include = "ID_KeHoach,SoKeHoach,NgayLapKeHoach,NoiDungKeHoach,ID_DanhSachCanBo")] NV_LapKeHoachXacMinh nV_LapKeHoachXacMinh, HttpPostedFileBase FileKeHoach)
        {

            if (nV_LapKeHoachXacMinh.ID_KeHoach == 0)
            {
                if (FileKeHoach != null)
                {
                    string _FileName = user.GetRandomPassword(6) + "-" + Path.GetFileName(FileKeHoach.FileName);
                    string _path = Path.Combine(Server.MapPath("~/Content/uploads"), _FileName);
                    nV_LapKeHoachXacMinh.FileKeHoach = _FileName;
                    FileKeHoach.SaveAs(_path);
                }
                nV_LapKeHoachXacMinh.TrangThai = false;
                nV_LapKeHoachXacMinh.TrangThai_ChinhSua = false;
                nV_LapKeHoachXacMinh.TienDo = 0;
                db.NV_LapKeHoachXacMinh.Add(nV_LapKeHoachXacMinh);
                db.SaveChanges();

                return Json(new { status = "success", title = "Lập Kế Hoạch Thành Công", message = "Kế Hoạch Xác Minh Tài Sản Thu Nhập Đã Được Lập."}, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var data = db.NV_LapKeHoachXacMinh.Where(_ => _.ID_KeHoach == nV_LapKeHoachXacMinh.ID_KeHoach).First();
                data.SoKeHoach = nV_LapKeHoachXacMinh.SoKeHoach;
                data.NoiDungKeHoach = nV_LapKeHoachXacMinh.NoiDungKeHoach;
                data.NgayLapKeHoach = nV_LapKeHoachXacMinh.NgayLapKeHoach;

                if (FileKeHoach != null)
                {
                    string _FileName = user.GetRandomPassword(6) + "-" + Path.GetFileName(FileKeHoach.FileName);
                    string _path = Path.Combine(Server.MapPath("~/Content/uploads"), _FileName);
                    data.FileKeHoach = _FileName;
                    FileKeHoach.SaveAs(_path);
                }

                db.Entry(data).State = EntityState.Modified;
                db.SaveChanges();

                return Json(new { status = "success", title = "Cập Nhật Kế Hoạch Thành Công", message = "Kế Hoạch Xác Minh Tài Sản Thu Nhập Đã Được Cập Nhật"}, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult LoadDataCoQuan(int? id)
        {
            var draw = Request.Form.GetValues("draw").FirstOrDefault();
            var start = Request.Form.GetValues("start").FirstOrDefault();
            var length = Request.Form.GetValues("length").FirstOrDefault();
            var search = Request.Form.GetValues("columns[0][search][value]").FirstOrDefault();

            int pageSize = length != null ? Convert.ToInt32(length) : 0;
            int skip = start != null ? Convert.ToInt32(start) : 0;
            int recordsTotal = 0;


            var data = (from lkhxm in db.NV_LapKeHoachXacMinh_DanhSachCanBoXacMinh_ChiTiet
                        join cb in db.DM_CanBo on lkhxm.Ma_CanBo equals cb.Ma_CanBo
                        join cq in db.DM_CoQuanDonVi on cb.Ma_CoQuan_DonVi equals cq.Ma_CoQuan_DonVi
                        where lkhxm.ID_DanhSach == id
                        group cq by cq.Ma_CoQuan_DonVi into cq_group
                        select new {MaCoQuan = cq_group.Key, SoLuong = cq_group.Count() }).ToList();

            var data1 = (from cq_group in data
                         join cq in db.DM_CoQuanDonVi on cq_group.MaCoQuan equals cq.Ma_CoQuan_DonVi
                         join lcq in db.DM_Loai_CoQuan_DonVi on cq.MaLoai_CoQuan_DonVi equals lcq.Ma_Loai_CQDV
                         orderby lcq.Ten_Loai_CQDV, cq.Ten
                         select new {cq.Ma_CoQuan_DonVi, cq_group.SoLuong, cq.Ten, lcq.Ten_Loai_CQDV }).ToList();

            if (!string.IsNullOrEmpty(search))
            {
                data1 = data1.Where(a => a.Ten.ToUpper().Contains(search.ToUpper())).ToList();
            }


            recordsTotal = data1.Count();
            var data2 = data1.Skip(skip).Take(pageSize).ToList();
            return Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data2 }, JsonRequestBehavior.AllowGet);

        }

        public JsonResult LoadDataCanBo(int? id)
        {
            var draw = Request.Form.GetValues("draw").FirstOrDefault();
            var start = Request.Form.GetValues("start").FirstOrDefault();
            var length = Request.Form.GetValues("length").FirstOrDefault();
            var search = Request.Form.GetValues("columns[0][search][value]").FirstOrDefault();

            int pageSize = length != null ? Convert.ToInt32(length) : 0;
            int skip = start != null ? Convert.ToInt32(start) : 0;
            int recordsTotal = 0;

            var ds = db.NV_LapKeHoachXacMinh_DanhSachCanBoXacMinh.Where(_ => _.ID_DanhSach == id).FirstOrDefault();

            var NamKeHoach = ds.KeHoachNam;

            var dskh = db.NV_LapKeHoachKeKhai.Where(_ => _.KeHoachNam == NamKeHoach && _.Ma_Loai_KeKhai == 4).Select(_ => _.MaKeHoachKeKhai).ToList();


            var data = (from lkhxm in db.NV_LapKeHoachXacMinh_DanhSachCanBoXacMinh_ChiTiet
                        join cb in db.DM_CanBo on lkhxm.Ma_CanBo equals cb.Ma_CanBo
                        join cq in db.DM_CoQuanDonVi on cb.Ma_CoQuan_DonVi equals cq.Ma_CoQuan_DonVi
                        join lcq in db.DM_Loai_CoQuan_DonVi on cq.MaLoai_CoQuan_DonVi equals lcq.Ma_Loai_CQDV
                        join cv in db.DM_ChucVu_ChucDanh on cb.Ma_ChucVu_ChucDanh equals cv.Ma_ChucVu_ChucDanh
                        join tk in db.HT_TaiKhoan on cb.Ma_CanBo equals tk.Ma_CanBo
                        join ntk in db.HT_NhomTaiKhoan on tk.MaNhomTaiKhoan equals ntk.MaNhomTaiKhoan
                        orderby lcq.Ten_Loai_CQDV, cq.Ten, ntk.Sort, cb.Ten
                            
                        select new { cb.Ma_CanBo,cb.HoTen, cq.Ten, cv.Ten_ChucVu_ChucDanh,db.Nv_KeKhai_TSTN.Where(_ => _.Ma_CanBo == cb.Ma_CanBo && _.Ma_Loai_KeKhai == 4 && dskh.Contains((int)_.MaKeHoachKeKhai)).FirstOrDefault().FileDinhKem  }).ToList();

         

            if (!string.IsNullOrEmpty(search))
            {
                data = data.Where(a => a.HoTen.ToUpper().Contains(search.ToUpper()) || a.Ten.ToUpper().Contains(search.ToUpper())).ToList();
            }


            recordsTotal = data.Count();
            var data1 = data.Skip(skip).Take(pageSize).ToList();
            return Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data }, JsonRequestBehavior.AllowGet);

        }
    }
}