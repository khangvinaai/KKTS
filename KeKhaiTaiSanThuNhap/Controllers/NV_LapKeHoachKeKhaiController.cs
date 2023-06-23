using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using KeKhaiTaiSanThuNhap.Models;

namespace KeKhaiTaiSanThuNhap.Controllers
{
    [Authorize(Roles = "ADMIN")]
    public class NV_LapKeHoachKeKhaiController : Controller
    {
        private KSTNEntities db = new KSTNEntities();
        private UserInfo user = new UserInfo();
        public ActionResult Index()
        {
            if (user.CheckQuyen("NV_LapKeHoachKeKhai", "Xem"))
            {
                return new HttpStatusCodeResult(404, "Not found");
            }
            ViewBag.Role = user.GetRole();
            return View();
        }

        [HttpPost]
        public JsonResult Create([Bind(Include = "MaKeHoachKeKhai,TenKeHoachKeKhai,KeHoachNam,ThoiGianBatDau,ThoiGianKetThuc,ThoiGianBatDauCongKhai,ThoiGianKetThucCongKhai,TrangThai,Ma_Loai_KeKhai,Ma_CoQuan_DonVi")] NV_LapKeHoachKeKhai nV_LapKeHoachKeKhai, List<HttpPostedFileBase> NghiDinh)
        {
            if (ModelState.IsValid)
            {

                nV_LapKeHoachKeKhai.Ma_CoQuan_DonVi = user.GetUserCoQuan();
                nV_LapKeHoachKeKhai.NguoiTao = user.GetRole();

                if (nV_LapKeHoachKeKhai.Ma_Loai_KeKhai == 4)
                {
                    var KKHN = db.NV_LapKeHoachKeKhai.Where(_ =>
                    _.KeHoachNam == nV_LapKeHoachKeKhai.KeHoachNam &&
                    _.Ma_CoQuan_DonVi == nV_LapKeHoachKeKhai.Ma_CoQuan_DonVi &&
                    _.Ma_Loai_KeKhai == 4 &&
                    _.NguoiTao != "NDDTTT"
                    ).ToList();
                    var CheckKeHoachP3 = db.NV_LapKeHoachKeKhai.Where(_ =>
                    _.KeHoachNam == nV_LapKeHoachKeKhai.KeHoachNam &&
                    _.Ma_Loai_KeKhai == 4 &&
                    _.NguoiTao == "NDDTTT"
                    ).ToList();
                    if (user.GetRole() == "NDDTTT" && CheckKeHoachP3.Count() == 0)
                    {
                        if (NghiDinh.Count() > 0)
                        {
                            foreach (HttpPostedFileBase file in NghiDinh)
                            {
                                if (file.ContentLength > 0)
                                {
                                    string _FileName = user.GetRandomPassword(6).ToLower() + "-" + Path.GetFileName(file.FileName);
                                    string _path = Path.Combine(Server.MapPath("~/Content/uploads"), _FileName);
                                    nV_LapKeHoachKeKhai.NghiDinh = _FileName;
                                    file.SaveAs(_path);
                                }
                            }

                        }


                        nV_LapKeHoachKeKhai.TrangThai = false;
                        db.NV_LapKeHoachKeKhai.Add(nV_LapKeHoachKeKhai);
                        db.SaveChanges();
                        if (user.GetRole() != "NDDTTT")
                        {
                            var hn = new NV_LapKeHoachKeKhai_DanhSachKeKhaiHangNam();
                            hn.TrangThai = false;
                            hn.Ma_CoQuan_DonVi = nV_LapKeHoachKeKhai.Ma_CoQuan_DonVi;
                            hn.MaKeHoachKeKhai = nV_LapKeHoachKeKhai.MaKeHoachKeKhai;
                            db.NV_LapKeHoachKeKhai_DanhSachKeKhaiHangNam.Add(hn);
                            db.SaveChanges();

                            var bs = new NV_LapKeHoachKeKhai_DanhSachKeKhaiBoSung();
                            bs.TrangThai = false;
                            bs.Ma_CoQuan_DonVi = nV_LapKeHoachKeKhai.Ma_CoQuan_DonVi;
                            bs.MaKeHoachKeKhai = nV_LapKeHoachKeKhai.MaKeHoachKeKhai;
                            db.NV_LapKeHoachKeKhai_DanhSachKeKhaiBoSung.Add(bs);
                            db.SaveChanges();
                        }
                        return Json(new { status = true, message = "Kế Hoạch Kê Khai Tài Sản, Thu Nhập Năm" + nV_LapKeHoachKeKhai.KeHoachNam + " Đã Được Lập" }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        if (CheckKeHoachP3.Count()
                            != 0 &&
                            KKHN.Count() == 0 &&
                            user.GetRole() != "NDDTTT"
                            )
                        {
                            if (NghiDinh.Count() > 0)
                            {
                                foreach (HttpPostedFileBase file in NghiDinh)
                                {
                                    if (file.ContentLength > 0)
                                    {
                                        string _FileName = user.GetRandomPassword(6).ToLower() + "-" + Path.GetFileName(file.FileName);
                                        string _path = Path.Combine(Server.MapPath("~/Content/uploads"), _FileName);
                                        nV_LapKeHoachKeKhai.NghiDinh = _FileName;
                                        file.SaveAs(_path);
                                    }
                                }

                            }

                            nV_LapKeHoachKeKhai.TrangThai = false;
                            db.NV_LapKeHoachKeKhai.Add(nV_LapKeHoachKeKhai);
                            db.SaveChanges();
                            if (user.GetRole() != "NDDTTT")
                            {
                                var hn = new NV_LapKeHoachKeKhai_DanhSachKeKhaiHangNam();
                                hn.TrangThai = false;
                                hn.Ma_CoQuan_DonVi = nV_LapKeHoachKeKhai.Ma_CoQuan_DonVi;
                                hn.MaKeHoachKeKhai = nV_LapKeHoachKeKhai.MaKeHoachKeKhai;
                                db.NV_LapKeHoachKeKhai_DanhSachKeKhaiHangNam.Add(hn);
                                db.SaveChanges();

                                var bs = new NV_LapKeHoachKeKhai_DanhSachKeKhaiBoSung();
                                bs.TrangThai = false;
                                bs.Ma_CoQuan_DonVi = nV_LapKeHoachKeKhai.Ma_CoQuan_DonVi;
                                bs.MaKeHoachKeKhai = nV_LapKeHoachKeKhai.MaKeHoachKeKhai;
                                db.NV_LapKeHoachKeKhai_DanhSachKeKhaiBoSung.Add(bs);
                                db.SaveChanges();
                            }


                            return Json(new { status = true, message = "Kế Hoạch Kê Khai Tài Sản, Thu Nhập Năm" + nV_LapKeHoachKeKhai.KeHoachNam + " Đã Được Lập" }, JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            return Json(new { status = false, message = "Kế Hoạch Kê Khai Tài Sản, Thu Nhập Năm " + nV_LapKeHoachKeKhai.KeHoachNam + " Đã Được Lập" }, JsonRequestBehavior.AllowGet);
                        }
                    }

                }
                else
                {
                    if (NghiDinh.Count() > 0)
                    {
                        foreach (HttpPostedFileBase file in NghiDinh)
                        {
                            if (file.ContentLength > 0)
                            {
                                string _FileName = user.GetRandomPassword(6).ToLower() + "-" + Path.GetFileName(file.FileName);
                                string _path = Path.Combine(Server.MapPath("~/Content/uploads"), _FileName);
                                nV_LapKeHoachKeKhai.NghiDinh = _FileName;
                                file.SaveAs(_path);
                            }
                        }
                    }

                    nV_LapKeHoachKeKhai.TrangThai = false;
                    db.NV_LapKeHoachKeKhai.Add(nV_LapKeHoachKeKhai);
                    db.SaveChanges();
                    if (user.GetRole() != "NDDTTT")
                    {
                        if (nV_LapKeHoachKeKhai.Ma_Loai_KeKhai == 3)
                        {
                            var ld = new NV_LapKeHoachKeKhai_DanhSachKeKhaiLanDau();
                            ld.TrangThai = false;
                            ld.Ma_CoQuan_DonVi = nV_LapKeHoachKeKhai.Ma_CoQuan_DonVi;
                            ld.MaKeHoachKeKhai = nV_LapKeHoachKeKhai.MaKeHoachKeKhai;
                            db.NV_LapKeHoachKeKhai_DanhSachKeKhaiLanDau.Add(ld);
                            db.SaveChanges();
                        }
                        else
                        {
                            var bnctcb = new NV_LapKeHoachKeKhai_DanhSachKeKhaiBoNhiemCongTacCanBo();
                            bnctcb.TrangThai = false;
                            bnctcb.Ma_CoQuan_DonVi = nV_LapKeHoachKeKhai.Ma_CoQuan_DonVi;
                            bnctcb.MaKeHoachKeKhai = nV_LapKeHoachKeKhai.MaKeHoachKeKhai;
                            db.NV_LapKeHoachKeKhai_DanhSachKeKhaiBoNhiemCongTacCanBo.Add(bnctcb);
                            db.SaveChanges();
                        }
                    }


                    return Json(new { status = true, message = "Kế Hoạch Kê Khai Tài Sản, Thu Nhập Năm " + nV_LapKeHoachKeKhai.KeHoachNam + " Đã Được Lập" }, JsonRequestBehavior.AllowGet);
                }
            }
            return Json(new { status = true, message = "Kế Hoạch Kê Khai Tài Sản, Thu Nhập Năm " + nV_LapKeHoachKeKhai.KeHoachNam + " Đã Được Lập" }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult LoadData()
        {
            var draw = Request.Form.GetValues("draw").FirstOrDefault();
            var start = Request.Form.GetValues("start").FirstOrDefault();
            var length = Request.Form.GetValues("length").FirstOrDefault();
            var search = Request.Form.GetValues("columns[0][search][value]").FirstOrDefault();
            var loaikehoach_search = Request.Form.GetValues("columns[1][search][value]").FirstOrDefault();
            var kehoachnam_search = Request.Form.GetValues("columns[2][search][value]").FirstOrDefault();
            var CoQuan_search = Request.Form.GetValues("columns[3][search][value]").FirstOrDefault();
            var MaCoQuan = user.GetUserCoQuan();

            var list = new List<NV_LapKeHoachKeKhaiDS>();

            int pageSize = length != null ? Convert.ToInt32(length) : 0;
            int skip = start != null ? Convert.ToInt32(start) : 0;
            int recordsTotal = 0;

            var Role = user.GetRole();

            var dt = db.NV_LapKeHoachKeKhai.Where(_ => _.Ma_CoQuan_DonVi == MaCoQuan || _.NguoiTao == "NDDTTT").OrderByDescending(_ => _.KeHoachNam).ToList();
            var data = (from lkhkk in db.NV_LapKeHoachKeKhai
                        join cq in db.DM_CoQuanDonVi on lkhkk.Ma_CoQuan_DonVi equals cq.Ma_CoQuan_DonVi
                        orderby lkhkk.KeHoachNam descending
                        select new { lkhkk.MaKeHoachKeKhai, lkhkk.TenKeHoachKeKhai, lkhkk.NghiDinh, lkhkk.ThoiGianBatDauCongKhai, lkhkk.Ma_Loai_KeKhai, lkhkk.ThoiGianBatDau, lkhkk.ThoiGianKetThuc, lkhkk.ThoiGianKetThucCongKhai, lkhkk.Ma_CoQuan_DonVi, cq.Ten, lkhkk.NguoiTao, lkhkk.TrangThai, lkhkk.TienDo, lkhkk.KeHoachNam }
                      ).ToList();

            if (Role != "NDDTTT" && Role != "ADMIN")
            {
                data = data.Where(_ => _.Ma_CoQuan_DonVi == MaCoQuan || _.NguoiTao == "NDDTTT").ToList();
            }

            if (!string.IsNullOrEmpty(search))
            {
                data = data.Where(a => a.TenKeHoachKeKhai.ToUpper().Contains(search.ToUpper()) || a.KeHoachNam.ToString().ToUpper().Contains(search.ToUpper())).ToList();
            }


            if (!string.IsNullOrEmpty(kehoachnam_search))
            {
                var kehoachnam_search_int = Int32.Parse(kehoachnam_search);
                data = data.Where(a => a.KeHoachNam == kehoachnam_search_int).ToList();
            }

            if (!string.IsNullOrEmpty(CoQuan_search))
            {
                var maCoQuan_search = Int32.Parse(CoQuan_search);
                if (maCoQuan_search != 0)
                {
                    data = data.Where(a => a.Ma_CoQuan_DonVi == maCoQuan_search).ToList();
                }

            }

            if (!string.IsNullOrEmpty(loaikehoach_search))
            {
                var loaikehoach_search_int = Int32.Parse(loaikehoach_search);
                if (loaikehoach_search_int == 0)
                {

                }
                else
                {
                    data = data.Where(a => a.Ma_Loai_KeKhai == loaikehoach_search_int).ToList();

                }
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
                khkkds.Ten = item.Ten;
                khkkds.NguoiTao = item.NguoiTao;
                if (item.Ma_Loai_KeKhai == 3)
                {
                    if (item.NguoiTao != "NDDTTT")
                    {
                        var dsld = db.NV_LapKeHoachKeKhai_DanhSachKeKhaiLanDau.Where(_ => _.MaKeHoachKeKhai == item.MaKeHoachKeKhai).First();
                        khkkds.TrangThaiDS = dsld.TrangThai;
                        khkkds.TrangThaiTonTaiDS = db.NV_LapKeHoachKeKhai_DanhSachKeKhaiLanDau_ChiTiet.Where(_ => _.MaKeHoachKeKhai_DanhSachKeKhaiLanDau_ID == dsld.MaKeHoachKeKhai_DanhSachKeKhaiLanDau_ID).Count();
                    }
                    else
                    {

                        khkkds.TrangThaiDS = true;
                        khkkds.TrangThaiTonTaiDS = 0;
                    }

                }
                else if (item.Ma_Loai_KeKhai == 4)
                {
                    if (item.NguoiTao != "NDDTTT")
                    {
                        var dshn = db.NV_LapKeHoachKeKhai_DanhSachKeKhaiHangNam.Where(_ => _.MaKeHoachKeKhai == item.MaKeHoachKeKhai).First();
                        khkkds.TrangThaiDS = dshn.TrangThai;
                        khkkds.TrangThaiTonTaiDS = db.NV_LapKeHoachKeKhai_DanhSachKeKhaiHangNam_ChiTiet.Where(_ => _.MaKeHoachKeKhai_DanhSachKeKhaiHangNam_ID == dshn.MaKeHoachKeKhai_DanhSachKeKhaiHangNam_ID).Count();
                    }
                    else
                    {
                        khkkds.TrangThaiDS = true;
                        khkkds.TrangThaiTonTaiDS = 0;
                    }

                }
                else if (item.Ma_Loai_KeKhai == 5)
                {
                    var dsbs = db.NV_LapKeHoachKeKhai_DanhSachKeKhaiBoSung.Where(_ => _.MaKeHoachKeKhai == item.MaKeHoachKeKhai).First();
                    khkkds.TrangThaiDS = dsbs.TrangThai;
                    khkkds.TrangThaiTonTaiDS = db.NV_LapKeHoachKeKhai_DanhSachKeKhaiBoSung_ChiTiet.Where(_ => _.MaKeHoachKeKhai_DanhSachKeKhaiBoSung_ID == dsbs.MaKeHoachKeKhai_DanhSachKeKhaiBoSung_ID).Count();
                }
                else
                {
                    if (item.NguoiTao != "NDDTTT")
                    {

                        var dsbnctcb = db.NV_LapKeHoachKeKhai_DanhSachKeKhaiBoNhiemCongTacCanBo.Where(_ => _.MaKeHoachKeKhai == item.MaKeHoachKeKhai).First();
                        khkkds.TrangThaiDS = dsbnctcb.TrangThai;
                        khkkds.TrangThaiTonTaiDS = db.NV_LapKeHoachKeKhai_DanhSachKeKhaiBoNhiemCongTacCanBo_ChiTiet.Where(_ => _.MaKeHoachKeKhai_DanhSachKeKhaiBoNhiemCongTacCanBo_ID == dsbnctcb.MaKeHoachKeKhai_DanhSachKeKhaiBoNhiemCongTacCanBo_ID).Count();
                    }
                    else
                    {
                        khkkds.TrangThaiDS = true;
                        khkkds.TrangThaiTonTaiDS = 0;
                    }

                }

                list.Add(khkkds);

            }


            return Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = list }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult LoadData_DSCanBoKeKhai()
        {
            var draw = Request.Form.GetValues("draw").FirstOrDefault();
            var start = Request.Form.GetValues("start").FirstOrDefault();
            var length = Request.Form.GetValues("length").FirstOrDefault();
            var search = Request.Form.GetValues("columns[0][search][value]").FirstOrDefault();
            var MaCoQuan = user.GetUserCoQuan();
            var Role = user.GetRole();
            var list = new List<NV_LapKeHoachKeKhaiDS>();

            int pageSize = length != null ? Convert.ToInt32(length) : 0;
            int skip = start != null ? Convert.ToInt32(start) : 0;
            int recordsTotal = 0;


            List<NV_LapKeHoachKeKhai> data = new List<NV_LapKeHoachKeKhai>();
            if (Role != "ADMIN")
            {
                data = db.NV_LapKeHoachKeKhai.Where(_ => _.Ma_CoQuan_DonVi == MaCoQuan && _.NguoiTao != "NDDTTT").OrderByDescending(_ => _.KeHoachNam).ToList();
            }
            else
            {
                data = db.NV_LapKeHoachKeKhai.Where(_ => _.NguoiTao != "NDDTTT").ToList();
            }
            //var dt = (from lkhkk in db.NV_LapKeHoachKeKhai
            //          join cq in db.DM_CoQuanDonVi on lkhkk.Ma_CoQuan_DonVi equals cq.Ma_CoQuan_DonVi
            //          orderby lkhkk.KeHoachNam
            //          select new { lkhkk.MaKeHoachKeKhai, lkhkk.TenKeHoachKeKhai, lkhkk.NghiDinh, lkhkk.ThoiGianBatDauCongKhai, lkhkk.Ma_Loai_KeKhai, lkhkk.ThoiGianBatDau, lkhkk.ThoiGianKetThuc, lkhkk.ThoiGianKetThucCongKhai, lkhkk.Ma_CoQuan_DonVi, lkhkk.NguoiTao, lkhkk.TrangThai, lkhkk.TienDo, lkhkk.KeHoachNam, cq.Ten }
            //          ).ToList();

            //if (Role != "ADMIN")
            //{
            //    dt = dt.Where(_ => _.Ma_CoQuan_DonVi == MaCoQuan || _.NguoiTao == "NDDTTT").ToList();
            //}

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
                }
                else if (item.Ma_Loai_KeKhai == 4)
                {
                    var dshn = db.NV_LapKeHoachKeKhai_DanhSachKeKhaiHangNam.Where(_ => _.MaKeHoachKeKhai == item.MaKeHoachKeKhai).First();
                    khkkds.TrangThaiDS = dshn.TrangThai;
                    khkkds.TrangThaiTonTaiDS = db.NV_LapKeHoachKeKhai_DanhSachKeKhaiHangNam_ChiTiet.Where(_ => _.MaKeHoachKeKhai_DanhSachKeKhaiHangNam_ID == dshn.MaKeHoachKeKhai_DanhSachKeKhaiHangNam_ID).Count();
                }
                else if (item.Ma_Loai_KeKhai == 5)
                {
                    var dsbs = db.NV_LapKeHoachKeKhai_DanhSachKeKhaiBoSung.Where(_ => _.MaKeHoachKeKhai == item.MaKeHoachKeKhai).First();
                    khkkds.TrangThaiDS = dsbs.TrangThai;
                    khkkds.TrangThaiTonTaiDS = db.NV_LapKeHoachKeKhai_DanhSachKeKhaiBoSung_ChiTiet.Where(_ => _.MaKeHoachKeKhai_DanhSachKeKhaiBoSung_ID == dsbs.MaKeHoachKeKhai_DanhSachKeKhaiBoSung_ID).Count();
                }
                else
                {
                    var dsbnctcb = db.NV_LapKeHoachKeKhai_DanhSachKeKhaiBoNhiemCongTacCanBo.Where(_ => _.MaKeHoachKeKhai == item.MaKeHoachKeKhai).First();
                    khkkds.TrangThaiDS = dsbnctcb.TrangThai;
                    khkkds.TrangThaiTonTaiDS = db.NV_LapKeHoachKeKhai_DanhSachKeKhaiBoNhiemCongTacCanBo_ChiTiet.Where(_ => _.MaKeHoachKeKhai_DanhSachKeKhaiBoNhiemCongTacCanBo_ID == dsbnctcb.MaKeHoachKeKhai_DanhSachKeKhaiBoNhiemCongTacCanBo_ID).Count();
                }

                list.Add(khkkds);

            }


            return Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = list }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult LoadDataDetail(int id)
        {
            var dt = db.NV_LapKeHoachKeKhai.Find(id);

            var data = (from lkhkk in db.NV_LapKeHoachKeKhai
                        where lkhkk.MaKeHoachKeKhai == id || (lkhkk.NguoiTao == "NDDTTT" && lkhkk.KeHoachNam == dt.KeHoachNam && lkhkk.Ma_Loai_KeKhai == dt.Ma_Loai_KeKhai)
                        select lkhkk
                        ).ToList();
            return Json(dt, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetKeHoachKeKhai()
        {
            var data = db.NV_LapKeHoachKeKhai.ToList();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public FileResult Download(int id)
        {
            var CTT = db.NV_LapKeHoachKeKhai.Single(_ => _.MaKeHoachKeKhai == id);
            var url = Path.Combine(Server.MapPath("~/Content/uploads"), CTT.NghiDinh);
            byte[] fileBytes = System.IO.File.ReadAllBytes(url);
            string fileName = CTT.NghiDinh;
            return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
        }

        public JsonResult CheckLoaiKeHoachKeKhai(int KeHoachNam, int LoaiKeKhai)
        {
            var mess = new MessageLapKeHoachKeKhai();
            var Ma_CoQuan_DonVi = user.GetUserCoQuan();
            var Ma_NhomTaiKhoan = user.GetRole();

            if (Ma_NhomTaiKhoan == "NDDTTT")
            {
                mess.type = "success";
            }
            else
            {
                var data = db.NV_LapKeHoachKeKhai.Where(_ => _.NguoiTao == "NDDTTT" && _.Ma_Loai_KeKhai == LoaiKeKhai);


                if (data.Count() == 0)
                {
                    mess.type = "error";
                    mess.message = "Thanh Tra Tỉnh Chưa Lập Kế Hoạch Kê Khai Tài Sản " + KeHoachNam;
                }
                else
                {
                    if (LoaiKeKhai == 4)
                    {
                        data = data.Where(_ => _.KeHoachNam == KeHoachNam);
                        if (data.Count() == 0)
                        {
                            mess.type = "error";
                            mess.message = "Thanh Tra Tỉnh Chưa Lập Kế Hoạch Kê Khai Tài Sản, Thu Nhập Hằng Năm " + KeHoachNam;
                        }
                        else
                        {
                            var KHTT = data.First();
                            mess.type = "warning";
                            mess.message = "Vui lòng lập kế hoạch có thời gian kê khai từ " + Convert.ToDateTime(KHTT.ThoiGianBatDau).ToString("dd/MM/yyyy") + " đến " + Convert.ToDateTime(KHTT.ThoiGianKetThuc).ToString("dd/MM/yyyy");
                            mess.thoigianbatdau = Convert.ToDateTime(KHTT.ThoiGianBatDau).ToString("yyyy-MM-dd");
                            mess.thoigianketthuc = Convert.ToDateTime(KHTT.ThoiGianKetThuc).ToString("yyyy-MM-dd");
                            mess.file = KHTT.NghiDinh;
                        }

                    }
                    else if (LoaiKeKhai == 3)
                    {
                        var KHTT = data.First();
                        mess.type = "warning";
                        mess.message = "Vui lòng lập kế hoạch có thời gian kê khai từ " + Convert.ToDateTime(KHTT.ThoiGianBatDau).ToString("dd/MM/yyyy");
                        mess.thoigianbatdau = Convert.ToDateTime(KHTT.ThoiGianBatDau).ToString("yyyy-MM-dd");
                        mess.thoigianketthuc = Convert.ToDateTime(KHTT.ThoiGianKetThuc).ToString("yyyy-MM-dd");
                        mess.file = KHTT.NghiDinh;
                    }
                    else
                    {
                    }
                }
            }
            return Json(mess, JsonRequestBehavior.AllowGet);

        }


        public JsonResult KeHoachKeKhaiThanhTraTinh(int KeHoachNam, int LoaiKeKhai)
        {
            var mess = new MessageLapKeHoachKeKhai();
            var Ma_CoQuan_DonVi = user.GetUserCoQuan();
            var Ma_NhomTaiKhoan = user.GetRole();


            var data = db.NV_LapKeHoachKeKhai.Where(_ => _.NguoiTao == "NDDTTT" && _.Ma_Loai_KeKhai == LoaiKeKhai);


            if (data.Count() == 0)
            {
                mess.type = "error";
                mess.message = "Thanh Tra Tỉnh Chưa Lập Kế Hoạch Kê Khai Tài Sản " + KeHoachNam;
            }
            else
            {
                if (LoaiKeKhai == 4)
                {
                    data = data.Where(_ => _.KeHoachNam == KeHoachNam);
                    if (data.Count() == 0)
                    {
                        mess.type = "error";
                        mess.message = "Thanh Tra Tỉnh Chưa Lập Kế Hoạch Kê Khai Tài Sản, Thu Nhập Hằng Năm " + KeHoachNam;
                    }
                    else
                    {
                        var KHTT = data.First();
                        mess.type = "warning";
                        mess.message = "Vui lòng lập kế hoạch có thời gian kê khai từ " + Convert.ToDateTime(KHTT.ThoiGianBatDau).ToString("dd/MM/yyyy") + " đến " + Convert.ToDateTime(KHTT.ThoiGianKetThuc).ToString("dd/MM/yyyy");
                        mess.thoigianbatdau = Convert.ToDateTime(KHTT.ThoiGianBatDau).ToString("yyyy-MM-dd");
                        mess.thoigianketthuc = Convert.ToDateTime(KHTT.ThoiGianKetThuc).ToString("yyyy-MM-dd");
                        mess.file = KHTT.NghiDinh;
                    }

                }
                else if (LoaiKeKhai == 3)
                {
                    var KHTT = data.First();
                    mess.type = "warning";
                    mess.message = "Vui lòng lập kế hoạch có thời gian kê khai từ " + Convert.ToDateTime(KHTT.ThoiGianBatDau).ToString("dd/MM/yyyy");
                    mess.thoigianbatdau = Convert.ToDateTime(KHTT.ThoiGianBatDau).ToString("yyyy-MM-dd");
                    mess.thoigianketthuc = Convert.ToDateTime(KHTT.ThoiGianKetThuc).ToString("yyyy-MM-dd");
                    mess.file = KHTT.NghiDinh;
                }
                else
                {
                }
            }

            return Json(mess, JsonRequestBehavior.AllowGet);

        }


        public class MessageLapKeHoachKeKhai
        {
            public string type { get; set; }
            public string thoigianbatdau { get; set; }
            public string thoigianketthuc { get; set; }

            public string message { get; set; }

            public string file { get; set; }
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
            public string Ten { get; set; }
            public string NguoiTao { get; set; }
        }
    }
}
