using PBL3_MVC.Data;
using PBL3_MVC.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PBL3_MVC.Controllers
{
    public class AuthController : Controller
    {
        private readonly BookingBusEntities _db = new BookingBusEntities();

        //public AuthController(BookingBusEntities db) { 
        //    _db = db;
        //}

        // GET: Auth
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult SignIn()
        {
            return View();
        }
        [HttpPost]
        public ActionResult SignIn(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var Account = _db.Accounts.Where(account => account.UserName == model.username && account.Password == model.password).FirstOrDefault();
                if (Account == null) {
                    ModelState.AddModelError("", "Nhập sai tài khoản hoặc mật khẩu");
                    return View(model);
                }
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
        public ActionResult SignUp()
        {
            return View();
        }
    }
}