using PBL3_MVC.Data;
using PBL3_MVC.Models;
using PBL3_MVC.Utils;
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
                    var f_password = md5helper.string2md5(model.password);
                    var User = _db.Accounts.FirstOrDefault(account => account.UserName == model.username && account.Password == f_password);
                    if (User == null)
                    {
                        ModelState.AddModelError("", "Nhập sai tài khoản hoặc mật khẩu");
                        return View(model);
                    }
                    Session["User"] = User;
                    if (User.Role == null) 
                    {
                        return RedirectToAction("Index", "Home", new { area = "Customer" });
                    }
                    if (User.Role.RoleName == "Admin")
                    {
                        return RedirectToAction("Index", "Home", new { area = "Admin" });
                    }
                    else if (User.Role.RoleName == "BusStation")
                    {
                        return RedirectToAction("Index", "Home", new { area = "BusStationArea" });
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
                        model.password = md5helper.string2md5(model.password);

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
                        ModelState.AddModelError("", "Tên người dùng đã tồn tại!!");
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
        
    }
}