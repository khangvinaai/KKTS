using KeKhaiTaiSanThuNhap.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KeKhaiTaiSanThuNhap.Controllers
{
    [Authorize(Roles = "ADMIN")]
    public class NV_XacMinhTaiSanThuNhapController : Controller
    {
        private KSTNEntities db = new KSTNEntities();
        private UserInfo user = new UserInfo();
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult LoadDataKeHoachXacMinh()
        {
           
            var draw = Request.Form.GetValues("draw").FirstOrDefault();
            var start = Request.Form.GetValues("start").FirstOrDefault();
            var length = Request.Form.GetValues("length").FirstOrDefault();
            var search = Request.Form.GetValues("columns[0][search][value]").FirstOrDefault();
            int pageSize = length != null ? Convert.ToInt32(length) : 0;
            int skip = start != null ? Convert.ToInt32(start) : 0;
            int recordsTotal = 0;

            var data = (from khxm in db.NV_LapKeHoachXacMinh
                        orderby khxm.NgayLapKeHoach descending
                        select new {khxm.ID_DanhSachCanBo, khxm.ID_KeHoach, khxm.SoKeHoach, khxm.NgayLapKeHoach, khxm.NoiDungKeHoach, khxm.FileKeHoach, khxm.TienDo, khxm.TrangThai,FileDanhSach = db.NV_LapKeHoachXacMinh_DanhSachCanBoXacMinh.Where(_ => _.ID_DanhSach == khxm.ID_DanhSachCanBo).FirstOrDefault().FileDinhKem })
                       .ToList();

            if (!string.IsNullOrEmpty(search))
            {
                data = data.Where(a => a.NoiDungKeHoach.ToUpper().Contains(search.ToUpper())).ToList();
            }

            recordsTotal = data.Count();
            var data1 = data.Skip(skip).Take(pageSize).ToList();
            return Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data1 }, JsonRequestBehavior.AllowGet);

        }

        public JsonResult LoadDataGiaiTrinh(int? id)
        {

            var draw = Request.Form.GetValues("draw").FirstOrDefault();
            var start = Request.Form.GetValues("start").FirstOrDefault();
            var length = Request.Form.GetValues("length").FirstOrDefault();
            var search = Request.Form.GetValues("columns[0][search][value]").FirstOrDefault();
            int pageSize = length != null ? Convert.ToInt32(length) : 0;
            int skip = start != null ? Convert.ToInt32(start) : 0;
            int recordsTotal = 0;

            var data = db.NV_LapKeHoachXacMinh_GiaiTrinhTaiSanThuNhap.Where(_ => _.ID_KeHoachXacMinh == id).ToList();
          

            if (!string.IsNullOrEmpty(search))
            {
                data = data.Where(a => a.TenNguoiDuocXacMinh.ToUpper().Contains(search.ToUpper())).ToList();
            }

            recordsTotal = data.Count();
            var data1 = data.Skip(skip).Take(pageSize).ToList();
            return Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data1 }, JsonRequestBehavior.AllowGet);

        }

        public JsonResult LoadDataXacMinh(int? id)
        {

            var draw = Request.Form.GetValues("draw").FirstOrDefault();
            var start = Request.Form.GetValues("start").FirstOrDefault();
            var length = Request.Form.GetValues("length").FirstOrDefault();
            var search = Request.Form.GetValues("columns[0][search][value]").FirstOrDefault();
            int pageSize = length != null ? Convert.ToInt32(length) : 0;
            int skip = start != null ? Convert.ToInt32(start) : 0;
            int recordsTotal = 0;

            var data = db.NV_LapKeHoachXacMinh_TienHanhXacMinh.Where(_ => _.ID_KeHoachXacMinh == id).ToList();


            if (!string.IsNullOrEmpty(search))
            {
                data = data.Where(a => a.TenNguoiDuocXacMinh.ToUpper().Contains(search.ToUpper())).ToList();
            }

            recordsTotal = data.Count();
            var data1 = data.Skip(skip).Take(pageSize).ToList();
            return Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data1 }, JsonRequestBehavior.AllowGet);

        }

        public JsonResult LoadDataBaoCao(int? id)
        {

            var draw = Request.Form.GetValues("draw").FirstOrDefault();
            var start = Request.Form.GetValues("start").FirstOrDefault();
            var length = Request.Form.GetValues("length").FirstOrDefault();
            var search = Request.Form.GetValues("columns[0][search][value]").FirstOrDefault();
            int pageSize = length != null ? Convert.ToInt32(length) : 0;
            int skip = start != null ? Convert.ToInt32(start) : 0;
            int recordsTotal = 0;

            var data = db.NV_LapKeHoachXacMinh_BaoCaoKetQuaXacMinh.Where(_ => _.ID_KeHoachXacMinh == id).ToList();


            if (!string.IsNullOrEmpty(search))
            {
                data = data.Where(a => a.SoBaoCao.ToUpper().Contains(search.ToUpper())).ToList();
            }

            recordsTotal = data.Count();
            var data1 = data.Skip(skip).Take(pageSize).ToList();
            return Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data1 }, JsonRequestBehavior.AllowGet);

        }

        public JsonResult LoadDataKetLuan(int? id)
        {

            var draw = Request.Form.GetValues("draw").FirstOrDefault();
            var start = Request.Form.GetValues("start").FirstOrDefault();
            var length = Request.Form.GetValues("length").FirstOrDefault();
            var search = Request.Form.GetValues("columns[0][search][value]").FirstOrDefault();
            int pageSize = length != null ? Convert.ToInt32(length) : 0;
            int skip = start != null ? Convert.ToInt32(start) : 0;
            int recordsTotal = 0;

            var data = db.NV_LapKeHoachXacMinh_KetLuanXacMinh.Where(_ => _.ID_KeHoachXacMinh == id).ToList();


            if (!string.IsNullOrEmpty(search))
            {
                data = data.Where(a => a.SoKetLuan.ToUpper().Contains(search.ToUpper())).ToList();
            }

            recordsTotal = data.Count();
            var data1 = data.Skip(skip).Take(pageSize).ToList();
            return Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data1 }, JsonRequestBehavior.AllowGet);

        }

        public JsonResult LoadDataCongKhai(int? id)
        {

            var draw = Request.Form.GetValues("draw").FirstOrDefault();
            var start = Request.Form.GetValues("start").FirstOrDefault();
            var length = Request.Form.GetValues("length").FirstOrDefault();
            var search = Request.Form.GetValues("columns[0][search][value]").FirstOrDefault();
            int pageSize = length != null ? Convert.ToInt32(length) : 0;
            int skip = start != null ? Convert.ToInt32(start) : 0;
            int recordsTotal = 0;

            var data = db.NV_LapKeHoachXacMinh_GuiVaCongKhaiKetLuanXacMinh.Where(_ => _.ID_KeHoachXacMinh == id).ToList();


            if (!string.IsNullOrEmpty(search))
            {
                data = data.Where(a => a.NoiDungBienBan.ToUpper().Contains(search.ToUpper())).ToList();
            }

            recordsTotal = data.Count();
            var data1 = data.Skip(skip).Take(pageSize).ToList();
            return Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data1 }, JsonRequestBehavior.AllowGet);

        }

        public JsonResult GetTienDo(int? ID_KeHoach)
        {
            var data = (from khxm in db.NV_LapKeHoachXacMinh
                        where khxm.ID_KeHoach == ID_KeHoach
                        select new { TrangThai = khxm.TrangThai,TienDoHienTai = khxm.TienDo, ThoiGianHoanThanhTienDo = (khxm.TienDo == 0) ? 0 : db.DM_TienDo.Where(_ => _.ID_TienDo == khxm.TienDo).FirstOrDefault().SoNgay, NgayHoanThanhTienDoTruoc = ((int)khxm.TienDo <= 1) ? null : db.NV_LapKeHoachXacMinh_ThoiGianHoanThanh_TienDo.Where(_ => _.ID_KeHoachXacMinh == khxm.ID_KeHoach && _.TienDo == khxm.TienDo - 1).FirstOrDefault().NgayHoanThanh, 
                        CheckRaQuyetDinh = db.NV_LapKeHoachXacMinh_RaQuyetDinhXacMinh.Count(_ => _.ID_KeHoachXacMinh == khxm.ID_KeHoach)}).FirstOrDefault();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetRaQuyetDinhXacMinh(int? ID_KeHoach)
        {
            var data = db.NV_LapKeHoachXacMinh_RaQuyetDinhXacMinh.Where(_ => _.ID_KeHoachXacMinh == ID_KeHoach).First();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetGiaiTrinh(int? ID_GiaiTrinh)
        {
            var data = db.NV_LapKeHoachXacMinh_GiaiTrinhTaiSanThuNhap.Where(_ => _.ID_GiaiTrinhTaiSanThuNhap == ID_GiaiTrinh).First();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetXacMinh(int? ID_XacMinh)
        {
            var data = db.NV_LapKeHoachXacMinh_TienHanhXacMinh.Where(_ => _.ID_TienHanhXacMinh == ID_XacMinh).First();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetBaoCao(int? ID_BaoCao)
        {
            var data = db.NV_LapKeHoachXacMinh_BaoCaoKetQuaXacMinh.Where(_ => _.ID_BaoCaoKetQuaXacMinh == ID_BaoCao).First();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetKetLuan(int? ID_KetLuan)
        {
            var data = db.NV_LapKeHoachXacMinh_KetLuanXacMinh.Where(_ => _.ID_KetLuanXacMinh == ID_KetLuan).First();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetCongKhai(int? ID_CongKhai)
        {
            var data = db.NV_LapKeHoachXacMinh_GuiVaCongKhaiKetLuanXacMinh.Where(_ => _.ID_GuiVaCongKhaiKetLuanXacMinh == ID_CongKhai).First();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult HoanThanh(int? ID_KeHoach, int? TienDo)
        {
            if(TienDo == 1)
            {
                var raquyetdinh = db.NV_LapKeHoachXacMinh_RaQuyetDinhXacMinh.Where(_ => _.ID_KeHoachXacMinh == ID_KeHoach);

                if(raquyetdinh.Count() == 0)
                {
                    return Json(new { status = "error", title = "Không Thể Hoàn Thành", message = "Dữ Liệu Ra Quyết Định Xác Minh Chưa Có? Vui Lòng Kiểm Tra Lại.", TienDoTiepTheo = 1 }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    //Cập tiến độ từ 1 thành 2
                    var khxm = db.NV_LapKeHoachXacMinh.Where(_ => _.ID_KeHoach == ID_KeHoach).First();
                    khxm.TienDo = 2;
                    db.Entry(khxm).State = EntityState.Modified;
                    db.SaveChanges();

                    var rqd = raquyetdinh.First();
                    var tiendo = new NV_LapKeHoachXacMinh_ThoiGianHoanThanh_TienDo();

                    tiendo.ID_KeHoachXacMinh = ID_KeHoach;
                    tiendo.NgayHoanThanh = rqd.NgayQuyetDinh;
                    tiendo.TienDo = 1;
                    db.NV_LapKeHoachXacMinh_ThoiGianHoanThanh_TienDo.Add(tiendo);
                    db.SaveChanges();

                    return Json(new { status = "success", title = "Thành Công", message = "Đã Cập Nhật Tiến Độ Thành Giải Trình Tài Sản, Thu Nhập.", TienDoTiepTheo = 2}, JsonRequestBehavior.AllowGet);
                }
            }
            else if(TienDo == 2)
            {
               
                var giaitrinh = db.NV_LapKeHoachXacMinh_GiaiTrinhTaiSanThuNhap.Where(_ => _.ID_KeHoachXacMinh == ID_KeHoach);

                if (giaitrinh.Count() == 0)
                {
                    return Json(new { status = "error", title = "Không Thể Hoàn Thành", message = "Dữ Liệu Giải Trình Tài Sản Thu Nhập Chưa Có? Vui Lòng Kiểm Tra Lại.", TienDoTiepTheo = 3 }, JsonRequestBehavior.AllowGet);
                }
                else
                {

                    //Cập tiến độ từ 2 thành 3
                    var khxm = db.NV_LapKeHoachXacMinh.Where(_ => _.ID_KeHoach == ID_KeHoach).First();
                    khxm.TienDo = 3;
                    db.Entry(khxm).State = EntityState.Modified;
                    db.SaveChanges();


                    var tiendo = new NV_LapKeHoachXacMinh_ThoiGianHoanThanh_TienDo();
                    tiendo.ID_KeHoachXacMinh = ID_KeHoach;
                    tiendo.NgayHoanThanh = DateTime.Now;
                    tiendo.TienDo = 2;
                    db.NV_LapKeHoachXacMinh_ThoiGianHoanThanh_TienDo.Add(tiendo);
                    db.SaveChanges();

                    return Json(new { status = "success", title = "Thành Công", message = "Đã Cập Nhật Tiến Độ Thành Tiến Hành Xác Minh Tài Sản, Thu Nhập.", TienDoTiepTheo = 3 }, JsonRequestBehavior.AllowGet);
                }


            }
            else if (TienDo == 3)
            {
               

                var xacminh = db.NV_LapKeHoachXacMinh_TienHanhXacMinh.Where(_ => _.ID_KeHoachXacMinh == ID_KeHoach);

                if (xacminh.Count() == 0)
                {
                    return Json(new { status = "error", title = "Không Thể Hoàn Thành", message = "Dữ Liệu Xác Minh Tài Sản Thu Nhập Chưa Có? Vui Lòng Kiểm Tra Lại.", TienDoTiepTheo = 3 }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    //Cập tiến độ từ 3 thành 4
                    var khxm = db.NV_LapKeHoachXacMinh.Where(_ => _.ID_KeHoach == ID_KeHoach).First();
                    khxm.TienDo = 4;
                    db.Entry(khxm).State = EntityState.Modified;
                    db.SaveChanges();

                    var raquyetdinh = db.NV_LapKeHoachXacMinh_RaQuyetDinhXacMinh.Where(_ => _.ID_KeHoachXacMinh == ID_KeHoach).FirstOrDefault();

                    var tiendo = new NV_LapKeHoachXacMinh_ThoiGianHoanThanh_TienDo();
                    tiendo.ID_KeHoachXacMinh = ID_KeHoach;
                    tiendo.NgayHoanThanh = raquyetdinh.NgayQuyetDinh;
                    tiendo.TienDo = 3;
                    db.NV_LapKeHoachXacMinh_ThoiGianHoanThanh_TienDo.Add(tiendo);
                    db.SaveChanges();

                    return Json(new { status = "success", title = "Thành Công", message = "Đã Cập Nhật Tiến Độ Thành Báo Cáo Kết Quả Xác Minh.", TienDoTiepTheo = 4 }, JsonRequestBehavior.AllowGet);
                }


            }
            else if (TienDo == 4)
            {
                

                var baocao = db.NV_LapKeHoachXacMinh_BaoCaoKetQuaXacMinh.Where(_ => _.ID_KeHoachXacMinh == ID_KeHoach);

                if (baocao.Count() == 0)
                {
                    return Json(new { status = "error", title = "Không Thể Hoàn Thành", message = "Dữ Liệu Báo Cáo Kết Quả Xác Minh Tài Sản Thu Nhập Chưa Có? Vui Lòng Kiểm Tra Lại.", TienDoTiepTheo = 3 }, JsonRequestBehavior.AllowGet);
                }
                else
                {

                    //Cập tiến độ từ 4 thành 5
                    var khxm = db.NV_LapKeHoachXacMinh.Where(_ => _.ID_KeHoach == ID_KeHoach).First();
                    khxm.TienDo = 5;
                    db.Entry(khxm).State = EntityState.Modified;
                    db.SaveChanges();

                 
                    var tiendo = new NV_LapKeHoachXacMinh_ThoiGianHoanThanh_TienDo();
                    tiendo.ID_KeHoachXacMinh = ID_KeHoach;
                    tiendo.NgayHoanThanh = DateTime.Now;
                    tiendo.TienDo = 4;
                    db.NV_LapKeHoachXacMinh_ThoiGianHoanThanh_TienDo.Add(tiendo);
                    db.SaveChanges();

                    return Json(new { status = "success", title = "Thành Công", message = "Đã Cập Nhật Tiến Độ Thành Kết Luận Xác Minh.", TienDoTiepTheo = 5 }, JsonRequestBehavior.AllowGet);
                }


            }
            else if (TienDo == 5)
            {
                var ketluan = db.NV_LapKeHoachXacMinh_KetLuanXacMinh.Where(_ => _.ID_KeHoachXacMinh == ID_KeHoach);

                if (ketluan.Count() == 0)
                {
                    return Json(new { status = "error", title = "Không Thể Hoàn Thành", message = "Dữ Liệu Kết Luận Kết Quả Xác Minh Tài Sản Thu Nhập Chưa Có? Vui Lòng Kiểm Tra Lại.", TienDoTiepTheo = 3 }, JsonRequestBehavior.AllowGet);
                }
                else
                {

                    //Cập tiến độ từ 5 thành 6
                    var khxm = db.NV_LapKeHoachXacMinh.Where(_ => _.ID_KeHoach == ID_KeHoach).First();
                    khxm.TienDo = 6;
                    db.Entry(khxm).State = EntityState.Modified;
                    db.SaveChanges();


                    var tiendo = new NV_LapKeHoachXacMinh_ThoiGianHoanThanh_TienDo();
                    tiendo.ID_KeHoachXacMinh = ID_KeHoach;
                    tiendo.NgayHoanThanh = DateTime.Now;
                    tiendo.TienDo = 5;
                    db.NV_LapKeHoachXacMinh_ThoiGianHoanThanh_TienDo.Add(tiendo);
                    db.SaveChanges();

                    return Json(new { status = "success", title = "Thành Công", message = "Đã Cập Nhật Tiến Độ Thành Công Khai Kết Luận Xác Minh.", TienDoTiepTheo = 6 }, JsonRequestBehavior.AllowGet);
                }


            }
            else if (TienDo == 6)
            {
                var congkhai = db.NV_LapKeHoachXacMinh_GuiVaCongKhaiKetLuanXacMinh.Where(_ => _.ID_KeHoachXacMinh == ID_KeHoach);

                if (congkhai.Count() == 0)
                {
                    return Json(new { status = "error", title = "Không Thể Hoàn Thành", message = "Dữ Liệu Công Khai Kết Quả Xác Minh Tài Sản Thu Nhập Chưa Có? Vui Lòng Kiểm Tra Lại.", TienDoTiepTheo = 3 }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    //Cập tiến độ từ 6 thành 7
                    var khxm = db.NV_LapKeHoachXacMinh.Where(_ => _.ID_KeHoach == ID_KeHoach).First();
                    khxm.TienDo = 7;
                    khxm.TrangThai = true;
                    db.Entry(khxm).State = EntityState.Modified;
                    db.SaveChanges();


                    var tiendo = new NV_LapKeHoachXacMinh_ThoiGianHoanThanh_TienDo();
                    tiendo.ID_KeHoachXacMinh = ID_KeHoach;
                    tiendo.NgayHoanThanh = DateTime.Now;
                    tiendo.TienDo = 6;
                    db.NV_LapKeHoachXacMinh_ThoiGianHoanThanh_TienDo.Add(tiendo);
                    db.SaveChanges();

                    return Json(new { status = "success", title = "Thành Công", message = "Đã Hoàn Thành Xác Minh Tài Sản, Thu Nhập.", TienDoTiepTheo = 7 }, JsonRequestBehavior.AllowGet);
                }


            }
            return Json(new { status = "success", title = "Thành Công", message = "Đã Cập Nhật Tiến Độ Thành Giải Trình Tài Sản, Thu Nhập.", TienDoTiepTheo = 2}, JsonRequestBehavior.AllowGet);
        }

        public JsonResult LuuDuLieuRaQuyetDinhXacMinh([Bind(Include = "ID_RaQuyetDinhXacMinh,ID_KeHoachXacMinh,SoVanBan,NgayLapVanBan,NoiDungVanBan,SoQuyetDinh,NgayQuyetDinh,NoiDungXacMinh,ThoiHanXacMinh")] NV_LapKeHoachXacMinh_RaQuyetDinhXacMinh nV_LapKeHoachXacMinh_RaQuyetDinhXacMinh,
                                    HttpPostedFileBase FileVanBan, HttpPostedFileBase CanCuBanHanhQuyetDinhXacMinh, HttpPostedFileBase FileToXacMinh, HttpPostedFileBase FileQuyetDinh, HttpPostedFileBase FileDanhSachCanBoXacMinh,
                                    HttpPostedFileBase FileNhiemVuQuyenHan, HttpPostedFileBase FileCoQuanToChucPhoiHop)
        {
            if(nV_LapKeHoachXacMinh_RaQuyetDinhXacMinh.ID_RaQuyetDinhXacMinh == 0)
            {
                if (FileVanBan != null)
                {
                    string _FileName = user.GetRandomPassword(6) + "-" + Path.GetFileName(FileVanBan.FileName);
                    string _path = Path.Combine(Server.MapPath("~/Content/uploads"), _FileName);
                    nV_LapKeHoachXacMinh_RaQuyetDinhXacMinh.FileVanBan = _FileName;
                    FileVanBan.SaveAs(_path);
                }

                if (CanCuBanHanhQuyetDinhXacMinh != null)
                {
                    string _FileName = user.GetRandomPassword(6) + "-" + Path.GetFileName(CanCuBanHanhQuyetDinhXacMinh.FileName);
                    string _path = Path.Combine(Server.MapPath("~/Content/uploads"), _FileName);
                    nV_LapKeHoachXacMinh_RaQuyetDinhXacMinh.CanCuBanHanhQuyetDinhXacMinh = _FileName;
                    CanCuBanHanhQuyetDinhXacMinh.SaveAs(_path);
                }

                if (FileToXacMinh != null && FileToXacMinh.ContentLength > 0)
                {
                    string _FileName = user.GetRandomPassword(6) + "-" + Path.GetFileName(FileToXacMinh.FileName);
                    string _path = Path.Combine(Server.MapPath("~/Content/uploads"), _FileName);
                    nV_LapKeHoachXacMinh_RaQuyetDinhXacMinh.FileToXacMinh = _FileName;
                    FileToXacMinh.SaveAs(_path);
                }

                if (FileQuyetDinh != null)
                {
                    string _FileName = user.GetRandomPassword(6) + "-" + Path.GetFileName(FileQuyetDinh.FileName);
                    string _path = Path.Combine(Server.MapPath("~/Content/uploads"), _FileName);
                    nV_LapKeHoachXacMinh_RaQuyetDinhXacMinh.FileQuyetDinh = _FileName;
                    FileQuyetDinh.SaveAs(_path);
                }

                if (FileDanhSachCanBoXacMinh != null)
                {
                    string _FileName = user.GetRandomPassword(6)+ "-" + Path.GetFileName(FileDanhSachCanBoXacMinh.FileName);
                    string _path = Path.Combine(Server.MapPath("~/Content/uploads"), _FileName);
                    nV_LapKeHoachXacMinh_RaQuyetDinhXacMinh.FileDanhSachCanBoXacMinh = _FileName;
                    FileDanhSachCanBoXacMinh.SaveAs(_path);
                }

                if (FileQuyetDinh != null)
                {
                    string _FileName = user.GetRandomPassword(6) + "-" + Path.GetFileName(FileQuyetDinh.FileName);
                    string _path = Path.Combine(Server.MapPath("~/Content/uploads"), _FileName);
                    nV_LapKeHoachXacMinh_RaQuyetDinhXacMinh.FileQuyetDinh = _FileName;
                    FileQuyetDinh.SaveAs(_path);
                }

                if (FileDanhSachCanBoXacMinh != null)
                {
                    string _FileName = user.GetRandomPassword(6) + "-" + Path.GetFileName(FileDanhSachCanBoXacMinh.FileName);
                    string _path = Path.Combine(Server.MapPath("~/Content/uploads"), _FileName);
                    nV_LapKeHoachXacMinh_RaQuyetDinhXacMinh.FileDanhSachCanBoXacMinh = _FileName;
                    FileDanhSachCanBoXacMinh.SaveAs(_path);
                }

                if (FileNhiemVuQuyenHan != null)
                {
                    string _FileName = user.GetRandomPassword(6) + "-" + Path.GetFileName(FileNhiemVuQuyenHan.FileName);
                    string _path = Path.Combine(Server.MapPath("~/Content/uploads"), _FileName);
                    nV_LapKeHoachXacMinh_RaQuyetDinhXacMinh.FileNhiemVuQuyenHan = _FileName;
                    FileNhiemVuQuyenHan.SaveAs(_path);
                }

                if (FileCoQuanToChucPhoiHop != null)
                {
                    string _FileName = user.GetRandomPassword(6) + "-" + Path.GetFileName(FileCoQuanToChucPhoiHop.FileName);
                    string _path = Path.Combine(Server.MapPath("~/Content/uploads"), _FileName);
                    nV_LapKeHoachXacMinh_RaQuyetDinhXacMinh.FileCoQuanToChucPhoiHop = _FileName;
                    FileCoQuanToChucPhoiHop.SaveAs(_path);
                }
                db.NV_LapKeHoachXacMinh_RaQuyetDinhXacMinh.Add(nV_LapKeHoachXacMinh_RaQuyetDinhXacMinh);
                db.SaveChanges();

                return Json(new { status = "success", title = "Lưu Dữ Liệu Thành Công", message = "Dữ Liệu Ra Quyết Định Xác Minh Đã Được Lưu", ID_KeHoach = nV_LapKeHoachXacMinh_RaQuyetDinhXacMinh.ID_KeHoachXacMinh }, JsonRequestBehavior.AllowGet);

            }
            else
            {
                var data = db.NV_LapKeHoachXacMinh_RaQuyetDinhXacMinh.Where(_ => _.ID_KeHoachXacMinh == nV_LapKeHoachXacMinh_RaQuyetDinhXacMinh.ID_KeHoachXacMinh).First();
                data.SoVanBan = nV_LapKeHoachXacMinh_RaQuyetDinhXacMinh.SoVanBan;
                data.NgayLapVanBan = nV_LapKeHoachXacMinh_RaQuyetDinhXacMinh.NgayLapVanBan;
                data.NoiDungVanBan = nV_LapKeHoachXacMinh_RaQuyetDinhXacMinh.NoiDungVanBan;
                data.SoQuyetDinh = nV_LapKeHoachXacMinh_RaQuyetDinhXacMinh.SoQuyetDinh;
                data.NgayQuyetDinh = nV_LapKeHoachXacMinh_RaQuyetDinhXacMinh.NgayQuyetDinh;
                data.NoiDungXacMinh = nV_LapKeHoachXacMinh_RaQuyetDinhXacMinh.NoiDungXacMinh;
                data.ThoiHanXacMinh = nV_LapKeHoachXacMinh_RaQuyetDinhXacMinh.ThoiHanXacMinh;

                if (FileVanBan != null)
                {
                    string _FileName = user.GetRandomPassword(6) + "-" + Path.GetFileName(FileVanBan.FileName);
                    string _path = Path.Combine(Server.MapPath("~/Content/uploads"), _FileName);
                    data.FileVanBan = _FileName;
                    FileVanBan.SaveAs(_path);
                }

                if (CanCuBanHanhQuyetDinhXacMinh != null)
                {
                    string _FileName = user.GetRandomPassword(6) + "-" + Path.GetFileName(CanCuBanHanhQuyetDinhXacMinh.FileName);
                    string _path = Path.Combine(Server.MapPath("~/Content/uploads"), _FileName);
                    data.CanCuBanHanhQuyetDinhXacMinh = _FileName;
                    CanCuBanHanhQuyetDinhXacMinh.SaveAs(_path);
                }

                if (FileToXacMinh != null)
                {
                    string _FileName = user.GetRandomPassword(6) + "-" + Path.GetFileName(FileToXacMinh.FileName);
                    string _path = Path.Combine(Server.MapPath("~/Content/uploads"), _FileName);
                    data.FileToXacMinh = _FileName;
                    FileToXacMinh.SaveAs(_path);
                }

                if (FileQuyetDinh != null)
                {
                    string _FileName = user.GetRandomPassword(6) + "-" + Path.GetFileName(FileQuyetDinh.FileName);
                    string _path = Path.Combine(Server.MapPath("~/Content/uploads"), _FileName);
                    data.FileQuyetDinh = _FileName;
                    FileQuyetDinh.SaveAs(_path);
                }

                if (FileDanhSachCanBoXacMinh != null)
                {
                    string _FileName = user.GetRandomPassword(6) + "-" + Path.GetFileName(FileDanhSachCanBoXacMinh.FileName);
                    string _path = Path.Combine(Server.MapPath("~/Content/uploads"), _FileName);
                    data.FileDanhSachCanBoXacMinh = _FileName;
                    FileDanhSachCanBoXacMinh.SaveAs(_path);
                }

                if (FileQuyetDinh != null)
                {
                    string _FileName = user.GetRandomPassword(6) + "-" + Path.GetFileName(FileQuyetDinh.FileName);
                    string _path = Path.Combine(Server.MapPath("~/Content/uploads"), _FileName);
                    data.FileQuyetDinh = _FileName;
                    FileQuyetDinh.SaveAs(_path);
                }

                if (FileDanhSachCanBoXacMinh != null)
                {
                    string _FileName = user.GetRandomPassword(6) + "-" + Path.GetFileName(FileDanhSachCanBoXacMinh.FileName);
                    string _path = Path.Combine(Server.MapPath("~/Content/uploads"), _FileName);
                    data.FileDanhSachCanBoXacMinh = _FileName;
                    FileDanhSachCanBoXacMinh.SaveAs(_path);
                }

                if (FileNhiemVuQuyenHan != null)
                {
                    string _FileName = user.GetRandomPassword(6) + "-" + Path.GetFileName(FileNhiemVuQuyenHan.FileName);
                    string _path = Path.Combine(Server.MapPath("~/Content/uploads"), _FileName);
                    data.FileNhiemVuQuyenHan = _FileName;
                    FileNhiemVuQuyenHan.SaveAs(_path);
                }

                if (FileCoQuanToChucPhoiHop != null)
                {
                    string _FileName = user.GetRandomPassword(6) + "-" + Path.GetFileName(FileCoQuanToChucPhoiHop.FileName);
                    string _path = Path.Combine(Server.MapPath("~/Content/uploads"), _FileName);
                    data.FileCoQuanToChucPhoiHop = _FileName;
                    FileCoQuanToChucPhoiHop.SaveAs(_path);
                }

                db.Entry(data).State = EntityState.Modified;
                db.SaveChanges();

                return Json(new { status = "success", title = "Cập Nhật Dữ Liệu Thành Công", message = "Dữ Liệu Ra Quyết Định Xác Minh Đã Được Cập Nhật", ID_KeHoach = nV_LapKeHoachXacMinh_RaQuyetDinhXacMinh.ID_KeHoachXacMinh }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult LuuDuLieuGiaiTrinh([Bind(Include = "ID_GiaiTrinhTaiSanThuNhap,ID_KeHoachXacMinh,NgayLapBienBan,TenNguoiDuocXacMinh,NoiDungBienBan")] NV_LapKeHoachXacMinh_GiaiTrinhTaiSanThuNhap nV_LapKeHoachXacMinh_GiaiTrinhTaiSanThuNhap, HttpPostedFileBase FileBienBan)
        {
            if (nV_LapKeHoachXacMinh_GiaiTrinhTaiSanThuNhap.ID_GiaiTrinhTaiSanThuNhap == 0)
            {
                if (FileBienBan != null)
                {
                    string _FileName = user.GetRandomPassword(6) + "-" + Path.GetFileName(FileBienBan.FileName);
                    string _path = Path.Combine(Server.MapPath("~/Content/uploads"), _FileName);
                    nV_LapKeHoachXacMinh_GiaiTrinhTaiSanThuNhap.FileBienBan = _FileName;
                    FileBienBan.SaveAs(_path);
                }

                db.NV_LapKeHoachXacMinh_GiaiTrinhTaiSanThuNhap.Add(nV_LapKeHoachXacMinh_GiaiTrinhTaiSanThuNhap);
                db.SaveChanges();

                return Json(new { status = "success", title = "Lưu Dữ Liệu Thành Công", message = "Biên Bản Làm Việc Đã Được Lưu", ID_KeHoach = nV_LapKeHoachXacMinh_GiaiTrinhTaiSanThuNhap.ID_KeHoachXacMinh }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var data = db.NV_LapKeHoachXacMinh_GiaiTrinhTaiSanThuNhap.Where(_ => _.ID_GiaiTrinhTaiSanThuNhap == nV_LapKeHoachXacMinh_GiaiTrinhTaiSanThuNhap.ID_GiaiTrinhTaiSanThuNhap).First();
                data.NgayLapBienBan = nV_LapKeHoachXacMinh_GiaiTrinhTaiSanThuNhap.NgayLapBienBan;
                data.TenNguoiDuocXacMinh = nV_LapKeHoachXacMinh_GiaiTrinhTaiSanThuNhap.TenNguoiDuocXacMinh;
                data.NoiDungBienBan = nV_LapKeHoachXacMinh_GiaiTrinhTaiSanThuNhap.NoiDungBienBan;

                if (FileBienBan != null)
                {
                    string _FileName = user.GetRandomPassword(6) + "-" + Path.GetFileName(FileBienBan.FileName);
                    string _path = Path.Combine(Server.MapPath("~/Content/uploads"), _FileName);
                    data.FileBienBan = _FileName;
                    FileBienBan.SaveAs(_path);
                }

                db.Entry(data).State = EntityState.Modified;
                db.SaveChanges();

                return Json(new { status = "success", title = "Cập Nhật Dữ Liệu Thành Công", message = "Biên Bản Làm Việc Đã Được Cập Nhật", ID_KeHoach = nV_LapKeHoachXacMinh_GiaiTrinhTaiSanThuNhap.ID_KeHoachXacMinh }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult DeleteDuLieuGiaiTrinh(int? id)
        {
            var data = db.NV_LapKeHoachXacMinh_GiaiTrinhTaiSanThuNhap.Where(_ => _.ID_GiaiTrinhTaiSanThuNhap == id).First();
            db.NV_LapKeHoachXacMinh_GiaiTrinhTaiSanThuNhap.Remove(data);
            db.SaveChanges();

            return Json(new { status = "success", title = "Xóa Dữ Liệu Thành Công", message = "Biên Bản Làm Việc Đã Được Xóa" }, JsonRequestBehavior.AllowGet);

        }

        public JsonResult LuuDuLieuXacMinh([Bind(Include = "ID_TienHanhXacMinh,ID_KeHoachXacMinh,NgayLapBienBan,TenNguoiDuocXacMinh,NoiDungBienBan")] NV_LapKeHoachXacMinh_TienHanhXacMinh nV_LapKeHoachXacMinh_TienHanhXacMinh, HttpPostedFileBase FileBienBan)
        {
            if (nV_LapKeHoachXacMinh_TienHanhXacMinh.ID_TienHanhXacMinh == 0)
            {
                if (FileBienBan != null)
                {
                    string _FileName = user.GetRandomPassword(6) + "-" + Path.GetFileName(FileBienBan.FileName);
                    string _path = Path.Combine(Server.MapPath("~/Content/uploads"), _FileName);
                    nV_LapKeHoachXacMinh_TienHanhXacMinh.FileBienBan = _FileName;
                    FileBienBan.SaveAs(_path);
                }

                db.NV_LapKeHoachXacMinh_TienHanhXacMinh.Add(nV_LapKeHoachXacMinh_TienHanhXacMinh);
                db.SaveChanges();

                return Json(new { status = "success", title = "Lưu Dữ Liệu Thành Công", message = "Biên Bản Xác Minh Đã Được Lưu", ID_KeHoach = nV_LapKeHoachXacMinh_TienHanhXacMinh.ID_KeHoachXacMinh }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var data = db.NV_LapKeHoachXacMinh_TienHanhXacMinh.Where(_ => _.ID_TienHanhXacMinh == nV_LapKeHoachXacMinh_TienHanhXacMinh.ID_TienHanhXacMinh).First();
                data.NgayLapBienBan = nV_LapKeHoachXacMinh_TienHanhXacMinh.NgayLapBienBan;
                data.TenNguoiDuocXacMinh = nV_LapKeHoachXacMinh_TienHanhXacMinh.TenNguoiDuocXacMinh;
                data.NoiDungBienBan = nV_LapKeHoachXacMinh_TienHanhXacMinh.NoiDungBienBan;

                if (FileBienBan != null)
                {
                    string _FileName = user.GetRandomPassword(6) + "-" + Path.GetFileName(FileBienBan.FileName);
                    string _path = Path.Combine(Server.MapPath("~/Content/uploads"), _FileName);
                    data.FileBienBan = _FileName;
                    FileBienBan.SaveAs(_path);
                }

                db.Entry(data).State = EntityState.Modified;
                db.SaveChanges();

                return Json(new { status = "success", title = "Cập Nhật Dữ Liệu Thành Công", message = "Biên Bản Xác Minh Đã Được Cập Nhật", ID_KeHoach = nV_LapKeHoachXacMinh_TienHanhXacMinh.ID_KeHoachXacMinh }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult DeleteDuLieuXacMinh(int? id)
        {
            var data = db.NV_LapKeHoachXacMinh_TienHanhXacMinh.Where(_ => _.ID_TienHanhXacMinh == id).First();
            db.NV_LapKeHoachXacMinh_TienHanhXacMinh.Remove(data);
            db.SaveChanges();

            return Json(new { status = "success", title = "Xóa Dữ Liệu Thành Công", message = "Biên Bản Xác Minh Đã Được Xóa" }, JsonRequestBehavior.AllowGet);

        }


        public JsonResult LuuDuLieuBaoCao([Bind(Include = "ID_BaoCaoKetQuaXacMinh,ID_KeHoachXacMinh,SoBaoCao,NgayBaoCao")] NV_LapKeHoachXacMinh_BaoCaoKetQuaXacMinh nV_LapKeHoachXacMinh_BaoCaoKetQuaXacMinh, HttpPostedFileBase FileBaoCao)
        {
            if (nV_LapKeHoachXacMinh_BaoCaoKetQuaXacMinh.ID_BaoCaoKetQuaXacMinh == 0)
            {
                if (FileBaoCao != null)
                {
                    string _FileName = user.GetRandomPassword(6) + "-" + Path.GetFileName(FileBaoCao.FileName);
                    string _path = Path.Combine(Server.MapPath("~/Content/uploads"), _FileName);
                    nV_LapKeHoachXacMinh_BaoCaoKetQuaXacMinh.FileBaoCao = _FileName;
                    FileBaoCao.SaveAs(_path);
                }

                db.NV_LapKeHoachXacMinh_BaoCaoKetQuaXacMinh.Add(nV_LapKeHoachXacMinh_BaoCaoKetQuaXacMinh);
                db.SaveChanges();

                return Json(new { status = "success", title = "Lưu Dữ Liệu Thành Công", message = "Báo Cáo Kết Quả Xác Minh Đã Được Lưu", ID_KeHoach = nV_LapKeHoachXacMinh_BaoCaoKetQuaXacMinh.ID_KeHoachXacMinh }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var data = db.NV_LapKeHoachXacMinh_BaoCaoKetQuaXacMinh.Where(_ => _.ID_BaoCaoKetQuaXacMinh == nV_LapKeHoachXacMinh_BaoCaoKetQuaXacMinh.ID_BaoCaoKetQuaXacMinh).First();
                data.NgayBaoCao = nV_LapKeHoachXacMinh_BaoCaoKetQuaXacMinh.NgayBaoCao;
                data.SoBaoCao = nV_LapKeHoachXacMinh_BaoCaoKetQuaXacMinh.SoBaoCao;

                if (FileBaoCao != null)
                {
                    string _FileName = user.GetRandomPassword(6) + "-" + Path.GetFileName(FileBaoCao.FileName);
                    string _path = Path.Combine(Server.MapPath("~/Content/uploads"), _FileName);
                    nV_LapKeHoachXacMinh_BaoCaoKetQuaXacMinh.FileBaoCao = _FileName;
                    FileBaoCao.SaveAs(_path);
                }

                db.Entry(data).State = EntityState.Modified;
                db.SaveChanges();

                return Json(new { status = "success", title = "Cập Nhật Dữ Liệu Thành Công", message = "Báo Cáo Kết Quả Xác Minh Đã Được Cập Nhật", ID_KeHoach = nV_LapKeHoachXacMinh_BaoCaoKetQuaXacMinh.ID_KeHoachXacMinh }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult DeleteDuLieuBaoCao(int? id)
        {
            var data = db.NV_LapKeHoachXacMinh_BaoCaoKetQuaXacMinh.Where(_ => _.ID_BaoCaoKetQuaXacMinh == id).First();
            db.NV_LapKeHoachXacMinh_BaoCaoKetQuaXacMinh.Remove(data);
            db.SaveChanges();

            return Json(new { status = "success", title = "Xóa Dữ Liệu Thành Công", message = "Báo Cáo Kết Quả Xác Minh Đã Được Xóa" }, JsonRequestBehavior.AllowGet);

        }


        public JsonResult LuuDuLieuKetLuan([Bind(Include = "ID_KetLuanXacMinh,ID_KeHoachXacMinh,SoKetLuan,NgayKetLuan,NoiDungKetLuan")] NV_LapKeHoachXacMinh_KetLuanXacMinh nV_LapKeHoachXacMinh_KetLuanXacMinh, HttpPostedFileBase FileKetLuan)
        {
            if (nV_LapKeHoachXacMinh_KetLuanXacMinh.ID_KetLuanXacMinh == 0)
            {
                if (FileKetLuan != null)
                {
                    string _FileName = user.GetRandomPassword(6) + "-" + Path.GetFileName(FileKetLuan.FileName);
                    string _path = Path.Combine(Server.MapPath("~/Content/uploads"), _FileName);
                    nV_LapKeHoachXacMinh_KetLuanXacMinh.FileKetLuan = _FileName;
                    FileKetLuan.SaveAs(_path);
                }

                db.NV_LapKeHoachXacMinh_KetLuanXacMinh.Add(nV_LapKeHoachXacMinh_KetLuanXacMinh);
                db.SaveChanges();

                return Json(new { status = "success", title = "Lưu Dữ Liệu Thành Công", message = "Kết Luận Xác Minh Đã Được Lưu", ID_KeHoach = nV_LapKeHoachXacMinh_KetLuanXacMinh.ID_KeHoachXacMinh }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var data = db.NV_LapKeHoachXacMinh_KetLuanXacMinh.Where(_ => _.ID_KetLuanXacMinh == nV_LapKeHoachXacMinh_KetLuanXacMinh.ID_KetLuanXacMinh).First();
                data.NgayKetLuan = nV_LapKeHoachXacMinh_KetLuanXacMinh.NgayKetLuan;
                data.SoKetLuan = nV_LapKeHoachXacMinh_KetLuanXacMinh.SoKetLuan;
                data.NoiDungKetLuan = nV_LapKeHoachXacMinh_KetLuanXacMinh.NoiDungKetLuan;


                if (FileKetLuan != null)
                {
                    string _FileName = user.GetRandomPassword(6) + "-" + Path.GetFileName(FileKetLuan.FileName);
                    string _path = Path.Combine(Server.MapPath("~/Content/uploads"), _FileName);
                    data.FileKetLuan = _FileName;
                    FileKetLuan.SaveAs(_path);
                }

                db.Entry(data).State = EntityState.Modified;
                db.SaveChanges();

                return Json(new { status = "success", title = "Cập Nhật Dữ Liệu Thành Công", message = "Kết Luận Xác Minh Đã Được Cập Nhật", ID_KeHoach = nV_LapKeHoachXacMinh_KetLuanXacMinh.ID_KeHoachXacMinh }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult DeleteDuLieuKetLuan(int? id)
        {
            var data = db.NV_LapKeHoachXacMinh_KetLuanXacMinh.Where(_ => _.ID_KetLuanXacMinh == id).First();
            db.NV_LapKeHoachXacMinh_KetLuanXacMinh.Remove(data);
            db.SaveChanges();

            return Json(new { status = "success", title = "Xóa Dữ Liệu Thành Công", message = "Kết Luận Xác Minh Đã Được Xóa" }, JsonRequestBehavior.AllowGet);

        }

        public JsonResult LuuDuLieuCongKhai([Bind(Include = "ID_GuiVaCongKhaiKetLuanXacMinh,ID_KeHoachXacMinh,NgayCongKhaiKetLuan,NoiDungBienBan")] NV_LapKeHoachXacMinh_GuiVaCongKhaiKetLuanXacMinh nV_LapKeHoachXacMinh_GuiVaCongKhaiKetLuanXacMinh, HttpPostedFileBase FileBienBan)
        {
            if (nV_LapKeHoachXacMinh_GuiVaCongKhaiKetLuanXacMinh.ID_GuiVaCongKhaiKetLuanXacMinh == 0)
            {
                if (FileBienBan != null)
                {
                    string _FileName = user.GetRandomPassword(6) + "-" + Path.GetFileName(FileBienBan.FileName);
                    string _path = Path.Combine(Server.MapPath("~/Content/uploads"), _FileName);
                    nV_LapKeHoachXacMinh_GuiVaCongKhaiKetLuanXacMinh.FileBienBan = _FileName;
                    FileBienBan.SaveAs(_path);
                }

                db.NV_LapKeHoachXacMinh_GuiVaCongKhaiKetLuanXacMinh.Add(nV_LapKeHoachXacMinh_GuiVaCongKhaiKetLuanXacMinh);
                db.SaveChanges();

                return Json(new { status = "success", title = "Lưu Dữ Liệu Thành Công", message = "Công Khai Kết Luận Xác Minh Đã Được Lưu", ID_KeHoach = nV_LapKeHoachXacMinh_GuiVaCongKhaiKetLuanXacMinh.ID_KeHoachXacMinh }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var data = db.NV_LapKeHoachXacMinh_GuiVaCongKhaiKetLuanXacMinh.Where(_ => _.ID_GuiVaCongKhaiKetLuanXacMinh == nV_LapKeHoachXacMinh_GuiVaCongKhaiKetLuanXacMinh.ID_GuiVaCongKhaiKetLuanXacMinh).First();
                data.NgayCongKhaiKetLuan = nV_LapKeHoachXacMinh_GuiVaCongKhaiKetLuanXacMinh.NgayCongKhaiKetLuan;
                data.NoiDungBienBan = nV_LapKeHoachXacMinh_GuiVaCongKhaiKetLuanXacMinh.NoiDungBienBan;


                if (FileBienBan != null)
                {
                    string _FileName = user.GetRandomPassword(6) + "-" + Path.GetFileName(FileBienBan.FileName);
                    string _path = Path.Combine(Server.MapPath("~/Content/uploads"), _FileName);
                    data.FileBienBan = _FileName;
                    FileBienBan.SaveAs(_path);
                }

                db.Entry(data).State = EntityState.Modified;
                db.SaveChanges();

                return Json(new { status = "success", title = "Cập Nhật Dữ Liệu Thành Công", message = "Công Khai Kết Luận Xác Minh Đã Được Cập Nhật", ID_KeHoach = nV_LapKeHoachXacMinh_GuiVaCongKhaiKetLuanXacMinh.ID_KeHoachXacMinh }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult DeleteDuLieuCongKhai(int? id)
        {
            var data = db.NV_LapKeHoachXacMinh_GuiVaCongKhaiKetLuanXacMinh.Where(_ => _.ID_GuiVaCongKhaiKetLuanXacMinh == id).First();
            db.NV_LapKeHoachXacMinh_GuiVaCongKhaiKetLuanXacMinh.Remove(data);
            db.SaveChanges();

            return Json(new { status = "success", title = "Xóa Dữ Liệu Thành Công", message = "Công Khai Kết Luận Xác Minh Đã Được Xóa" }, JsonRequestBehavior.AllowGet);

        }
    }
}