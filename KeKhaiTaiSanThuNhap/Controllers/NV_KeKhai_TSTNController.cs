using DocumentFormat.OpenXml.Wordprocessing;
using ExcelDataReader.Log.Logger;
using iText.Html2pdf;
using iText.Html2pdf.Resolver.Font;
using KeKhaiTaiSanThuNhap.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Globalization;
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
    public class NV_KeKhai_TSTNController : Controller
    {

        private KSTNEntities db = new KSTNEntities();
        private UserInfo user = new UserInfo();

        public ActionResult GetFileDinhKem(int id)
        {
            
            var userID = user.GetUser();
            var CoQuanID = user.GetUserCoQuan();
            var role = user.GetRole();
            var data = db.Nv_KeKhai_TSTN.Where(_ => _.Ma_KeKhai_TSTN == id && _.Ma_CoQuan_DonVi == CoQuanID ).FirstOrDefault().FileDinhKem;

            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetFileDinhKemXacMinh(int id)
        {

            var userID = user.GetUser();
            var CoQuanID = user.GetUserCoQuan();
            var role = user.GetRole();
            var data = db.Nv_KeKhai_TSTN.Where(_ => _.Ma_KeKhai_TSTN == id && _.Ma_CoQuan_DonVi == CoQuanID).FirstOrDefault().FileKiXacNhan;

            return Json(data, JsonRequestBehavior.AllowGet);
        }


        public ActionResult BanIn(string id)
        {
            ViewBag.TenFile = id + ".pdf";
            return View();
        }

        public JsonResult GetLoaiKeKhai(int MaKeHoachKeKhai)
        {
            var MaCanBo = user.GetUser();
            var KHKK = db.NV_LapKeHoachKeKhai.Where(_ => _.MaKeHoachKeKhai == MaKeHoachKeKhai).First();

            if(KHKK.Ma_Loai_KeKhai == 3)
            {
                var data = db.DM_Loai_KeKhai.Where(_ => _.Ma_Loai_KeKhai == 3).First();
                return Json(data, JsonRequestBehavior.AllowGet);
            }
            else if(KHKK.Ma_Loai_KeKhai == 4)
            {
                var data_hn = (from cb in db.DM_CanBo
                               join cv in db.DM_ChucVu_ChucDanh on cb.Ma_ChucVu_ChucDanh equals cv.Ma_ChucVu_ChucDanh
                               join dskkhn_ct in db.NV_LapKeHoachKeKhai_DanhSachKeKhaiHangNam_ChiTiet on cb.Ma_CanBo equals dskkhn_ct.Ma_CanBo
                               join dskkhn in db.NV_LapKeHoachKeKhai_DanhSachKeKhaiHangNam on dskkhn_ct.MaKeHoachKeKhai_DanhSachKeKhaiHangNam_ID equals dskkhn.MaKeHoachKeKhai_DanhSachKeKhaiHangNam_ID
                               join lkkhn in db.NV_LapKeHoachKeKhai on dskkhn.MaKeHoachKeKhai equals lkkhn.MaKeHoachKeKhai
                               join cq in db.DM_CoQuanDonVi on lkkhn.Ma_CoQuan_DonVi equals cq.Ma_CoQuan_DonVi
                               join tk in db.HT_TaiKhoan on cb.Ma_CanBo equals tk.Ma_CanBo
                               join ntk in db.HT_NhomTaiKhoan on tk.MaNhomTaiKhoan equals ntk.MaNhomTaiKhoan
                               where lkkhn.MaKeHoachKeKhai == MaKeHoachKeKhai
                               orderby ntk.Sort
                               select cb.Ma_CanBo).ToList();

                if (data_hn.Contains(MaCanBo))
                {
                    var data = db.DM_Loai_KeKhai.Where(_ => _.Ma_Loai_KeKhai == 4).First();
                    return Json(data, JsonRequestBehavior.AllowGet);
                }

                else
                {
                    var data = db.DM_Loai_KeKhai.Where(_ => _.Ma_Loai_KeKhai == 5).First();
                    return Json(data, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                var data = db.DM_Loai_KeKhai.Where(_ => _.Ma_Loai_KeKhai == KHKK.Ma_Loai_KeKhai).First();
                return Json(data, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult Index()
        {
            if (user.CheckQuyen("NV_KeKhaiTaiSan", "Xem"))
            {
                return new HttpStatusCodeResult(404, "Not found");
            }
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
         
            var data = (from nvkk in db.Nv_KeKhai_TSTN
                        join cb in db.DM_CanBo on nvkk.Ma_CanBo equals cb.Ma_CanBo
                        join lkk in db.DM_Loai_KeKhai on nvkk.Ma_Loai_KeKhai equals lkk.Ma_Loai_KeKhai
                        where cb.Ma_CanBo == UserID
                        orderby nvkk.NgayThangNam descending
                        select new { nvkk.Ma_KeKhai_TSTN, nvkk.Ma_CanBo, cb.HoTen, nvkk.Nam_KeKhai, nvkk.NgayThangNam, nvkk.Thang_KeKhai, nvkk.Ngay_KeKhai, nvkk.Ma_Loai_KeKhai, lkk.Ten_KeKhai, nvkk.Ma_LinhVuc_KeKhai }).ToList();

          
            recordsTotal = data.Count();
            var data1 = data.Skip(skip).Take(pageSize).ToList();
            return Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data1, UserID }, JsonRequestBehavior.AllowGet);
        }


        public JsonResult LoadDataKeHoachKeKhai()
        {
            var draw = Request.Form.GetValues("draw").FirstOrDefault();
            var start = Request.Form.GetValues("start").FirstOrDefault();
            var length = Request.Form.GetValues("length").FirstOrDefault();
            int pageSize = length != null ? Convert.ToInt32(length) : 0;
            int skip = start != null ? Convert.ToInt32(start) : 0;
            int recordsTotal = 0;
            var UserID = user.GetUser();
            var maCoQuan = user.GetUserCoQuan();
            var search = Request.Form.GetValues("columns[0][search][value]").FirstOrDefault();
            var datakk_ld = db.Nv_KeKhai_TSTN.Where(_ => _.Ma_CanBo == UserID & _.Ma_Loai_KeKhai == 3).Select(_ => _.MaKeHoachKeKhai).ToList();
            var datakk_hn = db.Nv_KeKhai_TSTN.Where(_ => _.Ma_CanBo == UserID & _.Ma_Loai_KeKhai == 4).Select(_ => _.MaKeHoachKeKhai).ToList();
            var datakk_bs = db.Nv_KeKhai_TSTN.Where(_ => _.Ma_CanBo == UserID & _.Ma_Loai_KeKhai == 5).Select(_ => _.MaKeHoachKeKhai).ToList();
            var datakk_bnctcb = db.Nv_KeKhai_TSTN.Where(_ => _.Ma_CanBo == UserID & _.Ma_Loai_KeKhai == 12).Select(_ => _.MaKeHoachKeKhai).ToList();
            var completekk = db.Nv_KeKhai_TSTN.Where(_ => _.Ma_CanBo == UserID & _.TrangThai == true).Select(_ => _.MaKeHoachKeKhai).ToList();

            var datald = (from cb in db.DM_CanBo
                          join dsbsct in db.NV_LapKeHoachKeKhai_DanhSachKeKhaiLanDau_ChiTiet on cb.Ma_CanBo equals dsbsct.Ma_CanBo
                          join dsbs in db.NV_LapKeHoachKeKhai_DanhSachKeKhaiLanDau on dsbsct.MaKeHoachKeKhai_DanhSachKeKhaiLanDau_ID equals dsbs.MaKeHoachKeKhai_DanhSachKeKhaiLanDau_ID
                          join dskhkk in db.NV_LapKeHoachKeKhai on dsbs.MaKeHoachKeKhai equals dskhkk.MaKeHoachKeKhai
                          join cq in db.DM_CoQuanDonVi on dsbs.Ma_CoQuan_DonVi equals cq.Ma_CoQuan_DonVi
                          where cb.Ma_CanBo == UserID && dsbs.TrangThai == true
                          select new KeKhaiTSTN { Ma_KeKhai = db.Nv_KeKhai_TSTN.FirstOrDefault(_ => _.Ma_CanBo == cb.Ma_CanBo && _.MaKeHoachKeKhai == dsbs.MaKeHoachKeKhai && _.Ma_Loai_KeKhai == 3).Ma_KeKhai_TSTN == null ? 0 : db.Nv_KeKhai_TSTN.FirstOrDefault(_ => _.Ma_CanBo == cb.Ma_CanBo && _.MaKeHoachKeKhai == dsbs.MaKeHoachKeKhai && _.Ma_Loai_KeKhai == 3).Ma_KeKhai_TSTN,
                                                  FileDinhKem = db.Nv_KeKhai_TSTN.FirstOrDefault(_ => _.Ma_CanBo == cb.Ma_CanBo && _.MaKeHoachKeKhai == dsbs.MaKeHoachKeKhai && _.Ma_Loai_KeKhai == 3).FileDinhKem == null ? "" : db.Nv_KeKhai_TSTN.FirstOrDefault(_ => _.Ma_CanBo == cb.Ma_CanBo && _.MaKeHoachKeKhai == dsbs.MaKeHoachKeKhai && _.Ma_Loai_KeKhai == 3).FileDinhKem,
                                                  HoTen = cb.HoTen, 
                                                  Ma_CanBo = cb.Ma_CanBo, 
                                                  Ma_CoQuan_DonVi = (int)cb.Ma_CoQuan_DonVi, 
                                                  Ma_ChucVu_ChucDanh = (int)cb.Ma_ChucVu_ChucDanh, 
                                                  TenKeHoachKeKhai = dskhkk.TenKeHoachKeKhai, 
                                                  MaKeHoachKeKhai = (int)dskhkk.MaKeHoachKeKhai, 
                                                  ThoiGianBatDau = (DateTime)dskhkk.ThoiGianBatDau, 
                                                  ThoiGianKetThuc = (DateTime)dskhkk.ThoiGianKetThuc, 
                                                  NghiDinh = dskhkk.NghiDinh,
                                                  TrangThai = (bool)dsbs.TrangThai, 
                                                  loaiKK = "Kê Khai Lần Đầu", 
                                                  MaLoaiKeKhai = 3,
                                                  Ten = cq.Ten, 
                                                  KeHoachNam = (int)dskhkk.KeHoachNam, 
                                                  TrangThaiKK = datakk_ld.Contains((int)dskhkk.MaKeHoachKeKhai), 
                                                  completekk = completekk.Contains((int)dsbs.MaKeHoachKeKhai) }).ToList();

            var datahn = (from cb in db.DM_CanBo
                          join dsbsct in db.NV_LapKeHoachKeKhai_DanhSachKeKhaiHangNam_ChiTiet on cb.Ma_CanBo equals dsbsct.Ma_CanBo
                          join dsbs in db.NV_LapKeHoachKeKhai_DanhSachKeKhaiHangNam on dsbsct.MaKeHoachKeKhai_DanhSachKeKhaiHangNam_ID equals dsbs.MaKeHoachKeKhai_DanhSachKeKhaiHangNam_ID
                          join dskhkk in db.NV_LapKeHoachKeKhai on dsbs.MaKeHoachKeKhai equals dskhkk.MaKeHoachKeKhai
                          join cq in db.DM_CoQuanDonVi on dsbs.Ma_CoQuan_DonVi equals cq.Ma_CoQuan_DonVi
                          where cb.Ma_CanBo == UserID && dsbs.TrangThai == true
                          select new KeKhaiTSTN
                          {
                              Ma_KeKhai = db.Nv_KeKhai_TSTN.FirstOrDefault(_ => _.Ma_CanBo == cb.Ma_CanBo && _.MaKeHoachKeKhai == dsbs.MaKeHoachKeKhai && _.Ma_Loai_KeKhai == 4).Ma_KeKhai_TSTN == null ? 0 : db.Nv_KeKhai_TSTN.FirstOrDefault(_ => _.Ma_CanBo == cb.Ma_CanBo && _.MaKeHoachKeKhai == dsbs.MaKeHoachKeKhai && _.Ma_Loai_KeKhai == 4).Ma_KeKhai_TSTN,
                              FileDinhKem = db.Nv_KeKhai_TSTN.FirstOrDefault(_ => _.Ma_CanBo == cb.Ma_CanBo && _.MaKeHoachKeKhai == dsbs.MaKeHoachKeKhai && _.Ma_Loai_KeKhai == 4).FileDinhKem == null ? "" : db.Nv_KeKhai_TSTN.FirstOrDefault(_ => _.Ma_CanBo == cb.Ma_CanBo && _.MaKeHoachKeKhai == dsbs.MaKeHoachKeKhai && _.Ma_Loai_KeKhai == 4).FileDinhKem,
                              HoTen = cb.HoTen,
                              Ma_CanBo = cb.Ma_CanBo,
                              Ma_CoQuan_DonVi = (int)cb.Ma_CoQuan_DonVi,
                              Ma_ChucVu_ChucDanh = (int)cb.Ma_ChucVu_ChucDanh,
                              TenKeHoachKeKhai = dskhkk.TenKeHoachKeKhai,
                              MaKeHoachKeKhai = (int)dskhkk.MaKeHoachKeKhai,
                              ThoiGianBatDau = (DateTime)dskhkk.ThoiGianBatDau,
                              ThoiGianKetThuc = (DateTime)dskhkk.ThoiGianKetThuc,
                              NghiDinh = dskhkk.NghiDinh,
                              TrangThai = (bool)dsbs.TrangThai,
                              loaiKK = "Kê Khai Hằng Năm",
                              MaLoaiKeKhai = 4,
                              Ten = cq.Ten,
                              KeHoachNam = (int)dskhkk.KeHoachNam,
                              TrangThaiKK = datakk_hn.Contains((int)dskhkk.MaKeHoachKeKhai),
                              completekk = completekk.Contains((int)dsbs.MaKeHoachKeKhai)
                          }).ToList();

            var databs = (from cb in db.DM_CanBo
                          join dsbsct in db.NV_LapKeHoachKeKhai_DanhSachKeKhaiBoSung_ChiTiet on cb.Ma_CanBo equals dsbsct.Ma_CanBo
                          join dsbs in db.NV_LapKeHoachKeKhai_DanhSachKeKhaiBoSung on dsbsct.MaKeHoachKeKhai_DanhSachKeKhaiBoSung_ID equals dsbs.MaKeHoachKeKhai_DanhSachKeKhaiBoSung_ID
                          join dskhkk in db.NV_LapKeHoachKeKhai on dsbs.MaKeHoachKeKhai equals dskhkk.MaKeHoachKeKhai
                          join cq in db.DM_CoQuanDonVi on dsbs.Ma_CoQuan_DonVi equals cq.Ma_CoQuan_DonVi
                          where cb.Ma_CanBo == UserID && dsbs.TrangThai == true
                          select new KeKhaiTSTN
                          {
                              Ma_KeKhai = db.Nv_KeKhai_TSTN.FirstOrDefault(_ => _.Ma_CanBo == cb.Ma_CanBo && _.MaKeHoachKeKhai == dsbs.MaKeHoachKeKhai && _.Ma_Loai_KeKhai == 5).Ma_KeKhai_TSTN == null ? 0 : db.Nv_KeKhai_TSTN.FirstOrDefault(_ => _.Ma_CanBo == cb.Ma_CanBo && _.MaKeHoachKeKhai == dsbs.MaKeHoachKeKhai && _.Ma_Loai_KeKhai == 5).Ma_KeKhai_TSTN,
                              FileDinhKem = db.Nv_KeKhai_TSTN.FirstOrDefault(_ => _.Ma_CanBo == cb.Ma_CanBo && _.MaKeHoachKeKhai == dsbs.MaKeHoachKeKhai && _.Ma_Loai_KeKhai == 5).FileDinhKem == null ? "" : db.Nv_KeKhai_TSTN.FirstOrDefault(_ => _.Ma_CanBo == cb.Ma_CanBo && _.MaKeHoachKeKhai == dsbs.MaKeHoachKeKhai && _.Ma_Loai_KeKhai == 5).FileDinhKem,
                              HoTen = cb.HoTen,
                              Ma_CanBo = cb.Ma_CanBo,
                              Ma_CoQuan_DonVi = (int)cb.Ma_CoQuan_DonVi,
                              Ma_ChucVu_ChucDanh = (int)cb.Ma_ChucVu_ChucDanh,
                              TenKeHoachKeKhai = dskhkk.TenKeHoachKeKhai,
                              MaKeHoachKeKhai = (int)dskhkk.MaKeHoachKeKhai,
                              ThoiGianBatDau = (DateTime)dskhkk.ThoiGianBatDau,
                              ThoiGianKetThuc = (DateTime)dskhkk.ThoiGianKetThuc,
                              NghiDinh = dskhkk.NghiDinh,
                              TrangThai = (bool)dsbs.TrangThai,
                              loaiKK = "Kê Khai Bổ Sung",
                              MaLoaiKeKhai = 5,
                              Ten = cq.Ten,
                              KeHoachNam = (int)dskhkk.KeHoachNam,
                              TrangThaiKK = datakk_bs.Contains((int)dskhkk.MaKeHoachKeKhai),
                              completekk = completekk.Contains((int)dsbs.MaKeHoachKeKhai)
                          }).ToList();

            var databnctcb = (from cb in db.DM_CanBo
                          join dsbsct in db.NV_LapKeHoachKeKhai_DanhSachKeKhaiBoNhiemCongTacCanBo_ChiTiet on cb.Ma_CanBo equals dsbsct.Ma_CanBo
                          join dsbs in db.NV_LapKeHoachKeKhai_DanhSachKeKhaiBoNhiemCongTacCanBo on dsbsct.MaKeHoachKeKhai_DanhSachKeKhaiBoNhiemCongTacCanBo_ID equals dsbs.MaKeHoachKeKhai_DanhSachKeKhaiBoNhiemCongTacCanBo_ID
                          join dskhkk in db.NV_LapKeHoachKeKhai on dsbs.MaKeHoachKeKhai equals dskhkk.MaKeHoachKeKhai
                          join cq in db.DM_CoQuanDonVi on dsbs.Ma_CoQuan_DonVi equals cq.Ma_CoQuan_DonVi
                          where cb.Ma_CanBo == UserID && dsbs.TrangThai == true
                          select new KeKhaiTSTN
                          {
                              Ma_KeKhai = db.Nv_KeKhai_TSTN.FirstOrDefault(_ => _.Ma_CanBo == cb.Ma_CanBo && _.MaKeHoachKeKhai == dsbs.MaKeHoachKeKhai && _.Ma_Loai_KeKhai == 12).Ma_KeKhai_TSTN == null ? 0 : db.Nv_KeKhai_TSTN.FirstOrDefault(_ => _.Ma_CanBo == cb.Ma_CanBo && _.MaKeHoachKeKhai == dsbs.MaKeHoachKeKhai && _.Ma_Loai_KeKhai == 12).Ma_KeKhai_TSTN,
                              FileDinhKem = db.Nv_KeKhai_TSTN.FirstOrDefault(_ => _.Ma_CanBo == cb.Ma_CanBo && _.MaKeHoachKeKhai == dsbs.MaKeHoachKeKhai && _.Ma_Loai_KeKhai == 12).FileDinhKem == null ? "" : db.Nv_KeKhai_TSTN.FirstOrDefault(_ => _.Ma_CanBo == cb.Ma_CanBo && _.MaKeHoachKeKhai == dsbs.MaKeHoachKeKhai && _.Ma_Loai_KeKhai == 12).FileDinhKem,
                              HoTen = cb.HoTen,
                              Ma_CanBo = cb.Ma_CanBo,
                              Ma_CoQuan_DonVi = (int)cb.Ma_CoQuan_DonVi,
                              Ma_ChucVu_ChucDanh = (int)cb.Ma_ChucVu_ChucDanh,
                              TenKeHoachKeKhai = dskhkk.TenKeHoachKeKhai,
                              MaKeHoachKeKhai = (int)dskhkk.MaKeHoachKeKhai,
                              ThoiGianBatDau = (DateTime)dskhkk.ThoiGianBatDau,
                              ThoiGianKetThuc = (DateTime)dskhkk.ThoiGianKetThuc,
                              NghiDinh = dskhkk.NghiDinh,
                              TrangThai = (bool)dsbs.TrangThai,
                              loaiKK = "Phục Vụ Công Tác Cán Bộ",
                              MaLoaiKeKhai = 12,
                              Ten = cq.Ten,
                              KeHoachNam = (int)dskhkk.KeHoachNam,
                              TrangThaiKK = datakk_bnctcb.Contains((int)dskhkk.MaKeHoachKeKhai),
                              completekk = completekk.Contains((int)dsbs.MaKeHoachKeKhai)
                          }).ToList();

            var data = databs.Concat(datald).Concat(datahn).Concat(databnctcb).OrderByDescending(_ => _.KeHoachNam).ToList();

            if (!string.IsNullOrEmpty(search))
            {
                data = data.Where(a => a.TenKeHoachKeKhai.ToUpper().Contains(search.ToUpper()) || a.KeHoachNam.ToString() == search).ToList();
            }

            recordsTotal = data.Count();
            var data1 = data.Skip(skip).Take(pageSize).ToList();
            return Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data1, UserID }, JsonRequestBehavior.AllowGet);
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
            public int? MaLoaiKeKhai { get; set; }

            public string Ten { get; set; }

            public string FileDinhKem { get; set; }

            public int KeHoachNam { get; set; }

            public bool TrangThaiKK { get; set; }

            public bool daKhoa { get; set; }

            public bool completekk { get; set; }
        }

        public ActionResult Edit(int? id)
        {
            
            if (user.CheckQuyen("NV_KeKhaiTaiSan", "Sua"))
            {
                return new HttpStatusCodeResult(404, "Not found");
            }


            var MaCanBo = user.GetUser();
            var KKTS = db.Nv_KeKhai_TSTN.Find(id);
            if(KKTS.Ma_CanBo != MaCanBo)
            {
                return new HttpStatusCodeResult(404, "Not found");
            }

            if ((bool)KKTS.TrangThai)
            {
                return new HttpStatusCodeResult(404, "Not found");
            }

            ViewBag.MaKeHoachKeKhai = KKTS.MaKeHoachKeKhai;
            ViewBag.Ma_KeKhai_TSTN = id;
            ViewBag.Loai_KeKhai = KKTS.Ma_Loai_KeKhai;
            if (KKTS.Ma_Loai_KeKhai != 3)
            {
                ViewBag.CheckLoai = true;
            }
            else
            {
                ViewBag.CheckLoai = false;

            }
            
            return View();
        }

        public ActionResult LapBanKeKhai(int? id)
        {

            if (user.CheckQuyen("NV_KeKhaiTaiSan", "Them"))
            {
                return new HttpStatusCodeResult(404, "Not found");
            }
            var MaCanBo = user.GetUser();

            var KHKK = db.NV_LapKeHoachKeKhai.Where(_ => _.MaKeHoachKeKhai == id).First();

            if (KHKK.Ma_Loai_KeKhai == 3)
            {
               var data = (from cb in db.DM_CanBo
                                  join cv in db.DM_ChucVu_ChucDanh on cb.Ma_ChucVu_ChucDanh equals cv.Ma_ChucVu_ChucDanh
                                  join dskkhn_ct in db.NV_LapKeHoachKeKhai_DanhSachKeKhaiLanDau_ChiTiet on cb.Ma_CanBo equals dskkhn_ct.Ma_CanBo
                                  join dskkhn in db.NV_LapKeHoachKeKhai_DanhSachKeKhaiLanDau on dskkhn_ct.MaKeHoachKeKhai_DanhSachKeKhaiLanDau_ID equals dskkhn.MaKeHoachKeKhai_DanhSachKeKhaiLanDau_ID
                                  join lkkhn in db.NV_LapKeHoachKeKhai on dskkhn.MaKeHoachKeKhai equals lkkhn.MaKeHoachKeKhai
                                  join cq in db.DM_CoQuanDonVi on lkkhn.Ma_CoQuan_DonVi equals cq.Ma_CoQuan_DonVi
                                  join tk in db.HT_TaiKhoan on cb.Ma_CanBo equals tk.Ma_CanBo
                                  join ntk in db.HT_NhomTaiKhoan on tk.MaNhomTaiKhoan equals ntk.MaNhomTaiKhoan
                                  where lkkhn.MaKeHoachKeKhai == id
                                  orderby ntk.Sort
                                  select cb.Ma_CanBo).ToList();

                if (!data.Contains(MaCanBo))
                {
                    return new HttpStatusCodeResult(404, "Not found");
                }
            }
            else if (KHKK.Ma_Loai_KeKhai == 4)
            {
                var data_hn = (from cb in db.DM_CanBo
                                join cv in db.DM_ChucVu_ChucDanh on cb.Ma_ChucVu_ChucDanh equals cv.Ma_ChucVu_ChucDanh
                                join dskkhn_ct in db.NV_LapKeHoachKeKhai_DanhSachKeKhaiHangNam_ChiTiet on cb.Ma_CanBo equals dskkhn_ct.Ma_CanBo
                                join dskkhn in db.NV_LapKeHoachKeKhai_DanhSachKeKhaiHangNam on dskkhn_ct.MaKeHoachKeKhai_DanhSachKeKhaiHangNam_ID equals dskkhn.MaKeHoachKeKhai_DanhSachKeKhaiHangNam_ID
                                join lkkhn in db.NV_LapKeHoachKeKhai on dskkhn.MaKeHoachKeKhai equals lkkhn.MaKeHoachKeKhai
                                join cq in db.DM_CoQuanDonVi on lkkhn.Ma_CoQuan_DonVi equals cq.Ma_CoQuan_DonVi
                                join tk in db.HT_TaiKhoan on cb.Ma_CanBo equals tk.Ma_CanBo
                                join ntk in db.HT_NhomTaiKhoan on tk.MaNhomTaiKhoan equals ntk.MaNhomTaiKhoan
                                where lkkhn.MaKeHoachKeKhai == id
                                orderby ntk.Sort
                                select cb.Ma_CanBo).ToList();
                var data_bs = (from cb in db.DM_CanBo
                                          join cv in db.DM_ChucVu_ChucDanh on cb.Ma_ChucVu_ChucDanh equals cv.Ma_ChucVu_ChucDanh
                                          join dskkhn_ct in db.NV_LapKeHoachKeKhai_DanhSachKeKhaiBoSung_ChiTiet on cb.Ma_CanBo equals dskkhn_ct.Ma_CanBo
                                          join dskkhn in db.NV_LapKeHoachKeKhai_DanhSachKeKhaiBoSung on dskkhn_ct.MaKeHoachKeKhai_DanhSachKeKhaiBoSung_ID equals dskkhn.MaKeHoachKeKhai_DanhSachKeKhaiBoSung_ID
                                          join lkkhn in db.NV_LapKeHoachKeKhai on dskkhn.MaKeHoachKeKhai equals lkkhn.MaKeHoachKeKhai
                                          join cq in db.DM_CoQuanDonVi on lkkhn.Ma_CoQuan_DonVi equals cq.Ma_CoQuan_DonVi
                                          join tk in db.HT_TaiKhoan on cb.Ma_CanBo equals tk.Ma_CanBo
                                          join ntk in db.HT_NhomTaiKhoan on tk.MaNhomTaiKhoan equals ntk.MaNhomTaiKhoan
                                          where lkkhn.MaKeHoachKeKhai == id
                                          orderby ntk.Sort
                                          select cb.Ma_CanBo).ToList();
                if (!data_hn.Contains(MaCanBo) && !data_bs.Contains(MaCanBo))
                {
                    return new HttpStatusCodeResult(404, "Not found");
                }
            }
            else
            {
                var data = (from cb in db.DM_CanBo
                            join cv in db.DM_ChucVu_ChucDanh on cb.Ma_ChucVu_ChucDanh equals cv.Ma_ChucVu_ChucDanh
                            join dskkhn_ct in db.NV_LapKeHoachKeKhai_DanhSachKeKhaiBoNhiemCongTacCanBo_ChiTiet on cb.Ma_CanBo equals dskkhn_ct.Ma_CanBo
                            join dskkhn in db.NV_LapKeHoachKeKhai_DanhSachKeKhaiBoNhiemCongTacCanBo on dskkhn_ct.MaKeHoachKeKhai_DanhSachKeKhaiBoNhiemCongTacCanBo_ID equals dskkhn.MaKeHoachKeKhai_DanhSachKeKhaiBoNhiemCongTacCanBo_ID
                            join lkkhn in db.NV_LapKeHoachKeKhai on dskkhn.MaKeHoachKeKhai equals lkkhn.MaKeHoachKeKhai
                            join cq in db.DM_CoQuanDonVi on lkkhn.Ma_CoQuan_DonVi equals cq.Ma_CoQuan_DonVi
                            join tk in db.HT_TaiKhoan on cb.Ma_CanBo equals tk.Ma_CanBo
                            join ntk in db.HT_NhomTaiKhoan on tk.MaNhomTaiKhoan equals ntk.MaNhomTaiKhoan
                            where lkkhn.MaKeHoachKeKhai == id
                            orderby ntk.Sort
                            select cb.Ma_CanBo).ToList();
                if (!data.Contains(MaCanBo))
                {
                    return new HttpStatusCodeResult(404, "Not found");
                }
            }


            var kk = db.Nv_KeKhai_TSTN.Where(_ => _.Ma_CanBo == MaCanBo && _.MaKeHoachKeKhai == id);
            if(kk.Count() != 0)
            {
                return new HttpStatusCodeResult(404, "Not found");
            }

            ViewBag.MaKeHoachKeKhai = id;
            
            var data1 = db.NV_LapKeHoachKeKhai.Where(_ => _.MaKeHoachKeKhai == id).FirstOrDefault().Ma_Loai_KeKhai;
            ViewBag.Loai_KeKhai = data1;
            if (data1 == 3)
            {
                ViewBag.CheckLoai = false;
            }
            else
            {
                ViewBag.CheckLoai = true;
            }
            return View();
        }

       
        public ActionResult DinhKemFile(int? MaKeKhai, HttpPostedFileBase FileDinhKem)
        {
            var userID = user.GetUser();
            var data = db.Nv_KeKhai_TSTN.Where(_ => _.Ma_KeKhai_TSTN == MaKeKhai && _.Ma_CanBo == userID).FirstOrDefault();
            if (FileDinhKem.ContentLength > 0)
            {
                string _FileName = user.GetRandomPassword(6) + "-" + Path.GetFileName(FileDinhKem.FileName);
                string _path = Path.Combine(Server.MapPath("~/Content/uploads"), _FileName);
                data.FileDinhKem = _FileName;
                FileDinhKem.SaveAs(_path);

                db.Entry(data).State = EntityState.Modified;
                db.SaveChanges();

                return Json(new { status = "success", message = "Đã Đính Kèm Bản Kê Khai Thành Công. Chọn Hoàn Thành Để Kết Thúc Quá Trình Kê Khai Tài Sản, Thu Nhập" }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { status = "warning", message = "File Đính Kèm Không Tồn Tại, Vui Lòng Kiểm Tra Lại" }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetThongTinNguoiKeKhai()
        {
            var userID = user.GetUser();

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
            }
            catch
            {
                return Json(new { status = false, MaCanBo = MaCanBo }, JsonRequestBehavior.AllowGet);
            }
            try
            {
                var data = (from user in db.DM_CanBo
                            where user.Ma_CanBo == userID
                            join cq in db.DM_CoQuanDonVi on user.Ma_CoQuan_DonVi equals cq.Ma_CoQuan_DonVi
                            join cv in db.DM_ChucVu_ChucDanh on user.Ma_ChucVu_ChucDanh equals cv.Ma_ChucVu_ChucDanh
                            select new
                            {
                                HoTen = user.HoTen,
                                DoB = user.DoB,
                                DiaChiThuongTru = user.DiaChiThuongTru,
                                SoCCCD = user.SoCCCD,
                                NgayCap = user.NgayCap,
                                NoiCap = user.NoiCap,
                                Ma_CoQuan_DonVi = cq.Ten,
                                Ma_ChucVu_ChucDanh = cv.Ten_ChucVu_ChucDanh,
                                Ma_CanBo = user.Ma_CanBo,
                            }).Single();
            
                var data1 = (from tn in db.DM_CanBo_ThanNhan
                             where tn.Ma_CanBo == userID
                             select new
                             {
                                 HoTenThanNhan = tn.HoTen,
                                 DoBTN = tn.DoB,
                                 DiaChiThuongTruTN = tn.DiaChiThuongTru,
                                 SoCCCDTN = tn.SoCCCD,
                                 NgayCapTN = tn.NgayCap,
                                 NoiCapTN = tn.NoiCap,
                                 VaiTroThanNhan = tn.VaiTroThanNhan,
                                 NgheNghiep = tn.NgheNghiep,
                                 NoiLamViec = tn.NoiLamViec,
                             }).ToList();
                var data2 = new
                {
                    nguoikekhai = data,
                    thannhan = data1
                };
                return Json(data2, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(new { status = false, MaCanBo = MaCanBo }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult GetSuaThongTinNguoiKeKhai(int id)
        {
            var bankekhai = (from bkk in db.Nv_KeKhai_TSTN
                             where bkk.Ma_KeKhai_TSTN == id
                             join lkk in db.DM_Loai_KeKhai on bkk.Ma_Loai_KeKhai equals lkk.Ma_Loai_KeKhai
                             select new { bkk.Ma_CanBo, bkk.Ma_KeKhai_TSTN, bkk.Ma_LinhVuc_KeKhai, bkk.Ma_Loai_KeKhai, bkk.Nam_KeKhai, bkk.NgayThangNam, bkk.Ngay_KeKhai, bkk.Thang_KeKhai, lkk.Ten_KeKhai, bkk.MaKeHoachKeKhai, bkk.BienDongTaiSan }).SingleOrDefault();

            var data = (from user in db.DM_CanBo
                        where user.Ma_CanBo == bankekhai.Ma_CanBo
                        join cq in db.DM_CoQuanDonVi on user.Ma_CoQuan_DonVi equals cq.Ma_CoQuan_DonVi
                        join cv in db.DM_ChucVu_ChucDanh on user.Ma_ChucVu_ChucDanh equals cv.Ma_ChucVu_ChucDanh
                        select new
                        {
                            HoTen = user.HoTen,
                            DoB = user.DoB,
                            DiaChiThuongTru = user.DiaChiThuongTru,
                            SoCCCD = user.SoCCCD,
                            NgayCap = user.NgayCap,
                            NoiCap = user.NoiCap,
                            Ma_CoQuan_DonVi = cq.Ma_CoQuan_DonVi,
                            Ma_ChucVu_ChucDanh = cv.Ma_ChucVu_ChucDanh,
                            Ma_CanBo = user.Ma_CanBo,
                            TenChucDanh = cv.Ten_ChucVu_ChucDanh,
                            TenDonVi = cq.Ten,
                        }).Single();

            var data1 = (from tn in db.DM_CanBo_ThanNhan
                         where tn.Ma_CanBo == bankekhai.Ma_CanBo
                         select new
                         {
                             HoTenThanNhan = tn.HoTen,
                             DoBTN = tn.DoB,
                             DiaChiThuongTruTN = tn.DiaChiThuongTru,
                             SoCCCDTN = tn.SoCCCD,
                             NgayCapTN = tn.NgayCap,
                             NoiCapTN = tn.NoiCap,
                             VaiTroThanNhan = tn.VaiTroThanNhan
                         }).ToList();

            var dato = (from qsdt in db.NV_KeKhai_QuyenSuDungDat
                        where qsdt.Ma_KeKhai_TSTN == bankekhai.Ma_KeKhai_TSTN && qsdt.LoaiDat == "Đất Ở"
                        select qsdt);

            var datkhac = (from qsdt in db.NV_KeKhai_QuyenSuDungDat
                           where qsdt.Ma_KeKhai_TSTN == bankekhai.Ma_KeKhai_TSTN && qsdt.LoaiDat != "Đất Ở"
                           select qsdt);

            var nhao = (from no in db.NV_KeKhai_NhaO
                        where no.Ma_KeKhai_TSTN == bankekhai.Ma_KeKhai_TSTN
                        select no);

            var congtrinhxaydung = (from ctxd in db.NV_KeKhai_CongTrinh
                        where ctxd.Ma_KeKhai_TSTN == bankekhai.Ma_KeKhai_TSTN
                        select ctxd);
            var cophieu = (from cp in db.NV_KeKhai_TaiSanPhieu
                           where cp.Ma_KeKhai_TSTN == bankekhai.Ma_KeKhai_TSTN && cp.LoaiPhieu == "CoPhieu"
                           select cp);
            var traiphieu = (from cp in db.NV_KeKhai_TaiSanPhieu
                           where cp.Ma_KeKhai_TSTN == bankekhai.Ma_KeKhai_TSTN && cp.LoaiPhieu == "TraiPhieu"
                           select cp);
            var vongop = (from cp in db.NV_KeKhai_TaiSanPhieu
                           where cp.Ma_KeKhai_TSTN == bankekhai.Ma_KeKhai_TSTN && cp.LoaiPhieu == "VonGop"
                           select cp);
            var giayto = (from cp in db.NV_KeKhai_TaiSanPhieu
                           where cp.Ma_KeKhai_TSTN == bankekhai.Ma_KeKhai_TSTN && cp.LoaiPhieu == "GiayTo"
                           select cp);
            var taisankhac = (from ts in db.NV_KeKhai_TaiSanKhac
                          where ts.Ma_KeKhai_TSTN == bankekhai.Ma_KeKhai_TSTN && ts.LoaiTaiSanKhac == "TaiSanKhac"
                          select ts);
            var giaydangky = (from ts in db.NV_KeKhai_TaiSanKhac
                              where ts.Ma_KeKhai_TSTN == bankekhai.Ma_KeKhai_TSTN && ts.LoaiTaiSanKhac == "GiayDangKy"
                              select ts);
            
          

            var caylaunam = (from cln in db.NV_KeKhai_TaiSanGanVoiDat
                             where cln.Ma_KeKhai_TSTN == bankekhai.Ma_KeKhai_TSTN && cln.LoaiTaiSan == "cln"
                             select cln);

            var rungsanxuat = (from cln in db.NV_KeKhai_TaiSanGanVoiDat
                             where cln.Ma_KeKhai_TSTN == bankekhai.Ma_KeKhai_TSTN && cln.LoaiTaiSan == "rsx"
                             select cln);

            var vatkientruc = (from cln in db.NV_KeKhai_TaiSanGanVoiDat
                               where cln.Ma_KeKhai_TSTN == bankekhai.Ma_KeKhai_TSTN && cln.LoaiTaiSan == "vkt"
                               select cln);

            var kimloaidaquy = (from kldq in db.NV_KeKhai_TaiSanTrangSuc
                               where kldq.Ma_KeKhai_TSTN == bankekhai.Ma_KeKhai_TSTN
                               select kldq);

            var tien = (from t in db.NV_KeKhai_Tien
                                where t.Ma_KeKhai_TSTN == bankekhai.Ma_KeKhai_TSTN
                                select t);

            //tài sản nước ngoài
            var tsnndato = (from qsdt in db.NV_KeKhai_TaiSanNuocNgoai_QuyenSuDungDat
                        where qsdt.Ma_KeKhai_TSTN == bankekhai.Ma_KeKhai_TSTN && qsdt.LoaiDat_TSNN == "Đất Ở"
                        select qsdt);

            var tsnndatkhac = (from qsdt in db.NV_KeKhai_TaiSanNuocNgoai_QuyenSuDungDat
                               where qsdt.Ma_KeKhai_TSTN == bankekhai.Ma_KeKhai_TSTN && qsdt.LoaiDat_TSNN != "Đất Ở"
                           select qsdt);

            var tsnnnhao = (from no in db.NV_KeKhai_TaiSanNuocNgoai_NhaO
                            where no.Ma_KeKhai_TSTN == bankekhai.Ma_KeKhai_TSTN
                        select no);

            var tsnncongtrinhxaydung = (from ctxd in db.NV_KeKhai_TaiSanNuocNgoai_CongTrinh
                                        where ctxd.Ma_KeKhai_TSTN == bankekhai.Ma_KeKhai_TSTN
                                    select ctxd);
            var tsnncophieu = (from cp in db.NV_KeKhai_TaiSanNuocNgoai_TaiSanPhieu
                               where cp.Ma_KeKhai_TSTN == bankekhai.Ma_KeKhai_TSTN && cp.LoaiPhieu_TSNN == "CoPhieu"
                           select cp);
            var tsnntraiphieu = (from cp in db.NV_KeKhai_TaiSanNuocNgoai_TaiSanPhieu
                                 where cp.Ma_KeKhai_TSTN == bankekhai.Ma_KeKhai_TSTN && cp.LoaiPhieu_TSNN == "TraiPhieu"
                             select cp);
            var tsnnvongop = (from cp in db.NV_KeKhai_TaiSanNuocNgoai_TaiSanPhieu
                              where cp.Ma_KeKhai_TSTN == bankekhai.Ma_KeKhai_TSTN && cp.LoaiPhieu_TSNN == "VonGop"
                          select cp);
            var tsnngiayto = (from cp in db.NV_KeKhai_TaiSanNuocNgoai_TaiSanPhieu
                              where cp.Ma_KeKhai_TSTN == bankekhai.Ma_KeKhai_TSTN && cp.LoaiPhieu_TSNN == "GiayTo"
                          select cp);
            var tsnntaisankhac = (from ts in db.NV_KeKhai_TaiSanNuocNgoai_TaiSanKhac
                                  where ts.Ma_KeKhai_TSTN == bankekhai.Ma_KeKhai_TSTN && ts.LoaiTaiSanKhac_TSNN == "TaiSanKhac"
                              select ts);
            var tsnngiaydangky = (from ts in db.NV_KeKhai_TaiSanNuocNgoai_TaiSanKhac
                                  where ts.Ma_KeKhai_TSTN == bankekhai.Ma_KeKhai_TSTN && ts.LoaiTaiSanKhac_TSNN == "GiayDangKy"
                              select ts);

            var tsnncaylaunam = (from cln in db.NV_KeKhai_TaiSanNuocNgoai_TaiSanGanVoiDat
                                 where cln.Ma_KeKhai_TSTN == bankekhai.Ma_KeKhai_TSTN && cln.LoaiTaiSan_TSNN == "cln"
                             select cln);

            var tsnnrungsanxuat = (from cln in db.NV_KeKhai_TaiSanNuocNgoai_TaiSanGanVoiDat
                                   where cln.Ma_KeKhai_TSTN == bankekhai.Ma_KeKhai_TSTN && cln.LoaiTaiSan_TSNN == "rsx"
                               select cln);

            var tsnnvatkientruc = (from cln in db.NV_KeKhai_TaiSanNuocNgoai_TaiSanGanVoiDat
                                   where cln.Ma_KeKhai_TSTN == bankekhai.Ma_KeKhai_TSTN && cln.LoaiTaiSan_TSNN == "vkt"
                               select cln);

            var tsnnkimloaidaquy = (from kldq in db.NV_KeKhai_TaiSanNuocNgoai_TaiSanTrangSuc
                                    where kldq.Ma_KeKhai_TSTN == bankekhai.Ma_KeKhai_TSTN
                                select kldq);

            var tsnntien = (from t in db.NV_KeKhai_TaiSanNuocNgoai_Tien
                            where t.Ma_KeKhai_TSTN == bankekhai.Ma_KeKhai_TSTN
                        select t);


            //End tài sản nước ngoài


            var taisannuocngoai = (from ts in db.NV_KeKhai_TaiSanNuocNgoai
                                   where ts.Ma_KeKhai_TSTN == bankekhai.Ma_KeKhai_TSTN
                                   select ts);

            var taikhoannuocngoai = (from ts in db.NV_KeKhai_TaiKhoanNuocNgoai
                                     where ts.Ma_KeKhai_TSTN == bankekhai.Ma_KeKhai_TSTN
                                     select ts);
            var tongthunhap = (from ts in db.NV_KeKhai_TongThuNhap
                               where ts.Ma_KeKhai_TSTN == bankekhai.Ma_KeKhai_TSTN
                               select ts);

            var biendongdato = (from cln in db.NV_KeKhai_BienDongTaiSan
                                where cln.Ma_KeKhai_TSTN == bankekhai.Ma_KeKhai_TSTN && cln.LoaiTSTN == "DatO"
                                select cln);
            var biendongdatkhac = (from cln in db.NV_KeKhai_BienDongTaiSan
                                   where cln.Ma_KeKhai_TSTN == bankekhai.Ma_KeKhai_TSTN && cln.LoaiTSTN == "DatKhac"
                                   select cln);
            var biendongnhaocongtrinh = (from cln in db.NV_KeKhai_BienDongTaiSan
                                         where cln.Ma_KeKhai_TSTN == bankekhai.Ma_KeKhai_TSTN && cln.LoaiTSTN == "NhaOCongTrinh"
                                         select cln);
            var biendongcongtrinhkhac = (from cln in db.NV_KeKhai_BienDongTaiSan
                                         where cln.Ma_KeKhai_TSTN == bankekhai.Ma_KeKhai_TSTN && cln.LoaiTSTN == "CongTrinhKhac"
                                         select cln);
            var biendongcaylaunam = (from cln in db.NV_KeKhai_BienDongTaiSan
                                     where cln.Ma_KeKhai_TSTN == bankekhai.Ma_KeKhai_TSTN && cln.LoaiTSTN == "CayLauNam"
                                     select cln);
            var biendongrung = (from cln in db.NV_KeKhai_BienDongTaiSan
                                where cln.Ma_KeKhai_TSTN == bankekhai.Ma_KeKhai_TSTN && cln.LoaiTSTN == "Rung"
                                select cln);
            var biendongvatkientruc = (from cln in db.NV_KeKhai_BienDongTaiSan
                                       where cln.Ma_KeKhai_TSTN == bankekhai.Ma_KeKhai_TSTN && cln.LoaiTSTN == "VatKienTruc"
                                       select cln);
            var biendongvangbacdaquy = (from cln in db.NV_KeKhai_BienDongTaiSan
                                        where cln.Ma_KeKhai_TSTN == bankekhai.Ma_KeKhai_TSTN && cln.LoaiTSTN == "VangBacDaQuy"
                                        select cln);
            var biendongtien = (from cln in db.NV_KeKhai_BienDongTaiSan
                                where cln.Ma_KeKhai_TSTN == bankekhai.Ma_KeKhai_TSTN && cln.LoaiTSTN == "Tien"
                                select cln);
            var biendongcophieu = (from cln in db.NV_KeKhai_BienDongTaiSan
                                   where cln.Ma_KeKhai_TSTN == bankekhai.Ma_KeKhai_TSTN && cln.LoaiTSTN == "CoPhieu"
                                   select cln);
            var biendongtraiphieu = (from cln in db.NV_KeKhai_BienDongTaiSan
                                     where cln.Ma_KeKhai_TSTN == bankekhai.Ma_KeKhai_TSTN && cln.LoaiTSTN == "TraiPhieu"
                                     select cln);

            var biendongvongop = (from cln in db.NV_KeKhai_BienDongTaiSan
                                  where cln.Ma_KeKhai_TSTN == bankekhai.Ma_KeKhai_TSTN && cln.LoaiTSTN == "VonGop"
                                  select cln);
            var biendonggiaytokhac = (from cln in db.NV_KeKhai_BienDongTaiSan
                                      where cln.Ma_KeKhai_TSTN == bankekhai.Ma_KeKhai_TSTN && cln.LoaiTSTN == "GiayToKhac"
                                      select cln);
            var biendongtaisanqdpl = (from cln in db.NV_KeKhai_BienDongTaiSan
                                      where cln.Ma_KeKhai_TSTN == bankekhai.Ma_KeKhai_TSTN && cln.LoaiTSTN == "TaiSanQDPL"
                                      select cln);
            var biendongtaisankhac = (from cln in db.NV_KeKhai_BienDongTaiSan
                                      where cln.Ma_KeKhai_TSTN == bankekhai.Ma_KeKhai_TSTN && cln.LoaiTSTN == "TaiSanKhac"
                                      select cln);

            var biendongtaisannuocngoai = (from cln in db.NV_KeKhai_BienDongTaiSan
                                           where cln.Ma_KeKhai_TSTN == bankekhai.Ma_KeKhai_TSTN && cln.LoaiTSTN == "TaiSanNuocNgoai"
                                           select cln);
            var biendongtaikhoannuocngoai = (from cln in db.NV_KeKhai_BienDongTaiSan
                                           where cln.Ma_KeKhai_TSTN == bankekhai.Ma_KeKhai_TSTN && cln.LoaiTSTN == "TaiKhoanNuocNgoai"
                                           select cln);
            var biendongthunhap = (from cln in db.NV_KeKhai_BienDongTaiSan
                                   where cln.Ma_KeKhai_TSTN == bankekhai.Ma_KeKhai_TSTN && cln.LoaiTSTN == "ThuNhap"
                                   select cln);
            var data3 = new
            {
                dato = biendongdato,
                datkhac = biendongdatkhac,
                nhaocongtrinh = biendongnhaocongtrinh,
                congtrinhkhac = biendongcongtrinhkhac,
                caylaunam = biendongcaylaunam,
                rung = biendongrung,
                vatkientruc = biendongvatkientruc,
                vangbacdaquy = biendongvangbacdaquy,
                tien = biendongtien,
                cophieu = biendongcophieu,
                traiphieu = biendongtraiphieu,
                vongop = biendongvongop,
                giaytokhac = biendonggiaytokhac,
                taisanqdpl = biendongtaisanqdpl,
                taisankhac = biendongtaisankhac,
                taisannuocngoai = biendongtaisannuocngoai,
                taikhoannuocngoai = biendongtaikhoannuocngoai,
                thunhap = biendongthunhap
            };

            var data2 = new
            {
                bankekhai = bankekhai,
                nguoikekhai = data,
                thannhan = data1,
                dato = dato,
                datkhac = datkhac,
                nhao = nhao,
                congtrinhxaydung = congtrinhxaydung,
                cophieu = cophieu,
                traiphieu = traiphieu,
                vongop = vongop,
                giayto = giayto,
                taisankhac = taisankhac,
                giaydangky = giaydangky,
                taisannuocngoai = taisannuocngoai,
                taikhoannuocngoai = taikhoannuocngoai,
                tongthunhap = tongthunhap,
                caylaunam = caylaunam,
                rungsanxuat = rungsanxuat,
                vatkientruc = vatkientruc,
                kimloaidaquy = kimloaidaquy,
                tien = tien,
                tsnndato = tsnndato,
                tsnndatkhac = tsnndatkhac,
                tsnnnhao = tsnnnhao,
                tsnncongtrinhxaydung = tsnncongtrinhxaydung,
                tsnncophieu = tsnncophieu,
                tsnntraiphieu = tsnntraiphieu,
                tsnnvongop = tsnnvongop,
                tsnngiayto = tsnngiayto,
                tsnntaisankhac = tsnntaisankhac,
                tsnngiaydangky = tsnngiaydangky,
                tsnncaylaunam = tsnncaylaunam,
                tsnnrungsanxuat = tsnnrungsanxuat,
                tsnnvatkientruc = tsnnvatkientruc,
                tsnnkimloaidaquy = tsnnkimloaidaquy,
                tsnntien = tsnntien,
                biendongtaisan = data3
            };
            return Json(data2, JsonRequestBehavior.AllowGet);
        }

        //[HttpPost]
        //public async Task<JsonResult> Create([Bind(Include = "Ma_KeKhai_TSTN, Ma_CanBo,Ngay_KeKhai,Thang_KeKhai,Nam_KeKhai,NgayThangNam,Ma_Loai_KeKhai, Ma_LinhVuc_KeKhai")] Nv_KeKhai_TSTN nv_KeKhai_TSTN,
        //                         List<string> DiaChiDat, List<string> TenLoaiDat, List<string> LoaiDat, List<string> DienTichDat, List<string> GiaTriDat, List<string> ThongTinDatKhacNeuCo, List<string> GiayChungNhanQuyenSoHuuDat
        //                         , List<string> DiaChiNhaO, List<string> LoaiNhaO, List<string> DienTichNhaO, List<string> GiaTriNhaO, List<string> GiayChungNhanNhaO, List<string> ThongTinNhaOKhacNeuCo
        //                         , List<string> TenCongTrinh, List<string> DiaChiCongTrinh, List<string> LoaiCongTrinh, List<string> CapCongTrinh, List<string> DienTichCongTrinh, List<string> GiaTriCongTrinh, List<string> GiayChungNhanCongTrinh, List<string> ThongTinCongTrinhKhacNeuCo
        //                         , List<string> LoaiTaiSan, List<string> TenTaiSan, List<string> SoLuong_DienTich, List<string> GiaTriTS
        //                         , List<string> TenTrangSuc, List<string> GiaTriTrangSuc
        //                         , List<string> TenLoaiTien, List<string> GiaTriLoaiTien
        //                         , List<string> TenPhieu, List<string> LoaiPhieu, List<string> SoLuongPhieu, List<string> GiaTriPhieu
        //                         , List<string> TenTaiSanKhac, List<string> LoaiTaiSanKhac, List<string> SoDangKyTaiSanKhac, List<string> NamBatDauSoHuuTaiSanKhac, List<string> GiaTriTaiSanKhac
        //                         , List<string> GiaTriTaiSanNuocNgoai
        //                         , List<string> TenTaiKhoanNuocNgoai, List<string> SoTaiKhoanNuocNgoai, List<string> TenNganHangNguocNgoai
        //                         , List<string> ThuNhapNguoiKeKhai, List<string> ThuNhapVoHoacChong, List<string> ThuNhapCon, List<string> ThuNhapChung
        //                        , List<string> TenBienDongTS, List<string> LoaiBienDongTS, List<string> SoLuongBienDongTS, List<string> GiaTriBienDongTS, List<string> NoiDungBienDongTS,


        //                         List<string> tsnnDiaChiDat, List<string> tsnnTenLoaiDat, List<string> tsnnLoaiDat, List<string> tsnnDienTichDat, List<string> tsnnGiaTriDat, List<string> tsnnThongTinDatKhacNeuCo, List<string> tsnnGiayChungNhanQuyenSoHuuDat
        //                         , List<string> tsnnDiaChiNhaO, List<string> tsnnLoaiNhaO, List<string> tsnnDienTichNhaO, List<string> tsnnGiaTriNhaO, List<string> tsnnGiayChungNhanNhaO, List<string> tsnnThongTinNhaOKhacNeuCo
        //                         , List<string> tsnnTenCongTrinh, List<string> tsnnDiaChiCongTrinh, List<string> tsnnLoaiCongTrinh, List<string> tsnnCapCongTrinh, List<string> tsnnDienTichCongTrinh, List<string> tsnnGiaTriCongTrinh, List<string> tsnnGiayChungNhanCongTrinh, List<string> tsnnThongTinCongTrinhKhacNeuCo
        //                         , List<string> tsnnLoaiTaiSan, List<string> tsnnTenTaiSan, List<string> tsnnSoLuong_DienTich, List<string> tsnnGiaTriTS
        //                         , List<string> tsnnTenTrangSuc, List<string> tsnnGiaTriTrangSuc
        //                         , List<string> tsnnTenLoaiTien, List<string> tsnnGiaTriLoaiTien
        //                         , List<string> tsnnTenPhieu, List<string> tsnnLoaiPhieu, List<string> tsnnSoLuongPhieu, List<string> tsnnGiaTriPhieu
        //                         , List<string> tsnnTenTaiSanKhac, List<string> tsnnLoaiTaiSanKhac, List<string> tsnnSoDangKyTaiSanKhac, List<string> tsnnNamBatDauSoHuuTaiSanKhac, List<string> tsnnGiaTriTaiSanKhac,
        //                         int? id
        //    )

        //{
        //    var coquan = user.GetUserCoQuan();
        //    var canbo = (from cb in db.DM_CanBo
        //                 join cq in db.DM_CoQuanDonVi on cb.Ma_CoQuan_DonVi equals cq.Ma_CoQuan_DonVi
        //                 where cq.Ma_CoQuan_DonVi == coquan
        //                 select cb.Ma_CanBo).ToList();
        //    foreach (var ma in canbo)
        //    {
        //        nv_KeKhai_TSTN.Ma_CanBo = ma;
        //        nv_KeKhai_TSTN.MaKeHoachKeKhai = id;
        //        nv_KeKhai_TSTN.Ngay_KeKhai = (short)DateTime.Now.Day;
        //        nv_KeKhai_TSTN.Thang_KeKhai = (short)DateTime.Now.Month;
        //        nv_KeKhai_TSTN.Nam_KeKhai = (short)DateTime.Now.Year;
        //        nv_KeKhai_TSTN.NgayThangNam = DateTime.Now;
        //        nv_KeKhai_TSTN.TrangThai = false;


        //        db.Nv_KeKhai_TSTN.Add(nv_KeKhai_TSTN);
        //        db.SaveChanges();

        //        for (int i = 0; i < DiaChiDat.Count(); i++)
        //        {
        //            if (DiaChiDat[i] != "")
        //            {
        //                var nV_KeKhai_QuyenSuDungDat = new NV_KeKhai_QuyenSuDungDat();
        //                nV_KeKhai_QuyenSuDungDat.Ma_KeKhai_TSTN = nv_KeKhai_TSTN.Ma_KeKhai_TSTN;
        //                nV_KeKhai_QuyenSuDungDat.DiaChi = DiaChiDat[i];
        //                nV_KeKhai_QuyenSuDungDat.TenLoaiDat = TenLoaiDat[i];
        //                nV_KeKhai_QuyenSuDungDat.DienTich = DienTichDat[i];
        //                nV_KeKhai_QuyenSuDungDat.GiaTri = GiaTriDat[i];
        //                nV_KeKhai_QuyenSuDungDat.GiayChungNhan = GiayChungNhanQuyenSoHuuDat[i];
        //                nV_KeKhai_QuyenSuDungDat.LoaiDat = LoaiDat[i];
        //                nV_KeKhai_QuyenSuDungDat.ThongTinKhac = ThongTinDatKhacNeuCo[i];
        //                db.NV_KeKhai_QuyenSuDungDat.Add(nV_KeKhai_QuyenSuDungDat);
        //                db.SaveChanges();
        //            }
        //        }
        //        for (int i = 0; i < DiaChiNhaO.Count(); i++)
        //        {
        //            if (DiaChiNhaO[i] != "")
        //            {
        //                var nV_KeKhai_NhaO = new NV_KeKhai_NhaO();
        //                nV_KeKhai_NhaO.Ma_KeKhai_TSTN = nv_KeKhai_TSTN.Ma_KeKhai_TSTN;
        //                nV_KeKhai_NhaO.DiaChi = DiaChiNhaO[i];
        //                nV_KeKhai_NhaO.LoaiNha = LoaiNhaO[i];
        //                nV_KeKhai_NhaO.DienTichSuDung = DienTichNhaO[i];
        //                nV_KeKhai_NhaO.GiaTri = GiaTriNhaO[i];
        //                nV_KeKhai_NhaO.GiayChungNhan = GiayChungNhanNhaO[i];
        //                nV_KeKhai_NhaO.ThongTinKhac = ThongTinNhaOKhacNeuCo[i];
        //                db.NV_KeKhai_NhaO.Add(nV_KeKhai_NhaO);
        //                db.SaveChanges();
        //            }
        //        }
        //        for (int i = 0; i < DiaChiCongTrinh.Count(); i++)
        //        {
        //            if (DiaChiCongTrinh[i] != "")
        //            {
        //                var nV_KeKhai_CongTrinh = new NV_KeKhai_CongTrinh();
        //                nV_KeKhai_CongTrinh.Ma_KeKhai_TSTN = nv_KeKhai_TSTN.Ma_KeKhai_TSTN;
        //                nV_KeKhai_CongTrinh.TenCongTrinh = TenCongTrinh[i];
        //                nV_KeKhai_CongTrinh.DiaChi = DiaChiCongTrinh[i];
        //                nV_KeKhai_CongTrinh.LoaiCongTrinh = LoaiCongTrinh[i];
        //                nV_KeKhai_CongTrinh.CapCongTrinh = CapCongTrinh[i];
        //                nV_KeKhai_CongTrinh.DienTich = DienTichCongTrinh[i];
        //                nV_KeKhai_CongTrinh.GiaTri = GiaTriCongTrinh[i];
        //                nV_KeKhai_CongTrinh.GiayChungNhan = GiayChungNhanCongTrinh[i];
        //                nV_KeKhai_CongTrinh.ThongTinKhac = ThongTinCongTrinhKhacNeuCo[i];
        //                db.NV_KeKhai_CongTrinh.Add(nV_KeKhai_CongTrinh);
        //                db.SaveChanges();
        //            }
        //        }
        //        for (int i = 0; i < TenTaiSan.Count(); i++)
        //        {
        //            if (TenTaiSan[i] != "")
        //            {
        //                var nV_KeKhai_TaiSanGanVoiDat1 = new NV_KeKhai_TaiSanGanVoiDat();
        //                nV_KeKhai_TaiSanGanVoiDat1.Ma_KeKhai_TSTN = nv_KeKhai_TSTN.Ma_KeKhai_TSTN;
        //                nV_KeKhai_TaiSanGanVoiDat1.TenTaiSan = TenTaiSan[i];
        //                nV_KeKhai_TaiSanGanVoiDat1.SoLuong_DienTich = SoLuong_DienTich[i];
        //                nV_KeKhai_TaiSanGanVoiDat1.GiaTri = GiaTriTS[i];
        //                nV_KeKhai_TaiSanGanVoiDat1.LoaiTaiSan = LoaiTaiSan[i];
        //                db.NV_KeKhai_TaiSanGanVoiDat.Add(nV_KeKhai_TaiSanGanVoiDat1);
        //                db.SaveChanges();
        //            }
        //        }

        //        for (int i = 0; i < TenTrangSuc.Count(); i++)
        //        {
        //            if (TenTrangSuc[i] != "")
        //            {
        //                var nV_KeKhai_TaiSanTrangSuc = new NV_KeKhai_TaiSanTrangSuc();
        //                nV_KeKhai_TaiSanTrangSuc.Ma_KeKhai_TSTN = nv_KeKhai_TSTN.Ma_KeKhai_TSTN;
        //                nV_KeKhai_TaiSanTrangSuc.TenTrangSuc = TenTrangSuc[i];
        //                nV_KeKhai_TaiSanTrangSuc.GiaTri = GiaTriTrangSuc[i];
        //                db.NV_KeKhai_TaiSanTrangSuc.Add(nV_KeKhai_TaiSanTrangSuc);
        //                db.SaveChanges();
        //            }
        //        }
        //        for (int i = 0; i < TenLoaiTien.Count(); i++)
        //        {
        //            if (TenLoaiTien[i] != "")
        //            {
        //                var nV_KeKhai_Tien = new NV_KeKhai_Tien();
        //                nV_KeKhai_Tien.Ma_KeKhai_TSTN = nv_KeKhai_TSTN.Ma_KeKhai_TSTN;
        //                nV_KeKhai_Tien.TenLoaiTien = TenLoaiTien[i];
        //                nV_KeKhai_Tien.GiaTri = GiaTriLoaiTien[i];


        //                db.NV_KeKhai_Tien.Add(nV_KeKhai_Tien);
        //                db.SaveChanges();
        //            }
        //        }


        //        for (int i = 0; i < TenPhieu.Count(); i++)
        //        {
        //            if (TenPhieu[i] != "")
        //            {
        //                var nV_KeKhai_TaiSanPhieu = new NV_KeKhai_TaiSanPhieu();
        //                nV_KeKhai_TaiSanPhieu.Ma_KeKhai_TSTN = nv_KeKhai_TSTN.Ma_KeKhai_TSTN;
        //                nV_KeKhai_TaiSanPhieu.TenPhieu = TenPhieu[i];
        //                nV_KeKhai_TaiSanPhieu.LoaiPhieu = LoaiPhieu[i];
        //                nV_KeKhai_TaiSanPhieu.SoLuong = SoLuongPhieu[i];
        //                nV_KeKhai_TaiSanPhieu.GiaTri = GiaTriPhieu[i];

        //                db.NV_KeKhai_TaiSanPhieu.Add(nV_KeKhai_TaiSanPhieu);
        //                db.SaveChanges();
        //            }
        //        }
        //        for (int i = 0; i < TenTaiSanKhac.Count(); i++)
        //        {
        //            if (TenTaiSanKhac[i] != "")
        //            {
        //                var nV_KeKhai_TaiSanKhac = new NV_KeKhai_TaiSanKhac();
        //                nV_KeKhai_TaiSanKhac.Ma_KeKhai_TSTN = nv_KeKhai_TSTN.Ma_KeKhai_TSTN;
        //                nV_KeKhai_TaiSanKhac.TenTaiSan = TenTaiSanKhac[i];
        //                nV_KeKhai_TaiSanKhac.SoDangKy_NamBatDauSuDung = SoDangKyTaiSanKhac[i] != "" ? SoDangKyTaiSanKhac[i] : NamBatDauSoHuuTaiSanKhac[i];
        //                nV_KeKhai_TaiSanKhac.LoaiTaiSanKhac = LoaiTaiSanKhac[i];
        //                nV_KeKhai_TaiSanKhac.GiaTri = GiaTriTaiSanKhac[i];

        //                db.NV_KeKhai_TaiSanKhac.Add(nV_KeKhai_TaiSanKhac);
        //                db.SaveChanges();
        //            }
        //        }

        //        //kê khai tài sản nước ngoài
        //        for (int i = 0; i < tsnnDiaChiDat.Count(); i++)
        //        {
        //            if (tsnnDiaChiDat[i] != "")
        //            {
        //                var nV_KeKhai_TaiSanNuocNgoai_QuyenSuDungDat = new NV_KeKhai_TaiSanNuocNgoai_QuyenSuDungDat();
        //                nV_KeKhai_TaiSanNuocNgoai_QuyenSuDungDat.Ma_KeKhai_TSTN = nv_KeKhai_TSTN.Ma_KeKhai_TSTN;
        //                nV_KeKhai_TaiSanNuocNgoai_QuyenSuDungDat.DiaChi_TSNN = tsnnDiaChiDat[i];
        //                nV_KeKhai_TaiSanNuocNgoai_QuyenSuDungDat.TenLoaiDat_TSNN = tsnnTenLoaiDat[i];
        //                nV_KeKhai_TaiSanNuocNgoai_QuyenSuDungDat.DienTich_TSNN = tsnnDienTichDat[i];
        //                nV_KeKhai_TaiSanNuocNgoai_QuyenSuDungDat.GiaTri_TSNN = tsnnGiaTriDat[i];
        //                nV_KeKhai_TaiSanNuocNgoai_QuyenSuDungDat.GiayChungNhan_TSNN = tsnnGiayChungNhanQuyenSoHuuDat[i];
        //                nV_KeKhai_TaiSanNuocNgoai_QuyenSuDungDat.LoaiDat_TSNN = tsnnLoaiDat[i];
        //                nV_KeKhai_TaiSanNuocNgoai_QuyenSuDungDat.ThongTinKhac_TSNN = tsnnThongTinDatKhacNeuCo[i];
        //                db.NV_KeKhai_TaiSanNuocNgoai_QuyenSuDungDat.Add(nV_KeKhai_TaiSanNuocNgoai_QuyenSuDungDat);
        //                db.SaveChanges();
        //            }
        //        }
        //        for (int i = 0; i < tsnnDiaChiNhaO.Count(); i++)
        //        {
        //            if (tsnnDiaChiNhaO[i] != "")
        //            {
        //                var nV_KeKhai_TaiSanNuocNgoai_NhaO = new NV_KeKhai_TaiSanNuocNgoai_NhaO();
        //                nV_KeKhai_TaiSanNuocNgoai_NhaO.Ma_KeKhai_TSTN = nv_KeKhai_TSTN.Ma_KeKhai_TSTN;
        //                nV_KeKhai_TaiSanNuocNgoai_NhaO.DiaChi_TSNN = tsnnDiaChiNhaO[i];
        //                nV_KeKhai_TaiSanNuocNgoai_NhaO.LoaiNha_TSNN = tsnnLoaiNhaO[i];
        //                nV_KeKhai_TaiSanNuocNgoai_NhaO.DienTichSuDung_TSNN = tsnnDienTichNhaO[i];
        //                nV_KeKhai_TaiSanNuocNgoai_NhaO.GiaTri_TSNN = tsnnGiaTriNhaO[i];
        //                nV_KeKhai_TaiSanNuocNgoai_NhaO.GiayChungNhan_TSNN = tsnnGiayChungNhanNhaO[i];
        //                nV_KeKhai_TaiSanNuocNgoai_NhaO.ThongTinKhac_TSNN = tsnnThongTinNhaOKhacNeuCo[i];
        //                db.NV_KeKhai_TaiSanNuocNgoai_NhaO.Add(nV_KeKhai_TaiSanNuocNgoai_NhaO);
        //                db.SaveChanges();
        //            }
        //        }
        //        for (int i = 0; i < tsnnDiaChiCongTrinh.Count(); i++)
        //        {
        //            if (tsnnDiaChiCongTrinh[i] != "")
        //            {
        //                var nV_KeKhai_TaiSanNuocNgoai_CongTrinh = new NV_KeKhai_TaiSanNuocNgoai_CongTrinh();
        //                nV_KeKhai_TaiSanNuocNgoai_CongTrinh.Ma_KeKhai_TSTN = nv_KeKhai_TSTN.Ma_KeKhai_TSTN;
        //                nV_KeKhai_TaiSanNuocNgoai_CongTrinh.TenCongTrinh_TSNN = tsnnTenCongTrinh[i];
        //                nV_KeKhai_TaiSanNuocNgoai_CongTrinh.DiaChi_TSNN = tsnnDiaChiCongTrinh[i];
        //                nV_KeKhai_TaiSanNuocNgoai_CongTrinh.LoaiCongTrinh_TSNN = tsnnLoaiCongTrinh[i];
        //                nV_KeKhai_TaiSanNuocNgoai_CongTrinh.CapCongTrinh_TSNN = tsnnCapCongTrinh[i];
        //                nV_KeKhai_TaiSanNuocNgoai_CongTrinh.DienTich_TSNN = tsnnDienTichCongTrinh[i];
        //                nV_KeKhai_TaiSanNuocNgoai_CongTrinh.GiaTri_TSNN = tsnnGiaTriCongTrinh[i];
        //                nV_KeKhai_TaiSanNuocNgoai_CongTrinh.GiayChungNhan_TSNN = tsnnGiayChungNhanCongTrinh[i];
        //                nV_KeKhai_TaiSanNuocNgoai_CongTrinh.ThongTinKhac_TSNN = tsnnThongTinCongTrinhKhacNeuCo[i];
        //                db.NV_KeKhai_TaiSanNuocNgoai_CongTrinh.Add(nV_KeKhai_TaiSanNuocNgoai_CongTrinh);
        //                db.SaveChanges();
        //            }
        //        }
        //        for (int i = 0; i < tsnnTenTaiSan.Count(); i++)
        //        {
        //            if (tsnnTenTaiSan[i] != "")
        //            {
        //                var nV_KeKhai_TaiSanNuocNgoai_TaiSanGanVoiDat1 = new NV_KeKhai_TaiSanNuocNgoai_TaiSanGanVoiDat();
        //                nV_KeKhai_TaiSanNuocNgoai_TaiSanGanVoiDat1.Ma_KeKhai_TSTN = nv_KeKhai_TSTN.Ma_KeKhai_TSTN;
        //                nV_KeKhai_TaiSanNuocNgoai_TaiSanGanVoiDat1.TenTaiSan_TSNN = tsnnTenTaiSan[i];
        //                nV_KeKhai_TaiSanNuocNgoai_TaiSanGanVoiDat1.SoLuong_DienTich_TSNN = tsnnSoLuong_DienTich[i];
        //                nV_KeKhai_TaiSanNuocNgoai_TaiSanGanVoiDat1.GiaTri_TSNN = tsnnGiaTriTS[i];
        //                nV_KeKhai_TaiSanNuocNgoai_TaiSanGanVoiDat1.LoaiTaiSan_TSNN = tsnnLoaiTaiSan[i];
        //                db.NV_KeKhai_TaiSanNuocNgoai_TaiSanGanVoiDat.Add(nV_KeKhai_TaiSanNuocNgoai_TaiSanGanVoiDat1);
        //                db.SaveChanges();
        //            }

        //        }

        //        for (int i = 0; i < tsnnTenTrangSuc.Count(); i++)
        //        {
        //            if (tsnnTenTrangSuc[i] != "")
        //            {
        //                var nV_KeKhai_TaiSanNuocNgoai_TaiSanTrangSuc = new NV_KeKhai_TaiSanNuocNgoai_TaiSanTrangSuc();
        //                nV_KeKhai_TaiSanNuocNgoai_TaiSanTrangSuc.Ma_KeKhai_TSTN = nv_KeKhai_TSTN.Ma_KeKhai_TSTN;
        //                nV_KeKhai_TaiSanNuocNgoai_TaiSanTrangSuc.TenTrangSuc_TSNN = tsnnTenTrangSuc[i];
        //                nV_KeKhai_TaiSanNuocNgoai_TaiSanTrangSuc.GiaTri_TSNN = tsnnGiaTriTrangSuc[i];
        //                db.NV_KeKhai_TaiSanNuocNgoai_TaiSanTrangSuc.Add(nV_KeKhai_TaiSanNuocNgoai_TaiSanTrangSuc);
        //                db.SaveChanges();
        //            }
        //        }
        //        for (int i = 0; i < tsnnTenLoaiTien.Count(); i++)
        //        {
        //            if (tsnnTenLoaiTien[i] != "")
        //            {
        //                var nV_KeKhai_TaiSanNuocNgoai_Tien = new NV_KeKhai_TaiSanNuocNgoai_Tien();
        //                nV_KeKhai_TaiSanNuocNgoai_Tien.Ma_KeKhai_TSTN = nv_KeKhai_TSTN.Ma_KeKhai_TSTN;
        //                nV_KeKhai_TaiSanNuocNgoai_Tien.TenLoaiTien_TSNN = tsnnTenLoaiTien[i];
        //                nV_KeKhai_TaiSanNuocNgoai_Tien.GiaTri_TSNN = tsnnGiaTriLoaiTien[i];


        //                db.NV_KeKhai_TaiSanNuocNgoai_Tien.Add(nV_KeKhai_TaiSanNuocNgoai_Tien);
        //                db.SaveChanges();
        //            }
        //        }


        //        for (int i = 0; i < tsnnTenPhieu.Count(); i++)
        //        {
        //            if (tsnnTenPhieu[i] != "")
        //            {
        //                var nV_KeKhai_TaiSanNuocNgoai_TaiSanPhieu = new NV_KeKhai_TaiSanNuocNgoai_TaiSanPhieu();
        //                nV_KeKhai_TaiSanNuocNgoai_TaiSanPhieu.Ma_KeKhai_TSTN = nv_KeKhai_TSTN.Ma_KeKhai_TSTN;
        //                nV_KeKhai_TaiSanNuocNgoai_TaiSanPhieu.TenPhieu_TSNN = tsnnTenPhieu[i];
        //                nV_KeKhai_TaiSanNuocNgoai_TaiSanPhieu.LoaiPhieu_TSNN = tsnnLoaiPhieu[i];
        //                nV_KeKhai_TaiSanNuocNgoai_TaiSanPhieu.SoLuong_TSNN = tsnnSoLuongPhieu[i];
        //                nV_KeKhai_TaiSanNuocNgoai_TaiSanPhieu.GiaTri_TSNN = tsnnGiaTriPhieu[i];

        //                db.NV_KeKhai_TaiSanNuocNgoai_TaiSanPhieu.Add(nV_KeKhai_TaiSanNuocNgoai_TaiSanPhieu);
        //                db.SaveChanges();
        //            }
        //        }
        //        for (int i = 0; i < tsnnTenTaiSanKhac.Count(); i++)
        //        {
        //            if (tsnnTenTaiSanKhac[i] != "")
        //            {
        //                var nV_KeKhai_TaiSanNuocNgoai_TaiSanKhac = new NV_KeKhai_TaiSanNuocNgoai_TaiSanKhac();
        //                nV_KeKhai_TaiSanNuocNgoai_TaiSanKhac.Ma_KeKhai_TSTN = nv_KeKhai_TSTN.Ma_KeKhai_TSTN;
        //                nV_KeKhai_TaiSanNuocNgoai_TaiSanKhac.TenTaiSan_TSNN = TenTaiSanKhac[i];
        //                nV_KeKhai_TaiSanNuocNgoai_TaiSanKhac.SoDangKy_NamBatDauSuDung_TSNN = tsnnSoDangKyTaiSanKhac[i] != "" ? tsnnSoDangKyTaiSanKhac[i] : tsnnNamBatDauSoHuuTaiSanKhac[i];
        //                nV_KeKhai_TaiSanNuocNgoai_TaiSanKhac.LoaiTaiSanKhac_TSNN = tsnnLoaiTaiSanKhac[i];
        //                nV_KeKhai_TaiSanNuocNgoai_TaiSanKhac.GiaTri_TSNN = tsnnGiaTriTaiSanKhac[i];

        //                db.NV_KeKhai_TaiSanNuocNgoai_TaiSanKhac.Add(nV_KeKhai_TaiSanNuocNgoai_TaiSanKhac);
        //                db.SaveChanges();
        //            }
        //        }
        //        //end kê khai tài sản nước ngoài


        //        for (int i = 0; i < ThuNhapNguoiKeKhai.Count(); i++)
        //        {
        //            if (ThuNhapNguoiKeKhai[i] != "")
        //            {
        //                var nV_KeKhai_TongThuNhap = new NV_KeKhai_TongThuNhap();
        //                nV_KeKhai_TongThuNhap.Ma_KeKhai_TSTN = nv_KeKhai_TSTN.Ma_KeKhai_TSTN;
        //                nV_KeKhai_TongThuNhap.TongThuNhap_NguoiKeKhai = ThuNhapNguoiKeKhai[i];
        //                nV_KeKhai_TongThuNhap.TongThuNhap_VoHoacChong = ThuNhapVoHoacChong[i];
        //                nV_KeKhai_TongThuNhap.TongThuNhap_ConChuaThanhNien = ThuNhapCon[i];
        //                nV_KeKhai_TongThuNhap.TongThuNhap_CacKhoanChung = ThuNhapChung[i];
        //                db.NV_KeKhai_TongThuNhap.Add(nV_KeKhai_TongThuNhap);
        //                db.SaveChanges();
        //            }
        //        }
        //        for (int i = 0; i < TenTaiKhoanNuocNgoai.Count(); i++)
        //        {
        //            if (TenTaiKhoanNuocNgoai[i] != "")
        //            {
        //                var nV_KeKhai_TaiKhoanNuocNgoai = new NV_KeKhai_TaiKhoanNuocNgoai();
        //                nV_KeKhai_TaiKhoanNuocNgoai.Ma_KeKhai_TSTN = nv_KeKhai_TSTN.Ma_KeKhai_TSTN;
        //                nV_KeKhai_TaiKhoanNuocNgoai.TenChuTaiKhoan = TenTaiKhoanNuocNgoai[i];
        //                nV_KeKhai_TaiKhoanNuocNgoai.SoTaiKhoan = SoTaiKhoanNuocNgoai[i];
        //                nV_KeKhai_TaiKhoanNuocNgoai.TenNganHang = TenNganHangNguocNgoai[i];
        //                db.NV_KeKhai_TaiKhoanNuocNgoai.Add(nV_KeKhai_TaiKhoanNuocNgoai);
        //                db.SaveChanges();
        //            }
        //        }
        //        for (int i = 0; i < TenBienDongTS.Count(); i++)
        //        {
        //            if (TenBienDongTS[i] != "")
        //            {
        //                var nV_KeKhai_BienDongTaiSan = new NV_KeKhai_BienDongTaiSan();
        //                nV_KeKhai_BienDongTaiSan.Ma_KeKhai_TSTN = nv_KeKhai_TSTN.Ma_KeKhai_TSTN;
        //                nV_KeKhai_BienDongTaiSan.TenLoaiTSTNJson = TenBienDongTS[i];
        //                nV_KeKhai_BienDongTaiSan.LoaiTSTN = LoaiBienDongTS[i];
        //                nV_KeKhai_BienDongTaiSan.SoLuongTaiSanJson = SoLuongBienDongTS[i];
        //                nV_KeKhai_BienDongTaiSan.GiaTriTSTNJson = GiaTriBienDongTS[i];
        //                nV_KeKhai_BienDongTaiSan.NoiDungGiaiTrinhJson = NoiDungBienDongTS[i];
        //                db.NV_KeKhai_BienDongTaiSan.Add(nV_KeKhai_BienDongTaiSan);
        //                db.SaveChanges();
        //            }
        //        }
        //    }


        //    return Json(Url.Action("Index", "NV_KeKhai_TSTN"), JsonRequestBehavior.AllowGet);
        //}


        [HttpPost]
        public async Task<JsonResult> Create([Bind(Include = "Ma_KeKhai_TSTN, Ma_CanBo,Ngay_KeKhai,Thang_KeKhai,Nam_KeKhai,NgayThangNam,Ma_Loai_KeKhai, Ma_LinhVuc_KeKhai")] Nv_KeKhai_TSTN nv_KeKhai_TSTN,
                                int Ma_CanBo, string HoTen, DateTime DoB, string DiaChiThuongTru, string SoCCCD, DateTime NgayCap, string NoiCap, int Ma_CoQuan_DonVi, int Ma_ChucVu_ChucDanh,
                                List<string> HoTenThanNhan, List<DateTime> DoBTN, List<string> NgheNghiep, List<string> NoiLamViec, List<string> DiaChiThuongTruTN, List<string> SoCCCDTN, List<string> NgayCapTN, List<string> NoiCapTN, List<string> VaiTroThanNhan,
                                string BienDongTaiSan,
                                List<string> DiaChiDat, List<string> TenLoaiDat, List<string> LoaiDat, List<string> DienTichDat, List<string> GiaTriDat, List<string> ThongTinDatKhacNeuCo, List<string> GiayChungNhanQuyenSoHuuDat
                                , List<string> DiaChiNhaO, List<string> LoaiNhaO, List<string> DienTichNhaO, List<string> GiaTriNhaO, List<string> GiayChungNhanNhaO, List<string> ThongTinNhaOKhacNeuCo
                                , List<string> TenCongTrinh, List<string> DiaChiCongTrinh, List<string> LoaiCongTrinh, List<string> CapCongTrinh, List<string> DienTichCongTrinh, List<string> GiaTriCongTrinh, List<string> GiayChungNhanCongTrinh, List<string> ThongTinCongTrinhKhacNeuCo
                                , List<string> LoaiTaiSan, List<string> TenTaiSan, List<string> SoLuong_DienTich, List<string> GiaTriTS
                                , List<string> TenTrangSuc, List<string> GiaTriTrangSuc
                                , List<string> TenLoaiTien, List<string> GiaTriLoaiTien
                                , List<string> TenPhieu, List<string> LoaiPhieu, List<string> SoLuongPhieu, List<string> GiaTriPhieu
                                , List<string> TenTaiSanKhac, List<string> LoaiTaiSanKhac, List<string> SoDangKyTaiSanKhac, List<string> NamBatDauSoHuuTaiSanKhac, List<string> GiaTriTaiSanKhac
                                , List<string> GiaTriTaiSanNuocNgoai
                                , List<string> TenTaiKhoanNuocNgoai, List<string> SoTaiKhoanNuocNgoai, List<string> TenNganHangNguocNgoai
                                , List<string> ThuNhapNguoiKeKhai, List<string> ThuNhapVoHoacChong, List<string> ThuNhapCon, List<string> ThuNhapChung
                               , List<string> TenBienDongTS, List<string> LoaiBienDongTS, List<string> SoLuongBienDongTS, List<string> GiaTriBienDongTS, List<string> NoiDungBienDongTS,


                                List<string> tsnnDiaChiDat, List<string> tsnnTenLoaiDat, List<string> tsnnLoaiDat, List<string> tsnnDienTichDat, List<string> tsnnGiaTriDat, List<string> tsnnThongTinDatKhacNeuCo, List<string> tsnnGiayChungNhanQuyenSoHuuDat
                                , List<string> tsnnDiaChiNhaO, List<string> tsnnLoaiNhaO, List<string> tsnnDienTichNhaO, List<string> tsnnGiaTriNhaO, List<string> tsnnGiayChungNhanNhaO, List<string> tsnnThongTinNhaOKhacNeuCo
                                , List<string> tsnnTenCongTrinh, List<string> tsnnDiaChiCongTrinh, List<string> tsnnLoaiCongTrinh, List<string> tsnnCapCongTrinh, List<string> tsnnDienTichCongTrinh, List<string> tsnnGiaTriCongTrinh, List<string> tsnnGiayChungNhanCongTrinh, List<string> tsnnThongTinCongTrinhKhacNeuCo
                                , List<string> tsnnLoaiTaiSan, List<string> tsnnTenTaiSan, List<string> tsnnSoLuong_DienTich, List<string> tsnnGiaTriTS
                                , List<string> tsnnTenTrangSuc, List<string> tsnnGiaTriTrangSuc
                                , List<string> tsnnTenLoaiTien, List<string> tsnnGiaTriLoaiTien
                                , List<string> tsnnTenPhieu, List<string> tsnnLoaiPhieu, List<string> tsnnSoLuongPhieu, List<string> tsnnGiaTriPhieu
                                , List<string> tsnnTenTaiSanKhac, List<string> tsnnLoaiTaiSanKhac, List<string> tsnnSoDangKyTaiSanKhac, List<string> tsnnNamBatDauSoHuuTaiSanKhac, List<string> tsnnGiaTriTaiSanKhac,
                                int? id
           )

        {

            var canbo = db.DM_CanBo.Find(Ma_CanBo);

            canbo.HoTen = HoTen;
            canbo.DoB = Convert.ToDateTime(DoB).ToString("dd/MM/yyyy"); 
            canbo.DiaChiThuongTru = DiaChiThuongTru;
            canbo.SoCCCD = SoCCCD;
            canbo.NgayCap = Convert.ToDateTime(NgayCap).ToString("dd/MM/yyyy");
            canbo.NoiCap = NoiCap;
            canbo.Ma_CoQuan_DonVi = Ma_CoQuan_DonVi;
            canbo.Ma_ChucVu_ChucDanh = Ma_ChucVu_ChucDanh;
            canbo.Ten = HoTen.Substring(HoTen.LastIndexOf(" ") + 1);
            
            

            db.Entry(canbo).State = EntityState.Modified;
            db.SaveChanges();

            var cbth = db.DM_CanBo_ThanNhan.Where(_ => _.Ma_CanBo == Ma_CanBo);
            foreach (var i in cbth)
            {
                db.DM_CanBo_ThanNhan.Remove(i);

            }
            db.SaveChanges();

            for (int i = 0; i < HoTenThanNhan.Count(); i++)
            {
                if (HoTenThanNhan[i] != "")
                {
                    var tn = new DM_CanBo_ThanNhan();
                    tn.Ma_CanBo = Ma_CanBo;
                    tn.HoTen = HoTenThanNhan[i];
                    tn.DoB = Convert.ToDateTime(DoBTN[i]).ToString("dd/MM/yyyy");
                    tn.DiaChiThuongTru = DiaChiThuongTruTN[i];

                    tn.SoCCCD = SoCCCDTN[i];
                    tn.NgayCap = NgayCapTN[i] == "" ? "01/01/0001" : Convert.ToDateTime(NgayCapTN[i]).ToString("dd/MM/yyyy");

                    //if (tn.NgayCap == "01/01/0001") tn.NgayCap = "";

                    tn.NoiCap = NoiCapTN[i];
                    tn.VaiTroThanNhan = VaiTroThanNhan[i];
                    tn.NgheNghiep = NgheNghiep[i];
                    tn.NoiLamViec = NoiLamViec[i];
                    db.DM_CanBo_ThanNhan.Add(tn);
                    db.SaveChanges();
                }
            }
            
            if (id == null)
            {
                nv_KeKhai_TSTN.MaKeHoachKeKhai = 0;
            }
            else
            {
                nv_KeKhai_TSTN.MaKeHoachKeKhai = id;
            }
            nv_KeKhai_TSTN.Ngay_KeKhai = (short)DateTime.Now.Day;
            nv_KeKhai_TSTN.Thang_KeKhai = (short)DateTime.Now.Month;
            nv_KeKhai_TSTN.Nam_KeKhai = (short)DateTime.Now.Year;
            nv_KeKhai_TSTN.NgayThangNam = DateTime.Now;
            nv_KeKhai_TSTN.TrangThai = false;
            nv_KeKhai_TSTN.TrangThaiTiepNhan = 0;
            nv_KeKhai_TSTN.BienDongTaiSan = BienDongTaiSan;
            nv_KeKhai_TSTN.Ma_LinhVuc_KeKhai = 1;
            nv_KeKhai_TSTN.HoTen = canbo.HoTen;
            nv_KeKhai_TSTN.DoB = DoB;
            nv_KeKhai_TSTN.DiaChiThuongTru = canbo.DiaChiThuongTru;
            nv_KeKhai_TSTN.SoCCCD = canbo.SoCCCD;
            nv_KeKhai_TSTN.NgayCap = NgayCap;
            nv_KeKhai_TSTN.NoiCap = canbo.NoiCap;
            nv_KeKhai_TSTN.Ma_CoQuan_DonVi = canbo.Ma_CoQuan_DonVi;
            nv_KeKhai_TSTN.Ma_ChucVu_ChucDanh = canbo.Ma_ChucVu_ChucDanh;
            


            db.Nv_KeKhai_TSTN.Add(nv_KeKhai_TSTN);
            db.SaveChanges();

            for (int i = 0; i < DiaChiDat.Count(); i++)
            {
                if (DiaChiDat[i] != "")
                {
                    var nV_KeKhai_QuyenSuDungDat = new NV_KeKhai_QuyenSuDungDat();
                    nV_KeKhai_QuyenSuDungDat.Ma_KeKhai_TSTN = nv_KeKhai_TSTN.Ma_KeKhai_TSTN;
                    nV_KeKhai_QuyenSuDungDat.DiaChi = DiaChiDat[i];
                    nV_KeKhai_QuyenSuDungDat.TenLoaiDat = TenLoaiDat[i];
                    nV_KeKhai_QuyenSuDungDat.DienTich = DienTichDat[i];
                    nV_KeKhai_QuyenSuDungDat.GiaTri = GiaTriDat[i];
                    nV_KeKhai_QuyenSuDungDat.GiayChungNhan = GiayChungNhanQuyenSoHuuDat[i];
                    nV_KeKhai_QuyenSuDungDat.LoaiDat = LoaiDat[i];
                    nV_KeKhai_QuyenSuDungDat.ThongTinKhac = ThongTinDatKhacNeuCo[i];
                    db.NV_KeKhai_QuyenSuDungDat.Add(nV_KeKhai_QuyenSuDungDat);
                    db.SaveChanges();
                }
            }
            for (int i = 0; i < DiaChiNhaO.Count(); i++)
            {
                if (DiaChiNhaO[i] != "")
                {
                    var nV_KeKhai_NhaO = new NV_KeKhai_NhaO();
                    nV_KeKhai_NhaO.Ma_KeKhai_TSTN = nv_KeKhai_TSTN.Ma_KeKhai_TSTN;
                    nV_KeKhai_NhaO.DiaChi = DiaChiNhaO[i];
                    nV_KeKhai_NhaO.LoaiNha = LoaiNhaO[i];
                    nV_KeKhai_NhaO.DienTichSuDung = DienTichNhaO[i];
                    nV_KeKhai_NhaO.GiaTri = GiaTriNhaO[i];
                    nV_KeKhai_NhaO.GiayChungNhan = GiayChungNhanNhaO[i];
                    nV_KeKhai_NhaO.ThongTinKhac = ThongTinNhaOKhacNeuCo[i];
                    db.NV_KeKhai_NhaO.Add(nV_KeKhai_NhaO);
                    db.SaveChanges();
                }
            }
            for (int i = 0; i < DiaChiCongTrinh.Count(); i++)
            {
                if (DiaChiCongTrinh[i] != "")
                {
                    var nV_KeKhai_CongTrinh = new NV_KeKhai_CongTrinh();
                    nV_KeKhai_CongTrinh.Ma_KeKhai_TSTN = nv_KeKhai_TSTN.Ma_KeKhai_TSTN;
                    nV_KeKhai_CongTrinh.TenCongTrinh = TenCongTrinh[i];
                    nV_KeKhai_CongTrinh.DiaChi = DiaChiCongTrinh[i];
                    nV_KeKhai_CongTrinh.LoaiCongTrinh = LoaiCongTrinh[i];
                    nV_KeKhai_CongTrinh.CapCongTrinh = CapCongTrinh[i];
                    nV_KeKhai_CongTrinh.DienTich = DienTichCongTrinh[i];
                    nV_KeKhai_CongTrinh.GiaTri = GiaTriCongTrinh[i];
                    nV_KeKhai_CongTrinh.GiayChungNhan = GiayChungNhanCongTrinh[i];
                    nV_KeKhai_CongTrinh.ThongTinKhac = ThongTinCongTrinhKhacNeuCo[i];
                    db.NV_KeKhai_CongTrinh.Add(nV_KeKhai_CongTrinh);
                    db.SaveChanges();
                }
            }
            for (int i = 0; i < TenTaiSan.Count(); i++)
            {
                if (TenTaiSan[i] != "")
                {
                    var nV_KeKhai_TaiSanGanVoiDat1 = new NV_KeKhai_TaiSanGanVoiDat();
                    nV_KeKhai_TaiSanGanVoiDat1.Ma_KeKhai_TSTN = nv_KeKhai_TSTN.Ma_KeKhai_TSTN;
                    nV_KeKhai_TaiSanGanVoiDat1.TenTaiSan = TenTaiSan[i];
                    nV_KeKhai_TaiSanGanVoiDat1.SoLuong_DienTich = SoLuong_DienTich[i];
                    nV_KeKhai_TaiSanGanVoiDat1.GiaTri = GiaTriTS[i];
                    nV_KeKhai_TaiSanGanVoiDat1.LoaiTaiSan = LoaiTaiSan[i];
                    db.NV_KeKhai_TaiSanGanVoiDat.Add(nV_KeKhai_TaiSanGanVoiDat1);
                    db.SaveChanges();
                }
            }

            for (int i = 0; i < TenTrangSuc.Count(); i++)
            {
                if (TenTrangSuc[i] != "")
                {
                    var nV_KeKhai_TaiSanTrangSuc = new NV_KeKhai_TaiSanTrangSuc();
                    nV_KeKhai_TaiSanTrangSuc.Ma_KeKhai_TSTN = nv_KeKhai_TSTN.Ma_KeKhai_TSTN;
                    nV_KeKhai_TaiSanTrangSuc.TenTrangSuc = TenTrangSuc[i];
                    nV_KeKhai_TaiSanTrangSuc.GiaTri = GiaTriTrangSuc[i];
                    db.NV_KeKhai_TaiSanTrangSuc.Add(nV_KeKhai_TaiSanTrangSuc);
                    db.SaveChanges();
                }
            }
            for (int i = 0; i < TenLoaiTien.Count(); i++)
            {
                if (TenLoaiTien[i] != "")
                {
                    var nV_KeKhai_Tien = new NV_KeKhai_Tien();
                    nV_KeKhai_Tien.Ma_KeKhai_TSTN = nv_KeKhai_TSTN.Ma_KeKhai_TSTN;
                    nV_KeKhai_Tien.TenLoaiTien = TenLoaiTien[i];
                    nV_KeKhai_Tien.GiaTri = GiaTriLoaiTien[i];


                    db.NV_KeKhai_Tien.Add(nV_KeKhai_Tien);
                    db.SaveChanges();
                }
            }


            for (int i = 0; i < TenPhieu.Count(); i++)
            {
                if (TenPhieu[i] != "")
                {
                    var nV_KeKhai_TaiSanPhieu = new NV_KeKhai_TaiSanPhieu();
                    nV_KeKhai_TaiSanPhieu.Ma_KeKhai_TSTN = nv_KeKhai_TSTN.Ma_KeKhai_TSTN;
                    nV_KeKhai_TaiSanPhieu.TenPhieu = TenPhieu[i];
                    nV_KeKhai_TaiSanPhieu.LoaiPhieu = LoaiPhieu[i];
                    nV_KeKhai_TaiSanPhieu.SoLuong = SoLuongPhieu[i];
                    nV_KeKhai_TaiSanPhieu.GiaTri = GiaTriPhieu[i];

                    db.NV_KeKhai_TaiSanPhieu.Add(nV_KeKhai_TaiSanPhieu);
                    db.SaveChanges();
                }
            }
            for (int i = 0; i < TenTaiSanKhac.Count(); i++)
            {
                if (TenTaiSanKhac[i] != "")
                {
                    var nV_KeKhai_TaiSanKhac = new NV_KeKhai_TaiSanKhac();
                    nV_KeKhai_TaiSanKhac.Ma_KeKhai_TSTN = nv_KeKhai_TSTN.Ma_KeKhai_TSTN;
                    nV_KeKhai_TaiSanKhac.TenTaiSan = TenTaiSanKhac[i];
                    nV_KeKhai_TaiSanKhac.SoDangKy_NamBatDauSuDung = SoDangKyTaiSanKhac[i] != "" ? SoDangKyTaiSanKhac[i] : NamBatDauSoHuuTaiSanKhac[i];
                    nV_KeKhai_TaiSanKhac.LoaiTaiSanKhac = LoaiTaiSanKhac[i];
                    nV_KeKhai_TaiSanKhac.GiaTri = GiaTriTaiSanKhac[i];

                    db.NV_KeKhai_TaiSanKhac.Add(nV_KeKhai_TaiSanKhac);
                    db.SaveChanges();
                }
            }

            //kê khai tài sản nước ngoài
            for (int i = 0; i < tsnnDiaChiDat.Count(); i++)
            {
                if (tsnnDiaChiDat[i] != "")
                {
                    var nV_KeKhai_TaiSanNuocNgoai_QuyenSuDungDat = new NV_KeKhai_TaiSanNuocNgoai_QuyenSuDungDat();
                    nV_KeKhai_TaiSanNuocNgoai_QuyenSuDungDat.Ma_KeKhai_TSTN = nv_KeKhai_TSTN.Ma_KeKhai_TSTN;
                    nV_KeKhai_TaiSanNuocNgoai_QuyenSuDungDat.DiaChi_TSNN = tsnnDiaChiDat[i];
                    nV_KeKhai_TaiSanNuocNgoai_QuyenSuDungDat.TenLoaiDat_TSNN = tsnnTenLoaiDat[i];
                    nV_KeKhai_TaiSanNuocNgoai_QuyenSuDungDat.DienTich_TSNN = tsnnDienTichDat[i];
                    nV_KeKhai_TaiSanNuocNgoai_QuyenSuDungDat.GiaTri_TSNN = tsnnGiaTriDat[i];
                    nV_KeKhai_TaiSanNuocNgoai_QuyenSuDungDat.GiayChungNhan_TSNN = tsnnGiayChungNhanQuyenSoHuuDat[i];
                    nV_KeKhai_TaiSanNuocNgoai_QuyenSuDungDat.LoaiDat_TSNN = tsnnLoaiDat[i];
                    nV_KeKhai_TaiSanNuocNgoai_QuyenSuDungDat.ThongTinKhac_TSNN = tsnnThongTinDatKhacNeuCo[i];
                    db.NV_KeKhai_TaiSanNuocNgoai_QuyenSuDungDat.Add(nV_KeKhai_TaiSanNuocNgoai_QuyenSuDungDat);
                    db.SaveChanges();
                }
            }
            for (int i = 0; i < tsnnDiaChiNhaO.Count(); i++)
            {
                if (tsnnDiaChiNhaO[i] != "")
                {
                    var nV_KeKhai_TaiSanNuocNgoai_NhaO = new NV_KeKhai_TaiSanNuocNgoai_NhaO();
                    nV_KeKhai_TaiSanNuocNgoai_NhaO.Ma_KeKhai_TSTN = nv_KeKhai_TSTN.Ma_KeKhai_TSTN;
                    nV_KeKhai_TaiSanNuocNgoai_NhaO.DiaChi_TSNN = tsnnDiaChiNhaO[i];
                    nV_KeKhai_TaiSanNuocNgoai_NhaO.LoaiNha_TSNN = tsnnLoaiNhaO[i];
                    nV_KeKhai_TaiSanNuocNgoai_NhaO.DienTichSuDung_TSNN = tsnnDienTichNhaO[i];
                    nV_KeKhai_TaiSanNuocNgoai_NhaO.GiaTri_TSNN = tsnnGiaTriNhaO[i];
                    nV_KeKhai_TaiSanNuocNgoai_NhaO.GiayChungNhan_TSNN = tsnnGiayChungNhanNhaO[i];
                    nV_KeKhai_TaiSanNuocNgoai_NhaO.ThongTinKhac_TSNN = tsnnThongTinNhaOKhacNeuCo[i];
                    db.NV_KeKhai_TaiSanNuocNgoai_NhaO.Add(nV_KeKhai_TaiSanNuocNgoai_NhaO);
                    db.SaveChanges();
                }
            }
            for (int i = 0; i < tsnnDiaChiCongTrinh.Count(); i++)
            {
                if (tsnnDiaChiCongTrinh[i] != "")
                {
                    var nV_KeKhai_TaiSanNuocNgoai_CongTrinh = new NV_KeKhai_TaiSanNuocNgoai_CongTrinh();
                    nV_KeKhai_TaiSanNuocNgoai_CongTrinh.Ma_KeKhai_TSTN = nv_KeKhai_TSTN.Ma_KeKhai_TSTN;
                    nV_KeKhai_TaiSanNuocNgoai_CongTrinh.TenCongTrinh_TSNN = tsnnTenCongTrinh[i];
                    nV_KeKhai_TaiSanNuocNgoai_CongTrinh.DiaChi_TSNN = tsnnDiaChiCongTrinh[i];
                    nV_KeKhai_TaiSanNuocNgoai_CongTrinh.LoaiCongTrinh_TSNN = tsnnLoaiCongTrinh[i];
                    nV_KeKhai_TaiSanNuocNgoai_CongTrinh.CapCongTrinh_TSNN = tsnnCapCongTrinh[i];
                    nV_KeKhai_TaiSanNuocNgoai_CongTrinh.DienTich_TSNN = tsnnDienTichCongTrinh[i];
                    nV_KeKhai_TaiSanNuocNgoai_CongTrinh.GiaTri_TSNN = tsnnGiaTriCongTrinh[i];
                    nV_KeKhai_TaiSanNuocNgoai_CongTrinh.GiayChungNhan_TSNN = tsnnGiayChungNhanCongTrinh[i];
                    nV_KeKhai_TaiSanNuocNgoai_CongTrinh.ThongTinKhac_TSNN = tsnnThongTinCongTrinhKhacNeuCo[i];
                    db.NV_KeKhai_TaiSanNuocNgoai_CongTrinh.Add(nV_KeKhai_TaiSanNuocNgoai_CongTrinh);
                    db.SaveChanges();
                }
            }
            for (int i = 0; i < tsnnTenTaiSan.Count(); i++)
            {
                if (tsnnTenTaiSan[i] != "")
                {
                    var nV_KeKhai_TaiSanNuocNgoai_TaiSanGanVoiDat1 = new NV_KeKhai_TaiSanNuocNgoai_TaiSanGanVoiDat();
                    nV_KeKhai_TaiSanNuocNgoai_TaiSanGanVoiDat1.Ma_KeKhai_TSTN = nv_KeKhai_TSTN.Ma_KeKhai_TSTN;
                    nV_KeKhai_TaiSanNuocNgoai_TaiSanGanVoiDat1.TenTaiSan_TSNN = tsnnTenTaiSan[i];
                    nV_KeKhai_TaiSanNuocNgoai_TaiSanGanVoiDat1.SoLuong_DienTich_TSNN = tsnnSoLuong_DienTich[i];
                    nV_KeKhai_TaiSanNuocNgoai_TaiSanGanVoiDat1.GiaTri_TSNN = tsnnGiaTriTS[i];
                    nV_KeKhai_TaiSanNuocNgoai_TaiSanGanVoiDat1.LoaiTaiSan_TSNN = tsnnLoaiTaiSan[i];
                    db.NV_KeKhai_TaiSanNuocNgoai_TaiSanGanVoiDat.Add(nV_KeKhai_TaiSanNuocNgoai_TaiSanGanVoiDat1);
                    db.SaveChanges();
                }

            }

            for (int i = 0; i < tsnnTenTrangSuc.Count(); i++)
            {
                if (tsnnTenTrangSuc[i] != "")
                {
                    var nV_KeKhai_TaiSanNuocNgoai_TaiSanTrangSuc = new NV_KeKhai_TaiSanNuocNgoai_TaiSanTrangSuc();
                    nV_KeKhai_TaiSanNuocNgoai_TaiSanTrangSuc.Ma_KeKhai_TSTN = nv_KeKhai_TSTN.Ma_KeKhai_TSTN;
                    nV_KeKhai_TaiSanNuocNgoai_TaiSanTrangSuc.TenTrangSuc_TSNN = tsnnTenTrangSuc[i];
                    nV_KeKhai_TaiSanNuocNgoai_TaiSanTrangSuc.GiaTri_TSNN = tsnnGiaTriTrangSuc[i];
                    db.NV_KeKhai_TaiSanNuocNgoai_TaiSanTrangSuc.Add(nV_KeKhai_TaiSanNuocNgoai_TaiSanTrangSuc);
                    db.SaveChanges();
                }
            }
            for (int i = 0; i < tsnnTenLoaiTien.Count(); i++)
            {
                if (tsnnTenLoaiTien[i] != "")
                {
                    var nV_KeKhai_TaiSanNuocNgoai_Tien = new NV_KeKhai_TaiSanNuocNgoai_Tien();
                    nV_KeKhai_TaiSanNuocNgoai_Tien.Ma_KeKhai_TSTN = nv_KeKhai_TSTN.Ma_KeKhai_TSTN;
                    nV_KeKhai_TaiSanNuocNgoai_Tien.TenLoaiTien_TSNN = tsnnTenLoaiTien[i];
                    nV_KeKhai_TaiSanNuocNgoai_Tien.GiaTri_TSNN = tsnnGiaTriLoaiTien[i];


                    db.NV_KeKhai_TaiSanNuocNgoai_Tien.Add(nV_KeKhai_TaiSanNuocNgoai_Tien);
                    db.SaveChanges();
                }
            }


            for (int i = 0; i < tsnnTenPhieu.Count(); i++)
            {
                if (tsnnTenPhieu[i] != "")
                {
                    var nV_KeKhai_TaiSanNuocNgoai_TaiSanPhieu = new NV_KeKhai_TaiSanNuocNgoai_TaiSanPhieu();
                    nV_KeKhai_TaiSanNuocNgoai_TaiSanPhieu.Ma_KeKhai_TSTN = nv_KeKhai_TSTN.Ma_KeKhai_TSTN;
                    nV_KeKhai_TaiSanNuocNgoai_TaiSanPhieu.TenPhieu_TSNN = tsnnTenPhieu[i];
                    nV_KeKhai_TaiSanNuocNgoai_TaiSanPhieu.LoaiPhieu_TSNN = tsnnLoaiPhieu[i];
                    nV_KeKhai_TaiSanNuocNgoai_TaiSanPhieu.SoLuong_TSNN = tsnnSoLuongPhieu[i];
                    nV_KeKhai_TaiSanNuocNgoai_TaiSanPhieu.GiaTri_TSNN = tsnnGiaTriPhieu[i];

                    db.NV_KeKhai_TaiSanNuocNgoai_TaiSanPhieu.Add(nV_KeKhai_TaiSanNuocNgoai_TaiSanPhieu);
                    db.SaveChanges();
                }
            }
            for (int i = 0; i < tsnnTenTaiSanKhac.Count(); i++)
            {
                if (tsnnTenTaiSanKhac[i] != "")
                {
                    var nV_KeKhai_TaiSanNuocNgoai_TaiSanKhac = new NV_KeKhai_TaiSanNuocNgoai_TaiSanKhac();
                    nV_KeKhai_TaiSanNuocNgoai_TaiSanKhac.Ma_KeKhai_TSTN = nv_KeKhai_TSTN.Ma_KeKhai_TSTN;
                    nV_KeKhai_TaiSanNuocNgoai_TaiSanKhac.TenTaiSan_TSNN = TenTaiSanKhac[i];
                    nV_KeKhai_TaiSanNuocNgoai_TaiSanKhac.SoDangKy_NamBatDauSuDung_TSNN = tsnnSoDangKyTaiSanKhac[i] != "" ? tsnnSoDangKyTaiSanKhac[i] : tsnnNamBatDauSoHuuTaiSanKhac[i];
                    nV_KeKhai_TaiSanNuocNgoai_TaiSanKhac.LoaiTaiSanKhac_TSNN = tsnnLoaiTaiSanKhac[i];
                    nV_KeKhai_TaiSanNuocNgoai_TaiSanKhac.GiaTri_TSNN = tsnnGiaTriTaiSanKhac[i];

                    db.NV_KeKhai_TaiSanNuocNgoai_TaiSanKhac.Add(nV_KeKhai_TaiSanNuocNgoai_TaiSanKhac);
                    db.SaveChanges();
                }
            }
            //end kê khai tài sản nước ngoài


            for (int i = 0; i < ThuNhapNguoiKeKhai.Count(); i++)
            {
                if (ThuNhapNguoiKeKhai[i] != "")
                {
                    var nV_KeKhai_TongThuNhap = new NV_KeKhai_TongThuNhap();
                    nV_KeKhai_TongThuNhap.Ma_KeKhai_TSTN = nv_KeKhai_TSTN.Ma_KeKhai_TSTN;
                    nV_KeKhai_TongThuNhap.TongThuNhap_NguoiKeKhai = ThuNhapNguoiKeKhai[i];
                    nV_KeKhai_TongThuNhap.TongThuNhap_VoHoacChong = ThuNhapVoHoacChong[i];
                    nV_KeKhai_TongThuNhap.TongThuNhap_ConChuaThanhNien = ThuNhapCon[i];
                    nV_KeKhai_TongThuNhap.TongThuNhap_CacKhoanChung = ThuNhapChung[i];
                    db.NV_KeKhai_TongThuNhap.Add(nV_KeKhai_TongThuNhap);
                    db.SaveChanges();
                }
            }
            for (int i = 0; i < TenTaiKhoanNuocNgoai.Count(); i++)
            {
                if (TenTaiKhoanNuocNgoai[i] != "")
                {
                    var nV_KeKhai_TaiKhoanNuocNgoai = new NV_KeKhai_TaiKhoanNuocNgoai();
                    nV_KeKhai_TaiKhoanNuocNgoai.Ma_KeKhai_TSTN = nv_KeKhai_TSTN.Ma_KeKhai_TSTN;
                    nV_KeKhai_TaiKhoanNuocNgoai.TenChuTaiKhoan = TenTaiKhoanNuocNgoai[i];
                    nV_KeKhai_TaiKhoanNuocNgoai.SoTaiKhoan = SoTaiKhoanNuocNgoai[i];
                    nV_KeKhai_TaiKhoanNuocNgoai.TenNganHang = TenNganHangNguocNgoai[i];
                    db.NV_KeKhai_TaiKhoanNuocNgoai.Add(nV_KeKhai_TaiKhoanNuocNgoai);
                    db.SaveChanges();
                }
            }
            for (int i = 0; i < TenBienDongTS.Count(); i++)
            {
                if (TenBienDongTS[i] != "")
                {
                    var nV_KeKhai_BienDongTaiSan = new NV_KeKhai_BienDongTaiSan();
                    nV_KeKhai_BienDongTaiSan.Ma_KeKhai_TSTN = nv_KeKhai_TSTN.Ma_KeKhai_TSTN;
                    nV_KeKhai_BienDongTaiSan.TenLoaiTSTNJson = TenBienDongTS[i];
                    nV_KeKhai_BienDongTaiSan.LoaiTSTN = LoaiBienDongTS[i];
                    nV_KeKhai_BienDongTaiSan.SoLuongTaiSanJson = SoLuongBienDongTS[i];
                    nV_KeKhai_BienDongTaiSan.GiaTriTSTNJson = GiaTriBienDongTS[i];
                    nV_KeKhai_BienDongTaiSan.NoiDungGiaiTrinhJson = NoiDungBienDongTS[i];
                    db.NV_KeKhai_BienDongTaiSan.Add(nV_KeKhai_BienDongTaiSan);
                    db.SaveChanges();
                }
            }

            return Json(Url.Action("Index", "NV_KeKhai_TSTN"), JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public async Task<JsonResult> Edit([Bind(Include = "Ma_KeKhai_TSTN, Ma_CanBo,Ngay_KeKhai,Thang_KeKhai,Nam_KeKhai,NgayThangNam,Ma_Loai_KeKhai, Ma_LinhVuc_KeKhai, MaKeHoachKeKhai, TrangThai")] Nv_KeKhai_TSTN nv_KeKhai_TSTN,
                                int Ma_CanBo, string HoTen, DateTime DoB, string DiaChiThuongTru, string SoCCCD, DateTime NgayCap, string NoiCap, int Ma_CoQuan_DonVi, int Ma_ChucVu_ChucDanh,
                                List<string> HoTenThanNhan, List<DateTime> DoBTN, List<string> NgheNghiep, List<string> NoiLamViec, List<string> DiaChiThuongTruTN, List<string> SoCCCDTN, List<string> NgayCapTN, List<string> NoiCapTN, List<string> VaiTroThanNhan
                                ,string BienDongTaiSan
                                , List<int> Ma_QuyenSuDungDat, List<string> DiaChiDat, List<string> TenLoaiDat, List<string> LoaiDat, List<string> DienTichDat, List<string> GiaTriDat, List<string> ThongTinDatKhacNeuCo, List<string> GiayChungNhanQuyenSoHuuDat
                               , List<int> Ma_NhaO, List<string> DiaChiNhaO, List<string> LoaiNhaO, List<string> DienTichNhaO, List<string> GiaTriNhaO, List<string> GiayChungNhanNhaO, List<string> ThongTinNhaOKhacNeuCo
                               , List<int> Ma_CongTrinh, List<string> TenCongTrinh, List<string> DiaChiCongTrinh, List<string> LoaiCongTrinh, List<string> CapCongTrinh, List<string> DienTichCongTrinh, List<string> GiaTriCongTrinh, List<string> GiayChungNhanCongTrinh, List<string> ThongTinCongTrinhKhacNeuCo
                               , List<int> Ma_TaiSanGanVoiDat, List<string> LoaiTaiSan, List<string> TenTaiSan, List<string> SoLuong_DienTich, List<string> GiaTriTS     
                               , List<int> Ma_TrangSuc, List<string> TenTrangSuc, List<string> GiaTriTrangSuc
                               , List<int> Ma_Tien, List<string> TenLoaiTien, List<string> GiaTriLoaiTien
                               , List<int> Ma_TaiSanPhieu, List<string> TenPhieu, List<string> LoaiPhieu, List<string> SoLuongPhieu, List<string> GiaTriPhieu
                               , List<int> Ma_TaiSanKhac, List<string> TenTaiSanKhac, List<string> LoaiTaiSanKhac, List<string> SoDangKyTaiSanKhac, List<string> NamBatDauSoHuuTaiSanKhac, List<string> GiaTriTaiSanKhac
                               , List<int> Ma_TaiSanNuocNgoai, List<string> TenTaiSanNuocNgoai, List<string> GiaTriTaiSanNuocNgoai
                               , List<int> Ma_TaiKhoanNuocNgoai, List<string> TenTaiKhoanNuocNgoai, List<string> SoTaiKhoanNuocNgoai, List<string> TenNganHangNguocNgoai
                               , List<int> Ma_TongThuNhap, List<string> ThuNhapNguoiKeKhai, List<string> ThuNhapVoHoacChong, List<string> ThuNhapCon, List<string> ThuNhapChung
                               , List<int> Ma_BienDongTaiSan, List<string> TenBienDongTS, List<string> LoaiBienDongTS, List<string> SoLuongBienDongTS, List<string> GiaTriBienDongTS, List<string> NoiDungBienDongTS

                               , List<int> tsnnMa_QuyenSuDungDat, List<string> tsnnDiaChiDat, List<string> tsnnTenLoaiDat, List<string> tsnnLoaiDat, List<string> tsnnDienTichDat, List<string> tsnnGiaTriDat, List<string> tsnnThongTinDatKhacNeuCo, List<string> tsnnGiayChungNhanQuyenSoHuuDat
                               , List<int> tsnnMa_NhaO, List<string> tsnnDiaChiNhaO, List<string> tsnnLoaiNhaO, List<string> tsnnDienTichNhaO, List<string> tsnnGiaTriNhaO, List<string> tsnnGiayChungNhanNhaO, List<string> tsnnThongTinNhaOKhacNeuCo
                               , List<int> tsnnMa_CongTrinh, List<string> tsnnTenCongTrinh, List<string> tsnnDiaChiCongTrinh, List<string> tsnnLoaiCongTrinh, List<string> tsnnCapCongTrinh, List<string> tsnnDienTichCongTrinh, List<string> tsnnGiaTriCongTrinh, List<string> tsnnGiayChungNhanCongTrinh, List<string> tsnnThongTinCongTrinhKhacNeuCo
                               , List<int> tsnnMa_TaiSanGanVoiDat, List<string> tsnnLoaiTaiSan, List<string> tsnnTenTaiSan, List<string> tsnnSoLuong_DienTich, List<string> tsnnGiaTriTS
                               , List<int> tsnnMa_TrangSuc, List<string> tsnnTenTrangSuc, List<string> tsnnGiaTriTrangSuc
                               , List<int> tsnnMa_Tien, List<string> tsnnTenLoaiTien, List<string> tsnnGiaTriLoaiTien
                               , List<int> tsnnMa_TaiSanPhieu, List<string> tsnnTenPhieu, List<string> tsnnLoaiPhieu, List<string> tsnnSoLuongPhieu, List<string> tsnnGiaTriPhieu
                               , List<int> tsnnMa_TaiSanKhac, List<string> tsnnTenTaiSanKhac, List<string> tsnnLoaiTaiSanKhac, List<string> tsnnSoDangKyTaiSanKhac, List<string> tsnnNamBatDauSoHuuTaiSanKhac, List<string> tsnnGiaTriTaiSanKhac
                               , List<int> tsnnMa_TaiSanNuocNgoai, List<string> tsnnTenTaiSanNuocNgoai, List<string> tsnnGiaTriTaiSanNuocNgoai)
                               
        {

            var canbo = db.DM_CanBo.Find(Ma_CanBo);

            canbo.HoTen = HoTen;
            canbo.DoB = Convert.ToDateTime(DoB).ToString("dd/MM/yyyy");
            canbo.DiaChiThuongTru = DiaChiThuongTru;
            canbo.SoCCCD = SoCCCD;
            canbo.NgayCap = Convert.ToDateTime(NgayCap).ToString("dd/MM/yyyy");
            canbo.NoiCap = NoiCap;
            canbo.Ma_CoQuan_DonVi = Ma_CoQuan_DonVi;
            canbo.Ma_ChucVu_ChucDanh = Ma_ChucVu_ChucDanh;
            canbo.Ten = HoTen.Substring(HoTen.LastIndexOf(" ") + 1);



            db.Entry(canbo).State = EntityState.Modified;
            db.SaveChanges();

            var cbth = db.DM_CanBo_ThanNhan.Where(_ => _.Ma_CanBo == Ma_CanBo);
            foreach (var i in cbth)
            {
                db.DM_CanBo_ThanNhan.Remove(i);

            }
            db.SaveChanges();

            for (int i = 0; i < HoTenThanNhan.Count(); i++)
            {
                if (HoTenThanNhan[i] != "")
                {
                    var tn = new DM_CanBo_ThanNhan();
                    tn.Ma_CanBo = Ma_CanBo;
                    tn.HoTen = HoTenThanNhan[i];
                    tn.DoB = Convert.ToDateTime(DoBTN[i]).ToString("dd/MM/yyyy");
                    tn.DiaChiThuongTru = DiaChiThuongTruTN[i];

                    tn.SoCCCD = SoCCCDTN[i];
                    tn.NgayCap = Convert.ToDateTime(NgayCapTN[i]).ToString("dd/MM/yyyy");

                    if (tn.NgayCap == "01/01/0001") tn.NgayCap = "";

                    tn.NoiCap = NoiCapTN[i];
                    tn.VaiTroThanNhan = VaiTroThanNhan[i];
                    tn.NgheNghiep = NgheNghiep[i];
                    tn.NoiLamViec = NoiLamViec[i];
                    db.DM_CanBo_ThanNhan.Add(tn);
                    db.SaveChanges();
                }
            }

            var MaKeHoachkk = db.Nv_KeKhai_TSTN.Where(_ => _.MaKeHoachKeKhai == nv_KeKhai_TSTN.MaKeHoachKeKhai).Select(_ => _.MaKeHoachKeKhai).FirstOrDefault();
            nv_KeKhai_TSTN.Ngay_KeKhai = (short)DateTime.Now.Day;
            nv_KeKhai_TSTN.Thang_KeKhai = (short)DateTime.Now.Month;
            nv_KeKhai_TSTN.Nam_KeKhai = (short)DateTime.Now.Year;
            nv_KeKhai_TSTN.NgayThangNam = DateTime.Now;
            nv_KeKhai_TSTN.TrangThai = false;
            nv_KeKhai_TSTN.TrangThaiTiepNhan = 0;
            nv_KeKhai_TSTN.MaKeHoachKeKhai = MaKeHoachkk;
            nv_KeKhai_TSTN.BienDongTaiSan = BienDongTaiSan;
            nv_KeKhai_TSTN.Ma_LinhVuc_KeKhai = 1;
            nv_KeKhai_TSTN.HoTen = canbo.HoTen;
            nv_KeKhai_TSTN.DoB = DoB;
            nv_KeKhai_TSTN.DiaChiThuongTru = canbo.DiaChiThuongTru;
            nv_KeKhai_TSTN.SoCCCD = canbo.SoCCCD;
            nv_KeKhai_TSTN.NgayCap = NgayCap;
            nv_KeKhai_TSTN.NoiCap = canbo.NoiCap;
            nv_KeKhai_TSTN.Ma_CoQuan_DonVi = canbo.Ma_CoQuan_DonVi;
            nv_KeKhai_TSTN.Ma_ChucVu_ChucDanh = canbo.Ma_ChucVu_ChucDanh;

            db.Entry(nv_KeKhai_TSTN).State = EntityState.Modified;
            db.SaveChanges();

            //Store delete toàn bộ dữ liệu bởi Mã kê khai
            db.stp_DelKeKhaiTaiSan(nv_KeKhai_TSTN.Ma_KeKhai_TSTN);



            for (int i = 0; i < DienTichDat.Count(); i++)
            {
                if (DienTichDat[i] != "")
                {
                    var nV_KeKhai_QuyenSuDungDat = new NV_KeKhai_QuyenSuDungDat();
                    nV_KeKhai_QuyenSuDungDat.Ma_KeKhai_TSTN = nv_KeKhai_TSTN.Ma_KeKhai_TSTN;
                    nV_KeKhai_QuyenSuDungDat.DiaChi = DiaChiDat[i];
                    nV_KeKhai_QuyenSuDungDat.TenLoaiDat = TenLoaiDat[i];
                    nV_KeKhai_QuyenSuDungDat.DienTich = DienTichDat[i];
                    nV_KeKhai_QuyenSuDungDat.GiaTri = GiaTriDat[i];
                    nV_KeKhai_QuyenSuDungDat.GiayChungNhan = GiayChungNhanQuyenSoHuuDat[i];
                    nV_KeKhai_QuyenSuDungDat.LoaiDat = LoaiDat[i];
                    nV_KeKhai_QuyenSuDungDat.ThongTinKhac = ThongTinDatKhacNeuCo[i];
                    db.NV_KeKhai_QuyenSuDungDat.Add(nV_KeKhai_QuyenSuDungDat);
                    db.SaveChanges();
                }
            }

            for (int i = 0; i < DienTichNhaO.Count(); i++)
            {
                if (DienTichNhaO[i] != "")
                {
                    var nV_KeKhai_NhaO = new NV_KeKhai_NhaO();
                    nV_KeKhai_NhaO.Ma_KeKhai_TSTN = nv_KeKhai_TSTN.Ma_KeKhai_TSTN;
                    nV_KeKhai_NhaO.DiaChi = DiaChiNhaO[i];
                    nV_KeKhai_NhaO.LoaiNha = LoaiNhaO[i];
                    nV_KeKhai_NhaO.DienTichSuDung = DienTichNhaO[i];
                    nV_KeKhai_NhaO.GiaTri = GiaTriNhaO[i];
                    nV_KeKhai_NhaO.GiayChungNhan = GiayChungNhanNhaO[i];
                    nV_KeKhai_NhaO.ThongTinKhac = ThongTinNhaOKhacNeuCo[i];
                    db.NV_KeKhai_NhaO.Add(nV_KeKhai_NhaO);
                    db.SaveChanges();
                }
            }
            for (int i = 0; i < DiaChiCongTrinh.Count(); i++)
            {
                if (DiaChiCongTrinh[i] != "")
                {
                    var nV_KeKhai_CongTrinh = new NV_KeKhai_CongTrinh();
                    nV_KeKhai_CongTrinh.Ma_KeKhai_TSTN = nv_KeKhai_TSTN.Ma_KeKhai_TSTN;
                    nV_KeKhai_CongTrinh.TenCongTrinh = TenCongTrinh[i];
                    nV_KeKhai_CongTrinh.DiaChi = DiaChiCongTrinh[i];
                    nV_KeKhai_CongTrinh.LoaiCongTrinh = LoaiCongTrinh[i];
                    nV_KeKhai_CongTrinh.CapCongTrinh = CapCongTrinh[i];
                    nV_KeKhai_CongTrinh.DienTich = DienTichCongTrinh[i];
                    nV_KeKhai_CongTrinh.GiaTri = GiaTriCongTrinh[i];
                    nV_KeKhai_CongTrinh.GiayChungNhan = GiayChungNhanCongTrinh[i];
                    nV_KeKhai_CongTrinh.ThongTinKhac = ThongTinCongTrinhKhacNeuCo[i];
                    db.NV_KeKhai_CongTrinh.Add(nV_KeKhai_CongTrinh);
                    db.SaveChanges();
                }
            }
            for (int i = 0; i < TenTaiSan.Count(); i++)
            {
                if (TenTaiSan[i] != "")
                {
                    var nV_KeKhai_TaiSanGanVoiDat1 = new NV_KeKhai_TaiSanGanVoiDat();
                    nV_KeKhai_TaiSanGanVoiDat1.Ma_KeKhai_TSTN = nv_KeKhai_TSTN.Ma_KeKhai_TSTN;
                    nV_KeKhai_TaiSanGanVoiDat1.TenTaiSan = TenTaiSan[i];
                    nV_KeKhai_TaiSanGanVoiDat1.SoLuong_DienTich = SoLuong_DienTich[i];
                    nV_KeKhai_TaiSanGanVoiDat1.GiaTri = GiaTriTS[i];
                    nV_KeKhai_TaiSanGanVoiDat1.LoaiTaiSan = LoaiTaiSan[i];
                    db.NV_KeKhai_TaiSanGanVoiDat.Add(nV_KeKhai_TaiSanGanVoiDat1);
                    db.SaveChanges();
                }
            }

            for (int i = 0; i < TenTrangSuc.Count(); i++)
            {
                if (TenTrangSuc[i] != "")
                {
                    var nV_KeKhai_TaiSanTrangSuc = new NV_KeKhai_TaiSanTrangSuc();
                    nV_KeKhai_TaiSanTrangSuc.Ma_KeKhai_TSTN = nv_KeKhai_TSTN.Ma_KeKhai_TSTN;
                    nV_KeKhai_TaiSanTrangSuc.TenTrangSuc = TenTrangSuc[i];
                    nV_KeKhai_TaiSanTrangSuc.GiaTri = GiaTriTrangSuc[i];
                    db.NV_KeKhai_TaiSanTrangSuc.Add(nV_KeKhai_TaiSanTrangSuc);
                    db.SaveChanges();
                }
            }
            for (int i = 0; i < TenLoaiTien.Count(); i++)
            {
                if (TenLoaiTien[i] != "")
                {
                    var nV_KeKhai_Tien = new NV_KeKhai_Tien();
                    nV_KeKhai_Tien.Ma_KeKhai_TSTN = nv_KeKhai_TSTN.Ma_KeKhai_TSTN;
                    nV_KeKhai_Tien.TenLoaiTien = TenLoaiTien[i];
                    nV_KeKhai_Tien.GiaTri = GiaTriLoaiTien[i];


                    db.NV_KeKhai_Tien.Add(nV_KeKhai_Tien);
                    db.SaveChanges();
                }
            }


            for (int i = 0; i < TenPhieu.Count(); i++)
            {
                if (TenPhieu[i] != "")
                {
                    var nV_KeKhai_TaiSanPhieu = new NV_KeKhai_TaiSanPhieu();
                    nV_KeKhai_TaiSanPhieu.Ma_KeKhai_TSTN = nv_KeKhai_TSTN.Ma_KeKhai_TSTN;
                    nV_KeKhai_TaiSanPhieu.TenPhieu = TenPhieu[i];
                    nV_KeKhai_TaiSanPhieu.LoaiPhieu = LoaiPhieu[i];
                    nV_KeKhai_TaiSanPhieu.SoLuong = SoLuongPhieu[i];
                    nV_KeKhai_TaiSanPhieu.GiaTri = GiaTriPhieu[i];

                    db.NV_KeKhai_TaiSanPhieu.Add(nV_KeKhai_TaiSanPhieu);
                    db.SaveChanges();
                }
            }
            for (int i = 0; i < TenTaiSanKhac.Count(); i++)
            {
                if (TenTaiSanKhac[i] != "")
                {
                    var nV_KeKhai_TaiSanKhac = new NV_KeKhai_TaiSanKhac();
                    nV_KeKhai_TaiSanKhac.Ma_KeKhai_TSTN = nv_KeKhai_TSTN.Ma_KeKhai_TSTN;
                    nV_KeKhai_TaiSanKhac.TenTaiSan = TenTaiSanKhac[i];
                    nV_KeKhai_TaiSanKhac.SoDangKy_NamBatDauSuDung = SoDangKyTaiSanKhac[i] != "" ? SoDangKyTaiSanKhac[i] : NamBatDauSoHuuTaiSanKhac[i];
                    nV_KeKhai_TaiSanKhac.LoaiTaiSanKhac = LoaiTaiSanKhac[i];
                    nV_KeKhai_TaiSanKhac.GiaTri = GiaTriTaiSanKhac[i];

                    db.NV_KeKhai_TaiSanKhac.Add(nV_KeKhai_TaiSanKhac);
                    db.SaveChanges();
                }
            }
            //Tài sản nước ngoài
            for (int i = 0; i < tsnnDiaChiDat.Count(); i++)
            {
                if (tsnnDiaChiDat[i] != "")
                {
                    var nV_KeKhai_TaiSanNuocNgoai_QuyenSuDungDat = new NV_KeKhai_TaiSanNuocNgoai_QuyenSuDungDat();
                    nV_KeKhai_TaiSanNuocNgoai_QuyenSuDungDat.Ma_KeKhai_TSTN = nv_KeKhai_TSTN.Ma_KeKhai_TSTN;
                    nV_KeKhai_TaiSanNuocNgoai_QuyenSuDungDat.DiaChi_TSNN = tsnnDiaChiDat[i];
                    nV_KeKhai_TaiSanNuocNgoai_QuyenSuDungDat.TenLoaiDat_TSNN = tsnnTenLoaiDat[i];
                    nV_KeKhai_TaiSanNuocNgoai_QuyenSuDungDat.DienTich_TSNN = tsnnDienTichDat[i];
                    nV_KeKhai_TaiSanNuocNgoai_QuyenSuDungDat.GiaTri_TSNN = tsnnGiaTriDat[i];
                    nV_KeKhai_TaiSanNuocNgoai_QuyenSuDungDat.GiayChungNhan_TSNN = tsnnGiayChungNhanQuyenSoHuuDat[i];
                    nV_KeKhai_TaiSanNuocNgoai_QuyenSuDungDat.LoaiDat_TSNN = tsnnLoaiDat[i];
                    nV_KeKhai_TaiSanNuocNgoai_QuyenSuDungDat.ThongTinKhac_TSNN = tsnnThongTinDatKhacNeuCo[i];
                    db.NV_KeKhai_TaiSanNuocNgoai_QuyenSuDungDat.Add(nV_KeKhai_TaiSanNuocNgoai_QuyenSuDungDat);
                    db.SaveChanges();
                }
            }
            for (int i = 0; i < tsnnDiaChiNhaO.Count(); i++)
            {
                if (tsnnDiaChiNhaO[i] != "")
                {
                    var nV_KeKhai_TaiSanNuocNgoai_NhaO = new NV_KeKhai_TaiSanNuocNgoai_NhaO();
                    nV_KeKhai_TaiSanNuocNgoai_NhaO.Ma_KeKhai_TSTN = nv_KeKhai_TSTN.Ma_KeKhai_TSTN;
                    nV_KeKhai_TaiSanNuocNgoai_NhaO.DiaChi_TSNN = tsnnDiaChiNhaO[i];
                    nV_KeKhai_TaiSanNuocNgoai_NhaO.LoaiNha_TSNN = tsnnLoaiNhaO[i];
                    nV_KeKhai_TaiSanNuocNgoai_NhaO.DienTichSuDung_TSNN = tsnnDienTichNhaO[i];
                    nV_KeKhai_TaiSanNuocNgoai_NhaO.GiaTri_TSNN = tsnnGiaTriNhaO[i];
                    nV_KeKhai_TaiSanNuocNgoai_NhaO.GiayChungNhan_TSNN = tsnnGiayChungNhanNhaO[i];
                    nV_KeKhai_TaiSanNuocNgoai_NhaO.ThongTinKhac_TSNN = tsnnThongTinNhaOKhacNeuCo[i];
                    db.NV_KeKhai_TaiSanNuocNgoai_NhaO.Add(nV_KeKhai_TaiSanNuocNgoai_NhaO);
                    db.SaveChanges();
                }
            }
            for (int i = 0; i < tsnnDiaChiCongTrinh.Count(); i++)
            {
                if (tsnnDiaChiCongTrinh[i] != "")
                {
                    var nV_KeKhai_TaiSanNuocNgoai_CongTrinh = new NV_KeKhai_TaiSanNuocNgoai_CongTrinh();
                    nV_KeKhai_TaiSanNuocNgoai_CongTrinh.Ma_KeKhai_TSTN = nv_KeKhai_TSTN.Ma_KeKhai_TSTN;
                    nV_KeKhai_TaiSanNuocNgoai_CongTrinh.TenCongTrinh_TSNN = tsnnTenCongTrinh[i];
                    nV_KeKhai_TaiSanNuocNgoai_CongTrinh.DiaChi_TSNN = tsnnDiaChiCongTrinh[i];
                    nV_KeKhai_TaiSanNuocNgoai_CongTrinh.LoaiCongTrinh_TSNN = tsnnLoaiCongTrinh[i];
                    nV_KeKhai_TaiSanNuocNgoai_CongTrinh.CapCongTrinh_TSNN = tsnnCapCongTrinh[i];
                    nV_KeKhai_TaiSanNuocNgoai_CongTrinh.DienTich_TSNN = tsnnDienTichCongTrinh[i];
                    nV_KeKhai_TaiSanNuocNgoai_CongTrinh.GiaTri_TSNN = tsnnGiaTriCongTrinh[i];
                    nV_KeKhai_TaiSanNuocNgoai_CongTrinh.GiayChungNhan_TSNN = tsnnGiayChungNhanCongTrinh[i];
                    nV_KeKhai_TaiSanNuocNgoai_CongTrinh.ThongTinKhac_TSNN = tsnnThongTinCongTrinhKhacNeuCo[i];
                    db.NV_KeKhai_TaiSanNuocNgoai_CongTrinh.Add(nV_KeKhai_TaiSanNuocNgoai_CongTrinh);
                    db.SaveChanges();
                }
            }
            for (int i = 0; i < tsnnTenTaiSan.Count(); i++)
            {
                if (tsnnTenTaiSan[i] != "")
                {
                    var nV_KeKhai_TaiSanNuocNgoai_TaiSanGanVoiDat1 = new NV_KeKhai_TaiSanNuocNgoai_TaiSanGanVoiDat();
                    nV_KeKhai_TaiSanNuocNgoai_TaiSanGanVoiDat1.Ma_KeKhai_TSTN = nv_KeKhai_TSTN.Ma_KeKhai_TSTN;
                    nV_KeKhai_TaiSanNuocNgoai_TaiSanGanVoiDat1.TenTaiSan_TSNN = tsnnTenTaiSan[i];
                    nV_KeKhai_TaiSanNuocNgoai_TaiSanGanVoiDat1.SoLuong_DienTich_TSNN = tsnnSoLuong_DienTich[i];
                    nV_KeKhai_TaiSanNuocNgoai_TaiSanGanVoiDat1.GiaTri_TSNN = tsnnGiaTriTS[i];
                    nV_KeKhai_TaiSanNuocNgoai_TaiSanGanVoiDat1.LoaiTaiSan_TSNN = tsnnLoaiTaiSan[i];
                    db.NV_KeKhai_TaiSanNuocNgoai_TaiSanGanVoiDat.Add(nV_KeKhai_TaiSanNuocNgoai_TaiSanGanVoiDat1);
                    db.SaveChanges();
                }

            }

            for (int i = 0; i < tsnnTenTrangSuc.Count(); i++)
            {
                if (tsnnTenTrangSuc[i] != "")
                {
                    var nV_KeKhai_TaiSanNuocNgoai_TaiSanTrangSuc = new NV_KeKhai_TaiSanNuocNgoai_TaiSanTrangSuc();
                    nV_KeKhai_TaiSanNuocNgoai_TaiSanTrangSuc.Ma_KeKhai_TSTN = nv_KeKhai_TSTN.Ma_KeKhai_TSTN;
                    nV_KeKhai_TaiSanNuocNgoai_TaiSanTrangSuc.TenTrangSuc_TSNN = tsnnTenTrangSuc[i];
                    nV_KeKhai_TaiSanNuocNgoai_TaiSanTrangSuc.GiaTri_TSNN = tsnnGiaTriTrangSuc[i];
                    db.NV_KeKhai_TaiSanNuocNgoai_TaiSanTrangSuc.Add(nV_KeKhai_TaiSanNuocNgoai_TaiSanTrangSuc);
                    db.SaveChanges();
                }
            }
            for (int i = 0; i < tsnnTenLoaiTien.Count(); i++)
            {
                if (tsnnTenLoaiTien[i] != "")
                {
                    var nV_KeKhai_TaiSanNuocNgoai_Tien = new NV_KeKhai_TaiSanNuocNgoai_Tien();
                    nV_KeKhai_TaiSanNuocNgoai_Tien.Ma_KeKhai_TSTN = nv_KeKhai_TSTN.Ma_KeKhai_TSTN;
                    nV_KeKhai_TaiSanNuocNgoai_Tien.TenLoaiTien_TSNN = tsnnTenLoaiTien[i];
                    nV_KeKhai_TaiSanNuocNgoai_Tien.GiaTri_TSNN = tsnnGiaTriLoaiTien[i];


                    db.NV_KeKhai_TaiSanNuocNgoai_Tien.Add(nV_KeKhai_TaiSanNuocNgoai_Tien);
                    db.SaveChanges();
                }
            }


            for (int i = 0; i < tsnnTenPhieu.Count(); i++)
            {
                if (tsnnTenPhieu[i] != "")
                {
                    var nV_KeKhai_TaiSanNuocNgoai_TaiSanPhieu = new NV_KeKhai_TaiSanNuocNgoai_TaiSanPhieu();
                    nV_KeKhai_TaiSanNuocNgoai_TaiSanPhieu.Ma_KeKhai_TSTN = nv_KeKhai_TSTN.Ma_KeKhai_TSTN;
                    nV_KeKhai_TaiSanNuocNgoai_TaiSanPhieu.TenPhieu_TSNN = tsnnTenPhieu[i];
                    nV_KeKhai_TaiSanNuocNgoai_TaiSanPhieu.LoaiPhieu_TSNN = tsnnLoaiPhieu[i];
                    nV_KeKhai_TaiSanNuocNgoai_TaiSanPhieu.SoLuong_TSNN = tsnnSoLuongPhieu[i];
                    nV_KeKhai_TaiSanNuocNgoai_TaiSanPhieu.GiaTri_TSNN = tsnnGiaTriPhieu[i];

                    db.NV_KeKhai_TaiSanNuocNgoai_TaiSanPhieu.Add(nV_KeKhai_TaiSanNuocNgoai_TaiSanPhieu);
                    db.SaveChanges();
                }
            }
            for (int i = 0; i < tsnnTenTaiSanKhac.Count(); i++)
            {
                if (tsnnTenTaiSanKhac[i] != "")
                {
                    var nV_KeKhai_TaiSanNuocNgoai_TaiSanKhac = new NV_KeKhai_TaiSanNuocNgoai_TaiSanKhac();
                    nV_KeKhai_TaiSanNuocNgoai_TaiSanKhac.Ma_KeKhai_TSTN = nv_KeKhai_TSTN.Ma_KeKhai_TSTN;
                    nV_KeKhai_TaiSanNuocNgoai_TaiSanKhac.TenTaiSan_TSNN = tsnnTenTaiSanKhac[i];
                    nV_KeKhai_TaiSanNuocNgoai_TaiSanKhac.SoDangKy_NamBatDauSuDung_TSNN = tsnnSoDangKyTaiSanKhac[i] != "" ? tsnnSoDangKyTaiSanKhac[i] : tsnnNamBatDauSoHuuTaiSanKhac[i];
                    nV_KeKhai_TaiSanNuocNgoai_TaiSanKhac.LoaiTaiSanKhac_TSNN = tsnnLoaiTaiSanKhac[i];
                    nV_KeKhai_TaiSanNuocNgoai_TaiSanKhac.GiaTri_TSNN = tsnnGiaTriTaiSanKhac[i];

                    db.NV_KeKhai_TaiSanNuocNgoai_TaiSanKhac.Add(nV_KeKhai_TaiSanNuocNgoai_TaiSanKhac);
                    db.SaveChanges();
                }
            }
            // End tài sản nước ngoài

            for (int i = 0; i < ThuNhapNguoiKeKhai.Count(); i++)
            {
                if (ThuNhapNguoiKeKhai[i] != "")
                {
                    var nV_KeKhai_TongThuNhap = new NV_KeKhai_TongThuNhap();
                    nV_KeKhai_TongThuNhap.Ma_KeKhai_TSTN = nv_KeKhai_TSTN.Ma_KeKhai_TSTN;
                    nV_KeKhai_TongThuNhap.TongThuNhap_NguoiKeKhai = ThuNhapNguoiKeKhai[i];
                    nV_KeKhai_TongThuNhap.TongThuNhap_VoHoacChong = ThuNhapVoHoacChong[i];
                    nV_KeKhai_TongThuNhap.TongThuNhap_ConChuaThanhNien = ThuNhapCon[i];
                    nV_KeKhai_TongThuNhap.TongThuNhap_CacKhoanChung = ThuNhapChung[i];
                    db.NV_KeKhai_TongThuNhap.Add(nV_KeKhai_TongThuNhap);
                    db.SaveChanges();
                }
            }
            for (int i = 0; i < TenTaiKhoanNuocNgoai.Count(); i++)
            {
                if (TenTaiKhoanNuocNgoai[i] != "")
                {
                    var nV_KeKhai_TaiKhoanNuocNgoai = new NV_KeKhai_TaiKhoanNuocNgoai();
                    nV_KeKhai_TaiKhoanNuocNgoai.Ma_KeKhai_TSTN = nv_KeKhai_TSTN.Ma_KeKhai_TSTN;
                    nV_KeKhai_TaiKhoanNuocNgoai.TenChuTaiKhoan = TenTaiKhoanNuocNgoai[i];
                    nV_KeKhai_TaiKhoanNuocNgoai.SoTaiKhoan = SoTaiKhoanNuocNgoai[i];
                    nV_KeKhai_TaiKhoanNuocNgoai.TenNganHang = TenNganHangNguocNgoai[i];
                    db.NV_KeKhai_TaiKhoanNuocNgoai.Add(nV_KeKhai_TaiKhoanNuocNgoai);
                    db.SaveChanges();
                }
            }
            for (int i = 0; i < TenBienDongTS.Count(); i++)
            {
                if (TenBienDongTS[i] != "")
                {
                    var nV_KeKhai_BienDongTaiSan = new NV_KeKhai_BienDongTaiSan();
                    nV_KeKhai_BienDongTaiSan.Ma_KeKhai_TSTN = nv_KeKhai_TSTN.Ma_KeKhai_TSTN;
                    nV_KeKhai_BienDongTaiSan.TenLoaiTSTNJson = TenBienDongTS[i];
                    nV_KeKhai_BienDongTaiSan.LoaiTSTN = LoaiBienDongTS[i];
                    nV_KeKhai_BienDongTaiSan.SoLuongTaiSanJson = SoLuongBienDongTS[i];
                    nV_KeKhai_BienDongTaiSan.GiaTriTSTNJson = GiaTriBienDongTS[i];
                    nV_KeKhai_BienDongTaiSan.NoiDungGiaiTrinhJson = NoiDungBienDongTS[i];
                    db.NV_KeKhai_BienDongTaiSan.Add(nV_KeKhai_BienDongTaiSan);
                    db.SaveChanges();
                }
            }

            return Json(true, JsonRequestBehavior.AllowGet);
        }




        public JsonResult GetThongTinKeKhaiCanBo(int id)
        {
            var data = (from kk in db.Nv_KeKhai_TSTN
                        where kk.Ma_CanBo == id
                       join loaikk in db.DM_Loai_KeKhai on kk.Ma_Loai_KeKhai equals loaikk.Ma_Loai_KeKhai
                       orderby kk.NgayThangNam descending
                       select new {BienDongTaiSan = db.NV_KeKhai_BienDongTaiSan.Select(_ => _.Ma_KeKhai_TSTN).Contains(kk.Ma_KeKhai_TSTN), TenLoaiKeKhai = loaikk.Ten_KeKhai, NgayKeKhai = kk.NgayThangNam, MaKeKhai = kk.Ma_KeKhai_TSTN}).ToList();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult getMaKeKhaiByKeHoachKeKhai(int MaKeHoachKeKhai)
        {
            var userID = user.GetUser();

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
            }
            catch { }
            var makekhai = (from kk in db.Nv_KeKhai_TSTN
                            where kk.MaKeHoachKeKhai == MaKeHoachKeKhai && kk.Ma_CanBo == MaCanBo
                            select kk.Ma_KeKhai_TSTN).FirstOrDefault();
            return Json(makekhai);
        }

        public JsonResult completeKeKhai(int MaKeKhai)
        {
            
            var MaCanBo = user.GetUser();
            var idBanKeKhai = db.Nv_KeKhai_TSTN.Where(_ => _.Ma_KeKhai_TSTN == MaKeKhai && _.Ma_CanBo == MaCanBo).FirstOrDefault();

            if (idBanKeKhai.FileDinhKem == "" || idBanKeKhai.FileDinhKem == null)
            {
                return Json(new { status = "warning", message = "Bạn Chưa Đính Kèm Bản Kê Khai Đã Có Chữ Ký. Vui Lòng Thực Hiện!" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                idBanKeKhai.TrangThai = true;
                idBanKeKhai.TrangThaiTiepNhan = 1;
                idBanKeKhai.Ngay_KeKhai = (short)DateTime.Now.Day;
                idBanKeKhai.Thang_KeKhai = (short)DateTime.Now.Month;
                idBanKeKhai.Nam_KeKhai = (short)DateTime.Now.Year;
                idBanKeKhai.NgayThangNam = DateTime.Now;
                db.Entry(idBanKeKhai).State = EntityState.Modified;
                db.SaveChanges();
                return Json(new { status = "success", message = "Đã Hoàn Thành Và Gửi Bản Kê Khai Đi." }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult InBanKeKhaiCanBo(int? id , int? MaKeHoachKeKhai, int? MaCanBo)
        {
            if(id == null)
            {
                id = db.Nv_KeKhai_TSTN.FirstOrDefault(_ => _.Ma_CanBo == MaCanBo && _.MaKeHoachKeKhai == MaKeHoachKeKhai).Ma_KeKhai_TSTN;
            }
           

            var bankekhai = (from bkk in db.Nv_KeKhai_TSTN
                             join lkk in db.DM_Loai_KeKhai on bkk.Ma_Loai_KeKhai equals lkk.Ma_Loai_KeKhai
                             where bkk.Ma_KeKhai_TSTN == id
                             select new { bkk.Ma_CanBo, bkk.Ma_KeKhai_TSTN, bkk.Ma_Loai_KeKhai, bkk.Nam_KeKhai, bkk.NgayThangNam, bkk.Ngay_KeKhai, bkk.Thang_KeKhai, lkk.Ten_KeKhai, bkk.BienDongTaiSan}).SingleOrDefault();

            var ThongTinCoBan = (from user in db.DM_CanBo
                                 where user.Ma_CanBo == bankekhai.Ma_CanBo
                                 join cq in db.DM_CoQuanDonVi on user.Ma_CoQuan_DonVi equals cq.Ma_CoQuan_DonVi
                                 join cv in db.DM_ChucVu_ChucDanh on user.Ma_ChucVu_ChucDanh equals cv.Ma_ChucVu_ChucDanh
                                 join lcq in db.DM_Loai_CoQuan_DonVi on cq.MaLoai_CoQuan_DonVi equals lcq.Ma_Loai_CQDV
                                 select new
                                 {
                                     HoTen = user.HoTen,
                                     DoB = user.DoB,
                                     DiaChiThuongTru = user.DiaChiThuongTru,
                                     SoCCCD = user.SoCCCD,
                                     NgayCap = user.NgayCap,
                                     NoiCap = user.NoiCap,
                                     Ma_CoQuan_DonVi = cq.Ten,
                                     Ma_ChucVu_ChucDanh = cv.Ten_ChucVu_ChucDanh,
                                     Ma_CanBo = user.Ma_CanBo,
                                     TenChucDanh = cv.Ten_ChucVu_ChucDanh,
                                     TenDonVi = cq.Ten,
                                     TenLoaiCoQuan = lcq.Ten_Loai_CQDV,
                                     Ma_Loai_CQDV = lcq.Ma_Loai_CQDV

                                 }).Single();

            var ThongTinThanNhan = (from tn in db.DM_CanBo_ThanNhan
                                    where tn.Ma_CanBo == bankekhai.Ma_CanBo

                                    select new
                                    {
                                        HoTenThanNhan = tn.HoTen,
                                        DoBTN = tn.DoB,
                                        DiaChiThuongTruTN = tn.DiaChiThuongTru,
                                        SoCCCDTN = tn.SoCCCD,
                                        NgayCapTN = tn.NgayCap,
                                        NoiCapTN = tn.NoiCap,
                                        VaiTroThanNhan = tn.VaiTroThanNhan,
                                        NgheNghiep = tn.NgheNghiep,
                                        NoiLamViec = tn.NoiLamViec,
                                    }).ToList();

            var dato = (from qsdt in db.NV_KeKhai_QuyenSuDungDat
                        where qsdt.Ma_KeKhai_TSTN == bankekhai.Ma_KeKhai_TSTN && qsdt.LoaiDat == "Đất Ở"
                        select qsdt);

            var datkhac = (from qsdt in db.NV_KeKhai_QuyenSuDungDat
                           where qsdt.Ma_KeKhai_TSTN == bankekhai.Ma_KeKhai_TSTN && qsdt.LoaiDat != "Đất Ở"
                           select qsdt);

            var nhao = (from no in db.NV_KeKhai_NhaO
                        where no.Ma_KeKhai_TSTN == bankekhai.Ma_KeKhai_TSTN
                        select no);

            var congtrinhxaydung = (from ctxd in db.NV_KeKhai_CongTrinh
                                    where ctxd.Ma_KeKhai_TSTN == bankekhai.Ma_KeKhai_TSTN
                                    select ctxd);
            var cophieu = (from cp in db.NV_KeKhai_TaiSanPhieu
                           where cp.Ma_KeKhai_TSTN == bankekhai.Ma_KeKhai_TSTN && cp.LoaiPhieu == "CoPhieu"
                           select cp);
            var traiphieu = (from cp in db.NV_KeKhai_TaiSanPhieu
                             where cp.Ma_KeKhai_TSTN == bankekhai.Ma_KeKhai_TSTN && cp.LoaiPhieu == "TraiPhieu"
                             select cp);
            var vongop = (from cp in db.NV_KeKhai_TaiSanPhieu
                          where cp.Ma_KeKhai_TSTN == bankekhai.Ma_KeKhai_TSTN && cp.LoaiPhieu == "VonGop"
                          select cp);
            var giayto = (from cp in db.NV_KeKhai_TaiSanPhieu
                          where cp.Ma_KeKhai_TSTN == bankekhai.Ma_KeKhai_TSTN && cp.LoaiPhieu == "GiayTo"
                          select cp);
            var taisankhac = (from ts in db.NV_KeKhai_TaiSanKhac
                              where ts.Ma_KeKhai_TSTN == bankekhai.Ma_KeKhai_TSTN && ts.LoaiTaiSanKhac == "TaiSanKhac"
                              select ts);
             var taisanPLQD = (from ts in db.NV_KeKhai_TaiSanKhac
                              where ts.Ma_KeKhai_TSTN == bankekhai.Ma_KeKhai_TSTN && ts.LoaiTaiSanKhac != "TaiSanKhac"
                              select ts);

            var giaydangky = (from ts in db.NV_KeKhai_TaiSanKhac
                              where ts.Ma_KeKhai_TSTN == bankekhai.Ma_KeKhai_TSTN && ts.LoaiTaiSanKhac == "GiayDangKy"
                              select ts);
            var taisannuocngoai = (from ts in db.NV_KeKhai_TaiSanNuocNgoai
                                   where ts.Ma_KeKhai_TSTN == bankekhai.Ma_KeKhai_TSTN
                                   select ts);
            var taikhoannuocngoai = (from ts in db.NV_KeKhai_TaiKhoanNuocNgoai
                                     where ts.Ma_KeKhai_TSTN == bankekhai.Ma_KeKhai_TSTN
                                     select ts);
            var tongthunhap = (from ts in db.NV_KeKhai_TongThuNhap
                               where ts.Ma_KeKhai_TSTN == bankekhai.Ma_KeKhai_TSTN
                               select ts).FirstOrDefault();

            var caylaunam = (from cln in db.NV_KeKhai_TaiSanGanVoiDat
                             where cln.Ma_KeKhai_TSTN == bankekhai.Ma_KeKhai_TSTN && cln.LoaiTaiSan == "cln"
                             select cln);

            var rungsanxuat = (from cln in db.NV_KeKhai_TaiSanGanVoiDat
                               where cln.Ma_KeKhai_TSTN == bankekhai.Ma_KeKhai_TSTN && cln.LoaiTaiSan == "rsx"
                               select cln);

            var vatkientruc = (from cln in db.NV_KeKhai_TaiSanGanVoiDat
                               where cln.Ma_KeKhai_TSTN == bankekhai.Ma_KeKhai_TSTN && cln.LoaiTaiSan == "vkt"
                               select cln);

            var kimloaidaquy = (from kldq in db.NV_KeKhai_TaiSanTrangSuc
                                where kldq.Ma_KeKhai_TSTN == bankekhai.Ma_KeKhai_TSTN
                                select kldq);

            var tien = (from t in db.NV_KeKhai_Tien
                        where t.Ma_KeKhai_TSTN == bankekhai.Ma_KeKhai_TSTN
                        select t);


            //tài sản nước ngoài
            var tsnndato = (from qsdt in db.NV_KeKhai_TaiSanNuocNgoai_QuyenSuDungDat
                            where qsdt.Ma_KeKhai_TSTN == bankekhai.Ma_KeKhai_TSTN && qsdt.LoaiDat_TSNN == "Đất Ở"
                            select qsdt);

            var tsnndatkhac = (from qsdt in db.NV_KeKhai_TaiSanNuocNgoai_QuyenSuDungDat
                               where qsdt.Ma_KeKhai_TSTN == bankekhai.Ma_KeKhai_TSTN && qsdt.LoaiDat_TSNN != "Đất Ở"
                               select qsdt);

            var tsnnnhao = (from no in db.NV_KeKhai_TaiSanNuocNgoai_NhaO
                            where no.Ma_KeKhai_TSTN == bankekhai.Ma_KeKhai_TSTN
                            select no);

            var tsnncongtrinhxaydung = (from ctxd in db.NV_KeKhai_TaiSanNuocNgoai_CongTrinh
                                        where ctxd.Ma_KeKhai_TSTN == bankekhai.Ma_KeKhai_TSTN
                                        select ctxd);
            var tsnncophieu = (from cp in db.NV_KeKhai_TaiSanNuocNgoai_TaiSanPhieu
                               where cp.Ma_KeKhai_TSTN == bankekhai.Ma_KeKhai_TSTN && cp.LoaiPhieu_TSNN == "CoPhieu"
                               select cp);
            var tsnntraiphieu = (from cp in db.NV_KeKhai_TaiSanNuocNgoai_TaiSanPhieu
                                 where cp.Ma_KeKhai_TSTN == bankekhai.Ma_KeKhai_TSTN && cp.LoaiPhieu_TSNN == "TraiPhieu"
                                 select cp);
            var tsnnvongop = (from cp in db.NV_KeKhai_TaiSanNuocNgoai_TaiSanPhieu
                              where cp.Ma_KeKhai_TSTN == bankekhai.Ma_KeKhai_TSTN && cp.LoaiPhieu_TSNN == "VonGop"
                              select cp);
            var tsnngiayto = (from cp in db.NV_KeKhai_TaiSanNuocNgoai_TaiSanPhieu
                              where cp.Ma_KeKhai_TSTN == bankekhai.Ma_KeKhai_TSTN && cp.LoaiPhieu_TSNN == "GiayTo"
                              select cp);
            var tsnntaisankhac = (from ts in db.NV_KeKhai_TaiSanNuocNgoai_TaiSanKhac
                                  where ts.Ma_KeKhai_TSTN == bankekhai.Ma_KeKhai_TSTN && ts.LoaiTaiSanKhac_TSNN == "TaiSanKhac"
                                  select ts);
            var tsnngiaydangky = (from ts in db.NV_KeKhai_TaiSanNuocNgoai_TaiSanKhac
                                  where ts.Ma_KeKhai_TSTN == bankekhai.Ma_KeKhai_TSTN && ts.LoaiTaiSanKhac_TSNN == "GiayDangKy"
                                  select ts);

            var tsnncaylaunam = (from cln in db.NV_KeKhai_TaiSanNuocNgoai_TaiSanGanVoiDat
                                 where cln.Ma_KeKhai_TSTN == bankekhai.Ma_KeKhai_TSTN && cln.LoaiTaiSan_TSNN == "cln"
                                 select cln);

            var tsnnrungsanxuat = (from cln in db.NV_KeKhai_TaiSanNuocNgoai_TaiSanGanVoiDat
                                   where cln.Ma_KeKhai_TSTN == bankekhai.Ma_KeKhai_TSTN && cln.LoaiTaiSan_TSNN == "rsx"
                                   select cln);

            var tsnnvatkientruc = (from cln in db.NV_KeKhai_TaiSanNuocNgoai_TaiSanGanVoiDat
                                   where cln.Ma_KeKhai_TSTN == bankekhai.Ma_KeKhai_TSTN && cln.LoaiTaiSan_TSNN == "vkt"
                                   select cln);

            var tsnnkimloaidaquy = (from kldq in db.NV_KeKhai_TaiSanNuocNgoai_TaiSanTrangSuc
                                    where kldq.Ma_KeKhai_TSTN == bankekhai.Ma_KeKhai_TSTN
                                    select kldq);

            var tsnntien = (from t in db.NV_KeKhai_TaiSanNuocNgoai_Tien
                            where t.Ma_KeKhai_TSTN == bankekhai.Ma_KeKhai_TSTN
                            select t);
            //End tài sản nước ngoài

            var biendongdato = (from cln in db.NV_KeKhai_BienDongTaiSan
                                where cln.Ma_KeKhai_TSTN == bankekhai.Ma_KeKhai_TSTN && cln.LoaiTSTN == "DatO"
                                select cln);
            var biendongdatkhac = (from cln in db.NV_KeKhai_BienDongTaiSan
                                   where cln.Ma_KeKhai_TSTN == bankekhai.Ma_KeKhai_TSTN && cln.LoaiTSTN == "DatKhac"
                                   select cln);
            var biendongnhaocongtrinh = (from cln in db.NV_KeKhai_BienDongTaiSan
                                         where cln.Ma_KeKhai_TSTN == bankekhai.Ma_KeKhai_TSTN && cln.LoaiTSTN == "NhaOCongTrinh"
                                         select cln);
            var biendongcongtrinhkhac = (from cln in db.NV_KeKhai_BienDongTaiSan
                                         where cln.Ma_KeKhai_TSTN == bankekhai.Ma_KeKhai_TSTN && cln.LoaiTSTN == "CongTrinhKhac"
                                         select cln);
            var biendongcaylaunam = (from cln in db.NV_KeKhai_BienDongTaiSan
                                     where cln.Ma_KeKhai_TSTN == bankekhai.Ma_KeKhai_TSTN && cln.LoaiTSTN == "CayLauNam"
                                     select cln);
            var biendongrung = (from cln in db.NV_KeKhai_BienDongTaiSan
                                where cln.Ma_KeKhai_TSTN == bankekhai.Ma_KeKhai_TSTN && cln.LoaiTSTN == "Rung"
                                select cln);
            var biendongvatkientruc = (from cln in db.NV_KeKhai_BienDongTaiSan
                                       where cln.Ma_KeKhai_TSTN == bankekhai.Ma_KeKhai_TSTN && cln.LoaiTSTN == "VatKienTruc"
                                       select cln);
            var biendongvangbacdaquy = (from cln in db.NV_KeKhai_BienDongTaiSan
                                        where cln.Ma_KeKhai_TSTN == bankekhai.Ma_KeKhai_TSTN && cln.LoaiTSTN == "VangBacDaQuy"
                                        select cln);
            var biendongtien = (from cln in db.NV_KeKhai_BienDongTaiSan
                                where cln.Ma_KeKhai_TSTN == bankekhai.Ma_KeKhai_TSTN && cln.LoaiTSTN == "Tien"
                                select cln);
            var biendongcophieu = (from cln in db.NV_KeKhai_BienDongTaiSan
                                   where cln.Ma_KeKhai_TSTN == bankekhai.Ma_KeKhai_TSTN && cln.LoaiTSTN == "CoPhieu"
                                   select cln);
            var biendongtraiphieu = (from cln in db.NV_KeKhai_BienDongTaiSan
                                     where cln.Ma_KeKhai_TSTN == bankekhai.Ma_KeKhai_TSTN && cln.LoaiTSTN == "TraiPhieu"
                                     select cln);

            var biendongvongop = (from cln in db.NV_KeKhai_BienDongTaiSan
                                  where cln.Ma_KeKhai_TSTN == bankekhai.Ma_KeKhai_TSTN && cln.LoaiTSTN == "VonGop"
                                  select cln);
            var biendonggiaytokhac = (from cln in db.NV_KeKhai_BienDongTaiSan
                                      where cln.Ma_KeKhai_TSTN == bankekhai.Ma_KeKhai_TSTN && cln.LoaiTSTN == "GiayToKhac"
                                      select cln);
            var biendongtaisanqdpl = (from cln in db.NV_KeKhai_BienDongTaiSan
                                      where cln.Ma_KeKhai_TSTN == bankekhai.Ma_KeKhai_TSTN && cln.LoaiTSTN == "TaiSanQDPL"
                                      select cln);
            var biendongtaisankhac = (from cln in db.NV_KeKhai_BienDongTaiSan
                                      where cln.Ma_KeKhai_TSTN == bankekhai.Ma_KeKhai_TSTN && cln.LoaiTSTN == "TaiSanKhac"
                                      select cln);

            var biendongtaisannuocngoai = (from cln in db.NV_KeKhai_BienDongTaiSan
                                           where cln.Ma_KeKhai_TSTN == bankekhai.Ma_KeKhai_TSTN && cln.LoaiTSTN == "TaiSanNuocNgoai"
                                           select cln);
            var biendongtaikhoannuocngoai = (from cln in db.NV_KeKhai_BienDongTaiSan
                                             where cln.Ma_KeKhai_TSTN == bankekhai.Ma_KeKhai_TSTN && cln.LoaiTSTN == "TaiKhoanNuocNgoai"
                                             select cln);
            var biendongthunhap = (from cln in db.NV_KeKhai_BienDongTaiSan
                                   where cln.Ma_KeKhai_TSTN == bankekhai.Ma_KeKhai_TSTN && cln.LoaiTSTN == "ThuNhap"
                                   select cln);




            var BanKeKhai = new
            {
                dato = biendongdato,
                datkhac = biendongdatkhac,
                nhaocongtrinh = biendongnhaocongtrinh,
                congtrinhkhac = biendongcongtrinhkhac,
                caylaunam = biendongcaylaunam,
                rung = biendongrung,
                vatkientruc = biendongvatkientruc,
                vangbacdaquy = biendongvangbacdaquy,
                tien = biendongtien,
                cophieu = biendongcophieu,
                traiphieu = biendongtraiphieu,
                vongop = biendongvongop,
                giaytokhac = biendonggiaytokhac,
                taisanqdpl = biendongtaisanqdpl,
                taisankhac = biendongtaisankhac,
                taisannuocngoai = biendongtaisannuocngoai,
                taikhoannuocngoai = biendongtaikhoannuocngoai,
                thunhap = biendongthunhap
            };

            var data2 = new
            {
                bankekhai = bankekhai,
                nguoikekhai = ThongTinCoBan,
                thannhan = ThongTinThanNhan,
                dato = dato,
                datkhac = datkhac,
                nhao = nhao,
                congtrinhxaydung = congtrinhxaydung,
                cophieu = cophieu,
                traiphieu = traiphieu,
                vongop = vongop,
                giayto = giayto,
                taisankhac = taisankhac,
                taisanPLQD = taisanPLQD,
                giaydangky = giaydangky,
                taisannuocngoai = taisannuocngoai,
                taikhoannuocngoai = taikhoannuocngoai,
                tongthunhap = tongthunhap,
                caylaunam = caylaunam,
                rungsanxuat = rungsanxuat,
                vatkientruc = vatkientruc,
                kimloaidaquy = kimloaidaquy,
                tien = tien,
                tsnndato = tsnndato,
                tsnndatkhac = tsnndatkhac,
                tsnnnhao = tsnnnhao,
                tsnncongtrinhxaydung = tsnncongtrinhxaydung,
                tsnncophieu = tsnncophieu,
                tsnntraiphieu = tsnntraiphieu,
                tsnnvongop = tsnnvongop,
                tsnngiayto = tsnngiayto,
                tsnntaisankhac = tsnntaisankhac,
                tsnngiaydangky = tsnngiaydangky,
                tsnncaylaunam = tsnncaylaunam,
                tsnnrungsanxuat = tsnnrungsanxuat,
                tsnnvatkientruc = tsnnvatkientruc,
                tsnnkimloaidaquy = tsnnkimloaidaquy,
                tsnntien = tsnntien,
                biendongtaisan = bankekhai,
                biendongtaisanqdpl = biendongtaisanqdpl,
            };
            var LoaiKeKhai = "";
            if(bankekhai.Ma_Loai_KeKhai == 4) {
                LoaiKeKhai = "HẰNG NĂM";
            }
            else if(bankekhai.Ma_Loai_KeKhai == 3)
            {
                LoaiKeKhai = "LẦN ĐẦU";
            }
            else if (bankekhai.Ma_Loai_KeKhai == 5)
            {
                LoaiKeKhai = "BỔ SUNG";
            }
            else if(bankekhai.Ma_Loai_KeKhai == 12)
            {
                LoaiKeKhai = "PHỤC VỤ CÔNG TÁC CÁN BỘ";
            }
            else
            {
                LoaiKeKhai = "..............";
            }
            var CoQuanTrucThuoc = "";
            var TenCoQuan = data2.nguoikekhai.TenDonVi;
            if (data2.nguoikekhai.Ma_Loai_CQDV == 10)
            {
                var CQTT = "";
                if(data2.nguoikekhai.TenDonVi.ToUpper().Contains("CAM RANH") || data2.nguoikekhai.TenDonVi.ToUpper().Contains("NHA TRANG"))
                {
                    CQTT = "THÀNH PHỐ";
                }
                else if(data2.nguoikekhai.TenDonVi.ToUpper().Contains("NINH HÒA"))
                {
                    CQTT = "THỊ XÃ";
                }
                else
                {
                    CQTT = "HUYỆN";
                }

                CoQuanTrucThuoc = $@"<p class=MsoNormal align=center style='margin-bottom:6.0pt;text-align:center;
                          line-height:150%;background:white'><b>ỦY BAN NHÂN DÂN {CQTT} {TenCoQuan.ToUpper()} <br>
                                                        ---------------</span></b></p>";
            }
            else
            {
                CoQuanTrucThuoc = $@"<p class=MsoNormal align=center style='margin-bottom:6.0pt;text-align:center;
                          line-height:150%;background:white'>UBND TỈNH KHÁNH HÒA</p><p class=MsoNormal align=center style='margin-bottom:6.0pt;text-align:center;
                          line-height:150%;background:white'><b>{TenCoQuan.ToUpper()}</b> <br>
                                                        ---------------</span></b></p>"; 
            }
            var TenLoaiCoQuan = $@" {data2.nguoikekhai.TenLoaiCoQuan}";
            
            var ThongTinCanBo = $@"<table class=MsoNormalTable border=0 cellspacing=0 cellpadding=0 style='border-collapse:collapse; padding:0; margin: 0;'>
                                    <tr>
                                        <td width='321' valign='top' style='width:240.5pt;padding:0; margin: 0;' >
     
                                                 <p class='MsoNormal' style='margin-bottom:6.0pt;line-height:150%' ><span lang=VI style = 'color:black' > -
          
                                                          </span><span style='color:black'>Họ và tên: </span><span lang = VI style='color:black'>" + data2.nguoikekhai.HoTen+ @".</span></p>
                                        </td>
                                        <td width='321' valign='top' style = 'width:240.55pt;padding:0; margin: 0;' >
  
                                              <p class='MsoNormal' style = 'margin-bottom:6.0pt;line-height:150%' ><span style='color:black'>Ngày tháng
                                                    năm sinh: </span><span lang = VI style='color:black'>" + data2.nguoikekhai.DoB + @".</span></p>
                                        </td>
                                    </tr>
                                </table>

                                <p class='MsoNormal' style='margin-bottom:6.0pt;line-height:150%' ><span style='color:black'>- Chức vụ/chức danh
                                        công tác: </span><span lang = VI style='color:black'>" + data2.nguoikekhai.TenChucDanh + @".</span></p>

                                <p class=MsoNormal style = 'margin-bottom:6.0pt;line-height:150%' ><span style='color:black'>- Cơ quan/đơn vị công
                                            tác: </span><span lang = VI style='color:black'>" + data2.nguoikekhai.TenDonVi +  @".</span></p>

                                <p class=MsoNormal style = 'margin-bottom:6.0pt;line-height:150%' ><span style='color:black'>- Nơi thường trú:
                                    </span><span lang = VI style='color:black'>" + data2.nguoikekhai.DiaChiThuongTru + @".</span></p>

                                <p class=MsoNormal style = 'margin-bottom:6.0pt;line-height:150%' ><span style='color:black'>- Số căn cước công dân
                                            hoặc giấy chứng minh nhân dân:
                                        " + data2.nguoikekhai.SoCCCD + @"</span><span lang = VI style='color:black'><br/> </span></p>
                                    <table class=MsoNormalTable border=0 cellspacing=0 cellpadding=0 width='100%'
                                        style='width:100.0%;border-collapse:collapse; padding:0; margin: 0;'>
                                           <tr>
                                            <td width='50%' valign=top style='width:50.0%;padding:0; margin: 0;'>
                                                <p class='MsoNormal' style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>ngày
                                            cấp: " + data2.nguoikekhai.NgayCap + @".</span></p>
                                            </td>
                                            <td width='50%' valign=top style='width:50.0%;padding:0; margin: 0;'>
                                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>nơi cấp: " + data2.nguoikekhai.NoiCap + @".</span></p>
                                            </td>
                                        </tr>
                                    </table>
                                  ";

            var VoChong = "";
            var Con = "";
            var demcon = 0;
            foreach ( var i in data2.thannhan)
            {
                if(i.VaiTroThanNhan == "Con")
                {
                    demcon++;
                    if(demcon ==1)
                    {
                        Con += @"<p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>3. Con chưa thành
                                            niên (con đẻ, con nuôi theo quy định của
                                            pháp luật)</span></p>";
                    }
                    Con += @"<p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>3." + demcon + @". Con thứ
                                            " + demcon + $@":</span></p>

                                <table class=MsoNormalTable border=0 cellspacing=0 cellpadding=0 width='100%'
                                    style='width:100.0%;border-collapse:collapse; padding:0; margin: 0;'>
                                    <tr>
                                        <td width='50%' valign=top style='width:50.0%;padding:0; margin: 0;'>
                                            <p class='MsoNormal' style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>- Họ và
                                                    tên:  {(i.HoTenThanNhan != null && i.HoTenThanNhan != "" ? i.HoTenThanNhan : "Không")} .</span></p>
                                        </td>
                                        <td width='50%' valign=top style='width:50.0%;padding:0; margin: 0;'>
                                            <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>Ngày tháng
                                                    năm sinh: {(i.DoBTN != null && i.DoBTN != "" ? i.DoBTN : "Không")}.</span></p>
                                        </td>
                                    </tr>
                                </table>

                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>- Nơi thường
                                        trú: {(i.DiaChiThuongTruTN != null && i.DiaChiThuongTruTN != "" ? i.DiaChiThuongTruTN : "Không")}.</span></p>

                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>- Số căn cước công dân
                                        hoặc giấy chứng minh nhân dân: {(i.SoCCCDTN != null && i.SoCCCDTN != "" ? i.SoCCCDTN : "Không")} <br/></p>
                                    <table class=MsoNormalTable border=0 cellspacing=0 cellpadding=0 width='100%'
                                        style='width:100.0%;border-collapse:collapse; padding:0; margin: 0;'>
                                        <tr>
                                            <td width='50%' valign=top style='width:50.0%;padding:0; margin: 0;'>
                                                <p class='MsoNormal' style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>ngày
                                            cấp: {(i.NgayCapTN != null && i.NgayCapTN != "" ? i.NgayCapTN : "Không")}.</span></p>
                                            </td>
                                            <td width='50%' valign=top style='width:50.0%;padding:0; margin: 0;'>
                                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>nơi cấp: {(i.NoiCapTN != null && i.NoiCapTN != "" ? i.NoiCapTN : "Không")}.</span></p>
                                            </td>
                                        </tr>
                                    </table>
                                ";
                }
                else
                {

                    VoChong = $@"<p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>2. Vợ hoặc chồng
                                        của người kê khai tài sản, thu nhập</span></p>
                            <table class=MsoNormalTable border=0 cellspacing=0 cellpadding=0 width='100%'
                                style='width:100.0%;border-collapse:collapse; padding:0; margin: 0;'>
                                <tr >
                                    <td width='50%' style='width:50.0%;padding:0; margin: 0;'>
                                        <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>- Họ và
                                                tên: </span><span lang=VI style='color:black'> {(i.HoTenThanNhan != null && i.HoTenThanNhan != "" ? i.HoTenThanNhan : "Không")}.</span></p>
                                    </td>
                                    <td width='50%' style='width:50.0%;padding:0; margin: 0;'>
                                        <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>Ngày tháng
                                                năm sinh: </span><span lang=VI style='color:black'> {(i.DoBTN != null && i.DoBTN != "" ? i.DoBTN : "Không")}. </span></p>
                                    </td>
                                </tr>
                            </table>
                            <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>- Nghề nghiệp:
                                </span><span lang=VI style='color:black'> {(i.NgheNghiep != null && i.NgheNghiep != "" ? i.NgheNghiep : "Không")}.</span></p>

                            <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>- Nơi làm
                                    việc: </span><span lang=VI style='color:black'> {(i.NoiLamViec != null && i.NoiLamViec != "" ? i.NoiLamViec : "Không")}.</span></p>

                            < p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>- Nơi thường trú:
                                </span><span lang=VI style='color:black'> {(i.DiaChiThuongTruTN != null && i.DiaChiThuongTruTN != "" ? i.DiaChiThuongTruTN : "Không")}.</span></p>

                            < p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>- Số căn cước công dân
                                    hoặc giấy chứng minh nhân dân: </span><span lang=VI style='color:black'> {(i.SoCCCDTN != null && i.SoCCCDTN != "" ? i.SoCCCDTN : "Không")}</span><br/>
                                    <table class=MsoNormalTable border=0 cellspacing=0 cellpadding=0 width='100%'
                                        style='width:100.0%;border-collapse:collapse; padding:0; margin: 0;'>
                                        <tr>
                                            <td width='50%' valign=top style='width:50.0%;padding:0; margin: 0;'>
                                                <p class='MsoNormal' style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>ngày
                                            cấp: {(i.NgayCapTN != null && i.NgayCapTN != "" ? i.NgayCapTN : "Không")}.</span></p>
                                            </td>
                                            <td width='50%' valign=top style='width:50.0%;padding:0; margin: 0;'>
                                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>nơi cấp: {(i.NoiCapTN != null && i.NoiCapTN != "" ? i.NoiCapTN : "Không")}.</span></p>
                                            </td>
                                        </tr>
                                    </table>";
                }
            }

            if(Con == "")
            {
                Con += @"<p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>3. Con chưa thành
                                            niên (con đẻ, con nuôi theo quy định của
                                            pháp luật)</span></p>";
                Con += @"<p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>3.1. Con thứ 1:</span></p>

                                <table class=MsoNormalTable border=0 cellspacing=0 cellpadding=0 width='100%'
                                    style='width:100.0%;border-collapse:collapse; padding:0; margin: 0;'>
                                    <tr>
                                        <td width='50%' valign=top style='width:50.0%;padding:0; margin: 0;'>
                                            <p class='MsoNormal' style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>- Họ và
                                                    tên: Không.</span></p>
                                        </td>
                                        <td width='50%' valign=top style='width:50.0%;padding:0; margin: 0;'>
                                            <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>Ngày tháng
                                                    năm sinh: Không.</span></p>
                                        </td>
                                    </tr>
                                </table>

                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>- Nơi thường
                                        trú: Không.</span></p>

                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>- Số căn cước công dân
                                        hoặc giấy chứng minh nhân dân: Không <br/></p>
                                    <table class=MsoNormalTable border=0 cellspacing=0 cellpadding=0 width='100%'
                                        style='width:100.0%;border-collapse:collapse; padding:0; margin: 0;'>
                                        <tr>
                                            <td width='50%' valign=top style='width:50.0%;padding:0; margin: 0;'>
                                                <p class='MsoNormal' style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>ngày
                                            cấp: Không.</span></p>
                                            </td>
                                            <td width='50%' valign=top style='width:50.0%;padding:0; margin: 0;'>
                                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>nơi cấp: Không.</span></p>
                                            </td>
                                        </tr>
                                    </table>
                                ";
            }

            if(VoChong == "")
            {
                VoChong = @"<p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>2. Vợ hoặc chồng
                                        của người kê khai tài sản, thu nhập</span></p>
                            <table class=MsoNormalTable border=0 cellspacing=0 cellpadding=0 width='100%'
                                style='width:100.0%;border-collapse:collapse; padding:0; margin: 0;'>
                                <tr >
                                    <td width='50%' style='width:50.0%;padding:0; margin: 0;'>
                                        <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>- Họ và
                                                tên: </span><span lang=VI style='color:black'> Không.</span></p>
                                    </td>
                                    <td width='50%' style='width:50.0%;padding:0; margin: 0;'>
                                        <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>Ngày tháng
                                                năm sinh: </span><span lang=VI style='color:black'>Không. </span></p>
                                    </td>
                                </tr>
                            </table>
                            <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>- Nghề nghiệp:
                                </span><span lang=VI style='color:black'>Không.</span></p>

                            <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>- Nơi làm
                                    việc: </span><span lang=VI style='color:black'>Không.</span></p>

                            <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>- Nơi thường trú:
                                </span><span lang=VI style='color:black'>Không.</span></p>

                            <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>- Số căn cước công dân
                                    hoặc giấy chứng minh nhân dân: </span><span lang=VI style='color:black'>Không</span><br/>
                                    <table class=MsoNormalTable border=0 cellspacing=0 cellpadding=0 width='100%'
                                        style='width:100.0%;border-collapse:collapse; padding:0; margin: 0;'>
                                        <tr>
                                            <td width='50%' valign=top style='width:50.0%;padding:0; margin: 0;'>
                                                <p class='MsoNormal' style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>ngày
                                            cấp: Không.</span></p>
                                            </td>
                                            <td width='50%' valign=top style='width:50.0%;padding:0; margin: 0;'>
                                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>nơi cấp: Không.</span></p>
                                            </td>
                                        </tr>
                                    </table>";
            }

            var DatO = "";
            var DemDatO = 0;
            foreach(var i in data2.dato)
            {
                DemDatO++;

                i.DiaChi = i.DiaChi != null && i.DiaChi != "" ? i.DiaChi : "Không";
                i.DienTich = i.DienTich != null && i.DienTich != "" ? $@"{i.DienTich} m<sup>2</sup>" : "Không";
                i.GiaTri = i.GiaTri != null && i.GiaTri !=  "" ? $@"{i.GiaTri} VNĐ" : "Không";
                i.GiayChungNhan = i.GiayChungNhan != null && i.GiayChungNhan != "" ? $@"{i.GiayChungNhan}" : "Không";
                i.ThongTinKhac = i.ThongTinKhac != null  && i.ThongTinKhac != "" ? $@"{i.ThongTinKhac}" : "Không";

                DatO += $@"<p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>1.1.{DemDatO}. Thửa thứ
                                            {DemDatO}:</span ></p>
                                <p class=MsoNormal style = 'margin-bottom:6.0pt;line-height:150%' ><span style='color:black'>- Địa
                                             chỉ: </span><span lang = VI
                                        style= 'color:black' > {i.DiaChi}.</span></p>

                                <p class=MsoNormal style = 'margin-bottom:6.0pt;line-height:150%' ><span style='color:black'>- Diện
                                            tích: </span><span lang = VI style='color:black'>{i.DienTich}.</span></p>

                                <p class=MsoNormal style = 'margin-bottom:6.0pt;line-height:150%' ><span style='color:black'>- Giá
                                            trị: </span><span lang = VI
                                        style='color:black'>{i.GiaTri}.</span></p>

                                <p class=MsoNormal style = 'margin-bottom:6.0pt;line-height:150%' ><span style='color:black'>- Giấy chứng nhận
                                            quyền sử dụng</span><span lang = VI style='color:black'>: 
                                            {i.GiayChungNhan}.</span></p>

                                <p class=MsoNormal style = 'margin-bottom:6.0pt;line-height:150%' ><span style='color:black'>- Thông tin khác(nếu
                                            có): </span><span lang = VI
                                        style='color:black'>{i.ThongTinKhac}.</span></p>
                                    ";
            }
            if (DatO == "") {
                DatO += $@"<p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>1.1.1. Thửa thứ
                                            1:</span ></p>

                                <p class=MsoNormal style = 'margin-bottom:6.0pt;line-height:150%' ><span style='color:black'>- Địa
                                             chỉ: </span><span lang = VI
                                        style= 'color:black' > Không.</span></p>

                                <p class=MsoNormal style = 'margin-bottom:6.0pt;line-height:150%' ><span style='color:black'>- Diện
                                            tích: </span><span lang = VI style='color:black'>Không.</span></p>

                                <p class=MsoNormal style = 'margin-bottom:6.0pt;line-height:150%' ><span style='color:black'>- Giá
                                            trị: </span><span lang = VI
                                        style='color:black'>Không.</span></p>

                                <p class=MsoNormal style = 'margin-bottom:6.0pt;line-height:150%' ><span style='color:black'>- Giấy chứng nhận
                                            quyền sử dụng</span><span lang = VI style='color:black'>: 
                                            Không.</span></p>

                                <p class=MsoNormal style = 'margin-bottom:6.0pt;line-height:150%' ><span style='color:black'>- Thông tin khác(nếu
                                            có): </span><span lang = VI
                                        style='color:black'>Không.</span></p>
                                    ";
            }

            var CacLoaiDatKhac = "";
            var DemCacLoaiDatKhac = 0;
            foreach(var i in data2.datkhac)
            {
                
                DemCacLoaiDatKhac++;
                CacLoaiDatKhac += $@"<p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>1.2.{DemCacLoaiDatKhac}. Thửa thứ
                                            {DemCacLoaiDatKhac}:</span ></p>
                                 <p class=MsoNormal style = 'margin-bottom:6.0pt;line-height:150%' ><span style='color:black'>- Loại Đất: </span><span lang = VI
                                        style= 'color:black' > {(i.TenLoaiDat != null && i.TenLoaiDat != "" ? i.TenLoaiDat : "Không")}.</span></p>
                                <p class=MsoNormal style = 'margin-bottom:6.0pt;line-height:150%' ><span style='color:black'>- Địa
                                             chỉ: </span><span lang = VI
                                        style= 'color:black' > {(i.DiaChi != null && i.DiaChi != "" ? i.DiaChi : "Không")}.</span></p>

                                <p class=MsoNormal style = 'margin-bottom:6.0pt;line-height:150%'></span><span lang = VI style='color:black'><span style='color:black'>- Diện tích: </span><span lang = VI
                                        style= 'color:black' > {(i.DienTich != null && i.DienTich != "" ? $@"{i.DienTich} m<sup>2</sup>" : "Không")}.</span></p>

                                <p class=MsoNormal style = 'margin-bottom:6.0pt;line-height:150%' ><span style='color:black'>- Giá
                                            trị: </span><span lang = VI
                                        style='color:black'>{ (i.GiaTri != null && i.GiaTri != "" ? $@"{i.GiaTri} VNĐ" : "Không")}.</span></p>

                                <p class=MsoNormal style = 'margin-bottom:6.0pt;line-height:150%' ><span style='color:black'>- Giấy chứng nhận
                                            quyền sử dụng</span><span lang = VI style='color:black'>: 
                                            {(i.GiayChungNhan != null && i.GiayChungNhan != "" ? $@"{i.GiayChungNhan}" : "Không")}.</span></p>

                                <p class=MsoNormal style = 'margin-bottom:6.0pt;line-height:150%' ><span style='color:black'>- Thông tin khác(nếu
                                            có): </span><span lang = VI
                                        style='color:black'>{(i.ThongTinKhac != null && i.ThongTinKhac != "" ? $@"{i.ThongTinKhac}" : "Không")}.</span></p>
                                    ";
            }
            if(CacLoaiDatKhac == "")
            {
                CacLoaiDatKhac += $@"<p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>1.2.1. Thửa thứ
                                            1:</span ></p>
                                <p class=MsoNormal style = 'margin-bottom:6.0pt;line-height:150%' ><span style='color:black'>- Loại đất: </span><span lang = VI
                                        style= 'color:black' > Không.</span></p>
                                <p class=MsoNormal style = 'margin-bottom:6.0pt;line-height:150%' ><span style='color:black'>- Địa
                                             chỉ: </span><span lang = VI
                                        style= 'color:black' > Không.</span></p>

                                <p class=MsoNormal style = 'margin-bottom:6.0pt;line-height:150%'></span><span lang = VI style='color:black'><span style='color:black'>- Diện tích: </span><span lang = VI
                                        style= 'color:black' > Không.</span></p>

                                <p class=MsoNormal style = 'margin-bottom:6.0pt;line-height:150%' ><span style='color:black'>- Giá
                                            trị: </span><span lang = VI
                                        style='color:black'>Không.</span></p>

                                <p class=MsoNormal style = 'margin-bottom:6.0pt;line-height:150%' ><span style='color:black'>- Giấy chứng nhận
                                            quyền sử dụng</span><span lang = VI style='color:black'>: 
                                            Không.</span></p>

                                <p class=MsoNormal style = 'margin-bottom:6.0pt;line-height:150%' ><span style='color:black'>- Thông tin khác(nếu
                                            có): </span><span lang = VI
                                        style='color:black'>Không.</span></p>
                                    ";
            }

            var NhaO = "";
            var DemNha = 0;
            foreach(var i in data2.nhao)
            {
               
                DemNha++;
                NhaO += $@"<p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>2.1.{DemNha}. Nhà thứ
                                            {DemNha}</span><span style='color:black'>: </span></p>

                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>- Địa chỉ: {(i.DiaChi != null && i.DiaChi != "" ? $@"{i.DiaChi}" : "Không")}.</span></p>

                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>- Loại nhà: {(i.LoaiNha != null && i.LoaiNha != "" ? $@"{i.LoaiNha}" : "Không")}.</span></p>

                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>- Diện tích sử dụng:
                                        </span><span lang=VI style='color:black'>{(i.DienTichSuDung != null && i.DienTichSuDung != "" ? $@"{i.DienTichSuDung} m<sup>2</sup>" : "Không")} .</span></p>

                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>- Giá
                                        trị: </span><span lang=VI
                                        style='color:black'> {(i.GiaTri != null && i.GiaTri != "" ? $@"{i.GiaTri} VNĐ" : "Không")}.</span></p>

                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>- Giấy chứng nhận
                                        quyền sở hữu: {(i.GiayChungNhan != null && i.GiayChungNhan != "" ? $@"{i.GiayChungNhan}" : "Không")}.</span></p>

                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>- Thông tin khác (nếu
                                        có): {(i.ThongTinKhac != null && i.ThongTinKhac != "" ? $@"{i.ThongTinKhac}" : "Không")}.</span></p>";
            }
            if(NhaO == "")
            {
                NhaO += $@"<p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>2.1.1. Nhà thứ
                                            1</span><span style='color:black'>: </span></p>

                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>- Địa chỉ: Không.</span></p>

                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>- Loại nhà: Không.</span></p>

                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>- Diện tích sử dụng:
                                        </span><span lang=VI style='color:black'> Không.</span></p>

                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>- Giá
                                        trị: </span><span lang=VI
                                        style='color:black'> Không.</span></p>

                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>- Giấy chứng nhận
                                        quyền sở hữu: Không.</span></p>

                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>- Thông tin khác (nếu
                                        có): Không.</span></p>";
            }

            var CongTrinhXayDung = "";
            var DemCongTrinhXayDung = 0;
            foreach(var i in data2.congtrinhxaydung)
            {
               
                DemCongTrinhXayDung++;
                CongTrinhXayDung += $@"<p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>2.2.{DemCongTrinhXayDung}. Công trình
                                            thứ {DemCongTrinhXayDung}</span><span style='color:black'>:</span></p>

                                <table class=MsoNormalTable border=0 cellspacing=0 cellpadding=0 width='100%'
                                    style='width:100.0%;border-collapse:collapse; padding:0; margin: 0;'>
                                    <tr>
                                        <td width='50%' valign=top style='width:50.0%;padding:0; margin: 0;'>
                                            <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>- Tên công
                                                    trình:</span><span lang=VI style='color:black'> {(i.TenCongTrinh != null && i.TenCongTrinh != "" ? $@"{i.TenCongTrinh}" : "Không")}.</span></p>
                                        </td>
                                        <td width='50%' valign=top style='width:50.0%;padding:0; margin: 0;'>
                                            <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>Địa chỉ:
                                                    {(i.DiaChi != null && i.DiaChi != "" ? $@"{i.DiaChi}" : "Không")}.</span></p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width='50%' valign=top style='width:50.0%;padding:0; margin: 0;'>
                                            <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>- Loại
                                                    công trình: {(i.LoaiCongTrinh != null && i.LoaiCongTrinh != "" ? $@"{i.LoaiCongTrinh}" : "Không")}.</span></p>
                                        </td>
                                        <td width='50%' valign=top style='width:50.0%;padding:0; margin: 0;'>
                                            <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>Cấp công
                                                    trình: {(i.CapCongTrinh != null && i.CapCongTrinh != "" ? $@"{i.CapCongTrinh}" : "Không")}.</span></p>
                                        </td>
                                    </tr>
                                </table>

                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>- Diện tích: {(i.DienTich != null && i.DienTich != "" ? $@"{i.DienTich} m<sup>2</sup>" : "Không")}.</span>
                                </p>

                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>- Giá trị: {(i.GiaTri != null && i.GiaTri != "" ? $@"{i.GiaTri} VNĐ" : "Không")}.</span></p>

                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>- Giấy chứng nhận
                                        quyền sở hữu: </span><span lang=VI style='color:black'> {(i.GiayChungNhan != null && i.GiayChungNhan != "" ? $@"{i.GiayChungNhan}" : "Không")}.</span></p>

                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>- Thông tin khác (nếu
                                        có): {(i.ThongTinKhac != null && i.ThongTinKhac != "" ? $@"{i.ThongTinKhac}" : "Không")}.</span></p>
                                ";
            }
            if(CongTrinhXayDung == "")
            {
                CongTrinhXayDung += $@"<p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>2.2.1. Công trình
                                            thứ 1</span><span style='color:black'>:</span></p>

                                <table class=MsoNormalTable border=0 cellspacing=0 cellpadding=0 width='100%'
                                    style='width:100.0%;border-collapse:collapse; padding:0; margin: 0;'>
                                    <tr>
                                        <td width='50%' valign=top style='width:50.0%;padding:0; margin: 0;'>
                                            <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>- Tên công
                                                    trình:</span><span lang=VI style='color:black'> Không.</span></p>
                                        </td>
                                        <td width='50%' valign=top style='width:50.0%;padding:0; margin: 0;'>
                                            <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>Địa chỉ:
                                                    Không.</span></p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width='50%' valign=top style='width:50.0%;padding:0; margin: 0;'>
                                            <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>- Loại
                                                    công trình: Không.</span></p>
                                        </td>
                                        <td width='50%' valign=top style='width:50.0%;padding:0; margin: 0;'>
                                            <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>Cấp công
                                                    trình: Không.</span></p>
                                        </td>
                                    </tr>
                                </table>

                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>- Diện tích: Không.</span>
                                </p>

                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>- Giá trị: Không</span></p>

                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>- Giấy chứng nhận
                                        quyền sở hữu: </span><span lang=VI style='color:black'> Không.</span></p>

                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>- Thông tin khác (nếu
                                        có): Không.</span></p>
                                ";
            }

            var CayLauNam = "";
            foreach(var i in data2.caylaunam)
            {
                
                CayLauNam += $@"<tr>
                                    <td width='33%' valign=top style='width:33.34%;padding:0; margin: 0;'>
                                        <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>- Loại
                                                cây: </span> {(i.TenTaiSan != null && i.TenTaiSan != "" ? $@"{i.TenTaiSan}" : "Không")}.</p>
                                    </td>
                                    <td width='33%' valign=top style='width:33.34%;padding:0; margin: 0;'>
                                        <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>Số
                                                lượng: </span> {(i.SoLuong_DienTich != null && i.SoLuong_DienTich != "" ? $@"{i.SoLuong_DienTich}" : "Không")}.</p>
                                    </td>
                                    <td width='33%' valign=top style='width:33.34%;padding:0; margin: 0;'>
                                        <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>Giá
                                                trị: </span> {(i.GiaTri != null && i.GiaTri != "" ? $@"{i.GiaTri} VNĐ" : "Không")}</p>
                                    </td>
                                </tr>";
            }
            if (CayLauNam == "")
            {
                CayLauNam += $@"<tr>
                                    <td width='33%' valign=top style='width:33.34%;padding:0; margin: 0;'>
                                        <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>- Loại
                                                cây: </span> Không.</p>
                                    </td>
                                    <td width='33%' valign=top style='width:33.34%;padding:0; margin: 0;'>
                                        <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>Số
                                                lượng: </span> Không.</p>
                                    </td>
                                    <td width='33%' valign=top style='width:33.34%;padding:0; margin: 0;'>
                                        <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>Giá
                                                trị: </span> Không</p>
                                    </td>
                                </tr>";
            }

            var RungSanXuat = "";
            foreach(var i in data2.rungsanxuat)
            {
                
               
                RungSanXuat += $@"<table class=MsoNormalTable border=0 cellspacing=0 cellpadding=0 width='100%'
                                    style='width:100.0%;border-collapse:collapse'>
                                    <tr>
                                        <td width='36%' valign=top style='width:36.98%;padding:0; margin: 0;'>
                                            <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>- Loại
                                                    rừng: </span> {(i.TenTaiSan != null && i.TenTaiSan != "" ? $@"{i.TenTaiSan}" : "Không")}.</p>
                                        </td>
                                        <td width='31%' valign=top style='width:31.52%;padding:0; margin: 0;'>
                                            <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>Diện
                                                    tích: </span> {(i.SoLuong_DienTich != null && i.SoLuong_DienTich != "" ? $@"{i.SoLuong_DienTich} m<sup>2</sup>" : "Không")}.</p>
                                        </td>
                                        <td width='31%' valign=top style='width:31.5%;padding:0; margin: 0;'>
                                            <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>Giá
                                                    trị: </span>{(i.GiaTri != null && i.GiaTri != "" ? $@"{i.GiaTri} VNĐ" : "Không")}.</p>
                                        </td>
                                    </tr>
                                </table>";
            }
            if(RungSanXuat == "")
            {
                RungSanXuat += $@"<table class=MsoNormalTable border=0 cellspacing=0 cellpadding=0 width='100%'
                                    style='width:100.0%;border-collapse:collapse'>
                                    <tr>
                                        <td width='36%' valign=top style='width:36.98%;padding:0; margin: 0;'>
                                            <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>- Loại
                                                    rừng: </span> Không.</p>
                                        </td>
                                        <td width='31%' valign=top style='width:31.52%;padding:0; margin: 0;'>
                                            <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>Diện
                                                    tích: </span> Không.</p>
                                        </td>
                                        <td width='31%' valign=top style='width:31.5%;padding:0; margin: 0;'>
                                            <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>Giá
                                                    trị: </span>Không.</p>
                                        </td>
                                    </tr>
                                </table>";
            }

            var TaiSanGanVoiDat = "";
            foreach(var i in data2.datkhac)
            {
               
                TaiSanGanVoiDat += $@"<table class=MsoNormalTable border=0 cellspacing=0 cellpadding=0 width='100%'
                                    style='width:100.0%;border-collapse:collapse'>
                                    <tr>
                                        <td width='36%' valign=top style='width:36.98%;padding:0; margin: 0;'>
                                            <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>- Loại
                                                    rừng: </span> {(i.TenLoaiDat != null && i.TenLoaiDat != "" ? $@"{i.TenLoaiDat}" : "Không")}.</p>
                                        </td>
                                        <td width='31%' valign=top style='width:31.52%;padding:0; margin: 0;'>
                                            <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>Diện
                                                    tích: </span> {(i.DienTich != null && i.DienTich != "" ? $@"{i.DienTich} m<sup>2</sup>" : "Không")}.</p>
                                        </td>
                                        <td width='31%' valign=top style='width:31.5%;padding:0; margin: 0;'>
                                            <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>Giá
                                                    trị: </span>{(i.GiaTri != null && i.GiaTri != "" ? $@"{i.GiaTri} VNĐ" : "Không")}</p>
                                        </td>
                                    </tr>
                                </table>";
            }
            if(TaiSanGanVoiDat == "")
            {
                TaiSanGanVoiDat += $@"<table class=MsoNormalTable border=0 cellspacing=0 cellpadding=0 width='100%'
                                    style='width:100.0%;border-collapse:collapse'>
                                    <tr>
                                        <td width='36%' valign=top style='width:36.98%;padding:0; margin: 0;'>
                                            <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>- Loại
                                                    rừng: </span> Không.</p>
                                        </td>
                                        <td width='31%' valign=top style='width:31.52%;padding:0; margin: 0;'>
                                            <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>Diện
                                                    tích: </span> Không.</p>
                                        </td>
                                        <td width='31%' valign=top style='width:31.5%;padding:0; margin: 0;'>
                                            <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>Giá
                                                    trị: </span>Không</p>
                                        </td>
                                    </tr>
                                </table>";
            }


            var VatKienTruc = "";

            foreach(var i in data2.vatkientruc)
            {
                VatKienTruc += $@"<table class=MsoNormalTable border=0 cellspacing=0 cellpadding=0 width='100%'
                                    style='width:100.0%;border-collapse:collapse'>
                                    <tr>
                                        <td width='33%' valign=top style='width:33.34%;padding:0; margin: 0;'>
                                            <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>- Tên
                                                    gọi: </span> {(i.TenTaiSan != null && i.TenTaiSan != "" ? $@"{i.TenTaiSan}" : "Không")}.</p>
                                        </td>
                                        <td width='33%' valign=top style='width:33.34%;padding:0; margin: 0;'>
                                            <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>Số
                                                    lượng: </span> {(i.SoLuong_DienTich != null && i.SoLuong_DienTich != "" ? $@"{i.SoLuong_DienTich}" : "Không")}.</p>
                                        </td>
                                        <td width='33%' valign=top style='width:33.34%;padding:0; margin: 0;'>
                                            <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>Giá
                                                    trị: {(i.GiaTri != null && i.GiaTri != "" ? $@"{i.GiaTri} VNĐ" : "Không")}.</span></p>
                                        </td>
                                    </tr>
                                </table>
                            ";
            }
            if (VatKienTruc == "")
            {
                VatKienTruc += $@"<table class=MsoNormalTable border=0 cellspacing=0 cellpadding=0 width='100%'
                                    style='width:100.0%;border-collapse:collapse'>
                                    <tr>
                                        <td width='33%' valign=top style='width:33.34%;padding:0; margin: 0;'>
                                            <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>- Tên
                                                    gọi: </span> Không.</p>
                                        </td>
                                        <td width='33%' valign=top style='width:33.34%;padding:0; margin: 0;'>
                                            <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>Số
                                                    lượng: </span> Không.</p>
                                        </td>
                                        <td width='33%' valign=top style='width:33.34%;padding:0; margin: 0;'>
                                            <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>Giá
                                                    trị: Không.</span></p>
                                        </td>
                                    </tr>
                                </table>
                            ";
            }

            var Vang = "";

            foreach(var i in data2.kimloaidaquy)
            {
               
                Vang += $@"<table class=MsoNormalTable border=0 cellspacing=0 cellpadding=0 width='100%'
                                    style='width:100.0%;border-collapse:collapse'>
                                    <tr>
                                        <td width='33%' valign=top style='width:33.34%;padding:0; margin: 0;'>
                                            <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>- Tên
                                                    gọi: </span> {(i.TenTrangSuc != null && i.TenTrangSuc != "" ? $@"{i.TenTrangSuc}" : "Không")}.</p>
                                        </td>
                                        <td width='33%' valign=top style='width:33.34%;padding:0; margin: 0;'>
                                            <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>Giá
                                                    trị: {(i.GiaTri != null && i.GiaTri != "" ? $@"{i.GiaTri} VNĐ" : "Không")}.</span></p>
                                        </td>
                                    </tr>
                                </table>
                            ";
            }
            if(Vang == "")
            {
                Vang += $@"<table class=MsoNormalTable border=0 cellspacing=0 cellpadding=0 width='100%'
                                    style='width:100.0%;border-collapse:collapse'>
                                    <tr>
                                        <td width='33%' valign=top style='width:33.34%;padding:0; margin: 0;'>
                                            <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>- Tên
                                                    gọi: </span> Không.</p>
                                        </td>
                                        <td width='33%' valign=top style='width:33.34%;padding:0; margin: 0;'>
                                            <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>Giá
                                                    trị: Không.</span></p>
                                        </td>
                                    </tr>
                                </table>
                            ";
            }

            var Tien = "";

            foreach(var i in data2.tien)
            {
                
                Tien += $@"<table class=MsoNormalTable border=0 cellspacing=0 cellpadding=0 width='100%'
                                    style='width:100.0%;border-collapse:collapse'>
                                    <tr>
                                        <td width='33%' valign=top style='width:33.34%;padding:0; margin: 0;'>
                                            <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>- Tên
                                                    gọi: </span> {(i.TenLoaiTien != null && i.TenLoaiTien != "" ? $@"{i.TenLoaiTien}" : "Không")}.</p>
                                        </td>
                                        <td width='33%' valign=top style='width:33.34%;padding:0; margin: 0;'>
                                            <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>Giá
                                                    trị: {(i.GiaTri != null && i.GiaTri != "" ? $@"{i.GiaTri} VNĐ" : "Không")}.</span></p>
                                        </td>
                                    </tr>
                                </table>
                            ";
            }
            if(Tien == "")
            {
                Tien += $@"<table class=MsoNormalTable border=0 cellspacing=0 cellpadding=0 width='100%'
                                    style='width:100.0%;border-collapse:collapse'>
                                    <tr>
                                        <td width='33%' valign=top style='width:33.34%;padding:0; margin: 0;'>
                                            <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>- Tên
                                                    gọi: </span> Không.</p>
                                        </td>
                                        <td width='33%' valign=top style='width:33.34%;padding:0; margin: 0;'>
                                            <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>Giá
                                                    trị: Không.</span></p>
                                        </td>
                                    </tr>
                                </table>
                            ";
            }

            var CoPhieu = "";

            foreach(var i in data2.cophieu)
            {
                CoPhieu += $@"<table class=MsoNormalTable border=0 cellspacing=0 cellpadding=0 width='100%'
                                    style='width:100.0%;border-collapse:collapse'>
                                    <tr>
                                        <td width='33%' valign=top style='width:33.34%;padding:0; margin: 0;'>
                                            <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>- Tên cổ
                                                    phiếu: </span> {(i.TenPhieu != null && i.TenPhieu != "" ? $@"{i.TenPhieu}" : "Không")}.</p>
                                        </td>
                                        <td width='33%' valign=top style='width:33.34%;padding:0; margin: 0;'>
                                            <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>Số
                                                    lượng: </span> {(i.SoLuong != null && i.SoLuong != "" ? $@"{i.SoLuong}" : "Không")}.</p>
                                        </td>
                                        <td width='33%' valign=top style='width:33.34%;padding:0; margin: 0;'>
                                            <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>Giá
                                                    trị: </span>{(i.GiaTri != null && i.GiaTri != "" ? $@"{i.GiaTri} VNĐ" : "Không")}.</p>
                                        </td>
                                    </tr>
                                </table>
                            ";
            }
            if(CoPhieu == "")
            {
                CoPhieu += $@"<table class=MsoNormalTable border=0 cellspacing=0 cellpadding=0 width='100%'
                                    style='width:100.0%;border-collapse:collapse'>
                                    <tr>
                                        <td width='33%' valign=top style='width:33.34%;padding:0; margin: 0;'>
                                            <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>- Tên cổ
                                                    phiếu: </span> Không.</p>
                                        </td>
                                        <td width='33%' valign=top style='width:33.34%;padding:0; margin: 0;'>
                                            <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>Số
                                                    lượng: </span> Không.</p>
                                        </td>
                                        <td width='33%' valign=top style='width:33.34%;padding:0; margin: 0;'>
                                            <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>Giá
                                                    trị: </span>Không.</p>
                                        </td>
                                    </tr>
                                </table>
                            ";
            }


            var TraiPhieu = "";

            foreach(var i in data2.traiphieu)
            {
                
                TraiPhieu += $@"<table class=MsoNormalTable border=0 cellspacing=0 cellpadding=0 width='100%'
                                    style='width:100.0%;border-collapse:collapse'>
                                    <tr>
                                        <td width='33%' valign=top style='width:33.34%;padding:0; margin: 0;'>
                                            <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>- Tên cổ
                                                    phiếu: </span>{(i.TenPhieu != null && i.TenPhieu != "" ? $@"{i.TenPhieu}" : "Không")}.</p>
                                        </td>
                                        <td width='33%' valign=top style='width:33.34%;padding:0; margin: 0;'>
                                            <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>Số
                                                    lượng: </span> {(i.SoLuong != null && i.SoLuong != "" ? $@"{i.SoLuong}" : "Không")}.</p>
                                        </td>
                                        <td width='33%' valign=top style='width:33.34%;padding:0; margin: 0;'>
                                            <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>Giá
                                                    trị: </span>{(i.GiaTri != null && i.GiaTri != "" ? $@"{i.GiaTri} VNĐ" : "Không")}.</p>
                                        </td>
                                    </tr>
                                </table>
                            ";
            }
            if(TraiPhieu == "")
            {
                TraiPhieu += $@"<table class=MsoNormalTable border=0 cellspacing=0 cellpadding=0 width='100%'
                                    style='width:100.0%;border-collapse:collapse'>
                                    <tr>
                                        <td width='33%' valign=top style='width:33.34%;padding:0; margin: 0;'>
                                            <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>- Tên cổ
                                                    phiếu: </span> Không.</p>
                                        </td>
                                        <td width='33%' valign=top style='width:33.34%;padding:0; margin: 0;'>
                                            <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>Số
                                                    lượng: </span> Không.</p>
                                        </td>
                                        <td width='33%' valign=top style='width:33.34%;padding:0; margin: 0;'>
                                            <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>Giá
                                                    trị: </span>Không.</p>
                                        </td>
                                    </tr>
                                </table>
                            ";
            }

            var VonGop = "";

            foreach(var i in data2.vongop)
            {
                
                VonGop += $@" <table class=MsoNormalTable border=0 cellspacing=0 cellpadding=0 width='100%'
                                    style='width:100.0%;border-collapse:collapse'>
                                    <tr>
                                        <td width='55%' valign=top style='width:55.64%;padding:0; margin: 0;'>
                                            <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>- Hình
                                                    thức góp vốn: </span> {(i.TenPhieu != null && i.TenPhieu != "" ? $@"{i.TenPhieu}" : "Không")}.</p>
                                        </td>
                                        <td width='44%' valign=top style='width:44.36%;padding:0; margin: 0;'>
                                            <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>Giá
                                                    trị: </span> {(i.GiaTri != null && i.GiaTri != "" ? $@"{i.GiaTri} VNĐ" : "Không")}.</p>
                                        </td>
                                    </tr>
                                </table>
                            ";
            }
            if (VonGop == "")
            {
                VonGop += $@" <table class=MsoNormalTable border=0 cellspacing=0 cellpadding=0 width='100%'
                                    style='width:100.0%;border-collapse:collapse'>
                                    <tr>
                                        <td width='55%' valign=top style='width:55.64%;padding:0; margin: 0;'>
                                            <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>- Hình
                                                    thức góp vốn: </span> Không.</p>
                                        </td>
                                        <td width='44%' valign=top style='width:44.36%;padding:0; margin: 0;'>
                                            <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>Giá
                                                    trị: </span>Không.</p>
                                        </td>
                                    </tr>
                                </table>
                            ";
            }

             var GiayToKhac = "";

            foreach(var i in data2.giayto)
            {
                
                GiayToKhac += $@" <table class=MsoNormalTable border=0 cellspacing=0 cellpadding=0 width='100%'
                                    style='width:100.0%;border-collapse:collapse'>
                                    <tr>
                                        <td width='57%' valign=top style='width:57.12%;padding:0; margin: 0;'>
                                            <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>- Tên giấy
                                                    tờ có giá: </span> {(i.TenPhieu != null && i.TenPhieu != "" ? $@"{i.TenPhieu}" : "Không")}.</p>
                                        </td>
                                        <td width='42%' valign=top style='width:42.88%;padding:0; margin: 0;'>
                                            <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>Giá trị: 
                                                </span>{(i.GiaTri != null && i.GiaTri != "" ? $@"{i.GiaTri} VNĐ" : "Không")}.</p>
                                        </td>
                                    </tr>
                                </table>
                            ";
            }
            if (GiayToKhac == "")
            {
                GiayToKhac += $@" <table class=MsoNormalTable border=0 cellspacing=0 cellpadding=0 width='100%'
                                    style='width:100.0%;border-collapse:collapse'>
                                    <tr>
                                        <td width='57%' valign=top style='width:57.12%;padding:0; margin: 0;'>
                                            <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>- Tên giấy
                                                    tờ có giá: </span> Không.</p>
                                        </td>
                                        <td width='42%' valign=top style='width:42.88%;padding:0; margin: 0;'>
                                            <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>Giá trị: 
                                                </span>Không.</p>
                                        </td>
                                    </tr>
                                </table>
                            ";
            }

            var TaiSanKhac = "";

            foreach(var i in data2.taisankhac)
            {
                if(i.LoaiTaiSanKhac == "TaiSanKhac")
                {
                    
                    TaiSanKhac += $@" <table class=MsoNormalTable border=0 cellspacing=0 cellpadding=0 width='100%'
                                    style='width:100.0%;border-collapse:collapse'>
                                    <tr>
                                        <td width='33%' valign=top style='width:33.34%;padding:0; margin: 0;'>
                                            <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>- Tên tài sản: </span> {(i.TenTaiSan != null && i.TenTaiSan != "" ? $@"{i.TenTaiSan}" : "Không")}.</p>
                                        </td>
                                        <td width='33%' valign=top style='width:33.34%;padding:0; margin: 0;'>
                                            <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>Năm bắt đầu sở hữu: </span> {(i.SoDangKy_NamBatDauSuDung != null && i.SoDangKy_NamBatDauSuDung != "" ? $@"{i.SoDangKy_NamBatDauSuDung}" : "Không")}.</p>
                                        </td>
                                        <td width='33%' valign=top style='width:33.34%;padding:0; margin: 0;'>
                                            <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>Giá
                                                    trị: </span>{(i.GiaTri != null && i.GiaTri != "" ? $@"{i.GiaTri} VNĐ" : "Không")}.</p>
                                        </td>
                                    </tr>
                                </table>
                            ";
                }
               
            }
            if (TaiSanKhac == "")
            {
                TaiSanKhac += $@" <table class=MsoNormalTable border=0 cellspacing=0 cellpadding=0 width='100%'
                                    style='width:100.0%;border-collapse:collapse'>
                                    <tr>
                                        <td width='33%' valign=top style='width:33.34%;padding:0; margin: 0;'>
                                            <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>- Tên tài sản: </span> Không.</p>
                                        </td>
                                        <td width='33%' valign=top style='width:33.34%;padding:0; margin: 0;'>
                                            <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>Năm bắt đầu sở hữu: </span> Không.</p>
                                        </td>
                                        <td width='33%' valign=top style='width:33.34%;padding:0; margin: 0;'>
                                            <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>Giá
                                                    trị: </span>Không.</p>
                                        </td>
                                    </tr>
                                </table>
                            ";
            }

            var TaiSanPhapLuatQuyDinh = "";

            foreach(var i in data2.taisanPLQD)
            {
                
                TaiSanPhapLuatQuyDinh += $@" <table class=MsoNormalTable border=0 cellspacing=0 cellpadding=0 width='100%'
                                style='width:100.0%;border-collapse:collapse'>
                                <tr>
                                    <td width='33%' valign=top style='width:33.34%;padding:0; margin: 0;'>
                                        <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>- Tên tài sản: </span> {(i.TenTaiSan != null && i.TenTaiSan != "" ? $@"{i.TenTaiSan}" : "Không")}.</p>
                                    </td>
                                    <td width='33%' valign=top style='width:33.34%;padding:0; margin: 0;'>
                                        <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>Số đăng kí: </span> {(i.SoDangKy_NamBatDauSuDung != null && i.SoDangKy_NamBatDauSuDung != "" ? $@"{i.SoDangKy_NamBatDauSuDung}" : "Không")}.</p>
                                    </td>
                                    <td width='33%' valign=top style='width:33.34%;padding:0; margin: 0;'>
                                        <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>Giá
                                                trị: </span>{(i.GiaTri != null && i.GiaTri != "" ? $@"{i.GiaTri} VNĐ" : "Không")}.</p>
                                    </td>
                                </tr>
                            </table>
                        ";
            }
            if(TaiSanPhapLuatQuyDinh == "")
            {
                TaiSanPhapLuatQuyDinh += $@" <table class=MsoNormalTable border=0 cellspacing=0 cellpadding=0 width='100%'
                                style='width:100.0%;border-collapse:collapse'>
                                <tr>
                                    <td width='33%' valign=top style='width:33.34%;padding:0; margin: 0;'>
                                        <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>- Tên tài sản: </span> Không.</p>
                                    </td>
                                    <td width='33%' valign=top style='width:33.34%;padding:0; margin: 0;'>
                                        <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>Số đăng kí: </span> Không.</p>
                                    </td>
                                    <td width='33%' valign=top style='width:33.34%;padding:0; margin: 0;'>
                                        <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>Giá
                                                trị: </span>Không.</p>
                                    </td>
                                </tr>
                            </table>
                        ";
            }

            //tài sản nước ngoài
            var tsnnDatO = "";
            var tsnnDemDatO = 0;
            foreach (var i in data2.tsnndato)
            {
                
                tsnnDemDatO++;
                tsnnDatO += $@"<p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>8.1.1.{tsnnDemDatO}. Thửa thứ
                                            {tsnnDemDatO}:</span ></p>

                                <p class=MsoNormal style = 'margin-bottom:6.0pt;line-height:150%' ><span style='color:black'>- Địa
                                             chỉ: </span><span lang = VI
                                        style= 'color:black' > {(i.DiaChi_TSNN != null && i.DiaChi_TSNN != "" ? $@"{i.DiaChi_TSNN}" : "Không")}.</span></p>

                                <p class=MsoNormal style = 'margin-bottom:6.0pt;line-height:150%' ><span style='color:black'>- Diện
                                            tích: </span><span lang = VI style='color:black'>{(i.DienTich_TSNN != null && i.DienTich_TSNN != "" ? $@"{i.DienTich_TSNN} m<sup>2</sup>" : "Không")} .</span></p>

                                <p class=MsoNormal style = 'margin-bottom:6.0pt;line-height:150%' ><span style='color:black'>- Giá
                                            trị: </span><span lang = VI
                                        style='color:black'>{(i.GiaTri_TSNN != null && i.GiaTri_TSNN != "" ? $@"{i.GiaTri_TSNN} VNĐ" : "Không")}.</span></p>

                                <p class=MsoNormal style = 'margin-bottom:6.0pt;line-height:150%' ><span style='color:black'>- Giấy chứng nhận
                                            quyền sử dụng</span><span lang = VI style='color:black'>: 
                                            {(i.GiayChungNhan_TSNN != null && i.GiayChungNhan_TSNN != "" ? $@"{i.GiayChungNhan_TSNN}" : "Không")}.</span></p>

                                <p class=MsoNormal style = 'margin-bottom:6.0pt;line-height:150%' ><span style='color:black'>- Thông tin khác(nếu
                                            có): </span><span lang = VI
                                        style='color:black'>{(i.ThongTinKhac_TSNN != null && i.ThongTinKhac_TSNN != "" ? $@"{i.ThongTinKhac_TSNN}" : "Không")}.</span></p>
                                    ";
            }
            if (tsnnDatO == "")
            {
                tsnnDatO += $@"<p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>8.1.1.1. Thửa thứ
                                            1:</span ></p>

                                <p class=MsoNormal style = 'margin-bottom:6.0pt;line-height:150%' ><span style='color:black'>- Địa
                                             chỉ: </span><span lang = VI
                                        style= 'color:black' > Không.</span></p>

                                <p class=MsoNormal style = 'margin-bottom:6.0pt;line-height:150%' ><span style='color:black'>- Diện
                                            tích: </span><span lang = VI style='color:black'>Không.</span></p>

                                <p class=MsoNormal style = 'margin-bottom:6.0pt;line-height:150%' ><span style='color:black'>- Giá
                                            trị: </span><span lang = VI
                                        style='color:black'>Không.</span></p>

                                <p class=MsoNormal style = 'margin-bottom:6.0pt;line-height:150%' ><span style='color:black'>- Giấy chứng nhận
                                            quyền sử dụng</span><span lang = VI style='color:black'>: 
                                           Không.</span></p>

                                <p class=MsoNormal style = 'margin-bottom:6.0pt;line-height:150%' ><span style='color:black'>- Thông tin khác(nếu
                                            có): </span><span lang = VI
                                        style='color:black'>Không.</span></p>
                                    ";
            }

            var tsnnCacLoaiDatKhac = "";
            var tsnnDemCacLoaiDatKhac = 0;
            foreach (var i in data2.tsnndatkhac)
            {
                tsnnDemCacLoaiDatKhac++;
                tsnnCacLoaiDatKhac += $@"<p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>8.1.2.{tsnnDemCacLoaiDatKhac}. Thửa thứ
                                            {tsnnDemCacLoaiDatKhac}:</span ></p>
                                 <p class=MsoNormal style = 'margin-bottom:6.0pt;line-height:150%' ><span style='color:black'>- Loại đất: </span><span lang = VI
                                        style= 'color:black' > {(i.TenLoaiDat_TSNN != null && i.TenLoaiDat_TSNN != "" ? $@"{i.TenLoaiDat_TSNN}" : "Không")}.</span></p>
                                <p class=MsoNormal style = 'margin-bottom:6.0pt;line-height:150%' ><span style='color:black'>- Địa
                                             chỉ: </span><span lang = VI
                                        style= 'color:black' > {(i.DiaChi_TSNN != null && i.DiaChi_TSNN != "" ? $@"{i.DiaChi_TSNN}" : "Không")}.</span></p>

                                <p class=MsoNormal style = 'margin-bottom:6.0pt;line-height:150%'><span lang = VI style='color:black'>- Diện tích: </span><span lang = VI
                                        style= 'color:black' >{(i.DienTich_TSNN != null && i.DienTich_TSNN != "" ? $@"{i.DienTich_TSNN} m<sup>2</sup>" : "Không")}.</span></p>

                                <p class=MsoNormal style = 'margin-bottom:6.0pt;line-height:150%' ><span style='color:black'>- Giá
                                            trị: </span><span lang = VI
                                        style='color:black'>{(i.GiaTri_TSNN != null && i.GiaTri_TSNN != "" ? $@"{i.GiaTri_TSNN} VNĐ" : "Không")}.</span></p>

                                <p class=MsoNormal style = 'margin-bottom:6.0pt;line-height:150%' ><span style='color:black'>- Giấy chứng nhận
                                            quyền sử dụng</span><span lang = VI style='color:black'>: 
                                            {(i.GiayChungNhan_TSNN != null && i.GiayChungNhan_TSNN != "" ? $@"{i.GiayChungNhan_TSNN}" : "Không")}.</span></p>

                                <p class=MsoNormal style = 'margin-bottom:6.0pt;line-height:150%' ><span style='color:black'>- Thông tin khác(nếu
                                            có): </span><span lang = VI
                                        style='color:black'>{(i.ThongTinKhac_TSNN != null && i.ThongTinKhac_TSNN != "" ? $@"{i.ThongTinKhac_TSNN}" : "Không")}.</span></p>
                                    ";
            }
            if (tsnnCacLoaiDatKhac == "")
            {
                tsnnCacLoaiDatKhac += $@"<p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>8.1.2.1. Thửa thứ
                                            1:</span ></p>
                                        <p class=MsoNormal style = 'margin-bottom:6.0pt;line-height:150%' ><span style='color:black'>- Loại đất: </span><span lang = VI
                                        style= 'color:black' > Không.</span></p>
                                <p class=MsoNormal style = 'margin-bottom:6.0pt;line-height:150%' ><span style='color:black'>- Địa
                                             chỉ: </span><span lang = VI
                                        style= 'color:black' > Không.</span></p>

                                <p class=MsoNormal style = 'margin-bottom:6.0pt;line-height:150%'></span><span lang = VI style='color:black'>- Diện tích: </span><span lang = VI
                                        style= 'color:black' >Không.</span></p>

                                <p class=MsoNormal style = 'margin-bottom:6.0pt;line-height:150%' ><span style='color:black'>- Giá
                                            trị: </span><span lang = VI
                                        style='color:black'>Không.</span></p>

                                <p class=MsoNormal style = 'margin-bottom:6.0pt;line-height:150%' ><span style='color:black'>- Giấy chứng nhận
                                            quyền sử dụng</span><span lang = VI style='color:black'>: 
                                            Không.</span></p>

                                <p class=MsoNormal style = 'margin-bottom:6.0pt;line-height:150%' ><span style='color:black'>- Thông tin khác(nếu
                                            có): </span><span lang = VI
                                        style='color:black'>Không.</span></p>
                                    ";
            }

            var tsnnNhaO = "";
            var tsnnDemNha = 0;
            foreach (var i in data2.tsnnnhao)
            {
                tsnnDemNha++;
                tsnnNhaO += $@"<p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>8.2.1.{tsnnDemNha}. Nhà thứ
                                            {tsnnDemNha}</span><span style='color:black'>: </span></p>

                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>- Địa chỉ: {(i.DiaChi_TSNN != null && i.DiaChi_TSNN != "" ? $@"{i.DiaChi_TSNN}" : "Không")}.</span></p>

                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>- Loại nhà: {(i.LoaiNha_TSNN != null && i.LoaiNha_TSNN != "" ? $@"{i.LoaiNha_TSNN}" : "Không")}.</span></p>

                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>- Diện tích sử dụng:
                                        </span><span lang=VI style='color:black'> {(i.DienTichSuDung_TSNN != null && i.DienTichSuDung_TSNN != "" ? $@"{i.DienTichSuDung_TSNN} m<sup>2</sup>" : "Không")}.</span></p>

                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>- Giá
                                        trị: </span><span lang=VI
                                        style='color:black'> {(i.GiaTri_TSNN != null && i.GiaTri_TSNN != "" ? $@"{i.GiaTri_TSNN} VNĐ" : "Không")}.</span></p>

                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>- Giấy chứng nhận
                                        quyền sở hữu: {(i.GiayChungNhan_TSNN != null && i.GiayChungNhan_TSNN != "" ? $@"{i.GiayChungNhan_TSNN}" : "Không")}.</span></p>

                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>- Thông tin khác (nếu
                                        có): {(i.ThongTinKhac_TSNN != null && i.ThongTinKhac_TSNN != "" ? $@"{i.ThongTinKhac_TSNN}" : "Không")}.</span></p>";
            }
            if (tsnnNhaO == "")
            {
                tsnnNhaO += $@"<p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>8.2.1.1. Nhà thứ
                                            1</span><span style='color:black'>: </span></p>

                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>- Địa chỉ: Không.</span></p>

                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>- Loại nhà: Không.</span></p>

                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>- Diện tích sử dụng
                                        </span><span lang=VI style='color:black'>: Không.</span></p>

                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>- Giá
                                        trị: </span><span lang=VI
                                        style='color:black'> Không.</span></p>

                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>- Giấy chứng nhận
                                        quyền sở hữu: Không.</span></p>

                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>- Thông tin khác (nếu
                                        có): Không.</span></p>";
            }

            var tsnnCongTrinhXayDung = "";
            var tsnnDemCongTrinhXayDung = 0;
            foreach (var i in data2.tsnncongtrinhxaydung)
            {
                tsnnDemCongTrinhXayDung++;
                tsnnCongTrinhXayDung += $@"<p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>8.2.2.{tsnnDemCongTrinhXayDung}. Công trình
                                            thứ {tsnnDemCongTrinhXayDung}</span><span style='color:black'>:</span></p>

                                <table class=MsoNormalTable border=0 cellspacing=0 cellpadding=0 width='100%'
                                    style='width:100.0%;border-collapse:collapse; padding:0; margin: 0;'>
                                    <tr>
                                        <td width='50%' valign=top style='width:50.0%;padding:0; margin: 0;'>
                                            <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>- Tên công
                                                    trình:</span><span lang=VI style='color:black'> {(i.TenCongTrinh_TSNN != null && i.TenCongTrinh_TSNN != "" ? $@"{i.TenCongTrinh_TSNN}" : "Không")}.</span></p>
                                        </td>
                                        <td width='50%' valign=top style='width:50.0%;padding:0; margin: 0;'>
                                            <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>Địa chỉ:
                                                    {(i.DiaChi_TSNN != null && i.DiaChi_TSNN != "" ? $@"{i.DiaChi_TSNN}" : "Không")}.</span></p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width='50%' valign=top style='width:50.0%;padding:0; margin: 0;'>
                                            <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>- Loại
                                                    công trình: {(i.LoaiCongTrinh_TSNN != null && i.LoaiCongTrinh_TSNN != "" ? $@"{i.LoaiCongTrinh_TSNN}" : "Không")}.</span></p>
                                        </td>
                                        <td width='50%' valign=top style='width:50.0%;padding:0; margin: 0;'>
                                            <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>Cấp công
                                                    trình: {(i.CapCongTrinh_TSNN != null && i.CapCongTrinh_TSNN != "" ? $@"{i.CapCongTrinh_TSNN}" : "Không")}.</span></p>
                                        </td>
                                    </tr>
                                </table>

                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>- Diện tích: {(i.DienTich_TSNN != null && i.DienTich_TSNN != "" ? $@"{i.DienTich_TSNN} m<sup>2</sup>" : "Không")} .</span>
                                </p>

                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>- Giá trị: {(i.GiaTri_TSNN != null && i.GiaTri_TSNN != "" ? $@"{i.GiaTri_TSNN} VNĐ" : "Không")}</span></p>

                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>- Giấy chứng nhận
                                        quyền sở hữu: </span><span lang=VI style='color:black'> {(i.GiayChungNhan_TSNN != null && i.GiayChungNhan_TSNN != "" ? $@"{i.GiayChungNhan_TSNN}" : "Không")}.</span></p>

                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>- Thông tin khác (nếu
                                        có): {(i.ThongTinKhac_TSNN != null && i.ThongTinKhac_TSNN != "" ? $@"{i.ThongTinKhac_TSNN}" : "Không")}.</span></p>
                                ";
            }
            if (tsnnCongTrinhXayDung == "") {
                tsnnCongTrinhXayDung += $@"<p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>8.2.2.1. Công trình
                                            thứ 1</span><span style='color:black'>:</span></p>

                                <table class=MsoNormalTable border=0 cellspacing=0 cellpadding=0 width='100%'
                                    style='width:100.0%;border-collapse:collapse; padding:0; margin: 0;'>
                                    <tr>
                                        <td width='50%' valign=top style='width:50.0%;padding:0; margin: 0;'>
                                            <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>- Tên công
                                                    trình:</span><span lang=VI style='color:black'> không.</span></p>
                                        </td>
                                        <td width='50%' valign=top style='width:50.0%;padding:0; margin: 0;'>
                                            <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>Địa chỉ:
                                                    không.</span></p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width='50%' valign=top style='width:50.0%;padding:0; margin: 0;'>
                                            <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>- Loại
                                                    công trình: không.</span></p>
                                        </td>
                                        <td width='50%' valign=top style='width:50.0%;padding:0; margin: 0;'>
                                            <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>Cấp công
                                                    trình: không.</span></p>
                                        </td>
                                    </tr>
                                </table>

                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>- Diện tích: không.</span>
                                </p>

                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>- Giá trị: không</span></p>

                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>- Giấy chứng nhận
                                        quyền sở hữu: </span><span lang=VI style='color:black'> không.</span></p>

                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>- Thông tin khác (nếu
                                        có): không.</span></p>
                                ";
            }


            var tsnnCayLauNam = "";
            foreach (var i in data2.tsnncaylaunam)
            {
                tsnnCayLauNam += $@"<tr>
                                    <td width='33%' valign=top style='width:33.34%;padding:0; margin: 0;'>
                                        <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>- Loại
                                                cây: </span> {i.TenTaiSan_TSNN}{(i.TenTaiSan_TSNN != null && i.TenTaiSan_TSNN != "" ? $@"{i.TenTaiSan_TSNN}" : "Không")}.</p>
                                    </td>
                                    <td width='33%' valign=top style='width:33.34%;padding:0; margin: 0;'>
                                        <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>Số
                                                lượng: </span> {(i.SoLuong_DienTich_TSNN != null && i.SoLuong_DienTich_TSNN != "" ? $@"{i.SoLuong_DienTich_TSNN}" : "Không")}.</p>
                                    </td>
                                    <td width='33%' valign=top style='width:33.34%;padding:0; margin: 0;'>
                                        <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>Giá
                                                trị: </span> {(i.GiaTri_TSNN != null && i.GiaTri_TSNN != "" ? $@"{i.GiaTri_TSNN} VNĐ" : "Không")}</p>
                                    </td>
                                </tr>";
            }
            if (tsnnCayLauNam == "")
            {
                tsnnCayLauNam += $@"<tr>
                                    <td width='33%' valign=top style='width:33.34%;padding:0; margin: 0;'>
                                        <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>- Loại
                                                cây: </span>Không.</p>
                                    </td>
                                    <td width='33%' valign=top style='width:33.34%;padding:0; margin: 0;'>
                                        <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>Số
                                                lượng: </span>Không.</p>
                                    </td>
                                    <td width='33%' valign=top style='width:33.34%;padding:0; margin: 0;'>
                                        <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>Giá
                                                trị: </span>Không</p>
                                    </td>
                                </tr>";
            }

            var tsnnRungSanXuat = "";
            foreach (var i in data2.tsnnrungsanxuat)
            {
                tsnnRungSanXuat += $@"<table class=MsoNormalTable border=0 cellspacing=0 cellpadding=0 width='100%'
                                    style='width:100.0%;border-collapse:collapse'>
                                    <tr>
                                        <td width='36%' valign=top style='width:36.98%;padding:0; margin: 0;'>
                                            <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>- Loại
                                                    rừng: </span> {(i.TenTaiSan_TSNN != null && i.TenTaiSan_TSNN != "" ? $@"{i.TenTaiSan_TSNN}" : "Không")}.</p>
                                        </td>
                                        <td width='31%' valign=top style='width:31.52%;padding:0; margin: 0;'>
                                            <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>Diện
                                                    tích: </span> {(i.SoLuong_DienTich_TSNN != null && i.SoLuong_DienTich_TSNN != "" ? $@"{i.SoLuong_DienTich_TSNN} m<sup>2</sup>" : "Không")}.</p>
                                        </td>
                                        <td width='31%' valign=top style='width:31.5%;padding:0; margin: 0;'>
                                            <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>Giá
                                                    trị: </span>{(i.GiaTri_TSNN != null && i.GiaTri_TSNN != "" ? $@"{i.GiaTri_TSNN} VNĐ" : "Không")}.</p>
                                        </td>
                                    </tr>
                                </table>";
            }
            if(tsnnRungSanXuat == "")
            {
                tsnnRungSanXuat += $@"<table class=MsoNormalTable border=0 cellspacing=0 cellpadding=0 width='100%'
                                    style='width:100.0%;border-collapse:collapse'>
                                    <tr>
                                        <td width='36%' valign=top style='width:36.98%;padding:0; margin: 0;'>
                                            <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>- Loại
                                                    rừng: </span> Không.</p>
                                        </td>
                                        <td width='31%' valign=top style='width:31.52%;padding:0; margin: 0;'>
                                            <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>Diện
                                                    tích: </span> Không.</p>
                                        </td>
                                        <td width='31%' valign=top style='width:31.5%;padding:0; margin: 0;'>
                                            <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>Giá
                                                    trị: </span>Không.</p>
                                        </td>
                                    </tr>
                                </table>";
            }

            var tsnnTaiSanGanVoiDat = "";
            foreach (var i in data2.tsnndatkhac)
            {
                tsnnTaiSanGanVoiDat += $@"<table class=MsoNormalTable border=0 cellspacing=0 cellpadding=0 width='100%'
                                    style='width:100.0%;border-collapse:collapse'>
                                    <tr>
                                        <td width='36%' valign=top style='width:36.98%;padding:0; margin: 0;'>
                                            <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>- Loại
                                                    rừng: </span>{(i.TenLoaiDat_TSNN != null && i.TenLoaiDat_TSNN != "" ? $@"{i.TenLoaiDat_TSNN}" : "Không")}.</p>
                                        </td>
                                        <td width='31%' valign=top style='width:31.52%;padding:0; margin: 0;'>
                                            <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>Diện
                                                    tích: </span> {(i.DienTich_TSNN != null && i.DienTich_TSNN != "" ? $@"{i.DienTich_TSNN} m<sup>2</sup>" : "Không")}.</p>
                                        </td>
                                        <td width='31%' valign=top style='width:31.5%;padding:0; margin: 0;'>
                                            <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>Giá
                                                    trị: </span>{(i.GiaTri_TSNN != null && i.GiaTri_TSNN != "" ? $@"{i.GiaTri_TSNN} VNĐ" : "Không")}</p>
                                        </td>
                                    </tr>
                                </table>";
            }

            if (tsnnTaiSanGanVoiDat == "")
            {
                tsnnTaiSanGanVoiDat += $@"<table class=MsoNormalTable border=0 cellspacing=0 cellpadding=0 width='100%'
                                    style='width:100.0%;border-collapse:collapse'>
                                    <tr>
                                        <td width='36%' valign=top style='width:36.98%;padding:0; margin: 0;'>
                                            <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>- Loại
                                                    rừng: </span>Không.</p>
                                        </td>
                                        <td width='31%' valign=top style='width:31.52%;padding:0; margin: 0;'>
                                            <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>Diện
                                                    tích: </span>Không.</p>
                                        </td>
                                        <td width='31%' valign=top style='width:31.5%;padding:0; margin: 0;'>
                                            <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>Giá
                                                    trị: </span>Không</p>
                                        </td>
                                    </tr>
                                </table>";
            }

            var tsnnVatKienTruc = "";

            foreach (var i in data2.tsnnvatkientruc)
            {
                tsnnVatKienTruc += $@"<table class=MsoNormalTable border=0 cellspacing=0 cellpadding=0 width='100%'
                                    style='width:100.0%;border-collapse:collapse'>
                                    <tr>
                                        <td width='33%' valign=top style='width:33.34%;padding:0; margin: 0;'>
                                            <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>- Tên
                                                    gọi: </span>{(i.TenTaiSan_TSNN != null && i.TenTaiSan_TSNN != "" ? $@"{i.TenTaiSan_TSNN}" : "Không")}.</p>
                                        </td>
                                        <td width='33%' valign=top style='width:33.34%;padding:0; margin: 0;'>
                                            <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>Số
                                                    lượng: </span>{i.SoLuong_DienTich_TSNN}{(i.SoLuong_DienTich_TSNN != null && i.SoLuong_DienTich_TSNN != "" ? $@"{i.SoLuong_DienTich_TSNN}" : "Không")}.</p>
                                        </td>
                                        <td width='33%' valign=top style='width:33.34%;padding:0; margin: 0;'>
                                            <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>Giá
                                                    trị: {(i.GiaTri_TSNN != null && i.GiaTri_TSNN != "" ? $@"{i.GiaTri_TSNN} VNĐ" : "Không")}.</span></p>
                                        </td>
                                    </tr>
                                </table>
                            ";
            }
            if (tsnnVatKienTruc == "")
            {
                tsnnVatKienTruc += $@"<table class=MsoNormalTable border=0 cellspacing=0 cellpadding=0 width='100%'
                                    style='width:100.0%;border-collapse:collapse'>
                                    <tr>
                                        <td width='33%' valign=top style='width:33.34%;padding:0; margin: 0;'>
                                            <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>- Tên
                                                    gọi: </span>Không.</p>
                                        </td>
                                        <td width='33%' valign=top style='width:33.34%;padding:0; margin: 0;'>
                                            <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>Số
                                                    lượng: </span>Không.</p>
                                        </td>
                                        <td width='33%' valign=top style='width:33.34%;padding:0; margin: 0;'>
                                            <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>Giá
                                                    trị: Không.</span></p>
                                        </td>
                                    </tr>
                                </table>
                            ";
            }

            var tsnnVang = "";

            foreach (var i in data2.tsnnkimloaidaquy)
            {
                tsnnVang += $@"<table class=MsoNormalTable border=0 cellspacing=0 cellpadding=0 width='100%'
                                    style='width:100.0%;border-collapse:collapse'>
                                    <tr>
                                        <td width='33%' valign=top style='width:33.34%;padding:0; margin: 0;'>
                                            <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>- Tên
                                                    gọi: </span> {(i.TenTrangSuc_TSNN != null && i.TenTrangSuc_TSNN != "" ? $@"{i.TenTrangSuc_TSNN}" : "Không")}.</p>
                                        </td>
                                        <td width='33%' valign=top style='width:33.34%;padding:0; margin: 0;'>
                                            <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>Giá
                                                    trị: {(i.GiaTri_TSNN != null && i.GiaTri_TSNN != "" ? $@"{i.GiaTri_TSNN} VNĐ" : "Không")}.</span></p>
                                        </td>
                                    </tr>
                                </table>
                            ";
            }
            if (tsnnVang == "")
            {
                tsnnVang += $@"<table class=MsoNormalTable border=0 cellspacing=0 cellpadding=0 width='100%'
                                    style='width:100.0%;border-collapse:collapse'>
                                    <tr>
                                        <td width='33%' valign=top style='width:33.34%;padding:0; margin: 0;'>
                                            <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>- Tên
                                                    gọi: </span>Không.</p>
                                        </td>
                                        <td width='33%' valign=top style='width:33.34%;padding:0; margin: 0;'>
                                            <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>Giá
                                                    trị: không.</span></p>
                                        </td>
                                    </tr>
                                </table>
                            ";
            }


            var tsnnTien = "";

            foreach (var i in data2.tsnntien)
            {
                tsnnTien += $@"<table class=MsoNormalTable border=0 cellspacing=0 cellpadding=0 width='100%'
                                    style='width:100.0%;border-collapse:collapse'>
                                    <tr>
                                        <td width='33%' valign=top style='width:33.34%;padding:0; margin: 0;'>
                                            <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>- Tên
                                                    gọi: </span> {(i.TenLoaiTien_TSNN != null && i.TenLoaiTien_TSNN != "" ? $@"{i.TenLoaiTien_TSNN}" : "Không")}.</p>
                                        </td>
                                        <td width='33%' valign=top style='width:33.34%;padding:0; margin: 0;'>
                                            <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>Giá
                                                    trị: {(i.GiaTri_TSNN != null && i.GiaTri_TSNN != "" ? $@"{i.GiaTri_TSNN} VNĐ" : "Không")}.</span></p>
                                        </td>
                                    </tr>
                                </table>
                            ";
            }
            if (tsnnTien == "")
            {
                tsnnTien += $@"<table class=MsoNormalTable border=0 cellspacing=0 cellpadding=0 width='100%'
                                    style='width:100.0%;border-collapse:collapse'>
                                    <tr>
                                        <td width='33%' valign=top style='width:33.34%;padding:0; margin: 0;'>
                                            <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>- Tên
                                                    gọi: </span> Không.</p>
                                        </td>
                                        <td width='33%' valign=top style='width:33.34%;padding:0; margin: 0;'>
                                            <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>Giá
                                                    trị: Không.</span></p>
                                        </td>
                                    </tr>
                                </table>
                            ";
            }

            var tsnnCoPhieu = "";

            foreach (var i in data2.tsnncophieu)
            {
                tsnnCoPhieu += $@"<table class=MsoNormalTable border=0 cellspacing=0 cellpadding=0 width='100%'
                                    style='width:100.0%;border-collapse:collapse'>
                                    <tr>
                                        <td width='33%' valign=top style='width:33.34%;padding:0; margin: 0;'>
                                            <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>- Tên cổ
                                                    phiếu: </span> {(i.TenPhieu_TSNN != null && i.TenPhieu_TSNN != "" ? $@"{i.TenPhieu_TSNN}" : "Không")}.</p>
                                        </td>
                                        <td width='33%' valign=top style='width:33.34%;padding:0; margin: 0;'>
                                            <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>Số
                                                    lượng: </span> {(i.SoLuong_TSNN != null && i.SoLuong_TSNN != "" ? $@"{i.SoLuong_TSNN}" : "Không")}.</p>
                                        </td>
                                        <td width='33%' valign=top style='width:33.34%;padding:0; margin: 0;'>
                                            <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>Giá
                                                    trị: </span>{(i.GiaTri_TSNN != null && i.GiaTri_TSNN != "" ? $@"{i.GiaTri_TSNN} VNĐ" : "Không")}.</p>
                                        </td>
                                    </tr>
                                </table>
                            ";
            }
            if (tsnnCoPhieu == "")
            {
                tsnnCoPhieu += $@"<table class=MsoNormalTable border=0 cellspacing=0 cellpadding=0 width='100%'
                                    style='width:100.0%;border-collapse:collapse'>
                                    <tr>
                                        <td width='33%' valign=top style='width:33.34%;padding:0; margin: 0;'>
                                            <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>- Tên cổ
                                                    phiếu: </span>Không.</p>
                                        </td>
                                        <td width='33%' valign=top style='width:33.34%;padding:0; margin: 0;'>
                                            <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>Số
                                                    lượng: </span>Không.</p>
                                        </td>
                                        <td width='33%' valign=top style='width:33.34%;padding:0; margin: 0;'>
                                            <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>Giá
                                                    trị: </span>Không.</p>
                                        </td>
                                    </tr>
                                </table>
                            ";
            }

            var tsnnTraiPhieu = "";

            foreach (var i in data2.tsnntraiphieu)
            {
                tsnnTraiPhieu += $@"<table class=MsoNormalTable border=0 cellspacing=0 cellpadding=0 width='100%'
                                    style='width:100.0%;border-collapse:collapse'>
                                    <tr>
                                        <td width='33%' valign=top style='width:33.34%;padding:0; margin: 0;'>
                                            <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>- Tên cổ
                                                    phiếu: </span>{(i.TenPhieu_TSNN != null && i.TenPhieu_TSNN != "" ? $@"{i.TenPhieu_TSNN}" : "Không")}.</p>
                                        </td>
                                        <td width='33%' valign=top style='width:33.34%;padding:0; margin: 0;'>
                                            <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>Số
                                                    lượng: </span>{(i.SoLuong_TSNN != null && i.SoLuong_TSNN != "" ? $@"{i.SoLuong_TSNN}" : "Không")}.</p>
                                        </td>
                                        <td width='33%' valign=top style='width:33.34%;padding:0; margin: 0;'>
                                            <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>Giá
                                                    trị: </span>{(i.GiaTri_TSNN != null && i.GiaTri_TSNN != "" ? $@"{i.GiaTri_TSNN} VNĐ" : "Không")}.</p>
                                        </td>
                                    </tr>
                                </table>
                            ";
            }
            if (tsnnTraiPhieu == "")
            {
                tsnnTraiPhieu += $@"<table class=MsoNormalTable border=0 cellspacing=0 cellpadding=0 width='100%'
                                    style='width:100.0%;border-collapse:collapse'>
                                    <tr>
                                        <td width='33%' valign=top style='width:33.34%;padding:0; margin: 0;'>
                                            <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>- Tên cổ
                                                    phiếu: </span>Không.</p>
                                        </td>
                                        <td width='33%' valign=top style='width:33.34%;padding:0; margin: 0;'>
                                            <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>Số
                                                    lượng: </span>Không.</p>
                                        </td>
                                        <td width='33%' valign=top style='width:33.34%;padding:0; margin: 0;'>
                                            <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>Giá
                                                    trị: </span>Không.</p>
                                        </td>
                                    </tr>
                                </table>
                            ";
            }

            var tsnnVonGop = "";

            foreach (var i in data2.tsnnvongop)
            {
                tsnnVonGop += $@" <table class=MsoNormalTable border=0 cellspacing=0 cellpadding=0 width='100%'
                                    style='width:100.0%;border-collapse:collapse'>
                                    <tr>
                                        <td width='55%' valign=top style='width:55.64%;padding:0; margin: 0;'>
                                            <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>- Hình
                                                    thức góp vốn: </span>{(i.TenPhieu_TSNN != null && i.TenPhieu_TSNN != "" ? $@"{i.TenPhieu_TSNN}" : "Không")}.</p>
                                        </td>
                                        <td width='44%' valign=top style='width:44.36%;padding:0; margin: 0;'>
                                            <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>Giá
                                                    trị: </span> {(i.GiaTri_TSNN != null && i.GiaTri_TSNN != "" ? $@"{i.GiaTri_TSNN} VNĐ" : "Không")}.</p>
                                        </td>
                                    </tr>
                                </table>
                            ";
            }
            if (tsnnVonGop == "")
            {
                tsnnVonGop += $@" <table class=MsoNormalTable border=0 cellspacing=0 cellpadding=0 width='100%'
                                    style='width:100.0%;border-collapse:collapse'>
                                    <tr>
                                        <td width='55%' valign=top style='width:55.64%;padding:0; margin: 0;'>
                                            <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>- Hình
                                                    thức góp vốn: </span>Không.</p>
                                        </td>
                                        <td width='44%' valign=top style='width:44.36%;padding:0; margin: 0;'>
                                            <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>Giá
                                                    trị: </span> Không.</p>
                                        </td>
                                    </tr>
                                </table>
                            ";
            }

            var tsnnGiayToKhac = "";

            foreach (var i in data2.tsnngiayto)
            {
                tsnnGiayToKhac += $@" <table class=MsoNormalTable border=0 cellspacing=0 cellpadding=0 width='100%'
                                    style='width:100.0%;border-collapse:collapse'>
                                    <tr>
                                        <td width='57%' valign=top style='width:57.12%;padding:0; margin: 0;'>
                                            <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>- Tên giấy
                                                    tờ có giá: </span>{(i.TenPhieu_TSNN != null && i.TenPhieu_TSNN != "" ? $@"{i.TenPhieu_TSNN}" : "Không")}.</p>
                                        </td>
                                        <td width='42%' valign=top style='width:42.88%;padding:0; margin: 0;'>
                                            <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>Giá trị: 
                                                </span>{(i.GiaTri_TSNN != null && i.GiaTri_TSNN != "" ? $@"{i.GiaTri_TSNN} VNĐ" : "Không")}.</p>
                                        </td>
                                    </tr>
                                </table>
                            ";
            }
            if (tsnnGiayToKhac == "")
            {
                tsnnGiayToKhac += $@" <table class=MsoNormalTable border=0 cellspacing=0 cellpadding=0 width='100%'
                                    style='width:100.0%;border-collapse:collapse'>
                                    <tr>
                                        <td width='57%' valign=top style='width:57.12%;padding:0; margin: 0;'>
                                            <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>- Tên giấy
                                                    tờ có giá: </span>Không.</p>
                                        </td>
                                        <td width='42%' valign=top style='width:42.88%;padding:0; margin: 0;'>
                                            <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>Giá trị: 
                                                </span>Không.</p>
                                        </td>
                                    </tr>
                                </table>
                            ";
            }


            var tsnnTaiSanKhac = "";

            foreach (var i in data2.tsnntaisankhac)
            {
                if (i.LoaiTaiSanKhac_TSNN == "TaiSanKhac")
                {
                    tsnnTaiSanKhac += $@" <table class=MsoNormalTable border=0 cellspacing=0 cellpadding=0 width='100%'
                                    style='width:100.0%;border-collapse:collapse'>
                                    <tr>
                                        <td width='33%' valign=top style='width:33.34%;padding:0; margin: 0;'>
                                            <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>- Tên tài sản: </span>{(i.TenTaiSan_TSNN != null && i.TenTaiSan_TSNN != "" ? $@"{i.TenTaiSan_TSNN}" : "Không")}.</p>
                                        </td>
                                        <td width='33%' valign=top style='width:33.34%;padding:0; margin: 0;'>
                                            <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>Năm bắt đầu sở hữu: </span>{(i.SoDangKy_NamBatDauSuDung_TSNN != null && i.SoDangKy_NamBatDauSuDung_TSNN != "" ? $@"{i.SoDangKy_NamBatDauSuDung_TSNN}" : "Không")}.</p>
                                        </td>
                                        <td width='33%' valign=top style='width:33.34%;padding:0; margin: 0;'>
                                            <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>Giá
                                                    trị: </span>{(i.GiaTri_TSNN != null && i.GiaTri_TSNN != "" ? $@"{i.GiaTri_TSNN} VNĐ" : "Không")}.</p>
                                        </td>
                                    </tr>
                                </table>
                            ";
                }

            }
            if (tsnnTaiSanKhac == "")
            {
                tsnnTaiSanKhac += $@" <table class=MsoNormalTable border=0 cellspacing=0 cellpadding=0 width='100%'
                                    style='width:100.0%;border-collapse:collapse'>
                                    <tr>
                                        <td width='33%' valign=top style='width:33.34%;padding:0; margin: 0;'>
                                            <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>- Tên tài sản: </span>Không.</p>
                                        </td>
                                        <td width='33%' valign=top style='width:33.34%;padding:0; margin: 0;'>
                                            <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>Năm bắt đầu sở hữu: </span>Không.</p>
                                        </td>
                                        <td width='33%' valign=top style='width:33.34%;padding:0; margin: 0;'>
                                            <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>Giá
                                                    trị: </span>Không.</p>
                                        </td>
                                    </tr>
                                </table>
                            ";
            }

            var tsnnTaiSanPhapLuatQuyDinh = "";

            foreach (var i in data2.tsnngiaydangky)
            {

                tsnnTaiSanPhapLuatQuyDinh += $@" <table class=MsoNormalTable border=0 cellspacing=0 cellpadding=0 width='100%'
                                style='width:100.0%;border-collapse:collapse'>
                                <tr>
                                    <td width='33%' valign=top style='width:33.34%;padding:0; margin: 0;'>
                                        <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>- Tên tài sản: </span>{(i.TenTaiSan_TSNN != null && i.TenTaiSan_TSNN != "" ? $@"{i.TenTaiSan_TSNN}" : "Không")}.</p>
                                    </td>
                                    <td width='33%' valign=top style='width:33.34%;padding:0; margin: 0;'>
                                        <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>Số đăng kí: </span> {(i.SoDangKy_NamBatDauSuDung_TSNN != null && i.SoDangKy_NamBatDauSuDung_TSNN != "" ? $@"{i.SoDangKy_NamBatDauSuDung_TSNN}" : "Không")}.</p>
                                    </td>
                                    <td width='33%' valign=top style='width:33.34%;padding:0; margin: 0;'>
                                        <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>Giá
                                                trị: </span>{(i.GiaTri_TSNN != null && i.GiaTri_TSNN != "" ? $@"{i.GiaTri_TSNN} VNĐ" : "Không")}.</p>
                                    </td>
                                </tr>
                            </table>
                        ";

            }
            if (tsnnTaiSanPhapLuatQuyDinh == "")
            {
                tsnnTaiSanPhapLuatQuyDinh += $@" <table class=MsoNormalTable border=0 cellspacing=0 cellpadding=0 width='100%'
                                style='width:100.0%;border-collapse:collapse'>
                                <tr>
                                    <td width='33%' valign=top style='width:33.34%;padding:0; margin: 0;'>
                                        <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>- Tên tài sản: </span>Không.</p>
                                    </td>
                                    <td width='33%' valign=top style='width:33.34%;padding:0; margin: 0;'>
                                        <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>Số đăng kí: </span> Không.</p>
                                    </td>
                                    <td width='33%' valign=top style='width:33.34%;padding:0; margin: 0;'>
                                        <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>Giá
                                                trị: </span>Không.</p>
                                    </td>
                                </tr>
                            </table>
                        ";
            }


            //end tài khoản nước ngoài

            var TaiSanNuocNgoai = "";

            foreach(var i in data2.taisannuocngoai)
            {
                TaiSanNuocNgoai += $@" <table class=MsoNormalTable border=0 cellspacing=0 cellpadding=0 width='100%'
                                    style='width:100.0%;border-collapse:collapse'>
                                    <tr>
                                        <td width='57%' valign=top style='width:57.12%;padding:0; margin: 0;'>
                                            <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>- Tên giấy
                                                    tờ có giá: </span> {(i.TenTaiSanNuocNgoai != null && i.TenTaiSanNuocNgoai != "" ? $@"{i.TenTaiSanNuocNgoai}" : "Không")}.</p>
                                        </td>
                                        <td width='42%' valign=top style='width:42.88%;padding:0; margin: 0;'>
                                            <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>Giá trị: 
                                                </span>{(i.GiaTri != null && i.GiaTri != "" ? $@"{i.GiaTri} VNĐ" : "Không")}.</p>
                                        </td>
                                    </tr>
                                </table>
                            ";
            }
            if (TaiSanNuocNgoai == "")
            {
                TaiSanNuocNgoai += $@" <table class=MsoNormalTable border=0 cellspacing=0 cellpadding=0 width='100%'
                                    style='width:100.0%;border-collapse:collapse'>
                                    <tr>
                                        <td width='57%' valign=top style='width:57.12%;padding:0; margin: 0;'>
                                            <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>- Tên giấy
                                                    tờ có giá: </span>Không.</p>
                                        </td>
                                        <td width='42%' valign=top style='width:42.88%;padding:0; margin: 0;'>
                                            <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>Giá trị: 
                                                </span>Không.</p>
                                        </td>
                                    </tr>
                                </table>
                            ";
            }

            var TaiKhoanNuocNgoai = "";

            foreach(var i in data2.taikhoannuocngoai)
            {
                TaiKhoanNuocNgoai += $@" <table class=MsoNormalTable border=0 cellspacing=0 cellpadding=0 width='100%'
                                    style='width:100.0%;border-collapse:collapse'>
                                    <tr>
                                    <td width='50%' valign=top style='width:33.34%;padding:0; margin: 0;'>
                                        <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>- Tên chủ tài khoản: </span>{(i.TenChuTaiKhoan != null && i.TenChuTaiKhoan != "" ? $@"{i.TenChuTaiKhoan}" : "Không")}.</p>
                                    </td>
                                    <td width='50%' valign=top style='width:33.34%;padding:0; margin: 0;'>
                                        <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>Số tài khoản: </span>{(i.SoTaiKhoan != null && i.SoTaiKhoan != "" ? $@"{i.SoTaiKhoan}" : "Không")}.</p>
                                    </td>
                                   
                                </tr>
                                </table>
                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>- Tên ngân hàng, chi
                                        nhánh ngân hàng, tổ chức nơi mở tài
                                    khoản:</span> {(i.TenNganHang != null && i.TenNganHang != "" ? $@"{i.TenNganHang}" : "Không")}.</p>
                            ";
            }
            if (TaiKhoanNuocNgoai == "")
            {
                TaiKhoanNuocNgoai += $@" <table class=MsoNormalTable border=0 cellspacing=0 cellpadding=0 width='100%'
                                    style='width:100.0%;border-collapse:collapse'>
                                    <tr>
                                    <td width='50%' valign=top style='width:33.34%;padding:0; margin: 0;'>
                                        <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>- Tên chủ tài khoản: </span>Không.</p>
                                    </td>
                                    <td width='50%' valign=top style='width:33.34%;padding:0; margin: 0;'>
                                        <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>Số tài khoản: </span>Không.</p>
                                    </td>
                                   
                                </tr>
                                </table>
                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>- Tên ngân hàng, chi
                                        nhánh ngân hàng, tổ chức nơi mở tài
                                    khoản:</span> Không.</p>
                            ";
            }

            var TongThuNhap_NguoiKeKhai = "";
            var TongThuNhap_VoHoacChong = "";
            var TongThuNhap_ConChuaThanhNien = "";
            var TongThuNhap_CacKhoanChung = "";
            if (data2.tongthunhap != null)
            {
                
                if (data2.tongthunhap.TongThuNhap_NguoiKeKhai != null)
                {
                    TongThuNhap_NguoiKeKhai = $@"<p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>- Tổng thu nhập của
                                        người kê khai: </span>{(data2.tongthunhap.TongThuNhap_NguoiKeKhai != null && data2.tongthunhap.TongThuNhap_NguoiKeKhai != "" ? $@"{data2.tongthunhap.TongThuNhap_NguoiKeKhai} VNĐ" : "Không")}.</p>";
                }
                if (TongThuNhap_NguoiKeKhai == "")
                {
                    TongThuNhap_NguoiKeKhai = $@"<p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>- Tổng thu nhập của
                                        người kê khai: </span>Không.</p>";
                }


                if (data2.tongthunhap.TongThuNhap_VoHoacChong != null)
                {
                    TongThuNhap_VoHoacChong = $@"<p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>- Tổng thu nhập của vợ
                                        (hoặc chồng): </span>{(data2.tongthunhap.TongThuNhap_VoHoacChong != null && data2.tongthunhap.TongThuNhap_VoHoacChong != "" ? $@"{data2.tongthunhap.TongThuNhap_VoHoacChong} VNĐ" : "Không")}.</p>";
                }
                if (TongThuNhap_VoHoacChong == "")
                {
                    TongThuNhap_VoHoacChong = $@"<p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>- Tổng thu nhập của vợ
                                        (hoặc chồng): </span>Không.</p>";
                }

                if (data2.tongthunhap.TongThuNhap_ConChuaThanhNien != null)
                {
                    TongThuNhap_ConChuaThanhNien = $@"<p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>- Tổng thu nhập của
                                        con chưa thành niên: </span>{(data2.tongthunhap.TongThuNhap_ConChuaThanhNien != null && data2.tongthunhap.TongThuNhap_ConChuaThanhNien != "" ? $@"{data2.tongthunhap.TongThuNhap_ConChuaThanhNien} VNĐ" : "Không")}.</p>";
                }
                if (TongThuNhap_ConChuaThanhNien == "")
                {
                    TongThuNhap_ConChuaThanhNien = $@"<p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>- Tổng thu nhập của
                                        con chưa thành niên: </span>Không.</p>";
                }


                if (data2.tongthunhap.TongThuNhap_CacKhoanChung != null)
                {
                    TongThuNhap_CacKhoanChung = $@" <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>- Tổng các khoản thu
                                        nhập chung: </span>{(data2.tongthunhap.TongThuNhap_CacKhoanChung != null && data2.tongthunhap.TongThuNhap_CacKhoanChung != "" ? $@"{data2.tongthunhap.TongThuNhap_CacKhoanChung} VNĐ" : "Không")}.</p>";
                }
                if (TongThuNhap_CacKhoanChung == "")
                {
                    TongThuNhap_CacKhoanChung = $@" <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>- Tổng các khoản thu
                                        nhập chung: </span>Không.</p>";
                }

            }
            else
            {
                TongThuNhap_NguoiKeKhai = $@"<p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>- Tổng thu nhập của
                                        người kê khai: </span>Không.</p>";
                TongThuNhap_VoHoacChong = $@"<p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>- Tổng thu nhập của vợ
                                        (hoặc chồng): </span>Không.</p>";

                TongThuNhap_ConChuaThanhNien = $@"<p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>- Tổng thu nhập của
                                        con chưa thành niên: </span>Không.</p>";
                TongThuNhap_CacKhoanChung = $@" <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>- Tổng các khoản thu
                                        nhập chung: </span>Không.</p>";
            }

            //--------------------------------------------


            var BDTSDatO = "";
            var BDTSDatKhac = "";
            var BDTSNhaO = "";
            var BDTSCongTrinhKhach = "";
            var BDTSCayLauNam = "";
            var BDTSRungSanXuat = "";
            var BDTSVatKienTruc = "";
            var BDTSTrangSuc = "";
            var BDTSTien = "";
            var BDTSCoPhieu = "";
            var BDTSVonGop = "";
            var BDTSTraiPhieu = "";
            var BDTSGiayToKhac = "";
            var BDTSTaiSanPhapLuatQuyDinh = "";
            var BDTSDoMyNghe = "";
            var BDTSTaiSanNuocNgoai = "";
            var BDTSTaiKhoanNuocNgoai = "";
            var BDTSTongThuNhap = "";
            if (bankekhai.Ma_Loai_KeKhai != 3)
            {
                
                foreach (var i in biendongdato)
                {
                    var tenloaitstn = i.TenLoaiTSTNJson.Replace(Environment.NewLine, "<br/>");
                    var soluongtstn = i.SoLuongTaiSanJson.Replace(Environment.NewLine, "<br/>");
                    var giatritstn = i.GiaTriTSTNJson.Replace(Environment.NewLine, "<br/>");
                    var noidungtstn = i.NoiDungGiaiTrinhJson.Replace(Environment.NewLine, "<br/>");

                    BDTSDatO = $@"<tr >
                                <td width='43%' valign=top style='width:43.48%;border:none;border-left:solid windowtext 1.0pt; border-right: 1px solid black;
                                background:white;padding:0in 0in 0in 0in' >
                                    <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>{tenloaitstn}</span></p>
                                </td>
                                <td width='18%' valign=top style='width:18.54%;border:none;background:white;  border-right: 1px solid black; border-left: 1px solid black;
                    padding:0in 0in 0in 0in'>
                                    <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>{soluongtstn}</span></p>
                                </td>
                                <td width='22%' style='width:22.52%;border:none;background:white;padding: border-right: 1px solid black; border-left: 1px solid black;
                    0in 0in 0in 0in'>
                                    <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>{giatritstn}</span></p>
                                    <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'</p>
                                </td>
                                <td width='15%' valign=top style='width:15.46%;border:none;border-right:solid windowtext 1.0pt;  border-left: 1px solid black;
                    background:white;padding:0in 0in 0in 0in'>
                                    <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>{noidungtstn}</span></p>
                                </td>
                            </tr>";
                }

            
                foreach (var i in biendongdatkhac)
                {
                    var tenloaitstn = i.TenLoaiTSTNJson.Replace(Environment.NewLine, "<br/>");
                    var soluongtstn = i.SoLuongTaiSanJson.Replace(Environment.NewLine, "<br/>");
                    var giatritstn = i.GiaTriTSTNJson.Replace(Environment.NewLine, "<br/>");
                    var noidungtstn = i.NoiDungGiaiTrinhJson.Replace(Environment.NewLine, "<br/>");

                    BDTSDatKhac = $@"<tr>
                                            <td width='43%' valign=top style='width:43.48%;border:none;border-left:solid windowtext 1.0pt; border-right: 1px solid black; 
                                background:white;padding:0in 0in 0in 0in'>
                                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>{tenloaitstn}.</span></p>
                                            </td>
                                            <td width='18%' valign=top style='width:18.54%;border:none;background:white; border-right: 1px solid black; border-left: 1px solid black;
                                padding:0in 0in 0in 0in'>
                                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>{soluongtstn}.</span>
                                                </p>
                                            </td>
                                            <td width='22%' valign=top style='width:22.52%;border:none;background:white; border-right: 1px solid black; border-left: 1px solid black;
                                padding:0in 0in 0in 0in'>
                                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>{giatritstn}</span>
                                                </p>
                                            </td>
                                            <td width='15%' valign=top style='width:15.46%;border:none;border-right:solid windowtext 1.0pt; border-left: 1px solid black;
                                background:white;padding:0in 0in 0in 0in'>
                                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>{noidungtstn}</span>
                                                </p>
                                            </td>
                                        </tr>";
                }
                
                foreach (var i in biendongnhaocongtrinh)
                {
                    var tenloaitstn = i.TenLoaiTSTNJson.Replace(Environment.NewLine, "<br/>");
                    var soluongtstn = i.SoLuongTaiSanJson.Replace(Environment.NewLine, "<br/>");
                    var giatritstn = i.GiaTriTSTNJson.Replace(Environment.NewLine, "<br/>");
                    var noidungtstn = i.NoiDungGiaiTrinhJson.Replace(Environment.NewLine, "<br/>");

                    BDTSNhaO = $@"<tr  style='border-bottom: 1px solid black'>
                                            <td width='43%' valign=top style='width:43.48%;border:none;border-left:solid windowtext 1.0pt; border-right: 1px solid black;
                                background:white;padding:0in 0in 0in 0in'>
                                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>{tenloaitstn}</span>
                                                </p>
                                            </td>
                                            <td width='18%' valign=top style='width:18.54%;border:none;background:white;border-right: 1px solid black; border-left: 1px solid black;
                                padding:0in 0in 0in 0in'>
                                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>{soluongtstn}</span>
                                                </p>
                                            </td>
                                            <td width='22%' valign=top style='width:22.52%;border:none;background:white; border-right: 1px solid black; border-left: 1px solid black;
                                padding:0in 0in 0in 0in'>
                                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>{giatritstn}</span>
                                                </p>
                                            </td>
                                            <td width='15%' valign=top style='width:15.46%;border:none;border-right:solid windowtext 1.0pt;  border-left: 1px solid black;
                                background:white;padding:0in 0in 0in 0in'>
                                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>{noidungtstn}</span>
                                                </p>
                                            </td>
                                        </tr>";
                }

               
                foreach (var i in biendongcongtrinhkhac)
                {
                    var tenloaitstn = i.TenLoaiTSTNJson.Replace(Environment.NewLine, "<br/>");
                    var soluongtstn = i.SoLuongTaiSanJson.Replace(Environment.NewLine, "<br/>");
                    var giatritstn = i.GiaTriTSTNJson.Replace(Environment.NewLine, "<br/>");
                    var noidungtstn = i.NoiDungGiaiTrinhJson.Replace(Environment.NewLine, "<br/>");

                    BDTSCongTrinhKhach = $@"<tr  style='border: 1px solid black'>
                                            <td width='43%' valign=top style='width:43.48%;border:none;border-left:solid windowtext 1.0pt; border-right: 1px solid black;
                                background:white;padding:0in 0in 0in 0in'>
                                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>{tenloaitstn}</span>
                                                </p>
                                            </td>
                                            <td width='18%' valign=top style='width:18.54%;border:none;background:white; border-right: 1px solid black; border-left: 1px solid black;
                                padding:0in 0in 0in 0in'>
                                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>{soluongtstn}</span>
                                                </p>
                                            </td>
                                            <td width='22%' valign=top style='width:22.52%;border:none;background:white; border-right: 1px solid black; border-left: 1px solid black;
                                padding:0in 0in 0in 0in'>
                                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>{giatritstn}</span>
                                                </p>
                                            </td>
                                            <td width='15%' valign=top style='width:15.46%;border:none;border-right:solid windowtext 1.0pt; border-left: 1px solid black;
                                background:white;padding:0in 0in 0in 0in'>
                                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>{noidungtstn}</span>
                                                </p>
                                            </td>
                                        </tr>";
                }

               
                foreach (var i in biendongcaylaunam)
                {
                    var tenloaitstn = i.TenLoaiTSTNJson.Replace(Environment.NewLine, "<br/>");
                    var soluongtstn = i.SoLuongTaiSanJson.Replace(Environment.NewLine, "<br/>");
                    var giatritstn = i.GiaTriTSTNJson.Replace(Environment.NewLine, "<br/>");
                    var noidungtstn = i.NoiDungGiaiTrinhJson.Replace(Environment.NewLine, "<br/>");

                    BDTSCayLauNam = $@"<tr  style='border: 1px solid black'>
                                            <td width='43%' valign=top style='width:43.48%;border:none;border-left:solid windowtext 1.0pt; border-right: 1px solid black;
                                background:white;padding:0in 0in 0in 0in'>
                                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>{tenloaitstn}</span>
                                                </p>
                                            </td>
                                            <td width='18%' valign=top style='width:18.54%;border:none;background:white; border-right: 1px solid black; border-left: 1px solid black;
                                padding:0in 0in 0in 0in'>
                                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>{soluongtstn}</span>
                                                </p>
                                            </td>
                                            <td width='22%' valign=top style='width:22.52%;border:none;background:white; border-right: 1px solid black; border-left: 1px solid black;
                                padding:0in 0in 0in 0in'>
                                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>{giatritstn}</span>
                                                </p>
                                            </td>
                                            <td width='15%' valign=top style='width:15.46%;border:none;border-right:solid windowtext 1.0pt; border-left: 1px solid black;
                                background:white;padding:0in 0in 0in 0in'>
                                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>{noidungtstn}</span>
                                                </p>
                                            </td>
                                        </tr>";
                }

                foreach (var i in biendongrung)
                {
                    var tenloaitstn = i.TenLoaiTSTNJson.Replace(Environment.NewLine, "<br/>");
                    var soluongtstn = i.SoLuongTaiSanJson.Replace(Environment.NewLine, "<br/>");
                    var giatritstn = i.GiaTriTSTNJson.Replace(Environment.NewLine, "<br/>");
                    var noidungtstn = i.NoiDungGiaiTrinhJson.Replace(Environment.NewLine, "<br/>");

                    BDTSRungSanXuat = $@"<tr  style='border: 1px solid black'>
                                            <td width='43%' valign=top style='width:43.48%;border:none;border-left:solid windowtext 1.0pt; border-right: 1px solid black;
                                background:white;padding:0in 0in 0in 0in'>
                                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>{tenloaitstn}</span>
                                                </p>
                                            </td>
                                            <td width='18%' valign=top style='width:18.54%;border:none;background:white; border-right: 1px solid black; border-left: 1px solid black;
                                padding:0in 0in 0in 0in'>
                                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>{soluongtstn}</span>
                                                </p>
                                            </td>
                                            <td width='22%' valign=top style='width:22.52%;border:none;background:white; border-right: 1px solid black; border-left: 1px solid black;
                                padding:0in 0in 0in 0in'>
                                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>{giatritstn}</span>
                                                </p>
                                            </td>
                                            <td width='15%' valign=top style='width:15.46%;border:none;border-right:solid windowtext 1.0pt; border-left: 1px solid black;
                                background:white;padding:0in 0in 0in 0in'>
                                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>{noidungtstn}</span>
                                                </p>
                                            </td>
                                        </tr>";
                }


                foreach (var i in biendongvatkientruc)
                {
                    var tenloaitstn = i.TenLoaiTSTNJson.Replace(Environment.NewLine, "<br/>");
                    var soluongtstn = i.SoLuongTaiSanJson.Replace(Environment.NewLine, "<br/>");
                    var giatritstn = i.GiaTriTSTNJson.Replace(Environment.NewLine, "<br/>");
                    var noidungtstn = i.NoiDungGiaiTrinhJson.Replace(Environment.NewLine, "<br/>");

                    BDTSVatKienTruc = $@"<tr>
                                            <td width='43%' valign=top style='width:43.48%;border:none;border-left:solid windowtext 1.0pt; border-right: 1px solid black;
                                background:white;padding:0in 0in 0in 0in'>
                                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>{tenloaitstn}</span>
                                                </p>
                                            </td>
                                            <td width='18%' valign=top style='width:18.54%;border:none;background:white; border-right: 1px solid black; border-left: 1px solid black;
                                padding:0in 0in 0in 0in'>
                                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>{soluongtstn}</span>
                                                </p>
                                            </td>
                                            <td width='22%' valign=top style='width:22.52%;border:none;background:white; border-right: 1px solid black; border-left: 1px solid black;
                                padding:0in 0in 0in 0in'>
                                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>{giatritstn}</span>
                                                </p>
                                            </td>
                                            <td width='15%' valign=top style='width:15.46%;border:none;border-right:solid windowtext 1.0pt; border-left: 1px solid black;
                                background:white;padding:0in 0in 0in 0in'>
                                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>{noidungtstn}</span>
                                                </p>
                                            </td>
                                        </tr>";
                }

                
                foreach (var i in biendongvangbacdaquy)
                {
                    var tenloaitstn = i.TenLoaiTSTNJson.Replace(Environment.NewLine, "<br/>");
                    var soluongtstn = i.SoLuongTaiSanJson.Replace(Environment.NewLine, "<br/>");
                    var giatritstn = i.GiaTriTSTNJson.Replace(Environment.NewLine, "<br/>");
                    var noidungtstn = i.NoiDungGiaiTrinhJson.Replace(Environment.NewLine, "<br/>");

                    BDTSTrangSuc = $@"<tr>
                                            <td width='43%' valign=top style='width:43.48%;border:none;border-left:solid windowtext 1.0pt; border-right: 1px solid black; 
                                background:white;padding:0in 0in 0in 0in'>
                                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>{tenloaitstn}</span>
                                                </p>
                                            </td>
                                            <td width='18%' valign=top style='width:18.54%;border:none;background:white; border-right: 1px solid black; border-left: 1px solid black;
                                padding:0in 0in 0in 0in'>
                                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>{soluongtstn}</span>
                                                </p>
                                            </td>
                                            <td width='22%' valign=top style='width:22.52%;border:none;background:white; border-right: 1px solid black; border-left: 1px solid black;
                                padding:0in 0in 0in 0in'>
                                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>{giatritstn}</span>
                                                </p>
                                            </td>
                                            <td width='15%' valign=top style='width:15.46%;border:none;border-right:solid windowtext 1.0pt; border-left: 1px solid black;
                                background:white;padding:0in 0in 0in 0in'>
                                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>{noidungtstn}</span>
                                                </p>
                                            </td>
                                        </tr>";
                }
                
                foreach (var i in biendongtien)
                {
                    var tenloaitstn = i.TenLoaiTSTNJson.Replace(Environment.NewLine, "<br/>");
                    var soluongtstn = i.SoLuongTaiSanJson.Replace(Environment.NewLine, "<br/>");
                    var giatritstn = i.GiaTriTSTNJson.Replace(Environment.NewLine, "<br/>");
                    var noidungtstn = i.NoiDungGiaiTrinhJson.Replace(Environment.NewLine, "<br/>");

                    BDTSTien = $@"<tr>
                                            <td width='43%' valign=top style='width:43.48%;border:none;border-left:solid windowtext 1.0pt; border-right: 1px solid black; 
                                background:white;padding:0in 0in 0in 0in'>
                                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>{tenloaitstn}</span>
                                                </p>
                                            </td>
                                            <td width='18%' valign=top style='width:18.54%;border:none;background:white; border-right: 1px solid black; border-left: 1px solid black;
                                padding:0in 0in 0in 0in'>
                                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>{soluongtstn}</span>
                                                </p>
                                            </td>
                                            <td width='22%' valign=top style='width:22.52%;border:none;background:white; border-right: 1px solid black; border-left: 1px solid black;
                                padding:0in 0in 0in 0in'>
                                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>{giatritstn}</span>
                                                </p>
                                            </td>
                                            <td width='15%' valign=top style='width:15.46%;border:none;border-right:solid windowtext 1.0pt;  border-left: 1px solid black;
                                background:white;padding:0in 0in 0in 0in'>
                                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>{noidungtstn}</span>
                                                </p>
                                            </td>
                                        </tr>";
                }

               
                foreach (var i in biendongcophieu)
                {
                    var tenloaitstn = i.TenLoaiTSTNJson.Replace(Environment.NewLine, "<br/>");
                    var soluongtstn = i.SoLuongTaiSanJson.Replace(Environment.NewLine, "<br/>");
                    var giatritstn = i.GiaTriTSTNJson.Replace(Environment.NewLine, "<br/>");
                    var noidungtstn = i.NoiDungGiaiTrinhJson.Replace(Environment.NewLine, "<br/>");

                    BDTSCoPhieu = $@"<tr>
                                            <td width='43%' valign=top style='width:43.48%;border:none;border-left:solid windowtext 1.0pt; border-right: 1px solid black; 
                                background:white;padding:0in 0in 0in 0in'>
                                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>{tenloaitstn}</span>
                                                </p>
                                            </td>
                                            <td width='18%' valign=top style='width:18.54%;border:none;background:white; border-right: 1px solid black; border-left: 1px solid black;
                                padding:0in 0in 0in 0in'>
                                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>{soluongtstn}</span>
                                                </p>
                                            </td>
                                            <td width='22%' valign=top style='width:22.52%;border:none;background:white; border-right: 1px solid black; border-left: 1px solid black;
                                padding:0in 0in 0in 0in'>
                                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>{giatritstn}</span>
                                                </p>
                                            </td>
                                            <td width='15%' valign=top style='width:15.46%;border:none;border-right:solid windowtext 1.0pt; border-left: 1px solid black;
                                background:white;padding:0in 0in 0in 0in'>
                                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>{noidungtstn}</span>
                                                </p>
                                            </td>
                                        </tr>";
                }

                foreach (var i in biendongtraiphieu)
                {
                    var tenloaitstn = i.TenLoaiTSTNJson.Replace(Environment.NewLine, "<br/>");
                    var soluongtstn = i.SoLuongTaiSanJson.Replace(Environment.NewLine, "<br/>");
                    var giatritstn = i.GiaTriTSTNJson.Replace(Environment.NewLine, "<br/>");
                    var noidungtstn = i.NoiDungGiaiTrinhJson.Replace(Environment.NewLine, "<br/>");

                    BDTSTraiPhieu = $@"<tr>
                                            <td width='43%' valign=top style='width:43.48%;border:none;border-left:solid windowtext 1.0pt; border-right: 1px solid black;
                                background:white;padding:0in 0in 0in 0in'>
                                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>{tenloaitstn}</span>
                                                </p>
                                            </td>
                                            <td width='18%' valign=top style='width:18.54%;border:none;background:white; border-right: 1px solid black; border-left: 1px solid black;
                                padding:0in 0in 0in 0in'>
                                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>{soluongtstn}</span>
                                                </p>
                                            </td>
                                            <td width='22%' valign=top style='width:22.52%;border:none;background:white; border-right: 1px solid black; border-left: 1px solid black;
                                padding:0in 0in 0in 0in'>
                                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>{giatritstn}</span>
                                                </p>
                                            </td>
                                            <td width='15%' valign=top style='width:15.46%;border:none;border-right:solid windowtext 1.0pt; border-left: 1px solid black;
                                background:white;padding:0in 0in 0in 0in'>
                                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>{noidungtstn}</span>
                                                </p>
                                            </td>
                                        </tr>";
                }


              
                foreach (var i in biendongvongop)
                {
                    var tenloaitstn = i.TenLoaiTSTNJson.Replace(Environment.NewLine, "<br/>");
                    var soluongtstn = i.SoLuongTaiSanJson.Replace(Environment.NewLine, "<br/>");
                    var giatritstn = i.GiaTriTSTNJson.Replace(Environment.NewLine, "<br/>");
                    var noidungtstn = i.NoiDungGiaiTrinhJson.Replace(Environment.NewLine, "<br/>");

                    BDTSVonGop = $@"<tr>
                                            <td width='43%' valign=top style='width:43.48%;border:none;border-left:solid windowtext 1.0pt; border-right: 1px solid black; 
                                background:white;padding:0in 0in 0in 0in'>
                                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>{tenloaitstn}</span>
                                                </p>
                                            </td>
                                            <td width='18%' valign=top style='width:18.54%;border:none;background:white; border-right: 1px solid black; border-left: 1px solid black;
                                padding:0in 0in 0in 0in'>
                                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>{soluongtstn}</span>
                                                </p>
                                            </td>
                                            <td width='22%' valign=top style='width:22.52%;border:none;background:white; border-right: 1px solid black; border-left: 1px solid black;
                                padding:0in 0in 0in 0in'>
                                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>{giatritstn}</span>
                                                </p>
                                            </td>
                                            <td width='15%' valign=top style='width:15.46%;border:none;border-right:solid windowtext 1.0pt; border-left: 1px solid black;
                                background:white;padding:0in 0in 0in 0in'>
                                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>{noidungtstn}</span>
                                                </p>
                                            </td>
                                        </tr>";
                }


                
                foreach (var i in biendonggiaytokhac)
                {
                    var tenloaitstn = i.TenLoaiTSTNJson.Replace(Environment.NewLine, "<br/>");
                    var soluongtstn = i.SoLuongTaiSanJson.Replace(Environment.NewLine, "<br/>");
                    var giatritstn = i.GiaTriTSTNJson.Replace(Environment.NewLine, "<br/>");
                    var noidungtstn = i.NoiDungGiaiTrinhJson.Replace(Environment.NewLine, "<br/>");

                    BDTSGiayToKhac = $@"<tr>
                                            <td width='43%' valign=top style='width:43.48%;border:none;border-left:solid windowtext 1.0pt; border-right: 1px solid black; 
                                background:white;padding:0in 0in 0in 0in'>
                                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>{tenloaitstn}</span>
                                                </p>
                                            </td>
                                            <td width='18%' valign=top style='width:18.54%;border:none;background:white; border-right: 1px solid black; border-left: 1px solid black;
                                padding:0in 0in 0in 0in'>
                                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>{soluongtstn}</span>
                                                </p>
                                            </td>
                                            <td width='22%' valign=top style='width:22.52%;border:none;background:white; border-right: 1px solid black; border-left: 1px solid black;
                                padding:0in 0in 0in 0in'>
                                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>{giatritstn}</span>
                                                </p>
                                            </td>
                                            <td width='15%' valign=top style='width:15.46%;border:none;border-right:solid windowtext 1.0pt; border-left: 1px solid black;
                                background:white;padding:0in 0in 0in 0in'>
                                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>{noidungtstn}</span>
                                                </p>
                                            </td>
                                        </tr>";
                }

                
                foreach (var i in biendongtaisanqdpl)
                {
                    var tenloaitstn = i.TenLoaiTSTNJson.Replace(Environment.NewLine, "<br/>");
                    var soluongtstn = i.SoLuongTaiSanJson.Replace(Environment.NewLine, "<br/>");
                    var giatritstn = i.GiaTriTSTNJson.Replace(Environment.NewLine, "<br/>");
                    var noidungtstn = i.NoiDungGiaiTrinhJson.Replace(Environment.NewLine, "<br/>");

                    BDTSTaiSanPhapLuatQuyDinh = $@"<tr>
                                            <td width='43%' valign=top style='width:43.48%;border:none;border-left:solid windowtext 1.0pt; border-right: 1px solid black;
                                background:white;padding:0in 0in 0in 0in'>
                                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>{tenloaitstn}</span>
                                                </p>
                                            </td>
                                            <td width='18%' valign=top style='width:18.54%;border:none;background:white; border-right: 1px solid black; border-left: 1px solid black;
                                padding:0in 0in 0in 0in'>
                                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>{soluongtstn}</span>
                                                </p>
                                            </td>
                                            <td width='22%' valign=top style='width:22.52%;border:none;background:white; border-right: 1px solid black; border-left: 1px solid black;
                                padding:0in 0in 0in 0in'>
                                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>{giatritstn}</span>
                                                </p>
                                            </td>
                                            <td width='15%' valign=top style='width:15.46%;border:none;border-right:solid windowtext 1.0pt;  border-left: 1px solid black;
                                background:white;padding:0in 0in 0in 0in'>
                                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>{noidungtstn}</span>
                                                </p>
                                            </td>
                                        </tr>";
                }


                
                foreach (var i in biendongtaisankhac)
                {
                    var tenloaitstn = i.TenLoaiTSTNJson.Replace(Environment.NewLine, "<br/>");
                    var soluongtstn = i.SoLuongTaiSanJson.Replace(Environment.NewLine, "<br/>");
                    var giatritstn = i.GiaTriTSTNJson.Replace(Environment.NewLine, "<br/>");
                    var noidungtstn = i.NoiDungGiaiTrinhJson.Replace(Environment.NewLine, "<br/>");

                    BDTSDoMyNghe = $@"<tr>
                                            <td width='43%' valign=top style='width:43.48%;border:none;border-left:solid windowtext 1.0pt; border-right: 1px solid black; 
                                background:white;padding:0in 0in 0in 0in'>
                                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>{tenloaitstn}</span>
                                                </p>
                                            </td>
                                            <td width='18%' valign=top style='width:18.54%;border:none;background:white; border-right: 1px solid black; border-left: 1px solid black;
                                padding:0in 0in 0in 0in'>
                                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>{soluongtstn}</span>
                                                </p>
                                            </td>
                                            <td width='22%' valign=top style='width:22.52%;border:none;background:white; border-right: 1px solid black; border-left: 1px solid black;
                                padding:0in 0in 0in 0in'>
                                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>{giatritstn}</span>
                                                </p>
                                            </td>
                                            <td width='15%' valign=top style='width:15.46%;border:none;border-right:solid windowtext 1.0pt; border-left: 1px solid black;
                                background:white;padding:0in 0in 0in 0in'>
                                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>{noidungtstn}</span>
                                                </p>
                                            </td>
                                        </tr>";
                }

                
                foreach (var i in biendongtaisannuocngoai)
                {
                    var tenloaitstn = i.TenLoaiTSTNJson.Replace(Environment.NewLine, "<br/>");
                    var soluongtstn = i.SoLuongTaiSanJson.Replace(Environment.NewLine, "<br/>");
                    var giatritstn = i.GiaTriTSTNJson.Replace(Environment.NewLine, "<br/>");
                    var noidungtstn = i.NoiDungGiaiTrinhJson.Replace(Environment.NewLine, "<br/>");

                    BDTSTaiSanNuocNgoai = $@"<tr>
                                            <td width='43%' valign=top style='width:43.48%;border:none;border-left:solid windowtext 1.0pt; border-right: 1px solid black; 
                                background:white;padding:0in 0in 0in 0in'>
                                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>{tenloaitstn}</span>
                                                </p>
                                            </td>
                                            <td width='18%' valign=top style='width:18.54%;border:none;background:white; border-right: 1px solid black; border-left: 1px solid black;
                                padding:0in 0in 0in 0in'>
                                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>{soluongtstn}</span>
                                                </p>
                                            </td>
                                            <td width='22%' valign=top style='width:22.52%;border:none;background:white; border-right: 1px solid black; border-left: 1px solid black;
                                padding:0in 0in 0in 0in'>
                                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>{giatritstn}</span>
                                                </p>
                                            </td>
                                            <td width='15%' valign=top style='width:15.46%;border:none;border-right:solid windowtext 1.0pt; border-left: 1px solid black;
                                background:white;padding:0in 0in 0in 0in'>
                                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>{noidungtstn}</span>
                                                </p>
                                            </td>
                                        </tr>";
                }

                
                foreach (var i in biendongtaikhoannuocngoai)
                {
                    var tenloaitstn = i.TenLoaiTSTNJson.Replace(Environment.NewLine, "<br/>");
                    var soluongtstn = i.SoLuongTaiSanJson.Replace(Environment.NewLine, "<br/>");
                    var giatritstn = i.GiaTriTSTNJson.Replace(Environment.NewLine, "<br/>");
                    var noidungtstn = i.NoiDungGiaiTrinhJson.Replace(Environment.NewLine, "<br/>");

                    BDTSTaiKhoanNuocNgoai = $@"<tr>
                                            <td width='43%' valign=top style='width:43.48%;border:none;border-left:solid windowtext 1.0pt; border-right: 1px solid black;
                                background:white;padding:0in 0in 0in 0in'>
                                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>{tenloaitstn}</span>
                                                </p>
                                            </td>
                                            <td width='18%' valign=top style='width:18.54%;border:none;background:white; border-right: 1px solid black; border-left: 1px solid black;
                                padding:0in 0in 0in 0in'>
                                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>{soluongtstn}</span>
                                                </p>
                                            </td>
                                            <td width='22%' valign=top style='width:22.52%;border:none;background:white; border-right: 1px solid black; border-left: 1px solid black;
                                padding:0in 0in 0in 0in'>
                                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>{giatritstn}</span>
                                                </p>
                                            </td>
                                            <td width='15%' valign=top style='width:15.46%;border:none;border-right:solid windowtext 1.0pt; border-left: 1px solid black;
                                background:white;padding:0in 0in 0in 0in'>
                                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>{noidungtstn}</span>
                                                </p>
                                            </td>
                                        </tr>";
                }

               
                foreach (var i in biendongthunhap)
                {
                    var tenloaitstn = i.TenLoaiTSTNJson.Replace(Environment.NewLine, "<br/>");
                    var soluongtstn = i.SoLuongTaiSanJson.Replace(Environment.NewLine, "<br/>");
                    var giatritstn = i.GiaTriTSTNJson.Replace(Environment.NewLine, "<br/>");
                    var noidungtstn = i.NoiDungGiaiTrinhJson.Replace(Environment.NewLine, "<br/>");

                    BDTSTongThuNhap = $@"<tr>
                                    <td width='43%' valign=top style='width:43.48%;border:none;border-left:solid windowtext 1.0pt; border-right: 1px solid black;
                        background:white;padding:0in 0in 0in 0in'>
                                        <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>{tenloaitstn}</span>
                                        </p>
                                    </td>
                                    <td width='18%' valign=top style='width:18.54%;border:none;background:white; border-right: 1px solid black; border-left: 1px solid black;
                        padding:0in 0in 0in 0in'>
                                        <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>{soluongtstn}</span>
                                        </p>
                                    </td>
                                    <td width='22%' valign=top style='width:22.52%;border:none;background:white; border-right: 1px solid black; border-left: 1px solid black;
                        padding:0in 0in 0in 0in'>
                                        <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>{giatritstn}</span>
                                        </p>
                                    </td>
                                    <td width='15%' valign=top style='width:15.46%;border:none;border-right:solid windowtext 1.0pt; border-left: 1px solid black;
                        background:white;padding:0in 0in 0in 0in'>
                                        <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>{noidungtstn}</span>
                                        </p>
                                    </td>
                                </tr>";
                }

            }


            var biendongkk = "";
            if (bankekhai.Ma_Loai_KeKhai != 3)
            {
                if(bankekhai.Ma_Loai_KeKhai == 4 || bankekhai.Ma_Loai_KeKhai == 12)
                {
                  
                    biendongkk = $@" <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>10. Tổng thu nhập
                                            giữa hai lần kê khai:</span></p>

                                 {TongThuNhap_NguoiKeKhai} {TongThuNhap_VoHoacChong} {TongThuNhap_ConChuaThanhNien} {TongThuNhap_CacKhoanChung}
                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><b><span style='color:black'>III. BIẾN ĐỘNG TÀI
                                            SẢN, THU NHẬP; GIẢI TRÌNH NGUỒN GỐC CỦA
                                            TÀI SẢN, THU NHẬP TĂNG THÊM:</span></b> {((bankekhai.BienDongTaiSan != null && bankekhai.BienDongTaiSan != "") ? bankekhai.BienDongTaiSan : "Không" )}</p>

                                    <table class=MsoNormalTable border=1 cellspacing=0 cellpadding=0 width='100%'
                                        style='width:100.0%;border-collapse:collapse;border:none'>
                                        <thead>
                                            <tr>
                                                <td width='43%' rowspan=2 style=' width:43.48%;border:solid windowtext 1.0pt;
                                                border-right:none;background:white;padding:0in 0in 0in 0in; '>
                                                                    <p class=MsoNormal align=center style='margin-bottom:6.0pt;text-align:center;
                                                line-height:150%'><b><span style='color:black'>Loại tài sản, thu nhập</span></b></p>
                                                                </td>
                                                                <td width='41%' colspan=2 style='width:41.06%;border-top:solid windowtext 1.0pt;
                                                border-left:solid windowtext 1.0pt;border-bottom:none;border-right:none;
                                                background:white;padding:0in 0in 0in 0in; text-align: center;'>
                                                    <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%; text-align: center;'><b><span
                                                                style='color:black;'>Tăng/giảm</b> </span></p>
                                                </td>
                                                <td width='15%' style='width:15.46%;border:solid windowtext 1.0pt;
                                border-bottom:none;background:white;padding:0in 0in 0in 0in'>
                                                    <p class=MsoNormal align=center style='margin-bottom:6.0pt;text-align:center;
                                line-height:150%'><b><span style='color:black'>Nội dung giải trình nguồn gốc
                                                                của tài sản tăng thêm và tổng thu nhập</span></b></p>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width='18%' style='width:18.54%;border:solid windowtext 1.0pt;
                                border-right:none;background:white;padding:0in 0in 0in 0in'>
                                                    <p class=MsoNormal align=center style='margin-bottom:6.0pt;text-align:center;
                                line-height:150%'><b><span style='color:black'>Số lượng tài sản</span></b></p>
                                                </td>
                                                <td width='22%' style='width:22.52%;border:solid windowtext 1.0pt;
                                border-right:none;background:white;padding:0in 0in 0in 0in'>
                                                    <p class=MsoNormal align=center style='margin-bottom:6.0pt;text-align:center;
                                line-height:150%'><b><span style='color:black'>Giá trị tài sản, thu nhập</span></b></p>
                                                </td>
                                                <td width='15%' style='width:15.46%;border:solid windowtext 1.0pt;
                                padding:0in 0in 0in 0in'>
                                                    <p class=MsoNormal align=center style='margin-bottom:6.0pt;text-align:center;
                                line-height:150%'><span style='color:black'>&nbsp;</span></p>
                                                </td>
                                            </tr>
                                        </thead>
                                        <tr>
                                            <td width='43%' valign=top style='width:43.48%;border:none;border-left:solid windowtext 1.0pt; border-right: 1px solid black; 
                                background:white;padding:0in 0in 0in 0in'>
                                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>1. Quyền
                                                        sử dụng thực tế đối với đất</span></p>
                                            </td>
                                            <td width='18%' valign=top style='width:18.54%;border:none;background:white; border-right: 1px solid black; border-left: 1px solid black;
                                padding:0in 0in 0in 0in'>
                                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span
                                                        style='color:black'>&nbsp;</span></p>
                                            </td>
                                            <td width='22%' valign=top style='width:22.52%;border:none;background:white; border-right: 1px solid black; border-left: 1px solid black;
                                padding:0in 0in 0in 0in'>
                                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span
                                                        style='color:black'>&nbsp;</span></p>
                                            </td>
                                            <td width='15%' valign=top style='width:15.46%;border:none;border-right:solid windowtext 1.0pt; border-left: 1px solid black;
                                background:white;padding:0in 0in 0in 0in'>
                                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span
                                                        style='color:black'>&nbsp;</span></p>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width='43%' valign=top style='width:43.48%;border:none;border-left:solid windowtext 1.0pt;  border-right: 1px solid black; 
                                background:white;padding:0in 0in 0in 0in'>
                                                <p class=MsoNormal style='margin-top:0in;margin-right:0in;margin-bottom:6.0pt;
                                margin-left:21.0pt;text-indent:-21.0pt;line-height:150%'><span style='color:black'>1.1.<span style='font:7.0pt '
                                                            Times New Roman''>&nbsp; </span></span><span style='color:black'>Đất ở</span></p>
                                            </td>
                                            <td width='18%' valign=top style='width:18.54%;border:none;background:white; border-right: 1px solid black; border-left: 1px solid black;
                                padding:0in 0in 0in 0in'>
                                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span
                                                        style='color:black'>&nbsp;</span></p>
                                            </td>
                                            <td width='22%' valign=top style='width:22.52%;border:none;background:white; border-right: 1px solid black; border-left: 1px solid black;
                                padding:0in 0in 0in 0in'>
                                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span
                                                        style='color:black'>&nbsp;</span></p>
                                            </td>
                                            <td width='15%' valign=top style='width:15.46%;border:none;border-right:solid windowtext 1.0pt;  border-left: 1px solid black;
                                background:white;padding:0in 0in 0in 0in'>
                                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span
                                                        style='color:black'>&nbsp;</span></p>
                                            </td>
                                        </tr>
                                        {BDTSDatO}
                                        <tr >
                                            <td width='43%' valign=top style='width:43.48%;border:none;border-left:solid windowtext 1.0pt;  border-right: 1px solid black;
                                background:white;padding:0in 0in 0in 0in'>
                                                <p class=MsoNormal style='margin-top:0in;margin-right:0in;margin-bottom:6.0pt;
                                margin-left:21.0pt;text-indent:-21.0pt;line-height:150%'><span style='color:black'>1.2.<span style='font:7.0pt '
                                                            Times New Roman''>&nbsp; </span></span><span style='color:black'>Các loại đất
                                                        khác</span></p>
                                            </td>
                                            <td width='18%' valign=top style='width:18.54%;border:none;background:white;  border-right: 1px solid black; border-left: 1px solid black;
                                padding:0in 0in 0in 0in'>
                                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span
                                                        style='color:black'>&nbsp;</span></p>
                                            </td>
                                            <td width='22%' valign=top style='width:22.52%;border:none;background:white;  border-right: 1px solid black; border-left: 1px solid black;
                                padding:0in 0in 0in 0in'>
                                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span
                                                        style='color:black'>&nbsp;</span></p>
                                            </td>
                                            <td width='15%' valign=top style='width:15.46%;border:none;border-right:solid windowtext 1.0pt;  border-left: 1px solid black;
                                background:white;padding:0in 0in 0in 0in'>
                                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span
                                                        style='color:black'>&nbsp;</span></p>
                                            </td>
                                        </tr>
                                        {BDTSDatKhac}
                                        
                                        <tr>
                                            <td width='43%' valign=top style='width:43.48%;border:none;border-left:solid windowtext 1.0pt; border-top: 1px solid black ; border-right: 1px solid black;
                                background:white;padding:0in 0in 0in 0in'>
                                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>2. Nhà ở,
                                                        công trình xây dựng</span></p>
                                            </td>
                                            <td width='18%' valign=top style='width:18.54%;border:none;background:white; border-top: 1px solid black ; border-right: 1px solid black; border-left: 1px solid black;
                                padding:0in 0in 0in 0in'>
                                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span
                                                        style='color:black'>&nbsp;</span></p>
                                            </td>
                                            <td width='22%' valign=top style='width:22.52%;border:none;background:white; border-top: 1px solid black ; border-right: 1px solid black; border-left: 1px solid black;
                                padding:0in 0in 0in 0in'>
                                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span
                                                        style='color:black'>&nbsp;</span></p>
                                            </td>
                                            <td width='15%' valign=top style='width:15.46%;border:none;border-right:solid windowtext 1.0pt; border-top: 1px solid black ; border-left: 1px solid black;
                                background:white;padding:0in 0in 0in 0in'>
                                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span
                                                        style='color:black'>&nbsp;</span></p>
                                            </td>
                                        </tr>
                                        
                                        <tr>
                                            <td width='43%' valign=top style='width:43.48%;border:none;border-left:solid windowtext 1.0pt;  border-right: 1px solid black; 
                                background:white;padding:0in 0in 0in 0in'>
                                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>2.1. Nhà
                                                        ở</span></p>
                                            </td>
                                            <td width='18%' valign=top style='width:18.54%;border:none;background:white;  border-right: 1px solid black; border-left: 1px solid black;
                                padding:0in 0in 0in 0in'>
                                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span
                                                        style='color:black'>&nbsp;</span></p>
                                            </td>
                                            <td width='22%' valign=top style='width:22.52%;border:none;background:white;  border-right: 1px solid black; border-left: 1px solid black;
                                padding:0in 0in 0in 0in'>
                                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span
                                                        style='color:black'>&nbsp;</span></p>
                                            </td>
                                            <td width='15%' valign=top style='width:15.46%;border:none;border-right:solid windowtext 1.0pt; border-left: 1px solid black;
                                background:white;padding:0in 0in 0in 0in'>
                                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span
                                                        style='color:black'>&nbsp;</span></p>
                                            </td>
                                        </tr>
                                        {BDTSNhaO}
                                       
                                        <tr>
                                            <td width='43%' valign=top style='width:43.48%;border:none;border-left:solid windowtext 1.0pt;  border-right: 1px solid black; 
                                background:white;padding:0in 0in 0in 0in'>
                                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>2.2. Công
                                                        trình xây dựng khác</span></p>
                                            </td>
                                            <td width='18%' valign=top style='width:18.54%;border:none;background:white;  border-right: 1px solid black; border-left: 1px solid black;
                                padding:0in 0in 0in 0in'>
                                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span
                                                        style='color:black'>&nbsp;</span></p>
                                            </td>
                                            <td width='22%' valign=top style='width:22.52%;border:none;background:white; border-right: 1px solid black; border-left: 1px solid black;
                                padding:0in 0in 0in 0in'>
                                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span
                                                        style='color:black'>&nbsp;</span></p>
                                            </td>
                                            <td width='15%' valign=top style='width:15.46%;border:none;border-right:solid windowtext 1.0pt; border-left: 1px solid black;
                                background:white;padding:0in 0in 0in 0in'>
                                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span
                                                        style='color:black'>&nbsp;</span></p>
                                            </td>
                                        </tr>
                                    {BDTSCongTrinhKhach}
                                       
                                        <tr>
                                            <td width='43%' valign=top style='width:43.48%;border:none;border-left:solid windowtext 1.0pt; border-top: 1px solid black;  border-right: 1px solid black; 
                                background:white;padding:0in 0in 0in 0in'>
                                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>3. Tài sản
                                                        khác gắn liền với đất</span></p>
                                            </td>
                                            <td width='18%' valign=top style='width:18.54%;border:none;background:white; border-top: 1px solid black ; border-right: 1px solid black; border-left: 1px solid black;
                                padding:0in 0in 0in 0in'>
                                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span
                                                        style='color:black'>&nbsp;</span></p>
                                            </td>
                                            <td width='22%' valign=top style='width:22.52%;border:none;background:white; border-top: 1px solid black ; border-right: 1px solid black; border-left: 1px solid black;
                                padding:0in 0in 0in 0in'>
                                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span
                                                        style='color:black'>&nbsp;</span></p>
                                            </td>
                                            <td width='15%' valign=top style='width:15.46%;border:none;border-right:solid windowtext 1.0pt; border-top: 1px solid black;  border-left: 1px solid black;
                                background:white;padding:0in 0in 0in 0in'>
                                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span
                                                        style='color:black'>&nbsp;</span></p>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width='43%' valign=top style='width:43.48%;border:none;border-left:solid windowtext 1.0pt;  border-right: 1px solid black; 
                                background:white;padding:0in 0in 0in 0in'>
                                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>3.1. Cây
                                                        lâu năm, rừng sản xuất</span></p>
                                                
                                            </td>
                                            <td width='18%' valign=top style='width:18.54%;border:none;background:white;  border-right: 1px solid black; border-left: 1px solid black;
                                padding:0in 0in 0in 0in'>
                                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span
                                                        style='color:black'>&nbsp;</span></p>
                                            </td>
                                            <td width='22%' valign=top style='width:22.52%;border:none;background:white;  border-right: 1px solid black; border-left: 1px solid black;
                                padding:0in 0in 0in 0in'> 
                                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span
                                                        style='color:black'>&nbsp;</span></p>
                                            </td>
                                            <td width='15%' valign=top style='width:15.46%;border:none;border-right:solid windowtext 1.0pt; border-left: 1px solid black;
                                background:white;padding:0in 0in 0in 0in'>
                                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span
                                                        style='color:black'>&nbsp;</span></p>
                                            </td>
                                        </tr>
                                        {BDTSCayLauNam}
                                       {BDTSRungSanXuat}
                                        <tr>
                                            <td width='43%' valign=top style='width:43.48%;border:none;border-left:solid windowtext 1.0pt;  border-right: 1px solid black;
                                background:white;padding:0in 0in 0in 0in'>
                                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>3.2. Vật
                                                        kiến trúc gắn liền với đất</span></p>
                                            </td>
                                            <td width='18%' valign=top style='width:18.54%;border:none;background:white;  border-right: 1px solid black; border-left: 1px solid black;
                                padding:0in 0in 0in 0in'>
                                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span
                                                        style='color:black'>&nbsp;</span></p>
                                            </td>
                                            <td width='22%' valign=top style='width:22.52%;border:none;background:white;  border-right: 1px solid black; border-left: 1px solid black;
                                padding:0in 0in 0in 0in'>
                                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span
                                                        style='color:black'>&nbsp;</span></p>
                                            </td>
                                            <td width='15%' valign=top style='width:15.46%;border:none;border-right:solid windowtext 1.0pt; border-left: 1px solid black;
                                background:white;padding:0in 0in 0in 0in'>
                                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span
                                                        style='color:black'>&nbsp;</span></p>
                                            </td>
                                        </tr>
                                        {BDTSVatKienTruc}
                                       
                                        <tr>
                                            <td width='43%' valign=top style='width:43.48%;border:none;border-left:solid windowtext 1.0pt; border-top: 1px solid black;  border-right: 1px solid black; 
                                background:white;padding:0in 0in 0in 0in'>
                                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>4. Vàng,
                                                        kim cương, bạch kim và các kim loại quý, đá quý
                                                        khác có tổng giá trị từ 50 triệu đồng trở lên</span></p>
                                            </td>
                                            <td width='18%' valign=top style='width:18.54%;border:none;background:white; border-top: 1px solid black; border-right: 1px solid black; border-left: 1px solid black;
                                padding:0in 0in 0in 0in'>
                                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span
                                                        style='color:black'>&nbsp;</span></p>
                                            </td>
                                            <td width='22%' valign=top style='width:22.52%;border:none;background:white; border-top: 1px solid black; border-right: 1px solid black; border-left: 1px solid black;
                                padding:0in 0in 0in 0in'>
                                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span
                                                        style='color:black'>&nbsp;</span></p>
                                            </td>
                                            <td width='15%' valign=top style='width:15.46%;border:none;border-right:solid windowtext 1.0pt; border-top: 1px solid black;  border-left: 1px solid black;
                                background:white;padding:0in 0in 0in 0in'>
                                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span
                                                        style='color:black'>&nbsp;</span></p>
                                            </td>
                                        </tr>
                                        {BDTSTrangSuc}
                                        
                                        <tr>
                                            <td width='43%' valign=top style='width:43.48%;border:none;border-left:solid windowtext 1.0pt; border-top: 1px solid black; border-right: 1px solid black; 
                                background:white;padding:0in 0in 0in 0in'>
                                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>5. Tiền
                                                        (tiền Việt Nam, ngoại tệ) gồm tiền mặt, tiền cho
                                                        vay, tiền trả trước, tiền gửi cá nhân, tổ chức trong nước, tổ chức nước ngoài
                                                        tại Việt Nam mà tổng giá trị quy đổi từ 50 triệu đồng trở lên.</span></p>
                                            </td>
                                            <td width='18%' valign=top style='width:18.54%;border:none;background:white; border-top: 1px solid black; border-right: 1px solid black; border-left: 1px solid black;
                                padding:0in 0in 0in 0in'>
                                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span
                                                        style='color:black'>&nbsp;</span></p>
                                            </td>
                                            <td width='22%' valign=top style='width:22.52%;border:none;background:white; border-top: 1px solid black; border-right: 1px solid black; border-left: 1px solid black;
                                padding:0in 0in 0in 0in'>
                                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span
                                                        style='color:black'>&nbsp;</span></p>
                                            </td>
                                            <td width='15%' valign=top style='width:15.46%;border:none;border-right:solid windowtext 1.0pt; border-top: 1px solid black;  border-left: 1px solid black;
                                background:white;padding:0in 0in 0in 0in'>
                                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span
                                                        style='color:black'>&nbsp;</span></p>
                                            </td>
                                        </tr>
                                        {BDTSTien}
                                        
                                        <tr>
                                            <td width='43%' valign=top style='width:43.48%;border:none;border-left:solid windowtext 1.0pt; border-top: 1px solid black; border-right: 1px solid black;
                                background:white;padding:0in 0in 0in 0in'>
                                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>6. Cổ
                                                        phiếu, trái phiếu, vốn góp, các loại giấy tờ có giá
                                                        khác mà tổng giá trị từ 50 triệu đồng trở lên (khai theo từng loại):</span></p>
                                            </td>
                                            <td width='18%' valign=top style='width:18.54%;border:none;background:white; border-top: 1px solid black; border-right: 1px solid black; border-left: 1px solid black;
                                padding:0in 0in 0in 0in'>
                                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span
                                                        style='color:black'>&nbsp;</span></p>
                                            </td>
                                            <td width='22%' valign=top style='width:22.52%;border:none;background:white; border-top: 1px solid black; border-right: 1px solid black; border-left: 1px solid black;
                                padding:0in 0in 0in 0in'>
                                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span
                                                        style='color:black'>&nbsp;</span></p>
                                            </td>
                                            <td width='15%' valign=top style='width:15.46%;border:none;border-right:solid windowtext 1.0pt; border-top: 1px solid black; border-left: 1px solid black;
                                background:white;padding:0in 0in 0in 0in'>
                                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span
                                                        style='color:black'>&nbsp;</span></p>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width='43%' valign=top style='width:43.48%;border:none;border-left:solid windowtext 1.0pt; border-right: 1px solid black; 
                                background:white;padding:0in 0in 0in 0in'>
                                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>6.1. Cổ
                                                        phiếu</span></p>
                                            </td>
                                            <td width='18%' valign=top style='width:18.54%;border:none;background:white; border-right: 1px solid black; border-left: 1px solid black;
                                padding:0in 0in 0in 0in'>
                                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span
                                                        style='color:black'>&nbsp;</span></p>
                                            </td>
                                            <td width='22%' valign=top style='width:22.52%;border:none;background:white; border-right: 1px solid black; border-left: 1px solid black;
                                padding:0in 0in 0in 0in'>
                                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span
                                                        style='color:black'>&nbsp;</span></p>
                                            </td>
                                            <td width='15%' valign=top style='width:15.46%;border:none;border-right:solid windowtext 1.0pt;  border-left: 1px solid black;
                                background:white;padding:0in 0in 0in 0in'>
                                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span
                                                        style='color:black'>&nbsp;</span></p>
                                            </td>
                                        </tr>
                                        {BDTSCoPhieu}
                                        <tr>
                                            <td width='43%' valign=top style='width:43.48%;border:none;border-left:solid windowtext 1.0pt;  border-right: 1px solid black;
                                background:white;padding:0in 0in 0in 0in'>
                                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>6.2. Trái
                                                        phiếu</span></p>
                                               
                                            </td>
                                            <td width='18%' valign=top style='width:18.54%;border:none;background:white;  border-right: 1px solid black; border-left: 1px solid black;
                                padding:0in 0in 0in 0in'>
                                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span
                                                        style='color:black'>&nbsp;</span></p>
                                            </td>
                                            <td width='22%' valign=top style='width:22.52%;border:none;background:white;  border-right: 1px solid black; border-left: 1px solid black;
                                padding:0in 0in 0in 0in'>
                                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span
                                                        style='color:black'>&nbsp;</span></p>
                                            </td>
                                            <td width='15%' valign=top style='width:15.46%;border:none;border-right:solid windowtext 1.0pt; border-left: 1px solid black;
                                background:white;padding:0in 0in 0in 0in'>
                                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span
                                                        style='color:black'>&nbsp;</span></p>
                                            </td>
                                        </tr>
                                        {BDTSTraiPhieu}
                                        <tr>
                                            <td width='43%' valign=top style='width:43.48%;border:none;border-left:solid windowtext 1.0pt;  border-right: 1px solid black; 
                                background:white;padding:0in 0in 0in 0in'>
                                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>6.3. Vốn
                                                        góp</span></p>
                                            </td>
                                            <td width='18%' valign=top style='width:18.54%;border:none;background:white;  border-right: 1px solid black; border-left: 1px solid black;
                                padding:0in 0in 0in 0in'>
                                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span
                                                        style='color:black'>&nbsp;</span></p>
                                            </td>
                                            <td width='22%' valign=top style='width:22.52%;border:none;background:white;  border-right: 1px solid black; border-left: 1px solid black;
                                padding:0in 0in 0in 0in'>
                                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span
                                                        style='color:black'>&nbsp;</span></p>
                                            </td>
                                            <td width='15%' valign=top style='width:15.46%;border:none;border-right:solid windowtext 1.0pt; border-left: 1px solid black;
                                background:white;padding:0in 0in 0in 0in'>
                                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span
                                                        style='color:black'>&nbsp;</span></p>
                                            </td>
                                        </tr>
                                        {BDTSVonGop}
                                        
                                        <tr>
                                            <td width='43%' valign=top style='width:43.48%;border:none;border-left:solid windowtext 1.0pt;  border-right: 1px solid black; 
                                background:white;padding:0in 0in 0in 0in'>
                                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>6.4. Các
                                                        loại giấy tờ có giá khác</span></p>
                                            </td>
                                            <td width='18%' valign=top style='width:18.54%;border:none;background:white;  border-right: 1px solid black; border-left: 1px solid black;
                                padding:0in 0in 0in 0in'>
                                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span
                                                        style='color:black'>&nbsp;</span></p>
                                            </td>
                                            <td width='22%' valign=top style='width:22.52%;border:none;background:white;  border-right: 1px solid black; border-left: 1px solid black;
                                padding:0in 0in 0in 0in'>
                                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span
                                                        style='color:black'>&nbsp;</span></p>
                                            </td>
                                            <td width='15%' valign=top style='width:15.46%;border:none;border-right:solid windowtext 1.0pt;   border-left: 1px solid black;
                                background:white;padding:0in 0in 0in 0in'>
                                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span
                                                        style='color:black'>&nbsp;</span></p>
                                            </td>
                                        </tr>
                                        {BDTSGiayToKhac}
                                       
                                        <tr>
                                            <td width='43%' valign=top style='width:43.48%;border:none;border-left:solid windowtext 1.0pt; border-top: 1px solid black; border-right: 1px solid black;
                                background:white;padding:0in 0in 0in 0in'>
                                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>7. Tài sản
                                                        khác có giá trị từ 50 triệu đồng trở lên:</span></p>
                                            </td>
                                            <td width='18%' valign=top style='width:18.54%;border:none;background:white; border-top: 1px solid black; border-right: 1px solid black; border-left: 1px solid black;
                                padding:0in 0in 0in 0in'>
                                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span
                                                        style='color:black'>&nbsp;</span></p>
                                            </td>
                                            <td width='22%' valign=top style='width:22.52%;border:none;background:white; border-top: 1px solid black; border-right: 1px solid black; border-left: 1px solid black;
                                padding:0in 0in 0in 0in'>
                                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span
                                                        style='color:black'>&nbsp;</span></p>
                                            </td>
                                            <td width='15%' valign=top style='width:15.46%;border:none;border-right:solid windowtext 1.0pt; border-top: 1px solid black; border-left: 1px solid black;
                                background:white;padding:0in 0in 0in 0in'>
                                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span
                                                        style='color:black'>&nbsp;</span></p>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width='43%' valign=top style='width:43.48%;border:none;border-left:solid windowtext 1.0pt; border-right: 1px solid black; 
                                background:white;padding:0in 0in 0in 0in'>
                                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>7.1. Tài
                                                        sản theo quy định của pháp luật phải đăng ký sử
                                                        dụng và được cấp giấy đăng ký (tầu bay, tàu thủy, thuyền, máy ủi, máy xúc, ô
                                                        tô, mô tô, xe gắn máy...).</span></p>
                                            </td>
                                            <td width='18%' valign=top style='width:18.54%;border:none;background:white; border-right: 1px solid black; border-left: 1px solid black;
                                padding:0in 0in 0in 0in'>
                                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span
                                                        style='color:black'>&nbsp;</span></p>
                                            </td>
                                            <td width='22%' valign=top style='width:22.52%;border:none;background:white; border-right: 1px solid black; border-left: 1px solid black;
                                padding:0in 0in 0in 0in'>
                                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span
                                                        style='color:black'>&nbsp;</span></p>
                                            </td>
                                            <td width='15%' valign=top style='width:15.46%;border:none;border-right:solid windowtext 1.0pt; border-left: 1px solid black;
                                background:white;padding:0in 0in 0in 0in'>
                                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span
                                                        style='color:black'>&nbsp;</span></p>
                                            </td>
                                        </tr>
                                        {BDTSTaiSanPhapLuatQuyDinh}
                                       
                                        <tr>
                                            <td width='43%' valign=top style='width:43.48%;border:none;border-left:solid windowtext 1.0pt;  border-right: 1px solid black;
                                background:white;padding:0in 0in 0in 0in'>
                                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>7.2. Tài
                                                        sản khác (đồ mỹ nghệ, đồ thờ cúng, bàn ghế, cây
                                                        cảnh, tranh ảnh, các loại tài sản khác).</span></p>
                                            </td>
                                            <td width='18%' valign=top style='width:18.54%;border:none;background:white;  border-right: 1px solid black; border-left: 1px solid black;
                                padding:0in 0in 0in 0in'>
                                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span
                                                        style='color:black'>&nbsp;</span></p>
                                            </td>
                                            <td width='22%' valign=top style='width:22.52%;border:none;background:white;  border-right: 1px solid black; border-left: 1px solid black;
                                padding:0in 0in 0in 0in'>
                                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span
                                                        style='color:black'>&nbsp;</span></p>
                                            </td>
                                            <td width='15%' valign=top style='width:15.46%;border:none;border-right:solid windowtext 1.0pt;  border-left: 1px solid black;
                                background:white;padding:0in 0in 0in 0in'>
                                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span
                                                        style='color:black'>&nbsp;</span></p>
                                            </td>
                                        </tr>
                                         {BDTSDoMyNghe}
                                       
                                        <tr>
                                            <td width='43%' valign=top style='width:43.48%;border:none;border-left:solid windowtext 1.0pt; border-top: 1px solid black; border-right: 1px solid black;
                                background:white;padding:0in 0in 0in 0in'>
                                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>8. Tài sản
                                                        ở nước ngoài.</span></p>
                                            </td>
                                            <td width='18%' valign=top style='width:18.54%;border:none;background:white; border-top: 1px solid black; border-right: 1px solid black; border-left: 1px solid black;
                                padding:0in 0in 0in 0in'>
                                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span
                                                        style='color:black'>&nbsp;</span></p>
                                            </td>
                                            <td width='22%' valign=top style='width:22.52%;border:none;background:white; border-top: 1px solid black; border-right: 1px solid black; border-left: 1px solid black;
                                padding:0in 0in 0in 0in'>
                                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span
                                                        style='color:black'>&nbsp;</span></p>
                                            </td>
                                            <td width='15%' valign=top style='width:15.46%;border:none;border-right:solid windowtext 1.0pt; border-top: 1px solid black;  border-left: 1px solid black;
                                background:white;padding:0in 0in 0in 0in'>
                                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span
                                                        style='color:black'>&nbsp;</span></p>
                                            </td>
                                        </tr>
                                        {BDTSTaiSanNuocNgoai}
                                        
                                        
                                        <tr>
                                            <td width='43%' valign=top style='width:43.48%;border:none;border-left:solid windowtext 1.0pt; border-top: 1px solid black; border-right: 1px solid black; 
                                background:white;padding:0in 0in 0in 0in'>
                                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>9. Tổng
                                                        thu nhập giữa hai lần kê khai.</span></p>
                                            </td>
                                            <td width='18%' valign=top style='width:18.54%;border:none;background:white; border-top: 1px solid black; border-right: 1px solid black; border-left: 1px solid black;
                                padding:0in 0in 0in 0in'>
                                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span
                                                        style='color:black'>&nbsp;</span></p>
                                            </td>
                                            <td width='22%' valign=top style='width:22.52%;border:none;background:white; border-top: 1px solid black; border-right: 1px solid black; border-left: 1px solid black;
                                padding:0in 0in 0in 0in'>
                                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span
                                                        style='color:black'>&nbsp;</span></p>
                                            </td>
                                            <td width='15%' valign=top style='width:15.46%;border:none;border-right:solid windowtext 1.0pt; border-top: 1px solid black;  border-left: 1px solid black;
                                background:white;padding:0in 0in 0in 0in'>
                                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span
                                                        style='color:black'>&nbsp;</span></p>
                                            </td>
                                        </tr>
                                        {BDTSTongThuNhap}
                                        <tr>
                                            <td width='43%' valign=top style='border-bottom: 1px solid black'>
                                                
                                            </td>
                                            <td width='18%' valign=top style='border-bottom: 1px solid black'>
                                                
                                            </td>
                                            <td width='22%' valign=top style='border-bottom: 1px solid black'>
                                                
                                            </td>
                                            <td width='15%' valign=top style='border-bottom: 1px solid black'>
                                                
                                            </td>
                                        </tr>
                                    </table>";
                    var html = $@"<html>
                        <head>
                            <meta http-equiv=Content-Type content='text/html; charset=utf-8'>
                            <meta name=Generator content='Microsoft Word 15 (filtered)'>
                            <style>
                                <!--
                                /* Font Definitions */
                                @font-face {{
                                    font-family: 'Cambria Math';
                                    panose-1: 2 4 5 3 5 4 6 3 2 4;
                                }}
                                html{{  margin: 0.71cm 0.76cm 0.75cm 1.73cm;}}
                                th,td,p,div,b {{margin:0;padding:0}}
                                /* Style Definitions */
                                p.MsoNormal,
                                li.MsoNormal,
                                div.MsoNormal, p, span, b, tr, td {{
                                    margin: 0in;
                                    font-size: 17px;
                                    font-family: 'Times New Roman', serif;
                                }}

                                /* Page Definitions */
                                @page WordSection1 {{
                                    size: 8.5in 11.0in;
                                }}

                                div.WordSection1 {{
                                    page: WordSection1;
                                }}
                                -->
                            </style>

                        </head>

                        <body lang=EN-US style='word-wrap:break-word'>

                            <div class=WordSection1>

                                <table class=MsoNormalTable border=1 cellspacing=0 cellpadding=0 
                                    style='width:100%;border-collapse:collapse;border:none'>
                                    <tr>
                                        <td  valign=top style='width:40%;border:none;padding:0; margin: 0;'>
                                            {CoQuanTrucThuoc}
                                                       
                                        </td>
                                        <td  valign=top style='width:60%;border:none;padding:0; margin: 0;'>
                                            <p class=MsoNormal align=center style='margin-bottom:6.0pt;text-align:center;
                          line-height:150%;background:white'><b><span style='color:black'>CỘNG HÒA XÃ
                                                        HỘI CHỦ NGHĨA VIỆT NAM<br>
                                                        Độc lập - Tự do - Hạnh phúc <br>
                                                        ---------------</span></b></p>
                                        </td>
                                    </tr>
                                </table>

                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>&nbsp;</span></p>

                                <p class=MsoNormal align=center style='margin-bottom:6.0pt;text-align:center;
                        line-height:150%'><b><span style='color:black'>BẢN KÊ KHAI TÀI SẢN, THU NHẬP </span></b><b><span lang=VI
                                            style='color:black'>{LoaiKeKhai}</span<span
                                            style='color:black'><br>(Ngày</span></b><b><span style='color:black'> </span><span
                                            style='color:black'>{data2.bankekhai.Ngay_KeKhai} tháng {data2.bankekhai.Thang_KeKhai} năm {data2.bankekhai.Nam_KeKhai})</span></b></p>

                               

                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><b><span style='color:black'>I. THÔNG TIN
                                            CHUNG</span></b></p>

                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>1. Người kê khai
                                            tài sản, thu nhập</span></p>
                                {ThongTinCanBo}  {VoChong} {Con} 
                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><b><span style='color:black'>II. THÔNG TIN MÔ TẢ
                                            VỀ TÀI SẢN</span></b></p>

                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>1. Quyền sử dụng
                                            thực tế đối với đất</span>:</p>
                                 <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>1.1. Đất
                                                    ở</span><span style='color:black'>:</span></p>

                                {DatO}
                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>1.2. Các loại đất
                                            khác </span><span lang=VI style='color:black'>:</span></p>

                                {CacLoaiDatKhac}
                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>2. Nhà ở, công
                                            trình xây dựng:</span></p>

                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>2.1. Nhà
                                            ở:</span></p>

                                {NhaO}

                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>2.2. Công trình xây
                                            dựng khác</span></p>

                                {CongTrinhXayDung}
                                
                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>3. Tài sản khác gắn
                                            liền với đất:</span></p>
                                {TaiSanGanVoiDat}

                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>3.1. Cây lâu
                                            năm</span>:</p>

                                <table class=MsoNormalTable border=0 cellspacing=0 cellpadding=0 width='100%'
                                    style='width:100.0%;border-collapse:collapse'>
                                    {CayLauNam}
                                </table>

                              

                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>3.2. Rừng sản
                                            xuất:</span></p>

                                {RungSanXuat}

                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>3.3. Vật kiến trúc
                                            khác gắn liền với đất</span><span style='color:black'>:</span></p>

                                {VatKienTruc}
                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>4. Vàng, kim cương,
                                            bạch kim và các kim loại quý, đá quý
                                            khác có tổng giá trị từ 50 triệu đồng trở lên:</span></p>
                                {Vang}
                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>5. Tiền (tiền Việt
                                            Nam, ngoại tệ) gồm tiền mặt, tiền cho
                                            vay, tiền trả trước, tiền gửi cá nhân, tổ chức trong nước, tổ chức nước ngoài
                                            tại Việt Nam mà tổng giá trị quy đổi từ 50 triệu đồng trở lên:</span></p>
                                {Tien}
                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>6. Cổ phiếu, trái
                                            phiếu, vốn góp, các loại giấy tờ có giá
                                            khác mà tổng giá trị từ 50 triệu đồng trở lên (khai theo từng loại):</span></p>

                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>6.1. Cổ phiếu:</span>
                                </p>
                                
                                {CoPhieu}

                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>6.2. Trái
                                        phiếu:</span></p>
                                {TraiPhieu}

                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>6.3. Vốn
                                        góp:</span></p>
                               {VonGop}

                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>6.4. Các loại giấy tờ
                                        có giá khác: </span></p>

                                {GiayToKhac}

                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>7. Tài sản khác mà
                                            mỗi tài sản có giá trị từ 50 triệu đồng
                                            trở lên, bao gồm:</span></p>

                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>7.1. Tài sản theo quy
                                        định của pháp luật phải đăng ký sử
                                        dụng và được cấp giấy đăng ký (tầu bay, tầu thủy, thuyền, máy ủi, máy xúc, ô
                                        tô, mô tô, xe gắn máy...):</span></p>
                                {TaiSanPhapLuatQuyDinh}

                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>7.2. Tài sản khác (đồ
                                        mỹ nghệ, đồ thờ cúng, bàn ghế, cây
                                        cảnh, tranh, ảnh, các loại tài sản khác):</span></p>

                                  {TaiSanKhac}

                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>8. Tài sản ở nước
                                            ngoài:</span></p>







                                 <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>8.1. Quyền sử dụng
                                            thực tế đối với đất</span>:</p>
                                 <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>8.1.1. Đất
                                                    ở</span><span style='color:black'> 
                                                :</span></p>

                                {tsnnDatO}
                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>8.1.2. Các loại đất
                                            khác </span><span lang=VI style='color:black'>:</span></p>

                                {tsnnCacLoaiDatKhac}
                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>8.2. Nhà ở, công
                                            trình xây dựng:</span></p>

                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>8.2.1. Nhà
                                            ở:</span></p>

                                {tsnnNhaO}

                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>8.2.2. Công trình xây
                                            dựng khác</span></p>

                                {tsnnCongTrinhXayDung}
                                
                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>8.3. Tài sản khác gắn
                                            liền với đất:</span></p>
                                {tsnnTaiSanGanVoiDat}

                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>8.3.1. Cây lâu
                                            năm</span>:</p>

                                <table class=MsoNormalTable border=0 cellspacing=0 cellpadding=0 width='100%'
                                    style='width:100.0%;border-collapse:collapse'>
                                    {tsnnCayLauNam}
                                </table>

                              

                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>8.3.2. Rừng sản
                                            xuất:</span></p>

                                {tsnnRungSanXuat}

                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>8.3.3. Vật kiến trúc
                                            khác gắn liền với đất</span><span style='color:black'>:</span></p>

                                {tsnnVatKienTruc}
                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>8.4. Vàng, kim cương,
                                            bạch kim và các kim loại quý, đá quý
                                            khác có tổng giá trị từ 50 triệu đồng trở lên:</span></p>
                                {tsnnVang}
                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>8.5. Tiền (tiền Việt
                                            Nam, ngoại tệ) gồm tiền mặt, tiền cho
                                            vay, tiền trả trước, tiền gửi cá nhân, tổ chức trong nước, tổ chức nước ngoài
                                            tại Việt Nam mà tổng giá trị quy đổi từ 50 triệu đồng trở lên:</span></p>
                                {tsnnTien}
                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>8.6. Cổ phiếu, trái
                                            phiếu, vốn góp, các loại giấy tờ có giá
                                            khác mà tổng giá trị từ 50 triệu đồng trở lên (khai theo từng loại):</span></p>

                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>8.6.1. Cổ phiếu:</span>
                                </p>
                                
                                {tsnnCoPhieu}

                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>8.6.2. Trái
                                        phiếu:</span></p>
                                {tsnnTraiPhieu}

                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>8.6.3. Vốn
                                        góp:</span></p>
                               {tsnnVonGop}

                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>8.6.4. Các loại giấy tờ
                                        có giá khác: </span></p>

                                {tsnnGiayToKhac}

                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>8.7. Tài sản khác mà
                                            mỗi tài sản có giá trị từ 50 triệu đồng
                                            trở lên, bao gồm:</span></p>

                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>8.7.1. Tài sản theo quy
                                        định của pháp luật phải đăng ký sử
                                        dụng và được cấp giấy đăng ký (tầu bay, tầu thủy, thuyền, máy ủi, máy xúc, ô
                                        tô, mô tô, xe gắn máy...):</span></p>
                                {tsnnTaiSanPhapLuatQuyDinh}

                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>8.7.2. Tài sản khác (đồ
                                        mỹ nghệ, đồ thờ cúng, bàn ghế, cây
                                        cảnh, tranh, ảnh, các loại tài sản khác):</span></p>

                                  {tsnnTaiSanKhac}

                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>9. Tài khoản ở nước
                                            ngoài:</span></p>
                                {TaiKhoanNuocNgoai}

                                
                                    {biendongkk}
                                    </table>
                                    <br/>
                                    <br/>
                            <table class='MsoNormalTable' border='1' cellspacing='0' cellpadding='0' width='100%' style='width:100.0%;border-collapse:collapse;border:none'>
                                        <tbody><tr>
                                            <td width='50%' valign='top' style='width:50.0%;border:none;padding:0; margin: 0;'>
                                                <p class='MsoNormal' align='center' style='margin-bottom:6.0pt;text-align:center;
                                line-height:150%;background:white'><i><span style='color:black'>Khánh Hòa
                                                            ngày....tháng....năm....<br>
                                                        </span></i><b><span style='color:black'>NGƯỜI NHẬN BẢN KÊ KHAI<br>
                                                        </span></b><i><span style='color:black'>(Ký, ghi rõ họ tên, chức vụ/chức
                                                            danh)</span></i></p>
                                            </td>
                                            <td width='50%' valign='top' style='width:50.0%;border:none;padding:0; margin: 0;'>
                                                <p class='MsoNormal' align='center' style='margin-bottom:6.0pt;text-align:center;
                                line-height:150%;background:white'><i><span style='color:black'>Khánh Hòa
                                                            ngày {DateTime.Now.Day.ToString()} tháng {DateTime.Now.Month.ToString()} năm {DateTime.Now.Year.ToString()}<br>
                                                        </span></i><b><span style='color:black'>NGƯỜI KÊ KHAI TÀI SẢN<br>
                                                        </span></b><i><span style='color:black'>(Ký, ghi rõ họ tên)<br>
                                                            <br>
                                                            <br>
                                                            <br>
                                                        </span></i>
                                                        <span style='text-transform: uppercase;'><b>{data2.nguoikekhai.HoTen}</b></span></p>
                                            </td>
                                        </tr>
                                    </tbody>
                            </table>
                        </body>

                        </html>";



                    string filename = System.Guid.NewGuid().ToString();
                    string _filename = filename + ".pdf";
                    string _path = Path.Combine(Server.MapPath("~/Content/uploads"), _filename);
                    ConverterProperties properties = new ConverterProperties();
                    properties.SetFontProvider(new DefaultFontProvider(true, true, true));
                    HtmlConverter.ConvertToPdf(html, new FileStream(_path, FileMode.Create), properties);

                    return Json(filename, JsonRequestBehavior.AllowGet);

                }
                else if(bankekhai.Ma_Loai_KeKhai == 5)
                {
                    biendongkk = $@"
                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><b><span style='color:black'>II. BIẾN ĐỘNG TÀI
                                            SẢN, THU NHẬP; GIẢI TRÌNH NGUỒN GỐC CỦA
                                            TÀI SẢN, THU NHẬP TĂNG THÊM:</span></b> {bankekhai.BienDongTaiSan}</p>

                                    <table class=MsoNormalTable border=1 cellspacing=0 cellpadding=0 width='100%'
                                        style='width:100.0%;border-collapse:collapse;border:none'>
                                        <thead>
                                            <tr>
                                                <td width='43%' rowspan=2 style=' width:43.48%;border:solid windowtext 1.0pt;
                                                border-right:none;background:white;padding:0in 0in 0in 0in; '>
                                                                    <p class=MsoNormal align=center style='margin-bottom:6.0pt;text-align:center;
                                                line-height:150%'><b><span style='color:black'>Loại tài sản, thu nhập</span></b></p>
                                                                </td>
                                                                <td width='41%' colspan=2 style='width:41.06%;border-top:solid windowtext 1.0pt;
                                                border-left:solid windowtext 1.0pt;border-bottom:none;border-right:none;
                                                background:white;padding:0in 0in 0in 0in; text-align: center;'>
                                                    <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%; text-align: center;'><b><span
                                                                style='color:black;'>Tăng/giảm</b> </span></p>
                                                </td>
                                                <td width='15%' style='width:15.46%;border:solid windowtext 1.0pt;
                                border-bottom:none;background:white;padding:0in 0in 0in 0in'>
                                                    <p class=MsoNormal align=center style='margin-bottom:6.0pt;text-align:center;
                                line-height:150%'><b><span style='color:black'>Nội dung giải trình nguồn gốc
                                                                của tài sản tăng thêm và tổng thu nhập</span></b></p>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width='18%' style='width:18.54%;border:solid windowtext 1.0pt;
                                border-right:none;background:white;padding:0in 0in 0in 0in'>
                                                    <p class=MsoNormal align=center style='margin-bottom:6.0pt;text-align:center;
                                line-height:150%'><b><span style='color:black'>Số lượng tài sản</span></b></p>
                                                </td>
                                                <td width='22%' style='width:22.52%;border:solid windowtext 1.0pt;
                                border-right:none;background:white;padding:0in 0in 0in 0in'>
                                                    <p class=MsoNormal align=center style='margin-bottom:6.0pt;text-align:center;
                                line-height:150%'><b><span style='color:black'>Giá trị tài sản, thu nhập</span></b></p>
                                                </td>
                                                <td width='15%' style='width:15.46%;border:solid windowtext 1.0pt;
                                padding:0in 0in 0in 0in'>
                                                    <p class=MsoNormal align=center style='margin-bottom:6.0pt;text-align:center;
                                line-height:150%'><span style='color:black'>&nbsp;</span></p>
                                                </td>
                                            </tr>
                                        </thead>
                                        <tr>
                                            <td width='43%' valign=top style='width:43.48%;border:none;border-left:solid windowtext 1.0pt; border-right: 1px solid black; 
                                background:white;padding:0in 0in 0in 0in'>
                                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>1. Quyền
                                                        sử dụng thực tế đối với đất</span></p>
                                            </td>
                                            <td width='18%' valign=top style='width:18.54%;border:none;background:white; border-right: 1px solid black; border-left: 1px solid black;
                                padding:0in 0in 0in 0in'>
                                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span
                                                        style='color:black'>&nbsp;</span></p>
                                            </td>
                                            <td width='22%' valign=top style='width:22.52%;border:none;background:white; border-right: 1px solid black; border-left: 1px solid black;
                                padding:0in 0in 0in 0in'>
                                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span
                                                        style='color:black'>&nbsp;</span></p>
                                            </td>
                                            <td width='15%' valign=top style='width:15.46%;border:none;border-right:solid windowtext 1.0pt; border-left: 1px solid black;
                                background:white;padding:0in 0in 0in 0in'>
                                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span
                                                        style='color:black'>&nbsp;</span></p>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width='43%' valign=top style='width:43.48%;border:none;border-left:solid windowtext 1.0pt;  border-right: 1px solid black; 
                                background:white;padding:0in 0in 0in 0in'>
                                                <p class=MsoNormal style='margin-top:0in;margin-right:0in;margin-bottom:6.0pt;
                                margin-left:21.0pt;text-indent:-21.0pt;line-height:150%'><span style='color:black'>1.1.<span style='font:7.0pt '
                                                            Times New Roman''>&nbsp; </span></span><span style='color:black'>Đất ở</span></p>
                                            </td>
                                            <td width='18%' valign=top style='width:18.54%;border:none;background:white; border-right: 1px solid black; border-left: 1px solid black;
                                padding:0in 0in 0in 0in'>
                                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span
                                                        style='color:black'>&nbsp;</span></p>
                                            </td>
                                            <td width='22%' valign=top style='width:22.52%;border:none;background:white; border-right: 1px solid black; border-left: 1px solid black;
                                padding:0in 0in 0in 0in'>
                                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span
                                                        style='color:black'>&nbsp;</span></p>
                                            </td>
                                            <td width='15%' valign=top style='width:15.46%;border:none;border-right:solid windowtext 1.0pt;  border-left: 1px solid black;
                                background:white;padding:0in 0in 0in 0in'>
                                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span
                                                        style='color:black'>&nbsp;</span></p>
                                            </td>
                                        </tr>
                                        {BDTSDatO}
                                        <tr >
                                            <td width='43%' valign=top style='width:43.48%;border:none;border-left:solid windowtext 1.0pt;  border-right: 1px solid black;
                                background:white;padding:0in 0in 0in 0in'>
                                                <p class=MsoNormal style='margin-top:0in;margin-right:0in;margin-bottom:6.0pt;
                                margin-left:21.0pt;text-indent:-21.0pt;line-height:150%'><span style='color:black'>1.2.<span style='font:7.0pt '
                                                            Times New Roman''>&nbsp; </span></span><span style='color:black'>Các loại đất
                                                        khác</span></p>
                                            </td>
                                            <td width='18%' valign=top style='width:18.54%;border:none;background:white;  border-right: 1px solid black; border-left: 1px solid black;
                                padding:0in 0in 0in 0in'>
                                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span
                                                        style='color:black'>&nbsp;</span></p>
                                            </td>
                                            <td width='22%' valign=top style='width:22.52%;border:none;background:white;  border-right: 1px solid black; border-left: 1px solid black;
                                padding:0in 0in 0in 0in'>
                                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span
                                                        style='color:black'>&nbsp;</span></p>
                                            </td>
                                            <td width='15%' valign=top style='width:15.46%;border:none;border-right:solid windowtext 1.0pt;  border-left: 1px solid black;
                                background:white;padding:0in 0in 0in 0in'>
                                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span
                                                        style='color:black'>&nbsp;</span></p>
                                            </td>
                                        </tr>
                                        {BDTSDatKhac}
                                        
                                        <tr>
                                            <td width='43%' valign=top style='width:43.48%;border:none;border-left:solid windowtext 1.0pt; border-top: 1px solid black ; border-right: 1px solid black;
                                background:white;padding:0in 0in 0in 0in'>
                                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>2. Nhà ở,
                                                        công trình xây dựng</span></p>
                                            </td>
                                            <td width='18%' valign=top style='width:18.54%;border:none;background:white; border-top: 1px solid black ; border-right: 1px solid black; border-left: 1px solid black;
                                padding:0in 0in 0in 0in'>
                                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span
                                                        style='color:black'>&nbsp;</span></p>
                                            </td>
                                            <td width='22%' valign=top style='width:22.52%;border:none;background:white; border-top: 1px solid black ; border-right: 1px solid black; border-left: 1px solid black;
                                padding:0in 0in 0in 0in'>
                                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span
                                                        style='color:black'>&nbsp;</span></p>
                                            </td>
                                            <td width='15%' valign=top style='width:15.46%;border:none;border-right:solid windowtext 1.0pt; border-top: 1px solid black ; border-left: 1px solid black;
                                background:white;padding:0in 0in 0in 0in'>
                                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span
                                                        style='color:black'>&nbsp;</span></p>
                                            </td>
                                        </tr>
                                        
                                        <tr>
                                            <td width='43%' valign=top style='width:43.48%;border:none;border-left:solid windowtext 1.0pt;  border-right: 1px solid black; 
                                background:white;padding:0in 0in 0in 0in'>
                                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>2.1. Nhà
                                                        ở</span></p>
                                            </td>
                                            <td width='18%' valign=top style='width:18.54%;border:none;background:white;  border-right: 1px solid black; border-left: 1px solid black;
                                padding:0in 0in 0in 0in'>
                                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span
                                                        style='color:black'>&nbsp;</span></p>
                                            </td>
                                            <td width='22%' valign=top style='width:22.52%;border:none;background:white;  border-right: 1px solid black; border-left: 1px solid black;
                                padding:0in 0in 0in 0in'>
                                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span
                                                        style='color:black'>&nbsp;</span></p>
                                            </td>
                                            <td width='15%' valign=top style='width:15.46%;border:none;border-right:solid windowtext 1.0pt; border-left: 1px solid black;
                                background:white;padding:0in 0in 0in 0in'>
                                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span
                                                        style='color:black'>&nbsp;</span></p>
                                            </td>
                                        </tr>
                                        {BDTSNhaO}
                                       
                                        <tr>
                                            <td width='43%' valign=top style='width:43.48%;border:none;border-left:solid windowtext 1.0pt;  border-right: 1px solid black; 
                                background:white;padding:0in 0in 0in 0in'>
                                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>2.2. Công
                                                        trình xây dựng khác</span></p>
                                            </td>
                                            <td width='18%' valign=top style='width:18.54%;border:none;background:white;  border-right: 1px solid black; border-left: 1px solid black;
                                padding:0in 0in 0in 0in'>
                                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span
                                                        style='color:black'>&nbsp;</span></p>
                                            </td>
                                            <td width='22%' valign=top style='width:22.52%;border:none;background:white; border-right: 1px solid black; border-left: 1px solid black;
                                padding:0in 0in 0in 0in'>
                                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span
                                                        style='color:black'>&nbsp;</span></p>
                                            </td>
                                            <td width='15%' valign=top style='width:15.46%;border:none;border-right:solid windowtext 1.0pt; border-left: 1px solid black;
                                background:white;padding:0in 0in 0in 0in'>
                                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span
                                                        style='color:black'>&nbsp;</span></p>
                                            </td>
                                        </tr>
                                    {BDTSCongTrinhKhach}
                                       
                                        <tr>
                                            <td width='43%' valign=top style='width:43.48%;border:none;border-left:solid windowtext 1.0pt; border-top: 1px solid black;  border-right: 1px solid black; 
                                background:white;padding:0in 0in 0in 0in'>
                                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>3. Tài sản
                                                        khác gắn liền với đất</span></p>
                                            </td>
                                            <td width='18%' valign=top style='width:18.54%;border:none;background:white; border-top: 1px solid black ; border-right: 1px solid black; border-left: 1px solid black;
                                padding:0in 0in 0in 0in'>
                                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span
                                                        style='color:black'>&nbsp;</span></p>
                                            </td>
                                            <td width='22%' valign=top style='width:22.52%;border:none;background:white; border-top: 1px solid black ; border-right: 1px solid black; border-left: 1px solid black;
                                padding:0in 0in 0in 0in'>
                                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span
                                                        style='color:black'>&nbsp;</span></p>
                                            </td>
                                            <td width='15%' valign=top style='width:15.46%;border:none;border-right:solid windowtext 1.0pt; border-top: 1px solid black;  border-left: 1px solid black;
                                background:white;padding:0in 0in 0in 0in'>
                                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span
                                                        style='color:black'>&nbsp;</span></p>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width='43%' valign=top style='width:43.48%;border:none;border-left:solid windowtext 1.0pt;  border-right: 1px solid black; 
                                background:white;padding:0in 0in 0in 0in'>
                                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>3.1. Cây
                                                        lâu năm, rừng sản xuất</span></p>
                                                
                                            </td>
                                            <td width='18%' valign=top style='width:18.54%;border:none;background:white;  border-right: 1px solid black; border-left: 1px solid black;
                                padding:0in 0in 0in 0in'>
                                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span
                                                        style='color:black'>&nbsp;</span></p>
                                            </td>
                                            <td width='22%' valign=top style='width:22.52%;border:none;background:white;  border-right: 1px solid black; border-left: 1px solid black;
                                padding:0in 0in 0in 0in'> 
                                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span
                                                        style='color:black'>&nbsp;</span></p>
                                            </td>
                                            <td width='15%' valign=top style='width:15.46%;border:none;border-right:solid windowtext 1.0pt; border-left: 1px solid black;
                                background:white;padding:0in 0in 0in 0in'>
                                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span
                                                        style='color:black'>&nbsp;</span></p>
                                            </td>
                                        </tr>
                                        {BDTSCayLauNam}
                                       {BDTSRungSanXuat}
                                        <tr>
                                            <td width='43%' valign=top style='width:43.48%;border:none;border-left:solid windowtext 1.0pt;  border-right: 1px solid black;
                                background:white;padding:0in 0in 0in 0in'>
                                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>3.2. Vật
                                                        kiến trúc gắn liền với đất</span></p>
                                            </td>
                                            <td width='18%' valign=top style='width:18.54%;border:none;background:white;  border-right: 1px solid black; border-left: 1px solid black;
                                padding:0in 0in 0in 0in'>
                                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span
                                                        style='color:black'>&nbsp;</span></p>
                                            </td>
                                            <td width='22%' valign=top style='width:22.52%;border:none;background:white;  border-right: 1px solid black; border-left: 1px solid black;
                                padding:0in 0in 0in 0in'>
                                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span
                                                        style='color:black'>&nbsp;</span></p>
                                            </td>
                                            <td width='15%' valign=top style='width:15.46%;border:none;border-right:solid windowtext 1.0pt; border-left: 1px solid black;
                                background:white;padding:0in 0in 0in 0in'>
                                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span
                                                        style='color:black'>&nbsp;</span></p>
                                            </td>
                                        </tr>
                                        {BDTSVatKienTruc}
                                       
                                        <tr>
                                            <td width='43%' valign=top style='width:43.48%;border:none;border-left:solid windowtext 1.0pt; border-top: 1px solid black;  border-right: 1px solid black; 
                                background:white;padding:0in 0in 0in 0in'>
                                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>4. Vàng,
                                                        kim cương, bạch kim và các kim loại quý, đá quý
                                                        khác có tổng giá trị từ 50 triệu đồng trở lên</span></p>
                                            </td>
                                            <td width='18%' valign=top style='width:18.54%;border:none;background:white; border-top: 1px solid black; border-right: 1px solid black; border-left: 1px solid black;
                                padding:0in 0in 0in 0in'>
                                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span
                                                        style='color:black'>&nbsp;</span></p>
                                            </td>
                                            <td width='22%' valign=top style='width:22.52%;border:none;background:white; border-top: 1px solid black; border-right: 1px solid black; border-left: 1px solid black;
                                padding:0in 0in 0in 0in'>
                                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span
                                                        style='color:black'>&nbsp;</span></p>
                                            </td>
                                            <td width='15%' valign=top style='width:15.46%;border:none;border-right:solid windowtext 1.0pt; border-top: 1px solid black;  border-left: 1px solid black;
                                background:white;padding:0in 0in 0in 0in'>
                                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span
                                                        style='color:black'>&nbsp;</span></p>
                                            </td>
                                        </tr>
                                        {BDTSTrangSuc}
                                        
                                        <tr>
                                            <td width='43%' valign=top style='width:43.48%;border:none;border-left:solid windowtext 1.0pt; border-top: 1px solid black; border-right: 1px solid black; 
                                background:white;padding:0in 0in 0in 0in'>
                                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>5. Tiền
                                                        (tiền Việt Nam, ngoại tệ) gồm tiền mặt, tiền cho
                                                        vay, tiền trả trước, tiền gửi cá nhân, tổ chức trong nước, tổ chức nước ngoài
                                                        tại Việt Nam mà tổng giá trị quy đổi từ 50 triệu đồng trở lên.</span></p>
                                            </td>
                                            <td width='18%' valign=top style='width:18.54%;border:none;background:white; border-top: 1px solid black; border-right: 1px solid black; border-left: 1px solid black;
                                padding:0in 0in 0in 0in'>
                                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span
                                                        style='color:black'>&nbsp;</span></p>
                                            </td>
                                            <td width='22%' valign=top style='width:22.52%;border:none;background:white; border-top: 1px solid black; border-right: 1px solid black; border-left: 1px solid black;
                                padding:0in 0in 0in 0in'>
                                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span
                                                        style='color:black'>&nbsp;</span></p>
                                            </td>
                                            <td width='15%' valign=top style='width:15.46%;border:none;border-right:solid windowtext 1.0pt; border-top: 1px solid black;  border-left: 1px solid black;
                                background:white;padding:0in 0in 0in 0in'>
                                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span
                                                        style='color:black'>&nbsp;</span></p>
                                            </td>
                                        </tr>
                                        {BDTSTien}
                                        
                                        <tr>
                                            <td width='43%' valign=top style='width:43.48%;border:none;border-left:solid windowtext 1.0pt; border-top: 1px solid black; border-right: 1px solid black;
                                background:white;padding:0in 0in 0in 0in'>
                                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>6. Cổ
                                                        phiếu, trái phiếu, vốn góp, các loại giấy tờ có giá
                                                        khác mà tổng giá trị từ 50 triệu đồng trở lên (khai theo từng loại):</span></p>
                                            </td>
                                            <td width='18%' valign=top style='width:18.54%;border:none;background:white; border-top: 1px solid black; border-right: 1px solid black; border-left: 1px solid black;
                                padding:0in 0in 0in 0in'>
                                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span
                                                        style='color:black'>&nbsp;</span></p>
                                            </td>
                                            <td width='22%' valign=top style='width:22.52%;border:none;background:white; border-top: 1px solid black; border-right: 1px solid black; border-left: 1px solid black;
                                padding:0in 0in 0in 0in'>
                                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span
                                                        style='color:black'>&nbsp;</span></p>
                                            </td>
                                            <td width='15%' valign=top style='width:15.46%;border:none;border-right:solid windowtext 1.0pt; border-top: 1px solid black; border-left: 1px solid black;
                                background:white;padding:0in 0in 0in 0in'>
                                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span
                                                        style='color:black'>&nbsp;</span></p>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width='43%' valign=top style='width:43.48%;border:none;border-left:solid windowtext 1.0pt; border-right: 1px solid black; 
                                background:white;padding:0in 0in 0in 0in'>
                                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>6.1. Cổ
                                                        phiếu</span></p>
                                            </td>
                                            <td width='18%' valign=top style='width:18.54%;border:none;background:white; border-right: 1px solid black; border-left: 1px solid black;
                                padding:0in 0in 0in 0in'>
                                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span
                                                        style='color:black'>&nbsp;</span></p>
                                            </td>
                                            <td width='22%' valign=top style='width:22.52%;border:none;background:white; border-right: 1px solid black; border-left: 1px solid black;
                                padding:0in 0in 0in 0in'>
                                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span
                                                        style='color:black'>&nbsp;</span></p>
                                            </td>
                                            <td width='15%' valign=top style='width:15.46%;border:none;border-right:solid windowtext 1.0pt;  border-left: 1px solid black;
                                background:white;padding:0in 0in 0in 0in'>
                                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span
                                                        style='color:black'>&nbsp;</span></p>
                                            </td>
                                        </tr>
                                        {BDTSCoPhieu}
                                        <tr>
                                            <td width='43%' valign=top style='width:43.48%;border:none;border-left:solid windowtext 1.0pt;  border-right: 1px solid black;
                                background:white;padding:0in 0in 0in 0in'>
                                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>6.2. Trái
                                                        phiếu</span></p>
                                               
                                            </td>
                                            <td width='18%' valign=top style='width:18.54%;border:none;background:white;  border-right: 1px solid black; border-left: 1px solid black;
                                padding:0in 0in 0in 0in'>
                                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span
                                                        style='color:black'>&nbsp;</span></p>
                                            </td>
                                            <td width='22%' valign=top style='width:22.52%;border:none;background:white;  border-right: 1px solid black; border-left: 1px solid black;
                                padding:0in 0in 0in 0in'>
                                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span
                                                        style='color:black'>&nbsp;</span></p>
                                            </td>
                                            <td width='15%' valign=top style='width:15.46%;border:none;border-right:solid windowtext 1.0pt; border-left: 1px solid black;
                                background:white;padding:0in 0in 0in 0in'>
                                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span
                                                        style='color:black'>&nbsp;</span></p>
                                            </td>
                                        </tr>
                                        {BDTSTraiPhieu}
                                        <tr>
                                            <td width='43%' valign=top style='width:43.48%;border:none;border-left:solid windowtext 1.0pt;  border-right: 1px solid black; 
                                background:white;padding:0in 0in 0in 0in'>
                                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>6.3. Vốn
                                                        góp</span></p>
                                            </td>
                                            <td width='18%' valign=top style='width:18.54%;border:none;background:white;  border-right: 1px solid black; border-left: 1px solid black;
                                padding:0in 0in 0in 0in'>
                                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span
                                                        style='color:black'>&nbsp;</span></p>
                                            </td>
                                            <td width='22%' valign=top style='width:22.52%;border:none;background:white;  border-right: 1px solid black; border-left: 1px solid black;
                                padding:0in 0in 0in 0in'>
                                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span
                                                        style='color:black'>&nbsp;</span></p>
                                            </td>
                                            <td width='15%' valign=top style='width:15.46%;border:none;border-right:solid windowtext 1.0pt; border-left: 1px solid black;
                                background:white;padding:0in 0in 0in 0in'>
                                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span
                                                        style='color:black'>&nbsp;</span></p>
                                            </td>
                                        </tr>
                                        {BDTSVonGop}
                                        
                                        <tr>
                                            <td width='43%' valign=top style='width:43.48%;border:none;border-left:solid windowtext 1.0pt;  border-right: 1px solid black; 
                                background:white;padding:0in 0in 0in 0in'>
                                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>6.4. Các
                                                        loại giấy tờ có giá khác</span></p>
                                            </td>
                                            <td width='18%' valign=top style='width:18.54%;border:none;background:white;  border-right: 1px solid black; border-left: 1px solid black;
                                padding:0in 0in 0in 0in'>
                                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span
                                                        style='color:black'>&nbsp;</span></p>
                                            </td>
                                            <td width='22%' valign=top style='width:22.52%;border:none;background:white;  border-right: 1px solid black; border-left: 1px solid black;
                                padding:0in 0in 0in 0in'>
                                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span
                                                        style='color:black'>&nbsp;</span></p>
                                            </td>
                                            <td width='15%' valign=top style='width:15.46%;border:none;border-right:solid windowtext 1.0pt;   border-left: 1px solid black;
                                background:white;padding:0in 0in 0in 0in'>
                                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span
                                                        style='color:black'>&nbsp;</span></p>
                                            </td>
                                        </tr>
                                        {BDTSGiayToKhac}
                                       
                                        <tr>
                                            <td width='43%' valign=top style='width:43.48%;border:none;border-left:solid windowtext 1.0pt; border-top: 1px solid black; border-right: 1px solid black;
                                background:white;padding:0in 0in 0in 0in'>
                                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>7. Tài sản
                                                        khác có giá trị từ 50 triệu đồng trở lên:</span></p>
                                            </td>
                                            <td width='18%' valign=top style='width:18.54%;border:none;background:white; border-top: 1px solid black; border-right: 1px solid black; border-left: 1px solid black;
                                padding:0in 0in 0in 0in'>
                                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span
                                                        style='color:black'>&nbsp;</span></p>
                                            </td>
                                            <td width='22%' valign=top style='width:22.52%;border:none;background:white; border-top: 1px solid black; border-right: 1px solid black; border-left: 1px solid black;
                                padding:0in 0in 0in 0in'>
                                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span
                                                        style='color:black'>&nbsp;</span></p>
                                            </td>
                                            <td width='15%' valign=top style='width:15.46%;border:none;border-right:solid windowtext 1.0pt; border-top: 1px solid black; border-left: 1px solid black;
                                background:white;padding:0in 0in 0in 0in'>
                                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span
                                                        style='color:black'>&nbsp;</span></p>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width='43%' valign=top style='width:43.48%;border:none;border-left:solid windowtext 1.0pt; border-right: 1px solid black; 
                                background:white;padding:0in 0in 0in 0in'>
                                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>7.1. Tài
                                                        sản theo quy định của pháp luật phải đăng ký sử
                                                        dụng và được cấp giấy đăng ký (tầu bay, tàu thủy, thuyền, máy ủi, máy xúc, ô
                                                        tô, mô tô, xe gắn máy...).</span></p>
                                            </td>
                                            <td width='18%' valign=top style='width:18.54%;border:none;background:white; border-right: 1px solid black; border-left: 1px solid black;
                                padding:0in 0in 0in 0in'>
                                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span
                                                        style='color:black'>&nbsp;</span></p>
                                            </td>
                                            <td width='22%' valign=top style='width:22.52%;border:none;background:white; border-right: 1px solid black; border-left: 1px solid black;
                                padding:0in 0in 0in 0in'>
                                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span
                                                        style='color:black'>&nbsp;</span></p>
                                            </td>
                                            <td width='15%' valign=top style='width:15.46%;border:none;border-right:solid windowtext 1.0pt; border-left: 1px solid black;
                                background:white;padding:0in 0in 0in 0in'>
                                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span
                                                        style='color:black'>&nbsp;</span></p>
                                            </td>
                                        </tr>
                                        {BDTSTaiSanPhapLuatQuyDinh}
                                       
                                        <tr>
                                            <td width='43%' valign=top style='width:43.48%;border:none;border-left:solid windowtext 1.0pt;  border-right: 1px solid black;
                                background:white;padding:0in 0in 0in 0in'>
                                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>7.2. Tài
                                                        sản khác (đồ mỹ nghệ, đồ thờ cúng, bàn ghế, cây
                                                        cảnh, tranh ảnh, các loại tài sản khác).</span></p>
                                            </td>
                                            <td width='18%' valign=top style='width:18.54%;border:none;background:white;  border-right: 1px solid black; border-left: 1px solid black;
                                padding:0in 0in 0in 0in'>
                                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span
                                                        style='color:black'>&nbsp;</span></p>
                                            </td>
                                            <td width='22%' valign=top style='width:22.52%;border:none;background:white;  border-right: 1px solid black; border-left: 1px solid black;
                                padding:0in 0in 0in 0in'>
                                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span
                                                        style='color:black'>&nbsp;</span></p>
                                            </td>
                                            <td width='15%' valign=top style='width:15.46%;border:none;border-right:solid windowtext 1.0pt;  border-left: 1px solid black;
                                background:white;padding:0in 0in 0in 0in'>
                                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span
                                                        style='color:black'>&nbsp;</span></p>
                                            </td>
                                        </tr>
                                         {BDTSDoMyNghe}
                                       
                                        <tr>
                                            <td width='43%' valign=top style='width:43.48%;border:none;border-left:solid windowtext 1.0pt; border-top: 1px solid black; border-right: 1px solid black;
                                background:white;padding:0in 0in 0in 0in'>
                                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>8. Tài sản
                                                        ở nước ngoài.</span></p>
                                            </td>
                                            <td width='18%' valign=top style='width:18.54%;border:none;background:white; border-top: 1px solid black; border-right: 1px solid black; border-left: 1px solid black;
                                padding:0in 0in 0in 0in'>
                                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span
                                                        style='color:black'>&nbsp;</span></p>
                                            </td>
                                            <td width='22%' valign=top style='width:22.52%;border:none;background:white; border-top: 1px solid black; border-right: 1px solid black; border-left: 1px solid black;
                                padding:0in 0in 0in 0in'>
                                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span
                                                        style='color:black'>&nbsp;</span></p>
                                            </td>
                                            <td width='15%' valign=top style='width:15.46%;border:none;border-right:solid windowtext 1.0pt; border-top: 1px solid black;  border-left: 1px solid black;
                                background:white;padding:0in 0in 0in 0in'>
                                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span
                                                        style='color:black'>&nbsp;</span></p>
                                            </td>
                                        </tr>
                                        {BDTSTaiSanNuocNgoai}
                                        
                                        
                                        <tr>
                                            <td width='43%' valign=top style='width:43.48%;border:none;border-left:solid windowtext 1.0pt; border-top: 1px solid black; border-right: 1px solid black; 
                                background:white;padding:0in 0in 0in 0in'>
                                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>9. Tổng
                                                        thu nhập giữa hai lần kê khai.</span></p>
                                            </td>
                                            <td width='18%' valign=top style='width:18.54%;border:none;background:white; border-top: 1px solid black; border-right: 1px solid black; border-left: 1px solid black;
                                padding:0in 0in 0in 0in'>
                                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span
                                                        style='color:black'>&nbsp;</span></p>
                                            </td>
                                            <td width='22%' valign=top style='width:22.52%;border:none;background:white; border-top: 1px solid black; border-right: 1px solid black; border-left: 1px solid black;
                                padding:0in 0in 0in 0in'>
                                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span
                                                        style='color:black'>&nbsp;</span></p>
                                            </td>
                                            <td width='15%' valign=top style='width:15.46%;border:none;border-right:solid windowtext 1.0pt; border-top: 1px solid black;  border-left: 1px solid black;
                                background:white;padding:0in 0in 0in 0in'>
                                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span
                                                        style='color:black'>&nbsp;</span></p>
                                            </td>
                                        </tr>
                                        {BDTSTongThuNhap}
                                        <tr>
                                            <td width='43%' valign=top style='border-bottom: 1px solid black'>
                                                
                                            </td>
                                            <td width='18%' valign=top style='border-bottom: 1px solid black'>
                                                
                                            </td>
                                            <td width='22%' valign=top style='border-bottom: 1px solid black'>
                                                
                                            </td>
                                            <td width='15%' valign=top style='border-bottom: 1px solid black'>
                                                
                                            </td>
                                        </tr>
                                    </table>";

                    var html = $@"<html>
                        <head>
                            <meta http-equiv=Content-Type content='text/html; charset=utf-8'>
                            <meta name=Generator content='Microsoft Word 15 (filtered)'>
                            <style>
                                <!--
                                /* Font Definitions */
                                @font-face {{
                                    font-family: 'Cambria Math';
                                    panose-1: 2 4 5 3 5 4 6 3 2 4;
                                }}
                                html{{  margin: 0.71cm 0.76cm 0.75cm 1.73cm;}}
                                th,td,p,div,b {{margin:0;padding:0}}
                                /* Style Definitions */
                                p.MsoNormal,
                                li.MsoNormal,
                                div.MsoNormal, p, span, b, tr, td {{
                                    margin: 0in;
                                    font-size: 17px;
                                    font-family: 'Times New Roman', serif;
                                }}

                                /* Page Definitions */
                                @page WordSection1 {{
                                    size: 8.5in 11.0in;
                                }}

                                div.WordSection1 {{
                                    page: WordSection1;
                                }}
                                -->
                            </style>

                        </head>

                        <body lang=EN-US style='word-wrap:break-word'>

                            <div class=WordSection1>

                                <table class=MsoNormalTable border=1 cellspacing=0 cellpadding=0 
                                    style='width:100%;border-collapse:collapse;border:none'>
                                    <tr>
                                        <td  valign=top style='width:40%;border:none;padding:0; margin: 0;'>
                                            {CoQuanTrucThuoc}
                                                        
                                        </td>
                                        <td  valign=top style='width:60%;border:none;padding:0; margin: 0;'>
                                            <p class=MsoNormal align=center style='margin-bottom:6.0pt;text-align:center;
                          line-height:150%;background:white'><b><span style='color:black'>CỘNG HÒA XÃ
                                                        HỘI CHỦ NGHĨA VIỆT NAM<br>
                                                        Độc lập - Tự do - Hạnh phúc <br>
                                                        ---------------</span></b></p>
                                        </td>
                                    </tr>
                                </table>

                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>&nbsp;</span></p>

                                <p class=MsoNormal align=center style='margin-bottom:6.0pt;text-align:center;
                        line-height:150%'><b><span style='color:black'>BẢN KÊ KHAI TÀI SẢN, THU NHẬP </span></b><b><span lang=VI
                                            style='color:black'>{LoaiKeKhai}</span<span
                                            style='color:black'><br>(Ngày</span></b><b><span style='color:black'> </span><span
                                            style='color:black'>{data2.bankekhai.Ngay_KeKhai} tháng {data2.bankekhai.Thang_KeKhai} năm {data2.bankekhai.Nam_KeKhai})</span></b></p>

                               

                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><b><span style='color:black'>I. THÔNG TIN
                                            CHUNG</span></b></p>

                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>1. Người kê khai
                                            tài sản, thu nhập</span></p>
                                {ThongTinCanBo}  {VoChong} {Con} 
                                {biendongkk}
                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><b><span style='color:black'>III. THÔNG TIN MÔ TẢ
                                            VỀ TÀI SẢN</span></b></p>

                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>1. Quyền sử dụng
                                            thực tế đối với đất</span>:</p>
                                 <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>1.1. Đất
                                                    ở</span><span style='color:black'>:</span></p>

                                {DatO}
                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>1.2. Các loại đất
                                            khác </span><span lang=VI style='color:black'>:</span></p>

                                {CacLoaiDatKhac}
                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>2. Nhà ở, công
                                            trình xây dựng:</span></p>

                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>2.1. Nhà
                                            ở:</span></p>

                                {NhaO}

                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>2.2. Công trình xây
                                            dựng khác</span></p>

                                {CongTrinhXayDung}
                                
                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>3. Tài sản khác gắn
                                            liền với đất:</span></p>
                                {TaiSanGanVoiDat}

                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>3.1. Cây lâu
                                            năm</span>:</p>

                                <table class=MsoNormalTable border=0 cellspacing=0 cellpadding=0 width='100%'
                                    style='width:100.0%;border-collapse:collapse'>
                                    {CayLauNam}
                                </table>

                              

                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>3.2. Rừng sản
                                            xuất:</span></p>

                                {RungSanXuat}

                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>3.3. Vật kiến trúc
                                            khác gắn liền với đất</span><span style='color:black'>:</span></p>

                                {VatKienTruc}
                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>4. Vàng, kim cương,
                                            bạch kim và các kim loại quý, đá quý
                                            khác có tổng giá trị từ 50 triệu đồng trở lên:</span></p>
                                {Vang}
                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>5. Tiền (tiền Việt
                                            Nam, ngoại tệ) gồm tiền mặt, tiền cho
                                            vay, tiền trả trước, tiền gửi cá nhân, tổ chức trong nước, tổ chức nước ngoài
                                            tại Việt Nam mà tổng giá trị quy đổi từ 50 triệu đồng trở lên:</span></p>
                                {Tien}
                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>6. Cổ phiếu, trái
                                            phiếu, vốn góp, các loại giấy tờ có giá
                                            khác mà tổng giá trị từ 50 triệu đồng trở lên (khai theo từng loại):</span></p>

                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>6.1. Cổ phiếu:</span>
                                </p>
                                
                                {CoPhieu}

                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>6.2. Trái
                                        phiếu:</span></p>
                                {TraiPhieu}

                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>6.3. Vốn
                                        góp:</span></p>
                               {VonGop}

                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>6.4. Các loại giấy tờ
                                        có giá khác: </span></p>

                                {GiayToKhac}

                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>7. Tài sản khác mà
                                            mỗi tài sản có giá trị từ 50 triệu đồng
                                            trở lên, bao gồm:</span></p>

                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>7.1. Tài sản theo quy
                                        định của pháp luật phải đăng ký sử
                                        dụng và được cấp giấy đăng ký (tầu bay, tầu thủy, thuyền, máy ủi, máy xúc, ô
                                        tô, mô tô, xe gắn máy...):</span></p>
                                {TaiSanPhapLuatQuyDinh}

                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>7.2. Tài sản khác (đồ
                                        mỹ nghệ, đồ thờ cúng, bàn ghế, cây
                                        cảnh, tranh, ảnh, các loại tài sản khác):</span></p>

                                  {TaiSanKhac}

                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>8. Tài sản ở nước
                                            ngoài:</span></p>







                                 <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>8.1. Quyền sử dụng
                                            thực tế đối với đất</span>:</p>
                                 <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>8.1.1. Đất
                                                    ở</span><span style='color:black'> 
                                                :</span></p>

                                {tsnnDatO}
                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>8.1.2. Các loại đất
                                            khác </span><span lang=VI style='color:black'>:</span></p>

                                {tsnnCacLoaiDatKhac}
                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>8.2. Nhà ở, công
                                            trình xây dựng:</span></p>

                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>8.2.1. Nhà
                                            ở:</span></p>

                                {tsnnNhaO}

                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>8.2.2. Công trình xây
                                            dựng khác</span></p>

                                {tsnnCongTrinhXayDung}
                                
                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>8.3. Tài sản khác gắn
                                            liền với đất:</span></p>
                                {tsnnTaiSanGanVoiDat}

                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>8.3.1. Cây lâu
                                            năm</span>:</p>

                                <table class=MsoNormalTable border=0 cellspacing=0 cellpadding=0 width='100%'
                                    style='width:100.0%;border-collapse:collapse'>
                                    {tsnnCayLauNam}
                                </table>

                              

                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>8.3.2. Rừng sản
                                            xuất:</span></p>

                                {tsnnRungSanXuat}

                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>8.3.3. Vật kiến trúc
                                            khác gắn liền với đất</span><span style='color:black'>:</span></p>

                                {tsnnVatKienTruc}
                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>8.4. Vàng, kim cương,
                                            bạch kim và các kim loại quý, đá quý
                                            khác có tổng giá trị từ 50 triệu đồng trở lên:</span></p>
                                {tsnnVang}
                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>8.5. Tiền (tiền Việt
                                            Nam, ngoại tệ) gồm tiền mặt, tiền cho
                                            vay, tiền trả trước, tiền gửi cá nhân, tổ chức trong nước, tổ chức nước ngoài
                                            tại Việt Nam mà tổng giá trị quy đổi từ 50 triệu đồng trở lên:</span></p>
                                {tsnnTien}
                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>8.6. Cổ phiếu, trái
                                            phiếu, vốn góp, các loại giấy tờ có giá
                                            khác mà tổng giá trị từ 50 triệu đồng trở lên (khai theo từng loại):</span></p>

                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>8.6.1. Cổ phiếu:</span>
                                </p>
                                
                                {tsnnCoPhieu}

                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>8.6.2. Trái
                                        phiếu:</span></p>
                                {tsnnTraiPhieu}

                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>8.6.3. Vốn
                                        góp:</span></p>
                               {tsnnVonGop}

                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>8.6.4. Các loại giấy tờ
                                        có giá khác: </span></p>

                                {tsnnGiayToKhac}

                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>8.7. Tài sản khác mà
                                            mỗi tài sản có giá trị từ 50 triệu đồng
                                            trở lên, bao gồm:</span></p>

                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>8.7.1. Tài sản theo quy
                                        định của pháp luật phải đăng ký sử
                                        dụng và được cấp giấy đăng ký (tầu bay, tầu thủy, thuyền, máy ủi, máy xúc, ô
                                        tô, mô tô, xe gắn máy...):</span></p>
                                {tsnnTaiSanPhapLuatQuyDinh}

                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>8.7.2. Tài sản khác (đồ
                                        mỹ nghệ, đồ thờ cúng, bàn ghế, cây
                                        cảnh, tranh, ảnh, các loại tài sản khác):</span></p>

                                  {tsnnTaiSanKhac}

                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>9. Tài khoản ở nước
                                            ngoài:</span></p>
                                {TaiKhoanNuocNgoai}

                                
                                    </table>
                                    <br/>
                                    <br/>
                            <table class='MsoNormalTable' border='1' cellspacing='0' cellpadding='0' width='100%' style='width:100.0%;border-collapse:collapse;border:none'>
                                        <tbody><tr>
                                            <td width='50%' valign='top' style='width:50.0%;border:none;padding:0; margin: 0;'>
                                                <p class='MsoNormal' align='center' style='margin-bottom:6.0pt;text-align:center;
                                line-height:150%;background:white'><i><span style='color:black'>Khánh Hòa,
                                                            ngày....tháng....năm....<br>
                                                        </span></i><b><span style='color:black'>NGƯỜI NHẬN BẢN KÊ KHAI<br>
                                                        </span></b><i><span style='color:black'>(Ký, ghi rõ họ tên, chức vụ/chức
                                                            danh)</span></i></p>
                                            </td>
                                            <td width='50%' valign='top' style='width:50.0%;border:none;padding:0; margin: 0;'>
                                                <p class='MsoNormal' align='center' style='margin-bottom:6.0pt;text-align:center;
                                line-height:150%;background:white'><i><span style='color:black'>Khánh Hòa,
                                                            ngày {DateTime.Now.Day.ToString()} tháng {DateTime.Now.Month.ToString()} năm {DateTime.Now.Year.ToString()}<br>
                                                        </span></i><b><span style='color:black'>NGƯỜI KÊ KHAI TÀI SẢN<br>
                                                        </span></b><i><span style='color:black'>(Ký, ghi rõ họ tên)<br>
                                                            <br>
                                                            <br>
                                                            <br>
                                                        </span></i>
                                                        <span style='text-transform: uppercase;'><b>{data2.nguoikekhai.HoTen}</b></span></p>
                                            </td>
                                        </tr>
                                    </tbody>
                            </table>
                        </body>

                        </html>";



                    string filename = System.Guid.NewGuid().ToString();
                    string _filename = filename + ".pdf";
                    string _path = Path.Combine(Server.MapPath("~/Content/uploads"), _filename);
                    ConverterProperties properties = new ConverterProperties();
                    properties.SetFontProvider(new DefaultFontProvider(true, true, true));
                    HtmlConverter.ConvertToPdf(html, new FileStream(_path, FileMode.Create), properties);

                    return Json(filename, JsonRequestBehavior.AllowGet);

                }
            }
            else
            {
                var html = $@"<html>
                        <head>
                            <meta http-equiv=Content-Type content='text/html; charset=utf-8'>
                            <meta name=Generator content='Microsoft Word 15 (filtered)'>
                            <style>
                                <!--
                                /* Font Definitions */
                                @font-face {{
                                    font-family: 'Cambria Math';
                                    panose-1: 2 4 5 3 5 4 6 3 2 4;
                                }}
                                html{{  margin: 0.71cm 0.76cm 0.75cm 1.73cm;}}
                                th,td,p,div,b {{margin:0;padding:0}}
                                /* Style Definitions */
                                p.MsoNormal,
                                li.MsoNormal,
                                div.MsoNormal, p, span, b, tr, td {{
                                    margin: 0in;
                                    font-size: 17px;
                                    font-family: 'Times New Roman', serif;
                                }}

                                /* Page Definitions */
                                @page WordSection1 {{
                                    size: 8.5in 11.0in;
                                }}

                                div.WordSection1 {{
                                    page: WordSection1;
                                }}
                                -->
                            </style>

                        </head>

                        <body lang=EN-US style='word-wrap:break-word'>

                            <div class=WordSection1>

                                <table class=MsoNormalTable border=1 cellspacing=0 cellpadding=0 
                                    style='width:100%;border-collapse:collapse;border:none'>
                                    <tr>
                                        <td  valign=top style='width:40%;border:none;padding:0; margin: 0;'>
                                            {CoQuanTrucThuoc}
                                        </td>
                                        <td  valign=top style='width:60%;border:none;padding:0; margin: 0;'>
                                            <p class=MsoNormal align=center style='margin-bottom:6.0pt;text-align:center;
                          line-height:150%;background:white'><b><span style='color:black'>CỘNG HÒA XÃ
                                                        HỘI CHỦ NGHĨA VIỆT NAM<br>
                                                        Độc lập - Tự do - Hạnh phúc <br>
                                                        ---------------</span></b></p>
                                        </td>
                                    </tr>
                                </table>

                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>&nbsp;</span></p>

                                <p class=MsoNormal align=center style='margin-bottom:6.0pt;text-align:center;
                        line-height:150%'><b><span style='color:black'>BẢN KÊ KHAI TÀI SẢN, THU NHẬP </span></b><b><span lang=VI
                                            style='color:black'>{LoaiKeKhai}</span<span
                                            style='color:black'><br>(Ngày</span></b><b><span style='color:black'> </span><span
                                            style='color:black'>{data2.bankekhai.Ngay_KeKhai} tháng {data2.bankekhai.Thang_KeKhai} năm {data2.bankekhai.Nam_KeKhai})</span></b></p>

                               

                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><b><span style='color:black'>I. THÔNG TIN
                                            CHUNG</span></b></p>

                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>1. Người kê khai
                                            tài sản, thu nhập</span></p>
                                {ThongTinCanBo}  {VoChong} {Con} 
                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><b><span style='color:black'>II. THÔNG TIN MÔ TẢ
                                            VỀ TÀI SẢN</span></b></p>

                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>1. Quyền sử dụng
                                            thực tế đối với đất</span>:</p>
                                 <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>1.1. Đất
                                                    ở</span><span style='color:black'>:</span></p>

                                {DatO}
                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>1.2. Các loại đất
                                            khác </span><span lang=VI style='color:black'>:</span></p>

                                {CacLoaiDatKhac}
                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>2. Nhà ở, công
                                            trình xây dựng:</span></p>

                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>2.1. Nhà
                                            ở:</span></p>

                                {NhaO}

                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>2.2. Công trình xây
                                            dựng khác</span></p>

                                {CongTrinhXayDung}
                                
                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>3. Tài sản khác gắn
                                            liền với đất:</span></p>
                                {TaiSanGanVoiDat}

                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>3.1. Cây lâu
                                            năm</span>:</p>

                                <table class=MsoNormalTable border=0 cellspacing=0 cellpadding=0 width='100%'
                                    style='width:100.0%;border-collapse:collapse'>
                                    {CayLauNam}
                                </table>

                              

                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>3.2. Rừng sản
                                            xuất:</span></p>

                                {RungSanXuat}

                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>3.3. Vật kiến trúc
                                            khác gắn liền với đất</span><span style='color:black'>:</span></p>

                                {VatKienTruc}
                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>4. Vàng, kim cương,
                                            bạch kim và các kim loại quý, đá quý
                                            khác có tổng giá trị từ 50 triệu đồng trở lên:</span></p>
                                {Vang}
                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>5. Tiền (tiền Việt
                                            Nam, ngoại tệ) gồm tiền mặt, tiền cho
                                            vay, tiền trả trước, tiền gửi cá nhân, tổ chức trong nước, tổ chức nước ngoài
                                            tại Việt Nam mà tổng giá trị quy đổi từ 50 triệu đồng trở lên:</span></p>
                                {Tien}
                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>6. Cổ phiếu, trái
                                            phiếu, vốn góp, các loại giấy tờ có giá
                                            khác mà tổng giá trị từ 50 triệu đồng trở lên (khai theo từng loại):</span></p>

                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>6.1. Cổ phiếu:</span>
                                </p>
                                
                                {CoPhieu}

                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>6.2. Trái
                                        phiếu:</span></p>
                                {TraiPhieu}

                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>6.3. Vốn
                                        góp:</span></p>
                               {VonGop}

                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>6.4. Các loại giấy tờ
                                        có giá khác: </span></p>

                                {GiayToKhac}

                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>7. Tài sản khác mà
                                            mỗi tài sản có giá trị từ 50 triệu đồng
                                            trở lên, bao gồm:</span></p>

                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>7.1. Tài sản theo quy
                                        định của pháp luật phải đăng ký sử
                                        dụng và được cấp giấy đăng ký (tầu bay, tầu thủy, thuyền, máy ủi, máy xúc, ô
                                        tô, mô tô, xe gắn máy...):</span></p>
                                {TaiSanPhapLuatQuyDinh}

                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>7.2. Tài sản khác (đồ
                                        mỹ nghệ, đồ thờ cúng, bàn ghế, cây
                                        cảnh, tranh, ảnh, các loại tài sản khác):</span></p>

                                  {TaiSanKhac}

                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>8. Tài sản ở nước
                                            ngoài:</span></p>







                                 <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>8.1. Quyền sử dụng
                                            thực tế đối với đất</span>:</p>
                                 <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>8.1.1. Đất
                                                    ở</span><span style='color:black'> 
                                                :</span></p>

                                {tsnnDatO}
                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>8.1.2. Các loại đất
                                            khác </span><span lang=VI style='color:black'>:</span></p>

                                {tsnnCacLoaiDatKhac}
                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>8.2. Nhà ở, công
                                            trình xây dựng:</span></p>

                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>8.2.1. Nhà
                                            ở:</span></p>

                                {tsnnNhaO}

                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>8.2.2. Công trình xây
                                            dựng khác</span></p>

                                {tsnnCongTrinhXayDung}
                                
                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>8.3. Tài sản khác gắn
                                            liền với đất:</span></p>
                                {tsnnTaiSanGanVoiDat}

                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>8.3.1. Cây lâu
                                            năm</span>:</p>

                                <table class=MsoNormalTable border=0 cellspacing=0 cellpadding=0 width='100%'
                                    style='width:100.0%;border-collapse:collapse'>
                                    {tsnnCayLauNam}
                                </table>

                              

                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>8.3.2. Rừng sản
                                            xuất:</span></p>

                                {tsnnRungSanXuat}

                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>8.3.3. Vật kiến trúc
                                            khác gắn liền với đất</span><span style='color:black'>:</span></p>

                                {tsnnVatKienTruc}
                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>8.4. Vàng, kim cương,
                                            bạch kim và các kim loại quý, đá quý
                                            khác có tổng giá trị từ 50 triệu đồng trở lên:</span></p>
                                {tsnnVang}
                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>8.5. Tiền (tiền Việt
                                            Nam, ngoại tệ) gồm tiền mặt, tiền cho
                                            vay, tiền trả trước, tiền gửi cá nhân, tổ chức trong nước, tổ chức nước ngoài
                                            tại Việt Nam mà tổng giá trị quy đổi từ 50 triệu đồng trở lên:</span></p>
                                {tsnnTien}
                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>8.6. Cổ phiếu, trái
                                            phiếu, vốn góp, các loại giấy tờ có giá
                                            khác mà tổng giá trị từ 50 triệu đồng trở lên (khai theo từng loại):</span></p>

                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>8.6.1. Cổ phiếu:</span>
                                </p>
                                
                                {tsnnCoPhieu}

                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>8.6.2. Trái
                                        phiếu:</span></p>
                                {tsnnTraiPhieu}

                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>8.6.3. Vốn
                                        góp:</span></p>
                               {tsnnVonGop}

                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>8.6.4. Các loại giấy tờ
                                        có giá khác: </span></p>

                                {tsnnGiayToKhac}

                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>8.7. Tài sản khác mà
                                            mỗi tài sản có giá trị từ 50 triệu đồng
                                            trở lên, bao gồm:</span></p>

                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>8.7.1. Tài sản theo quy
                                        định của pháp luật phải đăng ký sử
                                        dụng và được cấp giấy đăng ký (tầu bay, tầu thủy, thuyền, máy ủi, máy xúc, ô
                                        tô, mô tô, xe gắn máy...):</span></p>
                                {tsnnTaiSanPhapLuatQuyDinh}

                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>8.7.2. Tài sản khác (đồ
                                        mỹ nghệ, đồ thờ cúng, bàn ghế, cây
                                        cảnh, tranh, ảnh, các loại tài sản khác):</span></p>

                                  {tsnnTaiSanKhac}

                                <p class=MsoNormal style='margin-bottom:6.0pt;line-height:150%'><span style='color:black'>9. Tài khoản ở nước
                                            ngoài:</span></p>
                                {TaiKhoanNuocNgoai}

                                
                                    {biendongkk}
                                    </table>
                                    <br/>
                                    <br/>
                            <table class='MsoNormalTable' border='1' cellspacing='0' cellpadding='0' width='100%' style='width:100.0%;border-collapse:collapse;border:none'>
                                        <tbody><tr>
                                            <td width='50%' valign='top' style='width:50.0%;border:none;padding:0; margin: 0;'>
                                                <p class='MsoNormal' align='center' style='margin-bottom:6.0pt;text-align:center;
                                line-height:150%;background:white'><i><span style='color:black'>Khánh Hòa,
                                                            ngày .... tháng .... năm ......<br>
                                                        </span></i><b><span style='color:black'>NGƯỜI NHẬN BẢN KÊ KHAI<br>
                                                        </span></b><i><span style='color:black'>(Ký, ghi rõ họ tên, chức vụ/chức
                                                            danh)</span></i></p>
                                            </td>
                                            <td width='50%' valign='top' style='width:50.0%;border:none;padding:0; margin: 0;'>
                                                <p class='MsoNormal' align='center' style='margin-bottom:6.0pt;text-align:center;
                                line-height:150%;background:white'><i><span style='color:black'>Khánh Hòa,
                                                            ngày {DateTime.Now.Day.ToString()} tháng {DateTime.Now.Month.ToString()} năm {DateTime.Now.Year.ToString()}<br>
                                                        </span></i><b><span style='color:black'>NGƯỜI KÊ KHAI TÀI SẢN<br>
                                                        </span></b><i><span style='color:black'>(Ký, ghi rõ họ tên)<br>
                                                            <br>
                                                            <br>
                                                            <br>
                                                        </span></i>
                                                        <span style='text-transform: uppercase;'><b>{data2.nguoikekhai.HoTen}</b></span></p>
                                            </td>
                                        </tr>
                                    </tbody>
                            </table>
                        </body>

                        </html>";



                string filename = System.Guid.NewGuid().ToString();
                string _filename = filename + ".pdf";
                string _path = Path.Combine(Server.MapPath("~/Content/uploads"), _filename);
                ConverterProperties properties = new ConverterProperties();
                properties.SetFontProvider(new DefaultFontProvider(true, true, true));
                HtmlConverter.ConvertToPdf(html, new FileStream(_path, FileMode.Create), properties);

                return Json(filename, JsonRequestBehavior.AllowGet);

            }
            return Json(false, JsonRequestBehavior.AllowGet);


        }


    }

    
}