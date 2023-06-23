using DocumentFormat.OpenXml.Drawing.Charts;
using iText.Html2pdf;
using iText.Html2pdf.Resolver.Font;
using iText.Layout.Font;
using KeKhaiTaiSanThuNhap.Models;
using Org.BouncyCastle.Bcpg.OpenPgp;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.Services;

namespace KeKhaiTaiSanThuNhap.Controllers
{
    [Authorize(Roles = "ADMIN")]
    public class NV_DanhSachCanBoKeKhaiController : Controller
    {
        private KSTNEntities db = new KSTNEntities();
        private UserInfo user = new UserInfo();
        public ActionResult Index()
        {
            if (user.CheckQuyen("NV_DanhSachCanBoKeKhai", "Xem"))
            {
                return new HttpStatusCodeResult(404, "Not found");
            }
            return View();
        }

        public ActionResult BanIn(string id)
        {
            ViewBag.TenFile = id + ".pdf";
            return View();
        }

        public ActionResult ThemChiTiet_CanBoKeKhai(int id)
        {
            ViewBag.id = id;

            var MaCanBo = user.GetUser();
            var Role = user.GetRole();
            var maCoQuan = user.GetUserCoQuan();

            var KHKK = db.NV_LapKeHoachKeKhai.Where(_ => _.MaKeHoachKeKhai == id);



            if (KHKK.Count() == 0)
            {
                return new HttpStatusCodeResult(404, "Not found");
            }
            else
            {
                var KHKK1 = KHKK.First();
                if (Role != "ADMIN")
                {
                    if (KHKK1.Ma_CoQuan_DonVi != maCoQuan)
                    {
                        return new HttpStatusCodeResult(404, "Not found");
                    }
                }


                if (KHKK1.TrangThai == true)
                {
                    if (user.CheckQuyen("NV_DanhSachCanBoKeKhai", "XemChiTiet"))
                    {
                        return new HttpStatusCodeResult(404, "Not found");
                    }
                }
                else
                {
                    if (user.CheckQuyen("NV_DanhSachCanBoKeKhai", "LapDanhSach"))
                    {
                        return new HttpStatusCodeResult(404, "Not found");
                    }
                }

                if (KHKK1.Ma_Loai_KeKhai == 3)
                {
                    ViewBag.TenLoaiKeHoach = "DANH SÁCH CÁN BỘ KÊ KHAI LẦN ĐẦU";
                    var DSLD = db.NV_LapKeHoachKeKhai_DanhSachKeKhaiLanDau.Where(_ => _.MaKeHoachKeKhai == id).First();
                    ViewBag.TrangThai = DSLD.TrangThai;
                    ViewBag.TrangThaiKKHN = false;


                }
                else if (KHKK1.Ma_Loai_KeKhai == 4)
                {
                    ViewBag.TenLoaiKeHoach = "DANH SÁCH CÁN BỘ KÊ KHAI HẰNG NĂM";
                    var DSHN = db.NV_LapKeHoachKeKhai_DanhSachKeKhaiHangNam.Where(_ => _.MaKeHoachKeKhai == id).First();
                    ViewBag.TrangThai = DSHN.TrangThai;
                    ViewBag.TrangThaiKKHN = true;
                }
                else
                {
                    ViewBag.TenLoaiKeHoach = "DANH SÁCH CÁN BỘ KÊ KHAI PHỤC VỤ CÔNG TÁC CÁN BỘ";
                    var DSBNCTCB = db.NV_LapKeHoachKeKhai_DanhSachKeKhaiBoNhiemCongTacCanBo.Where(_ => _.MaKeHoachKeKhai == id).First();
                    ViewBag.TrangThai = DSBNCTCB.TrangThai;
                    ViewBag.TrangThaiKKHN = false;
                }
            }
            return View();
        }

        public JsonResult deleteDanhSach(int Ma_CanBo, int MaKeHoachKeKhai)
        {

            var KHKK = db.NV_LapKeHoachKeKhai.Where(_ => _.MaKeHoachKeKhai == MaKeHoachKeKhai).First();

            if (KHKK.Ma_Loai_KeKhai == 3)
            {
                var data = (from dshnct in db.NV_LapKeHoachKeKhai_DanhSachKeKhaiLanDau_ChiTiet
                            join dshn in db.NV_LapKeHoachKeKhai_DanhSachKeKhaiLanDau on dshnct.MaKeHoachKeKhai_DanhSachKeKhaiLanDau_ID equals dshn.MaKeHoachKeKhai_DanhSachKeKhaiLanDau_ID
                            where dshnct.Ma_CanBo == Ma_CanBo && dshn.MaKeHoachKeKhai == MaKeHoachKeKhai
                            select dshnct).SingleOrDefault();

                db.NV_LapKeHoachKeKhai_DanhSachKeKhaiLanDau_ChiTiet.Remove(data);
                db.SaveChanges();

                return Json(new { isConfirmed = true, message = "Xóa Thành Công Cán Bộ Khỏi Danh Sách Kê Khai Lần Đầu" }, JsonRequestBehavior.AllowGet);
            }
            else if (KHKK.Ma_Loai_KeKhai == 4)
            {
                var data = (from dshnct in db.NV_LapKeHoachKeKhai_DanhSachKeKhaiHangNam_ChiTiet
                            join dshn in db.NV_LapKeHoachKeKhai_DanhSachKeKhaiHangNam on dshnct.MaKeHoachKeKhai_DanhSachKeKhaiHangNam_ID equals dshn.MaKeHoachKeKhai_DanhSachKeKhaiHangNam_ID
                            where dshnct.Ma_CanBo == Ma_CanBo && dshn.MaKeHoachKeKhai == MaKeHoachKeKhai
                            select dshnct).SingleOrDefault();

                db.NV_LapKeHoachKeKhai_DanhSachKeKhaiHangNam_ChiTiet.Remove(data);
                db.SaveChanges();

                return Json(new { isConfirmed = true, message = "Xóa Thành Công Cán Bộ Khỏi Danh Sách Kê Khai Hằng Năm" }, JsonRequestBehavior.AllowGet);
            }
            else if (KHKK.Ma_Loai_KeKhai == 5)
            {
                var data = (from dshnct in db.NV_LapKeHoachKeKhai_DanhSachKeKhaiBoSung_ChiTiet
                            join dshn in db.NV_LapKeHoachKeKhai_DanhSachKeKhaiBoSung on dshnct.MaKeHoachKeKhai_DanhSachKeKhaiBoSung_ID equals dshn.MaKeHoachKeKhai_DanhSachKeKhaiBoSung_ID
                            where dshnct.Ma_CanBo == Ma_CanBo && dshn.MaKeHoachKeKhai == MaKeHoachKeKhai
                            select dshnct).SingleOrDefault();

                db.NV_LapKeHoachKeKhai_DanhSachKeKhaiBoSung_ChiTiet.Remove(data);
                db.SaveChanges();

                return Json(new { isConfirmed = true, message = "Xóa Thành Công Cán Bộ Khỏi Danh Sách Kê Khai Bổ Sung" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var data = (from dshnct in db.NV_LapKeHoachKeKhai_DanhSachKeKhaiBoNhiemCongTacCanBo_ChiTiet
                            join dshn in db.NV_LapKeHoachKeKhai_DanhSachKeKhaiBoNhiemCongTacCanBo on dshnct.MaKeHoachKeKhai_DanhSachKeKhaiBoNhiemCongTacCanBo_ID equals dshn.MaKeHoachKeKhai_DanhSachKeKhaiBoNhiemCongTacCanBo_ID
                            where dshnct.Ma_CanBo == Ma_CanBo && dshn.MaKeHoachKeKhai == MaKeHoachKeKhai
                            select dshnct).SingleOrDefault();

                db.NV_LapKeHoachKeKhai_DanhSachKeKhaiBoNhiemCongTacCanBo_ChiTiet.Remove(data);
                db.SaveChanges();

                return Json(new { isConfirmed = true, message = "Xóa Thành Công Cán Bộ Khỏi Danh Sách Kê Khai Phục Vụ Công Tác Cán Bộ" }, JsonRequestBehavior.AllowGet);
            }

        }

        public JsonResult deleteDanhSach1(int Ma_CanBo, int MaKeHoachKeKhai)
        {
            var KHKK = db.NV_LapKeHoachKeKhai.Where(_ => _.MaKeHoachKeKhai == MaKeHoachKeKhai).First();
            var data = (from dshnct in db.NV_LapKeHoachKeKhai_DanhSachKeKhaiBoSung_ChiTiet
                        join dshn in db.NV_LapKeHoachKeKhai_DanhSachKeKhaiBoSung on dshnct.MaKeHoachKeKhai_DanhSachKeKhaiBoSung_ID equals dshn.MaKeHoachKeKhai_DanhSachKeKhaiBoSung_ID
                        where dshnct.Ma_CanBo == Ma_CanBo && dshn.MaKeHoachKeKhai == MaKeHoachKeKhai
                        select dshnct).SingleOrDefault();

            db.NV_LapKeHoachKeKhai_DanhSachKeKhaiBoSung_ChiTiet.Remove(data);
            db.SaveChanges();
            return Json(new { isConfirmed = true, message = "Xóa Thành Công Cán Bộ Khỏi Danh Sách Kê Khai Bổ Sung" }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult LoadData(int id)
        {
            //var maCoQuan = user.GetUserCoQuan();



            var KHKK = db.NV_LapKeHoachKeKhai.Where(_ => _.MaKeHoachKeKhai == id).First();

            int? maCoQuan = KHKK.Ma_CoQuan_DonVi;

            if (KHKK.Ma_Loai_KeKhai == 3)
            {
                var DanhSachCanBoDaKeKhaiLanDau = (from kkts in db.Nv_KeKhai_TSTN
                                                   join cb in db.DM_CanBo on kkts.Ma_CanBo equals cb.Ma_CanBo
                                                   join tk in db.HT_TaiKhoan on cb.Ma_CanBo equals tk.Ma_CanBo
                                                   join ntk in db.HT_NhomTaiKhoan on tk.MaNhomTaiKhoan equals ntk.MaNhomTaiKhoan
                                                   where cb.Ma_CoQuan_DonVi == maCoQuan && kkts.Ma_Loai_KeKhai == 3 && kkts.TrangThai == true && ntk.MaTaiKhoan != "NDDTTT"
                                                   select cb.Ma_CanBo);

                var DanhSachCanBoDaKeKhaiPhucVuCongTacCanBo = (from kkts in db.Nv_KeKhai_TSTN
                                                               join cb in db.DM_CanBo on kkts.Ma_CanBo equals cb.Ma_CanBo
                                                               join tk in db.HT_TaiKhoan on cb.Ma_CanBo equals tk.Ma_CanBo
                                                               join ntk in db.HT_NhomTaiKhoan on tk.MaNhomTaiKhoan equals ntk.MaNhomTaiKhoan
                                                               where cb.Ma_CoQuan_DonVi == maCoQuan && kkts.Ma_Loai_KeKhai == 12 && kkts.TrangThai == true && ntk.MaTaiKhoan != "NDDTTT"
                                                               select cb.Ma_CanBo);


                var DanhSachCanBoKeKhaiLanDauConHan = (from dskkldct in db.NV_LapKeHoachKeKhai_DanhSachKeKhaiLanDau_ChiTiet
                                                       join dskkld in db.NV_LapKeHoachKeKhai_DanhSachKeKhaiLanDau on dskkldct.MaKeHoachKeKhai_DanhSachKeKhaiLanDau_ID equals dskkld.MaKeHoachKeKhai_DanhSachKeKhaiLanDau_ID
                                                       join lkh in db.NV_LapKeHoachKeKhai on dskkld.MaKeHoachKeKhai equals lkh.MaKeHoachKeKhai
                                                       where lkh.Ma_Loai_KeKhai == 3 && lkh.ThoiGianBatDau <= DateTime.Now && lkh.ThoiGianKetThuc >= DateTime.Now && lkh.Ma_CoQuan_DonVi == maCoQuan
                                                       select dskkldct.Ma_CanBo);


                var DanhSachCanBoKeKhaiBoNhiemCongTacCanBoConHan = (from dskkldct in db.NV_LapKeHoachKeKhai_DanhSachKeKhaiBoNhiemCongTacCanBo_ChiTiet
                                                                    join dskkld in db.NV_LapKeHoachKeKhai_DanhSachKeKhaiBoNhiemCongTacCanBo on dskkldct.MaKeHoachKeKhai_DanhSachKeKhaiBoNhiemCongTacCanBo_ID equals dskkld.MaKeHoachKeKhai_DanhSachKeKhaiBoNhiemCongTacCanBo_ID
                                                                    join lkh in db.NV_LapKeHoachKeKhai on dskkld.MaKeHoachKeKhai equals lkh.MaKeHoachKeKhai
                                                                    where lkh.Ma_Loai_KeKhai == 12 && lkh.ThoiGianBatDau <= DateTime.Now && lkh.ThoiGianKetThuc >= DateTime.Now && lkh.Ma_CoQuan_DonVi == maCoQuan
                                                                    select dskkldct.Ma_CanBo);

                var DanhSachCanBoKeKhaiDaCo = (from dskkldct in db.NV_LapKeHoachKeKhai_DanhSachKeKhaiLanDau_ChiTiet
                                               join dskkld in db.NV_LapKeHoachKeKhai_DanhSachKeKhaiLanDau on dskkldct.MaKeHoachKeKhai_DanhSachKeKhaiLanDau_ID equals dskkld.MaKeHoachKeKhai_DanhSachKeKhaiLanDau_ID
                                               where dskkld.MaKeHoachKeKhai == id
                                               select dskkldct.Ma_CanBo);


                var data = (from cb in db.DM_CanBo
                            join cv in db.DM_ChucVu_ChucDanh on cb.Ma_ChucVu_ChucDanh equals cv.Ma_ChucVu_ChucDanh
                            join cq in db.DM_CoQuanDonVi on cb.Ma_CoQuan_DonVi equals cq.Ma_CoQuan_DonVi
                            join tk1 in db.HT_TaiKhoan on cb.Ma_CanBo equals tk1.Ma_CanBo
                            join ntk in db.HT_NhomTaiKhoan on tk1.MaNhomTaiKhoan equals ntk.MaNhomTaiKhoan
                            where ntk.MaNhomTaiKhoan != 1 && ntk.MaTaiKhoan != "NDDTTT" && cb.Ma_CoQuan_DonVi == maCoQuan && !DanhSachCanBoDaKeKhaiLanDau.Contains(cb.Ma_CanBo) && !DanhSachCanBoKeKhaiLanDauConHan.Contains(cb.Ma_CanBo) && !DanhSachCanBoKeKhaiDaCo.Contains(cb.Ma_CanBo) && !DanhSachCanBoKeKhaiBoNhiemCongTacCanBoConHan.Contains(cb.Ma_CanBo) && !DanhSachCanBoDaKeKhaiPhucVuCongTacCanBo.Contains(cb.Ma_CanBo)
                            orderby ntk.Sort
                            select new { ntk.MaNhomTaiKhoan, cb.Ma_CanBo, cb.HoTen, TenCanBo = cb.Ten, cb.Email, cb.DoB, cb.SoCCCD, cb.DiaChiThuongTru, cv.Ten_ChucVu_ChucDanh, cq.Ten, cq.Ma_CoQuan_DonVi }).ToList();
                return Json(new { data }, JsonRequestBehavior.AllowGet);
            }
            else if (KHKK.Ma_Loai_KeKhai == 4)
            {
                // Danh Sách Cán Bộ Đã Kê Khai Lần Đầu
                var DanhSachCanBoDaKeKhaiLanDau = (from kkts in db.Nv_KeKhai_TSTN
                                                   join lkh in db.NV_LapKeHoachKeKhai on kkts.MaKeHoachKeKhai equals lkh.MaKeHoachKeKhai
                                                   join cb in db.DM_CanBo on kkts.Ma_CanBo equals cb.Ma_CanBo
                                                   where cb.Ma_CoQuan_DonVi == maCoQuan && kkts.Ma_Loai_KeKhai == 3 && kkts.TrangThai == true && lkh.KeHoachNam <= KHKK.KeHoachNam
                                                   select cb.Ma_CanBo);

                var DanhSachCanBoDaKeKhaiBoNhiemCongTacCanBo = (from kkts in db.Nv_KeKhai_TSTN
                                                                join lkh in db.NV_LapKeHoachKeKhai on kkts.MaKeHoachKeKhai equals lkh.MaKeHoachKeKhai
                                                                join cb in db.DM_CanBo on kkts.Ma_CanBo equals cb.Ma_CanBo
                                                                where cb.Ma_CoQuan_DonVi == maCoQuan && kkts.Ma_Loai_KeKhai == 12 && kkts.TrangThai == true && lkh.KeHoachNam <= KHKK.KeHoachNam
                                                                select cb.Ma_CanBo);

                var DanhSachCanBoKeKhaiBoSungDaCo = (from dskkldct in db.NV_LapKeHoachKeKhai_DanhSachKeKhaiBoSung_ChiTiet
                                                     join dskkld in db.NV_LapKeHoachKeKhai_DanhSachKeKhaiBoSung on dskkldct.MaKeHoachKeKhai_DanhSachKeKhaiBoSung_ID equals dskkld.MaKeHoachKeKhai_DanhSachKeKhaiBoSung_ID
                                                     join lkh in db.NV_LapKeHoachKeKhai on dskkld.MaKeHoachKeKhai equals lkh.MaKeHoachKeKhai
                                                     where dskkld.MaKeHoachKeKhai == id
                                                     select dskkldct.Ma_CanBo);


                var DanhSachCanBoKeKhaiHangNamDaCo = (from dskkldct in db.NV_LapKeHoachKeKhai_DanhSachKeKhaiHangNam_ChiTiet
                                                      join dskkld in db.NV_LapKeHoachKeKhai_DanhSachKeKhaiHangNam on dskkldct.MaKeHoachKeKhai_DanhSachKeKhaiHangNam_ID equals dskkld.MaKeHoachKeKhai_DanhSachKeKhaiHangNam_ID
                                                      where dskkld.MaKeHoachKeKhai == id
                                                      select dskkldct.Ma_CanBo);


                var data = (from cb in db.DM_CanBo
                            join cv in db.DM_ChucVu_ChucDanh on cb.Ma_ChucVu_ChucDanh equals cv.Ma_ChucVu_ChucDanh
                            join cq in db.DM_CoQuanDonVi on cb.Ma_CoQuan_DonVi equals cq.Ma_CoQuan_DonVi
                            join tk1 in db.HT_TaiKhoan on cb.Ma_CanBo equals tk1.Ma_CanBo
                            join ntk in db.HT_NhomTaiKhoan on tk1.MaNhomTaiKhoan equals ntk.MaNhomTaiKhoan
                            where ntk.MaNhomTaiKhoan != 1 && ntk.MaTaiKhoan != "NDDTTT" && cb.Ma_CoQuan_DonVi == maCoQuan && !DanhSachCanBoKeKhaiHangNamDaCo.Contains(cb.Ma_CanBo) && !DanhSachCanBoKeKhaiBoSungDaCo.Contains(cb.Ma_CanBo) && (DanhSachCanBoDaKeKhaiLanDau.Contains(cb.Ma_CanBo) || DanhSachCanBoDaKeKhaiBoNhiemCongTacCanBo.Contains(cb.Ma_CanBo))
                            orderby ntk.Sort
                            select new { ntk.MaNhomTaiKhoan, cb.Ma_CanBo, cb.HoTen, TenCanBo = cb.Ten, cb.Email, cb.DoB, cb.SoCCCD, cb.DiaChiThuongTru, cv.Ten_ChucVu_ChucDanh, cq.Ten, cq.Ma_CoQuan_DonVi }).ToList();
                return Json(new { data }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var DanhSachCanBoKeKhaiDaCo = (from dskkldct in db.NV_LapKeHoachKeKhai_DanhSachKeKhaiBoNhiemCongTacCanBo_ChiTiet
                                               join dskkld in db.NV_LapKeHoachKeKhai_DanhSachKeKhaiBoNhiemCongTacCanBo on dskkldct.MaKeHoachKeKhai_DanhSachKeKhaiBoNhiemCongTacCanBo_ID equals dskkld.MaKeHoachKeKhai_DanhSachKeKhaiBoNhiemCongTacCanBo_ID
                                               where dskkld.MaKeHoachKeKhai == id
                                               select dskkldct.Ma_CanBo);

                var data = (from cb in db.DM_CanBo
                            join cv in db.DM_ChucVu_ChucDanh on cb.Ma_ChucVu_ChucDanh equals cv.Ma_ChucVu_ChucDanh
                            join cq in db.DM_CoQuanDonVi on cb.Ma_CoQuan_DonVi equals cq.Ma_CoQuan_DonVi
                            join tk1 in db.HT_TaiKhoan on cb.Ma_CanBo equals tk1.Ma_CanBo
                            join ntk in db.HT_NhomTaiKhoan on tk1.MaNhomTaiKhoan equals ntk.MaNhomTaiKhoan
                            where ntk.MaNhomTaiKhoan != 1 && ntk.MaTaiKhoan != "NDDTTT" && cb.Ma_CoQuan_DonVi == maCoQuan && !DanhSachCanBoKeKhaiDaCo.Contains(cb.Ma_CanBo)
                            orderby ntk.Sort
                            select new { ntk.MaNhomTaiKhoan, cb.Ma_CanBo, cb.HoTen, TenCanBo = cb.Ten, cb.Email, cb.DoB, cb.SoCCCD, cb.DiaChiThuongTru, cv.Ten_ChucVu_ChucDanh, cq.Ten, cq.Ma_CoQuan_DonVi }).ToList();
                return Json(new { data }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult LoadData2(int id)
        {
            var MaCanBo = user.GetUser();
            var Role = user.GetRole();
            var maCoQuan = user.GetUserCoQuan();

            var KHKK = db.NV_LapKeHoachKeKhai.Where(_ => _.MaKeHoachKeKhai == id).First();

            var draw = Request.Form.GetValues("draw").FirstOrDefault();
            var start = Request.Form.GetValues("start").FirstOrDefault();
            var length = Request.Form.GetValues("length").FirstOrDefault();
            var HoTen = Request.Form.GetValues("columns[0][search][value]").FirstOrDefault();
            int pageSize = length != null ? Convert.ToInt32(length) : 0;
            int skip = start != null ? Convert.ToInt32(start) : 0;
            int recordsTotal = 0;

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
                            select new
                            {
                                ntk.MaNhomTaiKhoan,
                                cb.Ma_CanBo,
                                cb.HoTen,
                                cb.SoCCCD,
                                cv.Ten_ChucVu_ChucDanh,
                                cq.Ten,
                                lkkhn.KeHoachNam,
                                lkkhn.NghiDinh,
                                lkkhn.TenKeHoachKeKhai,
                                lkkhn.MaKeHoachKeKhai,
                                dskkhn.TrangThai,
                                isKeKhai = (db.Nv_KeKhai_TSTN.Count(_ => _.Ma_CanBo == cb.Ma_CanBo && _.Ma_Loai_KeKhai == 3 && _.TrangThai == true && _.MaKeHoachKeKhai == id) == 1),
                                FileDinhKem = db.Nv_KeKhai_TSTN.FirstOrDefault(_ => _.Ma_CanBo == cb.Ma_CanBo && _.MaKeHoachKeKhai == dskkhn.MaKeHoachKeKhai && _.Ma_Loai_KeKhai == 3).FileDinhKem == null ? "" : db.Nv_KeKhai_TSTN.FirstOrDefault(_ => _.Ma_CanBo == cb.Ma_CanBo && _.MaKeHoachKeKhai == dskkhn.MaKeHoachKeKhai && _.Ma_Loai_KeKhai == 3).FileDinhKem,
                            }).ToList();

                if (!string.IsNullOrEmpty(HoTen))
                {
                    data = data.Where(a => a.HoTen.ToUpper().Contains(HoTen.ToUpper())).ToList();
                }

                recordsTotal = data.Count();
                var data1 = data.Skip(skip).Take(pageSize).ToList();
                return Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data1 }, JsonRequestBehavior.AllowGet);
            }
            else if (KHKK.Ma_Loai_KeKhai == 4)
            {
                var data = (from cb in db.DM_CanBo
                            join cv in db.DM_ChucVu_ChucDanh on cb.Ma_ChucVu_ChucDanh equals cv.Ma_ChucVu_ChucDanh
                            join dskkhn_ct in db.NV_LapKeHoachKeKhai_DanhSachKeKhaiHangNam_ChiTiet on cb.Ma_CanBo equals dskkhn_ct.Ma_CanBo
                            join dskkhn in db.NV_LapKeHoachKeKhai_DanhSachKeKhaiHangNam on dskkhn_ct.MaKeHoachKeKhai_DanhSachKeKhaiHangNam_ID equals dskkhn.MaKeHoachKeKhai_DanhSachKeKhaiHangNam_ID
                            join lkkhn in db.NV_LapKeHoachKeKhai on dskkhn.MaKeHoachKeKhai equals lkkhn.MaKeHoachKeKhai
                            join cq in db.DM_CoQuanDonVi on lkkhn.Ma_CoQuan_DonVi equals cq.Ma_CoQuan_DonVi
                            join tk in db.HT_TaiKhoan on cb.Ma_CanBo equals tk.Ma_CanBo
                            join ntk in db.HT_NhomTaiKhoan on tk.MaNhomTaiKhoan equals ntk.MaNhomTaiKhoan
                            where lkkhn.MaKeHoachKeKhai == id
                            orderby ntk.Sort
                            select new
                            {
                                ntk.MaNhomTaiKhoan,
                                cb.Ma_CanBo,
                                cb.HoTen,
                                cb.SoCCCD,
                                cv.Ten_ChucVu_ChucDanh,
                                cq.Ten,
                                lkkhn.KeHoachNam,
                                lkkhn.NghiDinh,
                                lkkhn.TenKeHoachKeKhai,
                                lkkhn.MaKeHoachKeKhai,
                                dskkhn.TrangThai,
                                isKeKhai = (db.Nv_KeKhai_TSTN.Count(_ => _.Ma_CanBo == cb.Ma_CanBo && _.Ma_Loai_KeKhai == 4 && _.TrangThai == true && _.MaKeHoachKeKhai == id) == 1),
                                FileDinhKem = db.Nv_KeKhai_TSTN.FirstOrDefault(_ => _.Ma_CanBo == cb.Ma_CanBo && _.MaKeHoachKeKhai == dskkhn.MaKeHoachKeKhai && _.Ma_Loai_KeKhai == 4).FileDinhKem == null ? "" : db.Nv_KeKhai_TSTN.FirstOrDefault(_ => _.Ma_CanBo == cb.Ma_CanBo && _.MaKeHoachKeKhai == dskkhn.MaKeHoachKeKhai && _.Ma_Loai_KeKhai == 4).FileDinhKem,
                            }).ToList();

                if (!string.IsNullOrEmpty(HoTen))
                {
                    data = data.Where(a => a.HoTen.ToUpper().Contains(HoTen.ToUpper())).ToList();
                }

                recordsTotal = data.Count();
                var data1 = data.Skip(skip).Take(pageSize).ToList();
                return Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data1 }, JsonRequestBehavior.AllowGet);
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
                            select new
                            {
                                ntk.MaNhomTaiKhoan,
                                cb.Ma_CanBo,
                                cb.HoTen,
                                cb.SoCCCD,
                                cv.Ten_ChucVu_ChucDanh,
                                cq.Ten,
                                lkkhn.KeHoachNam,
                                lkkhn.NghiDinh,
                                lkkhn.TenKeHoachKeKhai,
                                lkkhn.MaKeHoachKeKhai,
                                dskkhn.TrangThai,
                                isKeKhai = (db.Nv_KeKhai_TSTN.Count(_ => _.Ma_CanBo == cb.Ma_CanBo && _.Ma_Loai_KeKhai == 12 && _.TrangThai == true && _.MaKeHoachKeKhai == id) == 1),
                                FileDinhKem = db.Nv_KeKhai_TSTN.FirstOrDefault(_ => _.Ma_CanBo == cb.Ma_CanBo && _.MaKeHoachKeKhai == dskkhn.MaKeHoachKeKhai && _.Ma_Loai_KeKhai == 12).FileDinhKem == null ? "" : db.Nv_KeKhai_TSTN.FirstOrDefault(_ => _.Ma_CanBo == cb.Ma_CanBo && _.MaKeHoachKeKhai == dskkhn.MaKeHoachKeKhai && _.Ma_Loai_KeKhai == 12).FileDinhKem
                            }).ToList();

                if (!string.IsNullOrEmpty(HoTen))
                {
                    data = data.Where(a => a.HoTen.ToUpper().Contains(HoTen.ToUpper())).ToList();
                }

                recordsTotal = data.Count();
                var data1 = data.Skip(skip).Take(pageSize).ToList();
                return Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data1 }, JsonRequestBehavior.AllowGet);
            }



        }

        public JsonResult LoadData3(int id)
        {
            var MaCanBo = user.GetUser();
            var Role = user.GetRole();
            var maCoQuan = user.GetUserCoQuan();

            var KHKK = db.NV_LapKeHoachKeKhai.Where(_ => _.MaKeHoachKeKhai == id).First();

            var draw = Request.Form.GetValues("draw").FirstOrDefault();
            var start = Request.Form.GetValues("start").FirstOrDefault();
            var length = Request.Form.GetValues("length").FirstOrDefault();
            var HoTen = Request.Form.GetValues("columns[0][search][value]").FirstOrDefault();
            int pageSize = length != null ? Convert.ToInt32(length) : 0;
            int skip = start != null ? Convert.ToInt32(start) : 0;
            int recordsTotal = 0;


            var data = (from cb in db.DM_CanBo
                        join cv in db.DM_ChucVu_ChucDanh on cb.Ma_ChucVu_ChucDanh equals cv.Ma_ChucVu_ChucDanh
                        join dskkhn_ct in db.NV_LapKeHoachKeKhai_DanhSachKeKhaiBoSung_ChiTiet on cb.Ma_CanBo equals dskkhn_ct.Ma_CanBo
                        join dskkhn in db.NV_LapKeHoachKeKhai_DanhSachKeKhaiBoSung on dskkhn_ct.MaKeHoachKeKhai_DanhSachKeKhaiBoSung_ID equals dskkhn.MaKeHoachKeKhai_DanhSachKeKhaiBoSung_ID
                        join lkkhn in db.NV_LapKeHoachKeKhai on dskkhn.MaKeHoachKeKhai equals lkkhn.MaKeHoachKeKhai
                        join cq in db.DM_CoQuanDonVi on lkkhn.Ma_CoQuan_DonVi equals cq.Ma_CoQuan_DonVi
                        join tk in db.HT_TaiKhoan on cb.Ma_CanBo equals tk.Ma_CanBo
                        join ntk in db.HT_NhomTaiKhoan on tk.MaNhomTaiKhoan equals ntk.MaNhomTaiKhoan
                        where lkkhn.MaKeHoachKeKhai == id
                        orderby ntk.Sort
                        select new
                        {
                            ntk.MaNhomTaiKhoan,
                            cb.Ma_CanBo,
                            cb.HoTen,
                            cb.SoCCCD,
                            cv.Ten_ChucVu_ChucDanh,
                            cq.Ten,
                            lkkhn.KeHoachNam,
                            lkkhn.NghiDinh,
                            lkkhn.TenKeHoachKeKhai,
                            lkkhn.MaKeHoachKeKhai,
                            dskkhn.TrangThai,
                            isKeKhai = (db.Nv_KeKhai_TSTN.Count(_ => _.Ma_CanBo == cb.Ma_CanBo && _.Ma_Loai_KeKhai == 5 && _.TrangThai == true && _.MaKeHoachKeKhai == id) == 1),
                            FileDinhKem = db.Nv_KeKhai_TSTN.FirstOrDefault(_ => _.Ma_CanBo == cb.Ma_CanBo && _.MaKeHoachKeKhai == dskkhn.MaKeHoachKeKhai && _.Ma_Loai_KeKhai == 5).FileDinhKem == null ? "" : db.Nv_KeKhai_TSTN.FirstOrDefault(_ => _.Ma_CanBo == cb.Ma_CanBo && _.MaKeHoachKeKhai == dskkhn.MaKeHoachKeKhai && _.Ma_Loai_KeKhai == 5).FileDinhKem,
                        }).ToList();

            if (!string.IsNullOrEmpty(HoTen))
            {
                data = data.Where(a => a.HoTen.ToUpper().Contains(HoTen.ToUpper())).ToList();
            }

            recordsTotal = data.Count();
            var data1 = data.Skip(skip).Take(pageSize).ToList();
            return Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data1 }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult LuuDanhSachCanBoKeKhai(string[] MaCanBo, int MaKeHoachKeKhai, int MaLoaiKeKhai)
        {

            if (MaCanBo == null)
            {
                return Json(new { status = "warning", message = "Chưa Chọn Cán Bộ Để Thêm Vào Danh Sách" });
            }


            var MaCoQuan = user.GetUserCoQuan();

            var KHKK = db.NV_LapKeHoachKeKhai.Where(_ => _.MaKeHoachKeKhai == MaKeHoachKeKhai);

            if (KHKK.Count() == 0)
            {
                return Json(new { status = "error", message = "Kế Hoạch Kê Khai Không Tồn Tại" });
            }

            var KHKK1 = KHKK.First();
            if (KHKK1.Ma_Loai_KeKhai == 3)
            {
                var DSLD = db.NV_LapKeHoachKeKhai_DanhSachKeKhaiLanDau.Where(_ => _.MaKeHoachKeKhai == MaKeHoachKeKhai).First();

                var DanhSachCanBoDaKeKhaiLanDau = (from kkts in db.Nv_KeKhai_TSTN
                                                   join cb in db.DM_CanBo on kkts.Ma_CanBo equals cb.Ma_CanBo
                                                   where cb.Ma_CoQuan_DonVi == MaCoQuan && kkts.Ma_Loai_KeKhai == 3 && kkts.TrangThai == true
                                                   select cb.Ma_CanBo);

                var DanhSachCanBoKeKhaiLanDauConHan = (from dskkldct in db.NV_LapKeHoachKeKhai_DanhSachKeKhaiLanDau_ChiTiet
                                                       join dskkld in db.NV_LapKeHoachKeKhai_DanhSachKeKhaiLanDau on dskkldct.MaKeHoachKeKhai_DanhSachKeKhaiLanDau_ID equals dskkld.MaKeHoachKeKhai_DanhSachKeKhaiLanDau_ID
                                                       join lkh in db.NV_LapKeHoachKeKhai on dskkld.MaKeHoachKeKhai equals lkh.MaKeHoachKeKhai
                                                       where lkh.Ma_Loai_KeKhai == 3 && lkh.ThoiGianBatDau <= DateTime.Now && lkh.ThoiGianKetThuc >= DateTime.Now && lkh.Ma_CoQuan_DonVi == MaCoQuan
                                                       select dskkldct.Ma_CanBo);

                var DanhSachCanBoKeKhaiDaCo = (from dskkldct in db.NV_LapKeHoachKeKhai_DanhSachKeKhaiLanDau_ChiTiet
                                               join dskkld in db.NV_LapKeHoachKeKhai_DanhSachKeKhaiLanDau on dskkldct.MaKeHoachKeKhai_DanhSachKeKhaiLanDau_ID equals dskkld.MaKeHoachKeKhai_DanhSachKeKhaiLanDau_ID
                                               where dskkld.MaKeHoachKeKhai == MaKeHoachKeKhai
                                               select dskkldct.Ma_CanBo);

                var DanhSachCanBoKeKhaiBoNhiemCongTacCanBoConHan = (from dskkldct in db.NV_LapKeHoachKeKhai_DanhSachKeKhaiBoNhiemCongTacCanBo_ChiTiet
                                                                    join dskkld in db.NV_LapKeHoachKeKhai_DanhSachKeKhaiBoNhiemCongTacCanBo on dskkldct.MaKeHoachKeKhai_DanhSachKeKhaiBoNhiemCongTacCanBo_ID equals dskkld.MaKeHoachKeKhai_DanhSachKeKhaiBoNhiemCongTacCanBo_ID
                                                                    join lkh in db.NV_LapKeHoachKeKhai on dskkld.MaKeHoachKeKhai equals lkh.MaKeHoachKeKhai
                                                                    where lkh.Ma_Loai_KeKhai == 3 && lkh.ThoiGianBatDau <= DateTime.Now && lkh.ThoiGianKetThuc >= DateTime.Now && lkh.Ma_CoQuan_DonVi == MaCoQuan
                                                                    select dskkldct.Ma_CanBo);



                foreach (var item in MaCanBo)
                {
                    var MaCB = Int32.Parse(item);
                    var DSCTLD = db.NV_LapKeHoachKeKhai_DanhSachKeKhaiLanDau_ChiTiet.Where(_ => _.MaKeHoachKeKhai_DanhSachKeKhaiLanDau_ID == DSLD.MaKeHoachKeKhai_DanhSachKeKhaiLanDau_ID && _.Ma_CanBo == MaCB);

                    if (DSCTLD.Count() != 0)
                    {
                        return Json(new { status = "warning", message = "Danh Sách Tồn Tại Cán Bộ Không Hợp Lệ Vui Lòng F5 để chọn lại." });
                    }
                }


                foreach (var item in MaCanBo)
                {
                    var MaCB = Int32.Parse(item);

                    if (!DanhSachCanBoDaKeKhaiLanDau.Contains(MaCB) && !DanhSachCanBoKeKhaiLanDauConHan.Contains(MaCB) && !DanhSachCanBoKeKhaiDaCo.Contains(MaCB) && !DanhSachCanBoKeKhaiBoNhiemCongTacCanBoConHan.Contains(MaCB))
                    {
                        var dsldct = new NV_LapKeHoachKeKhai_DanhSachKeKhaiLanDau_ChiTiet();
                        dsldct.MaKeHoachKeKhai_DanhSachKeKhaiLanDau_ID = DSLD.MaKeHoachKeKhai_DanhSachKeKhaiLanDau_ID;
                        dsldct.Ma_CanBo = MaCB;
                        db.NV_LapKeHoachKeKhai_DanhSachKeKhaiLanDau_ChiTiet.Add(dsldct);
                        db.SaveChanges();
                    }

                }
                return Json(new { status = "success", message = "Cán Bộ Đã Được Thêm Vào Danh Sách Kê Khai Lần Đầu" });
            }
            else if (KHKK1.Ma_Loai_KeKhai == 4)
            {

                if (MaLoaiKeKhai == 4)
                {
                    var DSHN = db.NV_LapKeHoachKeKhai_DanhSachKeKhaiHangNam.Where(_ => _.MaKeHoachKeKhai == MaKeHoachKeKhai).First();
                    var DSBS = db.NV_LapKeHoachKeKhai_DanhSachKeKhaiBoSung.Where(_ => _.MaKeHoachKeKhai == MaKeHoachKeKhai).First();

                    foreach (var item in MaCanBo)
                    {
                        var MaCB = Int32.Parse(item);
                        var DSCTHN = db.NV_LapKeHoachKeKhai_DanhSachKeKhaiHangNam_ChiTiet.Where(_ => _.MaKeHoachKeKhai_DanhSachKeKhaiHangNam_ID == DSHN.MaKeHoachKeKhai_DanhSachKeKhaiHangNam_ID && _.Ma_CanBo == MaCB);
                        var DSCTBS = db.NV_LapKeHoachKeKhai_DanhSachKeKhaiBoSung_ChiTiet.Where(_ => _.MaKeHoachKeKhai_DanhSachKeKhaiBoSung_ID == DSBS.MaKeHoachKeKhai_DanhSachKeKhaiBoSung_ID && _.Ma_CanBo == MaCB);
                        if (DSCTHN.Count() != 0 || DSCTBS.Count() != 0)
                        {
                            return Json(new { status = "warning", message = "Danh Sách Tồn Tại Cán Bộ Không Hợp Lệ Vui Lòng F5 để chọn lại." });
                        }
                    }


                    foreach (var item in MaCanBo)
                    {
                        var MaCB = Int32.Parse(item);
                        var dshnct = new NV_LapKeHoachKeKhai_DanhSachKeKhaiHangNam_ChiTiet();
                        dshnct.MaKeHoachKeKhai_DanhSachKeKhaiHangNam_ID = DSHN.MaKeHoachKeKhai_DanhSachKeKhaiHangNam_ID;
                        dshnct.Ma_CanBo = MaCB;
                        db.NV_LapKeHoachKeKhai_DanhSachKeKhaiHangNam_ChiTiet.Add(dshnct);
                        db.SaveChanges();

                    }
                    return Json(new { status = "success", message = "Cán Bộ Đã Được Thêm Vào Danh Sách Kê Khai Hằng Năm" });
                }
                else
                {
                    var DSBS = db.NV_LapKeHoachKeKhai_DanhSachKeKhaiBoSung.Where(_ => _.MaKeHoachKeKhai == MaKeHoachKeKhai).First();
                    var DSHN = db.NV_LapKeHoachKeKhai_DanhSachKeKhaiHangNam.Where(_ => _.MaKeHoachKeKhai == MaKeHoachKeKhai).First();
                    foreach (var item in MaCanBo)
                    {
                        var MaCB = Int32.Parse(item);
                        var DSCTHN = db.NV_LapKeHoachKeKhai_DanhSachKeKhaiHangNam_ChiTiet.Where(_ => _.MaKeHoachKeKhai_DanhSachKeKhaiHangNam_ID == DSHN.MaKeHoachKeKhai_DanhSachKeKhaiHangNam_ID && _.Ma_CanBo == MaCB);
                        var DSCTBS = db.NV_LapKeHoachKeKhai_DanhSachKeKhaiBoSung_ChiTiet.Where(_ => _.MaKeHoachKeKhai_DanhSachKeKhaiBoSung_ID == DSBS.MaKeHoachKeKhai_DanhSachKeKhaiBoSung_ID && _.Ma_CanBo == MaCB);
                        if (DSCTHN.Count() != 0 || DSCTBS.Count() != 0)
                        {
                            return Json(new { status = "warning", message = "Danh Sách Tồn Tại Cán Bộ Không Hợp Lệ Vui Lòng F5 để chọn lại." });
                        }
                    }


                    foreach (var item in MaCanBo)
                    {
                        var MaCB = Int32.Parse(item);

                        var dsbsct = new NV_LapKeHoachKeKhai_DanhSachKeKhaiBoSung_ChiTiet();
                        dsbsct.MaKeHoachKeKhai_DanhSachKeKhaiBoSung_ID = DSBS.MaKeHoachKeKhai_DanhSachKeKhaiBoSung_ID;
                        dsbsct.Ma_CanBo = MaCB;
                        db.NV_LapKeHoachKeKhai_DanhSachKeKhaiBoSung_ChiTiet.Add(dsbsct);
                        db.SaveChanges();

                    }
                    return Json(new { status = "success", message = "Cán Bộ Đã Được Thêm Vào Danh Sách Kê Khai Bổ Sung" });
                }

            }
            else
            {

                var DSBNCTCB = db.NV_LapKeHoachKeKhai_DanhSachKeKhaiBoNhiemCongTacCanBo.Where(_ => _.MaKeHoachKeKhai == MaKeHoachKeKhai).First();

                foreach (var item in MaCanBo)
                {
                    var MaCB = Int32.Parse(item);
                    var DSCTBNCTCB = db.NV_LapKeHoachKeKhai_DanhSachKeKhaiBoNhiemCongTacCanBo_ChiTiet.Where(_ => _.MaKeHoachKeKhai_DanhSachKeKhaiBoNhiemCongTacCanBo_ID == DSBNCTCB.MaKeHoachKeKhai_DanhSachKeKhaiBoNhiemCongTacCanBo_ID && _.Ma_CanBo == MaCB);
                    if (DSCTBNCTCB.Count() != 0)
                    {
                        return Json(new { status = "warning", message = "Danh Sách Tồn Tại Cán Bộ Không Hợp Lệ Vui Lòng F5 để chọn lại." });
                    }

                }


                foreach (var item in MaCanBo)
                {
                    var MaCB = Int32.Parse(item);

                    var dsbnctcbct = new NV_LapKeHoachKeKhai_DanhSachKeKhaiBoNhiemCongTacCanBo_ChiTiet();
                    dsbnctcbct.MaKeHoachKeKhai_DanhSachKeKhaiBoNhiemCongTacCanBo_ID = DSBNCTCB.MaKeHoachKeKhai_DanhSachKeKhaiBoNhiemCongTacCanBo_ID;
                    dsbnctcbct.Ma_CanBo = MaCB;
                    db.NV_LapKeHoachKeKhai_DanhSachKeKhaiBoNhiemCongTacCanBo_ChiTiet.Add(dsbnctcbct);
                    db.SaveChanges();

                }
                return Json(new { status = "success", message = "Cán Bộ Đã Được Thêm Vào Danh Sách Kê Khai Phục Vụ Công Tác Cán Bộ" });
            }
            return Json(new { status = "error", message = "Lỗi Hệ Thống" });
        }

        public JsonResult HoanThanhDanhSach(int MaKeHoachKeKhai)
        {
            var MaCoQuan = user.GetUserCoQuan();

            var KHKK = db.NV_LapKeHoachKeKhai.Where(_ => _.MaKeHoachKeKhai == MaKeHoachKeKhai).First();




            if (KHKK.Ma_Loai_KeKhai == 3)
            {
                var dsld = db.NV_LapKeHoachKeKhai_DanhSachKeKhaiLanDau.Where(_ => _.MaKeHoachKeKhai == MaKeHoachKeKhai && _.TrangThai == false).First();
                var TrangThaiTonTaiDS = db.NV_LapKeHoachKeKhai_DanhSachKeKhaiLanDau_ChiTiet.Where(_ => _.MaKeHoachKeKhai_DanhSachKeKhaiLanDau_ID == dsld.MaKeHoachKeKhai_DanhSachKeKhaiLanDau_ID).Count();
                if (TrangThaiTonTaiDS == 0)
                {
                    return Json(new { status = "warning", message = "Danh Sách Kê Khai Lần Đầu Chưa Được Lập, Vui Lòng Kiểm Tra Lại" });
                }
                else
                {
                    dsld.TrangThai = true;
                    db.Entry(dsld).State = EntityState.Modified;
                    db.SaveChanges();

                    KHKK.TrangThai = true;
                    db.Entry(KHKK).State = EntityState.Modified;
                    db.SaveChanges();

                    return Json(new { status = "success", message = "Danh Sách Kê Khai Lần Đầu Đã Hoàn Thành" });

                }
            }
            else if (KHKK.Ma_Loai_KeKhai == 4)
            {
                var dshn = db.NV_LapKeHoachKeKhai_DanhSachKeKhaiHangNam.Where(_ => _.MaKeHoachKeKhai == MaKeHoachKeKhai && _.TrangThai == false).First();
                var dsbs = db.NV_LapKeHoachKeKhai_DanhSachKeKhaiBoSung.Where(_ => _.MaKeHoachKeKhai == MaKeHoachKeKhai && _.TrangThai == false).First();
                var TrangThaiTonTaiDSHN = db.NV_LapKeHoachKeKhai_DanhSachKeKhaiHangNam_ChiTiet.Where(_ => _.MaKeHoachKeKhai_DanhSachKeKhaiHangNam_ID == dshn.MaKeHoachKeKhai_DanhSachKeKhaiHangNam_ID).Count();
                var TrangThaiTonTaiDSBS = db.NV_LapKeHoachKeKhai_DanhSachKeKhaiBoSung_ChiTiet.Where(_ => _.MaKeHoachKeKhai_DanhSachKeKhaiBoSung_ID == dsbs.MaKeHoachKeKhai_DanhSachKeKhaiBoSung_ID).Count();
                if (TrangThaiTonTaiDSHN == 0)
                {
                    return Json(new { status = "warning", message = "Danh Sách Kê Khai Hằng Năm Chưa Được Lập Vui Lòng Kiểm Tra Lại!" });
                }
                else
                {
                    var dscbhn = (from cb in db.DM_CanBo
                                  join cv in db.DM_ChucVu_ChucDanh on cb.Ma_ChucVu_ChucDanh equals cv.Ma_ChucVu_ChucDanh
                                  join dskkhn_ct in db.NV_LapKeHoachKeKhai_DanhSachKeKhaiHangNam_ChiTiet on cb.Ma_CanBo equals dskkhn_ct.Ma_CanBo
                                  join dskkhn in db.NV_LapKeHoachKeKhai_DanhSachKeKhaiHangNam on dskkhn_ct.MaKeHoachKeKhai_DanhSachKeKhaiHangNam_ID equals dskkhn.MaKeHoachKeKhai_DanhSachKeKhaiHangNam_ID
                                  join lkkhn in db.NV_LapKeHoachKeKhai on dskkhn.MaKeHoachKeKhai equals lkkhn.MaKeHoachKeKhai
                                  join cq in db.DM_CoQuanDonVi on lkkhn.Ma_CoQuan_DonVi equals cq.Ma_CoQuan_DonVi
                                  join tk in db.HT_TaiKhoan on cb.Ma_CanBo equals tk.Ma_CanBo
                                  join ntk in db.HT_NhomTaiKhoan on tk.MaNhomTaiKhoan equals ntk.MaNhomTaiKhoan
                                  where lkkhn.MaKeHoachKeKhai == MaKeHoachKeKhai && (ntk.MaNhomTaiKhoan == 33 || ntk.MaNhomTaiKhoan == 34)
                                  orderby ntk.Sort
                                  select new
                                  {
                                      cb.Ma_CanBo
                                  }).ToList();

                    if (dscbhn.Count() == 0)
                    {
                        return Json(new { status = "warning", message = "Danh Sách Kê Khai Hằng Năm Phải Có Ít Nhất Là Một Người Đứng Đầu Hoặc Phó. Vui Lòng Kiểm Tra Lại!" });
                    }

                    dshn.TrangThai = true;
                    db.Entry(dshn).State = EntityState.Modified;
                    db.SaveChanges();

                    dsbs.TrangThai = true;
                    db.Entry(dsbs).State = EntityState.Modified;
                    db.SaveChanges();

                    KHKK.TrangThai = true;
                    db.Entry(KHKK).State = EntityState.Modified;
                    db.SaveChanges();

                    return Json(new { status = "success", message = "Danh Sách Kê Khai Hằng Năm Đã Hoàn Thành" });

                }
            }
            else
            {
                var dsbnctcb = db.NV_LapKeHoachKeKhai_DanhSachKeKhaiBoNhiemCongTacCanBo.Where(_ => _.MaKeHoachKeKhai == MaKeHoachKeKhai && _.TrangThai == false).First();
                var TrangThaiTonTaiDS = db.NV_LapKeHoachKeKhai_DanhSachKeKhaiBoNhiemCongTacCanBo_ChiTiet.Where(_ => _.MaKeHoachKeKhai_DanhSachKeKhaiBoNhiemCongTacCanBo_ID == dsbnctcb.MaKeHoachKeKhai_DanhSachKeKhaiBoNhiemCongTacCanBo_ID).Count();
                if (TrangThaiTonTaiDS == 0)
                {
                    return Json(new { status = "warning", message = "Danh Sách Kê Khai Phục Vụ Công Tác Cán Bộ Chưa Được Lập, Vui Lòng Kiểm Tra Lại" });
                }
                else
                {
                    KHKK.TrangThai = true;
                    db.Entry(KHKK).State = EntityState.Modified;
                    db.SaveChanges();

                    dsbnctcb.TrangThai = true;
                    db.Entry(dsbnctcb).State = EntityState.Modified;
                    db.SaveChanges();
                    return Json(new { status = "success", message = "Danh Sách Kê Khai Phục Vụ Công Tác Cán Bộ Đã Hoàn Thành" });

                }
            }



        }

        public JsonResult GetCoQuanDonVi()
        {
            var maCoQuan = 0;
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

                        maCoQuan = Int32.Parse(subs[3]);
                    }
                }

                var data = db.DM_CoQuanDonVi.Find(maCoQuan);
                return Json(data, JsonRequestBehavior.AllowGet);

            }
            catch
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }

        }

        public JsonResult LoadDanhSachCanBoHN(int id)
        {
            var MaCanBo = 0;
            var Role = "null";
            var maCoQuan = 0;

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
                        Role = subs[2];
                        maCoQuan = Int32.Parse(subs[3]);
                    }
                }

            }
            catch
            {

            }


            var data = (from cb in db.DM_CanBo
                        join cv in db.DM_ChucVu_ChucDanh on cb.Ma_ChucVu_ChucDanh equals cv.Ma_ChucVu_ChucDanh
                        join cq in db.DM_CoQuanDonVi on cb.Ma_CoQuan_DonVi equals cq.Ma_CoQuan_DonVi
                        join dskkhn_ct in db.NV_LapKeHoachKeKhai_DanhSachKeKhaiHangNam_ChiTiet on cb.Ma_CanBo equals dskkhn_ct.Ma_CanBo
                        join dskkhn in db.NV_LapKeHoachKeKhai_DanhSachKeKhaiHangNam on dskkhn_ct.MaKeHoachKeKhai_DanhSachKeKhaiHangNam_ID equals dskkhn.MaKeHoachKeKhai_DanhSachKeKhaiHangNam_ID
                        join lkkhn in db.NV_LapKeHoachKeKhai on dskkhn.MaKeHoachKeKhai equals lkkhn.MaKeHoachKeKhai
                        where lkkhn.MaKeHoachKeKhai == id && cq.Ma_CoQuan_DonVi == maCoQuan
                        select new { cb.Ma_CanBo, cb.DoB, cb.HoTen, cb.SoCCCD, cv.Ten_ChucVu_ChucDanh, cq.Ten, lkkhn.KeHoachNam, lkkhn.NghiDinh, lkkhn.TenKeHoachKeKhai, lkkhn.MaKeHoachKeKhai, lkkhn.TrangThai }).ToList();


            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult LoadDanhSachCanBoBS(int id)
        {
            var MaCanBo = 0;
            var Role = "null";
            var maCoQuan = 0;

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
                        Role = subs[2];
                        maCoQuan = Int32.Parse(subs[3]);
                    }
                }

            }
            catch
            {

            }

            var data = (from cb in db.DM_CanBo
                        join cv in db.DM_ChucVu_ChucDanh on cb.Ma_ChucVu_ChucDanh equals cv.Ma_ChucVu_ChucDanh
                        join cq in db.DM_CoQuanDonVi on cb.Ma_CoQuan_DonVi equals cq.Ma_CoQuan_DonVi
                        join dskkbs_ct in db.NV_LapKeHoachKeKhai_DanhSachKeKhaiBoSung_ChiTiet on cb.Ma_CanBo equals dskkbs_ct.Ma_CanBo
                        join dskkbs in db.NV_LapKeHoachKeKhai_DanhSachKeKhaiBoSung on dskkbs_ct.MaKeHoachKeKhai_DanhSachKeKhaiBoSung_ID equals dskkbs.MaKeHoachKeKhai_DanhSachKeKhaiBoSung_ID
                        join lkkbs in db.NV_LapKeHoachKeKhai on dskkbs.MaKeHoachKeKhai equals lkkbs.MaKeHoachKeKhai
                        where lkkbs.MaKeHoachKeKhai == id && cq.Ma_CoQuan_DonVi == maCoQuan
                        select new { cb.Ma_CanBo, cb.HoTen, cb.SoCCCD, cv.Ten_ChucVu_ChucDanh, cq.Ten, lkkbs.KeHoachNam, lkkbs.NghiDinh, lkkbs.TenKeHoachKeKhai, lkkbs.MaKeHoachKeKhai, lkkbs.TrangThai }).ToList();


            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult BanInDanhSachCanBo(int id)
        {
            var MaCanBo = user.GetUser();
            var maCoQuan = user.GetUserCoQuan();

            var ten = "";

            var CoQuan = db.DM_CoQuanDonVi.Find(maCoQuan);
            var LoaiCoQuan = db.DM_Loai_CoQuan_DonVi.Where(_ => _.Ma_Loai_CQDV == CoQuan.MaLoai_CoQuan_DonVi).Select(_ => _.Ma_Loai_CQDV).FirstOrDefault();

            var namKeHoach = db.NV_LapKeHoachKeKhai.Find(id);
            var now = DateTime.Now;

            var KHKK = db.NV_LapKeHoachKeKhai.Where(_ => _.MaKeHoachKeKhai == id).First();

            var CoQuanTrucThuoc = "";
            var TenCoQuan = CoQuan.Ten;
            if (LoaiCoQuan == 10)
            {
                var CQTT = "";
                if (TenCoQuan.ToUpper().Contains("CAM RANH") || TenCoQuan.ToUpper().Contains("NHA TRANG"))
                {
                    CQTT = "THÀNH PHỐ";
                }
                else if (TenCoQuan.ToUpper().Contains("NINH HÒA"))
                {
                    CQTT = "THỊ XÃ";
                }
                else
                {
                    CQTT = "HUYỆN";
                }

                CoQuanTrucThuoc = $@"<p><b>ỦY BAN NHÂN DÂN {CQTT} {TenCoQuan.ToUpper()}</b><br /><span style='font-weight:bold'>---------------</span></p>";
            }
            else
            {
                CoQuanTrucThuoc = $@"<p>UBND TỈNH KHÁNH HÒA</p><p><b>{TenCoQuan.ToUpper()}</b><br /><span style='font-weight:bold'>---------------</span></p>";
            }

            if (KHKK.Ma_Loai_KeKhai == 3)
            {
                var DSCBLanDau = (from khkk in db.NV_LapKeHoachKeKhai
                                  join dskknh in db.NV_LapKeHoachKeKhai_DanhSachKeKhaiLanDau on khkk.MaKeHoachKeKhai equals dskknh.MaKeHoachKeKhai
                                  join ctdskkhn in db.NV_LapKeHoachKeKhai_DanhSachKeKhaiLanDau_ChiTiet on dskknh.MaKeHoachKeKhai_DanhSachKeKhaiLanDau_ID equals ctdskkhn.MaKeHoachKeKhai_DanhSachKeKhaiLanDau_ID
                                  join cb in db.DM_CanBo on ctdskkhn.Ma_CanBo equals cb.Ma_CanBo
                                  join cv in db.DM_ChucVu_ChucDanh on cb.Ma_ChucVu_ChucDanh equals cv.Ma_ChucVu_ChucDanh
                                  join cq in db.DM_CoQuanDonVi on cb.Ma_CoQuan_DonVi equals cq.Ma_CoQuan_DonVi
                                  join tk in db.HT_TaiKhoan on cb.Ma_CanBo equals tk.Ma_CanBo
                                  join ntk in db.HT_NhomTaiKhoan on tk.MaNhomTaiKhoan equals ntk.MaNhomTaiKhoan
                                  where khkk.MaKeHoachKeKhai == id
                                  orderby ntk.Sort
                                  select new { cb.HoTen, cb.DoB, cv.Ten_ChucVu_ChucDanh, cq.Ten }).ToList();

                var StringDSCB = "";
                var dem = 0;
                foreach (var i in DSCBLanDau)
                {
                    dem++;
                    StringDSCB += @"<tr style='height:15pt'>
                                    <td
                                        style='width:12.34%; border-width:0.5pt; border-style:solid;  padding-right:5.03pt; padding-left:5.03pt; vertical-align:middle; background-color:#ffffff; -aw-border-bottom:0.5pt single; -aw-border-right:0.5pt single'>
                                        <p style='text-align:center; line-height:150%; font-size:10pt'><span>" + dem + @"</span></p>
                                    </td>
                                    <td
                                        style='width:20.68%; border-width:0.5pt;  border-style:solid; padding-right:5.03pt; padding-left:5.4pt; vertical-align:middle; background-color:#ffffff; -aw-border-bottom:0.5pt single; -aw-border-right:0.5pt single'>
                                        <p style='line-height:150%; font-size:10pt'><span>" + i.HoTen + @"</span></p>
                                    </td>
                                    <td
                                        style='width:13.94%; border-width:0.5pt;   border-style:solid;  padding-right:5.03pt; padding-left:5.4pt; vertical-align:middle; background-color:#ffffff; -aw-border-bottom:0.5pt single; -aw-border-right:0.5pt single'>
                                        <p style='text-align:center; line-height:150%; font-size:10pt'><span>" + i.DoB.Split(' ')[0] + @"</span></p>
                                    </td>
                                    <td
                                        style='width:19.64%;  border-width:0.5pt;  border-style:solid; padding-right:5.03pt; padding-left:5.4pt; vertical-align:middle; background-color:#ffffff; -aw-border-bottom:0.5pt single; -aw-border-right:0.5pt single'>
                                        <p style='text-align:center; line-height:150%; font-size:10pt'><span>" + i.Ten_ChucVu_ChucDanh + @"</span></p>
                                    </td>
                                    <td
                                        style='width:20.64%; border-width:0.5pt;  border-style:solid; padding-right:5.03pt; padding-left:5.4pt; vertical-align:middle; background-color:#ffffff; -aw-border-bottom:0.5pt single; -aw-border-right:0.5pt single'>
                                        <p style='text-align:center; line-height:150%; font-size:10pt'><span>" + i.Ten + @"</span></p>
                                    </td>
                                    <td
                                        style='width:12.76%; border-style:solid; border-width:0.5pt;  padding-right:5.03pt;  padding-left:5.4pt; vertical-align:middle; background-color:#ffffff; -aw-border-bottom:0.5pt single'>
                                        <p style='line-height:150%; font-size:10pt'><span>&#xa0;</span></p>
                                    </td>
                                </tr>";
                }
                var html = @"<html>
                            <head>
                                <meta http-equiv='Content-Type' content='text/html; charset=utf-8' />
                                <meta http-equiv='Content-Style-Type' content='text/css' />
                                <meta name='generator' content='Aspose.Words for .NET 22.5.0' />
                                <title></title>
                                <style type='text/css'>
                                    html {
                                        font-family: 'Times New Roman';
                                        font-size: 12pt
                                    }

                                    p {
                                        margin: 0pt
                                    }

                                    li,
                                    table {
                                        margin-top: 0pt;
                                        margin-bottom: 0pt
                                    }

                                    .Footer {
                                        font-size: 12pt
                                    }

                                    .Header {
                                        font-size: 12pt
                                    }

                                    span.FooterChar {
                                        font-size: 12pt
                                    }

                                    span.HeaderChar {
                                        font-size: 12pt
                                    }

                                    .TableGrid {}
                                </style>
                            </head>

                            <body>
                                <div>
                                    <table cellspacing='0' cellpadding='0' style='width:100%; border-collapse:collapse'>
                                        <tr>
                                            <td style='width:37.8%; padding-right:5.4pt; padding-left:5.4pt; vertical-align:top'>
                                                <p style='margin-top:6pt; text-align:center'><span> <center>" + CoQuanTrucThuoc + @"</center></span></p>
                                            </td>
                                            <td style='width:62.2%; padding-right:5.4pt; padding-left:5.4pt; vertical-align:top'>
                                                <p style='margin-top:6pt; text-align:center'><span style='font-weight:bold'>CỘNG HÒA XÃ HỘI CHỦ
                                                        NGHĨA VIỆT NAM</span><br /><span style='font-weight:bold'>Độc lập - Tự do - Hạnh phúc
                                                    </span><br /><span style='font-weight:bold'>---------------</span></p>
                                            </td>
                                        </tr>
                                    </table>
                                    <p style='margin-top:6pt; margin-bottom:14pt; text-align:center'><span
                                            style='font-weight:bold; -aw-import:ignore'>&#xa0;</span></p>
                                    <p style='text-align:center; line-height:150%'><span style='font-weight:bold'>TỔNG HỢP DANH SÁCH NGƯỜI CÓ NGHĨA
                                            VỤ KÊ KHAI TÀI SẢN, THU NHẬP LẦN ĐẦU</span></p>
                                    <p style='text-align:center; line-height:150%'><span style='font-weight:bold'>NĂM " + namKeHoach.KeHoachNam + @"</span></p>
                                    <p style='margin-top:6pt; margin-bottom:14pt; text-align:center'><span style='font-weight:bold'>(Do Thanh tra tỉnh kiểm soát tài sản, thu nhập)</span></p>
                                    <p style='margin-top:6pt; margin-bottom:14pt; text-align:center'><span style='font-style:italic'>(Kèm theo Công
                                            văn số: …./UBND-NC ngày …/…/" + namKeHoach.KeHoachNam + @" của UBND tỉnh)</span></p>
                                    <p style='margin-top:6pt; margin-bottom:14pt; text-align:center'><span
                                            style='font-style:italic; -aw-import:ignore'>&#xa0;</span></p>
                                    <table cellspacing='0' cellpadding='0'
                                        style='width:100%; border:0.75pt solid #000000; -aw-border:0.5pt single; border-collapse:collapse'>
                                        <tr style='height:25.5pt'>
                                            <td
                                                style='width:12.34%; border-style:solid; border-width:0.5pt;  padding-right:5.03pt; padding-left:5.03pt; vertical-align:middle; background-color:#ffffff; -aw-border-right:0.5pt single'>
                                                <p style='text-align:center; line-height:150%; font-size:10pt'><span
                                                        style='font-weight:bold'>STT</span></p>
                                            </td>
                                            <td
                                                style='width:20.68%; border-style:solid;  border-width:0.5pt;  padding-right:5.03pt; padding-left:5.4pt; vertical-align:middle; background-color:#ffffff; -aw-border-right:0.5pt single'>
                                                <p style='text-align:center; line-height:150%; font-size:10pt'><span style='font-weight:bold'>Họ và
                                                        tên</span></p>
                                            </td>
                                            <td
                                                style='width:13.94%; border-style:solid;  border-width:0.5pt;padding-right:5.03pt; padding-left:5.4pt; vertical-align:middle; background-color:#ffffff; -aw-border-bottom:0.5pt single; -aw-border-right:0.5pt single'>
                                                <p style='text-align:center; line-height:150%; font-size:10pt'><span style='font-weight:bold'>Ngày
                                                        tháng năm sinh</span></p>
                                            </td>
                                            <td
                                                style='width:19.64%; border-style:solid;  border-width:0.5pt;  padding-right:5.03pt; padding-left:5.4pt; vertical-align:middle; background-color:#ffffff; -aw-border-bottom:0.5pt single; -aw-border-right:0.5pt single'>
                                                <p style='text-align:center; line-height:150%; font-size:10pt'><span style='font-weight:bold'>Chức
                                                        vụ/ chức danh công tác</span></p>
                                            </td>
                                            <td
                                                style='width:20.64%; border-style:solid;  border-width:0.5pt;   padding-right:5.03pt; padding-left:5.4pt; vertical-align:middle; background-color:#ffffff; -aw-border-bottom:0.5pt single; -aw-border-right:0.5pt single'>
                                                <p style='text-align:center; line-height:150%; font-size:10pt'><span style='font-weight:bold'>Cơ
                                                        quan/đơn vị công tác</span></p>
                                            </td>
                                            <td
                                                style='width:12.76%; border-style:solid; border-width:0.5pt;  padding-left:5.4pt; vertical-align:middle; background-color:#ffffff'>
                                                <p style='text-align:center; line-height:150%; font-size:10pt'><span style='font-weight:bold'>Ghi
                                                        chú</span></p>
                                            </td>
                                        </tr>
                                        
                                        
                                       " + StringDSCB + @"
                                    </table>
                                    <p style='margin-top:6pt; margin-bottom:14pt; font-size:11pt'><span>Lưu ý: Đề nghị các cơ quan, đơn vị cho số
                                            điện thoại bộ phận quản lý, thực hiện việc kiểm soát TSTN của cơ quan, đơn vị mình để Thanh tra tỉnh
                                            liên hệ khi cần thiết.</span></p>
                                    <p style='margin-top:6pt; margin-bottom:14pt'><span style='-aw-import:ignore'>&#xa0;</span></p>
                                    <table cellspacing='0' cellpadding='0' style='width:100%; border-collapse:collapse'>
                                        <tr>
                                            <td style='width:50%; padding-right:5.4pt; padding-left:5.4pt; vertical-align:top'>
                                                <p style='text-align:center'><span style='font-style:italic; -aw-import:ignore'>&#xa0;</span></p>
                                            </td>
                                            <td style='width:50%; padding-right:5.4pt; padding-left:5.4pt; vertical-align:top'>
                                                <p style='text-align:center'><span style='font-style:italic'>Ngày </span><span
                                                        style='font-style:italic; -aw-import:spaces'>" + now.Day + @"</span><span
                                                        style='font-style:italic'>, tháng </span><span
                                                        style='font-style:italic; -aw-import:spaces'>" + now.Month + @"</span><span
                                                        style='font-style:italic'>, năm " + now.Year + @"</span></p>
                                                <p style='text-align:center; font-size:11pt'><span style='font-weight:bold'>Thủ trưởng đơn vị</span>
                                                </p>
                                                <p style='text-align:center'><span style='font-style:italic; -aw-import:ignore'>&#xa0;</span></p>
                                            </td>
                                        </tr>
                                    </table>
                                    <p style='margin-top:6pt; margin-bottom:14pt'><span style='font-style:italic; -aw-import:ignore'>&#xa0;</span>
                                    </p>
                                    <div style='-aw-headerfooter-type:footer-primary; clear:both'>
                                        <p class='Footer' style='text-align:center'><span style='-aw-field-start:true'></span><span
                                                style='-aw-field-code:' PAGE \\* MERGEFORMAT ''></span><span
                                                style='-aw-field-separator:true'></span><span>1</span><span style='-aw-field-end:true'></span></p>
                                        <p class='Footer'><span style='-aw-import:ignore'>&#xa0;</span></p>
                                    </div>
                                </div>
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
            else if (KHKK.Ma_Loai_KeKhai == 4)
            {
                var DSCBHangNam = (from khkk in db.NV_LapKeHoachKeKhai
                                   join dskknh in db.NV_LapKeHoachKeKhai_DanhSachKeKhaiHangNam on khkk.MaKeHoachKeKhai equals dskknh.MaKeHoachKeKhai
                                   join ctdskkhn in db.NV_LapKeHoachKeKhai_DanhSachKeKhaiHangNam_ChiTiet on dskknh.MaKeHoachKeKhai_DanhSachKeKhaiHangNam_ID equals ctdskkhn.MaKeHoachKeKhai_DanhSachKeKhaiHangNam_ID
                                   join cb in db.DM_CanBo on ctdskkhn.Ma_CanBo equals cb.Ma_CanBo
                                   join cv in db.DM_ChucVu_ChucDanh on cb.Ma_ChucVu_ChucDanh equals cv.Ma_ChucVu_ChucDanh
                                   join cq in db.DM_CoQuanDonVi on cb.Ma_CoQuan_DonVi equals cq.Ma_CoQuan_DonVi
                                   join tk in db.HT_TaiKhoan on cb.Ma_CanBo equals tk.Ma_CanBo
                                   join ntk in db.HT_NhomTaiKhoan on tk.MaNhomTaiKhoan equals ntk.MaNhomTaiKhoan
                                   where khkk.MaKeHoachKeKhai == id
                                   orderby ntk.Sort
                                   select new { cb.HoTen, cb.DoB, cv.Ten_ChucVu_ChucDanh, cq.Ten }).ToList();

                var StringDSCB = "";
                var dem = 0;
                foreach (var i in DSCBHangNam)
                {
                    dem++;
                    StringDSCB += @"<tr style='height:15pt'>
                                    <td
                                        style='width:12.34%; border-width:0.5pt; border-style:solid;  padding-right:5.03pt; padding-left:5.03pt; vertical-align:middle; background-color:#ffffff; -aw-border-bottom:0.5pt single; -aw-border-right:0.5pt single'>
                                        <p style='text-align:center; line-height:150%; font-size:10pt'><span>" + dem + @"</span></p>
                                    </td>
                                    <td
                                        style='width:20.68%; border-width:0.5pt;  border-style:solid; padding-right:5.03pt; padding-left:5.4pt; vertical-align:middle; background-color:#ffffff; -aw-border-bottom:0.5pt single; -aw-border-right:0.5pt single'>
                                        <p style='line-height:150%; font-size:10pt'><span>" + i.HoTen + @"</span></p>
                                    </td>
                                    <td
                                        style='width:13.94%; border-width:0.5pt;   border-style:solid;  padding-right:5.03pt; padding-left:5.4pt; vertical-align:middle; background-color:#ffffff; -aw-border-bottom:0.5pt single; -aw-border-right:0.5pt single'>
                                        <p style='text-align:center; line-height:150%; font-size:10pt'><span>" + i.DoB.Split(' ')[0] + @"</span></p>
                                    </td>
                                    <td
                                        style='width:19.64%;  border-width:0.5pt;  border-style:solid; padding-right:5.03pt; padding-left:5.4pt; vertical-align:middle; background-color:#ffffff; -aw-border-bottom:0.5pt single; -aw-border-right:0.5pt single'>
                                        <p style='text-align:center; line-height:150%; font-size:10pt'><span>" + i.Ten_ChucVu_ChucDanh + @"</span></p>
                                    </td>
                                    <td
                                        style='width:20.64%; border-width:0.5pt;  border-style:solid; padding-right:5.03pt; padding-left:5.4pt; vertical-align:middle; background-color:#ffffff; -aw-border-bottom:0.5pt single; -aw-border-right:0.5pt single'>
                                        <p style='text-align:center; line-height:150%; font-size:10pt'><span>" + i.Ten + @"</span></p>
                                    </td>
                                    <td
                                        style='width:12.76%; border-style:solid; border-width:0.5pt;  padding-right:5.03pt;  padding-left:5.4pt; vertical-align:middle; background-color:#ffffff; -aw-border-bottom:0.5pt single'>
                                        <p style='line-height:150%; font-size:10pt'><span>&#xa0;</span></p>
                                    </td>
                                </tr>";
                }
                var html = @"<html>
                            <head>
                                <meta http-equiv='Content-Type' content='text/html; charset=utf-8' />
                                <meta http-equiv='Content-Style-Type' content='text/css' />
                                <meta name='generator' content='Aspose.Words for .NET 22.5.0' />
                                <title></title>
                                <style type='text/css'>
                                    html {
                                        font-family: 'Times New Roman';
                                        font-size: 12pt
                                    }

                                    p {
                                        margin: 0pt
                                    }

                                    li,
                                    table {
                                        margin-top: 0pt;
                                        margin-bottom: 0pt
                                    }

                                    .Footer {
                                        font-size: 12pt
                                    }

                                    .Header {
                                        font-size: 12pt
                                    }

                                    span.FooterChar {
                                        font-size: 12pt
                                    }

                                    span.HeaderChar {
                                        font-size: 12pt
                                    }

                                    .TableGrid {}
                                </style>
                            </head>

                            <body>
                                <div>
                                    <table cellspacing='0' cellpadding='0' style='width:100%; border-collapse:collapse'>
                                        <tr>
                                            <td style='width:37.8%; padding-right:5.4pt; padding-left:5.4pt; vertical-align:top'>
                                                <p style='margin-top:6pt; text-align:center'><span><center>" + CoQuanTrucThuoc + @"</center></span></p>
                                            </td>
                                            <td style='width:62.2%; padding-right:5.4pt; padding-left:5.4pt; vertical-align:top'>
                                                <p style='margin-top:6pt; text-align:center'><span style='font-weight:bold'>CỘNG HÒA XÃ HỘI CHỦ
                                                        NGHĨA VIỆT NAM</span><br /><span style='font-weight:bold'>Độc lập - Tự do - Hạnh phúc
                                                    </span><br /><span style='font-weight:bold'>---------------</span></p>
                                            </td>
                                        </tr>
                                    </table>
                                    <p style='margin-top:6pt; margin-bottom:14pt; text-align:center'><span
                                            style='font-weight:bold; -aw-import:ignore'>&#xa0;</span></p>
                                    <p style='text-align:center; line-height:150%'><span style='font-weight:bold'>TỔNG HỢP DANH SÁCH NGƯỜI CÓ NGHĨA
                                            VỤ KÊ KHAI TÀI SẢN, THU NHẬP HẰNG NĂM</span></p>
                                    <p style='text-align:center; line-height:150%'><span style='font-weight:bold'>NĂM " + namKeHoach.KeHoachNam + @"</span></p>
                                    <p style='margin-top:6pt; margin-bottom:14pt; text-align:center'><span style='font-weight:bold'>(Do Thanh tra tỉnh kiểm soát tài sản, thu nhập)</span></p>
                                    <p style='margin-top:6pt; margin-bottom:14pt; text-align:center'><span style='font-style:italic'>(Kèm theo Công
                                            văn số: …./UBND-NC ngày …/…/" + namKeHoach.KeHoachNam + @" của UBND tỉnh)</span></p>
                                    <p style='margin-top:6pt; margin-bottom:14pt; text-align:center'><span
                                            style='font-style:italic; -aw-import:ignore'>&#xa0;</span></p>
                                    <table cellspacing='0' cellpadding='0'
                                        style='width:100%; border:0.75pt solid #000000; -aw-border:0.5pt single; border-collapse:collapse'>
                                        <tr style='height:25.5pt'>
                                            <td
                                                style='width:12.34%; border-style:solid; border-width:0.5pt;  padding-right:5.03pt; padding-left:5.03pt; vertical-align:middle; background-color:#ffffff; -aw-border-right:0.5pt single'>
                                                <p style='text-align:center; line-height:150%; font-size:10pt'><span
                                                        style='font-weight:bold'>STT</span></p>
                                            </td>
                                            <td
                                                style='width:20.68%; border-style:solid;  border-width:0.5pt;  padding-right:5.03pt; padding-left:5.4pt; vertical-align:middle; background-color:#ffffff; -aw-border-right:0.5pt single'>
                                                <p style='text-align:center; line-height:150%; font-size:10pt'><span style='font-weight:bold'>Họ và
                                                        tên</span></p>
                                            </td>
                                            <td
                                                style='width:13.94%; border-style:solid;  border-width:0.5pt;padding-right:5.03pt; padding-left:5.4pt; vertical-align:middle; background-color:#ffffff; -aw-border-bottom:0.5pt single; -aw-border-right:0.5pt single'>
                                                <p style='text-align:center; line-height:150%; font-size:10pt'><span style='font-weight:bold'>Ngày
                                                        tháng năm sinh</span></p>
                                            </td>
                                            <td
                                                style='width:19.64%; border-style:solid;  border-width:0.5pt;  padding-right:5.03pt; padding-left:5.4pt; vertical-align:middle; background-color:#ffffff; -aw-border-bottom:0.5pt single; -aw-border-right:0.5pt single'>
                                                <p style='text-align:center; line-height:150%; font-size:10pt'><span style='font-weight:bold'>Chức
                                                        vụ/ chức danh công tác</span></p>
                                            </td>
                                            <td
                                                style='width:20.64%; border-style:solid;  border-width:0.5pt;   padding-right:5.03pt; padding-left:5.4pt; vertical-align:middle; background-color:#ffffff; -aw-border-bottom:0.5pt single; -aw-border-right:0.5pt single'>
                                                <p style='text-align:center; line-height:150%; font-size:10pt'><span style='font-weight:bold'>Cơ
                                                        quan/đơn vị công tác</span></p>
                                            </td>
                                            <td
                                                style='width:12.76%; border-style:solid; border-width:0.5pt;  padding-left:5.4pt; vertical-align:middle; background-color:#ffffff'>
                                                <p style='text-align:center; line-height:150%; font-size:10pt'><span style='font-weight:bold'>Ghi
                                                        chú</span></p>
                                            </td>
                                        </tr>
                                        
                                        
                                       " + StringDSCB + @"
                                    </table>
                                    <p style='margin-top:6pt; margin-bottom:14pt; font-size:11pt'><span>Lưu ý: Đề nghị các cơ quan, đơn vị cho số
                                            điện thoại bộ phận quản lý, thực hiện việc kiểm soát TSTN của cơ quan, đơn vị mình để Thanh tra tỉnh
                                            liên hệ khi cần thiết.</span></p>
                                    <p style='margin-top:6pt; margin-bottom:14pt'><span style='-aw-import:ignore'>&#xa0;</span></p>
                                    <table cellspacing='0' cellpadding='0' style='width:100%; border-collapse:collapse'>
                                        <tr>
                                            <td style='width:50%; padding-right:5.4pt; padding-left:5.4pt; vertical-align:top'>
                                                <p style='text-align:center'><span style='font-style:italic; -aw-import:ignore'>&#xa0;</span></p>
                                            </td>
                                            <td style='width:50%; padding-right:5.4pt; padding-left:5.4pt; vertical-align:top'>
                                                <p style='text-align:center'><span style='font-style:italic'>Ngày </span><span
                                                        style='font-style:italic; -aw-import:spaces'>" + now.Day + @"</span><span
                                                        style='font-style:italic'>, tháng </span><span
                                                        style='font-style:italic; -aw-import:spaces'>" + now.Month + @"</span><span
                                                        style='font-style:italic'>, năm " + now.Year + @"</span></p>
                                                <p style='text-align:center; font-size:11pt'><span style='font-weight:bold'>Thủ trưởng đơn vị</span>
                                                </p>
                                                <p style='text-align:center'><span style='font-style:italic; -aw-import:ignore'>&#xa0;</span></p>
                                            </td>
                                        </tr>
                                    </table>
                                    <p style='margin-top:6pt; margin-bottom:14pt'><span style='font-style:italic; -aw-import:ignore'>&#xa0;</span>
                                    </p>
                                    <div style='-aw-headerfooter-type:footer-primary; clear:both'>
                                        <p class='Footer' style='text-align:center'><span style='-aw-field-start:true'></span><span
                                                style='-aw-field-code:' PAGE \\* MERGEFORMAT ''></span><span
                                                style='-aw-field-separator:true'></span><span>1</span><span style='-aw-field-end:true'></span></p>
                                        <p class='Footer'><span style='-aw-import:ignore'>&#xa0;</span></p>
                                    </div>
                                </div>
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
            else if (KHKK.Ma_Loai_KeKhai == 5)
            {
                var DSCBBoSung = (from khkk in db.NV_LapKeHoachKeKhai
                                  join dskknh in db.NV_LapKeHoachKeKhai_DanhSachKeKhaiBoSung on khkk.MaKeHoachKeKhai equals dskknh.MaKeHoachKeKhai
                                  join ctdskkhn in db.NV_LapKeHoachKeKhai_DanhSachKeKhaiBoSung_ChiTiet on dskknh.MaKeHoachKeKhai_DanhSachKeKhaiBoSung_ID equals ctdskkhn.MaKeHoachKeKhai_DanhSachKeKhaiBoSung_ID
                                  join cb in db.DM_CanBo on ctdskkhn.Ma_CanBo equals cb.Ma_CanBo
                                  join cv in db.DM_ChucVu_ChucDanh on cb.Ma_ChucVu_ChucDanh equals cv.Ma_ChucVu_ChucDanh
                                  join cq in db.DM_CoQuanDonVi on cb.Ma_CoQuan_DonVi equals cq.Ma_CoQuan_DonVi
                                  join tk in db.HT_TaiKhoan on cb.Ma_CanBo equals tk.Ma_CanBo
                                  join ntk in db.HT_NhomTaiKhoan on tk.MaNhomTaiKhoan equals ntk.MaNhomTaiKhoan
                                  where khkk.MaKeHoachKeKhai == id
                                  orderby ntk.Sort
                                  select new { cb.HoTen, cb.DoB, cv.Ten_ChucVu_ChucDanh, cq.Ten }).ToList();

                var StringDSCB = "";
                var dem = 0;
                foreach (var i in DSCBBoSung)
                {
                    dem++;
                    StringDSCB += @"<tr style='height:15pt'>
                                    <td
                                        style='width:12.34%; border-width:0.5pt; border-style:solid;  padding-right:5.03pt; padding-left:5.03pt; vertical-align:middle; background-color:#ffffff; -aw-border-bottom:0.5pt single; -aw-border-right:0.5pt single'>
                                        <p style='text-align:center; line-height:150%; font-size:10pt'><span>" + dem + @"</span></p>
                                    </td>
                                    <td
                                        style='width:20.68%; border-width:0.5pt;  border-style:solid; padding-right:5.03pt; padding-left:5.4pt; vertical-align:middle; background-color:#ffffff; -aw-border-bottom:0.5pt single; -aw-border-right:0.5pt single'>
                                        <p style='line-height:150%; font-size:10pt'><span>" + i.HoTen + @"</span></p>
                                    </td>
                                    <td
                                        style='width:13.94%; border-width:0.5pt;   border-style:solid;  padding-right:5.03pt; padding-left:5.4pt; vertical-align:middle; background-color:#ffffff; -aw-border-bottom:0.5pt single; -aw-border-right:0.5pt single'>
                                        <p style='text-align:center; line-height:150%; font-size:10pt'><span>" + i.DoB.Split(' ')[0] + @"</span></p>
                                    </td>
                                    <td
                                        style='width:19.64%;  border-width:0.5pt;  border-style:solid; padding-right:5.03pt; padding-left:5.4pt; vertical-align:middle; background-color:#ffffff; -aw-border-bottom:0.5pt single; -aw-border-right:0.5pt single'>
                                        <p style='text-align:center; line-height:150%; font-size:10pt'><span>" + i.Ten_ChucVu_ChucDanh + @"</span></p>
                                    </td>
                                    <td
                                        style='width:20.64%; border-width:0.5pt;  border-style:solid; padding-right:5.03pt; padding-left:5.4pt; vertical-align:middle; background-color:#ffffff; -aw-border-bottom:0.5pt single; -aw-border-right:0.5pt single'>
                                        <p style='text-align:center; line-height:150%; font-size:10pt'><span>" + i.Ten + @"</span></p>
                                    </td>
                                    <td
                                        style='width:12.76%; border-style:solid; border-width:0.5pt;  padding-right:5.03pt;  padding-left:5.4pt; vertical-align:middle; background-color:#ffffff; -aw-border-bottom:0.5pt single'>
                                        <p style='line-height:150%; font-size:10pt'><span>&#xa0;</span></p>
                                    </td>
                                </tr>";
                }
                var html = @"<html>
                            <head>
                                <meta http-equiv='Content-Type' content='text/html; charset=utf-8' />
                                <meta http-equiv='Content-Style-Type' content='text/css' />
                                <meta name='generator' content='Aspose.Words for .NET 22.5.0' />
                                <title></title>
                                <style type='text/css'>
                                    html {
                                        font-family: 'Times New Roman';
                                        font-size: 12pt
                                    }

                                    p {
                                        margin: 0pt
                                    }

                                    li,
                                    table {
                                        margin-top: 0pt;
                                        margin-bottom: 0pt
                                    }

                                    .Footer {
                                        font-size: 12pt
                                    }

                                    .Header {
                                        font-size: 12pt
                                    }

                                    span.FooterChar {
                                        font-size: 12pt
                                    }

                                    span.HeaderChar {
                                        font-size: 12pt
                                    }

                                    .TableGrid {}
                                </style>
                            </head>

                            <body>
                                <div>
                                    <table cellspacing='0' cellpadding='0' style='width:100%; border-collapse:collapse'>
                                        <tr>
                                            <td style='width:37.8%; padding-right:5.4pt; padding-left:5.4pt; vertical-align:top'>
                                                <p style='margin-top:6pt; text-align:center'><span><center>" + CoQuanTrucThuoc + @"</center></span></p>
                                            </td>
                                            <td style='width:62.2%; padding-right:5.4pt; padding-left:5.4pt; vertical-align:top'>
                                                <p style='margin-top:6pt; text-align:center'><span style='font-weight:bold'>CỘNG HÒA XÃ HỘI CHỦ
                                                        NGHĨA VIỆT NAM</span><br /><span style='font-weight:bold'>Độc lập - Tự do - Hạnh phúc
                                                    </span><br /><span style='font-weight:bold'>---------------</span></p>
                                            </td>
                                        </tr>
                                    </table>>
                                    <p style='margin-top:6pt; margin-bottom:14pt; text-align:center'><span
                                            style='font-weight:bold; -aw-import:ignore'>&#xa0;</span></p>
                                    <p style='text-align:center; line-height:150%'><span style='font-weight:bold'>TỔNG HỢP DANH SÁCH NGƯỜI CÓ NGHĨA
                                            VỤ KÊ KHAI TÀI SẢN, THU NHẬP BỔ SUNG</span></p>
                                    <p style='text-align:center; line-height:150%'><span style='font-weight:bold'>NĂM " + namKeHoach.KeHoachNam + @"</span></p>
                                    <p style='margin-top:6pt; margin-bottom:14pt; text-align:center'><span style='font-weight:bold'>(Do  Thanh tra tỉnh kiểm soát tài sản, thu nhập)</span></p>
                                    <p style='margin-top:6pt; margin-bottom:14pt; text-align:center'><span style='font-style:italic'>(Kèm theo Công
                                            văn số: …./UBND-NC ngày …/…/" + namKeHoach.KeHoachNam + @" của UBND tỉnh)</span></p>
                                    <p style='margin-top:6pt; margin-bottom:14pt; text-align:center'><span
                                            style='font-style:italic; -aw-import:ignore'>&#xa0;</span></p>
                                    <table cellspacing='0' cellpadding='0'
                                        style='width:100%; border:0.75pt solid #000000; -aw-border:0.5pt single; border-collapse:collapse'>
                                        <tr style='height:25.5pt'>
                                            <td
                                                style='width:12.34%; border-style:solid; border-width:0.5pt;  padding-right:5.03pt; padding-left:5.03pt; vertical-align:middle; background-color:#ffffff; -aw-border-right:0.5pt single'>
                                                <p style='text-align:center; line-height:150%; font-size:10pt'><span
                                                        style='font-weight:bold'>STT</span></p>
                                            </td>
                                            <td
                                                style='width:20.68%; border-style:solid;  border-width:0.5pt;  padding-right:5.03pt; padding-left:5.4pt; vertical-align:middle; background-color:#ffffff; -aw-border-right:0.5pt single'>
                                                <p style='text-align:center; line-height:150%; font-size:10pt'><span style='font-weight:bold'>Họ và
                                                        tên</span></p>
                                            </td>
                                            <td
                                                style='width:13.94%; border-style:solid;  border-width:0.5pt;padding-right:5.03pt; padding-left:5.4pt; vertical-align:middle; background-color:#ffffff; -aw-border-bottom:0.5pt single; -aw-border-right:0.5pt single'>
                                                <p style='text-align:center; line-height:150%; font-size:10pt'><span style='font-weight:bold'>Ngày
                                                        tháng năm sinh</span></p>
                                            </td>
                                            <td
                                                style='width:19.64%; border-style:solid;  border-width:0.5pt;  padding-right:5.03pt; padding-left:5.4pt; vertical-align:middle; background-color:#ffffff; -aw-border-bottom:0.5pt single; -aw-border-right:0.5pt single'>
                                                <p style='text-align:center; line-height:150%; font-size:10pt'><span style='font-weight:bold'>Chức
                                                        vụ/ chức danh công tác</span></p>
                                            </td>
                                            <td
                                                style='width:20.64%; border-style:solid;  border-width:0.5pt;   padding-right:5.03pt; padding-left:5.4pt; vertical-align:middle; background-color:#ffffff; -aw-border-bottom:0.5pt single; -aw-border-right:0.5pt single'>
                                                <p style='text-align:center; line-height:150%; font-size:10pt'><span style='font-weight:bold'>Cơ
                                                        quan/đơn vị công tác</span></p>
                                            </td>
                                            <td
                                                style='width:12.76%; border-style:solid; border-width:0.5pt;  padding-left:5.4pt; vertical-align:middle; background-color:#ffffff'>
                                                <p style='text-align:center; line-height:150%; font-size:10pt'><span style='font-weight:bold'>Ghi
                                                        chú</span></p>
                                            </td>
                                        </tr>
                                        
                                        
                                       " + StringDSCB + @"
                                    </table>
                                    <p style='margin-top:6pt; margin-bottom:14pt; font-size:11pt'><span>Lưu ý: Đề nghị các cơ quan, đơn vị cho số
                                            điện thoại bộ phận quản lý, thực hiện việc kiểm soát TSTN của cơ quan, đơn vị mình để Thanh tra tỉnh
                                            liên hệ khi cần thiết.</span></p>
                                    <p style='margin-top:6pt; margin-bottom:14pt'><span style='-aw-import:ignore'>&#xa0;</span></p>
                                    <table cellspacing='0' cellpadding='0' style='width:100%; border-collapse:collapse'>
                                        <tr>
                                            <td style='width:50%; padding-right:5.4pt; padding-left:5.4pt; vertical-align:top'>
                                                <p style='text-align:center'><span style='font-style:italic; -aw-import:ignore'>&#xa0;</span></p>
                                            </td>
                                            <td style='width:50%; padding-right:5.4pt; padding-left:5.4pt; vertical-align:top'>
                                                <p style='text-align:center'><span style='font-style:italic'>Ngày </span><span
                                                        style='font-style:italic; -aw-import:spaces'>" + now.Day + @"</span><span
                                                        style='font-style:italic'>, tháng </span><span
                                                        style='font-style:italic; -aw-import:spaces'>" + now.Month + @"</span><span
                                                        style='font-style:italic'>, năm " + now.Year + @"</span></p>
                                                <p style='text-align:center; font-size:11pt'><span style='font-weight:bold'>Thủ trưởng đơn vị</span>
                                                </p>
                                                <p style='text-align:center'><span style='font-style:italic; -aw-import:ignore'>&#xa0;</span></p>
                                            </td>
                                        </tr>
                                    </table>
                                    <p style='margin-top:6pt; margin-bottom:14pt'><span style='font-style:italic; -aw-import:ignore'>&#xa0;</span>
                                    </p>
                                    <div style='-aw-headerfooter-type:footer-primary; clear:both'>
                                        <p class='Footer' style='text-align:center'><span style='-aw-field-start:true'></span><span
                                                style='-aw-field-code:' PAGE \\* MERGEFORMAT ''></span><span
                                                style='-aw-field-separator:true'></span><span>1</span><span style='-aw-field-end:true'></span></p>
                                        <p class='Footer'><span style='-aw-import:ignore'>&#xa0;</span></p>
                                    </div>
                                </div>
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
            else
            {
                var DSCBBoSung = (from khkk in db.NV_LapKeHoachKeKhai
                                  join dskknh in db.NV_LapKeHoachKeKhai_DanhSachKeKhaiBoNhiemCongTacCanBo on khkk.MaKeHoachKeKhai equals dskknh.MaKeHoachKeKhai
                                  join ctdskkhn in db.NV_LapKeHoachKeKhai_DanhSachKeKhaiBoNhiemCongTacCanBo_ChiTiet on dskknh.MaKeHoachKeKhai_DanhSachKeKhaiBoNhiemCongTacCanBo_ID equals ctdskkhn.MaKeHoachKeKhai_DanhSachKeKhaiBoNhiemCongTacCanBo_ID
                                  join cb in db.DM_CanBo on ctdskkhn.Ma_CanBo equals cb.Ma_CanBo
                                  join cv in db.DM_ChucVu_ChucDanh on cb.Ma_ChucVu_ChucDanh equals cv.Ma_ChucVu_ChucDanh
                                  join cq in db.DM_CoQuanDonVi on cb.Ma_CoQuan_DonVi equals cq.Ma_CoQuan_DonVi
                                  join tk in db.HT_TaiKhoan on cb.Ma_CanBo equals tk.Ma_CanBo
                                  join ntk in db.HT_NhomTaiKhoan on tk.MaNhomTaiKhoan equals ntk.MaNhomTaiKhoan
                                  where khkk.MaKeHoachKeKhai == id
                                  orderby ntk.Sort
                                  select new { cb.HoTen, cb.DoB, cv.Ten_ChucVu_ChucDanh, cq.Ten }).ToList();

                var StringDSCB = "";
                var dem = 0;
                foreach (var i in DSCBBoSung)
                {
                    dem++;
                    StringDSCB += @"<tr style='height:15pt'>
                                    <td
                                        style='width:12.34%; border-width:0.5pt; border-style:solid;  padding-right:5.03pt; padding-left:5.03pt; vertical-align:middle; background-color:#ffffff; -aw-border-bottom:0.5pt single; -aw-border-right:0.5pt single'>
                                        <p style='text-align:center; line-height:150%; font-size:10pt'><span>" + dem + @"</span></p>
                                    </td>
                                    <td
                                        style='width:20.68%; border-width:0.5pt;  border-style:solid; padding-right:5.03pt; padding-left:5.4pt; vertical-align:middle; background-color:#ffffff; -aw-border-bottom:0.5pt single; -aw-border-right:0.5pt single'>
                                        <p style='line-height:150%; font-size:10pt'><span>" + i.HoTen + @"</span></p>
                                    </td>
                                    <td
                                        style='width:13.94%; border-width:0.5pt;   border-style:solid;  padding-right:5.03pt; padding-left:5.4pt; vertical-align:middle; background-color:#ffffff; -aw-border-bottom:0.5pt single; -aw-border-right:0.5pt single'>
                                        <p style='text-align:center; line-height:150%; font-size:10pt'><span>" + i.DoB.Split(' ')[0] + @"</span></p>
                                    </td>
                                    <td
                                        style='width:19.64%;  border-width:0.5pt;  border-style:solid; padding-right:5.03pt; padding-left:5.4pt; vertical-align:middle; background-color:#ffffff; -aw-border-bottom:0.5pt single; -aw-border-right:0.5pt single'>
                                        <p style='text-align:center; line-height:150%; font-size:10pt'><span>" + i.Ten_ChucVu_ChucDanh + @"</span></p>
                                    </td>
                                    <td
                                        style='width:20.64%; border-width:0.5pt;  border-style:solid; padding-right:5.03pt; padding-left:5.4pt; vertical-align:middle; background-color:#ffffff; -aw-border-bottom:0.5pt single; -aw-border-right:0.5pt single'>
                                        <p style='text-align:center; line-height:150%; font-size:10pt'><span>" + i.Ten + @"</span></p>
                                    </td>
                                    <td
                                        style='width:12.76%; border-style:solid; border-width:0.5pt;  padding-right:5.03pt;  padding-left:5.4pt; vertical-align:middle; background-color:#ffffff; -aw-border-bottom:0.5pt single'>
                                        <p style='line-height:150%; font-size:10pt'><span>&#xa0;</span></p>
                                    </td>
                                </tr>";
                }
                var html = @"<html>
                            <head>
                                <meta http-equiv='Content-Type' content='text/html; charset=utf-8' />
                                <meta http-equiv='Content-Style-Type' content='text/css' />
                                <meta name='generator' content='Aspose.Words for .NET 22.5.0' />
                                <title></title>
                                <style type='text/css'>
                                    html {
                                        font-family: 'Times New Roman';
                                        font-size: 12pt
                                    }

                                    p {
                                        margin: 0pt
                                    }

                                    li,
                                    table {
                                        margin-top: 0pt;
                                        margin-bottom: 0pt
                                    }

                                    .Footer {
                                        font-size: 12pt
                                    }

                                    .Header {
                                        font-size: 12pt
                                    }

                                    span.FooterChar {
                                        font-size: 12pt
                                    }

                                    span.HeaderChar {
                                        font-size: 12pt
                                    }

                                    .TableGrid {}
                                </style>
                            </head>

                            <body>
                                <div>
                                    <table cellspacing='0' cellpadding='0' style='width:100%; border-collapse:collapse'>
                                        <tr>
                                            <td style='width:37.8%; padding-right:5.4pt; padding-left:5.4pt; vertical-align:top'>
                                                <p style='margin-top:6pt; text-align:center'><span><center>" + CoQuanTrucThuoc + @"</center></span></p>
                                            </td>
                                            <td style='width:62.2%; padding-right:5.4pt; padding-left:5.4pt; vertical-align:top'>
                                                <p style='margin-top:6pt; text-align:center'><span style='font-weight:bold'>CỘNG HÒA XÃ HỘI CHỦ
                                                        NGHĨA VIỆT NAM</span><br /><span style='font-weight:bold'>Độc lập - Tự do - Hạnh phúc
                                                    </span><br /><span style='font-weight:bold'>---------------</span></p>
                                            </td>
                                        </tr>
                                    </table>
                                    <p style='margin-top:6pt; margin-bottom:14pt; text-align:center'><span
                                            style='font-weight:bold; -aw-import:ignore'>&#xa0;</span></p>
                                    <p style='text-align:center; line-height:150%'><span style='font-weight:bold'>TỔNG HỢP DANH SÁCH NGƯỜI CÓ NGHĨA
                                            VỤ KÊ KHAI TÀI SẢN, THU NHẬP PHỤC VỤ CÔNG TÁC CÁN BỘ</span></p>
                                    <p style='text-align:center; line-height:150%'><span style='font-weight:bold'>NĂM " + namKeHoach.KeHoachNam + @"</span></p>
                                    <p style='margin-top:6pt; margin-bottom:14pt; text-align:center'><span style='font-weight:bold'>(Do Thanh tra tỉnh kiểm soát tài sản, thu nhập)</span></p>
                                    <p style='margin-top:6pt; margin-bottom:14pt; text-align:center'><span style='font-style:italic'>(Kèm theo Công
                                            văn số: …./UBND-NC ngày …/…/" + namKeHoach.KeHoachNam + @" của UBND tỉnh)</span></p>
                                    <p style='margin-top:6pt; margin-bottom:14pt; text-align:center'><span
                                            style='font-style:italic; -aw-import:ignore'>&#xa0;</span></p>
                                    <table cellspacing='0' cellpadding='0'
                                        style='width:100%; border:0.75pt solid #000000; -aw-border:0.5pt single; border-collapse:collapse'>
                                        <tr style='height:25.5pt'>
                                            <td
                                                style='width:12.34%; border-style:solid; border-width:0.5pt;  padding-right:5.03pt; padding-left:5.03pt; vertical-align:middle; background-color:#ffffff; -aw-border-right:0.5pt single'>
                                                <p style='text-align:center; line-height:150%; font-size:10pt'><span
                                                        style='font-weight:bold'>STT</span></p>
                                            </td>
                                            <td
                                                style='width:20.68%; border-style:solid;  border-width:0.5pt;  padding-right:5.03pt; padding-left:5.4pt; vertical-align:middle; background-color:#ffffff; -aw-border-right:0.5pt single'>
                                                <p style='text-align:center; line-height:150%; font-size:10pt'><span style='font-weight:bold'>Họ và
                                                        tên</span></p>
                                            </td>
                                            <td
                                                style='width:13.94%; border-style:solid;  border-width:0.5pt;padding-right:5.03pt; padding-left:5.4pt; vertical-align:middle; background-color:#ffffff; -aw-border-bottom:0.5pt single; -aw-border-right:0.5pt single'>
                                                <p style='text-align:center; line-height:150%; font-size:10pt'><span style='font-weight:bold'>Ngày
                                                        tháng năm sinh</span></p>
                                            </td>
                                            <td
                                                style='width:19.64%; border-style:solid;  border-width:0.5pt;  padding-right:5.03pt; padding-left:5.4pt; vertical-align:middle; background-color:#ffffff; -aw-border-bottom:0.5pt single; -aw-border-right:0.5pt single'>
                                                <p style='text-align:center; line-height:150%; font-size:10pt'><span style='font-weight:bold'>Chức
                                                        vụ/ chức danh công tác</span></p>
                                            </td>
                                            <td
                                                style='width:20.64%; border-style:solid;  border-width:0.5pt;   padding-right:5.03pt; padding-left:5.4pt; vertical-align:middle; background-color:#ffffff; -aw-border-bottom:0.5pt single; -aw-border-right:0.5pt single'>
                                                <p style='text-align:center; line-height:150%; font-size:10pt'><span style='font-weight:bold'>Cơ
                                                        quan/đơn vị công tác</span></p>
                                            </td>
                                            <td
                                                style='width:12.76%; border-style:solid; border-width:0.5pt;  padding-left:5.4pt; vertical-align:middle; background-color:#ffffff'>
                                                <p style='text-align:center; line-height:150%; font-size:10pt'><span style='font-weight:bold'>Ghi
                                                        chú</span></p>
                                            </td>
                                        </tr>
                                        
                                        
                                       " + StringDSCB + @"
                                    </table>
                                    <p style='margin-top:6pt; margin-bottom:14pt; font-size:11pt'><span>Lưu ý: Đề nghị các cơ quan, đơn vị cho số
                                            điện thoại bộ phận quản lý, thực hiện việc kiểm soát TSTN của cơ quan, đơn vị mình để Thanh tra tỉnh
                                            liên hệ khi cần thiết.</span></p>
                                    <p style='margin-top:6pt; margin-bottom:14pt'><span style='-aw-import:ignore'>&#xa0;</span></p>
                                    <table cellspacing='0' cellpadding='0' style='width:100%; border-collapse:collapse'>
                                        <tr>
                                            <td style='width:50%; padding-right:5.4pt; padding-left:5.4pt; vertical-align:top'>
                                                <p style='text-align:center'><span style='font-style:italic; -aw-import:ignore'>&#xa0;</span></p>
                                            </td>
                                            <td style='width:50%; padding-right:5.4pt; padding-left:5.4pt; vertical-align:top'>
                                                <p style='text-align:center'><span style='font-style:italic'>Ngày </span><span
                                                        style='font-style:italic; -aw-import:spaces'>" + now.Day + @"</span><span
                                                        style='font-style:italic'>, tháng </span><span
                                                        style='font-style:italic; -aw-import:spaces'>" + now.Month + @"</span><span
                                                        style='font-style:italic'>, năm " + now.Year + @"</span></p>
                                                <p style='text-align:center; font-size:11pt'><span style='font-weight:bold'>Thủ trưởng đơn vị</span>
                                                </p>
                                                <p style='text-align:center'><span style='font-style:italic; -aw-import:ignore'>&#xa0;</span></p>
                                            </td>
                                        </tr>
                                    </table>
                                    <p style='margin-top:6pt; margin-bottom:14pt'><span style='font-style:italic; -aw-import:ignore'>&#xa0;</span>
                                    </p>
                                    <div style='-aw-headerfooter-type:footer-primary; clear:both'>
                                        <p class='Footer' style='text-align:center'><span style='-aw-field-start:true'></span><span
                                                style='-aw-field-code:' PAGE \\* MERGEFORMAT ''></span><span
                                                style='-aw-field-separator:true'></span><span>1</span><span style='-aw-field-end:true'></span></p>
                                        <p class='Footer'><span style='-aw-import:ignore'>&#xa0;</span></p>
                                    </div>
                                </div>
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


        public JsonResult BanInDanhSachCanBo1(int id)
        {
            var MaCanBo = user.GetUser();
            var maCoQuan = user.GetUserCoQuan();

            var ten = "";

            var CoQuan = db.DM_CoQuanDonVi.Find(maCoQuan);
            var LoaiCoQuan = db.DM_Loai_CoQuan_DonVi.Where(_ => _.Ma_Loai_CQDV == CoQuan.MaLoai_CoQuan_DonVi).Select(_ => _.Ma_Loai_CQDV).FirstOrDefault();

            var namKeHoach = db.NV_LapKeHoachKeKhai.Find(id);
            var now = DateTime.Now;

            var KHKK = db.NV_LapKeHoachKeKhai.Where(_ => _.MaKeHoachKeKhai == id).First();

            var DSCBBoSung = (from khkk in db.NV_LapKeHoachKeKhai
                              join dskknh in db.NV_LapKeHoachKeKhai_DanhSachKeKhaiBoSung on khkk.MaKeHoachKeKhai equals dskknh.MaKeHoachKeKhai
                              join ctdskkhn in db.NV_LapKeHoachKeKhai_DanhSachKeKhaiBoSung_ChiTiet on dskknh.MaKeHoachKeKhai_DanhSachKeKhaiBoSung_ID equals ctdskkhn.MaKeHoachKeKhai_DanhSachKeKhaiBoSung_ID
                              join cb in db.DM_CanBo on ctdskkhn.Ma_CanBo equals cb.Ma_CanBo
                              join cv in db.DM_ChucVu_ChucDanh on cb.Ma_ChucVu_ChucDanh equals cv.Ma_ChucVu_ChucDanh
                              join cq in db.DM_CoQuanDonVi on cb.Ma_CoQuan_DonVi equals cq.Ma_CoQuan_DonVi
                              join tk in db.HT_TaiKhoan on cb.Ma_CanBo equals tk.Ma_CanBo
                              join ntk in db.HT_NhomTaiKhoan on tk.MaNhomTaiKhoan equals ntk.MaNhomTaiKhoan
                              where khkk.MaKeHoachKeKhai == id
                              orderby ntk.Sort
                              select new { cb.HoTen, cb.DoB, cv.Ten_ChucVu_ChucDanh, cq.Ten }).ToList();

            var StringDSCB = "";
            var dem = 0;
            var CoQuanTrucThuoc = "";
            var TenCoQuan = CoQuan.Ten;
            if (LoaiCoQuan == 10)
            {
                var CQTT = "";
                if (TenCoQuan.ToUpper().Contains("CAM RANH") || TenCoQuan.ToUpper().Contains("NHA TRANG"))
                {
                    CQTT = "THÀNH PHỐ";
                }
                else if (TenCoQuan.ToUpper().Contains("NINH HÒA"))
                {
                    CQTT = "THỊ XÃ";
                }
                else
                {
                    CQTT = "HUYỆN";
                }

                CoQuanTrucThuoc = $@"<p><b>ỦY BAN NHÂN DÂN {CQTT} {TenCoQuan.ToUpper()}</b><br /><span style='font-weight:bold'>---------------</span></p>";
            }
            else
            {
                CoQuanTrucThuoc = $@"<p>UBND TỈNH KHÁNH HÒA</p><p><b>{TenCoQuan.ToUpper()}</b><br /><span style='font-weight:bold'>---------------</span></p>";
            }

            foreach (var i in DSCBBoSung)
            {
                dem++;
                StringDSCB += @"<tr style='height:15pt'>
                                    <td
                                        style='width:12.34%; border-width:0.5pt; border-style:solid;  padding-right:5.03pt; padding-left:5.03pt; vertical-align:middle; background-color:#ffffff; -aw-border-bottom:0.5pt single; -aw-border-right:0.5pt single'>
                                        <p style='text-align:center; line-height:150%; font-size:10pt'><span>" + dem + @"</span></p>
                                    </td>
                                    <td
                                        style='width:20.68%; border-width:0.5pt;  border-style:solid; padding-right:5.03pt; padding-left:5.4pt; vertical-align:middle; background-color:#ffffff; -aw-border-bottom:0.5pt single; -aw-border-right:0.5pt single'>
                                        <p style='line-height:150%; font-size:10pt'><span>" + i.HoTen + @"</span></p>
                                    </td>
                                    <td
                                        style='width:13.94%; border-width:0.5pt;   border-style:solid;  padding-right:5.03pt; padding-left:5.4pt; vertical-align:middle; background-color:#ffffff; -aw-border-bottom:0.5pt single; -aw-border-right:0.5pt single'>
                                        <p style='text-align:center; line-height:150%; font-size:10pt'><span>" + i.DoB.Split(' ')[0] + @"</span></p>
                                    </td>
                                    <td
                                        style='width:19.64%;  border-width:0.5pt;  border-style:solid; padding-right:5.03pt; padding-left:5.4pt; vertical-align:middle; background-color:#ffffff; -aw-border-bottom:0.5pt single; -aw-border-right:0.5pt single'>
                                        <p style='text-align:center; line-height:150%; font-size:10pt'><span>" + i.Ten_ChucVu_ChucDanh + @"</span></p>
                                    </td>
                                    <td
                                        style='width:20.64%; border-width:0.5pt;  border-style:solid; padding-right:5.03pt; padding-left:5.4pt; vertical-align:middle; background-color:#ffffff; -aw-border-bottom:0.5pt single; -aw-border-right:0.5pt single'>
                                        <p style='text-align:center; line-height:150%; font-size:10pt'><span>" + i.Ten + @"</span></p>
                                    </td>
                                    <td
                                        style='width:12.76%; border-style:solid; border-width:0.5pt;  padding-right:5.03pt;  padding-left:5.4pt; vertical-align:middle; background-color:#ffffff; -aw-border-bottom:0.5pt single'>
                                        <p style='line-height:150%; font-size:10pt'><span>&#xa0;</span></p>
                                    </td>
                                </tr>";
            }
            var html = @"<html>
                            <head>
                                <meta http-equiv='Content-Type' content='text/html; charset=utf-8' />
                                <meta http-equiv='Content-Style-Type' content='text/css' />
                                <meta name='generator' content='Aspose.Words for .NET 22.5.0' />
                                <title></title>
                                <style type='text/css'>
                                    html {
                                        font-family: 'Times New Roman';
                                        font-size: 12pt
                                    }

                                    p {
                                        margin: 0pt
                                    }

                                    li,
                                    table {
                                        margin-top: 0pt;
                                        margin-bottom: 0pt
                                    }

                                    .Footer {
                                        font-size: 12pt
                                    }

                                    .Header {
                                        font-size: 12pt
                                    }

                                    span.FooterChar {
                                        font-size: 12pt
                                    }

                                    span.HeaderChar {
                                        font-size: 12pt
                                    }

                                    .TableGrid {}
                                </style>
                            </head>

                            <body>
                                <div>
                                    <table cellspacing='0' cellpadding='0' style='width:100%; border-collapse:collapse'>
                                        <tr>
                                            <td style='width:37.8%; padding-right:5.4pt; padding-left:5.4pt; vertical-align:top'>
                                                <p style='margin-top:6pt; text-align:center'><span><center>" + CoQuanTrucThuoc + @"</center></span></p>
                                            </td>
                                            <td style='width:62.2%; padding-right:5.4pt; padding-left:5.4pt; vertical-align:top'>
                                                <p style='margin-top:6pt; text-align:center'><span style='font-weight:bold'>CỘNG HÒA XÃ HỘI CHỦ
                                                        NGHĨA VIỆT NAM</span><br /><span style='font-weight:bold'>Độc lập - Tự do - Hạnh phúc
                                                    </span><br /><span style='font-weight:bold'>---------------</span></p>
                                            </td>
                                        </tr>
                                    </table>
                                    <p style='margin-top:6pt; margin-bottom:14pt; text-align:center'><span
                                            style='font-weight:bold; -aw-import:ignore'>&#xa0;</span></p>
                                    <p style='text-align:center; line-height:150%'><span style='font-weight:bold'>TỔNG HỢP DANH SÁCH NGƯỜI CÓ NGHĨA
                                            VỤ KÊ KHAI TÀI SẢN, THU NHẬP BỔ SUNG</span></p>
                                    <p style='text-align:center; line-height:150%'><span style='font-weight:bold'>NĂM " + namKeHoach.KeHoachNam + @"</span></p>
                                    <p style='margin-top:6pt; margin-bottom:14pt; text-align:center'><span style='font-weight:bold'>(Do Thanh tra tỉnh kiểm soát tài sản, thu nhập)</span></p>
                                    <p style='margin-top:6pt; margin-bottom:14pt; text-align:center'><span style='font-style:italic'>(Kèm theo Công
                                            văn số: …./UBND-NC ngày …/…/" + namKeHoach.KeHoachNam + @" của UBND tỉnh)</span></p>
                                    <p style='margin-top:6pt; margin-bottom:14pt; text-align:center'><span
                                            style='font-style:italic; -aw-import:ignore'>&#xa0;</span></p>
                                    <table cellspacing='0' cellpadding='0'
                                        style='width:100%; border:0.75pt solid #000000; -aw-border:0.5pt single; border-collapse:collapse'>
                                        <tr style='height:25.5pt'>
                                            <td
                                                style='width:12.34%; border-style:solid; border-width:0.5pt;  padding-right:5.03pt; padding-left:5.03pt; vertical-align:middle; background-color:#ffffff; -aw-border-right:0.5pt single'>
                                                <p style='text-align:center; line-height:150%; font-size:10pt'><span
                                                        style='font-weight:bold'>STT</span></p>
                                            </td>
                                            <td
                                                style='width:20.68%; border-style:solid;  border-width:0.5pt;  padding-right:5.03pt; padding-left:5.4pt; vertical-align:middle; background-color:#ffffff; -aw-border-right:0.5pt single'>
                                                <p style='text-align:center; line-height:150%; font-size:10pt'><span style='font-weight:bold'>Họ và
                                                        tên</span></p>
                                            </td>
                                            <td
                                                style='width:13.94%; border-style:solid;  border-width:0.5pt;padding-right:5.03pt; padding-left:5.4pt; vertical-align:middle; background-color:#ffffff; -aw-border-bottom:0.5pt single; -aw-border-right:0.5pt single'>
                                                <p style='text-align:center; line-height:150%; font-size:10pt'><span style='font-weight:bold'>Ngày
                                                        tháng năm sinh</span></p>
                                            </td>
                                            <td
                                                style='width:19.64%; border-style:solid;  border-width:0.5pt;  padding-right:5.03pt; padding-left:5.4pt; vertical-align:middle; background-color:#ffffff; -aw-border-bottom:0.5pt single; -aw-border-right:0.5pt single'>
                                                <p style='text-align:center; line-height:150%; font-size:10pt'><span style='font-weight:bold'>Chức
                                                        vụ/ chức danh công tác</span></p>
                                            </td>
                                            <td
                                                style='width:20.64%; border-style:solid;  border-width:0.5pt;   padding-right:5.03pt; padding-left:5.4pt; vertical-align:middle; background-color:#ffffff; -aw-border-bottom:0.5pt single; -aw-border-right:0.5pt single'>
                                                <p style='text-align:center; line-height:150%; font-size:10pt'><span style='font-weight:bold'>Cơ
                                                        quan/đơn vị công tác</span></p>
                                            </td>
                                            <td
                                                style='width:12.76%; border-style:solid; border-width:0.5pt;  padding-left:5.4pt; vertical-align:middle; background-color:#ffffff'>
                                                <p style='text-align:center; line-height:150%; font-size:10pt'><span style='font-weight:bold'>Ghi
                                                        chú</span></p>
                                            </td>
                                        </tr>
                                       " + StringDSCB + @"
                                    </table>
                                    <p style='margin-top:6pt; margin-bottom:14pt; font-size:11pt'><span>Lưu ý: Đề nghị các cơ quan, đơn vị cho số
                                            điện thoại bộ phận quản lý, thực hiện việc kiểm soát TSTN của cơ quan, đơn vị mình để Thanh tra tỉnh
                                            liên hệ khi cần thiết.</span></p>
                                    <p style='margin-top:6pt; margin-bottom:14pt'><span style='-aw-import:ignore'>&#xa0;</span></p>
                                    <table cellspacing='0' cellpadding='0' style='width:100%; border-collapse:collapse'>
                                        <tr>
                                            <td style='width:50%; padding-right:5.4pt; padding-left:5.4pt; vertical-align:top'>
                                                <p style='text-align:center'><span style='font-style:italic; -aw-import:ignore'>&#xa0;</span></p>
                                            </td>
                                            <td style='width:50%; padding-right:5.4pt; padding-left:5.4pt; vertical-align:top'>
                                                <p style='text-align:center'><span style='font-style:italic'>Ngày </span><span
                                                        style='font-style:italic; -aw-import:spaces'>" + now.Day + @"</span><span
                                                        style='font-style:italic'>, tháng </span><span
                                                        style='font-style:italic; -aw-import:spaces'>" + now.Month + @"</span><span
                                                        style='font-style:italic'>, năm " + now.Year + @"</span></p>
                                                <p style='text-align:center; font-size:11pt'><span style='font-weight:bold'>Thủ trưởng đơn vị</span>
                                                </p>
                                                <p style='text-align:center'><span style='font-style:italic; -aw-import:ignore'>&#xa0;</span></p>
                                            </td>
                                        </tr>
                                    </table>
                                    <p style='margin-top:6pt; margin-bottom:14pt'><span style='font-style:italic; -aw-import:ignore'>&#xa0;</span>
                                    </p>
                                    <div style='-aw-headerfooter-type:footer-primary; clear:both'>
                                        <p class='Footer' style='text-align:center'><span style='-aw-field-start:true'></span><span
                                                style='-aw-field-code:' PAGE \\* MERGEFORMAT ''></span><span
                                                style='-aw-field-separator:true'></span><span>1</span><span style='-aw-field-end:true'></span></p>
                                        <p class='Footer'><span style='-aw-import:ignore'>&#xa0;</span></p>
                                    </div>
                                </div>
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




}