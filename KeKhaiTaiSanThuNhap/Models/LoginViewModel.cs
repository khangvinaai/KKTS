﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KeKhaiTaiSanThuNhap.Models
{
    public class LoginViewModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}