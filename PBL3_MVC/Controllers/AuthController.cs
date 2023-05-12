using PBL3_MVC.Data;
using PBL3_MVC.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace PBL3_MVC.Controllers
{
    public class AuthController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult SignIn()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SignIn(LoginModel model)
        {
            using (var _db = new Db())
            {
                if (ModelState.IsValid)
                {
                    var f_password = GetMD5(model.password);
                    var User = _db.Accounts.FirstOrDefault(account => account.UserName == model.username && account.Password == f_password);
                    if (User == null)
                    {
                        ModelState.AddModelError("", "Nhập sai tài khoản hoặc mật khẩu");
                        return View(model);
                    }
                    Session["User"] = User;
                    if (User.Role == null) 
                    {
                        return RedirectToAction("Index", "Auth");
                    }
                    if (User.Role.RoleName == "Admin")
                    {
                        return RedirectToAction("Index", "Home", new { area = "Admin" });
                    }
                    else if (User.Role.RoleName == "BusStation")
                    {
                        return RedirectToAction("Index", "Home", new { area = "BusStation" });
                    }
                }
                return View();
            }
        }
        public ActionResult SignUp()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SignUp(RegisterModel model)
        {
            using (var _db = new Db())
            {
                if (ModelState.IsValid)
                {
                    var check = _db.Accounts.FirstOrDefault(s => s.UserName == model.username);
                    if (check == null)
                    {
                        model.password = GetMD5(model.password);

                        var newAccount = _db.Accounts.Create();
                        newAccount.UserName = model.username;
                        newAccount.Password = model.password;
                        _db.Accounts.Add(newAccount);
                        _db.SaveChanges();

                        var newCustomer = _db.Customers.Create();
                        newCustomer.Account = _db.Accounts.FirstOrDefault(s => s.UserName == model.username);
                        newCustomer.Account.Customer = newCustomer;
                        newCustomer.Name = model.username;
                        newCustomer.Email = model.email;
                        _db.Customers.Add(newCustomer);
                        _db.SaveChanges();

                        return RedirectToAction("Index", "Auth");
                    }
                    else
                    {
                        ModelState.AddModelError("", "UserName đã tồn tại!!");
                    }
                }
            }
            return View();
        }
        public ActionResult SignOut()
        {
            Session.Clear();
            return RedirectToAction("Index", "Auth");
        }
        private static string GetMD5(string str)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] fromData = Encoding.UTF8.GetBytes(str);
            byte[] targetData = md5.ComputeHash(fromData);
            string byte2String = null;
            
            for (int i = 0; i < targetData.Length; i++)
            {
                byte2String += targetData[i].ToString("x2");
            }
            return byte2String;
        }
    }
}