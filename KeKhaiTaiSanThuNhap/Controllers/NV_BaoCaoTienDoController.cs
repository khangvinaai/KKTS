using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using KeKhaiTaiSanThuNhap.Models;

namespace KeKhaiTaiSanThuNhap.Controllers
{
    [Authorize(Roles = "ADMIN")]
    public class NV_BaoCaoTienDoController : Controller
    {
        private KSTNEntities db = new KSTNEntities();
        private UserInfo user = new UserInfo();

        // GET: NV_BaoCaoTienDo
        public ActionResult Index()
        {
            if (user.CheckQuyen("NV_KetQuaKeKhai", "Xem"))
            {
                return new HttpStatusCodeResult(404, "Not found");
            }

            return View();
        }


        public ActionResult XemChiTiet(int id)
        {
            if (user.CheckQuyen("NV_KetQuaKeKhai", "XemChiTiet"))
            {
                return new HttpStatusCodeResult(404, "Not found");
            }


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

                if (Role == "ADMIN")
                {
                    maCoQuan = (int)KHKK1.Ma_CoQuan_DonVi;
                }

                if (KHKK1.Ma_CoQuan_DonVi != maCoQuan)
                {
                    return new HttpStatusCodeResult(404, "Not found");
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

        public JsonResult LoadData(string ID_KeHoachKeKhai)
        {
            int id = Int16.Parse(ID_KeHoachKeKhai);
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
                            join cq in db.DM_CoQuanDonVi on cb.Ma_CoQuan_DonVi equals cq.Ma_CoQuan_DonVi
                            join dskkhn_ct in db.NV_LapKeHoachKeKhai_DanhSachKeKhaiLanDau_ChiTiet on cb.Ma_CanBo equals dskkhn_ct.Ma_CanBo
                            join dskkhn in db.NV_LapKeHoachKeKhai_DanhSachKeKhaiLanDau on dskkhn_ct.MaKeHoachKeKhai_DanhSachKeKhaiLanDau_ID equals dskkhn.MaKeHoachKeKhai_DanhSachKeKhaiLanDau_ID
                            join lkkhn in db.NV_LapKeHoachKeKhai on dskkhn.MaKeHoachKeKhai equals lkkhn.MaKeHoachKeKhai
                            join tk in db.HT_TaiKhoan on cb.Ma_CanBo equals tk.Ma_CanBo
                            join ntk in db.HT_NhomTaiKhoan on tk.MaNhomTaiKhoan equals ntk.MaNhomTaiKhoan
                            where lkkhn.MaKeHoachKeKhai == id && cq.Ma_CoQuan_DonVi == maCoQuan
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
                                isKeKhai = (db.Nv_KeKhai_TSTN.Count(_ => _.Ma_CanBo == cb.Ma_CanBo && _.Ma_Loai_KeKhai == 3 && _.TrangThai == true && _.MaKeHoachKeKhai == id) == 1)
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
                            where lkkhn.MaKeHoachKeKhai == id && cq.Ma_CoQuan_DonVi == maCoQuan
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
                                isKeKhai = (db.Nv_KeKhai_TSTN.Count(_ => _.Ma_CanBo == cb.Ma_CanBo && _.Ma_Loai_KeKhai == 4 && _.TrangThai == true && _.MaKeHoachKeKhai == id) == 1)
                            }).ToList();

                if (!string.IsNullOrEmpty(HoTen))
                {
                    data = data.Where(a => a.HoTen.ToUpper().Contains(HoTen.ToUpper())).ToList();
                }

                recordsTotal = data.Count();
                var data1 = data.Skip(skip).Take(pageSize).ToList();
                return Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data1 }, JsonRequestBehavior.AllowGet);
            }
            else if (KHKK.Ma_Loai_KeKhai == 5)
            {
                var data = (from cb in db.DM_CanBo
                            join cv in db.DM_ChucVu_ChucDanh on cb.Ma_ChucVu_ChucDanh equals cv.Ma_ChucVu_ChucDanh
                            join cq in db.DM_CoQuanDonVi on cb.Ma_CoQuan_DonVi equals cq.Ma_CoQuan_DonVi
                            join dskkhn_ct in db.NV_LapKeHoachKeKhai_DanhSachKeKhaiBoSung_ChiTiet on cb.Ma_CanBo equals dskkhn_ct.Ma_CanBo
                            join dskkhn in db.NV_LapKeHoachKeKhai_DanhSachKeKhaiBoSung on dskkhn_ct.MaKeHoachKeKhai_DanhSachKeKhaiBoSung_ID equals dskkhn.MaKeHoachKeKhai_DanhSachKeKhaiBoSung_ID
                            join lkkhn in db.NV_LapKeHoachKeKhai on dskkhn.MaKeHoachKeKhai equals lkkhn.MaKeHoachKeKhai
                            join tk in db.HT_TaiKhoan on cb.Ma_CanBo equals tk.Ma_CanBo
                            join ntk in db.HT_NhomTaiKhoan on tk.MaNhomTaiKhoan equals ntk.MaNhomTaiKhoan
                            where lkkhn.MaKeHoachKeKhai == id && cq.Ma_CoQuan_DonVi == maCoQuan
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
                                isKeKhai = (db.Nv_KeKhai_TSTN.Count(_ => _.Ma_CanBo == cb.Ma_CanBo && _.Ma_Loai_KeKhai == 5 && _.TrangThai == true && _.MaKeHoachKeKhai == id) == 1)
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
                            where lkkhn.MaKeHoachKeKhai == id && cq.Ma_CoQuan_DonVi == maCoQuan
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
                                isKeKhai = (db.Nv_KeKhai_TSTN.Count(_ => _.Ma_CanBo == cb.Ma_CanBo && _.Ma_Loai_KeKhai == 12 && _.TrangThai == true && _.MaKeHoachKeKhai == id) == 1)
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

        public JsonResult LoadData_NDD_CanBoKeKhaiHangNam()
        {
            var draw = Request.Form.GetValues("draw").FirstOrDefault();
            var start = Request.Form.GetValues("start").FirstOrDefault();
            var length = Request.Form.GetValues("length").FirstOrDefault();
            var HoTen = Request.Form.GetValues("columns[0][search][value]").FirstOrDefault();
            int pageSize = length != null ? Convert.ToInt32(length) : 0;
            int skip = start != null ? Convert.ToInt32(start) : 0;
            int recordsTotal = 0;

            var ID_KeHoachKeKhai = Int16.Parse(Request.Form.GetValues("ID_KeHoachKeKhai").FirstOrDefault());

            var LapKeHoachKeKhai = db.NV_LapKeHoachKeKhai.Find(ID_KeHoachKeKhai);

            var MaCanBo = 0;
            var Role = "null";
            var maCoQuan = 0;
            var search = Request.Form.GetValues("columns[1][search][value]").FirstOrDefault();
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
                        join tk in db.HT_TaiKhoan on cb.Ma_CanBo equals tk.Ma_CanBo
                        join ntk in db.HT_NhomTaiKhoan on tk.MaNhomTaiKhoan equals ntk.MaNhomTaiKhoan
                        where lkkhn.MaKeHoachKeKhai == ID_KeHoachKeKhai && cq.Ma_CoQuan_DonVi == maCoQuan
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
                            isKeKhai = (db.Nv_KeKhai_TSTN.Count(_ => _.Ma_CanBo == cb.Ma_CanBo && _.Ma_Loai_KeKhai == 4 && _.TrangThai == true && _.MaKeHoachKeKhai == ID_KeHoachKeKhai) == 1)
                        }).ToList();



            if (!string.IsNullOrEmpty(HoTen))
            {
                data = data.Where(a => a.HoTen.ToUpper().Contains(HoTen.ToUpper())).ToList();
            }

            recordsTotal = data.Count();
            var data1 = data.Skip(skip).Take(pageSize).ToList();
            return Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data1 }, JsonRequestBehavior.AllowGet);
        }

        public FileResult Download(int ID_BaoCaoTienDo)
        {
            var CTT = db.NV_BaoCaoTienDo.Single(_ => _.ID == ID_BaoCaoTienDo);
            var url = Path.Combine(Server.MapPath("~/Content/uploads"), CTT.TenFile);
            byte[] fileBytes = System.IO.File.ReadAllBytes(url);
            string fileName = CTT.TenFile;
            return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
        }

        public FileResult TaiFileBaoCaoMau()
        {

            var url = Path.Combine(Server.MapPath("~/Content/import"), "Mau BC ket qua trien khai cong tac KSTSTN.doc");
            byte[] fileBytes = System.IO.File.ReadAllBytes(url);
            string fileName = "Mau BC ket qua trien khai cong tac KSTSTN.doc";
            return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);

        }

        public JsonResult LoadDataKHKK()
        {
            var draw = Request.Form.GetValues("draw").FirstOrDefault();
            var start = Request.Form.GetValues("start").FirstOrDefault();
            var length = Request.Form.GetValues("length").FirstOrDefault();
            var search = Request.Form.GetValues("columns[0][search][value]").FirstOrDefault();
            var MaCoQuan = user.GetUserCoQuan();
            var role = user.GetRole();
            var list = new List<NV_LapKeHoachKeKhaiDS>();

            int pageSize = length != null ? Convert.ToInt32(length) : 0;
            int skip = start != null ? Convert.ToInt32(start) : 0;
            int recordsTotal = 0;

            List<NV_LapKeHoachKeKhai> data = new List<NV_LapKeHoachKeKhai>();
            if (role == "ADMIN")
            {
                data = db.NV_LapKeHoachKeKhai.Where(s=>s.NguoiTao != "NDDTTT").OrderByDescending(s => s.KeHoachNam).ToList();
            }
            else
            {
                data = db.NV_LapKeHoachKeKhai.Where(_ => _.Ma_CoQuan_DonVi == MaCoQuan && _.NguoiTao != "NDDTTT").OrderByDescending(_ => _.MaKeHoachKeKhai).OrderByDescending(_ => _.KeHoachNam).ToList();
            }

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
