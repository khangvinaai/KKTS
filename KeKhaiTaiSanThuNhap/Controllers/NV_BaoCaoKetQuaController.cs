using KeKhaiTaiSanThuNhap.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace KeKhaiTaiSanThuNhap.Controllers
{
    [Authorize(Roles = "ADMIN")]
    public class NV_BaoCaoKetQuaController : Controller
    {
        private KSTNEntities db = new KSTNEntities();
        // GET: NV_BaoCaoKetQua
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult BaoCao(int id)
        {
            ViewBag.id = id;
            return View();
        }

 
    }
}