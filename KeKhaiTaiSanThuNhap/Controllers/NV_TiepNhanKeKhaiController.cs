using DocumentFormat.OpenXml.Bibliography;
using DocumentFormat.OpenXml.Drawing;
using DocumentFormat.OpenXml.Vml.Office;
using KeKhaiTaiSanThuNhap.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;

namespace KeKhaiTaiSanThuNhap.Controllers
{
    public class NV_TiepNhanKeKhaiController : Controller
    {
        private KSTNEntities db = new KSTNEntities();
        private UserInfo user = new UserInfo();

        // GET: NV_TiepNhanKeKhai
        public ActionResult Index()
        {
            if (user.CheckQuyen("NV_TiepNhanKekhai", "Xem"))
            {
               
                return new HttpStatusCodeResult(404, "Not found");
            }
            ViewBag.Role = user.GetRole();
            return View();
        }




        public JsonResult LoadData()
        {
            
            var draw = Request.Form.GetValues("draw").FirstOrDefault();
            var start = Request.Form.GetValues("start").FirstOrDefault();
            var length = Request.Form.GetValues("length").FirstOrDefault();
            int pageSize = length != null ? Convert.ToInt32(length) : 0;
            int skip = start != null ? Convert.ToInt32(start) : 0;
            int recordsTotal = 0;
            var UserID = user.GetUser();
            var maCoQuan = user.GetUserCoQuan();
            var role = user.GetRole();
            var search = Request.Form.GetValues("columns[0][search][value]").FirstOrDefault();
            var loaiKeKhai_search = Request.Form.GetValues("columns[1][search][value]").FirstOrDefault();
            var coQuan_search = Request.Form.GetValues("columns[2][search][value]").FirstOrDefault();
            var namKekhai_search = Request.Form.GetValues("columns[3][search][value]").FirstOrDefault();
            var trangthai_search = Request.Form.GetValues("columns[4][search][value]").FirstOrDefault();
            var datakk_ld = db.Nv_KeKhai_TSTN.Where(_ => _.Ma_CoQuan_DonVi == maCoQuan & _.Ma_Loai_KeKhai == 3).Select(_ => _.MaKeHoachKeKhai).ToList();
            var datakk_hn = db.Nv_KeKhai_TSTN.Where(_ => _.Ma_CoQuan_DonVi == maCoQuan & _.Ma_Loai_KeKhai == 4).Select(_ => _.MaKeHoachKeKhai).ToList();
            var datakk_bs = db.Nv_KeKhai_TSTN.Where(_ => _.Ma_CoQuan_DonVi == maCoQuan & _.Ma_Loai_KeKhai == 5).Select(_ => _.MaKeHoachKeKhai).ToList();
            var datakk_bnctcb = db.Nv_KeKhai_TSTN.Where(_ => _.Ma_CoQuan_DonVi == maCoQuan & _.Ma_Loai_KeKhai == 12).Select(_ => _.MaKeHoachKeKhai).ToList();
            var completekk = db.Nv_KeKhai_TSTN.Where(_ => _.Ma_CoQuan_DonVi == maCoQuan & _.TrangThai == true).Select(_ => _.MaKeHoachKeKhai).ToList();

            var datald = (from cb in db.DM_CanBo
                            join dsbsct in db.NV_LapKeHoachKeKhai_DanhSachKeKhaiLanDau_ChiTiet on cb.Ma_CanBo equals dsbsct.Ma_CanBo
                            join dsbs in db.NV_LapKeHoachKeKhai_DanhSachKeKhaiLanDau on dsbsct.MaKeHoachKeKhai_DanhSachKeKhaiLanDau_ID equals dsbs.MaKeHoachKeKhai_DanhSachKeKhaiLanDau_ID
                            join dskhkk in db.NV_LapKeHoachKeKhai on dsbs.MaKeHoachKeKhai equals dskhkk.MaKeHoachKeKhai
                            join cq in db.DM_CoQuanDonVi on dsbs.Ma_CoQuan_DonVi equals cq.Ma_CoQuan_DonVi
                            join bkk in db.Nv_KeKhai_TSTN on cb.Ma_CanBo equals bkk.Ma_CanBo
                            where dsbs.TrangThai == true && bkk.MaKeHoachKeKhai == dsbs.MaKeHoachKeKhai && bkk.Ma_CanBo == dsbsct.Ma_CanBo && bkk.TrangThai == true && bkk.Ma_CoQuan_DonVi != null
                            select new KeKhaiTSTN
                            {
                                Ma_KeKhai = db.Nv_KeKhai_TSTN.FirstOrDefault(_ => _.Ma_CanBo == cb.Ma_CanBo && _.MaKeHoachKeKhai == dsbs.MaKeHoachKeKhai && _.Ma_Loai_KeKhai == 3).Ma_KeKhai_TSTN == null ? 0 : db.Nv_KeKhai_TSTN.FirstOrDefault(_ => _.Ma_CanBo == cb.Ma_CanBo && _.MaKeHoachKeKhai == dsbs.MaKeHoachKeKhai && _.Ma_Loai_KeKhai == 3).Ma_KeKhai_TSTN,
                                FileDinhKem = db.Nv_KeKhai_TSTN.FirstOrDefault(_ => _.Ma_CanBo == cb.Ma_CanBo && _.MaKeHoachKeKhai == dsbs.MaKeHoachKeKhai && _.Ma_Loai_KeKhai == 3).FileDinhKem == null ? "" : db.Nv_KeKhai_TSTN.FirstOrDefault(_ => _.Ma_CanBo == cb.Ma_CanBo && _.MaKeHoachKeKhai == dsbs.MaKeHoachKeKhai && _.Ma_Loai_KeKhai == 3).FileDinhKem,
                                HoTen = cb.HoTen,
                                Ma_CanBo = cb.Ma_CanBo,
                                Ma_CoQuan_DonVi = (int)bkk.Ma_CoQuan_DonVi,
                                Ma_ChucVu_ChucDanh = (int)cb.Ma_ChucVu_ChucDanh,
                                TenKeHoachKeKhai = dskhkk.TenKeHoachKeKhai,
                                MaKeHoachKeKhai = (int)dskhkk.MaKeHoachKeKhai,
                                ThoiGianBatDau = (DateTime)dskhkk.ThoiGianBatDau,
                                ThoiGianKetThuc = (DateTime)dskhkk.ThoiGianKetThuc,
                                NghiDinh = dskhkk.NghiDinh,
                                TrangThai = (bool)dsbs.TrangThai,
                                loaiKK = "Kê Khai Lần Đầu",
                                MaLoaiKeKhai = (int)bkk.Ma_Loai_KeKhai,
                                Ten = cq.Ten,
                                KeHoachNam = (int)dskhkk.KeHoachNam,
                                TrangThaiKK = true,
                                completekk = true,
                                nhomTaiKhoan = role,
                                trangThaiTiepNhan = (int)bkk.TrangThaiTiepNhan,
                                FileKiXacNhan = bkk.FileKiXacNhan,
                                NgayTiepNhan = bkk.NgayTiepNhan,
                            }).ToList();

            var datahn = (from cb in db.DM_CanBo
                            join dsbsct in db.NV_LapKeHoachKeKhai_DanhSachKeKhaiHangNam_ChiTiet on cb.Ma_CanBo equals dsbsct.Ma_CanBo
                            join dsbs in db.NV_LapKeHoachKeKhai_DanhSachKeKhaiHangNam on dsbsct.MaKeHoachKeKhai_DanhSachKeKhaiHangNam_ID equals dsbs.MaKeHoachKeKhai_DanhSachKeKhaiHangNam_ID
                            join dskhkk in db.NV_LapKeHoachKeKhai on dsbs.MaKeHoachKeKhai equals dskhkk.MaKeHoachKeKhai
                            join cq in db.DM_CoQuanDonVi on dsbs.Ma_CoQuan_DonVi equals cq.Ma_CoQuan_DonVi
                          join bkk in db.Nv_KeKhai_TSTN on cb.Ma_CanBo equals bkk.Ma_CanBo
                          where dsbs.TrangThai == true && bkk.MaKeHoachKeKhai == dsbs.MaKeHoachKeKhai && bkk.Ma_CanBo == dsbsct.Ma_CanBo  && bkk.TrangThai == true && bkk.Ma_CoQuan_DonVi != null
                          select new KeKhaiTSTN
                            {
                                Ma_KeKhai = db.Nv_KeKhai_TSTN.FirstOrDefault(_ => _.Ma_CanBo == cb.Ma_CanBo && _.MaKeHoachKeKhai == dsbs.MaKeHoachKeKhai && _.Ma_Loai_KeKhai == 4).Ma_KeKhai_TSTN == null ? 0 : db.Nv_KeKhai_TSTN.FirstOrDefault(_ => _.Ma_CanBo == cb.Ma_CanBo && _.MaKeHoachKeKhai == dsbs.MaKeHoachKeKhai && _.Ma_Loai_KeKhai == 4).Ma_KeKhai_TSTN,
                                FileDinhKem = db.Nv_KeKhai_TSTN.FirstOrDefault(_ => _.Ma_CanBo == cb.Ma_CanBo && _.MaKeHoachKeKhai == dsbs.MaKeHoachKeKhai && _.Ma_Loai_KeKhai == 4).FileDinhKem == null ? "" : db.Nv_KeKhai_TSTN.FirstOrDefault(_ => _.Ma_CanBo == cb.Ma_CanBo && _.MaKeHoachKeKhai == dsbs.MaKeHoachKeKhai && _.Ma_Loai_KeKhai == 4).FileDinhKem,
                                HoTen = cb.HoTen,
                                Ma_CanBo = cb.Ma_CanBo,

                                Ma_CoQuan_DonVi = (int)bkk.Ma_CoQuan_DonVi,
                                Ma_ChucVu_ChucDanh = (int)cb.Ma_ChucVu_ChucDanh,
                                TenKeHoachKeKhai = dskhkk.TenKeHoachKeKhai,
                                MaKeHoachKeKhai = (int)dskhkk.MaKeHoachKeKhai,
                                ThoiGianBatDau = (DateTime)dskhkk.ThoiGianBatDau,
                                ThoiGianKetThuc = (DateTime)dskhkk.ThoiGianKetThuc,
                                NghiDinh = dskhkk.NghiDinh,
                                TrangThai = (bool)dsbs.TrangThai,
                                loaiKK = "Kê Khai Hằng Năm",
                                MaLoaiKeKhai = (int)bkk.Ma_Loai_KeKhai,
                                Ten = cq.Ten,
                                KeHoachNam = (int)dskhkk.KeHoachNam,
                                TrangThaiKK = true,
                                completekk = true,
                                nhomTaiKhoan = role,
                                trangThaiTiepNhan = (int)bkk.TrangThaiTiepNhan,
                                FileKiXacNhan = bkk.FileKiXacNhan,
                              NgayTiepNhan = bkk.NgayTiepNhan,
                          }).ToList();

            var databs = (from cb in db.DM_CanBo
                            join dsbsct in db.NV_LapKeHoachKeKhai_DanhSachKeKhaiBoSung_ChiTiet on cb.Ma_CanBo equals dsbsct.Ma_CanBo
                            join dsbs in db.NV_LapKeHoachKeKhai_DanhSachKeKhaiBoSung on dsbsct.MaKeHoachKeKhai_DanhSachKeKhaiBoSung_ID equals dsbs.MaKeHoachKeKhai_DanhSachKeKhaiBoSung_ID
                            join dskhkk in db.NV_LapKeHoachKeKhai on dsbs.MaKeHoachKeKhai equals dskhkk.MaKeHoachKeKhai
                            join cq in db.DM_CoQuanDonVi on dsbs.Ma_CoQuan_DonVi equals cq.Ma_CoQuan_DonVi
                          join bkk in db.Nv_KeKhai_TSTN on cb.Ma_CanBo equals bkk.Ma_CanBo
                          where  dsbs.TrangThai == true && bkk.MaKeHoachKeKhai == dsbs.MaKeHoachKeKhai && bkk.Ma_CanBo == dsbsct.Ma_CanBo  && bkk.TrangThai == true && bkk.Ma_CoQuan_DonVi != null
                          select new KeKhaiTSTN
                            {
                                Ma_KeKhai = db.Nv_KeKhai_TSTN.FirstOrDefault(_ => _.Ma_CanBo == cb.Ma_CanBo && _.MaKeHoachKeKhai == dsbs.MaKeHoachKeKhai && _.Ma_Loai_KeKhai == 5).Ma_KeKhai_TSTN == null ? 0 : db.Nv_KeKhai_TSTN.FirstOrDefault(_ => _.Ma_CanBo == cb.Ma_CanBo && _.MaKeHoachKeKhai == dsbs.MaKeHoachKeKhai && _.Ma_Loai_KeKhai == 5).Ma_KeKhai_TSTN,
                                FileDinhKem = db.Nv_KeKhai_TSTN.FirstOrDefault(_ => _.Ma_CanBo == cb.Ma_CanBo && _.MaKeHoachKeKhai == dsbs.MaKeHoachKeKhai && _.Ma_Loai_KeKhai == 5).FileDinhKem == null ? "" : db.Nv_KeKhai_TSTN.FirstOrDefault(_ => _.Ma_CanBo == cb.Ma_CanBo && _.MaKeHoachKeKhai == dsbs.MaKeHoachKeKhai && _.Ma_Loai_KeKhai == 5).FileDinhKem,
                                HoTen = cb.HoTen,
                                Ma_CanBo = cb.Ma_CanBo,
                                Ma_CoQuan_DonVi = (int)bkk.Ma_CoQuan_DonVi,
                                Ma_ChucVu_ChucDanh = (int)cb.Ma_ChucVu_ChucDanh,
                                TenKeHoachKeKhai = dskhkk.TenKeHoachKeKhai,
                                MaKeHoachKeKhai = (int)dskhkk.MaKeHoachKeKhai,
                                ThoiGianBatDau = (DateTime)dskhkk.ThoiGianBatDau,
                                ThoiGianKetThuc = (DateTime)dskhkk.ThoiGianKetThuc,
                                NghiDinh = dskhkk.NghiDinh,
                                TrangThai = (bool)dsbs.TrangThai,
                                loaiKK = "Kê Khai Bổ Sung",
                              MaLoaiKeKhai = (int)bkk.Ma_Loai_KeKhai,
                              Ten = cq.Ten,
                                KeHoachNam = (int)dskhkk.KeHoachNam,
                                TrangThaiKK = true,
                                completekk = true,
                                nhomTaiKhoan = role,
                                trangThaiTiepNhan = (int)bkk.TrangThaiTiepNhan,
                                FileKiXacNhan = bkk.FileKiXacNhan,
                              NgayTiepNhan = bkk.NgayTiepNhan,
                          }).ToList();

            var databnctcb = (from cb in db.DM_CanBo
                                join dsbsct in db.NV_LapKeHoachKeKhai_DanhSachKeKhaiBoNhiemCongTacCanBo_ChiTiet on cb.Ma_CanBo equals dsbsct.Ma_CanBo
                                join dsbs in db.NV_LapKeHoachKeKhai_DanhSachKeKhaiBoNhiemCongTacCanBo on dsbsct.MaKeHoachKeKhai_DanhSachKeKhaiBoNhiemCongTacCanBo_ID equals dsbs.MaKeHoachKeKhai_DanhSachKeKhaiBoNhiemCongTacCanBo_ID
                                join dskhkk in db.NV_LapKeHoachKeKhai on dsbs.MaKeHoachKeKhai equals dskhkk.MaKeHoachKeKhai
                                join cq in db.DM_CoQuanDonVi on dsbs.Ma_CoQuan_DonVi equals cq.Ma_CoQuan_DonVi
                              join bkk in db.Nv_KeKhai_TSTN on cb.Ma_CanBo equals bkk.Ma_CanBo
                              where  dsbs.TrangThai == true && bkk.MaKeHoachKeKhai == dsbs.MaKeHoachKeKhai && bkk.Ma_CanBo == dsbsct.Ma_CanBo  && bkk.TrangThai == true && bkk.Ma_CoQuan_DonVi != null
                              select new KeKhaiTSTN
                                {
                                    Ma_KeKhai = db.Nv_KeKhai_TSTN.FirstOrDefault(_ => _.Ma_CanBo == cb.Ma_CanBo && _.MaKeHoachKeKhai == dsbs.MaKeHoachKeKhai && _.Ma_Loai_KeKhai == 12).Ma_KeKhai_TSTN == null ? 0 : db.Nv_KeKhai_TSTN.FirstOrDefault(_ => _.Ma_CanBo == cb.Ma_CanBo && _.MaKeHoachKeKhai == dsbs.MaKeHoachKeKhai && _.Ma_Loai_KeKhai == 12).Ma_KeKhai_TSTN,
                                    FileDinhKem = db.Nv_KeKhai_TSTN.FirstOrDefault(_ => _.Ma_CanBo == cb.Ma_CanBo && _.MaKeHoachKeKhai == dsbs.MaKeHoachKeKhai && _.Ma_Loai_KeKhai == 12).FileDinhKem == null ? "" : db.Nv_KeKhai_TSTN.FirstOrDefault(_ => _.Ma_CanBo == cb.Ma_CanBo && _.MaKeHoachKeKhai == dsbs.MaKeHoachKeKhai && _.Ma_Loai_KeKhai == 12).FileDinhKem,
                                    HoTen = cb.HoTen,
                                    Ma_CanBo = cb.Ma_CanBo,
                                    Ma_CoQuan_DonVi = (int)bkk.Ma_CoQuan_DonVi,
                                    Ma_ChucVu_ChucDanh = (int)cb.Ma_ChucVu_ChucDanh,
                                    TenKeHoachKeKhai = dskhkk.TenKeHoachKeKhai,
                                    MaKeHoachKeKhai = (int)dskhkk.MaKeHoachKeKhai,
                                    ThoiGianBatDau = (DateTime)dskhkk.ThoiGianBatDau,
                                    ThoiGianKetThuc = (DateTime)dskhkk.ThoiGianKetThuc,
                                    NghiDinh = dskhkk.NghiDinh,
                                    TrangThai = (bool)dsbs.TrangThai,
                                    loaiKK = "Phục Vụ Công Tác Cán Bộ",
                                    MaLoaiKeKhai = (int)bkk.Ma_Loai_KeKhai,
                                    Ten = cq.Ten,
                                    KeHoachNam = (int)dskhkk.KeHoachNam,
                                    TrangThaiKK = true,
                                    completekk = true,
                                    trangThaiTiepNhan = (int)bkk.TrangThaiTiepNhan,
                                    nhomTaiKhoan = role,
                                    FileKiXacNhan = bkk.FileKiXacNhan,
                                  NgayTiepNhan = bkk.NgayTiepNhan,
                              }).ToList();

            var data = databs.Concat(datald).Concat(datahn).Concat(databnctcb).OrderByDescending(_ => _.KeHoachNam).ToList();
            
            if(role == "NDDTTT")
            {
                data = data.Where(_ => _.trangThaiTiepNhan == 2).ToList();

                
            }
            else if (role == "NTNKK") {
                data = data.Where(_ => _.trangThaiTiepNhan == 1 || _.trangThaiTiepNhan == 2  && _.Ma_CoQuan_DonVi == maCoQuan).ToList();
                
            }


            if (!string.IsNullOrEmpty(search))
            {
                data = data.Where(a => a.HoTen.ToUpper().Contains(search.ToUpper()) || a.TenKeHoachKeKhai.ToUpper().Contains(search.ToUpper()) || a.KeHoachNam.ToString() == search).ToList();
            }
            if (!string.IsNullOrEmpty(loaiKeKhai_search) && loaiKeKhai_search != null && loaiKeKhai_search != "")
            {
                try
                {
                    var maLoaiKK = Int32.Parse(loaiKeKhai_search);
                    if (maLoaiKK == 0)
                    {

                    }
                    else
                    {
                        data = data.Where(a => a.MaLoaiKeKhai == maLoaiKK).ToList();
                    }
                        
                }
                catch
                {

                }

            }
            if (!string.IsNullOrEmpty(coQuan_search) && coQuan_search != null)
            {
                try
                {
                    var maCoQuanKK = Int32.Parse(coQuan_search);
                    if (maCoQuanKK == 0)
                    {

                    }
                    else
                    {
                        data = data.Where(a => a.Ma_CoQuan_DonVi == maCoQuanKK).ToList();
                    }

                }
                catch
                {

                }

            }

            if(!string.IsNullOrEmpty(trangthai_search) && trangthai_search != null)
            {
                try
                {
                    var trangthai_tiepnhan = Int32.Parse(trangthai_search);
                    if (trangthai_tiepnhan == 0)
                    {

                    }
                    else
                    {
                        data = data.Where(a => a.trangThaiTiepNhan == trangthai_tiepnhan).ToList();
                    }

                }
                catch
                {

                }

            }
            if (!string.IsNullOrEmpty(namKekhai_search) && namKekhai_search != null)
            {
                try
                {

                    var namKekhai = Int32.Parse(namKekhai_search);

                    data = data.Where(a => a.KeHoachNam == namKekhai).ToList();


                }
                catch
                {

                }
            }

            recordsTotal = data.Count();
            var data1 = data.Skip(skip).Take(pageSize).ToList();
            return Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data1, UserID }, JsonRequestBehavior.AllowGet);

        }

        public JsonResult GuiThanhTra(int MaKeHoachKeKhai, int Ma_CanBo)
        {
            var BanKeKhai = db.Nv_KeKhai_TSTN.Where(_ => _.MaKeHoachKeKhai == MaKeHoachKeKhai && _.Ma_CanBo == Ma_CanBo).FirstOrDefault();
            if(BanKeKhai != null)
            {
                BanKeKhai.TrangThaiTiepNhan = 2;
                
                db.Entry(BanKeKhai).State = EntityState.Modified;
                db.SaveChanges();
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
           
        }

        public JsonResult completeKeKhai(int MaKeKhai)
        {

            var MaCanBo = user.GetUser();
            var CoQuanID = user.GetUserCoQuan();
            var idBanKeKhai = db.Nv_KeKhai_TSTN.Where(_ => _.Ma_KeKhai_TSTN == MaKeKhai && _.Ma_CoQuan_DonVi == CoQuanID).FirstOrDefault();

            if (idBanKeKhai.FileKiXacNhan == "" || idBanKeKhai.FileKiXacNhan == null)
            {
                return Json(new { status = "warning", message = "Bạn Chưa Đính Kèm Bản Kê Khai Đã Có Chữ Ký. Vui Lòng Thực Hiện!" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                idBanKeKhai.TrangThaiTiepNhan = 2;
               
                db.Entry(idBanKeKhai).State = EntityState.Modified;
                db.SaveChanges();
                return Json(new { status = "success", message = "Đã Hoàn Thành Và Gửi Bản Kê Khai Đi." }, JsonRequestBehavior.AllowGet);
            }
        }


        public ActionResult DinhKemFile(int? MaKeKhai, HttpPostedFileBase FileDinhKem)
        {
            var userID = user.GetUser();
            var CoQuanID = user.GetUserCoQuan();
            var data = db.Nv_KeKhai_TSTN.Where(_ => _.Ma_KeKhai_TSTN == MaKeKhai && _.Ma_CoQuan_DonVi == CoQuanID).FirstOrDefault();
            if (FileDinhKem.ContentLength > 0)
            {
                string _FileName = user.GetRandomPassword(6) + "-" + System.IO.Path.GetFileName(FileDinhKem.FileName);
                string _path = System.IO.Path.Combine(Server.MapPath("~/Content/uploads"), _FileName);
                data.FileKiXacNhan = _FileName;
                FileDinhKem.SaveAs(_path);
                data.NgayTiepNhan = DateTime.Now;
                db.Entry(data).State = EntityState.Modified;
                db.SaveChanges();

                return Json(new { status = "success", message = "Đã Đính Kèm Bản Kê Khai Thành Công. Chọn Hoàn Thành Để Kết Thúc Quá Trình Kê Khai Tài Sản, Thu Nhập" }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { status = "warning", message = "File Đính Kèm Không Tồn Tại, Vui Lòng Kiểm Tra Lại" }, JsonRequestBehavior.AllowGet);
        }


        public class KeKhaiTSTN
        {
            public int Ma_KeKhai { get; set; }
            public string HoTen { get; set; }
            public int Ma_CanBo { get; set; }
            public int Ma_CoQuan_DonVi { get; set; }

            public int Ma_ChucVu_ChucDanh { get; set; }

            public string TenKeHoachKeKhai { get; set; }
            public int MaKeHoachKeKhai { get; set; }

            public DateTime ThoiGianKetThuc { get; set; }

            public DateTime ThoiGianBatDau { get; set; }

            public string NghiDinh { get; set; }

            public bool TrangThai { get; set; }
            public string loaiKK { get; set; }
            public int MaLoaiKeKhai { get; set; }
            public int trangThaiTiepNhan  { get; set; }


            public string Ten { get; set; }

            public string FileDinhKem { get; set; }

            public int KeHoachNam { get; set; }

            public bool TrangThaiKK { get; set; }
            public bool daKhoa { get; set; }
            public bool completekk { get; set; }
            public string nhomTaiKhoan { get; set; }
            public string FileKiXacNhan { get; set; }
            public DateTime? NgayTiepNhan { get; set; }

        }



    }
}