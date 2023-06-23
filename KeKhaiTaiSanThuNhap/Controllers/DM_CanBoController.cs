using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using ExcelDataReader;
using KeKhaiTaiSanThuNhap.Models;
using OfficeOpenXml;
using OfficeOpenXml.Style;

namespace KeKhaiTaiSanThuNhap.Controllers
{
    [Authorize(Roles = "ADMIN")]
    public class DM_CanBoController : Controller
    {
        private KSTNEntities db = new KSTNEntities();
        private UserInfo user = new UserInfo();
        public ActionResult Index()
        {
            if (user.CheckQuyen("DM_CanBo", "Xem"))
            {
                return new HttpStatusCodeResult(404, "Not found");
            }

            return View();
        }

        
        public ActionResult Edit(int? id)
        {
            var data = db.DM_CanBo.Where(_ => _.Ma_CanBo == id).FirstOrDefault();

            if (data == null)
            {
                return new HttpStatusCodeResult(404, "Not found");
            }
 
            if(user.GetUser() != id)
            {
                return new HttpStatusCodeResult(404, "Not found");
            }
            else
            {
                return View();
            }             
        }
        public ActionResult EditIndex([Bind(Include = "Ma_CanBo,HoTen,DoB,DiaChiThuongTru,SoCCCD,NgayCap,NoiCap,Ma_CoQuan_DonVi,DiaChi_LienHe,Ma_PhuongXa_LH,Ma_ChucVu_ChucDanh, Email")] DM_CanBo dM_CanBo, [Bind(Include = "ID,Ma_CanBo,TenTaiKhoan,MatKhau,MaNhomTaiKhoan")] HT_TaiKhoan hT_TaiKhoan)
        {
            var coQuanCu = db.DM_CanBo.Where(_ => _.Ma_CanBo == dM_CanBo.Ma_CanBo).Select(_ => _.Ma_CoQuan_DonVi).FirstOrDefault();
            var checkDaLapDanhSachKeKhaiHangNam = (from cb in db.DM_CanBo
                                                   join dscbkkHN_ct in db.NV_LapKeHoachKeKhai_DanhSachKeKhaiHangNam_ChiTiet on cb.Ma_CanBo equals dscbkkHN_ct.Ma_CanBo
                                                   join dscbkkHN in db.NV_LapKeHoachKeKhai_DanhSachKeKhaiHangNam on dscbkkHN_ct.MaKeHoachKeKhai_DanhSachKeKhaiHangNam_ID equals dscbkkHN.MaKeHoachKeKhai_DanhSachKeKhaiHangNam_ID
                                                   join khkk in db.NV_LapKeHoachKeKhai on dscbkkHN.MaKeHoachKeKhai equals khkk.MaKeHoachKeKhai
                                                   where cb.Ma_CanBo == dM_CanBo.Ma_CanBo && cb.Ma_CoQuan_DonVi == coQuanCu && khkk.KeHoachNam >= DateTime.Now.Year && khkk.Ma_Loai_KeKhai == 4
                                                   select cb.Ma_CanBo).Count();
            var checkDaLapDanhSachKeKhaiLanDau = (from cb in db.DM_CanBo
                                                    join dscbkkLD_ct in db.NV_LapKeHoachKeKhai_DanhSachKeKhaiLanDau_ChiTiet on cb.Ma_CanBo equals dscbkkLD_ct.Ma_CanBo
                                                    join dscbkkLD in db.NV_LapKeHoachKeKhai_DanhSachKeKhaiLanDau on dscbkkLD_ct.MaKeHoachKeKhai_DanhSachKeKhaiLanDau_ID equals dscbkkLD.MaKeHoachKeKhai_DanhSachKeKhaiLanDau_ID
                                                    join khkk in db.NV_LapKeHoachKeKhai on dscbkkLD.MaKeHoachKeKhai equals khkk.MaKeHoachKeKhai
                                                    where cb.Ma_CanBo == dM_CanBo.Ma_CanBo && cb.Ma_CoQuan_DonVi == coQuanCu && khkk.KeHoachNam >= DateTime.Now.Year && khkk.Ma_Loai_KeKhai == 3
                                                  select cb.Ma_CanBo).Count();
            var checkDaLapDanhSachKeKhaiBoSung = (from cb in db.DM_CanBo
                                                  join dscbkkBS_ct in db.NV_LapKeHoachKeKhai_DanhSachKeKhaiBoSung_ChiTiet on cb.Ma_CanBo equals dscbkkBS_ct.Ma_CanBo
                                                  join dscbkkBS in db.NV_LapKeHoachKeKhai_DanhSachKeKhaiBoSung on dscbkkBS_ct.MaKeHoachKeKhai_DanhSachKeKhaiBoSung_ID equals dscbkkBS.MaKeHoachKeKhai_DanhSachKeKhaiBoSung_ID
                                                  join khkk in db.NV_LapKeHoachKeKhai on dscbkkBS.MaKeHoachKeKhai equals khkk.MaKeHoachKeKhai
                                                  where cb.Ma_CanBo == dM_CanBo.Ma_CanBo && cb.Ma_CoQuan_DonVi == coQuanCu && khkk.KeHoachNam >= DateTime.Now.Year &&  khkk.Ma_Loai_KeKhai == 5
                                                  select cb.Ma_CanBo).Count();
            var checkDaLapDanhSachKeKhaiBNCTCB = (from cb in db.DM_CanBo
                                                  join dscbkkBNCTCB_ct in db.NV_LapKeHoachKeKhai_DanhSachKeKhaiBoNhiemCongTacCanBo_ChiTiet on cb.Ma_CanBo equals dscbkkBNCTCB_ct.Ma_CanBo
                                                  join dscbkkBNCTCB in db.NV_LapKeHoachKeKhai_DanhSachKeKhaiBoNhiemCongTacCanBo on dscbkkBNCTCB_ct.MaKeHoachKeKhai_DanhSachKeKhaiBoNhiemCongTacCanBo_ID equals dscbkkBNCTCB.MaKeHoachKeKhai_DanhSachKeKhaiBoNhiemCongTacCanBo_ID
                                                  join khkk in db.NV_LapKeHoachKeKhai on dscbkkBNCTCB.MaKeHoachKeKhai equals khkk.MaKeHoachKeKhai
                                                  where cb.Ma_CanBo == dM_CanBo.Ma_CanBo && cb.Ma_CoQuan_DonVi == coQuanCu && khkk.KeHoachNam >= DateTime.Now.Year && khkk.Ma_Loai_KeKhai == 12
                                                  select cb.Ma_CanBo).Count();

            if(checkDaLapDanhSachKeKhaiLanDau > 0 || checkDaLapDanhSachKeKhaiHangNam > 0 || checkDaLapDanhSachKeKhaiBoSung > 0 || checkDaLapDanhSachKeKhaiBNCTCB > 0)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
            else
            {
                dM_CanBo.DoB = Convert.ToDateTime(dM_CanBo.DoB).ToString("dd/MM/yyyy");
                dM_CanBo.NgayCap = Convert.ToDateTime(dM_CanBo.NgayCap).ToString("dd/MM/yyyy");
                dM_CanBo.Ten = dM_CanBo.HoTen.Substring(dM_CanBo.HoTen.LastIndexOf(" ") + 1);
                dM_CanBo.IsCapNhat = false;
                dM_CanBo.IsActive = true;
                db.Entry(dM_CanBo).State = EntityState.Modified;
                db.SaveChanges();


                hT_TaiKhoan.Ma_CanBo = dM_CanBo.Ma_CanBo;
                hT_TaiKhoan.NgaySua = DateTime.Now;
                hT_TaiKhoan.NguoiSua = user.GetUserNameAccount();
                hT_TaiKhoan.TrangThai = true;
                db.Entry(hT_TaiKhoan).State = EntityState.Modified;
                db.SaveChanges();
                return Json(dM_CanBo.HoTen, JsonRequestBehavior.AllowGet);
            }
            
        }
        [HttpPost]
        public JsonResult Create([Bind(Include = "Ma_CanBo,HoTen,DoB,DiaChiThuongTru,SoCCCD,NgayCap,NoiCap,Ma_CoQuan_DonVi,DiaChi_LienHe,Ma_PhuongXa_LH,Ma_ChucVu_ChucDanh, Email")] DM_CanBo dM_CanBo, [Bind(Include = "ID,MaTaiKhoan,Ma_CanBo,TenTaiKhoan,MatKhau,MaNhomTaiKhoan,TenNhomTaiKhoan,TrangThai,NguoiTao,NgayTao,NguoiSua,NgaySua")] HT_TaiKhoan hT_TaiKhoan)
        {
            dM_CanBo.DoB = Convert.ToDateTime(dM_CanBo.DoB).ToString("dd/MM/yyyy");
            dM_CanBo.NgayCap = Convert.ToDateTime(dM_CanBo.NgayCap).ToString("dd/MM/yyyy");
            dM_CanBo.Ten = dM_CanBo.HoTen.Substring(dM_CanBo.HoTen.LastIndexOf(" ") + 1);
            dM_CanBo.IsCapNhat = false;
            dM_CanBo.IsActive = true;
            db.DM_CanBo.Add(dM_CanBo);
            db.SaveChanges();

            hT_TaiKhoan.Ma_CanBo = dM_CanBo.Ma_CanBo;
            hT_TaiKhoan.NgaySua = DateTime.Now;
            hT_TaiKhoan.NgayTao = DateTime.Now;
            hT_TaiKhoan.NguoiTao = user.GetUserNameAccount();
            hT_TaiKhoan.NguoiSua = user.GetUserNameAccount();
            hT_TaiKhoan.CheckPass = false;
            hT_TaiKhoan.TrangThai = true;
            db.HT_TaiKhoan.Add(hT_TaiKhoan);
            db.SaveChanges();

            return Json(dM_CanBo.HoTen, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Edit([Bind(Include = "Ma_CanBo,HoTen,DoB,DiaChiThuongTru,SoCCCD,NgayCap,NoiCap,Ma_CoQuan_DonVi,DiaChi_LienHe,Ma_PhuongXa_LH,Ma_ChucVu_ChucDanh, Email")] DM_CanBo dM_CanBo, List<int> Ma_CanBo_ThanNhan, List<string> HoTenThanNhan, List<DateTime> DoBTN, List<string> DiaChiThuongTruTN, List<int> Ma_TinhThanhTN, List<int> Ma_QuanHuyenTN, List<int> Ma_PhuongXa_TTTN, List<string> SoCCCDTN, List<DateTime> NgayCapTN, List<string> NoiCapTN, List<string> VaiTroThanNhan, List<string> NgheNghiep, List<string> NoiLamViec)
        {

            var cbth = db.DM_CanBo_ThanNhan.Where(_ => _.Ma_CanBo == dM_CanBo.Ma_CanBo);
            foreach(var i in cbth)
            {
                db.DM_CanBo_ThanNhan.Remove(i);
                
            }
            db.SaveChanges();

            dM_CanBo.DoB = Convert.ToDateTime(dM_CanBo.DoB).ToString("dd/MM/yyyy");
            dM_CanBo.NgayCap = Convert.ToDateTime(dM_CanBo.NgayCap).ToString("dd/MM/yyyy");
            dM_CanBo.Ten = dM_CanBo.HoTen.Substring(dM_CanBo.HoTen.LastIndexOf(" ") + 1);
            dM_CanBo.IsCapNhat = true;
            dM_CanBo.IsActive = true;
            db.Entry(dM_CanBo).State = EntityState.Modified;
            db.SaveChanges();

            for (int i = 0; i < HoTenThanNhan.Count(); i++)
            {
                
                if (HoTenThanNhan[i] != "")
                {
                    var tn = new DM_CanBo_ThanNhan();
                    tn.Ma_CanBo = dM_CanBo.Ma_CanBo;
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

              return Json(true, JsonRequestBehavior.AllowGet);
          
        }

        [HttpPost]
        public JsonResult LoadData()
        {
            var Role = user.GetRole();
            var maCoQuan = user.GetUserCoQuan();
            var TaiKhoan = db.HT_TaiKhoan.Select(_ => _.Ma_CanBo);
            var draw = Request.Form.GetValues("draw").FirstOrDefault();
            var start = Request.Form.GetValues("start").FirstOrDefault();
            var length = Request.Form.GetValues("length").FirstOrDefault();
            var TenCanBo = Request.Form.GetValues("columns[0][search][value]").FirstOrDefault();
            int pageSize = length != null ? Convert.ToInt32(length) : 0;
            int skip = start != null ? Convert.ToInt32(start) : 0;
            int recordsTotal = 0;

            var data = (from cb in db.DM_CanBo
                        join cv in db.DM_ChucVu_ChucDanh on cb.Ma_ChucVu_ChucDanh equals cv.Ma_ChucVu_ChucDanh
                        join cq in db.DM_CoQuanDonVi on cb.Ma_CoQuan_DonVi equals cq.Ma_CoQuan_DonVi
                        join lcq in db.DM_Loai_CoQuan_DonVi on cq.MaLoai_CoQuan_DonVi equals lcq.Ma_Loai_CQDV
                        join tk in db.HT_TaiKhoan on cb.Ma_CanBo equals tk.Ma_CanBo
                        join ntk in db.HT_NhomTaiKhoan on tk.MaNhomTaiKhoan equals ntk.MaNhomTaiKhoan
                        orderby  ntk.Sort, cq.Ten, cv.Ten_ChucVu_ChucDanh, cb.Ten
                        where cb.IsActive == true && lcq.Ma_Loai_CQDV != 35 
                        select new { cb.Ma_CanBo, cb.HoTen, TenCanBo = cb.Ten, cb.Email, cb.DoB, cb.SoCCCD, cb.DiaChiThuongTru, cv.Ten_ChucVu_ChucDanh, ntk.MaTaiKhoan , cq.Ten, cq.Ma_CoQuan_DonVi, TK = TaiKhoan.Contains(cb.Ma_CanBo) })
                       .ToList();

            if (Role != "ADMIN" && Role != "NDDTTT")
            {
                data = data.Where(_ => _.Ma_CoQuan_DonVi == maCoQuan && _.MaTaiKhoan != "NDDTTT").ToList();
            }

            if (!string.IsNullOrEmpty(TenCanBo))
            {
                data = data.Where(a => a.HoTen.ToUpper().Contains(TenCanBo.ToUpper())).ToList();
            }

            recordsTotal = data.Count();
            var data1 = data.Skip(skip).Take(pageSize).ToList();
            return Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data1 }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost, ActionName("delete")]
        public JsonResult DeleteConfirmed(int id)
        {
            DM_CanBo dM_CanBo = db.DM_CanBo.Find(id);
            dM_CanBo.IsActive = false;
            db.Entry(dM_CanBo).State = EntityState.Modified;
            db.SaveChanges();

            return Json(dM_CanBo, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetSuaCanBo(int id)
        {
            var data = (from user in db.DM_CanBo
                        join tk in db.HT_TaiKhoan on user.Ma_CanBo equals tk.Ma_CanBo
                        join ntk in db.HT_NhomTaiKhoan on tk.MaNhomTaiKhoan equals ntk.MaNhomTaiKhoan
                        where user.Ma_CanBo == id && user.IsActive == true

                        select new
                        {
                            HoTen = user.HoTen,
                            DoB = user.DoB,
                            Email = user.Email,
                            DiaChiThuongTru = user.DiaChiThuongTru,
                            SoCCCD = user.SoCCCD,
                            NgayCap = user.NgayCap,
                            NoiCap = user.NoiCap,
                            Ma_CoQuan_DonVi = user.Ma_CoQuan_DonVi,
                            Ma_ChucVu_ChucDanh = user.Ma_ChucVu_ChucDanh,
                            Ma_CanBo = user.Ma_CanBo,
                            MaNhomTaiKhoan = ntk.MaNhomTaiKhoan,
                            ID_TaiKhoan = tk.ID,
                            TenTaiKhoan = tk.TenTaiKhoan,
                            MatKhau = tk.MatKhau
                        }).Single();

            var data1 = (from tn in db.DM_CanBo_ThanNhan
                         where tn.Ma_CanBo == id

                         select new
                         {
                             Ma_CanBo_ThanNhan = tn.Ma_CanBo_ThanNhan,
                             HoTenThanNhan = tn.HoTen,

                             DoBTN = tn.DoB,
                             DiaChiThuongTruTN = tn.DiaChiThuongTru,
                             SoCCCDTN = tn.SoCCCD,
                             NgayCapTN = tn.NgayCap,
                             NoiCapTN = tn.NoiCap,
                             VaiTroThanNhan = tn.VaiTroThanNhan,
                             NoiLamViec = tn.NoiLamViec,
                             NgheNghiep = tn.NgheNghiep
                         }).ToList();

            var data2 = new
            {
                nguoikekhai = data,
                thannhan = data1
            };


            return Json(data2, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetCanBo()
        {
            var data = (from cb in db.DM_CanBo
                        join cq in db.DM_CoQuanDonVi on cb.Ma_CoQuan_DonVi equals cq.Ma_CoQuan_DonVi
                        join cv in db.DM_ChucVu_ChucDanh on cb.Ma_ChucVu_ChucDanh equals cv.Ma_ChucVu_ChucDanh
                        where cb.IsActive == true
                        select new { cb.Ma_CanBo, cb.HoTen, cb.DoB, cb.Email, cb.SoCCCD, cb.NgayCap, cb.NoiCap, cb.DiaChiThuongTru, cq.Ten, cq.Ma_CoQuan_DonVi, cv.Ten_ChucVu_ChucDanh, cv.Ma_ChucVu_ChucDanh }).ToList();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetCanBoByCoQuan(int MaCoQuan)
        {
            var data = (from cb in db.DM_CanBo
                        join cq in db.DM_CoQuanDonVi on cb.Ma_CoQuan_DonVi equals cq.Ma_CoQuan_DonVi
                        join cv in db.DM_ChucVu_ChucDanh on cb.Ma_ChucVu_ChucDanh equals cv.Ma_ChucVu_ChucDanh
                        where cb.Ma_CoQuan_DonVi == MaCoQuan && cb.IsActive == true
                        select new { cb.Ma_CanBo, cb.HoTen, cb.DoB, cb.Email, cb.SoCCCD, cb.NgayCap, cb.NoiCap, cb.DiaChiThuongTru, cq.Ten, cq.Ma_CoQuan_DonVi, cv.Ten_ChucVu_ChucDanh, cv.Ma_ChucVu_ChucDanh }).ToList();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetCanBoTaiKhoan(int MaCoQuan)
        {
            var data1 = db.HT_TaiKhoan.Select(_ => _.Ma_CanBo);

            var data = (from cb in db.DM_CanBo
                        where !data1.Contains(cb.Ma_CanBo) && cb.Ma_CoQuan_DonVi == MaCoQuan
                        join cv in db.DM_ChucVu_ChucDanh on cb.Ma_ChucVu_ChucDanh equals cv.Ma_ChucVu_ChucDanh
                        orderby cv.Ten_ChucVu_ChucDanh, cb.HoTen
                        where cb.IsActive == true
                        select new { TenCanBo = cb.HoTen, cb.Email, MaCanBo = cb.Ma_CanBo, TenChucVu = cv.Ten_ChucVu_ChucDanh }).ToList();

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        
        public JsonResult GetCanBoByID(int MaCanBo)
        {
            var data = (from cb in db.DM_CanBo
                        join cq in db.DM_CoQuanDonVi on cb.Ma_CoQuan_DonVi equals cq.Ma_CoQuan_DonVi
                        join cv in db.DM_ChucVu_ChucDanh on cb.Ma_ChucVu_ChucDanh equals cv.Ma_ChucVu_ChucDanh
                        join tk in db.HT_TaiKhoan on cb.Ma_CanBo equals tk.Ma_CanBo
                        where cb.Ma_CanBo == MaCanBo && cb.IsActive == true
                        select new { TenCanBo = cb.HoTen, cb.Email, MaCanBo = cb.Ma_CanBo, TenChucVu = cv.Ten_ChucVu_ChucDanh, tenCoQuanDonVi = cq.Ten, MaCoQuanDonVi = cq.Ma_CoQuan_DonVi }).FirstOrDefault();

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetCanBoByIdCookei()
        {
            var idCanBo = 0;
            HttpCookie authCookie = System.Web.HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName];
            if (authCookie != null)
            {
                if (!string.IsNullOrEmpty(authCookie.Value))
                {
                    FormsAuthenticationTicket authTicket = FormsAuthentication.Decrypt(authCookie.Value);
                    string str = authTicket.UserData;
                    string[] subs = str.Split(',');
                    idCanBo = Int32.Parse(subs[0]);
                }
            }


            var data = db.DM_CanBo.Where(_ => _.Ma_CanBo == idCanBo && _.IsActive == true).ToList();


            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetCanBoKeHoach()
        {
            var data = (from cb in db.DM_CanBo
                        join cq in db.DM_CoQuanDonVi on cb.Ma_CoQuan_DonVi equals cq.Ma_CoQuan_DonVi
                        orderby cq.Ten, cb.HoTen
                        where cb.IsActive == true
                        select new { MaCanBo = cb.Ma_CanBo, cb.Email, TenCanBo = cb.HoTen, TenCoQuan = cq.Ten });
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetThongTinCanBoKeHoach(int id)
        {
            var data = (from cb in db.DM_CanBo
                        join cq in db.DM_CoQuanDonVi on cb.Ma_CoQuan_DonVi equals cq.Ma_CoQuan_DonVi
                        where cb.Ma_CanBo == id && cb.IsActive == true
                        select new { MaCanBo = cb.Ma_CanBo, cb.Email, HoTenCanBo = cb.HoTen, NgaySinh = cb.DoB, CCCD = cb.SoCCCD, DiaChi = cb.DiaChiThuongTru, TenCoQuan = cq.Ten }).ToList();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetImportDanhSachCanBo()
        {
            var socoquancanxacminh = Math.Round((double)(db.DM_CoQuanDonVi.Count() * 0.2));

            if (socoquancanxacminh == 0 && db.DM_CoQuanDonVi.Count() != 0) socoquancanxacminh = 1;

            var listMaCQDV = db.DM_CoQuanDonVi.OrderBy(n => Guid.NewGuid()).Take(Int32.Parse(socoquancanxacminh.ToString())).Select(_ => _.Ma_CoQuan_DonVi).ToList();
            IEnumerable<DM_CanBo> listcanbo = new List<DM_CanBo>();


            foreach (var item in listMaCQDV)
            {
                var soluongcanbocanlay = Math.Round(db.DM_CanBo.Where(_ => _.Ma_CoQuan_DonVi == item).Count() * 0.1);
                if (soluongcanbocanlay == 0 && db.DM_CanBo.Count() != 0) soluongcanbocanlay = 1;

                var idnguoidungdau = db.DM_CanBo.Where(_ => _.Ma_CoQuan_DonVi == item && (_.Ma_ChucVu_ChucDanh == 1 || _.Ma_ChucVu_ChucDanh == 6)).OrderBy(n => Guid.NewGuid()).Take(Int32.Parse(1.ToString())).Select(_ => _.Ma_CanBo).SingleOrDefault();
                var datanguoidungdau = db.DM_CanBo.Where(_ => _.Ma_CanBo == idnguoidungdau);
                listcanbo = listcanbo.Concat(datanguoidungdau);
                soluongcanbocanlay = soluongcanbocanlay - 1;
                var data = db.DM_CanBo.Where(_ => _.Ma_CoQuan_DonVi == item && _.Ma_CanBo != idnguoidungdau).OrderBy(n => Guid.NewGuid()).Take(Int32.Parse(soluongcanbocanlay.ToString())).ToList();
                listcanbo = listcanbo.Concat(data);
            }


            var data1 = (from cb in listcanbo
                         join cq in db.DM_CoQuanDonVi on cb.Ma_CoQuan_DonVi equals cq.Ma_CoQuan_DonVi
                         join cv in db.DM_ChucVu_ChucDanh on cb.Ma_ChucVu_ChucDanh equals cv.Ma_ChucVu_ChucDanh
                         orderby cb.Ma_ChucVu_ChucDanh descending 
                         select new { MaCanBo = cb.Ma_CanBo, cb.Email, HoTenCanBo = cb.HoTen, NgaySinh = cb.DoB, CCCD = cb.SoCCCD, DiaChi = cb.DiaChiThuongTru , TenCoQuan = cq.Ten, ChucVu = cv.Ten_ChucVu_ChucDanh }).ToList();

            return Json(data1, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ImportFileExcel(HttpPostedFileBase excel)
        {
            var Role = user.GetRole();
            var UserID = user.GetUser();
            var UserName = db.DM_CanBo.Where(_ => _.Ma_CanBo == UserID).FirstOrDefault().HoTen;
            string filename = System.Guid.NewGuid().ToString() + ".xlsx";
            string targetpath = Server.MapPath("~/Content/uploads/");
            excel.SaveAs(targetpath + filename);

            string pathToExcelFile = targetpath + filename;
            string TenFileKetQua = "";
            var success = 0;
            var fail = 0;

            if (Role.ToString().ToUpper() == "ADMIN")
            {
                using (var stream = System.IO.File.Open(pathToExcelFile, FileMode.Open, FileAccess.Read))
                {
                    var reader = ExcelReaderFactory.CreateReader(stream);
                    var result = reader.AsDataSet(new ExcelDataSetConfiguration
                    {
                        ConfigureDataTable = _ => new ExcelDataTableConfiguration
                        {
                            UseHeaderRow = true
                        }
                    });
                    DataTable dt = result.Tables[0];

                    var col1 = 0;
                    var row1 = 2;
                    var col2 = 0;
                    var row2 = 2;
                    var STT1 = 1;
                    var STT2 = 1;
                    var fileName = "ExcelData.xlsx";
                    var file = new FileInfo(fileName);
                    ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

                    var package1 = new ExcelPackage(file);
                    var worksheet2 = package1.Workbook.Worksheets.Add("Sheet1");

                    var package = new ExcelPackage(file);
                    var worksheet1 = package.Workbook.Worksheets.Add("Sheet1");

                    worksheet1.Column(1).Width = GetTrueColumnWidth(3.67);
                    worksheet1.Column(2).Width = GetTrueColumnWidth(28.11);
                    worksheet1.Column(3).Width = GetTrueColumnWidth(26.67);
                    worksheet1.Column(4).Width = GetTrueColumnWidth(44.11);
                    worksheet1.Column(5).Width = GetTrueColumnWidth(35.67);
                    worksheet1.Column(6).Width = GetTrueColumnWidth(32.44);
                    worksheet1.Column(7).Width = GetTrueColumnWidth(32.44);
                    worksheet1.Column(8).Width = GetTrueColumnWidth(32.44);

                    worksheet2.Column(1).Width = GetTrueColumnWidth(3.67);
                    worksheet2.Column(2).Width = GetTrueColumnWidth(28.11);
                    worksheet2.Column(3).Width = GetTrueColumnWidth(26.67);
                    worksheet2.Column(4).Width = GetTrueColumnWidth(44.11);
                    worksheet2.Column(5).Width = GetTrueColumnWidth(44.11);
                    worksheet2.Column(6).Width = GetTrueColumnWidth(35.67);
                    worksheet2.Column(7).Width = GetTrueColumnWidth(32.44);
                    worksheet2.Column(8).Width = GetTrueColumnWidth(32.44);

                    worksheet1.Cells["A1"].Value = "STT";
                    worksheet1.Cells["A1"].Style.Font.Bold = true;
                    worksheet1.Cells["A1"].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                    worksheet1.Cells["A1"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                    worksheet2.Cells["A1"].Value = "STT";
                    worksheet2.Cells["A1"].Style.Font.Bold = true;
                    worksheet2.Cells["A1"].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                    worksheet2.Cells["A1"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;


                    worksheet1.Cells["B1"].Value = "Họ Và Tên";
                    worksheet1.Cells["B1"].Style.Font.Bold = true;
                    worksheet1.Cells["B1"].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                    worksheet1.Cells["B1"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                    worksheet2.Cells["B1"].Value = "Họ Và Tên";
                    worksheet2.Cells["B1"].Style.Font.Bold = true;
                    worksheet2.Cells["B1"].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                    worksheet2.Cells["B1"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                    worksheet1.Cells["C1"].Value = "Ngày Sinh";
                    worksheet1.Cells["C1"].Style.Font.Bold = true;
                    worksheet1.Cells["C1"].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                    worksheet1.Cells["C1"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                    worksheet2.Cells["C1"].Value = "Ngày Sinh";
                    worksheet2.Cells["C1"].Style.Font.Bold = true;
                    worksheet2.Cells["C1"].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                    worksheet2.Cells["C1"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                    worksheet1.Cells["D1"].Value = "Chức Vụ/ Chức Danh Công Tác";
                    worksheet1.Cells["D1"].Style.Font.Bold = true;
                    worksheet1.Cells["D1"].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                    worksheet1.Cells["D1"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                    worksheet2.Cells["D1"].Value = "Chức Vụ/ Chức Danh Công Tác";
                    worksheet2.Cells["D1"].Style.Font.Bold = true;
                    worksheet2.Cells["D1"].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                    worksheet2.Cells["D1"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                    worksheet2.Cells["E1"].Value = "Cơ Quan Đơn Vị";
                    worksheet2.Cells["E1"].Style.Font.Bold = true;
                    worksheet2.Cells["E1"].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                    worksheet2.Cells["E1"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                    worksheet1.Cells["E1"].Value = "Email";
                    worksheet1.Cells["E1"].Style.Font.Bold = true;
                    worksheet1.Cells["E1"].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                    worksheet1.Cells["E1"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                    worksheet2.Cells["F1"].Value = "Email";
                    worksheet2.Cells["F1"].Style.Font.Bold = true;
                    worksheet2.Cells["F1"].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                    worksheet2.Cells["F1"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                    worksheet1.Cells["F1"].Value = "Kết Quả Import";
                    worksheet1.Cells["F1"].Style.Font.Bold = true;
                    worksheet1.Cells["F1"].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                    worksheet1.Cells["F1"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                    worksheet1.Cells["G1"].Value = "Tài Khoản";
                    worksheet1.Cells["G1"].Style.Font.Bold = true;
                    worksheet1.Cells["G1"].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                    worksheet1.Cells["G1"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                    worksheet2.Cells["G1"].Value = "Tài Khoản";
                    worksheet2.Cells["G1"].Style.Font.Bold = true;
                    worksheet2.Cells["G1"].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                    worksheet2.Cells["G1"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                    worksheet1.Cells["H1"].Value = "Mật Khẩu";
                    worksheet1.Cells["H1"].Style.Font.Bold = true;
                    worksheet1.Cells["h1"].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                    worksheet1.Cells["H1"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                    worksheet2.Cells["H1"].Value = "Mật Khẩu";
                    worksheet2.Cells["H1"].Style.Font.Bold = true;
                    worksheet2.Cells["H1"].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                    worksheet2.Cells["H1"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                    foreach (DataRow item in dt.Rows)
                    {
                        if (item["Họ Và Tên"].ToString() != "")
                        {
                            var Email = item["Email"].ToString().ToUpper();
                            if (db.DM_CanBo.Where(_ => _.Email.ToUpper() == Email).Count() == 0)
                            {
                                success++;
                                worksheet1.Cells[row1, col1 + 1].Value = STT1;
                                worksheet1.Cells[row1, col1 + 1].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                                worksheet1.Cells[row1, col1 + 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                worksheet2.Cells[row2, col2 + 1].Value = STT2;
                                worksheet2.Cells[row2, col2 + 1].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                                worksheet2.Cells[row2, col2 + 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                worksheet1.Cells[row1, col1 + 2].Value = item["Họ Và Tên"].ToString();
                                worksheet1.Cells[row1, col1 + 2].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                                worksheet1.Cells[row1, col1 + 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                worksheet2.Cells[row2, col2 + 2].Value = item["Họ Và Tên"].ToString();
                                worksheet2.Cells[row2, col2 + 2].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                                worksheet2.Cells[row2, col2 + 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                worksheet1.Cells[row1, col1 + 3].Value = item["Ngày Sinh"].ToString();
                                worksheet1.Cells[row1, col1 + 3].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                                worksheet1.Cells[row1, col1 + 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                worksheet2.Cells[row2, col2 + 3].Value = item["Ngày Sinh"].ToString();
                                worksheet2.Cells[row2, col2 + 3].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                                worksheet2.Cells[row2, col2 + 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                worksheet1.Cells[row1, col1 + 4].Value = item["Chức Vụ/ Chức Danh Công Tác"].ToString();
                                worksheet1.Cells[row1, col1 + 4].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                                worksheet1.Cells[row1, col1 + 4].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                worksheet2.Cells[row2, col2 + 4].Value = item["Chức Vụ/ Chức Danh Công Tác"].ToString();
                                worksheet2.Cells[row2, col2 + 4].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                                worksheet2.Cells[row2, col2 + 4].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                worksheet2.Cells[row2, col2 + 5].Value = db.DM_CoQuanDonVi.Find(137).Ten;
                                worksheet2.Cells[row2, col2 + 5].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                                worksheet2.Cells[row2, col2 + 5].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                worksheet1.Cells[row1, col1 + 5].Value = item["Email"].ToString();
                                worksheet1.Cells[row1, col1 + 5].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                                worksheet1.Cells[row1, col1 + 5].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                worksheet2.Cells[row2, col2 + 6].Value = item["Email"].ToString();
                                worksheet2.Cells[row2, col2 + 6].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                                worksheet2.Cells[row2, col2 + 6].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;


                                worksheet1.Cells[row1, col1 + 6].Value = "Import Thành Công";
                                worksheet1.Cells[row1, col1 + 6].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                                worksheet1.Cells[row1, col1 + 6].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                var ChucVuChucDanh = item["Chức Vụ/ Chức Danh Công Tác"].ToString().ToUpper();
                                var cvcd = db.DM_ChucVu_ChucDanh.Where(_ => _.Ten_ChucVu_ChucDanh.ToUpper() == ChucVuChucDanh);
                                var id_cvcd = 0;
                                if (cvcd.Count() == 0)
                                {
                                    var CV = new DM_ChucVu_ChucDanh();
                                    CV.Ten_ChucVu_ChucDanh = item["Chức Vụ/ Chức Danh Công Tác"].ToString();
                                    db.DM_ChucVu_ChucDanh.Add(CV);
                                    db.SaveChanges();
                                    id_cvcd = CV.Ma_ChucVu_ChucDanh;
                                }
                                else
                                {
                                    var CV = cvcd.First();
                                    id_cvcd = CV.Ma_ChucVu_ChucDanh;
                                }

                                var cb = new DM_CanBo();
                                cb.HoTen = item["Họ Và Tên"].ToString();
                                cb.DoB = Convert.ToDateTime(item["Ngày Sinh"].ToString()).ToString("dd/MM/yyyy");
                                cb.Ma_ChucVu_ChucDanh = id_cvcd;
                                cb.Ma_CoQuan_DonVi = 137;
                                string[] words = item["Họ Và Tên"].ToString().Split(' ');
                                cb.Ten = words[words.Length - 1];
                                cb.Email = item["Email"].ToString();
                                db.DM_CanBo.Add(cb);
                                db.SaveChanges();

                                var taikhoan = new HT_TaiKhoan();

                                var maCanBo = cb.Ma_CanBo;
                                var tenTaiKhoan = string.Concat(UnsignedVietnameseString(cb.HoTen) + cb.Ma_CanBo.ToString());
                                var matKhau = GetRandomPassword(8);
                                var maNhomTaiKhoan = 9;
                                bool trangThai = true;
                                var nguoiTao = UserName;
                                var ngayTao = DateTime.Now;
                                
                                taikhoan.Ma_CanBo = maCanBo;
                                taikhoan.TenTaiKhoan = tenTaiKhoan;
                                taikhoan.MatKhau = matKhau;
                                taikhoan.MaNhomTaiKhoan = maNhomTaiKhoan;
                                taikhoan.TrangThai = trangThai;
                                taikhoan.NguoiTao = nguoiTao;
                                taikhoan.NgayTao = ngayTao;
                                taikhoan.CheckPass = false;
                                db.HT_TaiKhoan.Add(taikhoan);
                                db.SaveChanges();

                                worksheet1.Cells[row1, col1 + 7].Value = taikhoan.TenTaiKhoan;
                                worksheet1.Cells[row1, col1 + 7].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                                worksheet1.Cells[row1, col1 + 7].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                worksheet2.Cells[row2, col2 + 7].Value = taikhoan.TenTaiKhoan;
                                worksheet2.Cells[row2, col2 + 7].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                                worksheet2.Cells[row2, col2 + 7].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                worksheet1.Cells[row1, col1 + 8].Value = taikhoan.MatKhau;
                                worksheet1.Cells[row1, col1 + 8].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                                worksheet1.Cells[row1, col1 + 8].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                worksheet2.Cells[row2, col2 + 8].Value = taikhoan.MatKhau;
                                worksheet2.Cells[row2, col2 + 8].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                                worksheet2.Cells[row2, col2 + 8].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                row1++;
                                row2++;
                                STT1++;
                                STT2++;
                            }
                            else
                            {
                                fail++;
                                worksheet1.Cells[row1, col1 + 1].Value = STT1;
                                worksheet1.Cells[row1, col1 + 1].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                                worksheet1.Cells[row1, col1 + 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                worksheet1.Cells[row1, col1 + 2].Value = item["Họ Và Tên"].ToString();
                                worksheet1.Cells[row1, col1 + 2].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                                worksheet1.Cells[row1, col1 + 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                worksheet1.Cells[row1, col1 + 3].Value = item["Ngày Sinh"].ToString();
                                worksheet1.Cells[row1, col1 + 3].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                                worksheet1.Cells[row1, col1 + 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                worksheet1.Cells[row1, col1 + 4].Value = item["Chức Vụ/ Chức Danh Công Tác"].ToString();
                                worksheet1.Cells[row1, col1 + 4].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                                worksheet1.Cells[row1, col1 + 4].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                worksheet1.Cells[row1, col1 + 5].Value = item["Email"].ToString();
                                worksheet1.Cells[row1, col1 + 5].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                                worksheet1.Cells[row1, col1 + 5].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                worksheet1.Cells[row1, col1 + 6].Value = "Email Đã Tồn Tại";
                                worksheet1.Cells[row1, col1 + 6].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                                worksheet1.Cells[row1, col1 + 6].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                worksheet1.Cells[row1, col1 + 7].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                                worksheet1.Cells[row1, col1 + 7].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                worksheet1.Cells[row1, col1 + 8].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                                worksheet1.Cells[row1, col1 + 8].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                row1++;
                                STT1++;
                            }


                        }

                    }

                    stream.Close();

                    string filename1 = "KetQua_" + filename;
                    TenFileKetQua = filename1;
                    string targetpath1 = Server.MapPath("~/Content/Uploads/");

                    FileInfo fi = new FileInfo(targetpath1 + filename1);
                    package.SaveAs(fi);

                    string filename2 = "TaiKhoan_" + filename;
                    FileInfo fi1 = new FileInfo(targetpath1 + filename2);
                    package1.SaveAs(fi1);

                    var ctk = new NV_CapTaiKhoan();
                    ctk.FileCap = filename2;
                    ctk.NgayCap = DateTime.Now;
                    ctk.NguoiCap = UserID;

                    db.NV_CapTaiKhoan.Add(ctk);
                    db.SaveChanges();
                }
                var data = new
                {
                    success = success,
                    fail = fail,
                    TenFileKetQua = TenFileKetQua
                };
                return Json(data, JsonRequestBehavior.AllowGet);

            }
            else if(Role.ToString().ToUpper() == "TTKSTN")
            {
                using (var stream = System.IO.File.Open(pathToExcelFile, FileMode.Open, FileAccess.Read))
                {
                    var reader = ExcelReaderFactory.CreateReader(stream);
                    var result = reader.AsDataSet(new ExcelDataSetConfiguration
                    {
                        ConfigureDataTable = _ => new ExcelDataTableConfiguration
                        {
                            UseHeaderRow = true
                        }
                    });
                    DataTable dt = result.Tables[0];

                    var col1 = 0;
                    var row1 = 2;
                    var col2 = 0;
                    var row2 = 2;
                    var STT1 = 1;
                    var STT2 = 1;
                    var fileName = "ExcelData.xlsx";
                    var file = new FileInfo(fileName);
                    ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

                    var package1 = new ExcelPackage(file);
                    var worksheet2 = package1.Workbook.Worksheets.Add("Sheet1");

                    var package = new ExcelPackage(file);
                    var worksheet1 = package.Workbook.Worksheets.Add("Sheet1");

                    worksheet1.Column(1).Width = GetTrueColumnWidth(3.67);
                    worksheet1.Column(2).Width = GetTrueColumnWidth(28.11);
                    worksheet1.Column(3).Width = GetTrueColumnWidth(26.67);
                    worksheet1.Column(4).Width = GetTrueColumnWidth(44.11);
                    worksheet1.Column(5).Width = GetTrueColumnWidth(35.67);
                    worksheet1.Column(6).Width = GetTrueColumnWidth(44.11);
                    worksheet1.Column(7).Width = GetTrueColumnWidth(44.11);
                    worksheet1.Column(8).Width = GetTrueColumnWidth(32.44);
                    worksheet1.Column(9).Width = GetTrueColumnWidth(32.44);
                    worksheet1.Column(10).Width = GetTrueColumnWidth(32.44);

                    worksheet2.Column(1).Width = GetTrueColumnWidth(3.67);
                    worksheet2.Column(2).Width = GetTrueColumnWidth(28.11);
                    worksheet2.Column(3).Width = GetTrueColumnWidth(26.67);
                    worksheet2.Column(4).Width = GetTrueColumnWidth(44.11);
                    worksheet2.Column(5).Width = GetTrueColumnWidth(44.11);
                    worksheet2.Column(6).Width = GetTrueColumnWidth(35.67);
                    worksheet2.Column(7).Width = GetTrueColumnWidth(32.44);
                    worksheet2.Column(8).Width = GetTrueColumnWidth(32.44);

                    worksheet1.Cells["A1"].Value = "STT";
                    worksheet1.Cells["A1"].Style.Font.Bold = true;
                    worksheet1.Cells["A1"].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                    worksheet1.Cells["A1"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                    worksheet2.Cells["A1"].Value = "STT";
                    worksheet2.Cells["A1"].Style.Font.Bold = true;
                    worksheet2.Cells["A1"].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                    worksheet2.Cells["A1"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;


                    worksheet1.Cells["B1"].Value = "Họ Và Tên";
                    worksheet1.Cells["B1"].Style.Font.Bold = true;
                    worksheet1.Cells["B1"].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                    worksheet1.Cells["B1"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                    worksheet2.Cells["B1"].Value = "Họ Và Tên";
                    worksheet2.Cells["B1"].Style.Font.Bold = true;
                    worksheet2.Cells["B1"].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                    worksheet2.Cells["B1"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                    worksheet1.Cells["C1"].Value = "Ngày Sinh";
                    worksheet1.Cells["C1"].Style.Font.Bold = true;
                    worksheet1.Cells["C1"].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                    worksheet1.Cells["C1"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                    worksheet2.Cells["C1"].Value = "Ngày Sinh";
                    worksheet2.Cells["C1"].Style.Font.Bold = true;
                    worksheet2.Cells["C1"].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                    worksheet2.Cells["C1"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                    worksheet1.Cells["D1"].Value = "Chức Vụ/ Chức Danh Công Tác";
                    worksheet1.Cells["D1"].Style.Font.Bold = true;
                    worksheet1.Cells["D1"].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                    worksheet1.Cells["D1"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                    worksheet2.Cells["D1"].Value = "Chức Vụ/ Chức Danh Công Tác";
                    worksheet2.Cells["D1"].Style.Font.Bold = true;
                    worksheet2.Cells["D1"].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                    worksheet2.Cells["D1"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                    worksheet2.Cells["E1"].Value = "Cơ Quan Đơn Vị";
                    worksheet2.Cells["E1"].Style.Font.Bold = true;
                    worksheet2.Cells["E1"].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                    worksheet2.Cells["E1"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                    worksheet1.Cells["E1"].Value = "Email";
                    worksheet1.Cells["E1"].Style.Font.Bold = true;
                    worksheet1.Cells["E1"].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                    worksheet1.Cells["E1"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                    worksheet2.Cells["F1"].Value = "Email";
                    worksheet2.Cells["F1"].Style.Font.Bold = true;
                    worksheet2.Cells["F1"].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                    worksheet2.Cells["F1"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                    worksheet1.Cells["F1"].Value = "Cơ Quan Đơn Vị";
                    worksheet1.Cells["F1"].Style.Font.Bold = true;
                    worksheet1.Cells["F1"].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                    worksheet1.Cells["F1"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                    worksheet1.Cells["G1"].Value = "Nhóm Tài Khoản";
                    worksheet1.Cells["G1"].Style.Font.Bold = true;
                    worksheet1.Cells["G1"].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                    worksheet1.Cells["G1"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                    worksheet1.Cells["H1"].Value = "Kết Quả Import";
                    worksheet1.Cells["H1"].Style.Font.Bold = true;
                    worksheet1.Cells["H1"].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                    worksheet1.Cells["H1"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                    worksheet1.Cells["I1"].Value = "Tài Khoản";
                    worksheet1.Cells["I1"].Style.Font.Bold = true;
                    worksheet1.Cells["I1"].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                    worksheet1.Cells["I1"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                    worksheet1.Cells["J1"].Value = "Mật Khẩu";
                    worksheet1.Cells["J1"].Style.Font.Bold = true;
                    worksheet1.Cells["J1"].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                    worksheet1.Cells["J1"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                    worksheet2.Cells["G1"].Value = "Tài Khoản";
                    worksheet2.Cells["G1"].Style.Font.Bold = true;
                    worksheet2.Cells["G1"].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                    worksheet2.Cells["G1"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                    worksheet1.Cells["H1"].Value = "Mật Khẩu";
                    worksheet1.Cells["H1"].Style.Font.Bold = true;
                    worksheet1.Cells["h1"].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                    worksheet1.Cells["H1"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                    worksheet2.Cells["H1"].Value = "Mật Khẩu";
                    worksheet2.Cells["H1"].Style.Font.Bold = true;
                    worksheet2.Cells["H1"].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                    worksheet2.Cells["H1"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                    foreach (DataRow item in dt.Rows)
                    {
                        if (item["Họ Và Tên"].ToString() != "")
                        {
                            var Email = item["Email"].ToString().ToUpper();
                            if (db.DM_CanBo.Where(_ => _.Email.ToUpper() == Email).Count() == 0)
                            {
                                var CQDV = item["Cơ Quan Đơn Vị"].ToString().ToUpper();
                                if (db.DM_CoQuanDonVi.Where(_ => _.Ten.ToUpper() == CQDV).Count() == 1)
                                {
                                    var NhomTK = item["Nhóm Tài Khoản"].ToString().ToUpper();

                                    if(NhomTK == "NGƯỜI ĐỨNG ĐẦU" || NhomTK == "PHÓ")
                                    {
                                        if (db.HT_NhomTaiKhoan.Where(_ => _.TenNhomTaiKhoan.ToUpper() == NhomTK).Count() == 1)
                                        {
                                            success++;
                                            var ntk = db.HT_NhomTaiKhoan.Where(_ => _.TenNhomTaiKhoan.ToUpper() == NhomTK).First();
                                            var cqdv = db.DM_CoQuanDonVi.Where(_ => _.Ten.ToUpper() == CQDV).First();
                                            worksheet1.Cells[row1, col1 + 1].Value = STT1;
                                            worksheet1.Cells[row1, col1 + 1].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                                            worksheet1.Cells[row1, col1 + 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                            worksheet2.Cells[row2, col2 + 1].Value = STT2;
                                            worksheet2.Cells[row2, col2 + 1].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                                            worksheet2.Cells[row2, col2 + 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                            worksheet1.Cells[row1, col1 + 2].Value = item["Họ Và Tên"].ToString();
                                            worksheet1.Cells[row1, col1 + 2].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                                            worksheet1.Cells[row1, col1 + 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                            worksheet2.Cells[row2, col2 + 2].Value = item["Họ Và Tên"].ToString();
                                            worksheet2.Cells[row2, col2 + 2].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                                            worksheet2.Cells[row2, col2 + 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                            worksheet1.Cells[row1, col1 + 3].Value = item["Ngày Sinh"].ToString();
                                            worksheet1.Cells[row1, col1 + 3].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                                            worksheet1.Cells[row1, col1 + 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                            worksheet2.Cells[row2, col2 + 3].Value = item["Ngày Sinh"].ToString();
                                            worksheet2.Cells[row2, col2 + 3].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                                            worksheet2.Cells[row2, col2 + 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                            worksheet1.Cells[row1, col1 + 4].Value = item["Chức Vụ/ Chức Danh Công Tác"].ToString();
                                            worksheet1.Cells[row1, col1 + 4].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                                            worksheet1.Cells[row1, col1 + 4].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                            worksheet2.Cells[row2, col2 + 4].Value = item["Chức Vụ/ Chức Danh Công Tác"].ToString();
                                            worksheet2.Cells[row2, col2 + 4].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                                            worksheet2.Cells[row2, col2 + 4].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                            worksheet2.Cells[row2, col2 + 5].Value = item["Cơ Quan Đơn Vị"].ToString();
                                            worksheet2.Cells[row2, col2 + 5].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                                            worksheet2.Cells[row2, col2 + 5].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                            worksheet1.Cells[row1, col1 + 5].Value = item["Email"].ToString();
                                            worksheet1.Cells[row1, col1 + 5].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                                            worksheet1.Cells[row1, col1 + 5].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                            worksheet2.Cells[row2, col2 + 6].Value = item["Email"].ToString();
                                            worksheet2.Cells[row2, col2 + 6].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                                            worksheet2.Cells[row2, col2 + 6].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                            worksheet1.Cells[row1, col1 + 6].Value = item["Cơ Quan Đơn Vị"].ToString();
                                            worksheet1.Cells[row1, col1 + 6].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                                            worksheet1.Cells[row1, col1 + 6].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                            worksheet1.Cells[row1, col1 + 7].Value = item["Nhóm Tài Khoản"].ToString();
                                            worksheet1.Cells[row1, col1 + 7].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                                            worksheet1.Cells[row1, col1 + 7].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                            worksheet1.Cells[row1, col1 + 8].Value = "Import Thành Công";
                                            worksheet1.Cells[row1, col1 + 8].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                                            worksheet1.Cells[row1, col1 + 8].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                            var ChucVuChucDanh = item["Chức Vụ/ Chức Danh Công Tác"].ToString().ToUpper();
                                            var cvcd = db.DM_ChucVu_ChucDanh.Where(_ => _.Ten_ChucVu_ChucDanh.ToUpper() == ChucVuChucDanh);
                                            var id_cvcd = 0;
                                            if (cvcd.Count() == 0)
                                            {
                                                var CV = new DM_ChucVu_ChucDanh();
                                                CV.Ten_ChucVu_ChucDanh = item["Chức Vụ/ Chức Danh Công Tác"].ToString();
                                                db.DM_ChucVu_ChucDanh.Add(CV);
                                                db.SaveChanges();
                                                id_cvcd = CV.Ma_ChucVu_ChucDanh;
                                            }
                                            else
                                            {
                                                var CV = cvcd.First();
                                                id_cvcd = CV.Ma_ChucVu_ChucDanh;
                                            }

                                            var cb = new DM_CanBo();
                                            cb.HoTen = item["Họ Và Tên"].ToString();
                                            cb.DoB = Convert.ToDateTime(item["Ngày Sinh"].ToString()).ToString("dd/MM/yyyy");
                                            cb.Ma_ChucVu_ChucDanh = id_cvcd;
                                            cb.Ma_CoQuan_DonVi = cqdv.Ma_CoQuan_DonVi;
                                            string[] words = item["Họ Và Tên"].ToString().Split(' ');
                                            cb.Ten = words[words.Length - 1];
                                            cb.Email = item["Email"].ToString();
                                            db.DM_CanBo.Add(cb);
                                            db.SaveChanges();

                                            var taikhoan = new HT_TaiKhoan();

                                            var maCanBo = cb.Ma_CanBo;
                                            var tenTaiKhoan = string.Concat(UnsignedVietnameseString(cb.HoTen) + cb.Ma_CanBo.ToString());
                                            var matKhau = GetRandomPassword(8);
                                            var maNhomTaiKhoan = ntk.MaNhomTaiKhoan;
                                            bool trangThai = true;
                                            var nguoiTao = UserName;
                                            var ngayTao = DateTime.Now;

                                            taikhoan.Ma_CanBo = maCanBo;
                                            taikhoan.TenTaiKhoan = tenTaiKhoan;
                                            taikhoan.MatKhau = matKhau;
                                            taikhoan.MaNhomTaiKhoan = maNhomTaiKhoan;
                                            taikhoan.TrangThai = trangThai;
                                            taikhoan.NguoiTao = nguoiTao;
                                            taikhoan.NgayTao = ngayTao;
                                            taikhoan.CheckPass = false;
                                            db.HT_TaiKhoan.Add(taikhoan);
                                            db.SaveChanges();



                                            worksheet1.Cells[row1, col1 + 9].Value = taikhoan.TenTaiKhoan;
                                            worksheet1.Cells[row1, col1 + 9].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                                            worksheet1.Cells[row1, col1 + 9].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                            worksheet2.Cells[row2, col2 + 7].Value = taikhoan.TenTaiKhoan;
                                            worksheet2.Cells[row2, col2 + 7].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                                            worksheet2.Cells[row2, col2 + 7].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                            worksheet1.Cells[row1, col1 + 10].Value = taikhoan.MatKhau;
                                            worksheet1.Cells[row1, col1 + 10].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                                            worksheet1.Cells[row1, col1 + 10].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                            worksheet2.Cells[row2, col2 + 8].Value = taikhoan.MatKhau;
                                            worksheet2.Cells[row2, col2 + 8].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                                            worksheet2.Cells[row2, col2 + 8].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                            row1++;
                                            row2++;
                                            STT1++;
                                            STT2++;
                                        }
                                        else
                                        {
                                            fail++;
                                            worksheet1.Cells[row1, col1 + 1].Value = STT1;
                                            worksheet1.Cells[row1, col1 + 1].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                                            worksheet1.Cells[row1, col1 + 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                            worksheet1.Cells[row1, col1 + 2].Value = item["Họ Và Tên"].ToString();
                                            worksheet1.Cells[row1, col1 + 2].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                                            worksheet1.Cells[row1, col1 + 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                            worksheet1.Cells[row1, col1 + 3].Value = item["Ngày Sinh"].ToString();
                                            worksheet1.Cells[row1, col1 + 3].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                                            worksheet1.Cells[row1, col1 + 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                            worksheet1.Cells[row1, col1 + 4].Value = item["Chức Vụ/ Chức Danh Công Tác"].ToString();
                                            worksheet1.Cells[row1, col1 + 4].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                                            worksheet1.Cells[row1, col1 + 4].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                            worksheet1.Cells[row1, col1 + 5].Value = item["Email"].ToString();
                                            worksheet1.Cells[row1, col1 + 5].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                                            worksheet1.Cells[row1, col1 + 5].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                            worksheet1.Cells[row1, col1 + 6].Value = item["Cơ Quan Đơn Vị"].ToString();
                                            worksheet1.Cells[row1, col1 + 6].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                                            worksheet1.Cells[row1, col1 + 6].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                            worksheet1.Cells[row1, col1 + 7].Value = item["Nhóm Tài Khoản"].ToString();
                                            worksheet1.Cells[row1, col1 + 7].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                                            worksheet1.Cells[row1, col1 + 7].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                            worksheet1.Cells[row1, col1 + 8].Value = "Nhóm Tài Khoản Không Tìm Thấy";
                                            worksheet1.Cells[row1, col1 + 8].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                                            worksheet1.Cells[row1, col1 + 8].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                            worksheet1.Cells[row1, col1 + 9].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                                            worksheet1.Cells[row1, col1 + 9].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                            worksheet1.Cells[row1, col1 + 10].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                                            worksheet1.Cells[row1, col1 + 10].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                            STT1++;
                                            row1++;
                                        }

                                    }
                                    else
                                    {
                                        fail++;
                                        worksheet1.Cells[row1, col1 + 1].Value = STT1;
                                        worksheet1.Cells[row1, col1 + 1].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                                        worksheet1.Cells[row1, col1 + 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                        worksheet1.Cells[row1, col1 + 2].Value = item["Họ Và Tên"].ToString();
                                        worksheet1.Cells[row1, col1 + 2].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                                        worksheet1.Cells[row1, col1 + 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                        worksheet1.Cells[row1, col1 + 3].Value = item["Ngày Sinh"].ToString();
                                        worksheet1.Cells[row1, col1 + 3].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                                        worksheet1.Cells[row1, col1 + 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                        worksheet1.Cells[row1, col1 + 4].Value = item["Chức Vụ/ Chức Danh Công Tác"].ToString();
                                        worksheet1.Cells[row1, col1 + 4].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                                        worksheet1.Cells[row1, col1 + 4].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                        worksheet1.Cells[row1, col1 + 5].Value = item["Email"].ToString();
                                        worksheet1.Cells[row1, col1 + 5].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                                        worksheet1.Cells[row1, col1 + 5].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                        worksheet1.Cells[row1, col1 + 6].Value = item["Cơ Quan Đơn Vị"].ToString();
                                        worksheet1.Cells[row1, col1 + 6].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                                        worksheet1.Cells[row1, col1 + 6].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                        worksheet1.Cells[row1, col1 + 7].Value = item["Nhóm Tài Khoản"].ToString();
                                        worksheet1.Cells[row1, col1 + 7].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                                        worksheet1.Cells[row1, col1 + 7].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                        worksheet1.Cells[row1, col1 + 8].Value = "Nhóm Tài Khoản Không Hợp Lệ";
                                        worksheet1.Cells[row1, col1 + 8].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                                        worksheet1.Cells[row1, col1 + 8].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                        worksheet1.Cells[row1, col1 + 9].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                                        worksheet1.Cells[row1, col1 + 9].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                        worksheet1.Cells[row1, col1 + 10].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                                        worksheet1.Cells[row1, col1 + 10].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                        STT1++;
                                        row1++;
                                    }
                                }
                                else
                                {
                                    fail++;
                                    worksheet1.Cells[row1, col1 + 1].Value = STT1;
                                    worksheet1.Cells[row1, col1 + 1].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                                    worksheet1.Cells[row1, col1 + 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                    worksheet1.Cells[row1, col1 + 2].Value = item["Họ Và Tên"].ToString();
                                    worksheet1.Cells[row1, col1 + 2].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                                    worksheet1.Cells[row1, col1 + 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                    worksheet1.Cells[row1, col1 + 3].Value = item["Ngày Sinh"].ToString();
                                    worksheet1.Cells[row1, col1 + 3].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                                    worksheet1.Cells[row1, col1 + 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                    worksheet1.Cells[row1, col1 + 4].Value = item["Chức Vụ/ Chức Danh Công Tác"].ToString();
                                    worksheet1.Cells[row1, col1 + 4].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                                    worksheet1.Cells[row1, col1 + 4].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                    worksheet1.Cells[row1, col1 + 5].Value = item["Email"].ToString();
                                    worksheet1.Cells[row1, col1 + 5].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                                    worksheet1.Cells[row1, col1 + 5].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                    worksheet1.Cells[row1, col1 + 6].Value = item["Cơ Quan Đơn Vị"].ToString();
                                    worksheet1.Cells[row1, col1 + 6].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                                    worksheet1.Cells[row1, col1 + 6].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                    worksheet1.Cells[row1, col1 + 7].Value = item["Nhóm Tài Khoản"].ToString();
                                    worksheet1.Cells[row1, col1 + 7].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                                    worksheet1.Cells[row1, col1 + 7].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                    worksheet1.Cells[row1, col1 + 8].Value = "Không Tìm Thấy Cơ Quan Đơn Vị";
                                    worksheet1.Cells[row1, col1 + 8].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                                    worksheet1.Cells[row1, col1 + 8].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                    worksheet1.Cells[row1, col1 + 9].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                                    worksheet1.Cells[row1, col1 + 9].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                    worksheet1.Cells[row1, col1 + 10].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                                    worksheet1.Cells[row1, col1 + 10].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                    STT1++;
                                    row1++;
                                }

                                
                            }
                            else
                            {
                                fail++;
                                worksheet1.Cells[row1, col1 + 1].Value = STT1;
                                worksheet1.Cells[row1, col1 + 1].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                                worksheet1.Cells[row1, col1 + 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                worksheet1.Cells[row1, col1 + 2].Value = item["Họ Và Tên"].ToString();
                                worksheet1.Cells[row1, col1 + 2].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                                worksheet1.Cells[row1, col1 + 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                worksheet1.Cells[row1, col1 + 3].Value = item["Ngày Sinh"].ToString();
                                worksheet1.Cells[row1, col1 + 3].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                                worksheet1.Cells[row1, col1 + 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                worksheet1.Cells[row1, col1 + 4].Value = item["Chức Vụ/ Chức Danh Công Tác"].ToString();
                                worksheet1.Cells[row1, col1 + 4].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                                worksheet1.Cells[row1, col1 + 4].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                worksheet1.Cells[row1, col1 + 5].Value = item["Email"].ToString();
                                worksheet1.Cells[row1, col1 + 5].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                                worksheet1.Cells[row1, col1 + 5].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                worksheet1.Cells[row1, col1 + 6].Value = item["Cơ Quan Đơn Vị"].ToString();
                                worksheet1.Cells[row1, col1 + 6].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                                worksheet1.Cells[row1, col1 + 6].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                worksheet1.Cells[row1, col1 + 7].Value = item["Nhóm Tài Khoản"].ToString();
                                worksheet1.Cells[row1, col1 + 7].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                                worksheet1.Cells[row1, col1 + 7].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                worksheet1.Cells[row1, col1 + 8].Value = "Email Đã Tồn Tại";
                                worksheet1.Cells[row1, col1 + 8].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                                worksheet1.Cells[row1, col1 + 8].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                worksheet1.Cells[row1, col1 + 9].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                                worksheet1.Cells[row1, col1 + 9].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                worksheet1.Cells[row1, col1 + 10].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                                worksheet1.Cells[row1, col1 + 10].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                STT1++;
                                row1++;
                            }
                        }

                    }

                    stream.Close();

                    string filename1 = "KetQua_" + filename;
                    TenFileKetQua = filename1;
                    string targetpath1 = Server.MapPath("~/Content/Uploads/");

                    FileInfo fi = new FileInfo(targetpath1 + filename1);
                    package.SaveAs(fi);

                    string filename2 = "TaiKhoan_" + filename;
                    FileInfo fi1 = new FileInfo(targetpath1 + filename2);
                    package1.SaveAs(fi1);

                    var ctk = new NV_CapTaiKhoan();
                    ctk.FileCap = filename2;
                    ctk.NgayCap = DateTime.Now;
                    ctk.NguoiCap = UserID;

                    db.NV_CapTaiKhoan.Add(ctk);
                    db.SaveChanges();
                }
                var data = new
                {
                    success = success,
                    fail = fail,
                    TenFileKetQua = TenFileKetQua
                };
                return Json(data, JsonRequestBehavior.AllowGet);
            }
            else if (Role.ToString().ToUpper() == "NQTCSBN")
            {

                using (var stream = System.IO.File.Open(pathToExcelFile, FileMode.Open, FileAccess.Read))
                {
                    var reader = ExcelReaderFactory.CreateReader(stream);
                    var result = reader.AsDataSet(new ExcelDataSetConfiguration
                    {
                        ConfigureDataTable = _ => new ExcelDataTableConfiguration
                        {
                            UseHeaderRow = true
                        }
                    });
                    DataTable dt = result.Tables[0];

                    var col1 = 0;
                    var row1 = 2;
                    var col2 = 0;
                    var row2 = 2;
                    var STT1 = 1;
                    var STT2 = 1;
                    var fileName = "ExcelData.xlsx";
                    var file = new FileInfo(fileName);
                    ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

                    var package1 = new ExcelPackage(file);
                    var worksheet2 = package1.Workbook.Worksheets.Add("Sheet1");

                    var package = new ExcelPackage(file);
                    var worksheet1 = package.Workbook.Worksheets.Add("Sheet1");

                    worksheet1.Column(1).Width = GetTrueColumnWidth(3.67);
                    worksheet1.Column(2).Width = GetTrueColumnWidth(28.11);
                    worksheet1.Column(3).Width = GetTrueColumnWidth(26.67);
                    worksheet1.Column(4).Width = GetTrueColumnWidth(44.11);
                    worksheet1.Column(5).Width = GetTrueColumnWidth(35.67);
                    worksheet1.Column(6).Width = GetTrueColumnWidth(32.44);
                    worksheet1.Column(7).Width = GetTrueColumnWidth(32.44);
                    worksheet1.Column(8).Width = GetTrueColumnWidth(32.44);

                    worksheet2.Column(1).Width = GetTrueColumnWidth(3.67);
                    worksheet2.Column(2).Width = GetTrueColumnWidth(28.11);
                    worksheet2.Column(3).Width = GetTrueColumnWidth(26.67);
                    worksheet2.Column(4).Width = GetTrueColumnWidth(44.11);
                    worksheet2.Column(5).Width = GetTrueColumnWidth(44.11);
                    worksheet2.Column(6).Width = GetTrueColumnWidth(35.67);
                    worksheet2.Column(7).Width = GetTrueColumnWidth(32.44);
                    worksheet2.Column(8).Width = GetTrueColumnWidth(32.44);

                    worksheet1.Cells["A1"].Value = "STT";
                    worksheet1.Cells["A1"].Style.Font.Bold = true;
                    worksheet1.Cells["A1"].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                    worksheet1.Cells["A1"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                    worksheet2.Cells["A1"].Value = "STT";
                    worksheet2.Cells["A1"].Style.Font.Bold = true;
                    worksheet2.Cells["A1"].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                    worksheet2.Cells["A1"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;


                    worksheet1.Cells["B1"].Value = "Họ Và Tên";
                    worksheet1.Cells["B1"].Style.Font.Bold = true;
                    worksheet1.Cells["B1"].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                    worksheet1.Cells["B1"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                    worksheet2.Cells["B1"].Value = "Họ Và Tên";
                    worksheet2.Cells["B1"].Style.Font.Bold = true;
                    worksheet2.Cells["B1"].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                    worksheet2.Cells["B1"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                    worksheet1.Cells["C1"].Value = "Ngày Sinh";
                    worksheet1.Cells["C1"].Style.Font.Bold = true;
                    worksheet1.Cells["C1"].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                    worksheet1.Cells["C1"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                    worksheet2.Cells["C1"].Value = "Ngày Sinh";
                    worksheet2.Cells["C1"].Style.Font.Bold = true;
                    worksheet2.Cells["C1"].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                    worksheet2.Cells["C1"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                    worksheet1.Cells["D1"].Value = "Chức Vụ/ Chức Danh Công Tác";
                    worksheet1.Cells["D1"].Style.Font.Bold = true;
                    worksheet1.Cells["D1"].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                    worksheet1.Cells["D1"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                    worksheet2.Cells["D1"].Value = "Chức Vụ/ Chức Danh Công Tác";
                    worksheet2.Cells["D1"].Style.Font.Bold = true;
                    worksheet2.Cells["D1"].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                    worksheet2.Cells["D1"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                    worksheet2.Cells["E1"].Value = "Cơ Quan Đơn Vị";
                    worksheet2.Cells["E1"].Style.Font.Bold = true;
                    worksheet2.Cells["E1"].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                    worksheet2.Cells["E1"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                    worksheet1.Cells["E1"].Value = "Email";
                    worksheet1.Cells["E1"].Style.Font.Bold = true;
                    worksheet1.Cells["E1"].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                    worksheet1.Cells["E1"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                    worksheet2.Cells["F1"].Value = "Email";
                    worksheet2.Cells["F1"].Style.Font.Bold = true;
                    worksheet2.Cells["F1"].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                    worksheet2.Cells["F1"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                    worksheet1.Cells["F1"].Value = "Kết Quả Import";
                    worksheet1.Cells["F1"].Style.Font.Bold = true;
                    worksheet1.Cells["F1"].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                    worksheet1.Cells["F1"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                    worksheet1.Cells["G1"].Value = "Tài Khoản";
                    worksheet1.Cells["G1"].Style.Font.Bold = true;
                    worksheet1.Cells["G1"].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                    worksheet1.Cells["G1"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                    worksheet2.Cells["G1"].Value = "Tài Khoản";
                    worksheet2.Cells["G1"].Style.Font.Bold = true;
                    worksheet2.Cells["G1"].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                    worksheet2.Cells["G1"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                    worksheet1.Cells["H1"].Value = "Mật Khẩu";
                    worksheet1.Cells["H1"].Style.Font.Bold = true;
                    worksheet1.Cells["h1"].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                    worksheet1.Cells["H1"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                    worksheet2.Cells["H1"].Value = "Mật Khẩu";
                    worksheet2.Cells["H1"].Style.Font.Bold = true;
                    worksheet2.Cells["H1"].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                    worksheet2.Cells["H1"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                    foreach (DataRow item in dt.Rows)
                    {
                        if (item["Họ Và Tên"].ToString() != "")
                        {
                            var Email = item["Email"].ToString().ToUpper();
                            if (db.DM_CanBo.Where(_ => _.Email.ToUpper() == Email).Count() == 0)
                            {
                                var canbo = db.DM_CanBo.Find(UserID).Ma_CoQuan_DonVi;
                                var coquan = db.DM_CoQuanDonVi.Find(canbo);
                                success++;
                                worksheet1.Cells[row1, col1 + 1].Value = STT1;
                                worksheet1.Cells[row1, col1 + 1].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                                worksheet1.Cells[row1, col1 + 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                worksheet2.Cells[row2, col2 + 1].Value = STT2;
                                worksheet2.Cells[row2, col2 + 1].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                                worksheet2.Cells[row2, col2 + 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                worksheet1.Cells[row1, col1 + 2].Value = item["Họ Và Tên"].ToString();
                                worksheet1.Cells[row1, col1 + 2].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                                worksheet1.Cells[row1, col1 + 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                worksheet2.Cells[row2, col2 + 2].Value = item["Họ Và Tên"].ToString();
                                worksheet2.Cells[row2, col2 + 2].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                                worksheet2.Cells[row2, col2 + 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                worksheet1.Cells[row1, col1 + 3].Value = item["Ngày Sinh"].ToString();
                                worksheet1.Cells[row1, col1 + 3].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                                worksheet1.Cells[row1, col1 + 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                worksheet2.Cells[row2, col2 + 3].Value = item["Ngày Sinh"].ToString();
                                worksheet2.Cells[row2, col2 + 3].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                                worksheet2.Cells[row2, col2 + 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                worksheet1.Cells[row1, col1 + 4].Value = item["Chức Vụ/ Chức Danh Công Tác"].ToString();
                                worksheet1.Cells[row1, col1 + 4].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                                worksheet1.Cells[row1, col1 + 4].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                worksheet2.Cells[row2, col2 + 4].Value = item["Chức Vụ/ Chức Danh Công Tác"].ToString();
                                worksheet2.Cells[row2, col2 + 4].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                                worksheet2.Cells[row2, col2 + 4].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                worksheet2.Cells[row2, col2 + 5].Value = coquan.Ten;
                                worksheet2.Cells[row2, col2 + 5].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                                worksheet2.Cells[row2, col2 + 5].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                worksheet1.Cells[row1, col1 + 5].Value = item["Email"].ToString();
                                worksheet1.Cells[row1, col1 + 5].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                                worksheet1.Cells[row1, col1 + 5].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                worksheet2.Cells[row2, col2 + 5].Value = item["Email"].ToString();
                                worksheet2.Cells[row2, col2 + 5].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                                worksheet2.Cells[row2, col2 + 5].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;


                                worksheet1.Cells[row1, col1 + 6].Value = "Import Thành Công";
                                worksheet1.Cells[row1, col1 + 6].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                                worksheet1.Cells[row1, col1 + 6].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                var ChucVuChucDanh = item["Chức Vụ/ Chức Danh Công Tác"].ToString().ToUpper();
                                var cvcd = db.DM_ChucVu_ChucDanh.Where(_ => _.Ten_ChucVu_ChucDanh.ToUpper() == ChucVuChucDanh);
                                var id_cvcd = 0;
                                if (cvcd.Count() == 0)
                                {
                                    var CV = new DM_ChucVu_ChucDanh();
                                    CV.Ten_ChucVu_ChucDanh = item["Chức Vụ/ Chức Danh Công Tác"].ToString();
                                    db.DM_ChucVu_ChucDanh.Add(CV);
                                    db.SaveChanges();
                                    id_cvcd = CV.Ma_ChucVu_ChucDanh;
                                }
                                else
                                {
                                    var CV = cvcd.First();
                                    id_cvcd = CV.Ma_ChucVu_ChucDanh;
                                }

                                var cb = new DM_CanBo();
                                cb.HoTen = item["Họ Và Tên"].ToString();
                                cb.DoB = Convert.ToDateTime(item["Ngày Sinh"].ToString()).ToString("dd/MM/yyyy");
                                cb.Ma_ChucVu_ChucDanh = id_cvcd;
                                cb.Ma_CoQuan_DonVi = coquan.Ma_CoQuan_DonVi;
                                string[] words = item["Họ Và Tên"].ToString().Split(' ');
                                cb.Ten = words[words.Length - 1];
                                cb.Email = item["Email"].ToString();
                                cb.IsActive = true;
                                db.DM_CanBo.Add(cb);
                                db.SaveChanges();

                                var taikhoan = new HT_TaiKhoan();

                                var maCanBo = cb.Ma_CanBo;
                                var tenTaiKhoan = string.Concat(UnsignedVietnameseString(cb.HoTen) + cb.Ma_CanBo.ToString());
                                var matKhau = GetRandomPassword(8);
                                var maNhomTaiKhoan = 35;
                                bool trangThai = true;
                                var nguoiTao = UserName;
                                var ngayTao = DateTime.Now;

                                taikhoan.Ma_CanBo = maCanBo;
                                taikhoan.TenTaiKhoan = tenTaiKhoan;
                                taikhoan.MatKhau = matKhau;
                                taikhoan.MaNhomTaiKhoan = maNhomTaiKhoan;
                                taikhoan.TrangThai = trangThai;
                                taikhoan.NguoiTao = nguoiTao;
                                taikhoan.NgayTao = ngayTao;
                                taikhoan.CheckPass = false;
                                
                                db.HT_TaiKhoan.Add(taikhoan);
                                db.SaveChanges();

                                worksheet1.Cells[row1, col1 + 7].Value = taikhoan.TenTaiKhoan;
                                worksheet1.Cells[row1, col1 + 7].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                                worksheet1.Cells[row1, col1 + 7].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                worksheet2.Cells[row2, col2 + 6].Value = taikhoan.TenTaiKhoan;
                                worksheet2.Cells[row2, col2 + 6].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                                worksheet2.Cells[row2, col2 + 6].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                worksheet1.Cells[row1, col1 + 8].Value = taikhoan.MatKhau;
                                worksheet1.Cells[row1, col1 + 8].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                                worksheet1.Cells[row1, col1 + 8].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                worksheet2.Cells[row2, col2 + 7].Value = taikhoan.MatKhau;
                                worksheet2.Cells[row2, col2 + 7].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                                worksheet2.Cells[row2, col2 + 7].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                row1++;
                                row2++;
                                STT1++;
                                STT2++;
                            }
                            else
                            {
                                fail++;
                                worksheet1.Cells[row1, col1 + 1].Value = STT1;
                                worksheet1.Cells[row1, col1 + 1].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                                worksheet1.Cells[row1, col1 + 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                worksheet1.Cells[row1, col1 + 2].Value = item["Họ Và Tên"].ToString();
                                worksheet1.Cells[row1, col1 + 2].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                                worksheet1.Cells[row1, col1 + 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                worksheet1.Cells[row1, col1 + 3].Value = item["Ngày Sinh"].ToString();
                                worksheet1.Cells[row1, col1 + 3].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                                worksheet1.Cells[row1, col1 + 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                worksheet1.Cells[row1, col1 + 4].Value = item["Chức Vụ/ Chức Danh Công Tác"].ToString();
                                worksheet1.Cells[row1, col1 + 4].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                                worksheet1.Cells[row1, col1 + 4].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                worksheet1.Cells[row1, col1 + 5].Value = item["Email"].ToString();
                                worksheet1.Cells[row1, col1 + 5].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                                worksheet1.Cells[row1, col1 + 5].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                worksheet1.Cells[row1, col1 + 6].Value = "Email Đã Tồn Tại";
                                worksheet1.Cells[row1, col1 + 6].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                                worksheet1.Cells[row1, col1 + 6].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                worksheet1.Cells[row1, col1 + 7].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                                worksheet1.Cells[row1, col1 + 7].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                worksheet1.Cells[row1, col1 + 8].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                                worksheet1.Cells[row1, col1 + 8].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                row1++;
                                STT1++;
                            }


                        }

                    }

                    stream.Close();

                    string filename1 = "KetQua_" + filename;
                    TenFileKetQua = filename1;
                    string targetpath1 = Server.MapPath("~/Content/Uploads/");

                    FileInfo fi = new FileInfo(targetpath1 + filename1);
                    package.SaveAs(fi);

                    string filename2 = "TaiKhoan_" + filename;
                    FileInfo fi1 = new FileInfo(targetpath1 + filename2);
                    package1.SaveAs(fi1);

                    var ctk = new NV_CapTaiKhoan();
                    ctk.FileCap = filename2;
                    ctk.NgayCap = DateTime.Now;
                    ctk.NguoiCap = UserID;

                    db.NV_CapTaiKhoan.Add(ctk);
                    db.SaveChanges();
                }
                var data = new
                {
                    success = success,
                    fail = fail,
                    TenFileKetQua = TenFileKetQua
                };
                return Json(data, JsonRequestBehavior.AllowGet);

            }
            return Json(false, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ExportCanBo()
        {
            var Role = user.GetRole();
            var coquan = user.GetUserCoQuan();
            var data = (from cb in db.DM_CanBo
                        join cq in db.DM_CoQuanDonVi on cb.Ma_CoQuan_DonVi equals cq.Ma_CoQuan_DonVi
                        join cv in db.DM_ChucVu_ChucDanh on cb.Ma_ChucVu_ChucDanh equals cv.Ma_ChucVu_ChucDanh
                        join tk in db.HT_TaiKhoan on cb.Ma_CanBo equals tk.Ma_CanBo
                        join ntk in db.HT_NhomTaiKhoan on tk.MaNhomTaiKhoan equals ntk.MaNhomTaiKhoan
                        orderby ntk.Sort ascending
                        select new {cb.Ma_CanBo, cb.HoTen, cb.DoB, cb.SoCCCD, cq.Ma_CoQuan_DonVi, cq.Ten, cv.Ma_ChucVu_ChucDanh, cv.Ten_ChucVu_ChucDanh, tk.TenTaiKhoan, tk.MatKhau, ntk.MaNhomTaiKhoan, ntk.TenNhomTaiKhoan, ntk.MaTaiKhoan}).ToList();

            string filename = System.Guid.NewGuid().ToString() + ".xlsx";
            string targetpath = Server.MapPath("~/Content/uploads/");
            var fileName = "ExcelData.xlsx";
            var file = new FileInfo(fileName);
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            var package = new ExcelPackage(file);
            var worksheet1 = package.Workbook.Worksheets.Add("Sheet1");
            var col1 = 0;
            var row1 = 2;
            var STT1 = 1;
            worksheet1.Column(1).Width = GetTrueColumnWidth(3.67);
            worksheet1.Column(2).Width = GetTrueColumnWidth(28.11);
            worksheet1.Column(3).Width = GetTrueColumnWidth(26.67);
            worksheet1.Column(4).Width = GetTrueColumnWidth(44.11);
            worksheet1.Column(5).Width = GetTrueColumnWidth(35.67);
            worksheet1.Column(6).Width = GetTrueColumnWidth(44.11);
            worksheet1.Column(7).Width = GetTrueColumnWidth(44.11);
            worksheet1.Column(8).Width = GetTrueColumnWidth(32.44);
            worksheet1.Column(9).Width = GetTrueColumnWidth(32.44);
            worksheet1.Column(10).Width = GetTrueColumnWidth(32.44);


            worksheet1.Cells["A1"].Value = "STT";
            worksheet1.Cells["A1"].Style.Font.Bold = true;
            worksheet1.Cells["A1"].Style.Border.BorderAround(ExcelBorderStyle.Thin);
            worksheet1.Cells["A1"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

            worksheet1.Cells["B1"].Value = "Họ Và Tên";
            worksheet1.Cells["B1"].Style.Font.Bold = true;
            worksheet1.Cells["B1"].Style.Border.BorderAround(ExcelBorderStyle.Thin);
            worksheet1.Cells["B1"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

            worksheet1.Cells["C1"].Value = "Ngày Sinh";
            worksheet1.Cells["C1"].Style.Font.Bold = true;
            worksheet1.Cells["C1"].Style.Border.BorderAround(ExcelBorderStyle.Thin);
            worksheet1.Cells["C1"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

            worksheet1.Cells["D1"].Value = "Chức Vụ/ Chức Danh Công Tác";
            worksheet1.Cells["D1"].Style.Font.Bold = true;
            worksheet1.Cells["D1"].Style.Border.BorderAround(ExcelBorderStyle.Thin);
            worksheet1.Cells["D1"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

            worksheet1.Cells["E1"].Value = "Cơ Quan Đơn Vị";
            worksheet1.Cells["E1"].Style.Font.Bold = true;
            worksheet1.Cells["E1"].Style.Border.BorderAround(ExcelBorderStyle.Thin);
            worksheet1.Cells["E1"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

            worksheet1.Cells["F1"].Value = "Nhóm Tài Khoản";
            worksheet1.Cells["F1"].Style.Font.Bold = true;
            worksheet1.Cells["F1"].Style.Border.BorderAround(ExcelBorderStyle.Thin);
            worksheet1.Cells["F1"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

            worksheet1.Cells["G1"].Value = "Tài Khoản";
            worksheet1.Cells["G1"].Style.Font.Bold = true;
            worksheet1.Cells["G1"].Style.Border.BorderAround(ExcelBorderStyle.Thin);
            worksheet1.Cells["G1"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

            worksheet1.Cells["H1"].Value = "Mật Khẩu";
            worksheet1.Cells["H1"].Style.Font.Bold = true;
            worksheet1.Cells["H1"].Style.Border.BorderAround(ExcelBorderStyle.Thin);
            worksheet1.Cells["H1"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            

            if (Role == "NDDTTT" || Role == "ADMIN")
            {
                foreach (var item in data)
                {
                    worksheet1.Cells[row1, col1 + 1].Value = STT1;
                    worksheet1.Cells[row1, col1 + 1].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                    worksheet1.Cells[row1, col1 + 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                    worksheet1.Cells[row1, col1 + 2].Value = item.HoTen.ToString();
                    worksheet1.Cells[row1, col1 + 2].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                    worksheet1.Cells[row1, col1 + 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                    worksheet1.Cells[row1, col1 + 3].Value = item.DoB.ToString();
                    worksheet1.Cells[row1, col1 + 3].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                    worksheet1.Cells[row1, col1 + 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                    worksheet1.Cells[row1, col1 + 4].Value = item.Ten_ChucVu_ChucDanh.ToString();
                    worksheet1.Cells[row1, col1 + 4].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                    worksheet1.Cells[row1, col1 + 4].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                    worksheet1.Cells[row1, col1 + 5].Value = item.Ten.ToString();
                    worksheet1.Cells[row1, col1 + 5].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                    worksheet1.Cells[row1, col1 + 5].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                    worksheet1.Cells[row1, col1 + 6].Value = item.TenNhomTaiKhoan.ToString();
                    worksheet1.Cells[row1, col1 + 6].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                    worksheet1.Cells[row1, col1 + 6].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                    worksheet1.Cells[row1, col1 + 7].Value = item.TenTaiKhoan.ToString();
                    worksheet1.Cells[row1, col1 + 7].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                    worksheet1.Cells[row1, col1 + 7].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                    worksheet1.Cells[row1, col1 + 8].Value = item.MatKhau.ToString();
                    worksheet1.Cells[row1, col1 + 8].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                    worksheet1.Cells[row1, col1 + 8].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    row1++;
                    STT1++;
                }
            }
            else if (Role == "NDDCSBN" || Role == "PLD" || Role == "NQTCSBN")
            {
                data = data.Where(_ => _.Ma_CoQuan_DonVi == coquan && _.MaTaiKhoan != "NDDTTT").ToList();
                foreach (var item in data)
                {
                    worksheet1.Cells[row1, col1 + 1].Value = STT1;
                    worksheet1.Cells[row1, col1 + 1].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                    worksheet1.Cells[row1, col1 + 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                    worksheet1.Cells[row1, col1 + 2].Value = item.HoTen.ToString();
                    worksheet1.Cells[row1, col1 + 2].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                    worksheet1.Cells[row1, col1 + 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                    worksheet1.Cells[row1, col1 + 3].Value = item.DoB.ToString();
                    worksheet1.Cells[row1, col1 + 3].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                    worksheet1.Cells[row1, col1 + 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                    worksheet1.Cells[row1, col1 + 4].Value = item.Ten_ChucVu_ChucDanh.ToString();
                    worksheet1.Cells[row1, col1 + 4].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                    worksheet1.Cells[row1, col1 + 4].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                    worksheet1.Cells[row1, col1 + 5].Value = item.Ten.ToString();
                    worksheet1.Cells[row1, col1 + 5].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                    worksheet1.Cells[row1, col1 + 5].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                    worksheet1.Cells[row1, col1 + 6].Value = item.TenNhomTaiKhoan.ToString();
                    worksheet1.Cells[row1, col1 + 6].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                    worksheet1.Cells[row1, col1 + 6].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                    worksheet1.Cells[row1, col1 + 7].Value = item.TenTaiKhoan.ToString();
                    worksheet1.Cells[row1, col1 + 7].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                    worksheet1.Cells[row1, col1 + 7].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                    worksheet1.Cells[row1, col1 + 8].Value = item.MatKhau.ToString();
                    worksheet1.Cells[row1, col1 + 8].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                    worksheet1.Cells[row1, col1 + 8].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    row1++;
                    STT1++;
                }
            }
          

            string filename1 = "KetQua_" + filename;
            string targetpath1 = Server.MapPath("~/Content/Uploads/");

            FileInfo fi = new FileInfo(targetpath1 + filename1);
            package.SaveAs(fi);
            var exprotfile = filename1.Split('.');
            string namefileExport = "";
            for(var i = 0; i < exprotfile.Count() - 1; i++)
            {
                namefileExport += exprotfile[i].ToString();
            }
            return Json(namefileExport, JsonRequestBehavior.AllowGet);
        }


        public FileResult TaiFileNhapCanBoMau()
        {
            var Role = user.GetRole();

            if (Role.ToString().ToUpper() == "ADMIN")
            {
                var url = Path.Combine(Server.MapPath("~/Content/import"), "FileNhapCanBoMau_QuanTri.xlsx");
                byte[] fileBytes = System.IO.File.ReadAllBytes(url);
                string fileName = "FileNhapCanBoMau_QuanTri.xlsx";
                return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
            }
            else if (Role.ToString().ToUpper() == "TTKSTN")
            {
                var url = Path.Combine(Server.MapPath("~/Content/import"), "FileNhapCanBoMau_ThanhTra.xlsx");
                byte[] fileBytes = System.IO.File.ReadAllBytes(url);
                string fileName = "FileNhapCanBoMau_ThanhTra.xlsx";
                return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
            }
            else if (Role.ToString().ToUpper() == "NDD")
            {
                var url = Path.Combine(Server.MapPath("~/Content/import"), "FileNhapCanBoMau_CanBo.xlsx");
                byte[] fileBytes = System.IO.File.ReadAllBytes(url);
                string fileName = "FileNhapCanBoMau_CanBo.xlsx";
                return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
            }
            else
            {
                var url = Path.Combine(Server.MapPath("~/Content/import"), "FileNhapCanBoMau_CanBo.xlsx");
                byte[] fileBytes = System.IO.File.ReadAllBytes(url);
                string fileName = "FileNhapCanBoMau.xlsx";
                return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
            }
        }

        public JsonResult GetDanhSachCanBoDuocXacMinh()
        {
            var socoquancanxacminh = Math.Round((double)(db.DM_CoQuanDonVi.Count() * 0.2));


            if (socoquancanxacminh == 0 && db.DM_CoQuanDonVi.Count() != 0) socoquancanxacminh = 1;

            var listMaCQDV = db.DM_CoQuanDonVi.OrderBy(n => Guid.NewGuid()).Take(Int32.Parse(socoquancanxacminh.ToString())).Select(_ => _.Ma_CoQuan_DonVi).ToList();
            IEnumerable<DM_CanBo> listcanbo = new List<DM_CanBo>();

            IEnumerable<DM_CoQuanDonVi> listCoQuanDonVi = new List<DM_CoQuanDonVi>();

            foreach (var item in listMaCQDV)
            {
                var CoQuan = db.DM_CoQuanDonVi.Where(_ => _.Ma_CoQuan_DonVi == item);
                var soluongcanbocanlay = Math.Round(db.DM_CanBo.Where(_ => _.Ma_CoQuan_DonVi == item).Count() * 0.1);
                if (soluongcanbocanlay == 0 && db.DM_CanBo.Count() != 0) soluongcanbocanlay = 1;

                var idnguoidungdau = db.DM_CanBo.Where(_ => _.Ma_CoQuan_DonVi == item && (_.Ma_ChucVu_ChucDanh == 1 || _.Ma_ChucVu_ChucDanh == 6)).OrderBy(n => Guid.NewGuid()).Take(Int32.Parse(1.ToString())).Select(_ => _.Ma_CanBo).SingleOrDefault();
                var datanguoidungdau = db.DM_CanBo.Where(_ => _.Ma_CanBo == idnguoidungdau);
                listcanbo = listcanbo.Concat(datanguoidungdau);
                soluongcanbocanlay = soluongcanbocanlay - 1;
                var data = db.DM_CanBo.Where(_ => _.Ma_CoQuan_DonVi == item && _.Ma_CanBo != idnguoidungdau && _.IsActive == true).OrderBy(n => Guid.NewGuid()).Take(Int32.Parse(soluongcanbocanlay.ToString())).ToList();
                listcanbo = listcanbo.Concat(data);
                listCoQuanDonVi = listCoQuanDonVi.Concat(CoQuan);
            }
            var data2 = (from cq in listCoQuanDonVi
                            join lcq in db.DM_Loai_CoQuan_DonVi on cq.MaLoai_CoQuan_DonVi equals lcq.Ma_Loai_CQDV
                            
                            select new { cq.Ten }
                        );

            var data1 = (from cb in listcanbo
                                //join px in db.DM_PhuongXa on cb.Ma_PhuongXa_TT equals px.Ma_PhuongXa
                                //join qh in db.DM_QuanHuyen on cb.Ma_QuanHuyen equals qh.Ma_QuanHuyen
                                //join tt in db.DM_TinhThanh on cb.Ma_TinhThanh equals tt.Ma_TinhThanh
                            join cq in db.DM_CoQuanDonVi on cb.Ma_CoQuan_DonVi equals cq.Ma_CoQuan_DonVi
                            join cv in db.DM_ChucVu_ChucDanh on cb.Ma_ChucVu_ChucDanh equals cv.Ma_ChucVu_ChucDanh
                            orderby cb.Ma_ChucVu_ChucDanh descending
                         where cb.IsActive == true
                         select new { MaCanBo = cb.Ma_CanBo, HoTenCanBo = cb.HoTen, NgaySinh = cb.DoB, CCCD = cb.SoCCCD, TenCoQuan = cq.Ten, ChucVu = cv.Ten_ChucVu_ChucDanh }).ToList();

            return Json(new { data1, data2 }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult EmailTonTai_DoiEmail(string email2)
        {
            var canbo = db.DM_CanBo.Where(_ => _.Email == email2 && _.IsActive ==true).Count();

            if (canbo > 0)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(true, JsonRequestBehavior.AllowGet);
            }
        }


        public JsonResult DoiEmail(int idCanBo, string email1, string email2, string MatKhau1)
        {
            var canbo = db.DM_CanBo.Find(idCanBo);

            var checkmatkhau = db.HT_TaiKhoan.Where(_ => _.Ma_CanBo == canbo.Ma_CanBo).FirstOrDefault();

            if (checkmatkhau.MatKhau != MatKhau1)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
            else
            {
                canbo.Email = email2;
                db.Entry(canbo).State = EntityState.Modified;
                db.SaveChanges();
                return Json(true, JsonRequestBehavior.AllowGet);
            }
        }

        public FileResult Download(string TenFile)
        {
            TenFile = TenFile + ".xlsx";
            var url = Path.Combine(Server.MapPath("~/Content/uploads/"), TenFile);
            byte[] fileBytes = System.IO.File.ReadAllBytes(url);
            string fileName = TenFile;
            return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
        }

        public static double GetTrueColumnWidth(double width)
        {
            //DEDUCE WHAT THE COLUMN WIDTH WOULD REALLY GET SET TO
            double z = 1d;
            if (width >= (1 + 2 / 3))
            {
                z = Math.Round((Math.Round(7 * (width - 1 / 256), 0) - 5) / 7, 2);
            }
            else
            {
                z = Math.Round((Math.Round(12 * (width - 1 / 256), 0) - Math.Round(5 * width, 0)) / 12, 2);
            }

            //HOW FAR OFF? (WILL BE LESS THAN 1)
            double errorAmt = width - z;

            //CALCULATE WHAT AMOUNT TO TACK ONTO THE ORIGINAL AMOUNT TO RESULT IN THE CLOSEST POSSIBLE SETTING 
            double adj = 0d;
            if (width >= (1 + 2 / 3))
            {
                adj = (Math.Round(7 * errorAmt - 7 / 256, 0)) / 7;
            }
            else
            {
                adj = ((Math.Round(12 * errorAmt - 12 / 256, 0)) / 12) + (2 / 12);
            }

            //RETURN A SCALED-VALUE THAT SHOULD RESULT IN THE NEAREST POSSIBLE VALUE TO THE TRUE DESIRED SETTING
            if (z > 0)
            {
                return width + adj + 0.11;
            }

            return 0d;
        }

        public static string UnsignedVietnameseString(string str)
        {
            Regex regex = new Regex("\\p{IsCombiningDiacriticalMarks}+");
            string temp = str.Replace("Đ","D").Replace("đ","d").Normalize(NormalizationForm.FormD);
            return regex.Replace(temp, String.Empty).Replace('\u0111', 'd').Replace('\u0110', 'D').Replace(" ", "").Replace("ð", "d").ToLower();
        }

        protected string RenderRazorViewToString(string viewName, object model)
        {
            if (model != null)
            {
                ViewData.Model = model;
            }
            using (StringWriter sw = new StringWriter())
            {
                ViewEngineResult viewResult = ViewEngines.Engines.FindPartialView(ControllerContext, viewName);
                ViewContext viewContext = new ViewContext(ControllerContext, viewResult.View, ViewData, TempData, sw);
                viewResult.View.Render(viewContext, sw);
                return sw.GetStringBuilder().ToString();
            }
        }

        // Tạo mật khẩu tự động
        public static string GetRandomPassword(int length)
        {
            const string chars = "0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";

            StringBuilder sb = new StringBuilder();
            Random rnd = new Random();

            for (int i = 0; i < length; i++)
            {
                int index = rnd.Next(chars.Length);
                sb.Append(chars[index]);
            }

            return sb.ToString();
        }

        [HttpPost]
        public JsonResult EmailTonTai1(string Email, int Ma_CanBo)
        {
            var flag = db.DM_CanBo.Where(_ => _.Ma_CanBo != Ma_CanBo).Select(_ => _.Email).Contains(Email);
            return Json(!flag, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult EmailTonTai(string Email)
        {
            var flag = db.DM_CanBo.Select(_ => _.Email).Contains(Email);
            return Json(!flag, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult RandomTaiKhoan(string HoTen)
        {
            Random _random = new Random();
            var TenTaiKhoan = string.Concat(UnsignedVietnameseString(HoTen) + _random.Next(0, 9999));
            return Json(TenTaiKhoan, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetRole(string ChucNangCode)
        {
            var username = user.GetUserNameAccount();
            var ntk = db.HT_TaiKhoan.FirstOrDefault(_ => _.TenTaiKhoan == username).MaTaiKhoan;

            var data = db.HT_ChiTietPhanQuyen.Where(_ => _.MaTaiKhoan == ntk && _.ChucNangCode.ToUpper() == ChucNangCode.ToUpper());

            return Json(data, JsonRequestBehavior.AllowGet);
        }

    }
}
