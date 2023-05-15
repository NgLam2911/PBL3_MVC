using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PBL3_MVC.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Tên tài khoản không được để trống!")]
        public string username { get; set; }
        [Required(ErrorMessage = "Mật khẩu không được để trống!")]
        public string password { get; set; }
    }
}