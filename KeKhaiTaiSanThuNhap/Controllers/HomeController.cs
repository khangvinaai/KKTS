using FluentEmail.Core;
using FluentEmail.Smtp;
using KeKhaiTaiSanThuNhap.Hubs;
using KeKhaiTaiSanThuNhap.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace KeKhaiTaiSanThuNhap.Controllers
{
    [Authorize(Roles = "ADMIN")]
    public class HomeController : Controller
    {
        private KSTNEntities db = new KSTNEntities();
        private UserInfo user = new UserInfo();

        public ActionResult Index()
        {
            var userid = user.GetUser();
            var canbo = db.DM_CanBo.Find(userid);
            ViewBag.Role = user.GetRole();
            ViewBag.Year = DateTime.Now.Year;
            var ToYear = DateTime.Now.Year;
            ViewBag.MaKeHoach = (db.NV_LapKeHoachKeKhai.Count() == 0)? 0 : db.NV_LapKeHoachKeKhai.FirstOrDefault().MaKeHoachKeKhai;

            if (canbo.IsCapNhat == null || canbo.IsCapNhat == false)
            {
                return RedirectToAction($"cap-nhat/{canbo.Ma_CanBo}", "DM_CanBo");
            }
            else
            {
                var UserID = user.GetUser();
                var maCoQuan = user.GetUserCoQuan();
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
                              where cb.Ma_CanBo == UserID && dsbs.TrangThai == true && dskhkk.KeHoachNam == ToYear
                              select new KeKhaiTSTN
                              {
                                  Ma_KeKhai = db.Nv_KeKhai_TSTN.FirstOrDefault(_ => _.Ma_CanBo == cb.Ma_CanBo && _.MaKeHoachKeKhai == dsbs.MaKeHoachKeKhai && _.Ma_Loai_KeKhai == 3).Ma_KeKhai_TSTN == null ? 0 : db.Nv_KeKhai_TSTN.FirstOrDefault(_ => _.Ma_CanBo == cb.Ma_CanBo && _.MaKeHoachKeKhai == dsbs.MaKeHoachKeKhai && _.Ma_Loai_KeKhai == 3).Ma_KeKhai_TSTN,
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
                                  completekk = completekk.Contains((int)dsbs.MaKeHoachKeKhai)
                              }).ToList();

                var datahn = (from cb in db.DM_CanBo
                              join dsbsct in db.NV_LapKeHoachKeKhai_DanhSachKeKhaiHangNam_ChiTiet on cb.Ma_CanBo equals dsbsct.Ma_CanBo
                              join dsbs in db.NV_LapKeHoachKeKhai_DanhSachKeKhaiHangNam on dsbsct.MaKeHoachKeKhai_DanhSachKeKhaiHangNam_ID equals dsbs.MaKeHoachKeKhai_DanhSachKeKhaiHangNam_ID
                              join dskhkk in db.NV_LapKeHoachKeKhai on dsbs.MaKeHoachKeKhai equals dskhkk.MaKeHoachKeKhai
                              join cq in db.DM_CoQuanDonVi on dsbs.Ma_CoQuan_DonVi equals cq.Ma_CoQuan_DonVi
                              where cb.Ma_CanBo == UserID && dsbs.TrangThai == true && dskhkk.KeHoachNam == ToYear
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
                              where cb.Ma_CanBo == UserID && dsbs.TrangThai == true && dskhkk.KeHoachNam == ToYear
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
                                  where cb.Ma_CanBo == UserID && dsbs.TrangThai == true && dskhkk.KeHoachNam == ToYear
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

                ViewBag.KeKhaiLanDau = datald.Count();
                ViewBag.KeKhaiHangNam = datahn.Count();
                ViewBag.KeKhaiBoSung = databs.Count();
                ViewBag.KeKhaiBoNhiemCanBo = databnctcb.Count();

                var data = databs.Concat(datald).Concat(datahn).Concat(databnctcb).OrderByDescending(_ => _.KeHoachNam).ToList();
                ViewBag.KeKhaiQuaHan = data.Where(_ => _.completekk == false && _.TrangThaiKK == false && _.ThoiGianKetThuc > DateTime.Now).Count();

                return View();
            }
            
           
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


        [HttpPost]
        public JsonResult GuiThongBao(int? MaCanBo_TB, int? MaCoQuan_TB, int? Loai_TB, string TieuDe_TB, string NoiDung_TB)
        {
            if(Loai_TB == 0)
            {
                var tb = new HT_ThongBao();
                tb.NguoiGui = user.GetUser();
                tb.NguoiNhan = MaCanBo_TB;
                tb.TrangThai = true;
                tb.NoiDung = NoiDung_TB;
                tb.TieuDe = TieuDe_TB;
                tb.ThoiGian = DateTime.Now;
                db.HT_ThongBao.Add(tb);
                db.SaveChanges();
                MyHub.ReloadData();
                return Json(new { status="success",title="Thành Công",message="Đã Gửi Thông Báo Thành Công"}, JsonRequestBehavior.AllowGet);
            }
            else if(Loai_TB == 1)
            {

                var MaCanBo = user.GetUser();
                var cb = db.DM_CanBo.Where(_ => _.Ma_CanBo == MaCanBo).FirstOrDefault();
                var cq = db.DM_CoQuanDonVi.Where(_ => _.Ma_CoQuan_DonVi == cb.Ma_CoQuan_DonVi).FirstOrDefault();
                var cv = db.DM_ChucVu_ChucDanh.Where(_ => _.Ma_ChucVu_ChucDanh == cb.Ma_ChucVu_ChucDanh).FirstOrDefault();

                var cb1 = db.DM_CanBo.Where(_ => _.Ma_CanBo == MaCanBo_TB).FirstOrDefault();

                Task t1 = sendmail(cb1.Email,cq.Ten + " - " + cb.HoTen,NoiDung_TB, TieuDe_TB);

                return Json(new { status = "success", title = "Thành Công", message = "Đã Gửi Email Thành Công" }, JsonRequestBehavior.AllowGet);
            }
           
            return Json(true, JsonRequestBehavior.AllowGet);   
        }

        public async Task sendmail(string emailName, string TenNguoiGui, string NoiDung, string TieuDe)
        {
            var sender = new SmtpSender(() => new System.Net.Mail.SmtpClient("smtp.gmail.com")
            {
                UseDefaultCredentials = false,
                Port = 587,
                Credentials = new NetworkCredential("vinaai.noreply.vn@gmail.com", "zkatseozgyalyeyo"),
                EnableSsl = true,
            });

            Email.DefaultSender = sender;

            var email = Email.From("vinaai.noreply.vn@gmail.com", TenNguoiGui)
                             .To(emailName)
                             .Subject(TieuDe)
                             .Body($"<div><div></div><div style ='width:100%;height:2px;background-color:grey;margin-top:1rem'></div><div style ='font-size: 16px;'><p style = 'color:black'>Xin chào<strong> bạn!<strong></strong></strong></p><p style = 'color:black'>Cảm ơn bạn đã tin tưởng và sử dụng dịch vụ từ <a href ='https://www.vinaai.com/'>VinaAI</a></p><p style = 'color:black'>{NoiDung}</p></div><div style = 'width:100%;height:2px;background-color:grey;margin-top:1rem'></div> ", true);
            try
            {
                await email.SendAsync();
            }
            catch (Exception e)
            {

            }
        }


        public JsonResult GetQuyen(string MenuCode)
        {
            var username = user.GetUserNameAccount();
            var tk = db.HT_TaiKhoan.FirstOrDefault(_ => _.TenTaiKhoan == username).MaNhomTaiKhoan;
            var ntk = db.HT_NhomTaiKhoan.FirstOrDefault(_ => _.MaNhomTaiKhoan == tk).MaTaiKhoan;

            var data = db.HT_ChiTietPhanQuyen.Where(_ => _.MaTaiKhoan == ntk && _.TrangThai == true && _.MenuCode.ToUpper() == MenuCode.ToUpper()).Select(_ => _.ChucNangCode.ToUpper());

            return Json(data, JsonRequestBehavior.AllowGet);
        }

    }
}