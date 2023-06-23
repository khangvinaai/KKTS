using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace KeKhaiTaiSanThuNhap.Models
{
    public class userRoleProvider : RoleProvider
    {
        public override string ApplicationName { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override void CreateRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        public override string[] GetAllRoles()
        {
            throw new NotImplementedException();
        }

        public override string[] GetRolesForUser(string username)
        {
            KSTNEntities db = new KSTNEntities();

            string[] role = null;
            int roletemp;

            var flag = db.HT_TaiKhoan.Where(_ => _.TenTaiKhoan == username).FirstOrDefault();


            if (flag != null)
            {
                roletemp = (int)(from tk in db.HT_TaiKhoan
                            where tk.TenTaiKhoan == username
                            select tk.MaNhomTaiKhoan).SingleOrDefault();
                role = (from ntk in db.HT_NhomTaiKhoan
                        where ntk.MaNhomTaiKhoan == roletemp
                        select ntk.MaTaiKhoan).ToArray();

                if(role != null)
                {
                    role[0] = "ADMIN";
                }

            }
            return role;
        }

        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool IsUserInRole(string username, string roleName)
        {
            throw new NotImplementedException();
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            throw new NotImplementedException();
        }
    }
}