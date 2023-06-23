using DocumentFormat.OpenXml.Drawing.Charts;
using KeKhaiTaiSanThuNhap.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static KeKhaiTaiSanThuNhap.Controllers.NV_LapKeHoachXacMinh_DanhSachCanBoXacMinhController;

namespace KeKhaiTaiSanThuNhap.Controllers
{
    [Authorize(Roles = "ADMIN")]
    public class BC_KeHoachThanhTraController : Controller
    {
        private KSTNEntities db = new KSTNEntities();
        private UserInfo user = new UserInfo();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult DanhSachKeHoachKeKhaiCoQuan(int? id)
        {
            if(id == null)
            {
                return new HttpStatusCodeResult(404, "Not found");
            }
            var data = db.DM_CoQuanDonVi.Where(_ => _.Ma_CoQuan_DonVi == id);
            if(data.Count() == 0)
            {
                return new HttpStatusCodeResult(404, "Not found");

            }

            ViewBag.id = id;
            ViewBag.TenCoQuan = data.FirstOrDefault().Ten;
            return View();
        }

        public ActionResult DanhSachKeHoachKeKhaiCoQuan_ChiTiet(int Ma_CoQuan_DonVi, int KeHoachNam, int Ma_Loai_KeKhai,  int MaKeHoach)
        {
            ViewBag.id = MaKeHoach;

            var MaCanBo = user.GetUser();
            var Role = user.GetRole();
            var maCoQuan = user.GetUserCoQuan();

            var KHKK = db.NV_LapKeHoachKeKhai.Where(_ => _.MaKeHoachKeKhai == MaKeHoach && _.Ma_CoQuan_DonVi == Ma_CoQuan_DonVi && _.KeHoachNam == KeHoachNam ).FirstOrDefault();
         
            if (KHKK == null)
            {
                return new HttpStatusCodeResult(404, "Not found");
            }
            else
            {
                ViewBag.TenCoQuan = db.DM_CoQuanDonVi.Where(_ => _.Ma_CoQuan_DonVi == KHKK.Ma_CoQuan_DonVi).FirstOrDefault().Ten;
                ViewBag.MaCoQuan = KHKK.Ma_CoQuan_DonVi;
                if (KHKK.Ma_Loai_KeKhai == 3)
                {
                    ViewBag.TenLoaiKeHoach = "DANH SÁCH CÁN BỘ KÊ KHAI LẦN ĐẦU";
                    var DSLD = db.NV_LapKeHoachKeKhai_DanhSachKeKhaiLanDau.Where(_ => _.MaKeHoachKeKhai == MaKeHoach).First();
                    ViewBag.TrangThai = DSLD.TrangThai;
                    ViewBag.TrangThaiKKHN = false;


                }
                else if (KHKK.Ma_Loai_KeKhai == 4)
                {
                    ViewBag.TenLoaiKeHoach = "DANH SÁCH CÁN BỘ KÊ KHAI HẰNG NĂM";
                    var DSHN = db.NV_LapKeHoachKeKhai_DanhSachKeKhaiHangNam.Where(_ => _.MaKeHoachKeKhai == MaKeHoach).First();
                    ViewBag.TrangThai = DSHN.TrangThai;
                    ViewBag.TrangThaiKKHN = true;
                }
                else
                {
                    ViewBag.TenLoaiKeHoach = "DANH SÁCH CÁN BỘ KÊ KHAI PHỤC VỤ CÔNG TÁC CÁN BỘ";
                    var DSBNCTCB = db.NV_LapKeHoachKeKhai_DanhSachKeKhaiBoNhiemCongTacCanBo.Where(_ => _.MaKeHoachKeKhai == MaKeHoach).First();
                    ViewBag.TrangThai = DSBNCTCB.TrangThai;
                    ViewBag.TrangThaiKKHN = false;
                }
            }
            return View();
        }

        public JsonResult GetLoaiKeKhai()
        {
            var data = db.DM_Loai_KeKhai.ToList();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public JsonResult LoadDataCoQuan()
        {
            var draw = Request.Form.GetValues("draw").FirstOrDefault();
            var start = Request.Form.GetValues("start").FirstOrDefault();
            var length = Request.Form.GetValues("length").FirstOrDefault();
            var NamKeKhai = Request.Form.GetValues("columns[0][search][value]").FirstOrDefault();
            var Ten = Request.Form.GetValues("columns[1][search][value]").FirstOrDefault();
            var LoaiKeKhai = Request.Form.GetValues("columns[2][search][value]").FirstOrDefault();
            var TrangThaiKeKhai = Request.Form.GetValues("columns[3][search][value]").FirstOrDefault();

            int pageSize = length != null ? Convert.ToInt32(length) : 0;
            int skip = start != null ? Convert.ToInt32(start) : 0;
            int recordsTotal = 0;

            //var data = (from cq in db.DM_CoQuanDonVi
            //            join khkk in db.NV_LapKeHoachKeKhai on cq.Ma_CoQuan_DonVi equals khkk.Ma_CoQuan_DonVi
            //            join lcq in db.DM_Loai_CoQuan_DonVi on cq.MaLoai_CoQuan_DonVi equals lcq.Ma_Loai_CQDV
            //            where cq.MaLoai_CoQuan_DonVi != 35
            //            orderby lcq.Ten_Loai_CQDV ascending, cq.Ten ascending
            //            select new { cq.Ma_CoQuan_DonVi, cq.Ten, cq.MaLoai_CoQuan_DonVi, lcq.Ten_Loai_CQDV, khkk.MaKeHoachKeKhai, khkk.TenKeHoachKeKhai, khkk.ThoiGianBatDauCongKhai, khkk.ThoiGianKetThucCongKhai, khkk.Ma_Loai_KeKhai, SoLuong = db.NV_LapKeHoachKeKhai.Where(_ => _.Ma_CoQuan_DonVi == cq.Ma_CoQuan_DonVi && _.TrangThai == true).Count(), }).ToList();

            var canBoKeKhaiHangNam = (from lkhkk in db.NV_LapKeHoachKeKhai
                                      join lkhkkHN in db.NV_LapKeHoachKeKhai_DanhSachKeKhaiHangNam on lkhkk.MaKeHoachKeKhai equals lkhkkHN.MaKeHoachKeKhai
                                      join lkhkkHN_ct in db.NV_LapKeHoachKeKhai_DanhSachKeKhaiHangNam_ChiTiet on lkhkkHN.MaKeHoachKeKhai_DanhSachKeKhaiHangNam_ID equals lkhkkHN_ct.MaKeHoachKeKhai_DanhSachKeKhaiHangNam_ID
                                      join cb in db.DM_CanBo on lkhkkHN_ct.Ma_CanBo equals cb.Ma_CanBo
                                      join cq in db.DM_CoQuanDonVi on cb.Ma_CoQuan_DonVi equals cq.Ma_CoQuan_DonVi
                                      join tk in db.HT_TaiKhoan on cb.Ma_CanBo equals tk.Ma_CanBo
                                      join ntk in db.HT_NhomTaiKhoan on tk.MaNhomTaiKhoan equals ntk.MaNhomTaiKhoan
                                      where ntk.MaTaiKhoan != "ADMIN" && ntk.MaTaiKhoan != "NDDTTT" && lkhkk.TrangThai == true
                                      select new { cb.Ma_CanBo, lkhkk.MaKeHoachKeKhai, cb.Ma_CoQuan_DonVi, lkhkk.Ma_Loai_KeKhai, lkhkk.KeHoachNam, }).ToList();

            var canBoKeKhaiHangNam_DaKeKhai = (from lkhkk in db.NV_LapKeHoachKeKhai
                                      join lkhkkHN in db.NV_LapKeHoachKeKhai_DanhSachKeKhaiHangNam on lkhkk.MaKeHoachKeKhai equals lkhkkHN.MaKeHoachKeKhai
                                      join lkhkkHN_ct in db.NV_LapKeHoachKeKhai_DanhSachKeKhaiHangNam_ChiTiet on lkhkkHN.MaKeHoachKeKhai_DanhSachKeKhaiHangNam_ID equals lkhkkHN_ct.MaKeHoachKeKhai_DanhSachKeKhaiHangNam_ID
                                      join cb in db.DM_CanBo on lkhkkHN_ct.Ma_CanBo equals cb.Ma_CanBo
                                      join cq in db.DM_CoQuanDonVi on cb.Ma_CoQuan_DonVi equals cq.Ma_CoQuan_DonVi
                                      join tk in db.HT_TaiKhoan on cb.Ma_CanBo equals tk.Ma_CanBo
                                      join ntk in db.HT_NhomTaiKhoan on tk.MaNhomTaiKhoan equals ntk.MaNhomTaiKhoan
                                      join bkk in db.Nv_KeKhai_TSTN on cb.Ma_CanBo equals bkk.Ma_CanBo
                                      where ntk.MaTaiKhoan != "ADMIN" && ntk.MaTaiKhoan != "NDDTTT" && lkhkk.TrangThai == true && bkk.MaKeHoachKeKhai == lkhkk.MaKeHoachKeKhai && bkk.TrangThai == true
                                      select new { cb.Ma_CanBo, lkhkk.MaKeHoachKeKhai, cb.Ma_CoQuan_DonVi, lkhkk.Ma_Loai_KeKhai, lkhkk.KeHoachNam, Ma_Loai_KeKhai_BKK  = bkk.Ma_Loai_KeKhai }).ToList();

            var canBoKeKhaiBoSung = (from lkhkk in db.NV_LapKeHoachKeKhai
                                      join lkhkkBS in db.NV_LapKeHoachKeKhai_DanhSachKeKhaiBoSung on lkhkk.MaKeHoachKeKhai equals lkhkkBS.MaKeHoachKeKhai
                                      join lkhkkBS_ct in db.NV_LapKeHoachKeKhai_DanhSachKeKhaiBoSung_ChiTiet on lkhkkBS.MaKeHoachKeKhai_DanhSachKeKhaiBoSung_ID equals lkhkkBS_ct.MaKeHoachKeKhai_DanhSachKeKhaiBoSung_ID
                                      join cb in db.DM_CanBo on lkhkkBS_ct.Ma_CanBo equals cb.Ma_CanBo
                                      join cq in db.DM_CoQuanDonVi on cb.Ma_CoQuan_DonVi equals cq.Ma_CoQuan_DonVi
                                      join tk in db.HT_TaiKhoan on cb.Ma_CanBo equals tk.Ma_CanBo
                                      join ntk in db.HT_NhomTaiKhoan on tk.MaNhomTaiKhoan equals ntk.MaNhomTaiKhoan
                                      where ntk.MaTaiKhoan != "ADMIN" && ntk.MaTaiKhoan != "NDDTTT" && lkhkk.TrangThai == true
                                     select new { cb.Ma_CanBo, lkhkk.MaKeHoachKeKhai, cb.Ma_CoQuan_DonVi, lkhkk.Ma_Loai_KeKhai, lkhkk.KeHoachNam, }).ToList();

            var canBoKeKhaiBoSung_DaKeKhai = (from lkhkk in db.NV_LapKeHoachKeKhai
                                               join lkhkkBS in db.NV_LapKeHoachKeKhai_DanhSachKeKhaiBoSung on lkhkk.MaKeHoachKeKhai equals lkhkkBS.MaKeHoachKeKhai
                                               join lkhkkBS_ct in db.NV_LapKeHoachKeKhai_DanhSachKeKhaiBoSung_ChiTiet on lkhkkBS.MaKeHoachKeKhai_DanhSachKeKhaiBoSung_ID equals lkhkkBS_ct.MaKeHoachKeKhai_DanhSachKeKhaiBoSung_ID
                                               join cb in db.DM_CanBo on lkhkkBS_ct.Ma_CanBo equals cb.Ma_CanBo
                                               join cq in db.DM_CoQuanDonVi on cb.Ma_CoQuan_DonVi equals cq.Ma_CoQuan_DonVi
                                               join tk in db.HT_TaiKhoan on cb.Ma_CanBo equals tk.Ma_CanBo
                                               join ntk in db.HT_NhomTaiKhoan on tk.MaNhomTaiKhoan equals ntk.MaNhomTaiKhoan
                                               join bkk in db.Nv_KeKhai_TSTN on cb.Ma_CanBo equals bkk.Ma_CanBo
                                               where ntk.MaTaiKhoan != "ADMIN" && ntk.MaTaiKhoan != "NDDTTT" && lkhkk.TrangThai == true && bkk.MaKeHoachKeKhai == lkhkk.MaKeHoachKeKhai && bkk.TrangThai == true
                                               select new { cb.Ma_CanBo, lkhkk.MaKeHoachKeKhai, cb.Ma_CoQuan_DonVi, lkhkk.Ma_Loai_KeKhai, lkhkk.KeHoachNam, Ma_Loai_KeKhai_BKK = bkk.Ma_Loai_KeKhai }).ToList();


            var canBoKeKhaiLanDau = (from lkhkk in db.NV_LapKeHoachKeKhai
                                     join lkhkkLD in db.NV_LapKeHoachKeKhai_DanhSachKeKhaiLanDau on lkhkk.MaKeHoachKeKhai equals lkhkkLD.MaKeHoachKeKhai
                                     join lkhkkLD_ct in db.NV_LapKeHoachKeKhai_DanhSachKeKhaiLanDau_ChiTiet on lkhkkLD.MaKeHoachKeKhai_DanhSachKeKhaiLanDau_ID equals lkhkkLD_ct.MaKeHoachKeKhai_DanhSachKeKhaiLanDau_ID
                                     join cb in db.DM_CanBo on lkhkkLD_ct.Ma_CanBo equals cb.Ma_CanBo
                                     join cq in db.DM_CoQuanDonVi on cb.Ma_CoQuan_DonVi equals cq.Ma_CoQuan_DonVi
                                     join tk in db.HT_TaiKhoan on cb.Ma_CanBo equals tk.Ma_CanBo
                                     join ntk in db.HT_NhomTaiKhoan on tk.MaNhomTaiKhoan equals ntk.MaNhomTaiKhoan
                                     where ntk.MaTaiKhoan != "ADMIN" && ntk.MaTaiKhoan != "NDDTTT" && lkhkk.TrangThai == true
                                     select new { cb.Ma_CanBo, lkhkk.MaKeHoachKeKhai, cb.Ma_CoQuan_DonVi, lkhkk.Ma_Loai_KeKhai, lkhkk.KeHoachNam }).ToList();

            var canBoKeKhaiLanDau_DaKeKHai = (from lkhkk in db.NV_LapKeHoachKeKhai
                                     join lkhkkLD in db.NV_LapKeHoachKeKhai_DanhSachKeKhaiLanDau on lkhkk.MaKeHoachKeKhai equals lkhkkLD.MaKeHoachKeKhai
                                     join lkhkkLD_ct in db.NV_LapKeHoachKeKhai_DanhSachKeKhaiLanDau_ChiTiet on lkhkkLD.MaKeHoachKeKhai_DanhSachKeKhaiLanDau_ID equals lkhkkLD_ct.MaKeHoachKeKhai_DanhSachKeKhaiLanDau_ID
                                     join cb in db.DM_CanBo on lkhkkLD_ct.Ma_CanBo equals cb.Ma_CanBo
                                     join cq in db.DM_CoQuanDonVi on cb.Ma_CoQuan_DonVi equals cq.Ma_CoQuan_DonVi
                                     join tk in db.HT_TaiKhoan on cb.Ma_CanBo equals tk.Ma_CanBo
                                     join ntk in db.HT_NhomTaiKhoan on tk.MaNhomTaiKhoan equals ntk.MaNhomTaiKhoan
                                     join bkk in db.Nv_KeKhai_TSTN on cb.Ma_CanBo equals bkk.Ma_CanBo
                                     where ntk.MaTaiKhoan != "ADMIN" && ntk.MaTaiKhoan != "NDDTTT" && lkhkk.TrangThai == true && bkk.MaKeHoachKeKhai == lkhkk.MaKeHoachKeKhai && bkk.TrangThai == true
                                     select new { cb.Ma_CanBo, lkhkk.MaKeHoachKeKhai, cb.Ma_CoQuan_DonVi, lkhkk.Ma_Loai_KeKhai, lkhkk.KeHoachNam, Ma_Loai_KeKhai_BKK = bkk.Ma_Loai_KeKhai }).ToList();

            var canBoKeKhaiBoNhiemCanBo = (from lkhkk in db.NV_LapKeHoachKeKhai
                                           join lkhkkBNCB in db.NV_LapKeHoachKeKhai_DanhSachKeKhaiBoNhiemCongTacCanBo on lkhkk.MaKeHoachKeKhai equals lkhkkBNCB.MaKeHoachKeKhai
                                           join lkhkkLD_ct in db.NV_LapKeHoachKeKhai_DanhSachKeKhaiBoNhiemCongTacCanBo_ChiTiet on lkhkkBNCB.MaKeHoachKeKhai_DanhSachKeKhaiBoNhiemCongTacCanBo_ID equals lkhkkLD_ct.MaKeHoachKeKhai_DanhSachKeKhaiBoNhiemCongTacCanBo_ID
                                           join cb in db.DM_CanBo on lkhkkLD_ct.Ma_CanBo equals cb.Ma_CanBo
                                           join cq in db.DM_CoQuanDonVi on cb.Ma_CoQuan_DonVi equals cq.Ma_CoQuan_DonVi
                                           join tk in db.HT_TaiKhoan on cb.Ma_CanBo equals tk.Ma_CanBo
                                           join ntk in db.HT_NhomTaiKhoan on tk.MaNhomTaiKhoan equals ntk.MaNhomTaiKhoan
                                           where ntk.MaTaiKhoan != "ADMIN" && ntk.MaTaiKhoan != "NDDTTT" && lkhkk.TrangThai == true
                                           select new { cb.Ma_CanBo, lkhkk.MaKeHoachKeKhai, cb.Ma_CoQuan_DonVi, lkhkk.Ma_Loai_KeKhai, lkhkk.KeHoachNam }).ToList();

            var canBoKeKhaiBoNhiemCanBo_DaKeKhai = (from lkhkk in db.NV_LapKeHoachKeKhai
                                           join lkhkkBNCB in db.NV_LapKeHoachKeKhai_DanhSachKeKhaiBoNhiemCongTacCanBo on lkhkk.MaKeHoachKeKhai equals lkhkkBNCB.MaKeHoachKeKhai
                                           join lkhkkLD_ct in db.NV_LapKeHoachKeKhai_DanhSachKeKhaiBoNhiemCongTacCanBo_ChiTiet on lkhkkBNCB.MaKeHoachKeKhai_DanhSachKeKhaiBoNhiemCongTacCanBo_ID equals lkhkkLD_ct.MaKeHoachKeKhai_DanhSachKeKhaiBoNhiemCongTacCanBo_ID
                                           join cb in db.DM_CanBo on lkhkkLD_ct.Ma_CanBo equals cb.Ma_CanBo
                                           join cq in db.DM_CoQuanDonVi on cb.Ma_CoQuan_DonVi equals cq.Ma_CoQuan_DonVi
                                           join tk in db.HT_TaiKhoan on cb.Ma_CanBo equals tk.Ma_CanBo
                                           join ntk in db.HT_NhomTaiKhoan on tk.MaNhomTaiKhoan equals ntk.MaNhomTaiKhoan
                                           join bkk in db.Nv_KeKhai_TSTN on cb.Ma_CanBo equals bkk.Ma_CanBo
                                           where ntk.MaTaiKhoan != "ADMIN" && ntk.MaTaiKhoan != "NDDTTT" && lkhkk.TrangThai == true && bkk.MaKeHoachKeKhai == lkhkk.MaKeHoachKeKhai && bkk.TrangThai == true
                                           select new { cb.Ma_CanBo, lkhkk.MaKeHoachKeKhai, cb.Ma_CoQuan_DonVi, lkhkk.Ma_Loai_KeKhai, lkhkk.KeHoachNam, Ma_Loai_KeKhai_BKK = bkk.Ma_Loai_KeKhai }).ToList();

            if (!string.IsNullOrEmpty(NamKeKhai))
            {
                canBoKeKhaiHangNam = canBoKeKhaiHangNam.Where(_ => _.KeHoachNam == Int32.Parse(NamKeKhai)).ToList();
                canBoKeKhaiHangNam_DaKeKhai = canBoKeKhaiHangNam_DaKeKhai.Where(_ => _.KeHoachNam == Int32.Parse(NamKeKhai)).ToList();

                canBoKeKhaiLanDau = canBoKeKhaiLanDau.Where(_ => _.KeHoachNam == Int32.Parse(NamKeKhai)).ToList();
                canBoKeKhaiLanDau_DaKeKHai = canBoKeKhaiLanDau_DaKeKHai.Where(_ => _.KeHoachNam == Int32.Parse(NamKeKhai)).ToList();

                canBoKeKhaiBoNhiemCanBo = canBoKeKhaiBoNhiemCanBo.Where(_ => _.KeHoachNam == Int32.Parse(NamKeKhai)).ToList();
                canBoKeKhaiBoNhiemCanBo_DaKeKhai = canBoKeKhaiBoNhiemCanBo_DaKeKhai.Where(_ => _.KeHoachNam == Int32.Parse(NamKeKhai)).ToList();

            }

           
           

            if (!string.IsNullOrEmpty(LoaiKeKhai))
            {
                if (Int32.Parse(LoaiKeKhai) == 3)
                {
                    var data = (from cq in db.DM_CoQuanDonVi
                                join khkk in db.NV_LapKeHoachKeKhai on cq.Ma_CoQuan_DonVi equals khkk.Ma_CoQuan_DonVi
                                join lcq in db.DM_Loai_CoQuan_DonVi on cq.MaLoai_CoQuan_DonVi equals lcq.Ma_Loai_CQDV
                                where cq.MaLoai_CoQuan_DonVi != 35 && khkk.Ma_Loai_KeKhai == 3 && khkk.TrangThai == true
                                orderby lcq.Ten_Loai_CQDV ascending, cq.Ten ascending
                                select new { cq.Ma_CoQuan_DonVi, cq.Ten, cq.MaLoai_CoQuan_DonVi, lcq.Ten_Loai_CQDV, khkk.MaKeHoachKeKhai, khkk.TenKeHoachKeKhai, khkk.KeHoachNam, khkk.ThoiGianBatDauCongKhai, khkk.ThoiGianKetThucCongKhai, khkk.Ma_Loai_KeKhai, SoLuong = db.NV_LapKeHoachKeKhai.Where(_ => _.Ma_CoQuan_DonVi == cq.Ma_CoQuan_DonVi && _.TrangThai == true).Count(), 
                                            DSCanBoPhaiKeKhai =  (from _lkhkk in db.NV_LapKeHoachKeKhai
                                                                 join _lkhkkLD in db.NV_LapKeHoachKeKhai_DanhSachKeKhaiLanDau on _lkhkk.MaKeHoachKeKhai equals _lkhkkLD.MaKeHoachKeKhai
                                                                 join _lkhkkLD_ct in db.NV_LapKeHoachKeKhai_DanhSachKeKhaiLanDau_ChiTiet on _lkhkkLD.MaKeHoachKeKhai_DanhSachKeKhaiLanDau_ID equals _lkhkkLD_ct.MaKeHoachKeKhai_DanhSachKeKhaiLanDau_ID
                                                                 join _cb in db.DM_CanBo on _lkhkkLD_ct.Ma_CanBo equals _cb.Ma_CanBo
                                                                 join _cq in db.DM_CoQuanDonVi on _cb.Ma_CoQuan_DonVi equals _cq.Ma_CoQuan_DonVi
                                                                 join _tk in db.HT_TaiKhoan on _cb.Ma_CanBo equals _tk.Ma_CanBo
                                                                 join _ntk in db.HT_NhomTaiKhoan on _tk.MaNhomTaiKhoan equals _ntk.MaNhomTaiKhoan
                                                                 where _ntk.MaTaiKhoan != "ADMIN" && _ntk.MaTaiKhoan != "NDDTTT" && _lkhkk.TrangThai == true && _cq.Ma_CoQuan_DonVi == cq.Ma_CoQuan_DonVi && khkk.MaKeHoachKeKhai == _lkhkk.MaKeHoachKeKhai && _lkhkk.Ma_Loai_KeKhai == khkk.Ma_Loai_KeKhai && _lkhkk.KeHoachNam == khkk.KeHoachNam
                                                                 select new { _cb.Ma_CanBo, _lkhkk.MaKeHoachKeKhai, _cb.Ma_CoQuan_DonVi, _lkhkk.Ma_Loai_KeKhai, _lkhkk.KeHoachNam }).Count(),
                                            CanBoDaKeKhai = (from _lkhkk in db.NV_LapKeHoachKeKhai
                                                             join _lkhkkLD in db.NV_LapKeHoachKeKhai_DanhSachKeKhaiLanDau on _lkhkk.MaKeHoachKeKhai equals _lkhkkLD.MaKeHoachKeKhai
                                                             join _lkhkkLD_ct in db.NV_LapKeHoachKeKhai_DanhSachKeKhaiLanDau_ChiTiet on _lkhkkLD.MaKeHoachKeKhai_DanhSachKeKhaiLanDau_ID equals _lkhkkLD_ct.MaKeHoachKeKhai_DanhSachKeKhaiLanDau_ID
                                                             join _cb in db.DM_CanBo on _lkhkkLD_ct.Ma_CanBo equals _cb.Ma_CanBo
                                                             join _cq in db.DM_CoQuanDonVi on _cb.Ma_CoQuan_DonVi equals _cq.Ma_CoQuan_DonVi
                                                             join _tk in db.HT_TaiKhoan on _cb.Ma_CanBo equals _tk.Ma_CanBo
                                                             join _ntk in db.HT_NhomTaiKhoan on _tk.MaNhomTaiKhoan equals _ntk.MaNhomTaiKhoan
                                                             join _bkk in db.Nv_KeKhai_TSTN on _cb.Ma_CanBo equals _bkk.Ma_CanBo
                                                             where _ntk.MaTaiKhoan != "ADMIN" && _ntk.MaTaiKhoan != "NDDTTT" && _lkhkk.TrangThai == true && _cq.Ma_CoQuan_DonVi == cq.Ma_CoQuan_DonVi && khkk.MaKeHoachKeKhai == _lkhkk.MaKeHoachKeKhai && _lkhkk.Ma_Loai_KeKhai == khkk.Ma_Loai_KeKhai && _lkhkk.KeHoachNam == khkk.KeHoachNam && _bkk.MaKeHoachKeKhai  == khkk.MaKeHoachKeKhai
                                                             select new { _cb.Ma_CanBo, _lkhkk.MaKeHoachKeKhai, _cb.Ma_CoQuan_DonVi, _lkhkk.Ma_Loai_KeKhai, _lkhkk.KeHoachNam }).Count()

                }).ToList();
                    if (!string.IsNullOrEmpty(Ten))
                    {
                        data = data.Where(a => a.Ten.ToUpper().Contains(Ten.ToUpper()) || a.Ten_Loai_CQDV.ToUpper().Contains(Ten.ToUpper())).ToList();
                    }
                    if (!string.IsNullOrEmpty(TrangThaiKeKhai))
                    {
                        if(TrangThaiKeKhai == "true")
                        {
                            data = data.Where(a => a.DSCanBoPhaiKeKhai == a.CanBoDaKeKhai).ToList();
                        }
                        else if(TrangThaiKeKhai == "false")
                        {
                            data = data.Where(a => a.DSCanBoPhaiKeKhai > a.CanBoDaKeKhai).ToList();
                        }
                        
                    }

                    if (!string.IsNullOrEmpty(NamKeKhai))
                    {

                        data = data.Where(a => a.KeHoachNam == Int32.Parse(NamKeKhai)).ToList();
                    }

                    recordsTotal = data.Count();
                    var data1 = data.Skip(skip).Take(pageSize).ToList();
                    return Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data1 }, JsonRequestBehavior.AllowGet);
                }
                else if (Int32.Parse(LoaiKeKhai) == 4 )
                {
                    var data = (from cq in db.DM_CoQuanDonVi
                                join khkk in db.NV_LapKeHoachKeKhai on cq.Ma_CoQuan_DonVi equals khkk.Ma_CoQuan_DonVi
                                join lcq in db.DM_Loai_CoQuan_DonVi on cq.MaLoai_CoQuan_DonVi equals lcq.Ma_Loai_CQDV
                                where khkk.Ma_Loai_KeKhai == 4
                                orderby lcq.Ten_Loai_CQDV ascending, cq.Ten ascending
                                select new { cq.Ma_CoQuan_DonVi, cq.Ten, cq.MaLoai_CoQuan_DonVi, lcq.Ten_Loai_CQDV, khkk.MaKeHoachKeKhai, khkk.TenKeHoachKeKhai, khkk.KeHoachNam, khkk.ThoiGianBatDauCongKhai, khkk.ThoiGianKetThucCongKhai, khkk.Ma_Loai_KeKhai,
                                    SoLuong = db.NV_LapKeHoachKeKhai.Where(_ => _.Ma_CoQuan_DonVi == cq.Ma_CoQuan_DonVi && _.TrangThai == true).Count(),
                                    DSCanBoPhaiKeKhai = (from _lkhkk in db.NV_LapKeHoachKeKhai
                                                         join _lkhkkHN in db.NV_LapKeHoachKeKhai_DanhSachKeKhaiHangNam on _lkhkk.MaKeHoachKeKhai equals _lkhkkHN.MaKeHoachKeKhai
                                                         join _lkhkkHN_ct in db.NV_LapKeHoachKeKhai_DanhSachKeKhaiHangNam_ChiTiet on _lkhkkHN.MaKeHoachKeKhai_DanhSachKeKhaiHangNam_ID equals _lkhkkHN_ct.MaKeHoachKeKhai_DanhSachKeKhaiHangNam_ID
                                                         join _cb in db.DM_CanBo on _lkhkkHN_ct.Ma_CanBo equals _cb.Ma_CanBo
                                                         join _cq in db.DM_CoQuanDonVi on _cb.Ma_CoQuan_DonVi equals _cq.Ma_CoQuan_DonVi
                                                         join _tk in db.HT_TaiKhoan on _cb.Ma_CanBo equals _tk.Ma_CanBo
                                                         join _ntk in db.HT_NhomTaiKhoan on _tk.MaNhomTaiKhoan equals _ntk.MaNhomTaiKhoan
                                                         where _ntk.MaTaiKhoan != "ADMIN" && _ntk.MaTaiKhoan != "NDDTTT" && _cq.Ma_CoQuan_DonVi == cq.Ma_CoQuan_DonVi && khkk.MaKeHoachKeKhai == _lkhkk.MaKeHoachKeKhai && _lkhkk.Ma_Loai_KeKhai == khkk.Ma_Loai_KeKhai && _lkhkk.KeHoachNam == khkk.KeHoachNam
                                                         select new { _cb.Ma_CanBo, _lkhkk.MaKeHoachKeKhai, _cb.Ma_CoQuan_DonVi, _lkhkk.Ma_Loai_KeKhai, _lkhkk.KeHoachNam }).Count(),
                                    CanBoDaKeKhai = (from _lkhkk in db.NV_LapKeHoachKeKhai
                                                     join _lkhkkHN in db.NV_LapKeHoachKeKhai_DanhSachKeKhaiHangNam on _lkhkk.MaKeHoachKeKhai equals _lkhkkHN.MaKeHoachKeKhai
                                                     join _lkhkkLD_ct in db.NV_LapKeHoachKeKhai_DanhSachKeKhaiHangNam_ChiTiet on _lkhkkHN.MaKeHoachKeKhai_DanhSachKeKhaiHangNam_ID equals _lkhkkLD_ct.MaKeHoachKeKhai_DanhSachKeKhaiHangNam_ID
                                                     join _cb in db.DM_CanBo on _lkhkkLD_ct.Ma_CanBo equals _cb.Ma_CanBo
                                                     join _cq in db.DM_CoQuanDonVi on _cb.Ma_CoQuan_DonVi equals _cq.Ma_CoQuan_DonVi
                                                     join _tk in db.HT_TaiKhoan on _cb.Ma_CanBo equals _tk.Ma_CanBo
                                                     join _ntk in db.HT_NhomTaiKhoan on _tk.MaNhomTaiKhoan equals _ntk.MaNhomTaiKhoan
                                                     join _bkk in db.Nv_KeKhai_TSTN on _cb.Ma_CanBo equals _bkk.Ma_CanBo
                                                     where _ntk.MaTaiKhoan != "ADMIN" && _ntk.MaTaiKhoan != "NDDTTT" && _cq.Ma_CoQuan_DonVi == cq.Ma_CoQuan_DonVi && khkk.MaKeHoachKeKhai == _lkhkk.MaKeHoachKeKhai && _lkhkk.Ma_Loai_KeKhai == khkk.Ma_Loai_KeKhai && _lkhkk.KeHoachNam == khkk.KeHoachNam && _bkk.MaKeHoachKeKhai == khkk.MaKeHoachKeKhai
                                                     select new { _cb.Ma_CanBo, _lkhkk.MaKeHoachKeKhai, _cb.Ma_CoQuan_DonVi, _lkhkk.Ma_Loai_KeKhai, _lkhkk.KeHoachNam }).Count()
                                }).ToList();
                    if (!string.IsNullOrEmpty(Ten))
                    {
                        data = data.Where(a => a.Ten.ToUpper().Contains(Ten.ToUpper()) || a.Ten_Loai_CQDV.ToUpper().Contains(Ten.ToUpper())).ToList();
                    }
                    if (!string.IsNullOrEmpty(TrangThaiKeKhai))
                    {
                        if (TrangThaiKeKhai == "true")
                        {
                            data = data.Where(a => a.DSCanBoPhaiKeKhai == a.CanBoDaKeKhai).ToList();
                        }
                        else if (TrangThaiKeKhai == "false")
                        {
                            data = data.Where(a => a.DSCanBoPhaiKeKhai > a.CanBoDaKeKhai).ToList();
                        }

                    }
                    if (!string.IsNullOrEmpty(NamKeKhai))
                    {

                        data = data.Where(a => a.KeHoachNam == Int32.Parse(NamKeKhai)).ToList();
                    }

                    recordsTotal = data.Count();
                    var data1 = data.Skip(skip).Take(pageSize).ToList();
                    return Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data1 }, JsonRequestBehavior.AllowGet);
                }
                else if (Int32.Parse(LoaiKeKhai) == 5)
                {
                    var data = (from cq in db.DM_CoQuanDonVi
                                join khkk in db.NV_LapKeHoachKeKhai on cq.Ma_CoQuan_DonVi equals khkk.Ma_CoQuan_DonVi
                                join lcq in db.DM_Loai_CoQuan_DonVi on cq.MaLoai_CoQuan_DonVi equals lcq.Ma_Loai_CQDV
                                where cq.MaLoai_CoQuan_DonVi != 35 && khkk.Ma_Loai_KeKhai == 4
                                orderby lcq.Ten_Loai_CQDV ascending, cq.Ten ascending
                                select new { cq.Ma_CoQuan_DonVi, cq.Ten, cq.MaLoai_CoQuan_DonVi, lcq.Ten_Loai_CQDV, khkk.MaKeHoachKeKhai, khkk.TenKeHoachKeKhai, khkk.KeHoachNam, khkk.ThoiGianBatDauCongKhai, khkk.ThoiGianKetThucCongKhai, khkk.Ma_Loai_KeKhai, 
                                    SoLuong = db.NV_LapKeHoachKeKhai.Where(_ => _.Ma_CoQuan_DonVi == cq.Ma_CoQuan_DonVi && _.TrangThai == true).Count(),
                                    DSCanBoPhaiKeKhai = (from _lkhkk in db.NV_LapKeHoachKeKhai
                                                         join _lkhkkBS in db.NV_LapKeHoachKeKhai_DanhSachKeKhaiBoSung on _lkhkk.MaKeHoachKeKhai equals _lkhkkBS.MaKeHoachKeKhai
                                                         join _lkhkkBS_ct in db.NV_LapKeHoachKeKhai_DanhSachKeKhaiBoSung_ChiTiet on _lkhkkBS.MaKeHoachKeKhai_DanhSachKeKhaiBoSung_ID equals _lkhkkBS_ct.MaKeHoachKeKhai_DanhSachKeKhaiBoSung_ID
                                                         join _cb in db.DM_CanBo on _lkhkkBS_ct.Ma_CanBo equals _cb.Ma_CanBo
                                                         join _cq in db.DM_CoQuanDonVi on _cb.Ma_CoQuan_DonVi equals _cq.Ma_CoQuan_DonVi
                                                         join _tk in db.HT_TaiKhoan on _cb.Ma_CanBo equals _tk.Ma_CanBo
                                                         join _ntk in db.HT_NhomTaiKhoan on _tk.MaNhomTaiKhoan equals _ntk.MaNhomTaiKhoan
                                                         where _ntk.MaTaiKhoan != "ADMIN" && _ntk.MaTaiKhoan != "NDDTTT" && _cq.Ma_CoQuan_DonVi == cq.Ma_CoQuan_DonVi && khkk.MaKeHoachKeKhai == _lkhkk.MaKeHoachKeKhai && _lkhkk.Ma_Loai_KeKhai == khkk.Ma_Loai_KeKhai && _lkhkk.KeHoachNam == khkk.KeHoachNam
                                                         select new { _cb.Ma_CanBo, _lkhkk.MaKeHoachKeKhai, _cb.Ma_CoQuan_DonVi, _lkhkk.Ma_Loai_KeKhai, _lkhkk.KeHoachNam }).Count(),
                                    CanBoDaKeKhai = (from _lkhkk in db.NV_LapKeHoachKeKhai
                                                     join _lkhkkBS in db.NV_LapKeHoachKeKhai_DanhSachKeKhaiBoSung on _lkhkk.MaKeHoachKeKhai equals _lkhkkBS.MaKeHoachKeKhai
                                                     join _lkhkkBS_ct in db.NV_LapKeHoachKeKhai_DanhSachKeKhaiBoSung_ChiTiet on _lkhkkBS.MaKeHoachKeKhai_DanhSachKeKhaiBoSung_ID equals _lkhkkBS_ct.MaKeHoachKeKhai_DanhSachKeKhaiBoSung_ID
                                                     join _cb in db.DM_CanBo on _lkhkkBS_ct.Ma_CanBo equals _cb.Ma_CanBo
                                                     join _cq in db.DM_CoQuanDonVi on _cb.Ma_CoQuan_DonVi equals _cq.Ma_CoQuan_DonVi
                                                     join _tk in db.HT_TaiKhoan on _cb.Ma_CanBo equals _tk.Ma_CanBo
                                                     join _ntk in db.HT_NhomTaiKhoan on _tk.MaNhomTaiKhoan equals _ntk.MaNhomTaiKhoan
                                                     join _bkk in db.Nv_KeKhai_TSTN on _cb.Ma_CanBo equals _bkk.Ma_CanBo
                                                     where _ntk.MaTaiKhoan != "ADMIN" && _ntk.MaTaiKhoan != "NDDTTT" && _cq.Ma_CoQuan_DonVi == cq.Ma_CoQuan_DonVi && khkk.MaKeHoachKeKhai == _lkhkk.MaKeHoachKeKhai && _lkhkk.Ma_Loai_KeKhai == khkk.Ma_Loai_KeKhai && _lkhkk.KeHoachNam == khkk.KeHoachNam && _bkk.MaKeHoachKeKhai == khkk.MaKeHoachKeKhai
                                                     select new { _cb.Ma_CanBo, _lkhkk.MaKeHoachKeKhai, _cb.Ma_CoQuan_DonVi, _lkhkk.Ma_Loai_KeKhai, _lkhkk.KeHoachNam }).Count()
                                }).ToList();

                    if (!string.IsNullOrEmpty(Ten))
                    {
                        data = data.Where(a => a.Ten.ToUpper().Contains(Ten.ToUpper()) || a.Ten_Loai_CQDV.ToUpper().Contains(Ten.ToUpper())).ToList();
                    }
                    if (!string.IsNullOrEmpty(TrangThaiKeKhai))
                    {
                        if (TrangThaiKeKhai == "true")
                        {
                            data = data.Where(a => a.DSCanBoPhaiKeKhai == a.CanBoDaKeKhai).ToList();
                        }
                        else if (TrangThaiKeKhai == "false")
                        {
                            data = data.Where(a => a.DSCanBoPhaiKeKhai > a.CanBoDaKeKhai).ToList();
                        }

                    }
                    if (!string.IsNullOrEmpty(NamKeKhai))
                    {

                        data = data.Where(a => a.KeHoachNam == Int32.Parse(NamKeKhai)).ToList();
                    }

                    recordsTotal = data.Count();
                    var data1 = data.Skip(skip).Take(pageSize).ToList();
                    return Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data1 }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    var Loaikk = Int32.Parse(LoaiKeKhai);
                    var data = (from cq in db.DM_CoQuanDonVi
                                join khkk in db.NV_LapKeHoachKeKhai on cq.Ma_CoQuan_DonVi equals khkk.Ma_CoQuan_DonVi
                                join lcq in db.DM_Loai_CoQuan_DonVi on cq.MaLoai_CoQuan_DonVi equals lcq.Ma_Loai_CQDV
                                where cq.MaLoai_CoQuan_DonVi != 35 && khkk.Ma_Loai_KeKhai == Loaikk
                                orderby lcq.Ten_Loai_CQDV ascending, cq.Ten ascending
                                select new { cq.Ma_CoQuan_DonVi, cq.Ten, cq.MaLoai_CoQuan_DonVi, lcq.Ten_Loai_CQDV, khkk.MaKeHoachKeKhai, khkk.TenKeHoachKeKhai, khkk.KeHoachNam, khkk.ThoiGianBatDauCongKhai, khkk.ThoiGianKetThucCongKhai, khkk.Ma_Loai_KeKhai, 
                                    SoLuong = 0, 
                                    DSCanBoPhaiKeKhai = (from _lkhkk in db.NV_LapKeHoachKeKhai
                                                         join _lkhkkBNCTCB in db.NV_LapKeHoachKeKhai_DanhSachKeKhaiBoNhiemCongTacCanBo on _lkhkk.MaKeHoachKeKhai equals _lkhkkBNCTCB.MaKeHoachKeKhai
                                                         join _lkhkkBNCTCB_ct in db.NV_LapKeHoachKeKhai_DanhSachKeKhaiBoNhiemCongTacCanBo_ChiTiet on _lkhkkBNCTCB.MaKeHoachKeKhai_DanhSachKeKhaiBoNhiemCongTacCanBo_ID equals _lkhkkBNCTCB_ct.MaKeHoachKeKhai_DanhSachKeKhaiBoNhiemCongTacCanBo_ID
                                                         join _cb in db.DM_CanBo on _lkhkkBNCTCB_ct.Ma_CanBo equals _cb.Ma_CanBo
                                                         join _cq in db.DM_CoQuanDonVi on _cb.Ma_CoQuan_DonVi equals _cq.Ma_CoQuan_DonVi
                                                         join _tk in db.HT_TaiKhoan on _cb.Ma_CanBo equals _tk.Ma_CanBo
                                                         join _ntk in db.HT_NhomTaiKhoan on _tk.MaNhomTaiKhoan equals _ntk.MaNhomTaiKhoan
                                                         where _ntk.MaTaiKhoan != "ADMIN" && _ntk.MaTaiKhoan != "NDDTTT" && _cq.Ma_CoQuan_DonVi == cq.Ma_CoQuan_DonVi && khkk.MaKeHoachKeKhai == _lkhkk.MaKeHoachKeKhai && _lkhkk.Ma_Loai_KeKhai == khkk.Ma_Loai_KeKhai && _lkhkk.KeHoachNam == khkk.KeHoachNam
                                                         select new { _cb.Ma_CanBo, _lkhkk.MaKeHoachKeKhai, _cb.Ma_CoQuan_DonVi, _lkhkk.Ma_Loai_KeKhai, _lkhkk.KeHoachNam }).Count(),
                                    CanBoDaKeKhai = (from _lkhkk in db.NV_LapKeHoachKeKhai
                                                     join _lkhkkBNCTCB in db.NV_LapKeHoachKeKhai_DanhSachKeKhaiBoNhiemCongTacCanBo on _lkhkk.MaKeHoachKeKhai equals _lkhkkBNCTCB.MaKeHoachKeKhai
                                                     join _lkhkkBNCTCB_ct in db.NV_LapKeHoachKeKhai_DanhSachKeKhaiBoNhiemCongTacCanBo_ChiTiet on _lkhkkBNCTCB.MaKeHoachKeKhai_DanhSachKeKhaiBoNhiemCongTacCanBo_ID equals _lkhkkBNCTCB_ct.MaKeHoachKeKhai_DanhSachKeKhaiBoNhiemCongTacCanBo_ID
                                                     join _cb in db.DM_CanBo on _lkhkkBNCTCB_ct.Ma_CanBo equals _cb.Ma_CanBo
                                                     join _cq in db.DM_CoQuanDonVi on _cb.Ma_CoQuan_DonVi equals _cq.Ma_CoQuan_DonVi
                                                     join _tk in db.HT_TaiKhoan on _cb.Ma_CanBo equals _tk.Ma_CanBo
                                                     join _ntk in db.HT_NhomTaiKhoan on _tk.MaNhomTaiKhoan equals _ntk.MaNhomTaiKhoan
                                                     join _bkk in db.Nv_KeKhai_TSTN on _cb.Ma_CanBo equals _bkk.Ma_CanBo
                                                     where _ntk.MaTaiKhoan != "ADMIN" && _ntk.MaTaiKhoan != "NDDTTT" && _cq.Ma_CoQuan_DonVi == cq.Ma_CoQuan_DonVi && khkk.MaKeHoachKeKhai == _lkhkk.MaKeHoachKeKhai && _lkhkk.Ma_Loai_KeKhai == khkk.Ma_Loai_KeKhai && _lkhkk.KeHoachNam == khkk.KeHoachNam && _bkk.MaKeHoachKeKhai == khkk.MaKeHoachKeKhai
                                                     select new { _cb.Ma_CanBo, _lkhkk.MaKeHoachKeKhai, _cb.Ma_CoQuan_DonVi, _lkhkk.Ma_Loai_KeKhai, _lkhkk.KeHoachNam }).Count()
                                }).ToList();
                    if (!string.IsNullOrEmpty(Ten))
                    {
                        data = data.Where(a => a.Ten.ToUpper().Contains(Ten.ToUpper()) || a.Ten_Loai_CQDV.ToUpper().Contains(Ten.ToUpper())).ToList();
                    }
                    if (!string.IsNullOrEmpty(TrangThaiKeKhai))
                    {
                        if (TrangThaiKeKhai == "true")
                        {
                            data = data.Where(a => a.DSCanBoPhaiKeKhai == a.CanBoDaKeKhai).ToList();
                        }
                        else if (TrangThaiKeKhai == "false")
                        {
                            data = data.Where(a => a.DSCanBoPhaiKeKhai > a.CanBoDaKeKhai).ToList();
                        }

                    }
                    if (!string.IsNullOrEmpty(NamKeKhai))
                    {

                        data = data.Where(a => a.KeHoachNam == Int32.Parse(NamKeKhai)).ToList();
                    }

                    recordsTotal = data.Count();
                    var data1 = data.Skip(skip).Take(pageSize).ToList();
                    return Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data1 }, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }

          
        }


        public JsonResult LoadDataKHKK(int? id)
        {
            var draw = Request.Form.GetValues("draw").FirstOrDefault();
            var start = Request.Form.GetValues("start").FirstOrDefault();
            var length = Request.Form.GetValues("length").FirstOrDefault();
            var search = Request.Form.GetValues("columns[0][search][value]").FirstOrDefault();
            var MaCoQuan = id;

            var list = new List<NV_LapKeHoachKeKhaiDS>();

            int pageSize = length != null ? Convert.ToInt32(length) : 0;
            int skip = start != null ? Convert.ToInt32(start) : 0;
            int recordsTotal = 0;


            var data = db.NV_LapKeHoachKeKhai.Where(_ => _.Ma_CoQuan_DonVi == MaCoQuan).OrderByDescending(_ => _.MaKeHoachKeKhai).ToList();

            if (!string.IsNullOrEmpty(search))
            {
                data = data.Where(a => a.TenKeHoachKeKhai.ToUpper().Contains(search.ToUpper()) || a.KeHoachNam.ToString().ToUpper().Contains(search.ToUpper())).ToList();
            }


            recordsTotal = data.Count();
            var data1 = data.Skip(skip).Take(pageSize).ToList();


            foreach (var item in data1)
            {
                var khkkds = new NV_LapKeHoachKeKhaiDS();
                khkkds.MaKeHoachKeKhai = item.MaKeHoachKeKhai;
                khkkds.TenKeHoachKeKhai = item.TenKeHoachKeKhai;
                khkkds.KeHoachNam = item.KeHoachNam;
                khkkds.ThoiGianBatDau = item.ThoiGianBatDau;
                khkkds.ThoiGianKetThuc = item.ThoiGianKetThuc;
                khkkds.NghiDinh = item.NghiDinh;
                khkkds.TienDo = item.TienDo;
                khkkds.TrangThai = item.TrangThai;
                khkkds.ThoiGianBatDauCongKhai = item.ThoiGianBatDauCongKhai;
                khkkds.ThoiGianKetThucCongKhai = item.ThoiGianKetThucCongKhai;
                khkkds.Ma_Loai_KeKhai = item.Ma_Loai_KeKhai;
                khkkds.Ma_CoQuan_DonVi = item.Ma_CoQuan_DonVi;

                if (item.Ma_Loai_KeKhai == 3)
                {
                    var dsld = db.NV_LapKeHoachKeKhai_DanhSachKeKhaiLanDau.Where(_ => _.MaKeHoachKeKhai == item.MaKeHoachKeKhai).First();
                    khkkds.TrangThaiDS = dsld.TrangThai;
                    khkkds.TrangThaiTonTaiDS = db.NV_LapKeHoachKeKhai_DanhSachKeKhaiLanDau_ChiTiet.Where(_ => _.MaKeHoachKeKhai_DanhSachKeKhaiLanDau_ID == dsld.MaKeHoachKeKhai_DanhSachKeKhaiLanDau_ID).Count();

                    if (dsld.TrangThai == true)
                    {
                        var dsdkk = db.Nv_KeKhai_TSTN.Where(_ => _.MaKeHoachKeKhai == item.MaKeHoachKeKhai && _.Ma_Loai_KeKhai == 3 && _.TrangThai == true);

                        khkkds.DaKeKhai = dsdkk.Count();
                    }

                }
                else if (item.Ma_Loai_KeKhai == 4)
                {
                    var dshn = db.NV_LapKeHoachKeKhai_DanhSachKeKhaiHangNam.Where(_ => _.MaKeHoachKeKhai == item.MaKeHoachKeKhai).First();
                    khkkds.TrangThaiDS = dshn.TrangThai;
                    khkkds.TrangThaiTonTaiDS = db.NV_LapKeHoachKeKhai_DanhSachKeKhaiHangNam_ChiTiet.Where(_ => _.MaKeHoachKeKhai_DanhSachKeKhaiHangNam_ID == dshn.MaKeHoachKeKhai_DanhSachKeKhaiHangNam_ID).Count();
                    if (dshn.TrangThai == true)
                    {
                        var dsdkkhn = db.Nv_KeKhai_TSTN.Where(_ => _.MaKeHoachKeKhai == item.MaKeHoachKeKhai && _.Ma_Loai_KeKhai == 4 && _.TrangThai == true);

                        khkkds.DaKeKhai = dsdkkhn.Count();

                    }
                }
                else
                {
                    var dsbnctcb = db.NV_LapKeHoachKeKhai_DanhSachKeKhaiBoNhiemCongTacCanBo.Where(_ => _.MaKeHoachKeKhai == item.MaKeHoachKeKhai).First();
                    khkkds.TrangThaiDS = dsbnctcb.TrangThai;
                    khkkds.TrangThaiTonTaiDS = db.NV_LapKeHoachKeKhai_DanhSachKeKhaiBoNhiemCongTacCanBo_ChiTiet.Where(_ => _.MaKeHoachKeKhai_DanhSachKeKhaiBoNhiemCongTacCanBo_ID == dsbnctcb.MaKeHoachKeKhai_DanhSachKeKhaiBoNhiemCongTacCanBo_ID).Count();
                    if (dsbnctcb.TrangThai == true)
                    {
                        var dsdkk = db.Nv_KeKhai_TSTN.Where(_ => _.MaKeHoachKeKhai == item.MaKeHoachKeKhai && _.Ma_Loai_KeKhai == 12 && _.TrangThai == true);

                        khkkds.DaKeKhai = dsdkk.Count();
                    }
                }
                if (khkkds.TrangThaiDS == true)
                {
                    list.Add(khkkds);
                }


            }


            return Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = list }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult LoadData2(int Ma_CoQuan_DonVi, int KeHoachNam,int Ma_Loai_KeKhai, int MaKeHoach)
        {
         
            var KHKK = db.NV_LapKeHoachKeKhai.Where(_ => _.MaKeHoachKeKhai == MaKeHoach && _.Ma_CoQuan_DonVi == Ma_CoQuan_DonVi && _.KeHoachNam == KeHoachNam ).First();

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
                            join cq in db.DM_CoQuanDonVi on cb.Ma_CoQuan_DonVi equals cq.Ma_CoQuan_DonVi
                            join dskkhn_ct in db.NV_LapKeHoachKeKhai_DanhSachKeKhaiLanDau_ChiTiet on cb.Ma_CanBo equals dskkhn_ct.Ma_CanBo
                            join dskkhn in db.NV_LapKeHoachKeKhai_DanhSachKeKhaiLanDau on dskkhn_ct.MaKeHoachKeKhai_DanhSachKeKhaiLanDau_ID equals dskkhn.MaKeHoachKeKhai_DanhSachKeKhaiLanDau_ID
                            join lkkhn in db.NV_LapKeHoachKeKhai on dskkhn.MaKeHoachKeKhai equals lkkhn.MaKeHoachKeKhai
                            join tk in db.HT_TaiKhoan on cb.Ma_CanBo equals tk.Ma_CanBo
                            join ntk in db.HT_NhomTaiKhoan on tk.MaNhomTaiKhoan equals ntk.MaNhomTaiKhoan
                            where lkkhn.MaKeHoachKeKhai == MaKeHoach
                            orderby ntk.Sort
                            select new
                            {
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
                                isKeKhai = (db.Nv_KeKhai_TSTN.Count(_ => _.Ma_CanBo == cb.Ma_CanBo && _.Ma_Loai_KeKhai == 3 && _.TrangThai == true && _.MaKeHoachKeKhai == MaKeHoach) == 1),
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
                            join cq in db.DM_CoQuanDonVi on cb.Ma_CoQuan_DonVi equals cq.Ma_CoQuan_DonVi
                            join dskkhn_ct in db.NV_LapKeHoachKeKhai_DanhSachKeKhaiHangNam_ChiTiet on cb.Ma_CanBo equals dskkhn_ct.Ma_CanBo
                            join dskkhn in db.NV_LapKeHoachKeKhai_DanhSachKeKhaiHangNam on dskkhn_ct.MaKeHoachKeKhai_DanhSachKeKhaiHangNam_ID equals dskkhn.MaKeHoachKeKhai_DanhSachKeKhaiHangNam_ID
                            join lkkhn in db.NV_LapKeHoachKeKhai on dskkhn.MaKeHoachKeKhai equals lkkhn.MaKeHoachKeKhai
                            join tk in db.HT_TaiKhoan on cb.Ma_CanBo equals tk.Ma_CanBo
                            join ntk in db.HT_NhomTaiKhoan on tk.MaNhomTaiKhoan equals ntk.MaNhomTaiKhoan
                            where lkkhn.MaKeHoachKeKhai == MaKeHoach
                            orderby ntk.Sort
                            select new
                            {
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
                                isKeKhai = (db.Nv_KeKhai_TSTN.Count(_ => _.Ma_CanBo == cb.Ma_CanBo && _.Ma_Loai_KeKhai == 4 && _.TrangThai == true && _.MaKeHoachKeKhai == MaKeHoach) == 1),
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
                            join cq in db.DM_CoQuanDonVi on cb.Ma_CoQuan_DonVi equals cq.Ma_CoQuan_DonVi
                            join dskkhn_ct in db.NV_LapKeHoachKeKhai_DanhSachKeKhaiBoNhiemCongTacCanBo_ChiTiet on cb.Ma_CanBo equals dskkhn_ct.Ma_CanBo
                            join dskkhn in db.NV_LapKeHoachKeKhai_DanhSachKeKhaiBoNhiemCongTacCanBo on dskkhn_ct.MaKeHoachKeKhai_DanhSachKeKhaiBoNhiemCongTacCanBo_ID equals dskkhn.MaKeHoachKeKhai_DanhSachKeKhaiBoNhiemCongTacCanBo_ID
                            join lkkhn in db.NV_LapKeHoachKeKhai on dskkhn.MaKeHoachKeKhai equals lkkhn.MaKeHoachKeKhai
                            join tk in db.HT_TaiKhoan on cb.Ma_CanBo equals tk.Ma_CanBo
                            join ntk in db.HT_NhomTaiKhoan on tk.MaNhomTaiKhoan equals ntk.MaNhomTaiKhoan
                            where lkkhn.MaKeHoachKeKhai == MaKeHoach
                            orderby ntk.Sort
                            select new
                            {
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
                                isKeKhai = (db.Nv_KeKhai_TSTN.Count(_ => _.Ma_CanBo == cb.Ma_CanBo && _.Ma_Loai_KeKhai == 12 && _.TrangThai == true && _.MaKeHoachKeKhai == MaKeHoach) == 1),
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
                        join cq in db.DM_CoQuanDonVi on cb.Ma_CoQuan_DonVi equals cq.Ma_CoQuan_DonVi
                        join dskkhn_ct in db.NV_LapKeHoachKeKhai_DanhSachKeKhaiBoSung_ChiTiet on cb.Ma_CanBo equals dskkhn_ct.Ma_CanBo
                        join dskkhn in db.NV_LapKeHoachKeKhai_DanhSachKeKhaiBoSung on dskkhn_ct.MaKeHoachKeKhai_DanhSachKeKhaiBoSung_ID equals dskkhn.MaKeHoachKeKhai_DanhSachKeKhaiBoSung_ID
                        join lkkhn in db.NV_LapKeHoachKeKhai on dskkhn.MaKeHoachKeKhai equals lkkhn.MaKeHoachKeKhai
                        join tk in db.HT_TaiKhoan on cb.Ma_CanBo equals tk.Ma_CanBo
                        join ntk in db.HT_NhomTaiKhoan on tk.MaNhomTaiKhoan equals ntk.MaNhomTaiKhoan
                        where lkkhn.MaKeHoachKeKhai == id
                        orderby ntk.Sort
                        select new
                        {
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
        public class NV_LapKeHoachKeKhaiDS
        {
            public int MaKeHoachKeKhai { get; set; }
            public string TenKeHoachKeKhai { get; set; }
            public Nullable<int> KeHoachNam { get; set; }
            public Nullable<System.DateTime> ThoiGianBatDau { get; set; }
            public Nullable<System.DateTime> ThoiGianKetThuc { get; set; }
            public string NghiDinh { get; set; }
            public string TienDo { get; set; }
            public Nullable<bool> TrangThai { get; set; }
            public Nullable<System.DateTime> ThoiGianBatDauCongKhai { get; set; }
            public Nullable<System.DateTime> ThoiGianKetThucCongKhai { get; set; }
            public Nullable<int> Ma_Loai_KeKhai { get; set; }
            public Nullable<int> Ma_CoQuan_DonVi { get; set; }
            public Nullable<bool> TrangThaiDS { get; set; }
            public Nullable<int> TrangThaiTonTaiDS { get; set; }

            public int DaKeKhai { get; set; }

        }

    }
}