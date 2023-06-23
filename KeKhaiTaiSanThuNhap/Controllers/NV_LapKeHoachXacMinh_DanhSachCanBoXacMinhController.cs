using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using DocumentFormat.OpenXml.Drawing.Charts;
using iText.Html2pdf;
using iText.Html2pdf.Resolver.Font;
using KeKhaiTaiSanThuNhap.Models;

namespace KeKhaiTaiSanThuNhap.Controllers
{
    public class NV_LapKeHoachXacMinh_DanhSachCanBoXacMinhController : Controller
    {
        private KSTNEntities db = new KSTNEntities();
        private UserInfo user = new UserInfo();

        // GET: NV_LapKeHoachXacMinh_DanhSachCanBoXacMinh
        public ActionResult Index()
        {
            if (user.CheckQuyen("NV_LapKeHoachXacMinh_DanhSachCanBoXacMinh", "Xem"))
            {
                return new HttpStatusCodeResult(404, "Not found");
            }
            return View();
        }

        public ActionResult LapDanhSach(int id)
        {
            if (user.CheckQuyen("NV_LapKeHoachXacMinh_DanhSachCanBoXacMinh", "LapDanhSach"))
            {
                return new HttpStatusCodeResult(404, "Not found");
            }
            var kehoachnam = db.NV_LapKeHoachXacMinh_DanhSachCanBoXacMinh.Find(id).KeHoachNam;
            ViewBag.KeHoachNam = kehoachnam;
            ViewBag.ID_DanhSach = id;
            return View();
        }

        public ActionResult InDanhSach(int id)
        {
            if (user.CheckQuyen("NV_LapKeHoachXacMinh_DanhSachCanBoXacMinh", "Xuat"))
            {
                return new HttpStatusCodeResult(404, "Not found");
            }
            var data = db.NV_LapKeHoachXacMinh_DanhSachCanBoXacMinh.Find(id);
            var ID_DanhSach = data.ID_DanhSach;
            var NamKeHoach = data.KeHoachNam;
            var MaCanBo = user.GetUser();
            var maCoQuan = user.GetUserCoQuan();
            var CoQuan = db.DM_CoQuanDonVi.Find(maCoQuan);
            var LoaiCoQuan = db.DM_Loai_CoQuan_DonVi.Where(_ => _.Ma_Loai_CQDV == CoQuan.MaLoai_CoQuan_DonVi).Select(_ => _.Ten_Loai_CQDV).FirstOrDefault();
            var now = DateTime.Now;
            var DSCBLanDau = (from dsct in db.NV_LapKeHoachXacMinh_DanhSachCanBoXacMinh_ChiTiet
                              join cb in db.DM_CanBo on dsct.Ma_CanBo equals cb.Ma_CanBo
                              join cv in db.DM_ChucVu_ChucDanh on cb.Ma_ChucVu_ChucDanh equals cv.Ma_ChucVu_ChucDanh
                              join cq in db.DM_CoQuanDonVi on cb.Ma_CoQuan_DonVi equals cq.Ma_CoQuan_DonVi
                              join tk in db.HT_TaiKhoan on cb.Ma_CanBo equals tk.Ma_CanBo
                              join ntk in db.HT_NhomTaiKhoan on tk.MaNhomTaiKhoan equals ntk.MaNhomTaiKhoan
                              where dsct.ID_DanhSach == id
                              orderby cq.Ma_CoQuan_DonVi, ntk.Sort, cb.Ten
                              select new { cb.HoTen, cb.DoB, cv.Ten_ChucVu_ChucDanh, cq.Ten }).ToList();

            var StringDSCB = "";
            var dem = 0;

            foreach (var i in DSCBLanDau)
            {
                dem++;
                StringDSCB += @"<tr style='height:15pt'>
                                    <td
                                        style='width:5%; border-width:0.5pt; border-style:solid;  padding-right:5.03pt; padding-left:5.03pt; vertical-align:middle; background-color:#ffffff; -aw-border-bottom:0.5pt single; -aw-border-right:0.5pt single'>
                                        <p style='text-align:center; line-height:150%; font-size:10pt'><span>" + dem + @"</span></p>
                                    </td>
                                    <td
                                        style='width:20%; border-width:0.5pt;  border-style:solid; padding-right:5.03pt; padding-left:5.4pt; vertical-align:middle; background-color:#ffffff; -aw-border-bottom:0.5pt single; -aw-border-right:0.5pt single'>
                                        <p style='line-height:150%; font-size:10pt'><span>" + i.HoTen + @"</span></p>
                                    </td>
                                    <td
                                        style='width:10%; border-width:0.5pt;   border-style:solid;  padding-right:5.03pt; padding-left:5.4pt; vertical-align:middle; background-color:#ffffff; -aw-border-bottom:0.5pt single; -aw-border-right:0.5pt single'>
                                        <p style='text-align:center; line-height:150%; font-size:10pt'><span>" + i.DoB.Split(' ')[0] + @"</span></p>
                                    </td>
                                    <td
                                        style='width:20%;  border-width:0.5pt;  border-style:solid; padding-right:5.03pt; padding-left:5.4pt; vertical-align:middle; background-color:#ffffff; -aw-border-bottom:0.5pt single; -aw-border-right:0.5pt single'>
                                        <p style='text-align:center; line-height:150%; font-size:10pt'><span>" + i.Ten_ChucVu_ChucDanh + @"</span></p>
                                    </td>
                                    <td
                                        style='width:30%; border-width:0.5pt;  border-style:solid; padding-right:5.03pt; padding-left:5.4pt; vertical-align:middle; background-color:#ffffff; -aw-border-bottom:0.5pt single; -aw-border-right:0.5pt single'>
                                        <p style='text-align:center; line-height:150%; font-size:10pt'><span>" + i.Ten + @"</span></p>
                                    </td>
                                    <td
                                        style='width:15%; border-style:solid; border-width:0.5pt;  padding-right:5.03pt;  padding-left:5.4pt; vertical-align:middle; background-color:#ffffff; -aw-border-bottom:0.5pt single'>
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
                                    
                                    <p style='margin-top:6pt; margin-bottom:14pt; text-align:center'><span
                                            style='font-weight:bold; -aw-import:ignore'>&#xa0;</span></p>
                                    <p style='text-align:center; line-height:150%'><span style='font-weight:bold'>DANH SÁCH CÁN BỘ ĐƯỢC XÁC MINH TÀI SẢN, THU NHẬP</span></p>
                                    <p style='text-align:center; line-height:150%'><span style='font-weight:bold'>NĂM " + NamKeHoach + @"</span></p>
                                   
                                    <p style='margin-top:6pt; margin-bottom:14pt; text-align:center'><span
                                            style='font-style:italic; -aw-import:ignore'>&#xa0;</span></p>
                                    <table cellspacing='0' cellpadding='0'
                                        style='width:100%; border:0.75pt solid #000000; -aw-border:0.5pt single; border-collapse:collapse'>
                                        <tr style='height:25.5pt'>
                                            <td
                                                style='width:5%; border-style:solid; border-width:0.5pt;  padding-right:5.03pt; padding-left:5.03pt; vertical-align:middle; background-color:#ffffff; -aw-border-right:0.5pt single'>
                                                <p style='text-align:center; line-height:150%; font-size:10pt'><span
                                                        style='font-weight:bold'>STT</span></p>
                                            </td>
                                            <td
                                                style='width:20%; border-style:solid;  border-width:0.5pt;  padding-right:5.03pt; padding-left:5.4pt; vertical-align:middle; background-color:#ffffff; -aw-border-right:0.5pt single'>
                                                <p style='text-align:center; line-height:150%; font-size:10pt'><span style='font-weight:bold'>Họ và
                                                        tên</span></p>
                                            </td>
                                            <td
                                                style='width:10%; border-style:solid;  border-width:0.5pt;padding-right:5.03pt; padding-left:5.4pt; vertical-align:middle; background-color:#ffffff; -aw-border-bottom:0.5pt single; -aw-border-right:0.5pt single'>
                                                <p style='text-align:center; line-height:150%; font-size:10pt'><span style='font-weight:bold'>Ngày
                                                        tháng năm sinh</span></p>
                                            </td>
                                            <td
                                                style='width:20%; border-style:solid;  border-width:0.5pt;  padding-right:5.03pt; padding-left:5.4pt; vertical-align:middle; background-color:#ffffff; -aw-border-bottom:0.5pt single; -aw-border-right:0.5pt single'>
                                                <p style='text-align:center; line-height:150%; font-size:10pt'><span style='font-weight:bold'>Chức
                                                        vụ/ chức danh công tác</span></p>
                                            </td>
                                            <td
                                                style='width:30%; border-style:solid;  border-width:0.5pt;   padding-right:5.03pt; padding-left:5.4pt; vertical-align:middle; background-color:#ffffff; -aw-border-bottom:0.5pt single; -aw-border-right:0.5pt single'>
                                                <p style='text-align:center; line-height:150%; font-size:10pt'><span style='font-weight:bold'>Cơ
                                                        quan/đơn vị công tác</span></p>
                                            </td>
                                            <td
                                                style='width:25%; border-style:solid; border-width:0.5pt;  padding-left:5.4pt; vertical-align:middle; background-color:#ffffff'>
                                                <p style='text-align:center; line-height:150%; font-size:10pt'><span style='font-weight:bold'>Ghi
                                                        chú</span></p>
                                            </td>
                                        </tr>
                                        
                                        
                                       " + StringDSCB + @"
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

        public ActionResult InDanhSachThongKe(string DanhSachCoQuan, int MaDanhSachCanBoXacMinh, string SoLanhDao, string SoNhanVien)
        {
            var listcanbo = new List<CanBoKeKhai>();

            // lấy mã kế hoạch
            var kehoachnam = db.NV_LapKeHoachXacMinh_DanhSachCanBoXacMinh.Find(MaDanhSachCanBoXacMinh).KeHoachNam;
            var kehoachkekhai = db.NV_LapKeHoachKeKhai.Where(_ => _.KeHoachNam == kehoachnam && _.Ma_Loai_KeKhai == 4).Select(_ => _.MaKeHoachKeKhai).ToList();

            //danh sách cơ quan đã lập kế hoạch kê khai
            var dscqdalapkehoach = (from cq in db.DM_CoQuanDonVi
                                    join khkk in db.NV_LapKeHoachKeKhai on cq.Ma_CoQuan_DonVi equals khkk.Ma_CoQuan_DonVi
                                    where khkk.Ma_Loai_KeKhai == 4
                                    select cq.MaLoai_CoQuan_DonVi).ToList();

            //danh sáng cán bộ đã lập kế hoạch kê khai
            var dscbdalapkehoachHN = (from cb in db.DM_CanBo
                                      join kkts in db.Nv_KeKhai_TSTN on cb.Ma_CanBo equals kkts.Ma_CanBo
                                      join khkk in db.NV_LapKeHoachKeKhai on kkts.MaKeHoachKeKhai equals khkk.MaKeHoachKeKhai
                                      select cb.Ma_CanBo).ToList();


            var listcoquan = DanhSachCoQuan.Split(',');
            var SoLanhDaoDaChon = SoLanhDao.Split(',');
            var SoNhanVienDaChon = SoNhanVien.Split(',');
            var dem = 0;
            var StringDSCB = "";

            var TongLanhDaoDaChon = 0;
            var TongChuyenVienDaChon = 0;
            var TongCongSoNguoiDuocChon = 0;

            var TongSoLanhDao = 0;
            var TongSoChuyenVien = 0;
            var TongCongSoNguoi = 0;

            foreach (var ma in listcoquan.OfType<string>().Select((x, i) => new { x, i }))
            {
                var MaCoQuan = Int32.Parse(ma.x);
                //var soluong = (int)Math.Round((double)cabo_kkhn / 10);
                var soluongLanhDao = Int32.Parse(SoLanhDaoDaChon[ma.i]);

                var soluongNhanVien = Int32.Parse(SoNhanVienDaChon[ma.i]);

                var tencoquan = db.DM_CoQuanDonVi.Find(MaCoQuan).Ten;
                var TongCanBo = (from cb in db.DM_CanBo
                                 join bkk in db.Nv_KeKhai_TSTN on cb.Ma_CanBo equals bkk.Ma_CanBo
                                 join cqdv in db.DM_CoQuanDonVi on cb.Ma_CoQuan_DonVi equals cqdv.Ma_CoQuan_DonVi
                                 join lcq in db.DM_Loai_CoQuan_DonVi on cqdv.MaLoai_CoQuan_DonVi equals lcq.Ma_Loai_CQDV
                                 join khkkhn in db.NV_LapKeHoachKeKhai_DanhSachKeKhaiHangNam on cqdv.Ma_CoQuan_DonVi equals khkkhn.Ma_CoQuan_DonVi
                                 join khkk in db.NV_LapKeHoachKeKhai on khkkhn.MaKeHoachKeKhai equals khkk.MaKeHoachKeKhai
                                 join khkknhct in db.NV_LapKeHoachKeKhai_DanhSachKeKhaiHangNam_ChiTiet on cb.Ma_CanBo equals khkknhct.Ma_CanBo
                                 where khkkhn.MaKeHoachKeKhai_DanhSachKeKhaiHangNam_ID == khkknhct.MaKeHoachKeKhai_DanhSachKeKhaiHangNam_ID && cqdv.Ma_CoQuan_DonVi == MaCoQuan && bkk.TrangThai == true && khkk.KeHoachNam == kehoachnam && khkk.Ma_Loai_KeKhai == 4 && bkk.Nam_KeKhai == kehoachnam && bkk.Ma_Loai_KeKhai == 4 && bkk.MaKeHoachKeKhai == khkk.MaKeHoachKeKhai
                                 select new { cb.Ma_CanBo }
                             ).Count();
                var TongLanhDao = (from cb in db.DM_CanBo
                                   join bkk in db.Nv_KeKhai_TSTN on cb.Ma_CanBo equals bkk.Ma_CanBo
                                   join tk in db.HT_TaiKhoan on cb.Ma_CanBo equals tk.Ma_CanBo
                                   join ntk in db.HT_NhomTaiKhoan on tk.MaNhomTaiKhoan equals ntk.MaNhomTaiKhoan
                                   join cqdv in db.DM_CoQuanDonVi on cb.Ma_CoQuan_DonVi equals cqdv.Ma_CoQuan_DonVi
                                   join lcq in db.DM_Loai_CoQuan_DonVi on cqdv.MaLoai_CoQuan_DonVi equals lcq.Ma_Loai_CQDV
                                   join khkkhn in db.NV_LapKeHoachKeKhai_DanhSachKeKhaiHangNam on cqdv.Ma_CoQuan_DonVi equals khkkhn.Ma_CoQuan_DonVi
                                   join khkknhct in db.NV_LapKeHoachKeKhai_DanhSachKeKhaiHangNam_ChiTiet on cb.Ma_CanBo equals khkknhct.Ma_CanBo
                                   join khkk in db.NV_LapKeHoachKeKhai on khkkhn.MaKeHoachKeKhai equals khkk.MaKeHoachKeKhai
                                   where khkkhn.MaKeHoachKeKhai_DanhSachKeKhaiHangNam_ID == khkknhct.MaKeHoachKeKhai_DanhSachKeKhaiHangNam_ID && khkk.KeHoachNam == kehoachnam && khkk.Ma_Loai_KeKhai == 4 && bkk.Nam_KeKhai == kehoachnam && bkk.Ma_Loai_KeKhai == 4 && cqdv.Ma_CoQuan_DonVi == MaCoQuan && bkk.TrangThai == true && bkk.MaKeHoachKeKhai == khkk.MaKeHoachKeKhai && (ntk.MaTaiKhoan == "NDDTTT" || ntk.MaTaiKhoan == "NDDCSBN" || ntk.MaTaiKhoan == "PLD")
                                   select new { cb.Ma_CanBo }
                  ).Count();
                var  TongChuyenVien = (from cb in db.DM_CanBo
                                    join bkk in db.Nv_KeKhai_TSTN on cb.Ma_CanBo equals bkk.Ma_CanBo
                                    join tk in db.HT_TaiKhoan on cb.Ma_CanBo equals tk.Ma_CanBo
                                    join ntk in db.HT_NhomTaiKhoan on tk.MaNhomTaiKhoan equals ntk.MaNhomTaiKhoan
                                    join cqdv in db.DM_CoQuanDonVi on cb.Ma_CoQuan_DonVi equals cqdv.Ma_CoQuan_DonVi
                                    join lcq in db.DM_Loai_CoQuan_DonVi on cqdv.MaLoai_CoQuan_DonVi equals lcq.Ma_Loai_CQDV
                                    join khkkhn in db.NV_LapKeHoachKeKhai_DanhSachKeKhaiHangNam on cqdv.Ma_CoQuan_DonVi equals khkkhn.Ma_CoQuan_DonVi
                                    join khkknhct in db.NV_LapKeHoachKeKhai_DanhSachKeKhaiHangNam_ChiTiet on cb.Ma_CanBo equals khkknhct.Ma_CanBo
                                    join khkk in db.NV_LapKeHoachKeKhai on khkkhn.MaKeHoachKeKhai equals khkk.MaKeHoachKeKhai
                                    where khkkhn.MaKeHoachKeKhai_DanhSachKeKhaiHangNam_ID == khkknhct.MaKeHoachKeKhai_DanhSachKeKhaiHangNam_ID && khkk.KeHoachNam == kehoachnam && khkk.Ma_Loai_KeKhai == 4 && bkk.Nam_KeKhai == kehoachnam && bkk.Ma_Loai_KeKhai == 4 && cqdv.Ma_CoQuan_DonVi == MaCoQuan && bkk.TrangThai == true && bkk.MaKeHoachKeKhai == khkk.MaKeHoachKeKhai && (ntk.MaTaiKhoan == "CBKKTSTN" || ntk.MaTaiKhoan == "NQTCSBN")
                                    select new { cb.Ma_CanBo }).Count();

                dem++;
                StringDSCB += $@"<tr style='height:15pt'>
                                    <td
                                        style='width:5%; border-width:0.5pt; border-style:solid;  padding-right:5.03pt; padding-left:5.03pt; vertical-align:middle; background-color:#ffffff; -aw-border-bottom:0.5pt single; -aw-border-right:0.5pt single'>
                                        <p style='text-align:center; line-height:150%; font-size:10pt'><span> {ma.i+1}</span></p>
                                    </td>
                                    <td
                                        style='width:30%; border-width:0.5pt;  border-style:solid; padding-right:5.03pt; padding-left:5.4pt; vertical-align:middle; background-color:#ffffff; -aw-border-bottom:0.5pt single; -aw-border-right:0.5pt single'>
                                        <p style='line-height:150%; font-size:10pt'><span> {tencoquan}</span></p>
                                    </td>
                                    <td
                                        style='width:15%; border-width:0.5pt;   border-style:solid;  padding-right:5.03pt; padding-left:5.4pt; vertical-align:middle; background-color:#ffffff; -aw-border-bottom:0.5pt single; -aw-border-right:0.5pt single'>
                                        <p style='text-align:center; line-height:150%; font-size:10pt'><span> {soluongLanhDao}/{TongLanhDao}</span></p>
                                    </td>
                                    <td
                                        style='width:15%;  border-width:0.5pt;  border-style:solid; padding-right:5.03pt; padding-left:5.4pt; vertical-align:middle; background-color:#ffffff; -aw-border-bottom:0.5pt single; -aw-border-right:0.5pt single'>
                                        <p style='text-align:center; line-height:150%; font-size:10pt'><span>{soluongNhanVien}/{TongChuyenVien}</span></p>
                                    </td>
                                    <td
                                        style='width:15%; border-width:0.5pt;  border-style:solid; padding-right:5.03pt; padding-left:5.4pt; vertical-align:middle; background-color:#ffffff; -aw-border-bottom:0.5pt single; -aw-border-right:0.5pt single'>
                                        <p style='text-align:center; line-height:150%; font-size:10pt'><span>{soluongLanhDao + soluongNhanVien}/{TongCanBo}</span></p>
                                    </td>
                                    <td
                                        style='width:20%; border-style:solid; border-width:0.5pt;  padding-right:5.03pt;  padding-left:5.4pt; vertical-align:middle; background-color:#ffffff; -aw-border-bottom:0.5pt single'>
                                        <p style='line-height:150%; font-size:10pt'><span>&#xa0;</span></p>
                                    </td>
                                </tr>";

                TongCongSoNguoiDuocChon += (soluongLanhDao + soluongNhanVien);
                TongLanhDaoDaChon += soluongLanhDao;
                TongChuyenVienDaChon += soluongNhanVien;

                TongCongSoNguoi += TongCanBo;
                TongSoLanhDao += TongLanhDao;
                TongSoChuyenVien += TongChuyenVien;

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
                                    
                                    <p style='text-align:center; line-height:150%'><span style='font-weight:bold'>DANH SÁCH THỐNG KÊ SỐ LƯỢNG CƠ QUAN XÁC MINH</span></p>
                                    
                                    <table cellspacing='0' cellpadding='0'
                                        style='width:100%; border:0.75pt solid #000000; -aw-border:0.5pt single; border-collapse:collapse'>
                                        <tr style='height:25.5pt'>
                                            <td
                                                style='width:5%; border-style:solid; border-width:0.5pt;  padding-right:5.03pt; padding-left:5.03pt; vertical-align:middle; background-color:#ffffff; -aw-border-right:0.5pt single'>
                                                <p style='text-align:center; line-height:150%; font-size:10pt'><span
                                                        style='font-weight:bold'>STT</span></p>
                                            </td>
                                            <td
                                                style='width:30%; border-style:solid;  border-width:0.5pt;  padding-right:5.03pt; padding-left:5.4pt; vertical-align:middle; background-color:#ffffff; -aw-border-right:0.5pt single'>
                                                <p style='text-align:center; line-height:150%; font-size:10pt'><span style='font-weight:bold'>Tên cơ quan xác minh</span></p>
                                            </td>
                                            <td
                                                style='width:15%; border-style:solid;  border-width:0.5pt;padding-right:5.03pt; padding-left:5.4pt; vertical-align:middle; background-color:#ffffff; -aw-border-bottom:0.5pt single; -aw-border-right:0.5pt single'>
                                                <p style='text-align:center; line-height:150%; font-size:10pt'><span style='font-weight:bold'>Số người đứng đầu hoặc cấp phó của người đứng đầu được chọn xác minh</span></p>
                                            </td>
                                            <td
                                                style='width:15%; border-style:solid;  border-width:0.5pt;  padding-right:5.03pt; padding-left:5.4pt; vertical-align:middle; background-color:#ffffff; -aw-border-bottom:0.5pt single; -aw-border-right:0.5pt single'>
                                                <p style='text-align:center; line-height:150%; font-size:10pt'><span style='font-weight:bold'>Số người được chọn xác minh còn lại </span></p>
                                            </td>
                                            <td
                                                style='width:15%; border-style:solid;  border-width:0.5pt;   padding-right:5.03pt; padding-left:5.4pt; vertical-align:middle; background-color:#ffffff; -aw-border-bottom:0.5pt single; -aw-border-right:0.5pt single'>
                                                <p style='text-align:center; line-height:150%; font-size:10pt'><span style='font-weight:bold'>Tổng cộng số người được chọn xác minh</span></p>
                                            </td>
                                            <td
                                                style='width:20%; border-style:solid; border-width:0.5pt;  padding-left:5.4pt; vertical-align:middle; background-color:#ffffff'>
                                                <p style='text-align:center; line-height:150%; font-size:10pt'><span style='font-weight:bold'>Ghi chú</span></p>
                                            </td>
                                        </tr>
                                        
                                        
                                       " + StringDSCB + $@"
                                        <tr style='height:15pt'>
                                            <td
                                                style='width:5%; border-width:0.5pt; border-style:solid;  padding-right:5.03pt; padding-left:5.03pt; vertical-align:middle; background-color:#ffffff; -aw-border-bottom:0.5pt single; -aw-border-right:0.5pt single'>
                                                <p style='text-align:center; line-height:150%; font-size:10pt'><span> </span></p>
                                            </td>
                                            <td
                                                style='width:30%; border-width:0.5pt;  border-style:solid; padding-right:5.03pt; padding-left:5.4pt; vertical-align:middle; background-color:#ffffff; -aw-border-bottom:0.5pt single; -aw-border-right:0.5pt single'>
                                                <p style='line-height:150%; font-size:10pt'><span> <b>Tổng cộng</b></span></p>
                                            </td>
                                            <td
                                                style='width:15%; border-width:0.5pt;   border-style:solid;  padding-right:5.03pt; padding-left:5.4pt; vertical-align:middle; background-color:#ffffff; -aw-border-bottom:0.5pt single; -aw-border-right:0.5pt single'>
                                                <p style='text-align:center; line-height:150%; font-size:10pt'><span> <b>{TongLanhDaoDaChon}/{TongSoLanhDao}</b></span></p>
                                            </td>
                                            <td
                                                style='width:15%;  border-width:0.5pt;  border-style:solid; padding-right:5.03pt; padding-left:5.4pt; vertical-align:middle; background-color:#ffffff; -aw-border-bottom:0.5pt single; -aw-border-right:0.5pt single'>
                                                <p style='text-align:center; line-height:150%; font-size:10pt'><span><b>{TongChuyenVienDaChon}/{TongSoChuyenVien}</b></span></p>
                                            </td>
                                            <td
                                                style='width:15%; border-width:0.5pt;  border-style:solid; padding-right:5.03pt; padding-left:5.4pt; vertical-align:middle; background-color:#ffffff; -aw-border-bottom:0.5pt single; -aw-border-right:0.5pt single'>
                                                <p style='text-align:center; line-height:150%; font-size:10pt'><span><b>{TongCongSoNguoiDuocChon}/{TongCongSoNguoi}</b></span></p>
                                            </td>
                                            <td
                                                style='width:20%; border-style:solid; border-width:0.5pt;  padding-right:5.03pt;  padding-left:5.4pt; vertical-align:middle; background-color:#ffffff; -aw-border-bottom:0.5pt single'>
                                                <p style='line-height:150%; font-size:10pt'><span>&#xa0;</span></p>
                                            </td>
                                        </tr>
                                    </table>
                                    
                                </div>
                            </body>

                            </html>";

            //var TongLanhDaoDaChon = 0;
            //var TongChuyenVienDaChon = 0;
            //var TongCongSoNguoiDuocChon = 0;

            //var TongSoLanhDao = 0;
            //var TongSoChuyenVien = 0;
            //var TongCongSoNguoi = 0;
            string filename = System.Guid.NewGuid().ToString();
            string _filename = filename + ".pdf";
            string _path = Path.Combine(Server.MapPath("~/Content/uploads"), _filename);
            ConverterProperties properties = new ConverterProperties();
            properties.SetFontProvider(new DefaultFontProvider(true, true, true));
            HtmlConverter.ConvertToPdf(html, new FileStream(_path, FileMode.Create), properties);

            return Json(filename, JsonRequestBehavior.AllowGet);
        }

        public ActionResult BanIn(string id)
        {
            ViewBag.TenFile = id + ".pdf";
            return View();
        }

        public JsonResult CreateKeHoachXacMinh_DanhSach(int KeHoachNam)
        {
            var data = db.NV_LapKeHoachXacMinh_DanhSachCanBoXacMinh.Where(_ => _.KeHoachNam == KeHoachNam);

            if (data.Count() != 0)
            {
                return Json(new { status = "warning", title = "Cảnh Báo", message = "Danh Sách Đã Được Lập Trước Đó. Vui Lòng Kiểm Tra Lại!" });
            }

            var LapKeHoachXacMinh_DanhSachCanBoXacMinh = new NV_LapKeHoachXacMinh_DanhSachCanBoXacMinh();
            LapKeHoachXacMinh_DanhSachCanBoXacMinh.NgayLapDanhSach = DateTime.Today;
            LapKeHoachXacMinh_DanhSachCanBoXacMinh.KeHoachNam = KeHoachNam;
            LapKeHoachXacMinh_DanhSachCanBoXacMinh.TrangThai = false;
            LapKeHoachXacMinh_DanhSachCanBoXacMinh.FileDinhKem = "";
            LapKeHoachXacMinh_DanhSachCanBoXacMinh.TenDanhSach = "Danh Sách Cán Bộ Được Xác Minh Năm " + KeHoachNam;

            db.NV_LapKeHoachXacMinh_DanhSachCanBoXacMinh.Add(LapKeHoachXacMinh_DanhSachCanBoXacMinh);
            db.SaveChanges();

            return Json(new { status = "success", title = "Thành Công", message = "Danh Sách Đã Được Tạo Thành Công! " });           
        }

 
        public JsonResult LoadThongTinDanhSachCanBo(int id)
        {
            var data = db.NV_LapKeHoachXacMinh_DanhSachCanBoXacMinh.Find(id).ID_DanhSach;
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Create(int ID_DanhSach, List<int> Ma_CanBo_XacMinh)
        {
            foreach (var i in Ma_CanBo_XacMinh)
            {
                var KeHoachXacMinh_ChiTiet = new NV_LapKeHoachXacMinh_DanhSachCanBoXacMinh_ChiTiet();
                KeHoachXacMinh_ChiTiet.ID_DanhSach = ID_DanhSach;
                KeHoachXacMinh_ChiTiet.Ma_CanBo = i;
                db.NV_LapKeHoachXacMinh_DanhSachCanBoXacMinh_ChiTiet.Add(KeHoachXacMinh_ChiTiet);
                db.SaveChanges();
            }
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ThemTepDinhKem(int ID_DanhSach, HttpPostedFileBase FileDinhKem)
        {
            var LapKeHoachXacMinh_DanhSachCanBoXacMinh = db.NV_LapKeHoachXacMinh_DanhSachCanBoXacMinh.Where(_ => _.ID_DanhSach == ID_DanhSach).SingleOrDefault();
            if (LapKeHoachXacMinh_DanhSachCanBoXacMinh != null)
            {
                if (FileDinhKem.ContentLength > 0)
                {
                    string _FileName4 = Path.GetFileName(FileDinhKem.FileName);
                    string _path4 = Path.Combine(Server.MapPath("~/Content/uploads"), _FileName4);
                    LapKeHoachXacMinh_DanhSachCanBoXacMinh.FileDinhKem = _FileName4;
                    FileDinhKem.SaveAs(_path4);
                }
                db.Entry(LapKeHoachXacMinh_DanhSachCanBoXacMinh).State = EntityState.Modified;
                db.SaveChanges();

                return Json(true, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }

        }

        //public FileResult Download(int id)
        //{
        //    var CTT = db.NV_LapKeHoachKeKhai.Single(_ => _.MaKeHoachKeKhai == id);
        //    var url = Path.Combine(Server.MapPath("~/Content/uploads"), CTT.NghiDinh);
        //    byte[] fileBytes = System.IO.File.ReadAllBytes(url);
        //    string fileName = CTT.NghiDinh;
        //    return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
        //}

        public FileResult DownloadbyName(string Name)
        {
          
            var url = Path.Combine(Server.MapPath("~/Content/uploads"), Name);
            byte[] fileBytes = System.IO.File.ReadAllBytes(url);
            string fileName = Name;
            return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
        }

        public JsonResult Create_KeHoachXacMinh(int ID_DanhSachCanBo, int SoKeHoach, string NoiDungKeHoach, DateTime NgayLapKeHoach, HttpPostedFileBase FileKeHoach)
        {
            try
            {
                var khxm = new NV_LapKeHoachXacMinh();
                khxm.ID_DanhSachCanBo = ID_DanhSachCanBo;
                khxm.SoKeHoach = SoKeHoach.ToString();
                khxm.NoiDungKeHoach = NoiDungKeHoach;
                khxm.NgayLapKeHoach = NgayLapKeHoach;
                if (FileKeHoach.ContentLength > 0)
                {
                    string _FileName4 = user.GetRandomPassword(6) + Path.GetFileName(FileKeHoach.FileName);
                    string _path4 = Path.Combine(Server.MapPath("~/Content/uploads"), _FileName4);
                    khxm.FileKeHoach = _FileName4;
                    FileKeHoach.SaveAs(_path4);
                }
                khxm.TienDo = 0;
                khxm.TrangThai = false;
                db.NV_LapKeHoachXacMinh.Add(khxm);
                db.SaveChanges();
                return Json(true, JsonRequestBehavior.AllowGet);
            }catch (Exception ex)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
           
        }

        public JsonResult LoadDataKeHoachXacMinh_DanhSachCanBoXacMinh()
        {
            var draw = Request.Form.GetValues("draw").FirstOrDefault();
            var start = Request.Form.GetValues("start").FirstOrDefault();
            var length = Request.Form.GetValues("length").FirstOrDefault();
            var search = Request.Form.GetValues("columns[0][search][value]").FirstOrDefault();

            int pageSize = length != null ? Convert.ToInt32(length) : 0;
            int skip = start != null ? Convert.ToInt32(start) : 0;
            int recordsTotal = 0;

            var data = (from lkhxm in db.NV_LapKeHoachXacMinh_DanhSachCanBoXacMinh
                        orderby lkhxm.KeHoachNam descending
                        select new { lkhxm.ID_DanhSach, lkhxm.TenDanhSach, lkhxm.KeHoachNam, lkhxm.NgayLapDanhSach, lkhxm.FileDinhKem, lkhxm.TrangThai, TrangThaiDS = db.NV_LapKeHoachXacMinh_DanhSachCanBoXacMinh_ChiTiet.Where(_ => _.ID_DanhSach == lkhxm.ID_DanhSach).Count() }).ToList();

            if (!string.IsNullOrEmpty(search))
            {
                data = data.Where(a => a.TenDanhSach.ToUpper().Contains(search.ToUpper())).ToList();
            }


            recordsTotal = data.Count();
            var data1 = data.Skip(skip).Take(pageSize).ToList();
            return Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data1 }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult LoadThongTinKeHoachKeKhai(int id)
        {
            
            var data = db.NV_LapKeHoachXacMinh.Where(_ => _.ID_DanhSachCanBo == id).FirstOrDefault();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetCanBoDuocXacMinh(int MaDanhSachCanBoXacMinh)
        {
            var Role = user.GetRole();
            var maCoQuan = user.GetUserCoQuan();
            var draw = Request.Form.GetValues("draw").FirstOrDefault();
            var start = Request.Form.GetValues("start").FirstOrDefault();
            var length = Request.Form.GetValues("length").FirstOrDefault();
            var TenCanBo = Request.Form.GetValues("columns[0][search][value]").FirstOrDefault();
            int pageSize = length != null ? Convert.ToInt32(length) : 0;
            int skip = start != null ? Convert.ToInt32(start) : 0;
            int recordsTotal = 0;

            var data = (from cb in db.DM_CanBo
                        join cq in db.DM_CoQuanDonVi on cb.Ma_CoQuan_DonVi equals cq.Ma_CoQuan_DonVi
                        join cv in db.DM_ChucVu_ChucDanh on cb.Ma_ChucVu_ChucDanh equals cv.Ma_ChucVu_ChucDanh
                        join khxm_ct in db.NV_LapKeHoachXacMinh_DanhSachCanBoXacMinh_ChiTiet on cb.Ma_CanBo equals khxm_ct.Ma_CanBo
                        join khxm in db.NV_LapKeHoachXacMinh_DanhSachCanBoXacMinh on khxm_ct.ID_DanhSach equals khxm.ID_DanhSach
                        where khxm.ID_DanhSach == MaDanhSachCanBoXacMinh
                        select new {cb.Ma_CanBo, cb.HoTen, cq.Ten, cv.Ten_ChucVu_ChucDanh, TrangThai = true }).ToList();

            recordsTotal = data.Count();
            var data1 = data.Skip(skip).Take(pageSize).ToList();
            return Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data1 }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult LayDanhSachCoQuan(int id)
        {
            var kehoachnam = db.NV_LapKeHoachXacMinh_DanhSachCanBoXacMinh.Find(id).KeHoachNam;

            var kehoachkekhai = db.NV_LapKeHoachKeKhai.Where(_ => _.KeHoachNam == kehoachnam && _.Ma_Loai_KeKhai == 4 || _.Ma_Loai_KeKhai == 5).Select(_ => _.MaKeHoachKeKhai).ToList();

            var dscbhn = (from cq in db.DM_CoQuanDonVi
                          join lcq in db.DM_Loai_CoQuan_DonVi on cq.MaLoai_CoQuan_DonVi equals lcq.Ma_Loai_CQDV
                          join khkkhn in db.NV_LapKeHoachKeKhai_DanhSachKeKhaiHangNam on cq.Ma_CoQuan_DonVi equals khkkhn.Ma_CoQuan_DonVi
                          join khkk in db.NV_LapKeHoachKeKhai on khkkhn.MaKeHoachKeKhai equals khkk.MaKeHoachKeKhai
                          where lcq.Ma_Loai_CQDV != 35 && kehoachkekhai.Contains(khkk.MaKeHoachKeKhai)
                          orderby lcq.Ten_Loai_CQDV, cq.Ten
                          select cq.Ma_CoQuan_DonVi).ToList();

            //var cbdkk = (from cb in db.DM_CanBo
            //            join bkk in db.Nv_KeKhai_TSTN on cb.Ma_CanBo equals bkk.Ma_CanBo
            //            join tk in db.HT_TaiKhoan on cb.Ma_CanBo equals tk.Ma_CanBo
            //            join ntk in db.HT_NhomTaiKhoan on tk.MaNhomTaiKhoan equals ntk.MaNhomTaiKhoan
            //            join cqdv in db.DM_CoQuanDonVi on cb.Ma_CoQuan_DonVi equals cqdv.Ma_CoQuan_DonVi
            //            join lcq in db.DM_Loai_CoQuan_DonVi on cqdv.MaLoai_CoQuan_DonVi equals lcq.Ma_Loai_CQDV
            //            join khkkhn in db.NV_LapKeHoachKeKhai_DanhSachKeKhaiHangNam on cqdv.Ma_CoQuan_DonVi equals khkkhn.Ma_CoQuan_DonVi
            //            join khkk in db.NV_LapKeHoachKeKhai on khkkhn.MaKeHoachKeKhai equals khkk.MaKeHoachKeKhai
            //            where lcq.Ma_Loai_CQDV != 35 && kehoachkekhai.Contains(khkk.MaKeHoachKeKhai) && cqdv.Ma_CoQuan_DonVi == cq.Ma_CoQuan_DonVi && bkk.TrangThai == true && (ntk.MaTaiKhoan == "NDDTTT" || ntk.MaTaiKhoan == "NDDCSBN" || ntk.MaTaiKhoan == "PLD")
            //            select new { cb.Ma_CanBo }
            //             );

            var data = (from cq in db.DM_CoQuanDonVi
                        join lcq in db.DM_Loai_CoQuan_DonVi on cq.MaLoai_CoQuan_DonVi equals lcq.Ma_Loai_CQDV
                        where lcq.Ma_Loai_CQDV != 35
                        orderby lcq.Ten_Loai_CQDV, cq.Ten
                        select new { cq.Ma_CoQuan_DonVi, cq.Ten, lcq.Ten_Loai_CQDV, TrangThai =  dscbhn.Contains(cq.Ma_CoQuan_DonVi),
                        TongCanBo = (from cb in db.DM_CanBo
                             join bkk in db.Nv_KeKhai_TSTN on cb.Ma_CanBo equals bkk.Ma_CanBo
                             join cqdv in db.DM_CoQuanDonVi on cb.Ma_CoQuan_DonVi equals cqdv.Ma_CoQuan_DonVi
                             join lcq in db.DM_Loai_CoQuan_DonVi on cqdv.MaLoai_CoQuan_DonVi equals lcq.Ma_Loai_CQDV
                             join khkkhn in db.NV_LapKeHoachKeKhai_DanhSachKeKhaiHangNam on cqdv.Ma_CoQuan_DonVi equals khkkhn.Ma_CoQuan_DonVi
                             join khkk in db.NV_LapKeHoachKeKhai on khkkhn.MaKeHoachKeKhai equals khkk.MaKeHoachKeKhai
                             join khkknhct in db.NV_LapKeHoachKeKhai_DanhSachKeKhaiHangNam_ChiTiet on cb.Ma_CanBo equals khkknhct.Ma_CanBo
                             where khkkhn.MaKeHoachKeKhai_DanhSachKeKhaiHangNam_ID == khkknhct.MaKeHoachKeKhai_DanhSachKeKhaiHangNam_ID && cqdv.Ma_CoQuan_DonVi == cq.Ma_CoQuan_DonVi && bkk.TrangThai == true && khkk.KeHoachNam == kehoachnam && khkk.Ma_Loai_KeKhai == 4 && bkk.Nam_KeKhai == kehoachnam && bkk.Ma_Loai_KeKhai == 4 && bkk.MaKeHoachKeKhai == khkk.MaKeHoachKeKhai
                                     select new { cb.Ma_CanBo }
                             ).Count(),
                        TongLanhDao = (from cb in db.DM_CanBo
                                       join bkk in db.Nv_KeKhai_TSTN on cb.Ma_CanBo equals bkk.Ma_CanBo
                                       join tk in db.HT_TaiKhoan on cb.Ma_CanBo equals tk.Ma_CanBo
                                       join ntk in db.HT_NhomTaiKhoan on tk.MaNhomTaiKhoan equals ntk.MaNhomTaiKhoan
                                       join cqdv in db.DM_CoQuanDonVi on cb.Ma_CoQuan_DonVi equals cqdv.Ma_CoQuan_DonVi
                                       join lcq in db.DM_Loai_CoQuan_DonVi on cqdv.MaLoai_CoQuan_DonVi equals lcq.Ma_Loai_CQDV
                                       join khkkhn in db.NV_LapKeHoachKeKhai_DanhSachKeKhaiHangNam on cqdv.Ma_CoQuan_DonVi equals khkkhn.Ma_CoQuan_DonVi
                                       join khkknhct in db.NV_LapKeHoachKeKhai_DanhSachKeKhaiHangNam_ChiTiet on cb.Ma_CanBo equals khkknhct.Ma_CanBo
                                       join khkk in db.NV_LapKeHoachKeKhai on khkkhn.MaKeHoachKeKhai equals khkk.MaKeHoachKeKhai
                                       where khkkhn.MaKeHoachKeKhai_DanhSachKeKhaiHangNam_ID == khkknhct.MaKeHoachKeKhai_DanhSachKeKhaiHangNam_ID && khkk.KeHoachNam == kehoachnam && khkk.Ma_Loai_KeKhai == 4 && bkk.Nam_KeKhai == kehoachnam && bkk.Ma_Loai_KeKhai == 4 && cqdv.Ma_CoQuan_DonVi == cq.Ma_CoQuan_DonVi && bkk.TrangThai == true && bkk.MaKeHoachKeKhai == khkk.MaKeHoachKeKhai && (ntk.MaTaiKhoan == "NDDTTT" || ntk.MaTaiKhoan == "NDDCSBN" || ntk.MaTaiKhoan == "PLD")
                                       select new { cb.Ma_CanBo }
                         ).Count(),
                        TongChuyenVien = (from cb in db.DM_CanBo
                                          join bkk in db.Nv_KeKhai_TSTN on cb.Ma_CanBo equals bkk.Ma_CanBo
                                          join tk in db.HT_TaiKhoan on cb.Ma_CanBo equals tk.Ma_CanBo
                                          join ntk in db.HT_NhomTaiKhoan on tk.MaNhomTaiKhoan equals ntk.MaNhomTaiKhoan
                                          join cqdv in db.DM_CoQuanDonVi on cb.Ma_CoQuan_DonVi equals cqdv.Ma_CoQuan_DonVi
                                          join lcq in db.DM_Loai_CoQuan_DonVi on cqdv.MaLoai_CoQuan_DonVi equals lcq.Ma_Loai_CQDV
                                          join khkkhn in db.NV_LapKeHoachKeKhai_DanhSachKeKhaiHangNam on cqdv.Ma_CoQuan_DonVi equals khkkhn.Ma_CoQuan_DonVi
                                          join khkknhct in db.NV_LapKeHoachKeKhai_DanhSachKeKhaiHangNam_ChiTiet on cb.Ma_CanBo equals khkknhct.Ma_CanBo
                                          join khkk in db.NV_LapKeHoachKeKhai on khkkhn.MaKeHoachKeKhai equals khkk.MaKeHoachKeKhai
                                          where khkkhn.MaKeHoachKeKhai_DanhSachKeKhaiHangNam_ID == khkknhct.MaKeHoachKeKhai_DanhSachKeKhaiHangNam_ID && khkk.KeHoachNam == kehoachnam && khkk.Ma_Loai_KeKhai == 4 && bkk.Nam_KeKhai == kehoachnam && bkk.Ma_Loai_KeKhai == 4 && cqdv.Ma_CoQuan_DonVi == cq.Ma_CoQuan_DonVi && bkk.TrangThai == true && bkk.MaKeHoachKeKhai == khkk.MaKeHoachKeKhai && (ntk.MaTaiKhoan == "CBKKTSTN" || ntk.MaTaiKhoan == "NQTCSBN")
                                          select new { cb.Ma_CanBo }
                         ).Count(),

                        }).ToList();

            return Json(new {data, kehoachkekhai}, JsonRequestBehavior.AllowGet);
        }
        public JsonResult LayDanhSachCoQuanDaChon(int MaDanhSachCanBoXacMinh, List<int> listCoQuanDaChon)
        {
            if(listCoQuanDaChon.Count != 0)
            {
                var tongSoCoQuan = db.DM_CoQuanDonVi.Count();
                var TongSoCoQuanDaChon = listCoQuanDaChon.Count();
                var tiLeCoQuanDaChon = (float)(((float)TongSoCoQuanDaChon / (float)tongSoCoQuan) * 100);
                if (tiLeCoQuanDaChon < 20 )
                {
                    return Json(new { TrangThai = "thap" }, JsonRequestBehavior.AllowGet);
                }

                var kehoachnam = db.NV_LapKeHoachXacMinh_DanhSachCanBoXacMinh.Find(MaDanhSachCanBoXacMinh).KeHoachNam;

                var kehoachkekhai = db.NV_LapKeHoachKeKhai.Where(_ => _.KeHoachNam == kehoachnam && _.Ma_Loai_KeKhai == 4 || _.Ma_Loai_KeKhai == 5).Select(_ => _.MaKeHoachKeKhai).ToList();

                var dscbhn = (from cq in db.DM_CoQuanDonVi
                              join lcq in db.DM_Loai_CoQuan_DonVi on cq.MaLoai_CoQuan_DonVi equals lcq.Ma_Loai_CQDV
                              join khkkhn in db.NV_LapKeHoachKeKhai_DanhSachKeKhaiHangNam on cq.Ma_CoQuan_DonVi equals khkkhn.Ma_CoQuan_DonVi
                              join khkk in db.NV_LapKeHoachKeKhai on khkkhn.MaKeHoachKeKhai equals khkk.MaKeHoachKeKhai
                              where lcq.Ma_Loai_CQDV != 35 && kehoachkekhai.Contains(khkk.MaKeHoachKeKhai)
                              orderby lcq.Ten_Loai_CQDV, cq.Ten
                              select cq.Ma_CoQuan_DonVi).ToList();

                var data = (from cq in db.DM_CoQuanDonVi
                            join lcq in db.DM_Loai_CoQuan_DonVi on cq.MaLoai_CoQuan_DonVi equals lcq.Ma_Loai_CQDV
                            where lcq.Ma_Loai_CQDV != 35 && listCoQuanDaChon.Contains(cq.Ma_CoQuan_DonVi)
                            orderby lcq.Ten_Loai_CQDV, cq.Ten
                            select new
                            {
                                cq.Ma_CoQuan_DonVi,
                                cq.Ten,
                                lcq.Ten_Loai_CQDV,
                                TrangThai = dscbhn.Contains(cq.Ma_CoQuan_DonVi),
                                TongCanBo = (from cb in db.DM_CanBo
                                             join bkk in db.Nv_KeKhai_TSTN on cb.Ma_CanBo equals bkk.Ma_CanBo
                                             join cqdv in db.DM_CoQuanDonVi on cb.Ma_CoQuan_DonVi equals cqdv.Ma_CoQuan_DonVi
                                             join lcq in db.DM_Loai_CoQuan_DonVi on cqdv.MaLoai_CoQuan_DonVi equals lcq.Ma_Loai_CQDV
                                             join khkkhn in db.NV_LapKeHoachKeKhai_DanhSachKeKhaiHangNam on cqdv.Ma_CoQuan_DonVi equals khkkhn.Ma_CoQuan_DonVi
                                             join khkk in db.NV_LapKeHoachKeKhai on khkkhn.MaKeHoachKeKhai equals khkk.MaKeHoachKeKhai
                                             join khkknhct in db.NV_LapKeHoachKeKhai_DanhSachKeKhaiHangNam_ChiTiet on cb.Ma_CanBo equals khkknhct.Ma_CanBo
                                             where khkkhn.MaKeHoachKeKhai_DanhSachKeKhaiHangNam_ID == khkknhct.MaKeHoachKeKhai_DanhSachKeKhaiHangNam_ID && cqdv.Ma_CoQuan_DonVi == cq.Ma_CoQuan_DonVi && bkk.TrangThai == true && khkk.KeHoachNam == kehoachnam && khkk.Ma_Loai_KeKhai == 4 && bkk.Nam_KeKhai == kehoachnam && bkk.Ma_Loai_KeKhai == 4 && bkk.MaKeHoachKeKhai == khkk.MaKeHoachKeKhai
                                             select new { cb.Ma_CanBo }
                                 ).Count(),
                                TongLanhDao = (from cb in db.DM_CanBo
                                               join bkk in db.Nv_KeKhai_TSTN on cb.Ma_CanBo equals bkk.Ma_CanBo
                                               join tk in db.HT_TaiKhoan on cb.Ma_CanBo equals tk.Ma_CanBo
                                               join ntk in db.HT_NhomTaiKhoan on tk.MaNhomTaiKhoan equals ntk.MaNhomTaiKhoan
                                               join cqdv in db.DM_CoQuanDonVi on cb.Ma_CoQuan_DonVi equals cqdv.Ma_CoQuan_DonVi
                                               join lcq in db.DM_Loai_CoQuan_DonVi on cqdv.MaLoai_CoQuan_DonVi equals lcq.Ma_Loai_CQDV
                                               join khkkhn in db.NV_LapKeHoachKeKhai_DanhSachKeKhaiHangNam on cqdv.Ma_CoQuan_DonVi equals khkkhn.Ma_CoQuan_DonVi
                                               join khkknhct in db.NV_LapKeHoachKeKhai_DanhSachKeKhaiHangNam_ChiTiet on cb.Ma_CanBo equals khkknhct.Ma_CanBo
                                               join khkk in db.NV_LapKeHoachKeKhai on khkkhn.MaKeHoachKeKhai equals khkk.MaKeHoachKeKhai
                                               where khkkhn.MaKeHoachKeKhai_DanhSachKeKhaiHangNam_ID == khkknhct.MaKeHoachKeKhai_DanhSachKeKhaiHangNam_ID && khkk.KeHoachNam == kehoachnam && khkk.Ma_Loai_KeKhai == 4 && bkk.Nam_KeKhai == kehoachnam && bkk.Ma_Loai_KeKhai == 4 && cqdv.Ma_CoQuan_DonVi == cq.Ma_CoQuan_DonVi && bkk.TrangThai == true && bkk.MaKeHoachKeKhai == khkk.MaKeHoachKeKhai && (ntk.MaTaiKhoan == "NDDTTT" || ntk.MaTaiKhoan == "NDDCSBN" || ntk.MaTaiKhoan == "PLD")
                                               select new { cb.Ma_CanBo }
                             ).Count(),
                                TongChuyenVien = (from cb in db.DM_CanBo
                                                  join bkk in db.Nv_KeKhai_TSTN on cb.Ma_CanBo equals bkk.Ma_CanBo
                                                  join tk in db.HT_TaiKhoan on cb.Ma_CanBo equals tk.Ma_CanBo
                                                  join ntk in db.HT_NhomTaiKhoan on tk.MaNhomTaiKhoan equals ntk.MaNhomTaiKhoan
                                                  join cqdv in db.DM_CoQuanDonVi on cb.Ma_CoQuan_DonVi equals cqdv.Ma_CoQuan_DonVi
                                                  join lcq in db.DM_Loai_CoQuan_DonVi on cqdv.MaLoai_CoQuan_DonVi equals lcq.Ma_Loai_CQDV
                                                  join khkkhn in db.NV_LapKeHoachKeKhai_DanhSachKeKhaiHangNam on cqdv.Ma_CoQuan_DonVi equals khkkhn.Ma_CoQuan_DonVi
                                                  join khkknhct in db.NV_LapKeHoachKeKhai_DanhSachKeKhaiHangNam_ChiTiet on cb.Ma_CanBo equals khkknhct.Ma_CanBo
                                                  join khkk in db.NV_LapKeHoachKeKhai on khkkhn.MaKeHoachKeKhai equals khkk.MaKeHoachKeKhai
                                                  where khkkhn.MaKeHoachKeKhai_DanhSachKeKhaiHangNam_ID == khkknhct.MaKeHoachKeKhai_DanhSachKeKhaiHangNam_ID && khkk.KeHoachNam == kehoachnam && khkk.Ma_Loai_KeKhai == 4 && bkk.Nam_KeKhai == kehoachnam && bkk.Ma_Loai_KeKhai == 4 && cqdv.Ma_CoQuan_DonVi == cq.Ma_CoQuan_DonVi && bkk.TrangThai == true && bkk.MaKeHoachKeKhai == khkk.MaKeHoachKeKhai && (ntk.MaTaiKhoan == "CBKKTSTN" || ntk.MaTaiKhoan == "NQTCSBN")
                                                  select new { cb.Ma_CanBo }
                             ).Count(),

                            }).ToList();

                return Json(new { TrangThai = "true", data1 = data }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { TrangThai = "false" }, JsonRequestBehavior.AllowGet);
            }
           
        }

        public ActionResult LayDanhSachCanBo(int id, string DSNgauNhienCoQuan)
        {
            var listcanbo = new List<CanBoKeKhai>();

            // lấy mã kế hoạch
            var kehoachnam = db.NV_LapKeHoachXacMinh_DanhSachCanBoXacMinh.Find(id).KeHoachNam;
            var kehoachkekhai = db.NV_LapKeHoachKeKhai.Where(_ => _.KeHoachNam == kehoachnam && _.Ma_Loai_KeKhai == 4).Select(_ => _.MaKeHoachKeKhai).ToList();

            //danh sách cơ quan đã lập kế hoạch kê khai
            var dscqdalapkehoach = (from cq in db.DM_CoQuanDonVi
                                    join khkk in db.NV_LapKeHoachKeKhai on cq.Ma_CoQuan_DonVi equals khkk.Ma_CoQuan_DonVi
                                    where khkk.Ma_Loai_KeKhai == 4
                                    select cq.MaLoai_CoQuan_DonVi).ToList();

            //danh sáng cán bộ đã lập kế hoạch kê khai
            var dscbdalapkehoachHN = (from cb in db.DM_CanBo
                                      join kkts in db.Nv_KeKhai_TSTN on cb.Ma_CanBo equals kkts.Ma_CanBo
                                      join khkk in db.NV_LapKeHoachKeKhai on kkts.MaKeHoachKeKhai equals khkk.MaKeHoachKeKhai
                                      select cb.Ma_CanBo).ToList();

            

            var listcoquan = DSNgauNhienCoQuan.Split(',');
            foreach (var ma in listcoquan)
            {
                var MaCoQuan = Int32.Parse(ma.ToString());

                var ds_cabo_kkhn = (from ct in db.NV_LapKeHoachKeKhai_DanhSachKeKhaiHangNam_ChiTiet
                                    join ds in db.NV_LapKeHoachKeKhai_DanhSachKeKhaiHangNam on ct.MaKeHoachKeKhai_DanhSachKeKhaiHangNam_ID equals ds.MaKeHoachKeKhai_DanhSachKeKhaiHangNam_ID
                                    join cb in db.DM_CanBo on ct.Ma_CanBo equals cb.Ma_CanBo
                                    join cv in db.DM_ChucVu_ChucDanh on cb.Ma_ChucVu_ChucDanh equals cv.Ma_ChucVu_ChucDanh
                                    join cq in db.DM_CoQuanDonVi on cb.Ma_CoQuan_DonVi equals cq.Ma_CoQuan_DonVi
                                    join tk in db.HT_TaiKhoan on cb.Ma_CanBo equals tk.Ma_CanBo
                                    join ntk in db.HT_NhomTaiKhoan on tk.MaNhomTaiKhoan equals ntk.MaNhomTaiKhoan
                                    where ds.Ma_CoQuan_DonVi == MaCoQuan
                                    orderby ntk.Sort, cb.Ten
                                    select new CanBoKeKhai { orderNTK = (int)ntk.Sort, MaCanBo = cb.Ma_CanBo, CCCD = cb.SoCCCD, TenCoQuan = cq.Ten, ChucVu = cv.Ten_ChucVu_ChucDanh, HoTen = cb.HoTen, NgaySinh = cb.DoB, MaQuyen = ntk.MaTaiKhoan, TrangThaiKK = dscbdalapkehoachHN.Contains(cb.Ma_CanBo) }).ToList();
                listcanbo = (List<CanBoKeKhai>)listcanbo.Concat(ds_cabo_kkhn).ToList();
            }

            listcanbo.Reverse();

            return Json(listcanbo, JsonRequestBehavior.AllowGet);
        }

        public ActionResult LayDanhSachCoQuanNgauNhien(int id)
        {
            var kehoachnam = db.NV_LapKeHoachXacMinh_DanhSachCanBoXacMinh.Find(id).KeHoachNam;

            var kehoachkekhai = db.NV_LapKeHoachKeKhai.Where(_ => _.KeHoachNam == kehoachnam && _.Ma_Loai_KeKhai == 4).Select(_ => _.MaKeHoachKeKhai).ToList();

            var dscbhn = (from cq in db.DM_CoQuanDonVi
                          join lcq in db.DM_Loai_CoQuan_DonVi on cq.MaLoai_CoQuan_DonVi equals lcq.Ma_Loai_CQDV
                          join khkkhn in db.NV_LapKeHoachKeKhai_DanhSachKeKhaiHangNam on cq.Ma_CoQuan_DonVi equals khkkhn.Ma_CoQuan_DonVi
                          join khkk in db.NV_LapKeHoachKeKhai on khkkhn.MaKeHoachKeKhai equals khkk.MaKeHoachKeKhai
                          where lcq.Ma_Loai_CQDV != 35 && kehoachkekhai.Contains(khkk.MaKeHoachKeKhai)
                          orderby lcq.Ten_Loai_CQDV, cq.Ten
                          select cq.Ma_CoQuan_DonVi).ToList();

            var data = (from cq in db.DM_CoQuanDonVi
                        join lcq in db.DM_Loai_CoQuan_DonVi on cq.MaLoai_CoQuan_DonVi equals lcq.Ma_Loai_CQDV
                        where lcq.Ma_Loai_CQDV != 35
                        orderby lcq.Ten_Loai_CQDV, cq.Ten
                        select new { cq.Ma_CoQuan_DonVi, cq.Ten, lcq.Ten_Loai_CQDV, TrangThai = dscbhn.Contains(cq.Ma_CoQuan_DonVi) }).ToList();

            var soluong = (int)Math.Round((double)data.Count() / 5);
            var data1 = data.OrderBy(n => Guid.NewGuid()).Take(soluong);
            return Json(new { data1, kehoachkekhai }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult LayDanhSachCanBoNgauNhien(string DanhSachCoQuan, int MaDanhSachCanBoXacMinh, string SoLanhDao, string SoNhanVien)
        {
            var listcanbo = new List<CanBoKeKhai>();

            // lấy mã kế hoạch
            var kehoachnam = db.NV_LapKeHoachXacMinh_DanhSachCanBoXacMinh.Find(MaDanhSachCanBoXacMinh).KeHoachNam;
            var kehoachkekhai = db.NV_LapKeHoachKeKhai.Where(_ => _.KeHoachNam == kehoachnam && _.Ma_Loai_KeKhai == 4).Select(_ => _.MaKeHoachKeKhai).ToList();

            //danh sách cơ quan đã lập kế hoạch kê khai
            var dscqdalapkehoach = (from cq in db.DM_CoQuanDonVi
                                    join khkk in db.NV_LapKeHoachKeKhai on cq.Ma_CoQuan_DonVi equals khkk.Ma_CoQuan_DonVi
                                    where khkk.Ma_Loai_KeKhai == 4
                                    select cq.MaLoai_CoQuan_DonVi).ToList();

            //danh sáng cán bộ đã lập kế hoạch kê khai
            var dscbdalapkehoachHN = (from cb in db.DM_CanBo
                                      join kkts in db.Nv_KeKhai_TSTN on cb.Ma_CanBo equals kkts.Ma_CanBo
                                      join khkk in db.NV_LapKeHoachKeKhai on kkts.MaKeHoachKeKhai equals khkk.MaKeHoachKeKhai
                                      select cb.Ma_CanBo).ToList();


            var listcoquan = DanhSachCoQuan.Split(',');
            var SoLanhDaoDaChon = SoLanhDao.Split(',');
            var SoNhanVienDaChon = SoNhanVien.Split(',');


            foreach (var ma in listcoquan.OfType<string>().Select((x, i) => new { x, i }))
            {
                var MaCoQuan = Int32.Parse(ma.x);
                var cabo_kkhn = (from ct in db.NV_LapKeHoachKeKhai_DanhSachKeKhaiHangNam_ChiTiet
                                 join ds in db.NV_LapKeHoachKeKhai_DanhSachKeKhaiHangNam on ct.MaKeHoachKeKhai_DanhSachKeKhaiHangNam_ID equals ds.MaKeHoachKeKhai_DanhSachKeKhaiHangNam_ID
                                 where kehoachkekhai.Contains((int)ds.MaKeHoachKeKhai) && ds.Ma_CoQuan_DonVi == MaCoQuan
                                 select ct.Ma_CanBo).ToList().Count();

                //var soluong = (int)Math.Round((double)cabo_kkhn / 10);
                var soluongLanhDao = Int32.Parse(SoLanhDaoDaChon[ma.i]);

                var soluongNhanVien = Int32.Parse(SoNhanVienDaChon[ma.i]);

                if (soluongLanhDao == 0) soluongLanhDao = 1;

                //if (soluongNhanVien == 0) soluongNhanVien = 1;

                //while (true)
                //{
                var ds_caboLanhDao_kkhn = (from ct in db.NV_LapKeHoachKeKhai_DanhSachKeKhaiHangNam_ChiTiet
                                           join ds in db.NV_LapKeHoachKeKhai_DanhSachKeKhaiHangNam on ct.MaKeHoachKeKhai_DanhSachKeKhaiHangNam_ID equals ds.MaKeHoachKeKhai_DanhSachKeKhaiHangNam_ID
                                           join cb in db.DM_CanBo on ct.Ma_CanBo equals cb.Ma_CanBo
                                           join cv in db.DM_ChucVu_ChucDanh on cb.Ma_ChucVu_ChucDanh equals cv.Ma_ChucVu_ChucDanh
                                           join cq in db.DM_CoQuanDonVi on cb.Ma_CoQuan_DonVi equals cq.Ma_CoQuan_DonVi
                                           join tk in db.HT_TaiKhoan on cb.Ma_CanBo equals tk.Ma_CanBo
                                           join ntk in db.HT_NhomTaiKhoan on tk.MaNhomTaiKhoan equals ntk.MaNhomTaiKhoan

                                           where kehoachkekhai.Contains((int)ds.MaKeHoachKeKhai) && ds.Ma_CoQuan_DonVi == MaCoQuan && (ntk.MaTaiKhoan == "NDDCSBN" || ntk.MaTaiKhoan == "PLD")
                                           select new CanBoKeKhai { orderNTK = (int)ntk.Sort, MaCanBo = cb.Ma_CanBo, CCCD = cb.SoCCCD, TenCoQuan = cq.Ten, ChucVu = cv.Ten_ChucVu_ChucDanh, HoTen = cb.HoTen, NgaySinh = cb.DoB, MaQuyen = ntk.MaTaiKhoan, TrangThaiKK = dscbdalapkehoachHN.Contains(cb.Ma_CanBo) }).ToList();

                var ds_caboNhanVien_kkhn = (from ct in db.NV_LapKeHoachKeKhai_DanhSachKeKhaiHangNam_ChiTiet
                                            join ds in db.NV_LapKeHoachKeKhai_DanhSachKeKhaiHangNam on ct.MaKeHoachKeKhai_DanhSachKeKhaiHangNam_ID equals ds.MaKeHoachKeKhai_DanhSachKeKhaiHangNam_ID
                                            join cb in db.DM_CanBo on ct.Ma_CanBo equals cb.Ma_CanBo
                                            join cv in db.DM_ChucVu_ChucDanh on cb.Ma_ChucVu_ChucDanh equals cv.Ma_ChucVu_ChucDanh
                                            join cq in db.DM_CoQuanDonVi on cb.Ma_CoQuan_DonVi equals cq.Ma_CoQuan_DonVi
                                            join tk in db.HT_TaiKhoan on cb.Ma_CanBo equals tk.Ma_CanBo
                                            join ntk in db.HT_NhomTaiKhoan on tk.MaNhomTaiKhoan equals ntk.MaNhomTaiKhoan

                                            where kehoachkekhai.Contains((int)ds.MaKeHoachKeKhai) && ds.Ma_CoQuan_DonVi == MaCoQuan && (ntk.MaTaiKhoan == "NQTCSBN" || ntk.MaTaiKhoan == "CBKKTSTN")
                                            select new CanBoKeKhai { orderNTK = (int)ntk.Sort, MaCanBo = cb.Ma_CanBo, CCCD = cb.SoCCCD, TenCoQuan = cq.Ten, ChucVu = cv.Ten_ChucVu_ChucDanh, HoTen = cb.HoTen, NgaySinh = cb.DoB, MaQuyen = ntk.MaTaiKhoan, TrangThaiKK = dscbdalapkehoachHN.Contains(cb.Ma_CanBo) }).ToList();


                var dscbLD = ds_caboLanhDao_kkhn.OrderBy(n => Guid.NewGuid()).Take(soluongLanhDao).ToList().OrderBy(_ => _.orderNTK).ThenBy(_ => _.HoTen);
                
                var dscbNV = ds_caboNhanVien_kkhn.OrderBy(n => Guid.NewGuid()).Take(soluongNhanVien).ToList().OrderBy(_ => _.orderNTK).ThenBy(_ => _.HoTen);

                var data = dscbLD.Concat(dscbNV).ToList();
                listcanbo = (List<CanBoKeKhai>)listcanbo.Concat(data).ToList();
                //if (dscbLD.Count() == 0)
                //{
                //    break;
                //}
                //else
                //{
                //var checkds = dscb.Where(_ => _.MaQuyen == "NDDCSBN" || _.MaQuyen == "PLD").Count();
                //if (checkds > 0) { listcanbo = (List<CanBoKeKhai>)listcanbo.Concat(dscb).ToList(); break; }
                //}
                //}
            }


            return Json(new { TrangThai = "true", listcanbo }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult indanhsachcanbo(int id)
        {
            var html = "";
            if (id == null)
            {
                html = "";
            }
            else
            {
                var lapkehoachxacminh = db.NV_LapKeHoachXacMinh_DanhSachCanBoXacMinh.Where(_ => _.ID_DanhSach == id).FirstOrDefault();
                var MaLapKeHoachXacMinh = lapkehoachxacminh.ID_DanhSach;
                var lapkehoachxacminh_chitiet = (from lkhxm in db.NV_LapKeHoachXacMinh_DanhSachCanBoXacMinh_ChiTiet
                                                 join cb in db.DM_CanBo on lkhxm.Ma_CanBo equals cb.Ma_CanBo
                                                 join cq in db.DM_CoQuanDonVi on cb.Ma_CoQuan_DonVi equals cq.Ma_CoQuan_DonVi
                                                 join cv in db.DM_ChucVu_ChucDanh on cb.Ma_ChucVu_ChucDanh equals cv.Ma_ChucVu_ChucDanh
                                                 where lkhxm.ID_DanhSach == MaLapKeHoachXacMinh
                                                 select new { lkhxm, cq, cb, cv }).ToList();
                var data = new { lapkehoachxacminh = lapkehoachxacminh, lapkehoachxacminh_chitiet = lapkehoachxacminh_chitiet };

                html = $@"
                        <html>
                        <head>
                            <meta http-equiv=Content-Type content='text/html; charset=utf-8'>
                            <meta name=Generator content='Microsoft Word 15 (filtered)'>
                            <style>
                                <!--
                                /* Font Definitions */
                                @font-face {{
                                    font-family: 'Times New Roman';
                                    panose-1: 2 4 5 3 5 4 6 3 2 4;
                                }}

                                /* Style Definitions */
                                h2.MsoNormal,
                                p.MsoNormal,
                                li.MsoNormal,
                                div.MsoNormal {{
                                    margin: 0in;
                                    font-size: 12.0pt;
                                    font-family: 'Times New Roman', serif;
                                }}

                                /* Page Definitions */
                                @page WordSection1 {{
                                    size: 8.5in 11.0in;
                                    margin: 56.7pt 56.7pt 56.7pt 85.05pt;
                                }}

                                div.WordSection1 {{
                                    page: WordSection1;
                                }}
                                .WordSection1 table td, tr{{
                                    border: 1px black solid; 

                                }}
                                -->
                            </style>

                        </head>

                        <body lang = VI style='word-wrap:break-word; width: 100%'>

                            <div class=WordSection1>
                             <center>   <h2 class='MsoNormal' style='margin-bottom:6.0pt;line-height:150%; width: 100%; text-align: center;' >
                                                    <span lang = VI style='color:black'>DANH SÁCH CÁC CÁN BỘ ĐƯỢC XÁC MINH KÊ KHAI TÀI SẢN NĂM {lapkehoachxacminh.KeHoachNam}</span>
                                                </h2></center>
                            <table class=MsoNormalTable border=0 cellspacing=0 cellpadding=0 style='border-collapse:collapse; padding:0; margin: 0; width: 100%'>
                                    <tr>
                                        <td valign='top' >

                                                 <p class='MsoNormal' style='margin-bottom:6.0pt;line-height:150%; width: 100%; text-align: center;' >
                                                    <span lang = VI style='color:black'>STT</span>
                                                </p>
                                        </td>
                                        <td valign='top' >

                                              <p class='MsoNormal' style = 'margin-bottom:6.0pt;line-height:150%; width: 100%; text-align: center;' ><span lang = VI style='color:black'>Họ và tên</span></p>
                                        </td>
                                        <td valign='top' >

                                                 <p class='MsoNormal' style='margin-bottom:6.0pt;line-height:150%; width: 100%; text-align: center;' >
                                                    <span lang = VI style='color:black'>Ngày sinh</span>
                                                </p>
                                        </td>
                                        <td valign='top' >

                                              <p class='MsoNormal' style = 'margin-bottom:6.0pt;line-height:150%; width: 100%; text-align: center;' ><span lang = VI style='color:black'>Số CCCD</span></p>
                                        </td>
                                        <td valign='top' >

                                                 <p class='MsoNormal' style='margin-bottom:6.0pt;line-height:150%; width: 100%; text-align: center;' >
                                                    <span lang = VI style='color:black'>Cơ Quan - Đơn vị</span>
                                                </p>
                                        </td>
                                        <td valign='top' >

                                              <p class='MsoNormal' style = 'margin-bottom:6.0pt;line-height:150%; width: 100%; text-align: center;' ><span lang = VI style='color:black'>Chức vụ - chức danh</span></p>
                                        </td>
                                    </tr>

                        <tbody id = 'DanhSachCoQuanDuocXacMinh' >
           ";

                var i = 0;
                foreach (var canbo in lapkehoachxacminh_chitiet)
                {
                    i++;
                    html += $@"<tr style='border: 1px black solid; font-family: 'Times New Roman';'>
                                <td valign='top' >

                                            <p class='MsoNormal' style='margin-bottom:6.0pt;line-height:150%; width: 100%; text-align: center;' >
                                            <span lang = VI style='color:black; text-align: center;'>{i}</span>
                                        </p>
                                </td>
                                <td valign='top' >

                                        <p class='MsoNormal' style = 'margin-bottom:6.0pt;line-height:150%' ><span lang = VI style='color:black'>{canbo.cb.HoTen}</span></p>
                                </td>
                                <td valign='top' >

                                            <p class='MsoNormal' style='margin-bottom:6.0pt;line-height:150%; width: 100%; text-align: center;' >
                                            <span lang = VI style='color:black'>{canbo.cb.DoB}</span>
                                        </p>
                                </td>
                                <td valign='top' >

                                        <p class='MsoNormal' style = 'margin-bottom:6.0pt;line-height:150%; width: 100%; text-align: center;' ><span lang = VI style='color:black'>{canbo.cb.SoCCCD}</span></p>
                                </td>
                                <td valign='top' >

                                            <p class='MsoNormal' style='margin-bottom:6.0pt;line-height:150%' >
                                            <span lang = VI style='color:black; width: 100%; text-align: center;'>{canbo.cq.Ten}</span>
                                        </p>
                                </td>
                                <td valign='top' >

                                        <p class='MsoNormal' style = 'margin-bottom:6.0pt;line-height:150%; width: 100%; text-align: center;' ><span lang = VI style='color:black'>{canbo.cv.Ten_ChucVu_ChucDanh}</span></p>
                                </td>
                            </tr>
                           ";
                }
                html += $@"
                            </tbody>
                           </table> 
                            </body>

                        </html> ";

            }







            string filename = System.Guid.NewGuid().ToString();
            string _filename = filename + ".pdf";
            string _path = Path.Combine(Server.MapPath("~/Content/uploads"), _filename);
            ConverterProperties properties = new ConverterProperties();
            properties.SetFontProvider(new DefaultFontProvider(true, true, true));
            HtmlConverter.ConvertToPdf(html, new FileStream(_path, FileMode.Create), properties);

            return Json(filename, JsonRequestBehavior.AllowGet);

        }

        public ActionResult GetFileDinhKem(int id)
        {
            var data = db.NV_LapKeHoachXacMinh_DanhSachCanBoXacMinh.Where(_ => _.ID_DanhSach == id).FirstOrDefault().FileDinhKem;
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult DinhKemFile(int ID_DanhSach, HttpPostedFileBase FileDinhKem)
        {
            var DanhSach = db.NV_LapKeHoachXacMinh_DanhSachCanBoXacMinh.Find(ID_DanhSach);
            try
            {
                if (FileDinhKem != null)
                {
                    var nameIMG = string.Join("", Regex.Split(DateTime.Now.ToString(), @"\D+")) + "-" + FileDinhKem.FileName;
                    DanhSach.FileDinhKem = nameIMG;

                    string path = Path.Combine(Server.MapPath("~/Content/uploads"), Path.GetFileName(nameIMG));
                    FileDinhKem.SaveAs(path);
                }

                db.Entry(DanhSach).State = EntityState.Modified;
                db.SaveChanges();

                return Json(new { status = "success" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { status = "error" }, JsonRequestBehavior.AllowGet);
            }

        }

        public JsonResult HoanThanhDanhSach(int id)
        {
            var DanhSach = db.NV_LapKeHoachXacMinh_DanhSachCanBoXacMinh.Find(id);

            if (DanhSach.FileDinhKem == "")
            {
                return Json(new { status = "warning", title = "Cảnh Báo", message = "File Danh Sách Chưa Được Đính Kèm. Vui Lòng Kiểm Tra Lại!" });
            }

            DanhSach.TrangThai = true;
            db.Entry(DanhSach).State = EntityState.Modified;
            db.SaveChanges();

            return Json(new { status = "success", title = "Thành Công", message = "Danh Sách Đã Được Hoàn Thành!" });

        }

        public class CanBoKeKhai
        {
            public int MaCanBo { get; set; }
            public string NgaySinh { get; set; }

            public string HoTen { get; set; }
            public string CCCD { get; set; }
            public string TenCoQuan { get; set; }
            public string ChucVu { get; set; }
            public int orderNTK { get; set; }

            public string MaQuyen { get; set; }

            public bool TrangThaiKK { get; set; }
            
            
        }
    }
}
