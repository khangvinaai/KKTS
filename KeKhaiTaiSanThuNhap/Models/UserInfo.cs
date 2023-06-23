using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Security;

namespace KeKhaiTaiSanThuNhap.Models
{
    public class UserInfo
    {
        private KSTNEntities db = new KSTNEntities();
        public UserInfo()
        {

        }

        public int GetUser()
        {
            HttpCookie authCookie = System.Web.HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName];
            FormsAuthenticationTicket authTicket = FormsAuthentication.Decrypt(authCookie.Value);
            string str = authTicket.UserData;
            string[] subs = str.Split(',');
            return Int32.Parse(subs[0]);

        }

        public string GetUserNameAccount()
        {
            HttpCookie authCookie = System.Web.HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName];

            FormsAuthenticationTicket authTicket = FormsAuthentication.Decrypt(authCookie.Value);

            string str = authTicket.UserData;
            string[] subs = str.Split(',');
            return subs[1];
           
        }

        public string GetRole()
        {
            HttpCookie authCookie = System.Web.HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName];
            FormsAuthenticationTicket authTicket = FormsAuthentication.Decrypt(authCookie.Value);
            string str = authTicket.UserData;
            string[] subs = str.Split(',');
            return subs[2];
        }

        public int GetUserCoQuan()
        {
            HttpCookie authCookie = System.Web.HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName];
            FormsAuthenticationTicket authTicket = FormsAuthentication.Decrypt(authCookie.Value);

            string str = authTicket.UserData;
            string[] subs = str.Split(',');
            return Int32.Parse(subs[3]);
        }

        public bool CheckQuyen(string menuCode, string ChucNangCode)
        {
            var user = GetUserNameAccount();
            var tk = db.HT_TaiKhoan.FirstOrDefault(_ => _.TenTaiKhoan == user).MaNhomTaiKhoan;
            var ntk = db.HT_NhomTaiKhoan.FirstOrDefault(_ => _.MaNhomTaiKhoan == tk).MaTaiKhoan;

            var data = db.HT_ChiTietPhanQuyen.Where(_ => _.MaTaiKhoan == ntk && _.TrangThai == true && _.ChucNangCode.ToUpper() == ChucNangCode.ToUpper()).Select(_ => _.MenuCode);

            return !data.Contains(menuCode);
        }

        public string GetRandomPassword(int length)
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
    }

   

}