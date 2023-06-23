using iText.Html2pdf;
using iText.Html2pdf.Resolver.Font;
using KeKhaiTaiSanThuNhap.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace KeKhaiTaiSanThuNhap.Controllers
{
    [Authorize(Roles = "ADMIN")]
    public class NV_BaoCaoKetQuaKeKhaiController : Controller
    {
       
        private KSTNEntities db = new KSTNEntities();
        private UserInfo user = new UserInfo();
        public ActionResult Index()
        {
            if (user.CheckQuyen("NV_BaoCaoTienDo", "Xem"))
            {
                return new HttpStatusCodeResult(404, "Not found");
            }
            ViewBag.Role = user.GetRole();
            return View();
        }

        [HttpPost]
        public ActionResult Create([Bind(Include = "ID,LoaiKeHoachKeKhai,TenBaoCao,NamBaoCao,ID_CoQuan")] NV_BaoCaoTienDo nV_BaoCaoTienDo)
        {
            if (user.CheckQuyen("NV_BaoCaoTienDo", "Them"))
            {
                return Json("Không Có Quyền Truy Cập", JsonRequestBehavior.AllowGet);
            }

            var MaCoQuan = user.GetUserCoQuan();
            var data = db.NV_BaoCaoTienDo.Where(_ => _.NamBaoCao == nV_BaoCaoTienDo.NamBaoCao && _.LoaiKeHoachKeKhai == nV_BaoCaoTienDo.LoaiKeHoachKeKhai && _.ID_CoQuan == MaCoQuan).ToList();
            if(data.Count() != 0)
            {
                return Json(new { status = "warning", title = "Cảnh Báo", message = "Báo Cáo Đã Được Lập Trước Đó. Vui Lòng Kiểm Tra Lại!" });
            }
            else
            {
                if(nV_BaoCaoTienDo.LoaiKeHoachKeKhai == 3)
                {
                    nV_BaoCaoTienDo.TenBaoCao = "Báo Cáo Kết Quả Kê Khai Tài Sản, Thu Nhập Lần Đầu " + nV_BaoCaoTienDo.NamBaoCao;
                }
                else
                {
                    nV_BaoCaoTienDo.TenBaoCao = "Báo Cáo Kết Quả Kê Khai Tài Sản, Thu Nhập Hằng Năm " + nV_BaoCaoTienDo.NamBaoCao;
                }
                nV_BaoCaoTienDo.ID_CoQuan = MaCoQuan;
                nV_BaoCaoTienDo.TenFile = "";
                nV_BaoCaoTienDo.TrangThai = false;
                db.NV_BaoCaoTienDo.Add(nV_BaoCaoTienDo);
                db.SaveChanges();
                return Json(new { status = "success", title = "Thành Công", message = "Báo Cáo Đã Được Lập Thành Công!" });
            }
           
        }


        public JsonResult LoadDataBaoCao()
        {

            var MaCanBo = user.GetUser();
            var Role = user.GetRole();
            var maCoQuan = user.GetUserCoQuan();
            var draw = Request.Form.GetValues("draw").FirstOrDefault();
            var start = Request.Form.GetValues("start").FirstOrDefault();
            var length = Request.Form.GetValues("length").FirstOrDefault();
            var search = Request.Form.GetValues("columns[0][search][value]").FirstOrDefault();
            var coquan_search = Request.Form.GetValues("columns[1][search][value]").FirstOrDefault();
            var loaikehoach_search = Request.Form.GetValues("columns[2][search][value]").FirstOrDefault();
            var kehoachnam_search = Request.Form.GetValues("columns[3][search][value]").FirstOrDefault();
            int pageSize = length != null ? Convert.ToInt32(length) : 0;
            int skip = start != null ? Convert.ToInt32(start) : 0;
            int recordsTotal = 0;
            var MaCoQuan = user.GetUserCoQuan();
            var data = (from bctd in db.NV_BaoCaoTienDo
                        join lkh in db.DM_Loai_KeKhai on bctd.LoaiKeHoachKeKhai equals lkh.Ma_Loai_KeKhai
                        join cq in db.DM_CoQuanDonVi on bctd.ID_CoQuan equals cq.Ma_CoQuan_DonVi
                        orderby bctd.NamBaoCao descending
                        select new { bctd.ID, bctd.ID_CoQuan, bctd.TenFile, bctd.NoiDung, cq.Ten, lkh.Ten_KeKhai, bctd.TenBaoCao, bctd.NamBaoCao, bctd.LoaiKeHoachKeKhai, bctd.TrangThai }).ToList();

            if(Role == "ADMIN" || Role == "NDDTTT")
            {
                data = data.Where(_ => _.TrangThai == true).ToList();
            }
            else
            {
                data = data.Where(_ => _.ID_CoQuan == maCoQuan).ToList();
            }

            if (!string.IsNullOrEmpty(search))
            {
                data = data.Where(a => a.TenBaoCao.ToUpper().Contains(search.ToUpper())).ToList();
            }
            if (!string.IsNullOrEmpty(coquan_search))
            {
                var maCoQuan_search = Int32.Parse(coquan_search);
                if(maCoQuan_search != 0)
                {
                    data = data.Where(a => a.ID_CoQuan == maCoQuan_search).ToList();

                }
            }
            if (!string.IsNullOrEmpty(loaikehoach_search))
            {
                var maloaikehoach_search = Int32.Parse(loaikehoach_search);
                if (maloaikehoach_search != 0)
                {
                    data = data.Where(a => a.LoaiKeHoachKeKhai == maloaikehoach_search).ToList();
                }
            }
            if (!string.IsNullOrEmpty(kehoachnam_search))
            {
                var maKeHoachNam_search = Int32.Parse(kehoachnam_search);
                if (maKeHoachNam_search > 0)
                {
                    data = data.Where(a => a.NamBaoCao == maKeHoachNam_search).ToList();
                }
            }

            recordsTotal = data.Count();
            var data1 = data.Skip(skip).Take(pageSize).ToList();
            return Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data1 }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult BanIn(string id)
        {
            ViewBag.TenFile = id + ".pdf";
            return View();
        }
        
        public ActionResult download(int ID)
        {
            var CTT = db.NV_BaoCaoTienDo.Single(_ => _.ID == ID);
            var url = Path.Combine(Server.MapPath("~/Content/uploads"), CTT.TenFile);
            byte[] fileBytes = System.IO.File.ReadAllBytes(url);
            string fileName = CTT.TenFile;
            return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
        }

        public JsonResult DinhKemFile(int MaBaoCao, string NoiDung, HttpPostedFileBase TenFile)
        {
            var BaoCaoKetQuaKeKhai = db.NV_BaoCaoTienDo.Find(MaBaoCao);
            try
            {
                BaoCaoKetQuaKeKhai.NoiDung = NoiDung;
                //Method 2 Get file details from HttpPostedFileBase class    
                if (TenFile != null)
                {
                    var nameIMG = string.Join("", Regex.Split(DateTime.Now.ToString(), @"\D+")) + "-" +TenFile.FileName;
                    BaoCaoKetQuaKeKhai.TenFile = nameIMG;

                    string path = Path.Combine(Server.MapPath("~/Content/uploads"), Path.GetFileName(nameIMG));
                    TenFile.SaveAs(path);
                }

                db.Entry(BaoCaoKetQuaKeKhai).State = EntityState.Modified;
                db.SaveChanges();

                return Json(new { status = "success" }, JsonRequestBehavior.AllowGet);
            }catch (Exception ex)
            {
                return Json(new { status = "error" }, JsonRequestBehavior.AllowGet);
            }
           
        }

        public JsonResult HoanThanhBaoCao(int ID)
        {
            var BaoCaoKetQuaKeKhai = db.NV_BaoCaoTienDo.Find(ID);

            if(BaoCaoKetQuaKeKhai.TenFile == ""){
                return Json(new { status = "warning", title = "Cảnh Báo", message = "File Báo Cáo Chưa Được Đính Kèm. Vui Lòng Kiểm Tra Lại!" });
            }

            BaoCaoKetQuaKeKhai.TrangThai = true;
            db.Entry(BaoCaoKetQuaKeKhai).State = EntityState.Modified;
            db.SaveChanges();

            return Json(new { status = "success", title = "Thành Công", message = "Báo Cáo Đã Được Hoàn Thành!" });
           
        }

        public ActionResult GetFileDinhKem(int id)
        {
            var IDCoQuan = user.GetUserCoQuan();
            var data = db.NV_BaoCaoTienDo.Where(_ => _.ID == id && _.ID_CoQuan == IDCoQuan).FirstOrDefault().TenFile;
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public ActionResult  InBanBaoCao(int LoaiKeHoachKeKhai, int NamBaoCao)
        {
            var html = "";
           var TongCoQuan = 0;
            var CoQuanDaToChuckk = 0;
            var TyLeCoQuan = 0.0;
            var CoQuanChuaToChuckk = 0;
            var TyLeCoQuanDaToChuckk = 0.0;
            var TyLeCoQuanChuaToChuckk = 0.0;
            var SoNguoiPhaiKeKhaiLanDau = 0;
            var SoNguoiDaKeKhaiLanDau = 0;
            var DanhSachCanBoCoNghiaVuKeKhai = "";

            if (LoaiKeHoachKeKhai == 3)
            {
                var MaCoQuan = user.GetUserCoQuan();
                var kehoachkekhai = db.NV_LapKeHoachKeKhai.Where(_ => _.Ma_Loai_KeKhai == 3 && _.KeHoachNam == NamBaoCao).Select(_ => _.MaKeHoachKeKhai).ToList();
                var kehoachkekhai_TongCoQuan = db.NV_LapKeHoachKeKhai_DanhSachKeKhaiLanDau.Where(_ => kehoachkekhai.Contains((int)_.MaKeHoachKeKhai) && _.Ma_CoQuan_DonVi == MaCoQuan).ToList();
                var kehoachkekhai_CoQuanChuaToChucKK = db.NV_LapKeHoachKeKhai_DanhSachKeKhaiLanDau.Where(_ => kehoachkekhai.Contains((int)_.MaKeHoachKeKhai) && _.TrangThai == true && _.Ma_CoQuan_DonVi == MaCoQuan).ToList();
                var kehoachkekhailandau = db.NV_LapKeHoachKeKhai_DanhSachKeKhaiLanDau.Where(_ => kehoachkekhai.Contains((int)_.MaKeHoachKeKhai) && _.TrangThai == true && _.Ma_CoQuan_DonVi == MaCoQuan).Select(_ => _.MaKeHoachKeKhai_DanhSachKeKhaiLanDau_ID);
                var kehoachkekhailandau_ChiTiet = (from khkkld_ct in db.NV_LapKeHoachKeKhai_DanhSachKeKhaiLanDau_ChiTiet
                                                   join cb in db.DM_CanBo on khkkld_ct.Ma_CanBo equals cb.Ma_CanBo
                                                   where kehoachkekhailandau.Contains((int)khkkld_ct.MaKeHoachKeKhai_DanhSachKeKhaiLanDau_ID) && cb.Ma_CoQuan_DonVi == MaCoQuan 
                                                   select new { cb }
                                                   ).ToList();

                var dataLapDanhSachCanBo = (from lkhkk in db.NV_LapKeHoachKeKhai
                                            join lkhkkld in db.NV_LapKeHoachKeKhai_DanhSachKeKhaiLanDau on lkhkk.MaKeHoachKeKhai equals lkhkkld.MaKeHoachKeKhai
                                            join lkhkkld_ct in db.NV_LapKeHoachKeKhai_DanhSachKeKhaiLanDau_ChiTiet on lkhkkld.MaKeHoachKeKhai_DanhSachKeKhaiLanDau_ID equals lkhkkld_ct.MaKeHoachKeKhai_DanhSachKeKhaiLanDau_ID
                                            join cb in db.DM_CanBo on lkhkkld_ct.Ma_CanBo equals cb.Ma_CanBo
                                            where lkhkk.KeHoachNam == NamBaoCao
                                            select new { cb, lkhkkld_ct }).ToList();

                var dataCanBoDaKeKhai = (from bkk in db.Nv_KeKhai_TSTN
                                         join lkk in db.DM_Loai_KeKhai on bkk.Ma_Loai_KeKhai equals lkk.Ma_Loai_KeKhai
                                         join cb in db.DM_CanBo on bkk.Ma_CanBo equals cb.Ma_CanBo
                                         join cq in db.DM_CoQuanDonVi on cb.Ma_CoQuan_DonVi equals cq.Ma_CoQuan_DonVi
                                         join cv in db.DM_ChucVu_ChucDanh on cb.Ma_ChucVu_ChucDanh equals cv.Ma_ChucVu_ChucDanh
                                         where kehoachkekhai.Contains((int)bkk.MaKeHoachKeKhai) && cb.Ma_CoQuan_DonVi == MaCoQuan
                                         select new { bkk }).ToList();


                TongCoQuan = kehoachkekhai_TongCoQuan.Count();
                CoQuanDaToChuckk = kehoachkekhailandau.Count();
                CoQuanChuaToChuckk = kehoachkekhai_CoQuanChuaToChucKK.Count();

                SoNguoiPhaiKeKhaiLanDau = kehoachkekhailandau_ChiTiet.Count();
                SoNguoiDaKeKhaiLanDau = dataCanBoDaKeKhai.Count();
                if(TongCoQuan == 0)
                {

                    TyLeCoQuanDaToChuckk = 0;
                    TyLeCoQuanChuaToChuckk = 0;
                }
                else
                {
                    TyLeCoQuanDaToChuckk = (float)((float)CoQuanDaToChuckk / (float)TongCoQuan) * 100;
                    TyLeCoQuanChuaToChuckk = (float)((float)CoQuanChuaToChuckk / (float)TongCoQuan) * 100;

                }
               
               

                html = $@"<html>
                        <head>
                            <meta http-equiv=Content-Type content='text/html; charset=utf-8'>
                            <meta name=Generator content='Microsoft Word 15 (filtered)'>
                            <style>
                                  p .MsoNormal, li.MsoNormal, div.MsoNormal
	                            {{margin:0cm;
	                            margin-bottom:.0001pt;
	                            font-size:12.0pt;
	                            font-family:'Times New Roman',serif;

                                color: black;
                                 }}
                                @font-face {{
                                    font-family: 'Times New Roman',serif;
                                    panose-1: 2 4 5 3 5 4 6 3 2 4;
                                }}
                                html{{  margin: 0cm 0cm 0cm 0.5cm;}}
                                    th,td,p,div,b {{margin:0;padding:0}}
                                    /* Style Definitions */
                                    p.MsoNormal,
                                    li.MsoNormal,
                                div.MsoNormal {{
                                    margin: 0in;
                                    font-size: 13px;
                                    font-family: 'Times New Roman', serif;
                                }}
                                    span.Vnbnnidung6
	                            {{mso-style-name:'V\0103n b\1EA3n n\1ED9i dung \(6\)_';
	                            mso-style-link:'V\0103n b\1EA3n n\1ED9i dung \(6\)';
	                            font-family:'Times New Roman',serif;
	                            background:white;
	                            font-weight:bold;}}
                                p.Vnbnnidung60, li.Vnbnnidung60, div.Vnbnnidung60
	                            {{mso-style-name:'V\0103n b\1EA3n n\1ED9i dung \(6\)';
	                            mso-style-link:'V\0103n b\1EA3n n\1ED9i dung \(6\)_';
	                            margin:0cm;
	                            margin-bottom:.0001pt;
	                            line-height:12.5pt;
	                            background:white;
	                            font-size:9.5pt;
	                            font-family:'Times New Roman',serif;
	                            font-weight:bold;}}

                                span.Vnbnnidung213pt
                                {{
                                mso - style - name:'V\0103n b\1EA3n n\1ED9i dung \(2\) + 13 pt\,Không in \0111\1EADm';
                                font - family:'Times New Roman',serif;
                                font - variant:normal!important;
                                color: black;
                                position: relative;
                                top: 0pt;
                                    letter - spacing:0pt;
                                    font - weight:normal;
                                    font - style:normal;
                                    text - decoration:none none;
                                }}
                                @page WordSection1
                                {{
                                    size:21.0cm 842.0pt;
                                    margin:51.05pt 48.2pt 36.85pt 70.9pt;
                                }}
                                div.WordSection1

                                {{ page: WordSection1; }}
                            </style>

                        </head>
                        
                        <body lang=EN-US style='word-wrap:break-word'>

                        <div class=WordSection1>

                        <p class=Vnbnnidung60 align=center style='margin-left:2.0pt;text-align:center;
                        line-height:normal;background:transparent'><span style='font-size:13.0pt'>MẪU
                        BÁO CÁO</span></p>

                        <p class=Vnbnnidung60 style='margin-left:2.0pt;line-height:normal;background:
                        transparent'><i><span style='font-size:13.0pt;font-weight:normal'>(Kèm theo Văn
                        bản số             /UBND ngày    /   /2021 của UBND tỉnh)</span></i></p>

                        <p class=Vnbnnidung60 style='line-height:normal;background:transparent'><span
                        style='font-size:13.0pt'>&nbsp;</span></p>

                        <table class=MsoNormalTable border=1 cellspacing=0 cellpadding=0 width='100%'
                         style='width:100.0%;border-collapse:collapse;border:none'>
                         <tr>
                          <td width='34%' valign=top style='width:34.34%;border:none;padding:0in 5.4pt 0in 5.4pt'>
                          <p class=MsoNormal align=center style='text-align:center'><b><span
                          style='font-size:13.0pt;color:windowtext'>Cơ
                          quan, tổ chức, đơn vị<br>
                          --------</span></b></p>
                          </td>
                          <td width='65%' valign=top style='width:65.66%;border:none;padding:0in 5.4pt 0in 5.4pt'>
                          <p class=MsoNormal align=center style='text-align:center'><b><span
                          style='font-size:13.0pt;color:windowtext'>CỘNG
                          HÒA XÃ HỘI CHỦ NGHĨA VIỆT NAM<br>
                          Độc lập - Tự do - Hạnh phúc <br>
                          ---------------</span></b></p>
                          </td>
                         </tr>
                         <tr>
                          <td width='34%' valign=top style='width:34.34%;border:none;padding:0in 5.4pt 0in 5.4pt'>
                          <p class=MsoNormal align=center style='text-align:center'><span
                          style='font-size:13.0pt;color:windowtext'>Số:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                          /</span></p>
                          </td>
                          <td width='65%' valign=top style='width:65.66%;border:none;padding:0in 5.4pt 0in 5.4pt'>
                          <p class=MsoNormal align=center style='text-align:center'><i><span
                          style='font-size:13.0pt;color:windowtext'>…………,
                          ngày&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; tháng&nbsp;&nbsp;&nbsp;&nbsp; năm</span></i></p>
                          </td>
                         </tr>
                        </table>

                        <p class=MsoNormal style='margin-bottom:6.0pt'><b><span style='color:windowtext'>&nbsp;</span></b></p>

                        <p class=MsoNormal align=center style='margin-bottom:6.0pt;text-align:center'><b><span
                        style='font-size:13.0pt;color:windowtext'>BÁO
                        CÁO</span></b></p>

                        <p class=MsoNormal align=center style='margin-bottom:6.0pt;text-align:center'><b><span
                        style='font-size:13.0pt;color:windowtext'>Kết
                        quả triển khai công tác kê khai tài sản, thu nhập</span></b></p>

                        <p class=MsoNormal style='margin-top:6.0pt;margin-right:0in;margin-bottom:6.0pt;
                        margin-left:0in;text-align:justify;text-indent:28.35pt;line-height:16.0pt'><b><span
                        style='font-size:13.0pt;color:windowtext'>1.
                        Quá trình chỉ đạo, triển khai, tổ chức thực hiện quy định về kê khai tài sản,
                        thu nhập</span></b></p>

                        <p class=MsoNormal style='margin-top:6.0pt;margin-right:0in;margin-bottom:6.0pt;
                        margin-left:0in;text-align:justify;text-indent:28.35pt;line-height:16.0pt'><span
                        style='font-size:13.0pt;color:windowtext'>-
                        Phạm vi, đặc điểm tổ chức, hoạt động của cơ quan, tổ chức, đơn vị;</span></p>

                        <p class=MsoNormal style='margin-top:6.0pt;margin-right:0in;margin-bottom:6.0pt;
                        margin-left:0in;text-align:justify;text-indent:28.35pt;line-height:16.0pt'><span
                        style='font-size:13.0pt;color:windowtext'>-
                        Các văn bản pháp luật áp dụng;</span></p>

                        <p class=MsoNormal style='margin-top:6.0pt;margin-right:0in;margin-bottom:6.0pt;
                        margin-left:0in;text-align:justify;text-indent:28.35pt;line-height:16.0pt'><span
                        style='font-size:13.0pt;color:windowtext'>-
                        Các văn bản chỉ đạo, đôn đốc của cấp trên;</span></p>

                        <p class=MsoNormal style='margin-top:6.0pt;margin-right:0in;margin-bottom:6.0pt;
                        margin-left:0in;text-align:justify;text-indent:28.35pt;line-height:16.0pt'><span
                        style='font-size:13.0pt;color:windowtext'>-
                        Các văn bản cơ quan, đơn vị đã triển khai như: Kế hoạch, phương án, tổ chức
                        tuyên truyền…..</span></p>

                        <p class=MsoNormal style='margin-top:6.0pt;margin-right:0in;margin-bottom:6.0pt;
                        margin-left:0in;text-align:justify;text-indent:28.35pt;line-height:16.0pt'><b><span
                        style='font-size:13.0pt;color:windowtext'>2.
                        Kết quả thực hiện kê khai tài sản, thu nhập</span></b></p>

                        <p class=MsoNormal style='margin-top:6.0pt;margin-right:0in;margin-bottom:6.0pt;
                        margin-left:0in;text-align:justify;text-indent:28.35pt;line-height:16.0pt'><span
                        style='font-size:13.0pt;color:windowtext'>-
                        Kết quả kê khai, công khai Bản kê khai tài sản, thu nhập (TSTN):</span></p>

                        <table class=MsoNormalTable border=1 cellspacing=0 cellpadding=0 width='100%'
                         style='width:100.0%;border-collapse:collapse;border:none'>
                         <tr>
                          <td width='7%' valign=top style='width:7.14%;border:solid windowtext 1.0pt;
                          padding:0in 5.4pt 0in 5.4pt'>
                          <p class=MsoNormal style='margin-top:6.0pt;margin-right:0in;margin-bottom:
                          6.0pt;margin-left:0in;text-align:justify;line-height:16.0pt'><b><span
                          style='font-size:13.0pt;color:windowtext'>TT</span></b></p>
                          </td>
                          <td width='52%' valign=top style='width:52.44%;border:solid windowtext 1.0pt;
                          border-left:none;padding:0in 5.4pt 0in 5.4pt'>
                          <p class=MsoNormal style='margin-top:6.0pt;margin-right:0in;margin-bottom:
                          6.0pt;margin-left:0in;text-align:justify;line-height:16.0pt'><b><span
                          style='font-size:13.0pt;color:windowtext'>NỘI
                          DUNG</span></b></p>
                          </td>
                          <td width='20%' valign=top style='width:20.96%;border:solid windowtext 1.0pt;
                          border-left:none;padding:0in 5.4pt 0in 5.4pt'>
                          <p class=MsoNormal style='margin-top:6.0pt;margin-right:0in;margin-bottom:
                          6.0pt;margin-left:0in;text-align:justify;line-height:16.0pt'><b><span
                          style='font-size:13.0pt;color:windowtext'>ĐƠN
                          VỊ TÍNH</span></b></p>
                          </td>
                          <td width='19%' valign=top style='width:19.48%;border:solid windowtext 1.0pt;
                          border-left:none;padding:0in 5.4pt 0in 5.4pt'>
                          <p class=MsoNormal style='margin-top:6.0pt;margin-right:0in;margin-bottom:
                          6.0pt;margin-left:0in;text-align:justify;line-height:16.0pt'><b><span
                          style='font-size:13.0pt;color:windowtext'>SỐ
                          LIỆU</span></b></p>
                          </td>
                         </tr>
                         <tr>
                          <td width='7%' valign=top style='width:7.14%;border:solid windowtext 1.0pt;
                          border-top:none;padding:0in 5.4pt 0in 5.4pt'>
                          <p class=MsoNormal style='margin-top:6.0pt;margin-right:0in;margin-bottom:
                          6.0pt;margin-left:0in;text-align:justify;line-height:16.0pt'><b><span
                          style='font-size:13.0pt;color:windowtext'>I</span></b></p>
                          </td>
                          <td width='52%' valign=top style='width:52.44%;border-top:none;border-left:
                          none;border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
                          padding:0in 5.4pt 0in 5.4pt'>
                          <p class=MsoNormal style='margin-top:6.0pt;margin-right:0in;margin-bottom:
                          6.0pt;margin-left:0in;text-align:justify;line-height:16.0pt'><b><span
                          style='font-size:13.0pt;color:windowtext'>Kê
                          khai tài sản, thu nhập</span></b></p>
                          </td>
                          <td width='20%' valign=top style='width:20.96%;border-top:none;border-left:
                          none;border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
                          padding:0in 5.4pt 0in 5.4pt'>
                          <p class=MsoNormal style='margin-top:6.0pt;margin-right:0in;margin-bottom:
                          6.0pt;margin-left:0in;text-align:justify;line-height:16.0pt'><span
                          style='font-size:13.0pt;color:windowtext'>&nbsp;</span></p>
                          </td>
                          <td width='19%' valign=top style='width:19.48%;border-top:none;border-left:
                          none;border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
                          padding:0in 5.4pt 0in 5.4pt'>
                          <p class=MsoNormal style='margin-top:6.0pt;margin-right:0in;margin-bottom:
                          6.0pt;margin-left:0in;text-align:justify;line-height:16.0pt'><span
                          style='font-size:13.0pt;color:windowtext'>&nbsp;</span></p>
                          </td>
                         </tr>
                         <tr>
                          <td width='7%' valign=top style='width:7.14%;border:solid windowtext 1.0pt;
                          border-top:none;padding:0in 5.4pt 0in 5.4pt'>
                          <p class=MsoNormal style='margin-top:6.0pt;margin-right:0in;margin-bottom:
                          6.0pt;margin-left:0in;text-align:justify;line-height:16.0pt'><span
                          style='font-size:13.0pt;color:windowtext'>1</span></p>
                          </td>
                          <td width='52%' valign=top style='width:52.44%;border-top:none;border-left:
                          none;border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
                          padding:0in 5.4pt 0in 5.4pt'>
                          <p class=MsoNormal style='margin-top:6.0pt;margin-right:0in;margin-bottom:
                          6.0pt;margin-left:0in;text-align:justify;line-height:16.0pt'><span
                          style='font-size:13.0pt;color:windowtext'>Tổng
                          số cơ quan, tổ chức, đơn vị phải tổ chức thực hiện việc kê khai TSTN (nếu chỉ
                          có 01 đơn vị thì ghi 01, trường hợp có các đơn vị trực thuộc thì ghi tổng số
                          đơn vị trực thuộc đồng thời liệt kê cụ thể tên các đơn vị trực thuộc)</span></p>
                          </td>
                          <td width='20%' valign=top style='width:20.96%;border-top:none;border-left:
                          none;border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
                          padding:0in 5.4pt 0in 5.4pt'>
                          <p class=MsoNormal align=center style='margin-top:6.0pt;margin-right:0in;
                          margin-bottom:6.0pt;margin-left:0in;text-align:center;line-height:16.0pt'><span
                          style='font-size:13.0pt;color:windowtext'>CQTCĐV</span></p>
                          </td>
                          <td width='19%' valign=top style='width:19.48%;border-top:none;border-left:
                          none;border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
                          padding:0in 5.4pt 0in 5.4pt'>
                          <p class=MsoNormal style='margin-top:6.0pt;margin-right:0in;margin-bottom:
                          6.0pt;margin-left:0in;text-align:center;line-height:16.0pt'><span
                          style='font-size:13.0pt;color:windowtext'>{TongCoQuan}</span></p>
                          </td>
                         </tr>
                         <tr>
                          <td width='7%' valign=top style='width:7.14%;border:solid windowtext 1.0pt;
                          border-top:none;padding:0in 5.4pt 0in 5.4pt'>
                          <p class=MsoNormal style='margin-top:6.0pt;margin-right:0in;margin-bottom:
                          6.0pt;margin-left:0in;text-align:center;line-height:16.0pt'><span
                          style='font-size:13.0pt;color:windowtext'>2</span></p>
                          </td>
                          <td width='52%' valign=top style='width:52.44%;border-top:none;border-left:
                          none;border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
                          padding:0in 5.4pt 0in 5.4pt'>
                          <p class=MsoNormal style='margin-top:6.0pt;margin-right:0in;margin-bottom:
                          6.0pt;margin-left:0in;text-align:justify;line-height:16.0pt'><span
                          style='font-size:13.0pt;color:windowtext'>Số
                          cơ quan, tổ chức, đơn vị đã tổ chức thực hiện việc kê khai tài sản, thu nhập
                          (ghi tương tự mục 1)</span></p>
                          </td>
                          <td width='20%' valign=top style='width:20.96%;border-top:none;border-left:
                          none;border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
                          padding:0in 5.4pt 0in 5.4pt'>
                          <p class=MsoNormal align=center style='margin-top:6.0pt;margin-right:0in;
                          margin-bottom:6.0pt;margin-left:0in;text-align:center;line-height:16.0pt'><span
                          style='font-size:13.0pt;color:windowtext'>CQTCĐV</span></p>
                          </td>
                          <td width='19%' valign=top style='width:19.48%;border-top:none;border-left:
                          none;border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
                          padding:0in 5.4pt 0in 5.4pt'>
                          <p class=MsoNormal style='margin-top:6.0pt;margin-right:0in;margin-bottom:
                          6.0pt;margin-left:0in;text-align:center;line-height:16.0pt'><span
                          style='font-size:13.0pt;color:windowtext'>{CoQuanDaToChuckk}</span></p>
                          </td>
                         </tr>
                         <tr>
                          <td width='7%' valign=top style='width:7.14%;border:solid windowtext 1.0pt;
                          border-top:none;padding:0in 5.4pt 0in 5.4pt'>
                          <p class=MsoNormal style='margin-top:6.0pt;margin-right:0in;margin-bottom:
                          6.0pt;margin-left:0in;text-align:center;line-height:16.0pt'><span
                          style='font-size:13.0pt;color:windowtext'>&nbsp;</span></p>
                          </td>
                          <td width='52%' valign=top style='width:52.44%;border-top:none;border-left:
                          none;border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
                          padding:0in 5.4pt 0in 5.4pt'>
                          <p class=MsoNormal style='margin-top:6.0pt;margin-right:0in;margin-bottom:
                          6.0pt;margin-left:0in;text-align:justify;line-height:16.0pt'><span
                          style='font-size:13.0pt;color:windowtext'>Tỷ
                          lệ so với tổng số cơ quan, tổ chức, đơn vị</span></p>
                          </td>
                          <td width='20%' valign=top style='width:20.96%;border-top:none;border-left:
                          none;border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
                          padding:0in 5.4pt 0in 5.4pt'>
                          <p class=MsoNormal align=center style='margin-top:6.0pt;margin-right:0in;
                          margin-bottom:6.0pt;margin-left:0in;text-align:center;line-height:16.0pt'><span
                          style='font-size:13.0pt;color:windowtext'>%</span></p>
                          </td>
                          <td width='19%' valign=top style='width:19.48%;border-top:none;border-left:
                          none;border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
                          padding:0in 5.4pt 0in 5.4pt'>
                          <p class=MsoNormal style='margin-top:6.0pt;margin-right:0in;margin-bottom:
                          6.0pt;margin-left:0in;text-align:center;line-height:16.0pt'><span
                          style='font-size:13.0pt;color:windowtext'>{TyLeCoQuanDaToChuckk}%</span></p>
                          </td>
                         </tr>
                         <tr>
                          <td width='7%' valign=top style='width:7.14%;border:solid windowtext 1.0pt;
                          border-top:none;padding:0in 5.4pt 0in 5.4pt'>
                          <p class=MsoNormal style='margin-top:6.0pt;margin-right:0in;margin-bottom:
                          6.0pt;margin-left:0in;text-align:center;line-height:16.0pt'><span
                          style='font-size:13.0pt;color:windowtext'>3</span></p>
                          </td>
                          <td width='52%' valign=top style='width:52.44%;border-top:none;border-left:
                          none;border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
                          padding:0in 5.4pt 0in 5.4pt'>
                          <p class=MsoNormal style='margin-top:6.0pt;margin-right:0in;margin-bottom:
                          6.0pt;margin-left:0in;text-align:justify;line-height:16.0pt'><span
                          style='font-size:13.0pt;color:windowtext'>Số
                          cơ quan, tổ chức, đơn vị chưa thực hiện hoặc chưa được tổng hợp kết quả trong
                          báo cáo này (ghi tương tự mục 1, 2)</span></p>
                          </td>
                          <td width='20%' valign=top style='width:20.96%;border-top:none;border-left:
                          none;border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
                          padding:0in 5.4pt 0in 5.4pt'>
                          <p class=MsoNormal align=center style='margin-top:6.0pt;margin-right:0in;
                          margin-bottom:6.0pt;margin-left:0in;text-align:center;line-height:16.0pt'><span
                          style='font-size:13.0pt;color:windowtext'>CQTCĐV</span></p>
                          </td>
                          <td width='19%' valign=top style='width:19.48%;border-top:none;border-left:
                          none;border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
                          padding:0in 5.4pt 0in 5.4pt'>
                          <p class=MsoNormal style='margin-top:6.0pt;margin-right:0in;margin-bottom:
                          6.0pt;margin-left:0in;text-align:center;line-height:16.0pt'><span
                          style='font-size:13.0pt;color:windowtext'>{CoQuanChuaToChuckk}</span></p>
                          </td>
                         </tr>
                         <tr>
                          <td width='7%' valign=top style='width:7.14%;border:solid windowtext 1.0pt;
                          border-top:none;padding:0in 5.4pt 0in 5.4pt'>
                          <p class=MsoNormal style='margin-top:6.0pt;margin-right:0in;margin-bottom:
                          6.0pt;margin-left:0in;text-align:center;line-height:16.0pt'><span
                          style='font-size:13.0pt;color:windowtext'>&nbsp;</span></p>
                          </td>
                          <td width='52%' valign=top style='width:52.44%;border-top:none;border-left:
                          none;border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
                          padding:0in 5.4pt 0in 5.4pt'>
                          <p class=MsoNormal style='margin-top:6.0pt;margin-right:0in;margin-bottom:
                          6.0pt;margin-left:0in;text-align:justify;line-height:16.0pt'><span
                          style='font-size:13.0pt;color:windowtext'>Tỷ
                          lệ so với tổng số cơ quan, tổ chức, đơn vị</span></p>
                          </td>
                          <td width='20%' valign=top style='width:20.96%;border-top:none;border-left:
                          none;border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
                          padding:0in 5.4pt 0in 5.4pt'>
                          <p class=MsoNormal align=center style='margin-top:6.0pt;margin-right:0in;
                          margin-bottom:6.0pt;margin-left:0in;text-align:center;line-height:16.0pt'><span
                          style='font-size:13.0pt;color:windowtext'>%</span></p>
                          </td>
                          <td width='19%' valign=top style='width:19.48%;border-top:none;border-left:
                          none;border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
                          padding:0in 5.4pt 0in 5.4pt'>
                          <p class=MsoNormal style='margin-top:6.0pt;margin-right:0in;margin-bottom:
                          6.0pt;margin-left:0in;text-align:center;line-height:16.0pt'><span
                          style='font-size:13.0pt;color:windowtext'>{TyLeCoQuanChuaToChuckk}%</span></p>
                          </td>
                         </tr>
                         <tr>
                          <td width='7%' valign=top style='width:7.14%;border:solid windowtext 1.0pt;
                          border-top:none;padding:0in 5.4pt 0in 5.4pt'>
                          <p class=MsoNormal style='margin-top:6.0pt;margin-right:0in;margin-bottom:
                          6.0pt;margin-left:0in;text-align:center;line-height:16.0pt'><span
                          style='font-size:13.0pt;color:windowtext'>4</span></p>
                          </td>
                          <td width='52%' valign=top style='width:52.44%;border-top:none;border-left:
                          none;border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
                          padding:0in 5.4pt 0in 5.4pt'>
                          <p class=MsoNormal style='margin-top:6.0pt;margin-right:0in;margin-bottom:
                          6.0pt;margin-left:0in;text-align:justify;line-height:16.0pt'><span
                          style='font-size:13.0pt;color:windowtext'>Số
                          người phải kê khai tài sản, thu nhập lần đầu</span></p>
                          </td>
                          <td width='20%' valign=top style='width:20.96%;border-top:none;border-left:
                          none;border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
                          padding:0in 5.4pt 0in 5.4pt'>
                          <p class=MsoNormal align=center style='margin-top:6.0pt;margin-right:0in;
                          margin-bottom:6.0pt;margin-left:0in;text-align:center;line-height:16.0pt'><span
                          style='font-size:13.0pt;color:windowtext'>Người</span></p>
                          </td>
                          <td width='19%' valign=top style='width:19.48%;border-top:none;border-left:
                          none;border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
                          padding:0in 5.4pt 0in 5.4pt'>
                          <p class=MsoNormal style='margin-top:6.0pt;margin-right:0in;margin-bottom:
                          6.0pt;margin-left:0in;text-align:center;line-height:16.0pt'><span
                          style='font-size:13.0pt;color:windowtext'>{SoNguoiPhaiKeKhaiLanDau}</span></p>
                          </td>
                         </tr>
                         <tr>
                          <td width='7%' valign=top style='width:7.14%;border:solid windowtext 1.0pt;
                          border-top:none;padding:0in 5.4pt 0in 5.4pt'>
                          <p class=MsoNormal style='margin-top:6.0pt;margin-right:0in;margin-bottom:
                          6.0pt;margin-left:0in;text-align:center;line-height:16.0pt'><span
                          style='font-size:13.0pt;color:windowtext'>5</span></p>
                          </td>
                          <td width='52%' valign=top style='width:52.44%;border-top:none;border-left:
                          none;border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
                          padding:0in 5.4pt 0in 5.4pt'>
                          <p class=MsoNormal style='margin-top:6.0pt;margin-right:0in;margin-bottom:
                          6.0pt;margin-left:0in;text-align:justify;line-height:16.0pt'><span
                          style='font-size:13.0pt;color:windowtext'>Số
                          người đã kê khai tài sản, thu nhập lần đầu</span></p>
                          </td>
                          <td width='20%' valign=top style='width:20.96%;border-top:none;border-left:
                          none;border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
                          padding:0in 5.4pt 0in 5.4pt'>
                          <p class=MsoNormal align=center style='margin-top:6.0pt;margin-right:0in;
                          margin-bottom:6.0pt;margin-left:0in;text-align:center;line-height:16.0pt'><span
                          style='font-size:13.0pt;color:windowtext'>Người</span></p>
                          </td>
                          <td width='19%' valign=top style='width:19.48%;border-top:none;border-left:
                          none;border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
                          padding:0in 5.4pt 0in 5.4pt'>
                          <p class=MsoNormal style='margin-top:6.0pt;margin-right:0in;margin-bottom:
                          6.0pt;margin-left:0in;text-align:center;line-height:16.0pt'><span
                          style='font-size:13.0pt;color:windowtext'>{SoNguoiDaKeKhaiLanDau}</span></p>
                          </td>
                         </tr>
                         <tr>
                          <td width='7%' valign=top style='width:7.14%;border:solid windowtext 1.0pt;
                          border-top:none;padding:0in 5.4pt 0in 5.4pt'>
                          <p class=MsoNormal style='margin-top:6.0pt;margin-right:0in;margin-bottom:
                          6.0pt;margin-left:0in;text-align:center;line-height:16.0pt'><b><span
                          style='font-size:13.0pt;color:windowtext'>II</span></b></p>
                          </td>
                          <td width='52%' valign=top style='width:52.44%;border-top:none;border-left:
                          none;border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
                          padding:0in 5.4pt 0in 5.4pt'>
                          <p class=MsoNormal style='margin-top:6.0pt;margin-right:0in;margin-bottom:
                          6.0pt;margin-left:0in;text-align:justify;line-height:16.0pt'><b><span
                          style='font-size:13.0pt;color:windowtext'>Công
                          khai Bản kê khai tài sản, thu nhập</span></b></p>
                          </td>
                          <td width='20%' valign=top style='width:20.96%;border-top:none;border-left:
                          none;border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
                          padding:0in 5.4pt 0in 5.4pt'>
                          <p class=MsoNormal align=center style='margin-top:6.0pt;margin-right:0in;
                          margin-bottom:6.0pt;margin-left:0in;text-align:center;line-height:16.0pt'><span
                          style='font-size:13.0pt;color:windowtext'>&nbsp;</span></p>
                          </td>
                          <td width='19%' valign=top style='width:19.48%;border-top:none;border-left:
                          none;border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
                          padding:0in 5.4pt 0in 5.4pt'>
                          <p class=MsoNormal style='margin-top:6.0pt;margin-right:0in;margin-bottom:
                          6.0pt;margin-left:0in;text-align:center;line-height:16.0pt'><span
                          style='font-size:13.0pt;color:windowtext'>&nbsp;</span></p>
                          </td>
                         </tr>
                         <tr>
                          <td width='7%' valign=top style='width:7.14%;border:solid windowtext 1.0pt;
                          border-top:none;padding:0in 5.4pt 0in 5.4pt'>
                          <p class=MsoNormal style='margin-top:6.0pt;margin-right:0in;margin-bottom:
                          6.0pt;margin-left:0in;text-align:center;line-height:16.0pt'><span
                          style='font-size:13.0pt;color:windowtext'>1</span></p>
                          </td>
                          <td width='52%' valign=top style='width:52.44%;border-top:none;border-left:
                          none;border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
                          padding:0in 5.4pt 0in 5.4pt'>
                          <p class=MsoNormal style='margin-top:6.0pt;margin-right:0in;margin-bottom:
                          6.0pt;margin-left:0in;text-align:justify;line-height:16.0pt'><span
                          style='font-size:13.0pt;color:windowtext'>Số
                          cơ quan, tổ chức, đơn vị đã tổ chức thực hiện việc công khai bản kê khai tài
                          sản, thu nhập</span></p>
                          </td>
                          <td width='20%' valign=top style='width:20.96%;border-top:none;border-left:
                          none;border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
                          padding:0in 5.4pt 0in 5.4pt'>
                          <p class=MsoNormal align=center style='margin-top:6.0pt;margin-right:0in;
                          margin-bottom:6.0pt;margin-left:0in;text-align:center;line-height:16.0pt'><span
                          style='font-size:13.0pt;color:windowtext'>CQTCĐV</span></p>
                          </td>
                          <td width='19%' valign=top style='width:19.48%;border-top:none;border-left:
                          none;border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
                          padding:0in 5.4pt 0in 5.4pt'>
                          <p class=MsoNormal style='margin-top:6.0pt;margin-right:0in;margin-bottom:
                          6.0pt;margin-left:0in;text-align:center;line-height:16.0pt'><span
                          style='font-size:13.0pt;color:windowtext'>&nbsp;</span></p>
                          </td>
                         </tr>
                         <tr>
                          <td width='7%' valign=top style='width:7.14%;border:solid windowtext 1.0pt;
                          border-top:none;padding:0in 5.4pt 0in 5.4pt'>
                          <p class=MsoNormal style='margin-top:6.0pt;margin-right:0in;margin-bottom:
                          6.0pt;margin-left:0in;text-align:center;line-height:16.0pt'><span
                          style='font-size:13.0pt;color:windowtext'>&nbsp;</span></p>
                          </td>
                          <td width='52%' valign=top style='width:52.44%;border-top:none;border-left:
                          none;border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
                          padding:0in 5.4pt 0in 5.4pt'>
                          <p class=MsoNormal style='margin-top:6.0pt;margin-right:0in;margin-bottom:
                          6.0pt;margin-left:0in;text-align:justify;line-height:16.0pt'><span
                          style='font-size:13.0pt;color:windowtext'>Tỷ
                          lệ so với tổng số cơ quan, tổ chức, đơn vị</span></p>
                          </td>
                          <td width='20%' valign=top style='width:20.96%;border-top:none;border-left:
                          none;border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
                          padding:0in 5.4pt 0in 5.4pt'>
                          <p class=MsoNormal align=center style='margin-top:6.0pt;margin-right:0in;
                          margin-bottom:6.0pt;margin-left:0in;text-align:center;line-height:16.0pt'><span
                          style='font-size:13.0pt;color:windowtext'>%</span></p>
                          </td>
                          <td width='19%' valign=top style='width:19.48%;border-top:none;border-left:
                          none;border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
                          padding:0in 5.4pt 0in 5.4pt'>
                          <p class=MsoNormal style='margin-top:6.0pt;margin-right:0in;margin-bottom:
                          6.0pt;margin-left:0in;text-align:center;line-height:16.0pt'><span
                          style='font-size:13.0pt;color:windowtext'>&nbsp;</span></p>
                          </td>
                         </tr>
                         <tr>
                          <td width='7%' valign=top style='width:7.14%;border:solid windowtext 1.0pt;
                          border-top:none;padding:0in 5.4pt 0in 5.4pt'>
                          <p class=MsoNormal style='margin-top:6.0pt;margin-right:0in;margin-bottom:
                          6.0pt;margin-left:0in;text-align:center;line-height:16.0pt'><span
                          style='font-size:13.0pt;color:windowtext'>2</span></p>
                          </td>
                          <td width='52%' valign=top style='width:52.44%;border-top:none;border-left:
                          none;border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
                          padding:0in 5.4pt 0in 5.4pt'>
                          <p class=MsoNormal style='margin-top:6.0pt;margin-right:0in;margin-bottom:
                          6.0pt;margin-left:0in;text-align:justify;line-height:16.0pt'><span
                          style='font-size:13.0pt;color:windowtext'>Số
                          cơ quan, tổ chức, đơn vị chưa thực hiện hoặc chưa được tổng hợp kết quả trong
                          báo cáo này</span></p>
                          </td>
                          <td width='20%' valign=top style='width:20.96%;border-top:none;border-left:
                          none;border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
                          padding:0in 5.4pt 0in 5.4pt'>
                          <p class=MsoNormal align=center style='margin-top:6.0pt;margin-right:0in;
                          margin-bottom:6.0pt;margin-left:0in;text-align:center;line-height:16.0pt'><span
                          style='font-size:13.0pt;color:windowtext'>CQTCĐV</span></p>
                          </td>
                          <td width='19%' valign=top style='width:19.48%;border-top:none;border-left:
                          none;border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
                          padding:0in 5.4pt 0in 5.4pt'>
                          <p class=MsoNormal style='margin-top:6.0pt;margin-right:0in;margin-bottom:
                          6.0pt;margin-left:0in;text-align:center;line-height:16.0pt'><span
                          style='font-size:13.0pt;color:windowtext'>&nbsp;</span></p>
                          </td>
                         </tr>
                         <tr>
                          <td width='7%' valign=top style='width:7.14%;border:solid windowtext 1.0pt;
                          border-top:none;padding:0in 5.4pt 0in 5.4pt'>
                          <p class=MsoNormal style='margin-top:6.0pt;margin-right:0in;margin-bottom:
                          6.0pt;margin-left:0in;text-align:center;line-height:16.0pt'><span
                          style='font-size:13.0pt;color:windowtext'>&nbsp;</span></p>
                          </td>
                          <td width='52%' valign=top style='width:52.44%;border-top:none;border-left:
                          none;border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
                          padding:0in 5.4pt 0in 5.4pt'>
                          <p class=MsoNormal style='margin-top:6.0pt;margin-right:0in;margin-bottom:
                          6.0pt;margin-left:0in;text-align:justify;line-height:16.0pt'><span
                          style='font-size:13.0pt;color:windowtext'>Tỷ
                          lệ so với tổng số cơ quan, tổ chức, đơn vị</span></p>
                          </td>
                          <td width='20%' valign=top style='width:20.96%;border-top:none;border-left:
                          none;border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
                          padding:0in 5.4pt 0in 5.4pt'>
                          <p class=MsoNormal align=center style='margin-top:6.0pt;margin-right:0in;
                          margin-bottom:6.0pt;margin-left:0in;text-align:center;line-height:16.0pt'><span
                          style='font-size:13.0pt;color:windowtext'>%</span></p>
                          </td>
                          <td width='19%' valign=top style='width:19.48%;border-top:none;border-left:
                          none;border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
                          padding:0in 5.4pt 0in 5.4pt'>
                          <p class=MsoNormal style='margin-top:6.0pt;margin-right:0in;margin-bottom:
                          6.0pt;margin-left:0in;text-align:center;line-height:16.0pt'><span
                          style='font-size:13.0pt;color:windowtext'>&nbsp;</span></p>
                          </td>
                         </tr>
                         <tr>
                          <td width='7%' valign=top style='width:7.14%;border:solid windowtext 1.0pt;
                          border-top:none;padding:0in 5.4pt 0in 5.4pt'>
                          <p class=MsoNormal style='margin-top:6.0pt;margin-right:0in;margin-bottom:
                          6.0pt;margin-left:0in;text-align:center;line-height:16.0pt'><span
                          style='font-size:13.0pt;color:windowtext'>3</span></p>
                          </td>
                          <td width='52%' valign=top style='width:52.44%;border-top:none;border-left:
                          none;border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
                          padding:0in 5.4pt 0in 5.4pt'>
                          <p class=MsoNormal style='margin-top:6.0pt;margin-right:0in;margin-bottom:
                          6.0pt;margin-left:0in;text-align:justify;line-height:16.0pt'><span
                          style='font-size:13.0pt;color:windowtext'>Tổng
                          số bản kê khai</span></p>
                          </td>
                          <td width='20%' valign=top style='width:20.96%;border-top:none;border-left:
                          none;border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
                          padding:0in 5.4pt 0in 5.4pt'>
                          <p class=MsoNormal align=center style='margin-top:6.0pt;margin-right:0in;
                          margin-bottom:6.0pt;margin-left:0in;text-align:center;line-height:16.0pt'><span
                          style='font-size:13.0pt;color:windowtext'>Bản
                          kê khai</span></p>
                          </td>
                          <td width='19%' valign=top style='width:19.48%;border-top:none;border-left:
                          none;border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
                          padding:0in 5.4pt 0in 5.4pt'>
                          <p class=MsoNormal style='margin-top:6.0pt;margin-right:0in;margin-bottom:
                          6.0pt;margin-left:0in;text-align:center;line-height:16.0pt'><span
                          style='font-size:13.0pt;color:windowtext'>&nbsp;</span></p>
                          </td>
                         </tr>
                         <tr>
                          <td width='7%' valign=top style='width:7.14%;border:solid windowtext 1.0pt;
                          border-top:none;padding:0in 5.4pt 0in 5.4pt'>
                          <p class=MsoNormal style='margin-top:6.0pt;margin-right:0in;margin-bottom:
                          6.0pt;margin-left:0in;text-align:center;line-height:16.0pt'><span
                          style='font-size:13.0pt;color:windowtext'>4</span></p>
                          </td>
                          <td width='52%' valign=top style='width:52.44%;border-top:none;border-left:
                          none;border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
                          padding:0in 5.4pt 0in 5.4pt'>
                          <p class=MsoNormal style='margin-top:6.0pt;margin-right:0in;margin-bottom:
                          6.0pt;margin-left:0in;text-align:justify;line-height:16.0pt'><span
                          style='font-size:13.0pt;color:windowtext'>Số
                          bản kê khai đã được công khai</span></p>
                          </td>
                          <td width='20%' valign=top style='width:20.96%;border-top:none;border-left:
                          none;border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
                          padding:0in 5.4pt 0in 5.4pt'>
                          <p class=MsoNormal align=center style='margin-top:6.0pt;margin-right:0in;
                          margin-bottom:6.0pt;margin-left:0in;text-align:center;line-height:16.0pt'><span
                          style='font-size:13.0pt;color:windowtext'>Bản
                          kê khai</span></p>
                          </td>
                          <td width='19%' valign=top style='width:19.48%;border-top:none;border-left:
                          none;border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
                          padding:0in 5.4pt 0in 5.4pt'>
                          <p class=MsoNormal style='margin-top:6.0pt;margin-right:0in;margin-bottom:
                          6.0pt;margin-left:0in;text-align:center;line-height:16.0pt'><span
                          style='font-size:13.0pt;color:windowtext'>&nbsp;</span></p>
                          </td>
                         </tr>
                         <tr>
                          <td width='7%' valign=top style='width:7.14%;border:solid windowtext 1.0pt;
                          border-top:none;padding:0in 5.4pt 0in 5.4pt'>
                          <p class=MsoNormal style='margin-top:6.0pt;margin-right:0in;margin-bottom:
                          6.0pt;margin-left:0in;text-align:center;line-height:16.0pt'><span
                          style='font-size:13.0pt;color:windowtext'>&nbsp;</span></p>
                          </td>
                          <td width='52%' valign=top style='width:52.44%;border-top:none;border-left:
                          none;border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
                          padding:0in 5.4pt 0in 5.4pt'>
                          <p class=MsoNormal style='margin-top:6.0pt;margin-right:0in;margin-bottom:
                          6.0pt;margin-left:0in;text-align:justify;line-height:16.0pt'><span
                          style='font-size:13.0pt;color:windowtext'>Tỷ
                          lệ so với tổng số bản kê khai</span></p>
                          </td>
                          <td width='20%' valign=top style='width:20.96%;border-top:none;border-left:
                          none;border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
                          padding:0in 5.4pt 0in 5.4pt'>
                          <p class=MsoNormal align=center style='margin-top:6.0pt;margin-right:0in;
                          margin-bottom:6.0pt;margin-left:0in;text-align:center;line-height:16.0pt'><span
                          style='font-size:13.0pt;color:windowtext'>%</span></p>
                          </td>
                          <td width='19%' valign=top style='width:19.48%;border-top:none;border-left:
                          none;border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
                          padding:0in 5.4pt 0in 5.4pt'>
                          <p class=MsoNormal style='margin-top:6.0pt;margin-right:0in;margin-bottom:
                          6.0pt;margin-left:0in;text-align:center;line-height:16.0pt'><span
                          style='font-size:13.0pt;color:windowtext'>&nbsp;</span></p>
                          </td>
                         </tr>
                         <tr>
                          <td width='7%' valign=top style='width:7.14%;border:solid windowtext 1.0pt;
                          border-top:none;padding:0in 5.4pt 0in 5.4pt'>
                          <p class=MsoNormal style='margin-top:6.0pt;margin-right:0in;margin-bottom:
                          6.0pt;margin-left:0in;text-align:center;line-height:16.0pt'><span
                          style='font-size:13.0pt;color:windowtext'>5</span></p>
                          </td>
                          <td width='52%' valign=top style='width:52.44%;border-top:none;border-left:
                          none;border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
                          padding:0in 5.4pt 0in 5.4pt'>
                          <p class=MsoNormal style='margin-top:6.0pt;margin-right:0in;margin-bottom:
                          6.0pt;margin-left:0in;text-align:justify;line-height:16.0pt'><span
                          style='font-size:13.0pt;color:windowtext'>Số
                          bản kê khai đã được công khai theo hình thức niêm yết</span></p>
                          </td>
                          <td width='20%' valign=top style='width:20.96%;border-top:none;border-left:
                          none;border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
                          padding:0in 5.4pt 0in 5.4pt'>
                          <p class=MsoNormal align=center style='margin-top:6.0pt;margin-right:0in;
                          margin-bottom:6.0pt;margin-left:0in;text-align:center;line-height:16.0pt'><span
                          style='font-size:13.0pt;color:windowtext'>Bản
                          kê khai</span></p>
                          </td>
                          <td width='19%' valign=top style='width:19.48%;border-top:none;border-left:
                          none;border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
                          padding:0in 5.4pt 0in 5.4pt'>
                          <p class=MsoNormal style='margin-top:6.0pt;margin-right:0in;margin-bottom:
                          6.0pt;margin-left:0in;text-align:center;line-height:16.0pt'><span
                          style='font-size:13.0pt;color:windowtext'>&nbsp;</span></p>
                          </td>
                         </tr>
                         <tr>
                          <td width='7%' valign=top style='width:7.14%;border:solid windowtext 1.0pt;
                          border-top:none;padding:0in 5.4pt 0in 5.4pt'>
                          <p class=MsoNormal style='margin-top:6.0pt;margin-right:0in;margin-bottom:
                          6.0pt;margin-left:0in;text-align:center;line-height:16.0pt'><span
                          style='font-size:13.0pt;color:windowtext'>&nbsp;</span></p>
                          </td>
                          <td width='52%' valign=top style='width:52.44%;border-top:none;border-left:
                          none;border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
                          padding:0in 5.4pt 0in 5.4pt'>
                          <p class=MsoNormal style='margin-top:6.0pt;margin-right:0in;margin-bottom:
                          6.0pt;margin-left:0in;text-align:justify;line-height:16.0pt'><span
                          style='font-size:13.0pt;color:windowtext'>Tỷ
                          lệ so với số bản kê khai đã được công khai</span></p>
                          </td>
                          <td width='20%' valign=top style='width:20.96%;border-top:none;border-left:
                          none;border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
                          padding:0in 5.4pt 0in 5.4pt'>
                          <p class=MsoNormal align=center style='margin-top:6.0pt;margin-right:0in;
                          margin-bottom:6.0pt;margin-left:0in;text-align:center;line-height:16.0pt'><span
                          style='font-size:13.0pt;color:windowtext'>%</span></p>
                          </td>
                          <td width='19%' valign=top style='width:19.48%;border-top:none;border-left:
                          none;border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
                          padding:0in 5.4pt 0in 5.4pt'>
                          <p class=MsoNormal style='margin-top:6.0pt;margin-right:0in;margin-bottom:
                          6.0pt;margin-left:0in;text-align:center;line-height:16.0pt'><span
                          style='font-size:13.0pt;color:windowtext'>&nbsp;</span></p>
                          </td>
                         </tr>
                         <tr>
                          <td width='7%' valign=top style='width:7.14%;border:solid windowtext 1.0pt;
                          border-top:none;padding:0in 5.4pt 0in 5.4pt'>
                          <p class=MsoNormal style='margin-top:6.0pt;margin-right:0in;margin-bottom:
                          6.0pt;margin-left:0in;text-align:center;line-height:16.0pt'><span
                          style='font-size:13.0pt;color:windowtext'>6</span></p>
                          </td>
                          <td width='52%' valign=top style='width:52.44%;border-top:none;border-left:
                          none;border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
                          padding:0in 5.4pt 0in 5.4pt'>
                          <p class=MsoNormal style='margin-top:6.0pt;margin-right:0in;margin-bottom:
                          6.0pt;margin-left:0in;text-align:justify;line-height:16.0pt'><span
                          style='font-size:13.0pt;color:windowtext'>Số
                          bản kê khai đã công khai theo hình thức công bố tại cuộc họp</span></p>
                          </td>
                          <td width='20%' valign=top style='width:20.96%;border-top:none;border-left:
                          none;border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
                          padding:0in 5.4pt 0in 5.4pt'>
                          <p class=MsoNormal align=center style='margin-top:6.0pt;margin-right:0in;
                          margin-bottom:6.0pt;margin-left:0in;text-align:center;line-height:16.0pt'><span
                          style='font-size:13.0pt;color:windowtext'>Bản
                          kê khai</span></p>
                          </td>
                          <td width='19%' valign=top style='width:19.48%;border-top:none;border-left:
                          none;border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
                          padding:0in 5.4pt 0in 5.4pt'>
                          <p class=MsoNormal style='margin-top:6.0pt;margin-right:0in;margin-bottom:
                          6.0pt;margin-left:0in;text-align:center;line-height:16.0pt'><span
                          style='font-size:13.0pt;color:windowtext'>&nbsp;</span></p>
                          </td>
                         </tr>
                         <tr>
                          <td width='7%' valign=top style='width:7.14%;border:solid windowtext 1.0pt;
                          border-top:none;padding:0in 5.4pt 0in 5.4pt'>
                          <p class=MsoNormal style='margin-top:6.0pt;margin-right:0in;margin-bottom:
                          6.0pt;margin-left:0in;text-align:center;line-height:16.0pt'><span
                          style='font-size:13.0pt;color:windowtext'>&nbsp;</span></p>
                          </td>
                          <td width='52%' valign=top style='width:52.44%;border-top:none;border-left:
                          none;border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
                          padding:0in 5.4pt 0in 5.4pt'>
                          <p class=MsoNormal style='margin-top:6.0pt;margin-right:0in;margin-bottom:
                          6.0pt;margin-left:0in;text-align:justify;line-height:16.0pt'><span
                          style='font-size:13.0pt;color:windowtext'>Tỷ
                          lệ so với số bản kê khai đã được công khai</span></p>
                          </td>
                          <td width='20%' valign=top style='width:20.96%;border-top:none;border-left:
                          none;border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
                          padding:0in 5.4pt 0in 5.4pt'>
                          <p class=MsoNormal align=center style='margin-top:6.0pt;margin-right:0in;
                          margin-bottom:6.0pt;margin-left:0in;text-align:center;line-height:16.0pt'><span
                          style='font-size:13.0pt;color:windowtext'>%</span></p>
                          </td>
                          <td width='19%' valign=top style='width:19.48%;border-top:none;border-left:
                          none;border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
                          padding:0in 5.4pt 0in 5.4pt'>
                          <p class=MsoNormal style='margin-top:6.0pt;margin-right:0in;margin-bottom:
                          6.0pt;margin-left:0in;text-align:center;line-height:16.0pt'><span
                          style='font-size:13.0pt;color:windowtext'>&nbsp;</span></p>
                          </td>
                         </tr>
                        </table>

                        <p class=MsoNormal style='margin-top:6.0pt;margin-right:0in;margin-bottom:6.0pt;
                        margin-left:0in;text-align:justify;text-indent:28.35pt;line-height:16.0pt'><span
                        style='font-size:13.0pt;color:windowtext'>-
                        Kết quả khác (nếu có)</span></p>

                        <p class=MsoNormal style='margin-top:6.0pt;margin-right:0in;margin-bottom:6.0pt;
                        margin-left:0in;text-align:justify;text-indent:28.35pt;line-height:16.0pt'><b><span
                        style='font-size:13.0pt;color:windowtext'>3.
                        Đánh giá chung và kiến nghị.</span></b></p>

                        <p class=MsoNormal style='margin-top:6.0pt;margin-right:0in;margin-bottom:6.0pt;
                        margin-left:0in;text-align:justify;text-indent:28.35pt;line-height:16.0pt'><span
                        style='font-size:13.0pt;color:windowtext'>-
                        Các mặt thuận lợi, khó khăn, vướng mắc khi triển khai công tác kê khai tài sản,
                        thu nhập trong cơ quan, tổ chức, đơn vị mình; giải pháp để chủ động hoặc đề
                        xuất giải pháp khắc phục khó khăn, vướng mắc.</span></p>

                        <p class=MsoNormal style='margin-top:6.0pt;margin-right:0in;margin-bottom:6.0pt;
                        margin-left:0in;text-align:justify;text-indent:28.35pt;line-height:16.0pt'><span
                        style='font-size:13.0pt;color:windowtext'>-
                        Những nội dung quy định cần hướng dẫn cụ thể hơn.</span></p>

                        <p class=MsoNormal style='margin-top:6.0pt;margin-right:0in;margin-bottom:6.0pt;
                        margin-left:0in;text-align:justify;text-indent:28.35pt;line-height:16.0pt'><span
                        style='font-size:13.0pt;color:windowtext'>-
                        Các ý kiến khác./.</span></p>

                        <table class=MsoNormalTable border=1 cellspacing=0 cellpadding=0 width='100%'
                         style='width:100.0%;border-collapse:collapse;border:none'>
                         <tr>
                          <td width='46%' valign=top style='width:46.96%;border:none;padding:0in 5.4pt 0in 5.4pt'>
                          <p class=MsoNormal><b><i><span lang=VI style='font-size:13.0pt;color:windowtext'>Nơi nhận:</span></i></b></p>
                          <p class=MsoNormal><span style='font-size:13.0pt;
                          color:windowtext'>&nbsp;</span></p>
                          </td>
                          <td width='53%' valign=top style='width:53.04%;border:none;padding:0in 5.4pt 0in 5.4pt'>
                          <p class=MsoNormal align=center style='text-align:center'><b><span
                          style='font-size:13.0pt;color:windowtext'>Thủ
                          trưởng cơ quan, tổ chức, đơn vị</span></b></p>
                          <p class=MsoNormal align=center style='text-align:center'><b><span
                          style='font-size:13.0pt;color:windowtext'>Ký
                          tên, đóng dấu</span></b></p>
                          </td>
                         </tr>
                        </table>

                        <p class=MsoNormal style='margin-bottom:6.0pt'><span style='
                        color:windowtext'>&nbsp;</span></p>

                        <p class=Vnbnnidung60 style='margin-left:2.0pt;line-height:normal;background:
                        transparent'><span style='font-size:13.0pt'>&nbsp;</span></p>

                        <p class=Vnbnnidung60 style='margin-left:2.0pt;line-height:normal;background:
                        transparent'><span style='font-size:13.0pt'>&nbsp;</span></p>

                        <p class=Vnbnnidung60 style='margin-left:2.0pt;line-height:normal;background:
                        transparent'><span style='font-size:13.0pt'>&nbsp;</span></p>

                        <p class=Vnbnnidung60 style='margin-left:2.0pt;line-height:normal;background:
                        transparent'><span style='font-size:13.0pt'>&nbsp;</span></p>

                        <p class=Vnbnnidung60 style='margin-left:2.0pt;line-height:normal;background:
                        transparent'><span style='font-size:13.0pt'>&nbsp;</span></p>

                        <p class=Vnbnnidung60 style='margin-left:2.0pt;line-height:normal;background:
                        transparent'><span style='font-size:13.0pt'>&nbsp;</span></p>

                        <p class=Vnbnnidung60 style='margin-left:2.0pt;line-height:normal;background:
                        transparent'><span style='font-size:13.0pt'>&nbsp;</span></p>

                        <p class=MsoNormal><span lang=VI>&nbsp;</span></p>

                        </div>

                        </body>

                        </html>";
            }
            else if (LoaiKeHoachKeKhai == 4)
            {
                var MaCoQuan = user.GetUserCoQuan();
                var kehoachkekhai = db.NV_LapKeHoachKeKhai.Where(_ => _.Ma_Loai_KeKhai == 3 && _.KeHoachNam == NamBaoCao).Select(_ => _.MaKeHoachKeKhai).ToList();
                //Tổng cơ quan
                var kehoachkekhaiBoSung_TongCoQuan = db.NV_LapKeHoachKeKhai_DanhSachKeKhaiBoSung.Where(_ => kehoachkekhai.Contains((int)_.MaKeHoachKeKhai) && _.Ma_CoQuan_DonVi == MaCoQuan).ToList();
                var kehoachkekhaiHangNam_TongCoQuan = db.NV_LapKeHoachKeKhai_DanhSachKeKhaiHangNam.Where(_ => kehoachkekhai.Contains((int)_.MaKeHoachKeKhai) && _.Ma_CoQuan_DonVi == MaCoQuan).ToList();
                //Cơ quan chưa lập kế hoạch
                var kehoachkekhaiBoSung_CoQuanChuaToChucKK = db.NV_LapKeHoachKeKhai_DanhSachKeKhaiBoSung.Where(_ => kehoachkekhai.Contains((int)_.MaKeHoachKeKhai) && _.TrangThai != true && _.Ma_CoQuan_DonVi == MaCoQuan).ToList();
                var kehoachkekhaiHangNam_CoQuanChuaToChucKK = db.NV_LapKeHoachKeKhai_DanhSachKeKhaiHangNam.Where(_ => kehoachkekhai.Contains((int)_.MaKeHoachKeKhai) && _.TrangThai != true && _.Ma_CoQuan_DonVi == MaCoQuan).ToList();
                //Cơ Quan đã lập kế hoạch
                var kehoachkekhaiBoSung = db.NV_LapKeHoachKeKhai_DanhSachKeKhaiBoSung.Where(_ => kehoachkekhai.Contains((int)_.MaKeHoachKeKhai) && _.TrangThai == true && _.Ma_CoQuan_DonVi == MaCoQuan).Select(_ => _.MaKeHoachKeKhai_DanhSachKeKhaiBoSung_ID);
                var kehoachkekhaiHangNam = db.NV_LapKeHoachKeKhai_DanhSachKeKhaiHangNam.Where(_ => kehoachkekhai.Contains((int)_.MaKeHoachKeKhai) && _.TrangThai == true && _.Ma_CoQuan_DonVi == MaCoQuan).Select(_ => _.MaKeHoachKeKhai_DanhSachKeKhaiHangNam_ID);
                //Tổng cán bộ có trong danh sách
                var kehoachkekhaiBoSung_ChiTiet = (from khkkbs_ct in db.NV_LapKeHoachKeKhai_DanhSachKeKhaiBoSung_ChiTiet
                                                   join cb in db.DM_CanBo on khkkbs_ct.Ma_CanBo equals cb.Ma_CanBo
                                                   where kehoachkekhaiBoSung.Contains((int)khkkbs_ct.MaKeHoachKeKhai_DanhSachKeKhaiBoSung_ID) && cb.Ma_CoQuan_DonVi == MaCoQuan
                                                   select new { cb }
                                                   ).ToList();
                var kehoachkekhaiHangNam_ChiTiet = (from khkkhn_ct in db.NV_LapKeHoachKeKhai_DanhSachKeKhaiHangNam_ChiTiet
                                                   join cb in db.DM_CanBo on khkkhn_ct.Ma_CanBo equals cb.Ma_CanBo
                                                   where kehoachkekhaiBoSung.Contains((int)khkkhn_ct.MaKeHoachKeKhai_DanhSachKeKhaiHangNam_ID) && cb.Ma_CoQuan_DonVi == MaCoQuan
                                                   select new { cb }
                                                   ).ToList();
                // danh sách cán bộ
                var dataLapDanhSachCanBoHangNam = (from lkhkk in db.NV_LapKeHoachKeKhai
                                            join lkhkkhn in db.NV_LapKeHoachKeKhai_DanhSachKeKhaiHangNam on lkhkk.MaKeHoachKeKhai equals lkhkkhn.MaKeHoachKeKhai
                                            join lkhkkhn_ct in db.NV_LapKeHoachKeKhai_DanhSachKeKhaiHangNam_ChiTiet on lkhkkhn.MaKeHoachKeKhai_DanhSachKeKhaiHangNam_ID equals lkhkkhn_ct.MaKeHoachKeKhai_DanhSachKeKhaiHangNam_ID
                                            join cb in db.DM_CanBo on lkhkkhn_ct.Ma_CanBo equals cb.Ma_CanBo
                                            where lkhkk.KeHoachNam == NamBaoCao
                                            select new { cb, lkhkkhn_ct }).ToList();

                var dataLapDanhSachCanBoBoSung = (from lkhkk in db.NV_LapKeHoachKeKhai
                                        join lkhkkbs in db.NV_LapKeHoachKeKhai_DanhSachKeKhaiBoSung on lkhkk.MaKeHoachKeKhai equals lkhkkbs.MaKeHoachKeKhai
                                        join lkhkkbs_ct in db.NV_LapKeHoachKeKhai_DanhSachKeKhaiBoSung_ChiTiet on lkhkkbs.MaKeHoachKeKhai_DanhSachKeKhaiBoSung_ID equals lkhkkbs_ct.MaKeHoachKeKhai_DanhSachKeKhaiBoSung_ID
                                        join cb in db.DM_CanBo on lkhkkbs_ct.Ma_CanBo equals cb.Ma_CanBo
                                        where lkhkk.KeHoachNam == NamBaoCao
                                        select new { cb, lkhkkbs_ct }).ToList();
                //Danh Sách cán bộ đã kê khai tài sản thu nhập
                var dataCanBoDaKeKhaihn = (from bkk in db.Nv_KeKhai_TSTN
                                         join lkk in db.DM_Loai_KeKhai on bkk.Ma_Loai_KeKhai equals lkk.Ma_Loai_KeKhai
                                         join cb in db.DM_CanBo on bkk.Ma_CanBo equals cb.Ma_CanBo
                                         join cq in db.DM_CoQuanDonVi on cb.Ma_CoQuan_DonVi equals cq.Ma_CoQuan_DonVi
                                         join cv in db.DM_ChucVu_ChucDanh on cb.Ma_ChucVu_ChucDanh equals cv.Ma_ChucVu_ChucDanh
                                         where kehoachkekhai.Contains((int)bkk.MaKeHoachKeKhai) && cb.Ma_CoQuan_DonVi == MaCoQuan && bkk.Ma_Loai_KeKhai == 4
                                         select new { bkk }).ToList();
   
                var dataCanBoDaKeKhaibs = (from bkk in db.Nv_KeKhai_TSTN
                                         join lkk in db.DM_Loai_KeKhai on bkk.Ma_Loai_KeKhai equals lkk.Ma_Loai_KeKhai
                                         join cb in db.DM_CanBo on bkk.Ma_CanBo equals cb.Ma_CanBo
                                         join cq in db.DM_CoQuanDonVi on cb.Ma_CoQuan_DonVi equals cq.Ma_CoQuan_DonVi
                                         join cv in db.DM_ChucVu_ChucDanh on cb.Ma_ChucVu_ChucDanh equals cv.Ma_ChucVu_ChucDanh
                                         where kehoachkekhai.Contains((int)bkk.MaKeHoachKeKhai) && cb.Ma_CoQuan_DonVi == MaCoQuan && bkk.Ma_Loai_KeKhai == 5
                                         select new { bkk }).ToList();


                TongCoQuan = kehoachkekhaiBoSung_TongCoQuan.Count() + kehoachkekhaiHangNam_TongCoQuan.Count();
                CoQuanDaToChuckk = kehoachkekhaiBoSung.Count() + kehoachkekhaiHangNam.Count() ;
                CoQuanChuaToChuckk = kehoachkekhaiBoSung_CoQuanChuaToChucKK.Count() + kehoachkekhaiHangNam_CoQuanChuaToChucKK.Count();
                 
                var SoNguoiPhaiKeKhaiTSHN =  kehoachkekhaiHangNam_ChiTiet.Count();
                var SoNguoiPhaiKeKhaiTSBS = kehoachkekhaiBoSung_ChiTiet.Count();
                var SoNguoiDaKeKhaiTSHN = dataCanBoDaKeKhaihn.Count();
                var SoNguoiDaKeKhaiTSBS = dataCanBoDaKeKhaibs.Count();
                if(TongCoQuan == 0)
                {
                    TyLeCoQuanDaToChuckk = 0.0;
                    TyLeCoQuanChuaToChuckk = 0.0;
                }
                else
                {
                    TyLeCoQuanDaToChuckk = (float)((float)CoQuanDaToChuckk / (float)TongCoQuan) * 100;
                    TyLeCoQuanChuaToChuckk = (float)((float)CoQuanChuaToChuckk / (float)TongCoQuan) * 100;
                }
                
                //TyLeCoQuanChuckk = (float)(CoQuanChuaToChuckk / TongCoQuan) * 100;

                html = $@"<html>
                        <head>
                            <meta http-equiv=Content-Type content='text/html; charset=utf-8'>
                            <meta name=Generator content='Microsoft Word 15 (filtered)'>
                            <style>
                                  p .MsoNormal, li.MsoNormal, div.MsoNormal
	                            {{margin:0cm;
	                            margin-bottom:.0001pt;
	                            font-size:12.0pt;
	                            font-family:'Courier New';

                                color: black;
                                 }}
                                @font-face {{
                                    font-family: 'Cambria Math';
                                    panose-1: 2 4 5 3 5 4 6 3 2 4;
                                }}
                                html{{  margin: 0cm 0cm 0cm 0.5cm;}}
                                    th,td,p,div,b {{margin:0;padding:0}}
                                    /* Style Definitions */
                                    p.MsoNormal,
                                    li.MsoNormal,
                                div.MsoNormal {{
                                    margin: 0in;
                                    font-size: 13px;
                                    font-family: 'Times New Roman', serif;
                                }}
                                    span.Vnbnnidung6
	                            {{mso-style-name:'V\0103n b\1EA3n n\1ED9i dung \(6\)_';
	                            mso-style-link:'V\0103n b\1EA3n n\1ED9i dung \(6\)';
	                            font-family:'Times New Roman',serif;
	                            background:white;
	                            font-weight:bold;}}
                                p.Vnbnnidung60, li.Vnbnnidung60, div.Vnbnnidung60
	                            {{mso-style-name:'V\0103n b\1EA3n n\1ED9i dung \(6\)';
	                            mso-style-link:'V\0103n b\1EA3n n\1ED9i dung \(6\)_';
	                            margin:0cm;
	                            margin-bottom:.0001pt;
	                            line-height:12.5pt;
	                            background:white;
	                            font-size:9.5pt;
	                            font-family:'Times New Roman',serif;
	                            font-weight:bold;}}

                                span.Vnbnnidung213pt
                                {{
                                mso - style - name:'V\0103n b\1EA3n n\1ED9i dung \(2\) + 13 pt\,Không in \0111\1EADm';
                                font - family:'Times New Roman',serif;
                                font - variant:normal!important;
                                color: black;
                                position: relative;
                                top: 0pt;
                                    letter - spacing:0pt;
                                    font - weight:normal;
                                    font - style:normal;
                                    text - decoration:none none;
                                }}
                                @page WordSection1
                                {{
                                    size:21.0cm 842.0pt;
                                    margin:51.05pt 48.2pt 36.85pt 70.9pt;
                                }}
                                div.WordSection1

                                {{ page: WordSection1; }}
                            </style>

                        </head>
                        <div class='WordSection1' style='padding: 2rem !important'>
                          <p class=Vnbnnidung60 style='line-height:normal;background:transparent'>
                              <span lang=X-NONE style='font-size:13.0pt'>&nbsp;</span>
                          </p>
                          <table class=MsoNormalTable border=1 cellspacing=0 cellpadding=0 width='100%'
                                 style='width:100.0%;border-collapse:collapse;border:none'>
                              <tr>
                                  <td width='37%' valign=top style='width:37.8%;border:none;padding:0cm 5.4pt 0cm 5.4pt'>
                                      <p class=MsoNormal align=center style='text-align:center'>

                                          <span style='font-size:13.0pt;color:windowtext'>
                                              <span id='LoaiCoQuan' style='  text-transform: uppercase'>UBND Huyện Cam Ranh</span>
                                              <br>
                                              <span id='CoQuanDonVi' style=' text-transform: uppercase'><b>Huyện Cam Ranh</b></span>
                                              <br />
                                              --------
                                          </span>

                                      </p>
                                  </td>
                                  <td width='62%' valign=top style='width:62.2%;border:none;padding:0cm 5.4pt 0cm 5.4pt'>
                                      <p class=MsoNormal align=center style='text-align:center'>
                                          <b>
                                              <span style='font-size:13.0pt;color:windowtext'>
                                                  CỘNG
                                                  HÒA XÃ HỘI CHỦ NGHĨA VIỆT NAM<br>
                                                  Độc lập - Tự do - Hạnh phúc <br>
                                                  ---------------
                                              </span>
                                          </b>
                                      </p>
                                  </td>
                              </tr>
                              <tr>
                                  <td width='37%' valign=top style='width:37.8%;border:none;padding:0cm 5.4pt 0cm 5.4pt'>
                                      <p class=MsoNormal align=center style='text-align:center'>
                                          <span style='font-size:13.0pt;color:windowtext'>
                                              Số: <span id='baocaoso'>(1)......</span>
                                              / <span id='baocaoso_qd'>(2).........</span>
                                          </span>
                                      </p>
                                  </td>
                                  <td width='62%' valign=top style='width:62.2%;border:none;padding:0cm 5.4pt 0cm 5.4pt'>
                                      <p class=MsoNormal align=center style='text-align:center'>
                                          <i>
                                              <span style='font-size:13.0pt;color:windowtext'>
                                                  <span id='baocaotinh'>Khánh Hòa</span>,
                                                  <span id='baocaongay'>
                                                      ngày ...... tháng ......
                                                      năm ......
                                                  </span>
                                              </span>
                                          </i>
                                      </p>
                                  </td>
                              </tr>
                          </table>

                          <p class=MsoNormal style='margin-bottom:6.0pt'>
                              <b>
                                  <span style='color:windowtext'>&nbsp;</span>
                              </b>
                          </p>

                          <p class=MsoNormal align=center style='margin-bottom:6.0pt;text-align:center'>
                              <b>
                                  <span style='font-size:13.0pt;color:windowtext'>
                                      BÁO CÁO
                                  </span>
                              </b>
                          </p>

                          <p class=MsoNormal align=center style='margin-bottom:6.0pt;text-align:center'>
                              <b>
                                  <span style='font-size:13.0pt;color:windowtext'>
                                      Kết
                                      quả triển khai công tác kiểm soát tài sản, thu
                                      nhập năm 2021
                                  </span>
                              </b>
                          </p>

                          <div align=center>

                              <table class=MsoTableGrid border=1 cellspacing=0 cellpadding=0
                                     style='border-collapse:collapse;border:none'>
                                  <tr>
                                      <td width=284 valign=top style='width:5.0cm;border:none;border-top:solid windowtext 1.0pt;  padding:0cm 5.4pt 0cm 5.4pt'>
                                          <p class=MsoNormal align=center style='margin-bottom:6.0pt;text-align:center'>
                                              <span style='font-size:13.0pt;color:windowtext'>&nbsp;</span>
                                          </p>
                                      </td>
                                  </tr>
                              </table>

                          </div>

                          <p class=MsoNormal style='margin-top:6.0pt;margin-right:0cm;margin-bottom:6.0pt; margin-left:0cm;text-align:justify;text-indent:1.0cm;line-height:16.0pt'>
                              <b>
                                  <span style='font-size:13.0pt;color:windowtext'>
                                      1.
                                      Việc chỉ đạo, triển khai, tổ chức
                                      thực hiện quy định về kiểm soát tài
                                      sản, thu nhập
                                  </span>
                              </b>
                          </p>

                          <p class=MsoNormal style='margin-top:6.0pt;margin-right:0cm;margin-bottom:6.0pt; margin-left:0cm;text-align:justify;text-indent:1.0cm;line-height:16.0pt'>
                              <span style='font-size:13.0pt;color:windowtext'>
                                  -
                                  Phạm vi, đặc điểm tổ chức, hoạt
                                  động của cơ quan, tổ chức, đơn
                                  vị.
                              </span>
                          </p>

                          <p class=MsoNormal style='margin-top:6.0pt;margin-right:0cm;margin-bottom:6.0pt; margin-left:0cm;text-align:justify;text-indent:1.0cm;line-height:16.0pt'>
                              <span style='font-size:13.0pt;color:windowtext'>
                                  -
                                  Các văn bản pháp luật áp dụng.
                              </span>
                          </p>

                          <p class=MsoNormal style='margin-top:6.0pt;margin-right:0cm;margin-bottom:6.0pt; margin-left:0cm;text-align:justify;text-indent:1.0cm;line-height:16.0pt'>
                              <span style='font-size:13.0pt;color:windowtext'>
                                  -
                                  Các văn bản chỉ đạo, đôn đốc
                                  của cấp trên.
                              </span>
                          </p>

                          <p class=MsoNormal style='margin-top:6.0pt;margin-right:0cm;margin-bottom:6.0pt; margin-left:0cm;text-align:justify;text-indent:1.0cm;line-height:16.0pt'>
                              <span style='font-size:13.0pt;color:windowtext'>
                                  -
                                  Các văn bản cơ quan, đơn vị đã triển
                                  khai như: Kế hoạch, phương án, tổ chức
                                  tuyên truyền…
                              </span>
                          </p>

                          <p class=MsoNormal style='margin-top:6.0pt;margin-right:0cm;margin-bottom:6.0pt; margin-left:0cm;text-align:justify;text-indent:1.0cm;line-height:16.0pt'>
                              <b>
                                  <span style='font-size:13.0pt;color:windowtext'>
                                      2.
                                      Kết quả thực hiện
                                  </span>
                              </b>
                          </p>

                          <p class=MsoNormal style='margin-top:6.0pt;margin-right:0cm;margin-bottom:6.0pt; margin-left:0cm;text-align:justify;text-indent:1.0cm;line-height:16.0pt'>
                              <b>
                                  <span style='font-size:13.0pt;color:windowtext'>
                                      2.1.
                                      Kết quả kê khai, công khai Bản kê khai tài sản, thu
                                      nhập hằng năm và bổ sung năm 2021
                                  </span>
                              </b><span style='font-size:13.0pt;color:windowtext'> (TSTN)</span>
                          </p>

                          <table class=MsoNormalTable border=0 cellspacing=0 cellpadding=0 width='100%'
                                 style='width:100.0%;border-collapse:collapse'>
                              <tr style='height:28.5pt'>
                                  <td width='9%' style='width:9.5%;border:solid windowtext 1.0pt;padding:0cm 5.4pt 0cm 5.4pt; height:28.5pt'>
                                      <p class=MsoNormal align=center style='text-align:center'>
                                          <b>
                                              <span style='font-size:11.0pt;color:windowtext'>STT</span>
                                          </b>
                                      </p>
                                  </td>
                                  <td width='55%' style='width:55.44%;border:solid windowtext 1.0pt;border-left: none;padding:0cm 5.4pt 0cm 5.4pt;height:28.5pt'>
                                      <p class=MsoNormal align=center style='text-align:center'>
                                          <b>
                                              <span style='font-size:11.0pt;color:windowtext'>
                                                  NỘI
                                                  DUNG
                                              </span>
                                          </b>
                                      </p>
                                  </td>
                                  <td width='19%' style='width:19.8%;border:solid windowtext 1.0pt;border-left: none;padding:0cm 5.4pt 0cm 5.4pt;height:28.5pt'>
                                      <p class=MsoNormal align=center style='text-align:center'>
                                          <b>
                                              <span style='font-size:11.0pt;color:windowtext'>
                                                  ĐƠN
                                                  VỊ TÍNH
                                              </span>
                                          </b>
                                      </p>
                                  </td>
                                  <td width='15%' style='width:15.24%;border:solid windowtext 1.0pt;border-left: none;padding:0cm 5.4pt 0cm 5.4pt;height:28.5pt'>
                                      <p class=MsoNormal align=center style='text-align:center'>
                                          <b>
                                              <span style='font-size:11.0pt;color:windowtext'>
                                                  SỐ
                                                  LIỆU
                                              </span>
                                          </b>
                                      </p>
                                  </td>
                              </tr>
                              <tr style='height:15.0pt'>
                                  <td width='9%' style='width:9.5%;border:solid windowtext 1.0pt;border-top: none;padding:0cm 5.4pt 0cm 5.4pt;height:15.0pt'>
                                      <p class=MsoNormal align=center style='text-align:center'>
                                          <b>
                                              <span style='font-size:11.0pt;color:windowtext'>I</span>
                                          </b>
                                      </p>
                                  </td>
                                  <td width='55%' style='width:55.44%;border-top:none;border-left:none; border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
                              padding:0cm 5.4pt 0cm 5.4pt;height:15.0pt'>
                                      <p class=MsoNormal style='text-align:justify'>
                                          <b>
                                              <span style='font-size:11.0pt;  color:windowtext'>
                                                  Kê khai tài sản,
                                                  thu nhập
                                              </span>
                                          </b>
                                      </p>
                                  </td>
                                  <td width='19%' style='width:19.8%;border-top:none;border-left:none;
                        border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
                        padding:0cm 5.4pt 0cm 5.4pt;height:15.0pt'>
                                      <p class=MsoNormal style='text-align:justify'>
                                          <span style='font-size:11.0pt;
                        color:windowtext'>&nbsp;</span>
                                      </p>
                                  </td>
                                  <td width='15%' style='width:15.24%;border-top:none;border-left:none;
                        border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
                        padding:0cm 5.4pt 0cm 5.4pt;height:15.0pt'>
                                      <p class=MsoNormal style='text-align:justify'>
                                          <span style='font-size:11.0pt;
                        color:windowtext'>&nbsp;</span>
                                      </p>
                                  </td>
                              </tr>
                              <tr style='height:60.0pt'>
                                  <td width='9%' style='width:9.5%;border:solid windowtext 1.0pt;border-top:
                        none;padding:0cm 5.4pt 0cm 5.4pt;height:60.0pt'>
                                      <p class=MsoNormal align=center style='text-align:center'>
                                          <span style='font-size:11.0pt;color:windowtext'>1</span>
                                      </p>
                                  </td>
                                  <td width='55%' style='width:55.44%;border-top:none;border-left:none;
                        border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
                        padding:0cm 5.4pt 0cm 5.4pt;height:60.0pt'>
                                      <p class=MsoNormal style='text-align:justify'>
                                          <span style='font-size:11.0pt;
                        color:windowtext'>
                                              Tổng số
                                              cơ quan, tổ chức, đơn vị phải tổ
                                              chức thực hiện việc kê khai TSTN (nếu chỉ
                                              có 01 đơn vị thì ghi 01, trường hợp có các đơn
                                              vị trực thuộc thì ghi tổng số đơn
                                              vị trực thuộc đồng thời liệt kê
                                              cụ thể tên các đơn vị trực thuộc)
                                          </span>
                                      </p>
                                  </td>
                                  <td width='19%' style='width:19.8%;border-top:none;border-left:none;
                        border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
                        padding:0cm 5.4pt 0cm 5.4pt;height:60.0pt'>
                                      <p class=MsoNormal align=center style='text-align:center'>
                                          <span style='font-size:11.0pt;color:windowtext'>
                                              CQ,
                                              TC, ĐV
                                          </span>
                                      </p>
                                  </td>
                                  <td width='15%' style='width:15.24%;border-top:none;border-left:none;
                        border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
                        padding:0cm 5.4pt 0cm 5.4pt;height:60.0pt'>
                                      <center><p class=MsoNormal style='text-align:center'>
                                          <span style='font-size:11.0pt;
                        color:windowtext'>{TongCoQuan}</span>
                                      </p></center>
                                  </td>
                              </tr>
                              <tr style='height:30.0pt'>
                                  <td width='9%' style='width:9.5%;border:solid windowtext 1.0pt;border-top:
                        none;padding:0cm 5.4pt 0cm 5.4pt;height:30.0pt'>
                                      <p class=MsoNormal align=center style='text-align:center'>
                                          <span style='font-size:11.0pt;color:windowtext'>2</span>
                                      </p>
                                  </td>
                                  <td width='55%' style='width:55.44%;border-top:none;border-left:none;
                        border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
                        padding:0cm 5.4pt 0cm 5.4pt;height:30.0pt'>
                                      <p class=MsoNormal style='text-align:center'>
                                          <span style='font-size:11.0pt;
                        color:windowtext'>
                                              Số cơ quan,
                                              tổ chức, đơn vị đã tổ chức
                                              thực hiện việc kê khai tài sản, thu nhập (ghi
                                              tương tự mục 1)
                                          </span>
                                      </p>
                                  </td>
                                  <td width='19%' style='width:19.8%;border-top:none;border-left:none;
                        border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
                        padding:0cm 5.4pt 0cm 5.4pt;height:30.0pt'>
                                      <p class=MsoNormal align=center style='text-align:center'>
                                          <span style='font-size:11.0pt;color:windowtext'>
                                              CQ,
                                              TC, ĐV
                                          </span>
                                      </p>
                                  </td>
                                  <td width='15%' style='width:15.24%;border-top:none;border-left:none;
                        border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
                        padding:0cm 5.4pt 0cm 5.4pt;height:30.0pt'>
                                      <center><p class=MsoNormal style='text-align:center'>
                                          <span style='font-size:11.0pt;
                        color:windowtext'>{CoQuanDaToChuckk}</span>
                                      </p></center>
                                  </td>
                              </tr>
                              <tr style='height:15.0pt'>
                                  <td width='9%' style='width:9.5%;border:solid windowtext 1.0pt;border-top:
                        none;padding:0cm 5.4pt 0cm 5.4pt;height:15.0pt'>
                                      <p class=MsoNormal align=center style='text-align:center'>
                                          <span style='font-size:11.0pt;color:windowtext'>&nbsp;</span>
                                      </p>
                                  </td>
                                  <td width='55%' style='width:55.44%;border-top:none;border-left:none;
                        border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
                        padding:0cm 5.4pt 0cm 5.4pt;height:15.0pt'>
                                      <p class=MsoNormal style='text-align:center'>
                                          <span style='font-size:11.0pt;
                        color:windowtext'>
                                              Tỉ lệ so
                                              với tổng số cơ quan, tổ chức,
                                              đơn vị
                                          </span>
                                      </p>
                                  </td>
                                  <td width='19%' style='width:19.8%;border-top:none;border-left:none;
                        border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
                        padding:0cm 5.4pt 0cm 5.4pt;height:15.0pt'>
                                      <p class=MsoNormal align=center style='text-align:center'>
                                          <span style='font-size:11.0pt;color:windowtext'>%</span>
                                      </p>
                                  </td>
                                  <td width='15%' style='width:15.24%;border-top:none;border-left:none;
                        border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
                        padding:0cm 5.4pt 0cm 5.4pt;height:15.0pt'>
                                      <center><p class=MsoNormal style='text-align:center'>
                                          <span style='font-size:11.0pt;
                        color:windowtext'>{TyLeCoQuanDaToChuckk}</span>
                                      </p></center>
                                  </td>
                              </tr>
                              <tr style='height:30.0pt'>
                                  <td width='9%' style='width:9.5%;border:solid windowtext 1.0pt;border-top:
                        none;padding:0cm 5.4pt 0cm 5.4pt;height:30.0pt'>
                                      <p class=MsoNormal align=center style='text-align:center'>
                                          <span style='font-size:11.0pt;color:windowtext'>3</span>
                                      </p>
                                  </td>
                                  <td width='55%' style='width:55.44%;border-top:none;border-left:none;
                        border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
                        padding:0cm 5.4pt 0cm 5.4pt;height:30.0pt'>
                                      <p class=MsoNormal style='text-align:justify'>
                                          <span style='font-size:11.0pt;
                        color:windowtext'>
                                              Số cơ quan,
                                              tổ chức, đơn vị chưa thực hiện
                                              hoặc chưa được tổng hợp kết
                                              quả trong báo cáo này (ghi tương tự mục 1, 2)
                                          </span>
                                      </p>
                                  </td>
                                  <td width='19%' style='width:19.8%;border-top:none;border-left:none;
                        border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
                        padding:0cm 5.4pt 0cm 5.4pt;height:30.0pt'>
                                      <p class=MsoNormal align=center style='text-align:center'>
                                          <span style='font-size:11.0pt;color:windowtext'>
                                              CQ,
                                              TC, ĐV
                                          </span>
                                      </p>
                                  </td>
                                  <td width='15%' style='width:15.24%;border-top:none;border-left:none;
                        border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
                        padding:0cm 5.4pt 0cm 5.4pt;height:30.0pt'>
                                      <center><p class=MsoNormal style='text-align:center'>
                                          <span style='font-size:11.0pt;
                        color:windowtext'>{CoQuanChuaToChuckk}</span>
                                      </p></center>
                                  </td>
                              </tr>
                              <tr style='height:15.0pt'>
                                  <td width='9%' style='width:9.5%;border:solid windowtext 1.0pt;border-top:
                        none;padding:0cm 5.4pt 0cm 5.4pt;height:15.0pt'>
                                      <p class=MsoNormal align=center style='text-align:center'>
                                          <span style='font-size:11.0pt;color:windowtext'>&nbsp;</span>
                                      </p>
                                  </td>
                                  <td width='55%' style='width:55.44%;border-top:none;border-left:none;
                        border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
                        padding:0cm 5.4pt 0cm 5.4pt;height:15.0pt'>
                                      <p class=MsoNormal style='text-align:justify'>
                                          <span style='font-size:11.0pt;
                        color:windowtext'>
                                              Tỉ lệ so
                                              với tổng số cơ quan, tổ chức,
                                              đơn vị
                                          </span>
                                      </p>
                                  </td>
                                  <td width='19%' style='width:19.8%;border-top:none;border-left:none;
                        border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
                        padding:0cm 5.4pt 0cm 5.4pt;height:15.0pt'>
                                      <p class=MsoNormal align=center style='text-align:center'>
                                          <span style='font-size:11.0pt;color:windowtext'>%</span>
                                      </p>
                                  </td>
                                  <td width='15%' style='width:15.24%;border-top:none;border-left:none;
                        border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
                        padding:0cm 5.4pt 0cm 5.4pt;height:15.0pt'>
                                      <center><p class=MsoNormal style='text-align:center'>
                                          <span style='font-size:11.0pt;
                        color:windowtext'>{TyLeCoQuanChuaToChuckk}</span>
                                      </p></center>
                                  </td>
                              </tr>
                              <tr style='height:15.0pt'>
                                  <td width='9%' style='width:9.5%;border:solid windowtext 1.0pt;border-top:
                        none;padding:0cm 5.4pt 0cm 5.4pt;height:15.0pt'>
                                      <p class=MsoNormal align=center style='text-align:center'>
                                          <span style='font-size:11.0pt;color:windowtext'>4</span>
                                      </p>
                                  </td>
                                  <td width='55%' style='width:55.44%;border-top:none;border-left:none;
                        border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
                        padding:0cm 5.4pt 0cm 5.4pt;height:15.0pt'>
                                      <p class=MsoNormal style='text-align:justify'>
                                          <span style='font-size:11.0pt;
                        color:windowtext'>
                                              Số
                                              người phải kê khai tài sản, thu nhập hằng
                                              năm
                                          </span>
                                      </p>
                                  </td>
                                  <td width='19%' style='width:19.8%;border-top:none;border-left:none;
                        border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
                        padding:0cm 5.4pt 0cm 5.4pt;height:15.0pt'>
                                      <p class=MsoNormal align=center style='text-align:center'>
                                          <span style='font-size:11.0pt;color:windowtext'>Người</span>
                                      </p>
                                  </td>
                                  <td width='15%' style='width:15.24%;border-top:none;border-left:none;
                        border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
                        padding:0cm 5.4pt 0cm 5.4pt;height:15.0pt'>
                                      <center><p class=MsoNormal style='text-align:center'>
                                          <span style='font-size:11.0pt;
                        color:windowtext'>{SoNguoiPhaiKeKhaiTSBS}</span>
                                      </p></center>
                                  </td>
                              </tr>
                              <tr style='height:15.0pt'>
                                  <td width='9%' style='width:9.5%;border:solid windowtext 1.0pt;border-top:
                        none;padding:0cm 5.4pt 0cm 5.4pt;height:15.0pt'>
                                      <p class=MsoNormal align=center style='text-align:center'>
                                          <span style='font-size:11.0pt;color:windowtext'>5</span>
                                      </p>
                                  </td>
                                  <td width='55%' style='width:55.44%;border-top:none;border-left:none;
                        border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
                        padding:0cm 5.4pt 0cm 5.4pt;height:15.0pt'>
                                      <p class=MsoNormal style='text-align:justify'>
                                          <span style='font-size:11.0pt;
                        color:windowtext'>
                                              Số
                                              người đã kê khai tài sản, thu nhập hằng
                                              năm
                                          </span>
                                      </p>
                                  </td>
                                  <td width='19%' style='width:19.8%;border-top:none;border-left:none;
                        border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
                        padding:0cm 5.4pt 0cm 5.4pt;height:15.0pt'>
                                      <p class=MsoNormal align=center style='text-align:center'>
                                          <span style='font-size:11.0pt;color:windowtext'>Người</span>
                                      </p>
                                  </td>
                                  <td width='15%' style='width:15.24%;border-top:none;border-left:none;
                        border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
                        padding:0cm 5.4pt 0cm 5.4pt;height:15.0pt'>
                                      <center><p class=MsoNormal style='text-align:center'>
                                          <span style='font-size:11.0pt;
                        color:windowtext'>{SoNguoiDaKeKhaiTSHN}</span>
                                      </p></center>
                                  </td>
                              </tr>
                              <tr style='height:15.0pt'>
                                  <td width='9%' style='width:9.5%;border:solid windowtext 1.0pt;border-top:
                        none;padding:0cm 5.4pt 0cm 5.4pt;height:15.0pt'>
                                      <p class=MsoNormal align=center style='text-align:center'>
                                          <span style='font-size:11.0pt;color:windowtext'>6</span>
                                      </p>
                                  </td>
                                  <td width='55%' style='width:55.44%;border-top:none;border-left:none;
                        border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
                        padding:0cm 5.4pt 0cm 5.4pt;height:15.0pt'>
                                      <p class=MsoNormal style='text-align:justify'>
                                          <span style='font-size:11.0pt;
                        color:windowtext'>
                                              Số
                                              người phải kê khai tài sản, thu nhập bổ
                                              sung
                                          </span>
                                      </p>
                                  </td>
                                  <td width='19%' style='width:19.8%;border-top:none;border-left:none;
                        border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
                        padding:0cm 5.4pt 0cm 5.4pt;height:15.0pt'>
                                      <p class=MsoNormal align=center style='text-align:center'>
                                          <span style='font-size:11.0pt;color:windowtext'>Người</span>
                                      </p>
                                  </td>
                                  <td width='15%' style='width:15.24%;border-top:none;border-left:none;
                        border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
                        padding:0cm 5.4pt 0cm 5.4pt;height:15.0pt'>
                                      <center><p class=MsoNormal style='text-align:center'>
                                          <span style='font-size:11.0pt;
                        color:windowtext'>{SoNguoiPhaiKeKhaiTSBS}</span>
                                      </p><center>
                                  </td>
                              </tr>
                              <tr style='height:15.0pt'>
                                  <td width='9%' style='width:9.5%;border:solid windowtext 1.0pt;border-top:
                        none;padding:0cm 5.4pt 0cm 5.4pt;height:15.0pt'>
                                      <p class=MsoNormal align=center style='text-align:center'>
                                          <span style='font-size:11.0pt;color:windowtext'>7</span>
                                      </p>
                                  </td>
                                  <td width='55%' style='width:55.44%;border-top:none;border-left:none;
                        border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
                        padding:0cm 5.4pt 0cm 5.4pt;height:15.0pt'>
                                      <p class=MsoNormal style='text-align:justify'>
                                          <span style='font-size:11.0pt;
                        color:windowtext'>
                                              Số
                                              người đã kê khai tài sản, thu nhập bổ sung
                                          </span>
                                      </p>
                                  </td>
                                  <td width='19%' style='width:19.8%;border-top:none;border-left:none;
                        border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
                        padding:0cm 5.4pt 0cm 5.4pt;height:15.0pt'>
                                      <p class=MsoNormal align=center style='text-align:center'>
                                          <span style='font-size:11.0pt;color:windowtext'>Người</span>
                                      </p>
                                  </td>
                                  <td width='15%' style='width:15.24%;border-top:none;border-left:none;
                        border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
                        padding:0cm 5.4pt 0cm 5.4pt;height:15.0pt'>
                                      <center><p class=MsoNormal style='text-align:center'>
                                          <span style='font-size:11.0pt;
                        color:windowtext'>{SoNguoiDaKeKhaiTSBS}</span>
                                      </p><center>
                                  </td>
                              </tr>
                              <tr style='height:15.0pt'>
                                  <td width='9%' style='width:9.5%;border:solid windowtext 1.0pt;border-top:
                        none;padding:0cm 5.4pt 0cm 5.4pt;height:15.0pt'>
                                      <p class=MsoNormal align=center style='text-align:center'>
                                          <b>
                                              <span style='font-size:11.0pt;color:windowtext'>II</span>
                                          </b>
                                      </p>
                                  </td>
                                  <td width='55%' style='width:55.44%;border-top:none;border-left:none;
                        border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
                        padding:0cm 5.4pt 0cm 5.4pt;height:15.0pt'>
                                      <p class=MsoNormal style='text-align:justify'>
                                          <b>
                                              <span style='font-size:11.0pt;
                        color:windowtext'>
                                                  Công khai Bản kê
                                                  khai tài sản, thu nhập
                                              </span>
                                          </b>
                                      </p>
                                  </td>
                                  <td width='19%' style='width:19.8%;border-top:none;border-left:none;
                        border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
                        padding:0cm 5.4pt 0cm 5.4pt;height:15.0pt'>
                                      <p class=MsoNormal align=center style='text-align:center'>
                                          <span style='font-size:11.0pt;color:windowtext'>&nbsp;</span>
                                      </p>
                                  </td>
                                  <td width='15%' style='width:15.24%;border-top:none;border-left:none;
                        border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
                        padding:0cm 5.4pt 0cm 5.4pt;height:15.0pt'>
                                      <p class=MsoNormal style='text-align:center'>
                                          <span style='font-size:11.0pt;
                        color:windowtext'>&nbsp;</span>
                                      </p>
                                  </td>
                              </tr>
                              <tr style='height:30.0pt'>
                                  <td width='9%' style='width:9.5%;border:solid windowtext 1.0pt;border-top:
                        none;padding:0cm 5.4pt 0cm 5.4pt;height:30.0pt'>
                                      <p class=MsoNormal align=center style='text-align:center'>
                                          <span style='font-size:11.0pt;color:windowtext'>1</span>
                                      </p>
                                  </td>
                                  <td width='55%' style='width:55.44%;border-top:none;border-left:none;
                        border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
                        padding:0cm 5.4pt 0cm 5.4pt;height:30.0pt'>
                                      <p class=MsoNormal style='text-align:justify'>
                                          <span style='font-size:11.0pt;
                        color:windowtext'>
                                              Số cơ quan,
                                              tổ chức, đơn vị đã tổ chức
                                              thực hiện việc công khai bản kê khai tài sản,
                                              thu nhập
                                          </span>
                                      </p>
                                  </td>
                                  <td width='19%' style='width:19.8%;border-top:none;border-left:none;
                        border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
                        padding:0cm 5.4pt 0cm 5.4pt;height:30.0pt'>
                                      <p class=MsoNormal align=center style='text-align:center'>
                                          <span style='font-size:11.0pt;color:windowtext'>
                                              CQ,
                                              TC, ĐV
                                          </span>
                                      </p>
                                  </td>
                                  <td width='15%' style='width:15.24%;border-top:none;border-left:none;
                        border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
                        padding:0cm 5.4pt 0cm 5.4pt;height:30.0pt'>
                                      <p class=MsoNormal style='text-align:center'>
                                          <span style='font-size:11.0pt;
                        color:windowtext'>&nbsp;</span>
                                      </p>
                                  </td>
                              </tr>
                              <tr style='height:15.0pt'>
                                  <td width='9%' style='width:9.5%;border:solid windowtext 1.0pt;border-top:
                        none;padding:0cm 5.4pt 0cm 5.4pt;height:15.0pt'>
                                      <p class=MsoNormal align=center style='text-align:center'>
                                          <span style='font-size:11.0pt;color:windowtext'>&nbsp;</span>
                                      </p>
                                  </td>
                                  <td width='55%' style='width:55.44%;border-top:none;border-left:none;
                        border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
                        padding:0cm 5.4pt 0cm 5.4pt;height:15.0pt'>
                                      <p class=MsoNormal style='text-align:justify'>
                                          <span style='font-size:11.0pt;
                        color:windowtext'>
                                              Tỉ lệ so
                                              với tổng số cơ quan, tổ chức,
                                              đơn vị
                                          </span>
                                      </p>
                                  </td>
                                  <td width='19%' style='width:19.8%;border-top:none;border-left:none;
                        border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
                        padding:0cm 5.4pt 0cm 5.4pt;height:15.0pt'>
                                      <p class=MsoNormal align=center style='text-align:center'>
                                          <span style='font-size:11.0pt;color:windowtext'>%</span>
                                      </p>
                                  </td>
                                  <td width='15%' style='width:15.24%;border-top:none;border-left:none;
                        border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
                        padding:0cm 5.4pt 0cm 5.4pt;height:15.0pt'>
                                      <p class=MsoNormal style='text-align:center'>
                                          <span style='font-size:11.0pt;
                        color:windowtext'>&nbsp;</span>
                                      </p>
                                  </td>
                              </tr>
                              <tr style='height:30.0pt'>
                                  <td width='9%' style='width:9.5%;border:solid windowtext 1.0pt;border-top:
                        none;padding:0cm 5.4pt 0cm 5.4pt;height:30.0pt'>
                                      <p class=MsoNormal align=center style='text-align:center'>
                                          <span style='font-size:11.0pt;color:windowtext'>2</span>
                                      </p>
                                  </td>
                                  <td width='55%' style='width:55.44%;border-top:none;border-left:none;
                        border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
                        padding:0cm 5.4pt 0cm 5.4pt;height:30.0pt'>
                                      <p class=MsoNormal style='text-align:justify'>
                                          <span style='font-size:11.0pt;
                        color:windowtext'>
                                              Số cơ quan,
                                              tổ chức, đơn vị chưa thực hiện
                                              hoặc chưa được tổng hợp kết
                                              quả trong báo cáo này
                                          </span>
                                      </p>
                                  </td>
                                  <td width='19%' style='width:19.8%;border-top:none;border-left:none;
                        border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
                        padding:0cm 5.4pt 0cm 5.4pt;height:30.0pt'>
                                      <p class=MsoNormal align=center style='text-align:center'>
                                          <span style='font-size:11.0pt;color:windowtext'>CQTCĐV</span>
                                      </p>
                                  </td>
                                  <td width='15%' style='width:15.24%;border-top:none;border-left:none;
                        border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
                        padding:0cm 5.4pt 0cm 5.4pt;height:30.0pt'>
                                      <p class=MsoNormal style='text-align:center'>
                                          <span style='font-size:11.0pt;
                        color:windowtext'>&nbsp;</span>
                                      </p>
                                  </td>
                              </tr>
                              <tr style='height:15.0pt'>
                                  <td width='9%' style='width:9.5%;border:solid windowtext 1.0pt;border-top:
                        none;padding:0cm 5.4pt 0cm 5.4pt;height:15.0pt'>
                                      <p class=MsoNormal align=center style='text-align:center'>
                                          <span style='font-size:11.0pt;color:windowtext'>&nbsp;</span>
                                      </p>
                                  </td>
                                  <td width='55%' style='width:55.44%;border-top:none;border-left:none;
                        border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
                        padding:0cm 5.4pt 0cm 5.4pt;height:15.0pt'>
                                      <p class=MsoNormal style='text-align:justify'>
                                          <span style='font-size:11.0pt;
                        color:windowtext'>
                                              Tỉ lệ so với
                                              tổng số cơ quan, tổ chức, đơn vị
                                          </span>
                                      </p>
                                  </td>
                                  <td width='19%' style='width:19.8%;border-top:none;border-left:none;
                        border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
                        padding:0cm 5.4pt 0cm 5.4pt;height:15.0pt'>
                                      <p class=MsoNormal align=center style='text-align:center'>
                                          <span style='font-size:11.0pt;color:windowtext'>%</span>
                                      </p>
                                  </td>
                                  <td width='15%' style='width:15.24%;border-top:none;border-left:none;
                        border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
                        padding:0cm 5.4pt 0cm 5.4pt;height:15.0pt'>
                                      <p class=MsoNormal style='text-align:center'>
                                          <span style='font-size:11.0pt;
                        color:windowtext'>&nbsp;</span>
                                      </p>
                                  </td>
                              </tr>
                              <tr style='height:15.0pt'>
                                  <td width='9%' style='width:9.5%;border:solid windowtext 1.0pt;border-top:
                        none;padding:0cm 5.4pt 0cm 5.4pt;height:15.0pt'>
                                      <p class=MsoNormal align=center style='text-align:center'>
                                          <span style='font-size:11.0pt;color:windowtext'>3</span>
                                      </p>
                                  </td>
                                  <td width='55%' style='width:55.44%;border-top:none;border-left:none;
                        border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
                        padding:0cm 5.4pt 0cm 5.4pt;height:15.0pt'>
                                      <p class=MsoNormal style='text-align:justify'>
                                          <span style='font-size:11.0pt;
                        color:windowtext'>
                                              Tổng số
                                              bản kê khai
                                          </span>
                                      </p>
                                  </td>
                                  <td width='19%' style='width:19.8%;border-top:none;border-left:none;
                        border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
                        padding:0cm 5.4pt 0cm 5.4pt;height:15.0pt'>
                                      <p class=MsoNormal align=center style='text-align:center'>
                                          <span style='font-size:11.0pt;color:windowtext'>
                                              Bản
                                              kê khai
                                          </span>
                                      </p>
                                  </td>
                                  <td width='15%' style='width:15.24%;border-top:none;border-left:none;
                        border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
                        padding:0cm 5.4pt 0cm 5.4pt;height:15.0pt'>
                                      <p class=MsoNormal style='text-align:center'>
                                          <span style='font-size:11.0pt;
                        color:windowtext'>&nbsp;</span>
                                      </p>
                                  </td>
                              </tr>
                              <tr style='height:15.0pt'>
                                  <td width='9%' style='width:9.5%;border:solid windowtext 1.0pt;border-top:
                        none;padding:0cm 5.4pt 0cm 5.4pt;height:15.0pt'>
                                      <p class=MsoNormal align=center style='text-align:center'>
                                          <span style='font-size:11.0pt;color:windowtext'>4</span>
                                      </p>
                                  </td>
                                  <td width='55%' style='width:55.44%;border-top:none;border-left:none;
                        border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
                        padding:0cm 5.4pt 0cm 5.4pt;height:15.0pt'>
                                      <p class=MsoNormal style='text-align:justify'>
                                          <span style='font-size:11.0pt;
                        color:windowtext'>
                                              Số bản kê
                                              khai đã được công khai
                                          </span>
                                      </p>
                                  </td>
                                  <td width='19%' style='width:19.8%;border-top:none;border-left:none;
                        border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
                        padding:0cm 5.4pt 0cm 5.4pt;height:15.0pt'>
                                      <p class=MsoNormal align=center style='text-align:center'>
                                          <span style='font-size:11.0pt;color:windowtext'>
                                              Bản
                                              kê khai
                                          </span>
                                      </p>
                                  </td>
                                  <td width='15%' style='width:15.24%;border-top:none;border-left:none;
                        border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
                        padding:0cm 5.4pt 0cm 5.4pt;height:15.0pt'>
                                      <p class=MsoNormal style='text-align:center'>
                                          <span style='font-size:11.0pt;
                        color:windowtext'>&nbsp;</span>
                                      </p>
                                  </td>
                              </tr>
                              <tr style='height:15.0pt'>
                                  <td width='9%' style='width:9.5%;border:solid windowtext 1.0pt;border-top:
                        none;padding:0cm 5.4pt 0cm 5.4pt;height:15.0pt'>
                                      <p class=MsoNormal align=center style='text-align:center'>
                                          <span style='font-size:11.0pt;color:windowtext'>&nbsp;</span>
                                      </p>
                                  </td>
                                  <td width='55%' style='width:55.44%;border-top:none;border-left:none;
                        border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
                        padding:0cm 5.4pt 0cm 5.4pt;height:15.0pt'>
                                      <p class=MsoNormal style='text-align:justify'>
                                          <span style='font-size:11.0pt;
                        color:windowtext'>
                                              Tỉ lệ so
                                              với tổng số bản kê khai
                                          </span>
                                      </p>
                                  </td>
                                  <td width='19%' style='width:19.8%;border-top:none;border-left:none;
                        border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
                        padding:0cm 5.4pt 0cm 5.4pt;height:15.0pt'>
                                      <p class=MsoNormal align=center style='text-align:center'>
                                          <span style='font-size:11.0pt;color:windowtext'>%</span>
                                      </p>
                                  </td>
                                  <td width='15%' style='width:15.24%;border-top:none;border-left:none;
                        border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
                        padding:0cm 5.4pt 0cm 5.4pt;height:15.0pt'>
                                      <p class=MsoNormal style='text-align:center'>
                                          <span style='font-size:11.0pt;
                        color:windowtext'>&nbsp;</span>
                                      </p>
                                  </td>
                              </tr>
                              <tr style='height:15.0pt'>
                                  <td width='9%' style='width:9.5%;border:solid windowtext 1.0pt;border-top:
                        none;padding:0cm 5.4pt 0cm 5.4pt;height:15.0pt'>
                                      <p class=MsoNormal align=center style='text-align:center'>
                                          <span style='font-size:11.0pt;color:windowtext'>5</span>
                                      </p>
                                  </td>
                                  <td width='55%' style='width:55.44%;border-top:none;border-left:none;
                        border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
                        padding:0cm 5.4pt 0cm 5.4pt;height:15.0pt'>
                                      <p class=MsoNormal style='text-align:justify'>
                                          <span style='font-size:11.0pt;
                        color:windowtext'>
                                              Số bản kê
                                              khai đã được công khai theo hình thức niêm
                                              yết
                                          </span>
                                      </p>
                                  </td>
                                  <td width='19%' style='width:19.8%;border-top:none;border-left:none;
                        border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
                        padding:0cm 5.4pt 0cm 5.4pt;height:15.0pt'>
                                      <p class=MsoNormal align=center style='text-align:center'>
                                          <span style='font-size:11.0pt;color:windowtext'>
                                              Bản
                                              kê khai
                                          </span>
                                      </p>
                                  </td>
                                  <td width='15%' style='width:15.24%;border-top:none;border-left:none;
                        border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
                        padding:0cm 5.4pt 0cm 5.4pt;height:15.0pt'>
                                      <p class=MsoNormal style='text-align:center'>
                                          <span style='font-size:11.0pt;
                        color:windowtext'>&nbsp;</span>
                                      </p>
                                  </td>
                              </tr>
                              <tr style='height:15.0pt'>
                                  <td width='9%' style='width:9.5%;border:solid windowtext 1.0pt;border-top:
                        none;padding:0cm 5.4pt 0cm 5.4pt;height:15.0pt'>
                                      <p class=MsoNormal align=center style='text-align:center'>
                                          <span style='font-size:11.0pt;color:windowtext'>&nbsp;</span>
                                      </p>
                                  </td>
                                  <td width='55%' style='width:55.44%;border-top:none;border-left:none;
                        border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
                        padding:0cm 5.4pt 0cm 5.4pt;height:15.0pt'>
                                      <p class=MsoNormal style='text-align:justify'>
                                          <span style='font-size:11.0pt;
                        color:windowtext'>
                                              Tỉ lệ so
                                              với số bản kê khai đã được công khai
                                          </span>
                                      </p>
                                  </td>
                                  <td width='19%' style='width:19.8%;border-top:none;border-left:none;
                        border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
                        padding:0cm 5.4pt 0cm 5.4pt;height:15.0pt'>
                                      <p class=MsoNormal align=center style='text-align:center'>
                                          <span style='font-size:11.0pt;color:windowtext'>%</span>
                                      </p>
                                  </td>
                                  <td width='15%' style='width:15.24%;border-top:none;border-left:none;
                        border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
                        padding:0cm 5.4pt 0cm 5.4pt;height:15.0pt'>
                                      <p class=MsoNormal style='text-align:center'>
                                          <span style='font-size:11.0pt;
                        color:windowtext'>&nbsp;</span>
                                      </p>
                                  </td>
                              </tr>
                              <tr style='height:30.0pt'>
                                  <td width='9%' style='width:9.5%;border:solid windowtext 1.0pt;border-top:
                        none;padding:0cm 5.4pt 0cm 5.4pt;height:30.0pt'>
                                      <p class=MsoNormal align=center style='text-align:center'>
                                          <span style='font-size:11.0pt;color:windowtext'>6</span>
                                      </p>
                                  </td>
                                  <td width='55%' style='width:55.44%;border-top:none;border-left:none;
                        border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
                        padding:0cm 5.4pt 0cm 5.4pt;height:30.0pt'>
                                      <p class=MsoNormal style='text-align:justify'>
                                          <span style='font-size:11.0pt;
                        color:windowtext'>
                                              Số bản kê
                                              khai đã công khai theo hình thức công bố tại
                                              cuộc hộp
                                          </span>
                                      </p>
                                  </td>
                                  <td width='19%' style='width:19.8%;border-top:none;border-left:none;
                        border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
                        padding:0cm 5.4pt 0cm 5.4pt;height:30.0pt'>
                                      <p class=MsoNormal align=center style='text-align:center'>
                                          <span style='font-size:11.0pt;color:windowtext'>
                                              Bản
                                              kê khai
                                          </span>
                                      </p>
                                  </td>
                                  <td width='15%' style='width:15.24%;border-top:none;border-left:none;
                          border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
                          padding:0cm 5.4pt 0cm 5.4pt;height:30.0pt'>
                                      <p class=MsoNormal style='text-align:center'>
                                          <span style='font-size:11.0pt; color:windowtext'>&nbsp;</span>
                                      </p>
                                  </td>
                              </tr>
                              <tr style='height:15.0pt'>
                                  <td width='9%' style='width:9.5%;border:solid windowtext 1.0pt;border-top: none;padding:0cm 5.4pt 0cm 5.4pt;height:15.0pt'>
                                      <p class=MsoNormal align=center style='text-align:center'>
                                          <span style='font-size:11.0pt;color:windowtext'>&nbsp;</span>
                                      </p>
                                  </td>
                                  <td width='55%' style='width:55.44%;border-top:none;border-left:none;
                          border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
                          padding:0cm 5.4pt 0cm 5.4pt;height:15.0pt'>
                                      <p class=MsoNormal style='text-align:justify'>
                                          <span style='font-size:11.0pt; color:windowtext'>
                                              Tỉ lệ so
                                              với số bản kê khai đã được công khai
                                          </span>
                                      </p>
                                  </td>
                                  <td width='19%' style='width:19.8%;border-top:none;border-left:none;
                          border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
                          padding:0cm 5.4pt 0cm 5.4pt;height:15.0pt'>
                                      <p class=MsoNormal align=center style='text-align:center'>
                                          <span style='font-size:11.0pt;color:windowtext'>%</span>
                                      </p>
                                  </td>
                                  <td width='15%' style='width:15.24%;border-top:none;border-left:none;
                          border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
                          padding:0cm 5.4pt 0cm 5.4pt;height:15.0pt'>
                                      <p class=MsoNormal style='text-align:center'>
                                          <span style='font-size:11.0pt;   color:windowtext'>&nbsp;</span>
                                      </p>
                                  </td>
                              </tr>
                          </table>

                          <p class=MsoNormal style='margin-top:6.0pt;margin-right:0cm;margin-bottom:6.0pt; margin-left:0cm;text-align:justify;text-indent:1.0cm;line-height:16.0pt'>
                              <span style='font-size:13.0pt;color:windowtext'>&nbsp;</span>
                          </p>

                          <p class=MsoNormal style='margin-top:6.0pt;margin-right:0cm;margin-bottom:6.0pt; margin-left:0cm;text-align:justify;text-indent:1.0cm'>
                              <b>
                                  <span style='font-size:13.0pt;color:windowtext'>
                                      2.2.
                                      Kết quả kê khai, công khai Bản kê khai tài sản, thu
                                      nhập phục vụ công tác cán bộ năm 2021 theo
                                  </span>
                              </b><b>
                                  <span style='font-size:13.0pt;color:windowtext'>
                                      khoản
                                      4 Điều 36 Luật
                                  </span>
                              </b><span class=Vnbnnidung213pt>
                                  <b>
                                      <span style='font-size:13.0pt;color:windowtext'> PCTN năm </span>
                                  </b>
                              </span><b>
                                  <span style='font-size:13.0pt;color:windowtext'>2018</span>
                              </b>
                          </p>

                          <p class=MsoNormal style='margin-top:6.0pt;margin-right:0cm;margin-bottom:6.0pt; margin-left:0cm;text-align:justify;text-indent:1.0cm'>
                              <span style='font-size: 14.0pt;color:windowtext'>
                                  - Đơn
                                  vị báo cáo việc triển khai công tác kê khai, công khai tài
                                  sản, thu nhập của người có nghĩa vụ kê
                                  khai quy định tại các khoản 1, 2 và 3 Điều 34
                                  của Luật PCTN 2018 được dự kiến
                                  bầu, phê chuẩn, bổ nhiệm, bổ nhiệm
                                  lại, cử giữ chức vụ khác trong năm 2021.
                              </span>
                          </p>

                          <p class=MsoNormal style='margin-top:6.0pt;margin-right:0cm;margin-bottom:6.0pt; margin-left:0cm;text-align:justify;text-indent:1.0cm'>
                              <span style='font-size: 14.0pt;color:windowtext'>
                                  - Thời gian
                                  tổ chức cuộc hộp lấy phiếu tín nhiệm
                                  khi tiến hành bầu, phê chuẩn, bổ nhiệm, bổ
                                  nhiệm lại, cử giữ chức vụ khác: thứ… ngày…
                                  tháng… năm 2021
                              </span>
                          </p>

                          <p class=MsoNormal style='margin-top:6.0pt;margin-right:0cm;margin-bottom:6.0pt; margin-left:0cm;text-align:justify;text-indent:1.0cm'>
                              <span style='font-size: 14.0pt;color:windowtext'>
                                  - Danh sách người
                                  có nghĩa vụ kê khai quy định tại các khoản 1,
                                  2 và 3 Điều 34 của Luật PCTN 2018 được
                                  dự kiến bầu, phê chuẩn, bổ nhiệm, bổ nhiệm
                                  lại, cử giữ chức vụ khác trong năm 2021
                                  đã tiến hành kê khai tài sản, thu nhập:
                              </span>
                          </p>

                          <table class=MsoNormalTable border=0 cellspacing=0 cellpadding=0 width='100%'
                                 style='width:100.0%;border-collapse:collapse'>
                              <tr style='height:50.25pt'>
                                  <td width='9%' nowrap style='width:9.4%;border:solid windowtext 1.0pt; border-bottom:none;background:white;padding:0cm 5.4pt 0cm 5.4pt;height:50.25pt'>
                                      <p class=MsoNormal align=center style='text-align:center'>
                                          <b>
                                              <span style='font-size:10.0pt;color:windowtext'>STT</span>
                                          </b>
                                      </p>
                                  </td>
                                  <td width='16%' style='width:16.22%;border-top:solid windowtext 1.0pt;
                          border-left:none;border-bottom:none;border-right:solid windowtext 1.0pt;
                          background:white;padding:0cm 5.4pt 0cm 5.4pt;height:50.25pt'>
                                      <p class=MsoNormal align=center style='text-align:center'>
                                          <b>
                                              <span style='font-size:10.0pt;color:windowtext'>
                                                  Hộ
                                                  và tên
                                              </span>
                                          </b>
                                      </p>
                                  </td>
                                  <td width='21%' style='width:21.14%;border:solid windowtext 1.0pt;border-left: none;background:white;padding:0cm 5.4pt 0cm 5.4pt;height:50.25pt'>
                                      <p class=MsoNormal align=center style='text-align:center'>
                                          <b>
                                              <span style='font-size:10.0pt;color:windowtext'>
                                                  Ngày
                                                  tháng năm sinh
                                              </span>
                                          </b>
                                      </p>
                                  </td>
                                  <td width='23%' style='width:23.48%;border:solid windowtext 1.0pt;border-left: none;background:white;padding:0cm 5.4pt 0cm 5.4pt;height:50.25pt'>
                                      <p class=MsoNormal align=center style='text-align:center'>
                                          <b>
                                              <span style='font-size:10.0pt;color:windowtext'>
                                                  Chức
                                                  vụ/ chức danh công tác
                                              </span>
                                          </b>
                                      </p>
                                  </td>
                                  <td width='17%' style='width:17.52%;border:solid windowtext 1.0pt;border-left: none;background:white;padding:0cm 5.4pt 0cm 5.4pt;height:50.25pt'>
                                      <p class=MsoNormal align=center style='text-align:center'>
                                          <b>
                                              <span style='font-size:10.0pt;color:windowtext'>
                                                  Cơ
                                                  quan/đơn vị công tác
                                              </span>
                                          </b>
                                      </p>
                                  </td>
                                  <td width='12%' style='width:12.22%;border-top:solid windowtext 1.0pt;
                          border-left:none;border-bottom:none;border-right:solid windowtext 1.0pt;
                          background:white;padding:0cm 5.4pt 0cm 5.4pt;height:50.25pt'>
                                      <p class=MsoNormal align=center style='text-align:center'>
                                          <b>
                                              <span style='font-size:10.0pt;color:windowtext'>
                                                  Ghi
                                                  chú
                                              </span>
                                          </b>
                                      </p>
                                  </td>
                              </tr>
                              <tr style='height:15.0pt'>
                                  <td width='9%' nowrap style='width:9.4%;border:solid windowtext 1.0pt;   background:white;padding:0cm 5.4pt 0cm 5.4pt;height:15.0pt'>
                                      <p class=MsoNormal align=center style='text-align:center'>
                                          <span style='font-size:10.0pt;color:windowtext'>&nbsp;</span>
                                      </p>
                                  </td>
                                  <td width='16%' style='width:16.22%;border:solid windowtext 1.0pt;border-left: none;background:white;padding:0cm 5.4pt 0cm 5.4pt;height:15.0pt'>
                                      <p class=MsoNormal align=center style='text-align:center'>
                                          <span style='font-size:10.0pt;color:windowtext'>1</span>
                                      </p>
                                  </td>
                                  <td width='21%' nowrap style='width:21.14%;border-top:none;border-left:none;
                          border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
                          background:white;padding:0cm 5.4pt 0cm 5.4pt;height:15.0pt'>
                                      <p class=MsoNormal align=center style='text-align:center'>
                                          <span style='font-size:10.0pt;color:windowtext'>5</span>
                                      </p>
                                  </td>
                                  <td width='23%' nowrap style='width:23.48%;border-top:none;border-left:none;
                          border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
                          background:white;padding:0cm 5.4pt 0cm 5.4pt;height:15.0pt'>
                                      <p class=MsoNormal align=center style='text-align:center'>
                                          <span style='font-size:10.0pt;color:windowtext'>6</span>
                                      </p>
                                  </td>
                                  <td width='17%' nowrap style='width:17.52%;border-top:none;border-left:none;
                          border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
                          background:white;padding:0cm 5.4pt 0cm 5.4pt;height:15.0pt'>
                                      <p class=MsoNormal align=center style='text-align:center'>
                                          <span style='font-size:10.0pt;color:windowtext'>7</span>
                                      </p>
                                  </td>
                                  <td width='12%' style='width:12.22%;border:solid windowtext 1.0pt;border-left: none;background:white;padding:0cm 5.4pt 0cm 5.4pt;height:15.0pt'>
                                      <p class=MsoNormal align=center style='text-align:center'>
                                          <span style='font-size:10.0pt;color:windowtext'>17</span>
                                      </p>
                                  </td>
                              </tr>
                              <tr style='height:15.0pt'>
                                  <td width='9%' nowrap style='width:9.4%;border:solid windowtext 1.0pt; border-top:none;background:white;padding:0cm 5.4pt 0cm 5.4pt;height:15.0pt'>
                                      <p class=MsoNormal align=center style='text-align:center'>
                                          <span style='font-size:10.0pt;color:windowtext'>&nbsp;</span>
                                      </p>
                                  </td>
                                  <td width='16%' style='width:16.22%;border-top:none;border-left:none;
                          border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
                          background:white;padding:0cm 5.4pt 0cm 5.4pt;height:15.0pt'>
                                      <p class=MsoNormal>
                                          <span style='font-size:10.0pt; color:windowtext'>&nbsp;</span>
                                      </p>
                                  </td>
                                  <td width='21%' style='width:21.14%;border-top:none;border-left:none;
                          border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
                          background:white;padding:0cm 5.4pt 0cm 5.4pt;height:15.0pt'>
                                      <p class=MsoNormal align=center style='text-align:center'>
                                          <span style='font-size:10.0pt;color:windowtext'>&nbsp;</span>
                                      </p>
                                  </td>
                                  <td width='23%' style='width:23.48%;border-top:none;border-left:none;
                          border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
                          background:white;padding:0cm 5.4pt 0cm 5.4pt;height:15.0pt'>
                                      <p class=MsoNormal align=center style='text-align:center'>
                                          <span style='font-size:10.0pt;color:windowtext'>&nbsp;</span>
                                      </p>
                                  </td>
                                  <td width='17%' style='width:17.52%;border-top:none;border-left:none;
                          border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
                          background:white;padding:0cm 5.4pt 0cm 5.4pt;height:15.0pt'>
                                      <p class=MsoNormal align=center style='text-align:center'>
                                          <span style='font-size:10.0pt;color:windowtext'>&nbsp;</span>
                                      </p>
                                  </td>
                                  <td width='12%' style='width:12.22%;border-top:none;border-left:none;
                          border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
                          background:white;padding:0cm 5.4pt 0cm 5.4pt;height:15.0pt'>
                                      <p class=MsoNormal>
                                          <span style='font-size:10.0pt; color:windowtext'>&nbsp;</span>
                                      </p>
                                  </td>
                              </tr>
                              <tr style='height:15.0pt'>
                                  <td width='9%' nowrap style='width:9.4%;border:solid windowtext 1.0pt; border-top:none;background:white;padding:0cm 5.4pt 0cm 5.4pt;height:15.0pt'>
                                      <p class=MsoNormal align=center style='text-align:center'>
                                          <b>
                                              <span style='font-size:10.0pt;color:windowtext'>&nbsp;</span>
                                          </b>
                                      </p>
                                  </td>
                                  <td width='16%' style='width:16.22%;border-top:none;border-left:none;
                          border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
                          background:white;padding:0cm 5.4pt 0cm 5.4pt;height:15.0pt'>
                                      <p class=MsoNormal>
                                          <b>
                                              <span style='font-size:10.0pt; color:windowtext'>&nbsp;</span>
                                          </b>
                                      </p>
                                  </td>
                                  <td width='21%' style='width:21.14%;border-top:none;border-left:none;
                          border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
                          background:white;padding:0cm 5.4pt 0cm 5.4pt;height:15.0pt'>
                                      <p class=MsoNormal align=center style='text-align:center'>
                                          <b>
                                              <i>
                                                  <span style='font-size:10.0pt;color:windowtext'>&nbsp;</span>
                                              </i>
                                          </b>
                                      </p>
                                  </td>
                                  <td width='23%' style='width:23.48%;border-top:none;border-left:none;
                          border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
                          background:white;padding:0cm 5.4pt 0cm 5.4pt;height:15.0pt'>
                                      <p class=MsoNormal align=center style='text-align:center'>
                                          <b>
                                              <i>
                                                  <span style='font-size:10.0pt;color:windowtext'>&nbsp;</span>
                                              </i>
                                          </b>
                                      </p>
                                  </td>
                                  <td width='17%' style='width:17.52%;border-top:none;border-left:none;
                          border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
                          background:white;padding:0cm 5.4pt 0cm 5.4pt;height:15.0pt'>
                                      <p class=MsoNormal align=center style='text-align:center'>
                                          <b>
                                              <i>
                                                  <span style='font-size:10.0pt;color:windowtext'>&nbsp;</span>
                                              </i>
                                          </b>
                                      </p>
                                  </td>
                                  <td width='12%' style='width:12.22%;border-top:none;border-left:none;
                          border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
                          background:white;padding:0cm 5.4pt 0cm 5.4pt;height:15.0pt'>
                                      <p class=MsoNormal>
                                          <i>
                                              <span style='font-size:10.0pt; color:windowtext'>&nbsp;</span>
                                          </i>
                                      </p>
                                  </td>
                              </tr>
                              <tr style='height:15.0pt'>
                                  <td width='9%' nowrap style='width:9.4%;border:solid windowtext 1.0pt; border-top:none;background:white;padding:0cm 5.4pt 0cm 5.4pt;height:15.0pt'>
                                      <p class=MsoNormal align=center style='text-align:center'>
                                          <b>
                                              <span style='font-size:10.0pt;color:windowtext'>&nbsp;</span>
                                          </b>
                                      </p>
                                  </td>
                                  <td width='16%' style='width:16.22%;border-top:none;border-left:none;
                          border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
                          background:white;padding:0cm 5.4pt 0cm 5.4pt;height:15.0pt'>
                                      <p class=MsoNormal>
                                          <span style='font-size:10.0pt;   color:windowtext'>&nbsp;</span>
                                      </p>
                                  </td>
                                  <td width='21%' style='width:21.14%;border-top:none;border-left:none;
                          border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
                          background:white;padding:0cm 5.4pt 0cm 5.4pt;height:15.0pt'>
                                      <p class=MsoNormal align=center style='text-align:center'>
                                          <span style='font-size:10.0pt;color:windowtext'>&nbsp;</span>
                                      </p>
                                  </td>
                                  <td width='23%' style='width:23.48%;border-top:none;border-left:none;
                          border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
                          background:white;padding:0cm 5.4pt 0cm 5.4pt;height:15.0pt'>
                                      <p class=MsoNormal align=center style='text-align:center'>
                                          <span style='font-size:10.0pt;color:windowtext'>&nbsp;</span>
                                      </p>
                                  </td>
                                  <td width='17%' style='width:17.52%;border-top:none;border-left:none;
                          border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
                          background:white;padding:0cm 5.4pt 0cm 5.4pt;height:15.0pt'>
                                      <p class=MsoNormal align=center style='text-align:center'>
                                          <span style='font-size:10.0pt;color:windowtext'>&nbsp;</span>
                                      </p>
                                  </td>
                                  <td width='12%' style='width:12.22%;border-top:none;border-left:none;
                          border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
                          background:white;padding:0cm 5.4pt 0cm 5.4pt;height:15.0pt'>
                                      <p class=MsoNormal>
                                          <i>
                                              <span style='font-size:10.0pt;   color:windowtext'>&nbsp;</span>
                                          </i>
                                      </p>
                                  </td>
                              </tr>
                              <tr style='height:15.0pt'>
                                  <td width='9%' nowrap style='width:9.4%;border:solid windowtext 1.0pt;   border-top:none;background:white;padding:0cm 5.4pt 0cm 5.4pt;height:15.0pt'>
                                      <p class=MsoNormal align=center style='text-align:center'>
                                          <b>
                                              <span style='font-size:10.0pt;color:windowtext'>&nbsp;</span>
                                          </b>
                                      </p>
                                  </td>
                                  <td width='16%' style='width:16.22%;border-top:none;border-left:none;
                          border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
                          background:white;padding:0cm 5.4pt 0cm 5.4pt;height:15.0pt'>
                                      <p class=MsoNormal>
                                          <span style='font-size:10.0pt; color:windowtext'>&nbsp;</span>
                                      </p>
                                  </td>
                                  <td width='21%' style='width:21.14%;border-top:none;border-left:none;
                          border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
                          background:white;padding:0cm 5.4pt 0cm 5.4pt;height:15.0pt'>
                                      <p class=MsoNormal align=center style='text-align:center'>
                                          <span style='font-size:10.0pt;color:windowtext'>&nbsp;</span>
                                      </p>
                                  </td>
                                  <td width='23%' style='width:23.48%;border-top:none;border-left:none;
                          border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
                          background:white;padding:0cm 5.4pt 0cm 5.4pt;height:15.0pt'>
                                      <p class=MsoNormal align=center style='text-align:center'>
                                          <span style='font-size:10.0pt;color:windowtext'>&nbsp;</span>
                                      </p>
                                  </td>
                                  <td width='17%' style='width:17.52%;border-top:none;border-left:none;
                          border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
                          background:white;padding:0cm 5.4pt 0cm 5.4pt;height:15.0pt'>
                                      <p class=MsoNormal align=center style='text-align:center'>
                                          <span style='font-size:10.0pt;color:windowtext'>&nbsp;</span>
                                      </p>
                                  </td>
                                  <td width='12%' style='width:12.22%;border-top:none;border-left:none;
                          border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
                          background:white;padding:0cm 5.4pt 0cm 5.4pt;height:15.0pt'>
                                      <p class=MsoNormal>
                                          <i>
                                              <span style='font-size:10.0pt; color:windowtext'>&nbsp;</span>
                                          </i>
                                      </p>
                                  </td>
                              </tr>
                              <tr style='height:33.75pt'>
                                  <td width='9%' style='width:9.4%;border:solid windowtext 1.0pt;border-top:   none;background:white;padding:0cm 5.4pt 0cm 5.4pt;height:33.75pt'>
                                      <p class=MsoNormal align=center style='text-align:center'>
                                          <b>
                                              <span style='font-size:10.0pt;color:windowtext'>
                                                  Tổng
                                                  cộng
                                              </span>
                                          </b>
                                      </p>
                                  </td>
                                  <td width='16%' style='width:16.22%;border-top:none;border-left:none;
                          border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
                          background:white;padding:0cm 5.4pt 0cm 5.4pt;height:33.75pt'>
                                      <p class=MsoNormal>
                                          <span style='font-size:11.0pt;   color:windowtext'>&nbsp;</span>
                                      </p>
                                  </td>
                                  <td width='21%' nowrap style='width:21.14%;border-top:none;border-left:none;
                          border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
                          background:white;padding:0cm 5.4pt 0cm 5.4pt;height:33.75pt'>
                                      <p class=MsoNormal align=center style='text-align:center'>
                                          <b>
                                              <span style='font-size:11.0pt;color:windowtext'>&nbsp;</span>
                                          </b>
                                      </p>
                                  </td>
                                  <td width='23%' nowrap style='width:23.48%;border-top:none;border-left:none;
                          border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
                          background:white;padding:0cm 5.4pt 0cm 5.4pt;height:33.75pt'>
                                      <p class=MsoNormal align=center style='text-align:center'>
                                          <b>
                                              <span style='font-size:11.0pt;color:windowtext'>&nbsp;</span>
                                          </b>
                                      </p>
                                  </td>
                                  <td width='17%' nowrap style='width:17.52%;border-top:none;border-left:none;
                          border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
                          background:white;padding:0cm 5.4pt 0cm 5.4pt;height:33.75pt'>
                                      <p class=MsoNormal align=center style='text-align:center'>
                                          <b>
                                              <span style='font-size:11.0pt;color:windowtext'>&nbsp;</span>
                                          </b>
                                      </p>
                                  </td>
                                  <td width='12%' style='width:12.22%;border-top:none;border-left:none;
                          border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
                          background:white;padding:0cm 5.4pt 0cm 5.4pt;height:33.75pt'>
                                      <p class=MsoNormal>
                                          <span style='font-size:11.0pt;   color:windowtext'>&nbsp;</span>
                                      </p>
                                  </td>
                              </tr>
                          </table>

                          <p class=MsoNormal style='margin-top:6.0pt;margin-right:0cm;margin-bottom:6.0pt; margin-left:0cm;text-align:justify;text-indent:1.0cm'>
                              <span style='font-size: 14.0pt;color:windowtext'>&nbsp;</span>
                          </p>

                          <p class=MsoNormal style='margin-top:6.0pt;margin-right:0cm;margin-bottom:6.0pt; margin-left:0cm;text-align:justify;text-indent:1.0cm'>
                              <span style='font-size: 14.0pt;color:windowtext'>- </span><span style='font-size:13.0pt;color:windowtext'>
                                  Số
                                  bản kê khai của người có nghĩa vụ kê khai
                                  được dự kiến bầu, phê chuẩn, bổ
                                  nhiệm, bổ nhiệm lại, cử giữ chức
                                  vụ khác trong năm 2021 đã được công khai trong
                                  cuộc hộp: …/…
                              </span>
                          </p>

                          <p class=MsoNormal style='margin-top:6.0pt;margin-right:0cm;margin-bottom:6.0pt; margin-left:0cm;text-align:justify;text-indent:1.0cm'>
                              <span style='font-size: 14.0pt;color:windowtext'>&nbsp;</span>
                          </p>

                          <p class=MsoNormal style='margin-top:6.0pt;margin-right:0cm;margin-bottom:6.0pt; margin-left:0cm;text-align:justify;text-indent:1.0cm'>
                              <span style='font-size: 14.0pt;color:windowtext'>&nbsp;</span>
                          </p>

                          <p class=MsoNormal style='margin-top:6.0pt;margin-right:0cm;margin-bottom:6.0pt; margin-left:0cm;text-align:justify;text-indent:1.0cm'>
                                                <b>
                                                    <span style='font-size:13.0pt;color:windowtext'>
                                                        2.3.
                                                        Kết quả kê khai, công khai Bản kê khai tài sản, thu
                                                        nhập lần đầu năm 2021 theo điểm b
                                                    </span>
                                                </b><b>
                                                    <span style='font-size:13.0pt;color:windowtext'>
                                                        khoản
                                                        1 Điều 36 Luật
                                                    </span>
                                                </b><span class=Vnbnnidung213pt>
                                                    <b>
                                                        <span style='font-size:13.0pt;color:windowtext'> PCTN năm </span>
                                                    </b>
                                                </span><b>
                                                    <span style='font-size:13.0pt;color:windowtext'>2018</span>
                                                </b>
                                            </p>

                                            <p class=MsoNormal style='margin-top:6.0pt;margin-right:0cm;margin-bottom:6.0pt; margin-left:0cm;text-align:justify;text-indent:1.0cm'>
                                                <span style='font-size: 14.0pt;color:windowtext'>
                                                    - Đơn
                                                    vị báo cáo việc triển khai công tác kê khai, công khai tài
                                                    sản, thu nhập lần đầu của người
                                                    có nghĩa vụ kê khai quy định tại điểm b
                                                </span><span style='font-size:13.0pt;color:windowtext'>
                                                    khoản
                                                    1 Điều 36 Luật
                                                </span><span class=Vnbnnidung213pt>
                                                    <span style='font-size:13.0pt;color:windowtext'> PCTN năm </span>
                                                </span><span style='font-size:13.0pt;color:windowtext'>
                                                    2018
                                                    trong năm 2021.
                                                </span>
                                            </p>

                                            <p class=MsoNormal style='margin-top:6.0pt;margin-right:0cm;margin-bottom:6.0pt;
                                    margin-left:0cm;text-align:justify;text-indent:1.0cm'>
                                                <span style='font-size: 14.0pt;color:windowtext'>
                                                    - Danh sách người
                                                    có nghĩa vụ kê khai:
                                                </span>
                                            </p>

                                            <table class=MsoNormalTable border=0 cellspacing=0 cellpadding=0 width='100%'
                                                    style='width:100.0%;border-collapse:collapse'>
                                                <tr style='height:38.25pt'>
                                                    <td width='5%' nowrap rowspan=2 style='width:5.96%;border:solid windowtext 1.0pt;
                                            border-bottom:solid black 1.0pt;background:white;padding:0cm 5.4pt 0cm 5.4pt;
                                            height:38.25pt'>
                                                        <p class=MsoNormal align=center style='text-align:center'>
                                                            <b>
                                                                <span style='font-size:10.0pt;color:windowtext'>STT</span>
                                                            </b>
                                                        </p>
                                                    </td>
                                                    <td width='12%' rowspan=2 style='width:12.52%;border-top:solid windowtext 1.0pt;
                                            border-left:none;border-bottom:solid black 1.0pt;border-right:solid windowtext 1.0pt;
                                            background:white;padding:0cm 5.4pt 0cm 5.4pt;height:38.25pt'>
                                                        <p class=MsoNormal align=center style='text-align:center'>
                                                            <b>
                                                                <span style='font-size:10.0pt;color:windowtext'>
                                                                    Hộ
                                                                    tên
                                                                </span>
                                                            </b>
                                                        </p>
                                                    </td>
                                                    <td width='10%' rowspan=2 style='width:10.24%;border-top:solid windowtext 1.0pt;
                                            border-left:none;border-bottom:solid black 1.0pt;border-right:solid windowtext 1.0pt;
                                            background:white;padding:0cm 5.4pt 0cm 5.4pt;height:38.25pt'>
                                                        <p class=MsoNormal align=center style='text-align:center'>
                                                            <b>
                                                                <span style='font-size:10.0pt;color:windowtext'>
                                                                    Ngày
                                                                    tháng năm sinh
                                                                </span>
                                                            </b>
                                                        </p>
                                                    </td>
                                                    <td width='9%' rowspan=2 style='width:9.62%;border-top:solid windowtext 1.0pt;
                                            border-left:none;border-bottom:solid black 1.0pt;border-right:solid windowtext 1.0pt;
                                            background:white;padding:0cm 5.4pt 0cm 5.4pt;height:38.25pt'>
                                                        <p class=MsoNormal align=center style='text-align:center'>
                                                            <b>
                                                                <span style='font-size:10.0pt;color:windowtext'>
                                                                    Chức
                                                                    vụ/ chức danh công tác
                                                                </span>
                                                            </b>
                                                        </p>
                                                    </td>
                                                    <td width='11%' rowspan=2 style='width:11.42%;border-top:solid windowtext 1.0pt;
                                            border-left:none;border-bottom:solid black 1.0pt;border-right:solid windowtext 1.0pt;
                                            background:white;padding:0cm 5.4pt 0cm 5.4pt;height:38.25pt'>
                                                        <p class=MsoNormal align=center style='text-align:center'>
                                                            <b>
                                                                <span style='font-size:10.0pt;color:windowtext'>
                                                                    Cơ
                                                                    quan/đơn vị công tác
                                                                </span>
                                                            </b>
                                                        </p>
                                                    </td>
                                                    <td width='50%' colspan=3 style='width:50.22%;border:solid windowtext 1.0pt;
                                            border-left:none;background:white;padding:0cm 5.4pt 0cm 5.4pt;height:38.25pt'>
                                                        <p class=MsoNormal align=center style='text-align:center'>
                                                            <b>
                                                                <span style='font-size:10.0pt;color:windowtext'>
                                                                    Ngày
                                                                    được tiếp nhận, tuyển dụng, bố trí
                                                                    vào vị trí công tác
                                                                </span>
                                                            </b>
                                                        </p>
                                                    </td>
                                                </tr>
                                                <tr style='height:55.5pt'>
                                                    <td width='16%' style='width:16.1%;border-top:none;border-left:none;
                                            border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
                                            background:white;padding:0cm 5.4pt 0cm 5.4pt;height:55.5pt'>
                                                        <p class=MsoNormal align=center style='text-align:center'>
                                                            <b>
                                                                <span style='font-size:10.0pt;color:windowtext'>
                                                                    Số
                                                                    VB tiếp nhận, tuyển dụng, bố trí vào vị
                                                                    trí công tác
                                                                </span>
                                                            </b>
                                                        </p>
                                                    </td>
                                                    <td width='17%' style='width:17.06%;border-top:none;border-left:none;
                                            border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
                                            background:white;padding:0cm 5.4pt 0cm 5.4pt;height:55.5pt'>
                                                        <p class=MsoNormal align=center style='text-align:center'>
                                                            <b>
                                                                <span style='font-size:10.0pt;color:windowtext'>
                                                                    Ngày
                                                                    VB tiếp nhận, tuyển dụng, bố trí vào vị
                                                                    trí công tác
                                                                </span>
                                                            </b>
                                                        </p>
                                                    </td>
                                                    <td width='17%' style='width:17.06%;border-top:none;border-left:none;
                                                border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
                                                background:white;padding:0cm 5.4pt 0cm 5.4pt;height:55.5pt'>
                                                        <p class=MsoNormal align=center style='text-align:center'>
                                                            <b>
                                                                <span style='font-size:10.0pt;color:windowtext'>
                                                                    Ngày
                                                                    được tiếp nhận, tuyển dụng, bố
                                                                    trí vào vị trí công tác
                                                                </span>
                                                            </b>
                                                        </p>
                                                    </td>
                                                </tr>
                                                <tr style='height:15.0pt'>
                                                    <td width='5%' nowrap style='width:5.96%;border:solid windowtext 1.0pt;
                                            border-top:none;background:white;padding:0cm 5.4pt 0cm 5.4pt;height:15.0pt'>
                                                        <p class=MsoNormal align=center style='text-align:center'>
                                                            <span style='font-size:10.0pt;color:windowtext'>1</span>
                                                        </p>
                                                    </td>
                                                    <td width='12%' style='width:12.52%;border-top:none;border-left:none;
                                            border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
                                            background:white;padding:0cm 5.4pt 0cm 5.4pt;height:15.0pt'>
                                                        <p class=MsoNormal align=center style='text-align:center'>
                                                            <span style='font-size:10.0pt;color:windowtext'>2</span>
                                                        </p>
                                                    </td>
                                                    <td width='10%' nowrap style='width:10.24%;border-top:none;border-left:none;
                                            border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
                                            background:white;padding:0cm 5.4pt 0cm 5.4pt;height:15.0pt'>
                                                        <p class=MsoNormal align=center style='text-align:center'>
                                                            <span style='font-size:10.0pt;color:windowtext'>3</span>
                                                        </p>
                                                    </td>
                                                    <td width='9%' style='width:9.62%;border-top:none;border-left:none;
                                            border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
                                            background:white;padding:0cm 5.4pt 0cm 5.4pt;height:15.0pt'>
                                                        <p class=MsoNormal align=center style='text-align:center'>
                                                            <span style='font-size:10.0pt;color:windowtext'>4</span>
                                                        </p>
                                                    </td>
                                                    <td width='11%' nowrap style='width:11.42%;border-top:none;border-left:none;
                                            border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
                                            background:white;padding:0cm 5.4pt 0cm 5.4pt;height:15.0pt'>
                                                        <p class=MsoNormal align=center style='text-align:center'>
                                                            <span style='font-size:10.0pt;color:windowtext'>5</span>
                                                        </p>
                                                    </td>
                                                    <td width='16%' style='width:16.1%;border-top:none;border-left:none;
                                            border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
                                            background:white;padding:0cm 5.4pt 0cm 5.4pt;height:15.0pt'>
                                                        <p class=MsoNormal align=center style='text-align:center'>
                                                            <span style='font-size:10.0pt;color:windowtext'>6</span>
                                                        </p>
                                                    </td>
                                                    <td width='17%' nowrap style='width:17.06%;border-top:none;border-left:none;
                                            border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
                                            background:white;padding:0cm 5.4pt 0cm 5.4pt;height:15.0pt'>
                                                        <p class=MsoNormal align=center style='text-align:center'>
                                                            <span style='font-size:10.0pt;color:windowtext'>7</span>
                                                        </p>
                                                    </td>
                                                    <td width='17%' style='width:17.06%;border-top:none;border-left:none;
                                            border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
                                            background:white;padding:0cm 5.4pt 0cm 5.4pt;height:15.0pt'>
                                                        <p class=MsoNormal align=center style='text-align:center'>
                                                            <span style='font-size:10.0pt;color:windowtext'>8</span>
                                                        </p>
                                                    </td>
                                                </tr>
                                                <tr style='height:15.0pt'>
                                                    <td width='5%' nowrap style='width:5.96%;border:solid windowtext 1.0pt;
border-top:none;background:white;padding:0cm 5.4pt 0cm 5.4pt;height:15.0pt'>
                                                        <p class=MsoNormal align=center style='text-align:center'>
                                                            <b>
                                                                <span style='font-size:10.0pt;color:windowtext'>&nbsp;</span>
                                                            </b>
                                                        </p>
                                                    </td>
                                                    <td width='12%' style='width:12.52%;border-top:none;border-left:none;
border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
background:white;padding:0cm 5.4pt 0cm 5.4pt;height:15.0pt'>
                                                        <p class=MsoNormal>
                                                            <b>
                                                                <span style='font-size:10.0pt;
color:windowtext'>&nbsp;</span>
                                                            </b>
                                                        </p>
                                                    </td>
                                                    <td width='10%' style='width:10.24%;border-top:none;border-left:none;
border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
background:white;padding:0cm 5.4pt 0cm 5.4pt;height:15.0pt'>
                                                        <p class=MsoNormal align=center style='text-align:center'>
                                                            <b>
                                                                <i>
                                                                    <span style='font-size:10.0pt;color:windowtext'>&nbsp;</span>
                                                                </i>
                                                            </b>
                                                        </p>
                                                    </td>
                                                    <td width='9%' style='width:9.62%;border-top:none;border-left:none;
border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
background:white;padding:0cm 5.4pt 0cm 5.4pt;height:15.0pt'>
                                                        <p class=MsoNormal align=center style='text-align:center'>
                                                            <b>
                                                                <i>
                                                                    <span style='font-size:10.0pt;color:windowtext'>&nbsp;</span>
                                                                </i>
                                                            </b>
                                                        </p>
                                                    </td>
                                                    <td width='11%' style='width:11.42%;border-top:none;border-left:none;
border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
background:white;padding:0cm 5.4pt 0cm 5.4pt;height:15.0pt'>
                                                        <p class=MsoNormal align=center style='text-align:center'>
                                                            <b>
                                                                <i>
                                                                    <span style='font-size:10.0pt;color:windowtext'>&nbsp;</span>
                                                                </i>
                                                            </b>
                                                        </p>
                                                    </td>
                                                    <td width='16%' style='width:16.1%;border-top:none;border-left:none;
border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
background:white;padding:0cm 5.4pt 0cm 5.4pt;height:15.0pt'>
                                                        <p class=MsoNormal>
                                                            <i>
                                                                <span style='font-size:10.0pt;
color:windowtext'>&nbsp;</span>
                                                            </i>
                                                        </p>
                                                    </td>
                                                    <td width='17%' nowrap valign=bottom style='width:17.06%;border-top:none;
border-left:none;border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
padding:0cm 5.4pt 0cm 5.4pt;height:15.0pt'>
                                                        <p class=MsoNormal><span style='font-size:11.0pt;'>&nbsp;</span></p>
                                                    </td>
                                                    <td width='17%' nowrap valign=bottom style='width:17.06%;border-top:none;
border-left:none;border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
padding:0cm 5.4pt 0cm 5.4pt;height:15.0pt'>
                                                        <p class=MsoNormal><span style='font-size:11.0pt;'>&nbsp;</span></p>
                                                    </td>
                                                </tr>
                                                <tr style='height:15.0pt'>
                                                    <td width='5%' nowrap style='width:5.96%;border:solid windowtext 1.0pt;
border-top:none;background:white;padding:0cm 5.4pt 0cm 5.4pt;height:15.0pt'>
                                                        <p class=MsoNormal align=center style='text-align:center'>
                                                            <b>
                                                                <span style='font-size:10.0pt;color:windowtext'>&nbsp;</span>
                                                            </b>
                                                        </p>
                                                    </td>
                                                    <td width='12%' style='width:12.52%;border-top:none;border-left:none;
border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
background:white;padding:0cm 5.4pt 0cm 5.4pt;height:15.0pt'>
                                                        <p class=MsoNormal>
                                                            <span style='font-size:10.0pt;
color:windowtext'>&nbsp;</span>
                                                        </p>
                                                    </td>
                                                    <td width='10%' style='width:10.24%;border-top:none;border-left:none;  border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;  background:white;padding:0cm 5.4pt 0cm 5.4pt;height:15.0pt'>
                                                        <p class=MsoNormal align=center style='text-align:center'>
                                                            <span style='font-size:10.0pt;color:windowtext'>&nbsp;</span>
                                                        </p>
                                                    </td>
                                                    <td width='9%' style='width:9.62%;border-top:none;border-left:none;
border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
background:white;padding:0cm 5.4pt 0cm 5.4pt;height:15.0pt'>
                                                        <p class=MsoNormal align=center style='text-align:center'>
                                                            <span style='font-size:10.0pt;color:windowtext'>&nbsp;</span>
                                                        </p>
                                                    </td>
                                                    <td width='11%' style='width:11.42%;border-top:none;border-left:none;
border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
background:white;padding:0cm 5.4pt 0cm 5.4pt;height:15.0pt'>
                                                        <p class=MsoNormal align=center style='text-align:center'>
                                                            <span style='font-size:10.0pt;color:windowtext'>&nbsp;</span>
                                                        </p>
                                                    </td>
                                                    <td width='16%' style='width:16.1%;border-top:none;border-left:none;
border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
background:white;padding:0cm 5.4pt 0cm 5.4pt;height:15.0pt'>
                                                        <p class=MsoNormal>
                                                            <i>
                                                                <span style='font-size:10.0pt;
color:windowtext'>&nbsp;</span>
                                                            </i>
                                                        </p>
                                                    </td>
                                                    <td width='17%' nowrap valign=bottom style='width:17.06%;border-top:none;
border-left:none;border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
padding:0cm 5.4pt 0cm 5.4pt;height:15.0pt'>
                                                        <p class=MsoNormal><span style='font-size:11.0pt;'>&nbsp;</span></p>
                                                    </td>
                                                    <td width='17%' nowrap valign=bottom style='width:17.06%;border-top:none;
border-left:none;border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
padding:0cm 5.4pt 0cm 5.4pt;height:15.0pt'>
                                                        <p class=MsoNormal><span style='font-size:11.0pt;'>&nbsp;</span></p>
                                                    </td>
                                                </tr>
                                                <tr style='height:15.0pt'>
                                                    <td width='5%' nowrap style='width:5.96%;border:solid windowtext 1.0pt;
border-top:none;background:white;padding:0cm 5.4pt 0cm 5.4pt;height:15.0pt'>
                                                        <p class=MsoNormal align=center style='text-align:center'>
                                                            <b>
                                                                <span style='font-size:10.0pt;color:windowtext'>&nbsp;</span>
                                                            </b>
                                                        </p>
                                                    </td>
                                                    <td width='12%' style='width:12.52%;border-top:none;border-left:none;
border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
background:white;padding:0cm 5.4pt 0cm 5.4pt;height:15.0pt'>
                                                        <p class=MsoNormal>
                                                            <span style='font-size:10.0pt;
color:windowtext'>&nbsp;</span>
                                                        </p>
                                                    </td>
                                                    <td width='10%' style='width:10.24%;border-top:none;border-left:none;
border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
background:white;padding:0cm 5.4pt 0cm 5.4pt;height:15.0pt'>
                                                        <p class=MsoNormal align=center style='text-align:center'>
                                                            <span style='font-size:10.0pt;color:windowtext'>&nbsp;</span>
                                                        </p>
                                                    </td>
                                                    <td width='9%' style='width:9.62%;border-top:none;border-left:none;
border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
background:white;padding:0cm 5.4pt 0cm 5.4pt;height:15.0pt'>
                                                        <p class=MsoNormal align=center style='text-align:center'>
                                                            <span style='font-size:10.0pt;color:windowtext'>&nbsp;</span>
                                                        </p>
                                                    </td>
                                                    <td width='11%' style='width:11.42%;border-top:none;border-left:none;
border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
background:white;padding:0cm 5.4pt 0cm 5.4pt;height:15.0pt'>
                                                        <p class=MsoNormal align=center style='text-align:center'>
                                                            <span style='font-size:10.0pt;color:windowtext'>&nbsp;</span>
                                                        </p>
                                                    </td>
                                                    <td width='16%' style='width:16.1%;border-top:none;border-left:none;
border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
background:white;padding:0cm 5.4pt 0cm 5.4pt;height:15.0pt'>
                                                        <p class=MsoNormal>
                                                            <i>
                                                                <span style='font-size:10.0pt;
color:windowtext'>&nbsp;</span>
                                                            </i>
                                                        </p>
                                                    </td>
                                                    <td width='17%' nowrap valign=bottom style='width:17.06%;border-top:none;
border-left:none;border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
padding:0cm 5.4pt 0cm 5.4pt;height:15.0pt'>
                                                        <p class=MsoNormal><span style='font-size:11.0pt;'>&nbsp;</span></p>
                                                    </td>
                                                    <td width='17%' nowrap valign=bottom style='width:17.06%;border-top:none;
border-left:none;border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
padding:0cm 5.4pt 0cm 5.4pt;height:15.0pt'>
                                                        <p class=MsoNormal><span style='font-size:11.0pt;'>&nbsp;</span></p>
                                                    </td>
                                                </tr>
                                                <tr style='height:15.0pt'>
                                                    <td width='5%' style='width:5.96%;border:solid windowtext 1.0pt;border-top:
none;background:white;padding:0cm 5.4pt 0cm 5.4pt;height:15.0pt'>
                                                        <p class=MsoNormal align=center style='text-align:center'>
                                                            <b>
                                                                <span style='font-size:10.0pt;color:windowtext'>&nbsp;</span>
                                                            </b>
                                                        </p>
                                                    </td>
                                                    <td width='12%' style='width:12.52%;border-top:none;border-left:none;
border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
background:white;padding:0cm 5.4pt 0cm 5.4pt;height:15.0pt'>
                                                        <p class=MsoNormal align=center style='text-align:center'>
                                                            <b>
                                                                <span style='font-size:10.0pt;color:windowtext'>
                                                                    Tổng
                                                                    cộng
                                                                </span>
                                                            </b>
                                                        </p>
                                                    </td>
                                                    <td width='10%' nowrap style='width:10.24%;border-top:none;border-left:none;
border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
background:white;padding:0cm 5.4pt 0cm 5.4pt;height:15.0pt'>
                                                        <p class=MsoNormal align=center style='text-align:center'>
                                                            <b>
                                                                <span style='font-size:11.0pt;color:windowtext'>&nbsp;</span>
                                                            </b>
                                                        </p>
                                                    </td>
                                                    <td width='9%' nowrap style='width:9.62%;border-top:none;border-left:none;
border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
background:white;padding:0cm 5.4pt 0cm 5.4pt;height:15.0pt'>
                                                        <p class=MsoNormal align=center style='text-align:center'>
                                                            <b>
                                                                <span style='font-size:11.0pt;color:windowtext'>&nbsp;</span>
                                                            </b>
                                                        </p>
                                                    </td>
                                                    <td width='11%' nowrap style='width:11.42%;border-top:none;border-left:none;
border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
background:white;padding:0cm 5.4pt 0cm 5.4pt;height:15.0pt'>
                                                        <p class=MsoNormal align=center style='text-align:center'>
                                                            <b>
                                                                <span style='font-size:11.0pt;color:windowtext'>&nbsp;</span>
                                                            </b>
                                                        </p>
                                                    </td>
                                                    <td width='16%' style='width:16.1%;border-top:none;border-left:none;
border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
background:white;padding:0cm 5.4pt 0cm 5.4pt;height:15.0pt'>
                                                        <p class=MsoNormal>
                                                            <span style='font-size:11.0pt;
color:windowtext'>&nbsp;</span>
                                                        </p>
                                                    </td>
                                                    <td width='17%' nowrap valign=bottom style='width:17.06%;border-top:none;
border-left:none;border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
padding:0cm 5.4pt 0cm 5.4pt;height:15.0pt'>
                                                        <p class=MsoNormal><span style='font-size:11.0pt;'>&nbsp;</span></p>
                                                    </td>
                                                    <td width='17%' nowrap valign=bottom style='width:17.06%;border-top:none;
border-left:none;border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
padding:0cm 5.4pt 0cm 5.4pt;height:15.0pt'>
                                                        <p class=MsoNormal><span style='font-size:11.0pt;'>&nbsp;</span></p>
                                                    </td>
                                                </tr>
                                            </table>

                                            <p class=MsoNormal style='margin-top:6.0pt;margin-right:0cm;margin-bottom:6.0pt;
margin-left:0cm;text-align:justify;text-indent:1.0cm'>
                                                <span style='font-size:
14.0pt;color:windowtext'>&nbsp;</span>
                                            </p>

                                            <p class=MsoNormal style='margin-top:6.0pt;margin-right:0cm;margin-bottom:6.0pt;
margin-left:0cm;text-align:justify;text-indent:1.0cm;line-height:110%;
text-autospace:none'>
                                                <span style='font-size:13.0pt;line-height:110%;color:windowtext'>
                                                    - Kết quả kê khai, công
                                                    khai:
                                                </span>
                                            </p>

                                            <table class=MsoNormalTable border=0 cellspacing=0 cellpadding=0 width='100%'
                                                    style='width:100.0%;border-collapse:collapse'>
                                                <tr style='height:25.0pt'>
                                                    <td width='7%' style='width:7.1%;border:solid windowtext 1.0pt;padding:0cm 5.4pt 0cm 5.4pt;
height:25.0pt'>
                                                        <p class=MsoNormal align=center style='text-align:center'>
                                                            <b>
                                                                <span style='font-size:10.0pt;color:windowtext'>STT</span>
                                                            </b>
                                                        </p>
                                                    </td>
                                                    <td width='60%' style='width:60.5%;border:solid windowtext 1.0pt;border-left:
none;padding:0cm 5.4pt 0cm 5.4pt;height:25.0pt'>
                                                        <p class=MsoNormal align=center style='text-align:center'>
                                                            <b>
                                                                <span style='font-size:10.0pt;color:windowtext'>
                                                                    NỘI
                                                                    DUNG
                                                                </span>
                                                            </b>
                                                        </p>
                                                    </td>
                                                    <td width='18%' style='width:18.6%;border:solid windowtext 1.0pt;border-left:
none;padding:0cm 5.4pt 0cm 5.4pt;height:25.0pt'>
                                                        <p class=MsoNormal align=center style='text-align:center'>
                                                            <b>
                                                                <span style='font-size:10.0pt;color:windowtext'>
                                                                    ĐƠN
                                                                    VỊ TÍNH
                                                                </span>
                                                            </b>
                                                        </p>
                                                    </td>
                                                    <td width='13%' style='width:13.82%;border:solid windowtext 1.0pt;border-left:
none;padding:0cm 5.4pt 0cm 5.4pt;height:25.0pt'>
                                                        <p class=MsoNormal align=center style='text-align:center'>
                                                            <b>
                                                                <span style='font-size:10.0pt;color:windowtext'>
                                                                    SỐ
                                                                    LIỆU
                                                                </span>
                                                            </b>
                                                        </p>
                                                    </td>
                                                </tr>
                                                <tr style='height:15.0pt'>
                                                    <td width='7%' style='width:7.1%;border:solid windowtext 1.0pt;border-top:
none;padding:0cm 5.4pt 0cm 5.4pt;height:15.0pt'>
                                                        <p class=MsoNormal align=center style='text-align:center'>
                                                            <b>
                                                                <span style='font-size:10.0pt;color:windowtext'>I</span>
                                                            </b>
                                                        </p>
                                                    </td>
                                                    <td width='60%' style='width:60.5%;border-top:none;border-left:none;
border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
padding:0cm 5.4pt 0cm 5.4pt;height:15.0pt'>
                                                        <p class=MsoNormal style='text-align:justify'>
                                                            <b>
                                                                <span style='font-size:10.0pt;
color:windowtext'>
                                                                    Kê khai tài sản,
                                                                    thu nhập lần đầu
                                                                </span>
                                                            </b>
                                                        </p>
                                                    </td>
                                                    <td width='18%' style='width:18.6%;border-top:none;border-left:none;
border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
padding:0cm 5.4pt 0cm 5.4pt;height:15.0pt'>
                                                        <p class=MsoNormal style='text-align:justify'>
                                                            <span style='font-size:10.0pt;
color:windowtext'>&nbsp;</span>
                                                        </p>
                                                    </td>
                                                    <td width='13%' style='width:13.82%;border-top:none;border-left:none;
border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
padding:0cm 5.4pt 0cm 5.4pt;height:15.0pt'>
                                                        <p class=MsoNormal style='text-align:justify'>
                                                            <span style='font-size:10.0pt;
color:windowtext'>&nbsp;</span>
                                                        </p>
                                                    </td>
                                                </tr>
                                                <tr style='height:76.5pt'>
                                                    <td width='7%' style='width:7.1%;border:solid windowtext 1.0pt;border-top:
none;padding:0cm 5.4pt 0cm 5.4pt;height:76.5pt'>
                                                        <p class=MsoNormal align=center style='text-align:center'>
                                                            <span style='font-size:10.0pt;color:windowtext'>1</span>
                                                        </p>
                                                    </td>
                                                    <td width='60%' style='width:60.5%;border-top:none;border-left:none;
border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
padding:0cm 5.4pt 0cm 5.4pt;height:76.5pt'>
                                                        <p class=MsoNormal style='text-align:justify'>
                                                            <span style='font-size:10.0pt;
color:windowtext'>
                                                                Tổng số
                                                                cơ quan, tổ chức, đơn vị phải tổ
                                                                chức thực hiện việc kê khai TSTN (nếu chỉ
                                                                có 01 đơn vị thì ghi 01, trường hợp có các
                                                                đơn vị trực thuộc thì ghi tổng số
                                                                đơn vị trực thuộc đồng thời
                                                                liệt kê cụ thể tên các đơn vị trực
                                                                thuộc)
                                                            </span>
                                                        </p>
                                                    </td>
                                                    <td width='18%' style='width:18.6%;border-top:none;border-left:none;
border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
padding:0cm 5.4pt 0cm 5.4pt;height:76.5pt'>
                                                        <p class=MsoNormal align=center style='text-align:center'>
                                                            <span style='font-size:10.0pt;color:windowtext'>
                                                                CQ,
                                                                TC, ĐV
                                                            </span>
                                                        </p>
                                                    </td>
                                                    <td width='13%' style='width:13.82%;border-top:none;border-left:none;
border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
padding:0cm 5.4pt 0cm 5.4pt;height:76.5pt'>
                                                        <p class=MsoNormal style='text-align:justify'>
                                                            <span style='font-size:10.0pt;
color:windowtext'>&nbsp;</span>
                                                        </p>
                                                    </td>
                                                </tr>
                                                <tr style='height:38.25pt'>
                                                    <td width='7%' style='width:7.1%;border:solid windowtext 1.0pt;border-top:
none;padding:0cm 5.4pt 0cm 5.4pt;height:38.25pt'>
                                                        <p class=MsoNormal align=center style='text-align:center'>
                                                            <span style='font-size:10.0pt;color:windowtext'>2</span>
                                                        </p>
                                                    </td>
                                                    <td width='60%' style='width:60.5%;border-top:none;border-left:none;
border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
padding:0cm 5.4pt 0cm 5.4pt;height:38.25pt'>
                                                        <p class=MsoNormal style='text-align:justify'>
                                                            <span style='font-size:10.0pt;
color:windowtext'>
                                                                Số cơ quan,
                                                                tổ chức, đơn vị đã tổ chức
                                                                thực hiện việc kê khai tài sản, thu nhập (ghi
                                                                tương tự mục 1)
                                                            </span>
                                                        </p>
                                                    </td>
                                                    <td width='18%' style='width:18.6%;border-top:none;border-left:none;
border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
padding:0cm 5.4pt 0cm 5.4pt;height:38.25pt'>
                                                        <p class=MsoNormal align=center style='text-align:center'>
                                                            <span style='font-size:10.0pt;color:windowtext'>
                                                                CQ,
                                                                TC, ĐV
                                                            </span>
                                                        </p>
                                                    </td>
                                                    <td width='13%' style='width:13.82%;border-top:none;border-left:none;
border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
padding:0cm 5.4pt 0cm 5.4pt;height:38.25pt'>
                                                        <p class=MsoNormal style='text-align:justify'>
                                                            <span style='font-size:10.0pt;
color:windowtext'>&nbsp;</span>
                                                        </p>
                                                    </td>
                                                </tr>
                                                <tr style='height:25.5pt'>
                                                    <td width='7%' style='width:7.1%;border:solid windowtext 1.0pt;border-top:
none;padding:0cm 5.4pt 0cm 5.4pt;height:25.5pt'>
                                                        <p class=MsoNormal align=center style='text-align:center'>
                                                            <span style='font-size:10.0pt;color:windowtext'>&nbsp;</span>
                                                        </p>
                                                    </td>
                                                    <td width='60%' style='width:60.5%;border-top:none;border-left:none;
border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
padding:0cm 5.4pt 0cm 5.4pt;height:25.5pt'>
                                                        <p class=MsoNormal style='text-align:justify'>
                                                            <span style='font-size:10.0pt;
color:windowtext'>
                                                                Tỉ lệ so
                                                                với tổng số cơ quan, tổ chức,
                                                                đơn vị
                                                            </span>
                                                        </p>
                                                    </td>
                                                    <td width='18%' style='width:18.6%;border-top:none;border-left:none;
border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
padding:0cm 5.4pt 0cm 5.4pt;height:25.5pt'>
                                                        <p class=MsoNormal align=center style='text-align:center'>
                                                            <span style='font-size:10.0pt;color:windowtext'>%</span>
                                                        </p>
                                                    </td>
                                                    <td width='13%' style='width:13.82%;border-top:none;border-left:none;
border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
padding:0cm 5.4pt 0cm 5.4pt;height:25.5pt'>
                                                        <p class=MsoNormal style='text-align:justify'>
                                                            <span style='font-size:10.0pt;
color:windowtext'>&nbsp;</span>
                                                        </p>
                                                    </td>
                                                </tr>
                                                <tr style='height:38.25pt'>
                                                    <td width='7%' style='width:7.1%;border:solid windowtext 1.0pt;border-top:
none;padding:0cm 5.4pt 0cm 5.4pt;height:38.25pt'>
                                                        <p class=MsoNormal align=center style='text-align:center'>
                                                            <span style='font-size:10.0pt;color:windowtext'>3</span>
                                                        </p>
                                                    </td>
                                                    <td width='60%' style='width:60.5%;border-top:none;border-left:none;
border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
padding:0cm 5.4pt 0cm 5.4pt;height:38.25pt'>
                                                        <p class=MsoNormal style='text-align:justify'>
                                                            <span style='font-size:10.0pt;
color:windowtext'>
                                                                Số cơ quan,
                                                                tổ chức, đơn vị chưa thực hiện
                                                                hoặc chưa được tổng hợp kết
                                                                quả trong báo cáo này (ghi tương tự mục 1, 2)
                                                            </span>
                                                        </p>
                                                    </td>
                                                    <td width='18%' style='width:18.6%;border-top:none;border-left:none;
border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
padding:0cm 5.4pt 0cm 5.4pt;height:38.25pt'>
                                                        <p class=MsoNormal align=center style='text-align:center'>
                                                            <span style='font-size:10.0pt;color:windowtext'>
                                                                CQ,
                                                                TC, ĐV
                                                            </span>
                                                        </p>
                                                    </td>
                                                    <td width='13%' style='width:13.82%;border-top:none;border-left:none;
border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
padding:0cm 5.4pt 0cm 5.4pt;height:38.25pt'>
                                                        <p class=MsoNormal style='text-align:justify'>
                                                            <span style='font-size:10.0pt;
color:windowtext'>&nbsp;</span>
                                                        </p>
                                                    </td>
                                                </tr>
                                                <tr style='height:25.5pt'>
                                                    <td width='7%' style='width:7.1%;border:solid windowtext 1.0pt;border-top:
none;padding:0cm 5.4pt 0cm 5.4pt;height:25.5pt'>
                                                        <p class=MsoNormal align=center style='text-align:center'>
                                                            <span style='font-size:10.0pt;color:windowtext'>&nbsp;</span>
                                                        </p>
                                                    </td>
                                                    <td width='60%' style='width:60.5%;border-top:none;border-left:none;
border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
padding:0cm 5.4pt 0cm 5.4pt;height:25.5pt'>
                                                        <p class=MsoNormal style='text-align:justify'>
                                                            <span style='font-size:10.0pt;
color:windowtext'>
                                                                Tỉ lệ so
                                                                với tổng số cơ quan, tổ chức,
                                                                đơn vị
                                                            </span>
                                                        </p>
                                                    </td>
                                                    <td width='18%' style='width:18.6%;border-top:none;border-left:none;
border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
padding:0cm 5.4pt 0cm 5.4pt;height:25.5pt'>
                                                        <p class=MsoNormal align=center style='text-align:center'>
                                                            <span style='font-size:10.0pt;color:windowtext'>%</span>
                                                        </p>
                                                    </td>
                                                    <td width='13%' style='width:13.82%;border-top:none;border-left:none;
border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
padding:0cm 5.4pt 0cm 5.4pt;height:25.5pt'>
                                                        <p class=MsoNormal style='text-align:justify'>
                                                            <span style='font-size:10.0pt;
color:windowtext'>&nbsp;</span>
                                                        </p>
                                                    </td>
                                                </tr>
                                                <tr style='height:25.5pt'>
                                                    <td width='7%' style='width:7.1%;border:solid windowtext 1.0pt;border-top:
none;padding:0cm 5.4pt 0cm 5.4pt;height:25.5pt'>
                                                        <p class=MsoNormal align=center style='text-align:center'>
                                                            <span style='font-size:10.0pt;color:windowtext'>4</span>
                                                        </p>
                                                    </td>
                                                    <td width='60%' style='width:60.5%;border-top:none;border-left:none;
border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
padding:0cm 5.4pt 0cm 5.4pt;height:25.5pt'>
                                                        <p class=MsoNormal style='text-align:justify'>
                                                            <span style='font-size:10.0pt;
color:windowtext'>
                                                                Số
                                                                người phải kê khai tài sản, thu nhập lần
                                                                đầu
                                                            </span>
                                                        </p>
                                                    </td>
                                                    <td width='18%' style='width:18.6%;border-top:none;border-left:none;
border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
padding:0cm 5.4pt 0cm 5.4pt;height:25.5pt'>
                                                        <p class=MsoNormal align=center style='text-align:center'>
                                                            <span style='font-size:10.0pt;color:windowtext'>Người</span>
                                                        </p>
                                                    </td>
                                                    <td width='13%' style='width:13.82%;border-top:none;border-left:none;
border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
padding:0cm 5.4pt 0cm 5.4pt;height:25.5pt'>
                                                        <p class=MsoNormal style='text-align:justify'>
                                                            <span style='font-size:10.0pt;
color:windowtext'>&nbsp;</span>
                                                        </p>
                                                    </td>
                                                </tr>
                                                <tr style='height:15.0pt'>
                                                    <td width='7%' style='width:7.1%;border:solid windowtext 1.0pt;border-top:
none;padding:0cm 5.4pt 0cm 5.4pt;height:15.0pt'>
                                                        <p class=MsoNormal align=center style='text-align:center'>
                                                            <span style='font-size:10.0pt;color:windowtext'>5</span>
                                                        </p>
                                                    </td>
                                                    <td width='60%' style='width:60.5%;border-top:none;border-left:none;
border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
padding:0cm 5.4pt 0cm 5.4pt;height:15.0pt'>
                                                        <p class=MsoNormal style='text-align:justify'>
                                                            <span style='font-size:10.0pt;
color:windowtext'>
                                                                Số
                                                                người đã kê khai tài sản, thu nhập lần
                                                                đầu
                                                            </span>
                                                        </p>
                                                    </td>
                                                    <td width='18%' style='width:18.6%;border-top:none;border-left:none;
border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
padding:0cm 5.4pt 0cm 5.4pt;height:15.0pt'>
                                                        <p class=MsoNormal align=center style='text-align:center'>
                                                            <span style='font-size:10.0pt;color:windowtext'>Người</span>
                                                        </p>
                                                    </td>
                                                    <td width='13%' style='width:13.82%;border-top:none;border-left:none;
border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
padding:0cm 5.4pt 0cm 5.4pt;height:15.0pt'>
                                                        <p class=MsoNormal style='text-align:justify'>
                                                            <span style='font-size:10.0pt;
color:windowtext'>&nbsp;</span>
                                                        </p>
                                                    </td>
                                                </tr>
                                                <tr style='height:25.5pt'>
                                                    <td width='7%' style='width:7.1%;border:solid windowtext 1.0pt;border-top:
none;padding:0cm 5.4pt 0cm 5.4pt;height:25.5pt'>
                                                        <p class=MsoNormal align=center style='text-align:center'>
                                                            <b>
                                                                <span style='font-size:10.0pt;color:windowtext'>II</span>
                                                            </b>
                                                        </p>
                                                    </td>
                                                    <td width='60%' style='width:60.5%;border-top:none;border-left:none;
border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
padding:0cm 5.4pt 0cm 5.4pt;height:25.5pt'>
                                                        <p class=MsoNormal style='text-align:justify'>
                                                            <b>
                                                                <span style='font-size:10.0pt;
color:windowtext'>
                                                                    Công khai Bản kê
                                                                    khai tài sản, thu nhập lần đầu
                                                                </span>
                                                            </b>
                                                        </p>
                                                    </td>
                                                    <td width='18%' style='width:18.6%;border-top:none;border-left:none;
border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
padding:0cm 5.4pt 0cm 5.4pt;height:25.5pt'>
                                                        <p class=MsoNormal align=center style='text-align:center'>
                                                            <span style='font-size:10.0pt;color:windowtext'>&nbsp;</span>
                                                        </p>
                                                    </td>
                                                    <td width='13%' style='width:13.82%;border-top:none;border-left:none;
border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
padding:0cm 5.4pt 0cm 5.4pt;height:25.5pt'>
                                                        <p class=MsoNormal style='text-align:justify'>
                                                            <span style='font-size:10.0pt;
color:windowtext'>&nbsp;</span>
                                                        </p>
                                                    </td>
                                                </tr>
                                                <tr style='height:38.25pt'>
                                                    <td width='7%' style='width:7.1%;border:solid windowtext 1.0pt;border-top:
none;padding:0cm 5.4pt 0cm 5.4pt;height:38.25pt'>
                                                        <p class=MsoNormal align=center style='text-align:center'>
                                                            <span style='font-size:10.0pt;color:windowtext'>1</span>
                                                        </p>
                                                    </td>
                                                    <td width='60%' style='width:60.5%;border-top:none;border-left:none;
border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
padding:0cm 5.4pt 0cm 5.4pt;height:38.25pt'>
                                                        <p class=MsoNormal style='text-align:justify'>
                                                            <span style='font-size:10.0pt;
color:windowtext'>
                                                                Số cơ quan,
                                                                tổ chức, đơn vị đã tổ chức
                                                                thực hiện việc công khai bản kê khai tài sản,
                                                                thu nhập
                                                            </span>
                                                        </p>
                                                    </td>
                                                    <td width='18%' style='width:18.6%;border-top:none;border-left:none;
border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
padding:0cm 5.4pt 0cm 5.4pt;height:38.25pt'>
                                                        <p class=MsoNormal align=center style='text-align:center'>
                                                            <span style='font-size:10.0pt;color:windowtext'>
                                                                CQ,
                                                                TC, ĐV
                                                            </span>
                                                        </p>
                                                    </td>
                                                    <td width='13%' style='width:13.82%;border-top:none;border-left:none;
border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
padding:0cm 5.4pt 0cm 5.4pt;height:38.25pt'>
                                                        <p class=MsoNormal style='text-align:justify'>
                                                            <span style='font-size:10.0pt;
color:windowtext'>&nbsp;</span>
                                                        </p>
                                                    </td>
                                                </tr>
                                                <tr style='height:25.5pt'>
                                                    <td width='7%' style='width:7.1%;border:solid windowtext 1.0pt;border-top:
none;padding:0cm 5.4pt 0cm 5.4pt;height:25.5pt'>
                                                        <p class=MsoNormal align=center style='text-align:center'>
                                                            <span style='font-size:10.0pt;color:windowtext'>&nbsp;</span>
                                                        </p>
                                                    </td>
                                                    <td width='60%' style='width:60.5%;border-top:none;border-left:none;
border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
padding:0cm 5.4pt 0cm 5.4pt;height:25.5pt'>
                                                        <p class=MsoNormal style='text-align:justify'>
                                                            <span style='font-size:10.0pt;
color:windowtext'>
                                                                Tỉ lệ so
                                                                với tổng số cơ quan, tổ chức,
                                                                đơn vị
                                                            </span>
                                                        </p>
                                                    </td>
                                                    <td width='18%' style='width:18.6%;border-top:none;border-left:none;
border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
padding:0cm 5.4pt 0cm 5.4pt;height:25.5pt'>
                                                        <p class=MsoNormal align=center style='text-align:center'>
                                                            <span style='font-size:10.0pt;color:windowtext'>%</span>
                                                        </p>
                                                    </td>
                                                    <td width='13%' style='width:13.82%;border-top:none;border-left:none;
border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
padding:0cm 5.4pt 0cm 5.4pt;height:25.5pt'>
                                                        <p class=MsoNormal style='text-align:justify'>
                                                            <span style='font-size:10.0pt;
color:windowtext'>&nbsp;</span>
                                                        </p>
                                                    </td>
                                                </tr>
                                                <tr style='height:38.25pt'>
                                                    <td width='7%' style='width:7.1%;border:solid windowtext 1.0pt;border-top:
none;padding:0cm 5.4pt 0cm 5.4pt;height:38.25pt'>
                                                        <p class=MsoNormal align=center style='text-align:center'>
                                                            <span style='font-size:10.0pt;color:windowtext'>2</span>
                                                        </p>
                                                    </td>
                                                    <td width='60%' style='width:60.5%;border-top:none;border-left:none;
border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
padding:0cm 5.4pt 0cm 5.4pt;height:38.25pt'>
                                                        <p class=MsoNormal style='text-align:justify'>
                                                            <span style='font-size:10.0pt;
color:windowtext'>
                                                                Số cơ quan,
                                                                tổ chức, đơn vị chưa thực hiện
                                                                hoặc chưa được tổng hợp kết
                                                                quả trong báo cáo này
                                                            </span>
                                                        </p>
                                                    </td>
                                                    <td width='18%' style='width:18.6%;border-top:none;border-left:none;
border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
padding:0cm 5.4pt 0cm 5.4pt;height:38.25pt'>
                                                        <p class=MsoNormal align=center style='text-align:center'>
                                                            <span style='font-size:10.0pt;color:windowtext'>CQTCĐV</span>
                                                        </p>
                                                    </td>
                                                    <td width='13%' style='width:13.82%;border-top:none;border-left:none;
border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
padding:0cm 5.4pt 0cm 5.4pt;height:38.25pt'>
                                                        <p class=MsoNormal style='text-align:justify'>
                                                            <span style='font-size:10.0pt;
color:windowtext'>&nbsp;</span>
                                                        </p>
                                                    </td>
                                                </tr>
                                                <tr style='height:25.5pt'>
                                                    <td width='7%' style='width:7.1%;border:solid windowtext 1.0pt;border-top:
none;padding:0cm 5.4pt 0cm 5.4pt;height:25.5pt'>
                                                        <p class=MsoNormal align=center style='text-align:center'>
                                                            <span style='font-size:10.0pt;color:windowtext'>&nbsp;</span>
                                                        </p>
                                                    </td>
                                                    <td width='60%' style='width:60.5%;border-top:none;border-left:none;
border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
padding:0cm 5.4pt 0cm 5.4pt;height:25.5pt'>
                                                        <p class=MsoNormal style='text-align:justify'>
                                                            <span style='font-size:10.0pt;
color:windowtext'>
                                                                Tỉ lệ so
                                                                với tổng số cơ quan, tổ chức,
                                                                đơn vị
                                                            </span>
                                                        </p>
                                                    </td>
                                                    <td width='18%' style='width:18.6%;border-top:none;border-left:none;
border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
padding:0cm 5.4pt 0cm 5.4pt;height:25.5pt'>
                                                        <p class=MsoNormal align=center style='text-align:center'>
                                                            <span style='font-size:10.0pt;color:windowtext'>%</span>
                                                        </p>
                                                    </td>
                                                    <td width='13%' style='width:13.82%;border-top:none;border-left:none;
border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
padding:0cm 5.4pt 0cm 5.4pt;height:25.5pt'>
                                                        <p class=MsoNormal style='text-align:justify'>
                                                            <span style='font-size:10.0pt;
color:windowtext'>&nbsp;</span>
                                                        </p>
                                                    </td>
                                                </tr>
                                                <tr style='height:15.0pt'>
                                                    <td width='7%' style='width:7.1%;border:solid windowtext 1.0pt;border-top:
none;padding:0cm 5.4pt 0cm 5.4pt;height:15.0pt'>
                                                        <p class=MsoNormal align=center style='text-align:center'>
                                                            <span style='font-size:10.0pt;color:windowtext'>3</span>
                                                        </p>
                                                    </td>
                                                    <td width='60%' style='width:60.5%;border-top:none;border-left:none;
border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
padding:0cm 5.4pt 0cm 5.4pt;height:15.0pt'>
                                                        <p class=MsoNormal style='text-align:justify'>
                                                            <span style='font-size:10.0pt;
color:windowtext'>
                                                                Tổng số
                                                                bản kê khai
                                                            </span>
                                                        </p>
                                                    </td>
                                                    <td width='18%' style='width:18.6%;border-top:none;border-left:none;
border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
padding:0cm 5.4pt 0cm 5.4pt;height:15.0pt'>
                                                        <p class=MsoNormal align=center style='text-align:center'>
                                                            <span style='font-size:10.0pt;color:windowtext'>
                                                                Bản
                                                                kê khai
                                                            </span>
                                                        </p>
                                                    </td>
                                                    <td width='13%' style='width:13.82%;border-top:none;border-left:none;
border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
padding:0cm 5.4pt 0cm 5.4pt;height:15.0pt'>
                                                        <p class=MsoNormal style='text-align:justify'>
                                                            <span style='font-size:10.0pt;
color:windowtext'>&nbsp;</span>
                                                        </p>
                                                    </td>
                                                </tr>
                                                <tr style='height:15.0pt'>
                                                    <td width='7%' style='width:7.1%;border:solid windowtext 1.0pt;border-top:
none;padding:0cm 5.4pt 0cm 5.4pt;height:15.0pt'>
                                                        <p class=MsoNormal align=center style='text-align:center'>
                                                            <span style='font-size:10.0pt;color:windowtext'>4</span>
                                                        </p>
                                                    </td>
                                                    <td width='60%' style='width:60.5%;border-top:none;border-left:none;
border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
padding:0cm 5.4pt 0cm 5.4pt;height:15.0pt'>
                                                        <p class=MsoNormal style='text-align:justify'>
                                                            <span style='font-size:10.0pt;
color:windowtext'>
                                                                Số bản kê
                                                                khai đã được công khai
                                                            </span>
                                                        </p>
                                                    </td>
                                                    <td width='18%' style='width:18.6%;border-top:none;border-left:none;
border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
padding:0cm 5.4pt 0cm 5.4pt;height:15.0pt'>
                                                        <p class=MsoNormal align=center style='text-align:center'>
                                                            <span style='font-size:10.0pt;color:windowtext'>
                                                                Bản
                                                                kê khai
                                                            </span>
                                                        </p>
                                                    </td>
                                                    <td width='13%' style='width:13.82%;border-top:none;border-left:none;
border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
padding:0cm 5.4pt 0cm 5.4pt;height:15.0pt'>
                                                        <p class=MsoNormal style='text-align:justify'>
                                                            <span style='font-size:10.0pt;
color:windowtext'>&nbsp;</span>
                                                        </p>
                                                    </td>
                                                </tr>
                                                <tr style='height:15.0pt'>
                                                    <td width='7%' style='width:7.1%;border:solid windowtext 1.0pt;border-top:
none;padding:0cm 5.4pt 0cm 5.4pt;height:15.0pt'>
                                                        <p class=MsoNormal align=center style='text-align:center'>
                                                            <span style='font-size:10.0pt;color:windowtext'>&nbsp;</span>
                                                        </p>
                                                    </td>
                                                    <td width='60%' style='width:60.5%;border-top:none;border-left:none;
border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
padding:0cm 5.4pt 0cm 5.4pt;height:15.0pt'>
                                                        <p class=MsoNormal style='text-align:justify'>
                                                            <span style='font-size:10.0pt;
color:windowtext'>
                                                                Tỉ lệ so
                                                                với tổng số bản kê khai
                                                            </span>
                                                        </p>
                                                    </td>
                                                    <td width='18%' style='width:18.6%;border-top:none;border-left:none;
border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
padding:0cm 5.4pt 0cm 5.4pt;height:15.0pt'>
                                                        <p class=MsoNormal align=center style='text-align:center'>
                                                            <span style='font-size:10.0pt;color:windowtext'>%</span>
                                                        </p>
                                                    </td>
                                                    <td width='13%' style='width:13.82%;border-top:none;border-left:none;
border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
padding:0cm 5.4pt 0cm 5.4pt;height:15.0pt'>
                                                        <p class=MsoNormal style='text-align:justify'>
                                                            <span style='font-size:10.0pt;
color:windowtext'>&nbsp;</span>
                                                        </p>
                                                    </td>
                                                </tr>
                                                <tr style='height:25.5pt'>
                                                    <td width='7%' style='width:7.1%;border:solid windowtext 1.0pt;border-top:
none;padding:0cm 5.4pt 0cm 5.4pt;height:25.5pt'>
                                                        <p class=MsoNormal align=center style='text-align:center'>
                                                            <span style='font-size:10.0pt;color:windowtext'>5</span>
                                                        </p>
                                                    </td>
                                                    <td width='60%' style='width:60.5%;border-top:none;border-left:none;
border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
padding:0cm 5.4pt 0cm 5.4pt;height:25.5pt'>
                                                        <p class=MsoNormal style='text-align:justify'>
                                                            <span style='font-size:10.0pt;
color:windowtext'>
                                                                Số bản kê
                                                                khai đã được công khai theo hình thức niêm
                                                                yết
                                                            </span>
                                                        </p>
                                                    </td>
                                                    <td width='18%' style='width:18.6%;border-top:none;border-left:none;
border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
padding:0cm 5.4pt 0cm 5.4pt;height:25.5pt'>
                                                        <p class=MsoNormal align=center style='text-align:center'>
                                                            <span style='font-size:10.0pt;color:windowtext'>
                                                                Bản
                                                                kê khai
                                                            </span>
                                                        </p>
                                                    </td>
                                                    <td width='13%' style='width:13.82%;border-top:none;border-left:none;
border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
padding:0cm 5.4pt 0cm 5.4pt;height:25.5pt'>
                                                        <p class=MsoNormal style='text-align:justify'>
                                                            <span style='font-size:10.0pt;
color:windowtext'>&nbsp;</span>
                                                        </p>
                                                    </td>
                                                </tr>
                                                <tr style='height:25.5pt'>
                                                    <td width='7%' style='width:7.1%;border:solid windowtext 1.0pt;border-top:
none;padding:0cm 5.4pt 0cm 5.4pt;height:25.5pt'>
                                                        <p class=MsoNormal align=center style='text-align:center'>
                                                            <span style='font-size:10.0pt;color:windowtext'>&nbsp;</span>
                                                        </p>
                                                    </td>
                                                    <td width='60%' style='width:60.5%;border-top:none;border-left:none;
border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
padding:0cm 5.4pt 0cm 5.4pt;height:25.5pt'>
                                                        <p class=MsoNormal style='text-align:justify'>
                                                            <span style='font-size:10.0pt;
color:windowtext'>
                                                                Tỉ lệ so
                                                                với số bản kê khai đã được công khai
                                                            </span>
                                                        </p>
                                                    </td>
                                                    <td width='18%' style='width:18.6%;border-top:none;border-left:none;
border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
padding:0cm 5.4pt 0cm 5.4pt;height:25.5pt'>
                                                        <p class=MsoNormal align=center style='text-align:center'>
                                                            <span style='font-size:10.0pt;color:windowtext'>%</span>
                                                        </p>
                                                    </td>
                                                    <td width='13%' style='width:13.82%;border-top:none;border-left:none;
border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
padding:0cm 5.4pt 0cm 5.4pt;height:25.5pt'>
                                                        <p class=MsoNormal style='text-align:justify'>
                                                            <span style='font-size:10.0pt;
color:windowtext'>&nbsp;</span>
                                                        </p>
                                                    </td>
                                                </tr>
                                                <tr style='height:25.5pt'>
                                                    <td width='7%' style='width:7.1%;border:solid windowtext 1.0pt;border-top:
none;padding:0cm 5.4pt 0cm 5.4pt;height:25.5pt'>
                                                        <p class=MsoNormal align=center style='text-align:center'>
                                                            <span style='font-size:10.0pt;color:windowtext'>6</span>
                                                        </p>
                                                    </td>
                                                    <td width='60%' style='width:60.5%;border-top:none;border-left:none;
border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
padding:0cm 5.4pt 0cm 5.4pt;height:25.5pt'>
                                                        <p class=MsoNormal style='text-align:justify'>
                                                            <span style='font-size:10.0pt;
color:windowtext'>
                                                                Số bản kê
                                                                khai đã công khai theo hình thức công bố tại
                                                                cuộc hộp
                                                            </span>
                                                        </p>
                                                    </td>
                                                    <td width='18%' style='width:18.6%;border-top:none;border-left:none;
border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
padding:0cm 5.4pt 0cm 5.4pt;height:25.5pt'>
                                                        <p class=MsoNormal align=center style='text-align:center'>
                                                            <span style='font-size:10.0pt;color:windowtext'>
                                                                Bản
                                                                kê khai
                                                            </span>
                                                        </p>
                                                    </td>
                                                    <td width='13%' style='width:13.82%;border-top:none;border-left:none;
border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
padding:0cm 5.4pt 0cm 5.4pt;height:25.5pt'>
                                                        <p class=MsoNormal style='text-align:justify'>
                                                            <span style='font-size:10.0pt;
color:windowtext'>&nbsp;</span>
                                                        </p>
                                                    </td>
                                                </tr>
                                                <tr style='height:25.5pt'>
                                                    <td width='7%' style='width:7.1%;border:solid windowtext 1.0pt;border-top:
none;padding:0cm 5.4pt 0cm 5.4pt;height:25.5pt'>
                                                        <p class=MsoNormal align=center style='text-align:center'>
                                                            <span style='font-size:10.0pt;color:windowtext'>&nbsp;</span>
                                                        </p>
                                                    </td>
                                                    <td width='60%' style='width:60.5%;border-top:none;border-left:none;
border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
padding:0cm 5.4pt 0cm 5.4pt;height:25.5pt'>
                                                        <p class=MsoNormal style='text-align:justify'>
                                                            <span style='font-size:10.0pt;
color:windowtext'>
                                                                Tỉ lệ so
                                                                với số bản kê khai đã được công khai
                                                            </span>
                                                        </p>
                                                    </td>
                                                    <td width='18%' style='width:18.6%;border-top:none;border-left:none;
border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
padding:0cm 5.4pt 0cm 5.4pt;height:25.5pt'>
                                                        <p class=MsoNormal align=center style='text-align:center'>
                                                            <span style='font-size:10.0pt;color:windowtext'>%</span>
                                                        </p>
                                                    </td>
                                                    <td width='13%' style='width:13.82%;border-top:none;border-left:none;
border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
padding:0cm 5.4pt 0cm 5.4pt;height:25.5pt'>
                                                        <p class=MsoNormal style='text-align:justify'>
                                                            <span style='font-size:10.0pt;
color:windowtext'>&nbsp;</span>
                                                        </p>
                                                    </td>
                                                </tr>
                                            </table>

                                            <p class=MsoNormal style='margin-top:6.0pt;margin-right:0cm;margin-bottom:6.0pt;
margin-left:0cm;text-align:justify;text-indent:1.0cm;line-height:110%;
text-autospace:none'>
                                                <span style='font-size:13.0pt;line-height:110%;color:windowtext'>&nbsp;</span>
                                            </p>

                                            <p class=MsoNormal style='margin-top:6.0pt;margin-right:0cm;margin-bottom:6.0pt;
margin-left:0cm;text-align:justify;text-indent:1.0cm;line-height:110%;
text-autospace:none'>
                                                <b>
                                                    <i>
                                                        <span style='font-size:13.0pt;line-height:110%;
color:windowtext'>
                                                            * Đề
                                                            nghị đơn vị cung cấp
                                                        </span>
                                                    </i>
                                                </b><span style='font-size:13.0pt;line-height:110%;
color:windowtext'>: </span><span style='font-size:13.0pt;line-height:110%;
'>
                                                    Biên bản cuộc hộp công
                                                    khai hoặc biên bản niêm yết công khai; Các bản kê khai
                                                    tài sản, thu nhập của người có nghĩa vụ
                                                    kê khai do Thanh tra tỉnh kiểm soát.
                                                </span>
                                            </p>

                                            <p class=MsoNormal style='margin-top:6.0pt;margin-right:0cm;margin-bottom:6.0pt;
margin-left:0cm;text-align:justify;text-indent:1.0cm'>
                                                <b>
                                                    <span style='font-size:13.0pt;color:windowtext'>
                                                        3.
                                                        Đánh giá và kiến nghị
                                                    </span>
                                                </b>
                                            </p>

                                            <p class=MsoNormal style='margin-top:6.0pt;margin-right:0cm;margin-bottom:6.0pt;
margin-left:0cm;text-align:justify;text-indent:1.0cm'>
                                                <span style='font-size:
14.0pt;color:windowtext'>
                                                    - Các mặt
                                                    thuận lợi, khó khăn, vướng mắc khi
                                                    triển khai công tác kê khai tài sản, thu nhập trong cơ
                                                    quan, tổ chức, đơn vị mình; giải pháp
                                                    để chủ động hoặc đề xuất
                                                    giải pháp khắc phục khó khăn, vướng
                                                    mắc.
                                                </span>
                                            </p>

                                            <p class=MsoNormal style='margin-top:6.0pt;margin-right:0cm;margin-bottom:6.0pt;
margin-left:0cm;text-align:justify;text-indent:1.0cm'>
                                                <span style='font-size:
14.0pt;color:windowtext'>
                                                    - Những
                                                    nội dung quy định cần hướng d&#7851;n
                                                    cụ thể hơn.
                                                </span>
                                            </p>

                                            <p class=MsoNormal style='margin-top:6.0pt;margin-right:0cm;margin-bottom:6.0pt;
margin-left:0cm;text-align:justify;text-indent:1.0cm'>
                                                <span style='font-size:
14.0pt;color:windowtext'>
                                                    - Các ý kiến
                                                    khác./.
                                                </span>
                                            </p>

                                            <p class=MsoNormal style='margin-top:6.0pt;margin-right:0cm;margin-bottom:6.0pt;
margin-left:0cm;text-align:justify;text-indent:1.0cm;line-height:16.0pt'>
                                                <span style='font-size:13.0pt;color:windowtext'>&nbsp;</span>
                                            </p>

                                            <table class=MsoNormalTable border=1 cellspacing=0 cellpadding=0 width='100%'
                                                    style='width:100.0%;border-collapse:collapse;border:none'>
                                                <tr>
                                                    <td width='46%' valign=top style='width:46.96%;border:none;padding:0cm 5.4pt 0cm 5.4pt'>
                                                        <p class=MsoNormal>
                                                            <b>
                                                                <i>
                                                                    <span lang=VI style='font-size:13.0pt;color:windowtext'>Nơi nhận:</span>
                                                                </i>
                                                            </b>
                                                        </p>
                                                        <p class=MsoNormal>
                                                            <span style='font-size:13.0pt;
color:windowtext'>&nbsp;</span>
                                                        </p>
                                                    </td>
                                                    <td width='53%' valign=top style='width:53.04%;border:none;padding:0cm 5.4pt 0cm 5.4pt'>
                                                        <p class=MsoNormal align=center style='text-align:center'>
                                                            <b>
                                                                <span style='font-size:13.0pt;color:windowtext'>
                                                                    Thủ
                                                                    trưởng cơ quan, tổ chức, đơn vị
                                                                </span>
                                                            </b>
                                                        </p>
                                                        <p class=MsoNormal align=center style='text-align:center'>
                                                            <b>
                                                                <span style='font-size:13.0pt;color:windowtext'>
                                                                    Ký
                                                                    tên, đóng dấu
                                                                </span>
                                                            </b>
                                                        </p>
                                                    </td>
                                                </tr>
                                            </table>

                                            <p class=MsoNormal style='margin-bottom:6.0pt'>
                                                <span style='
color:windowtext'>&nbsp;</span>
                                            </p>

                                        </div>
                                    </div>
                                 
                                </div>



                            </div>
                   
                </div>
</html>";

            }
            else
            {

            

            }




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