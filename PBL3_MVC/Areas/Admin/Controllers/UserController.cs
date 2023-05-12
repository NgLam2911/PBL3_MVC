using PBL3_MVC.Data;
using PBL3_MVC.Models;
using PBL3_MVC.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PBL3_MVC.Areas.Admin.Controllers
{
    public class UserController : Controller
    {
        private Db db = new Db();
        // GET: Admin/User
        public ActionResult Index()
        {
            List<UserModel> users = db.Customers.Where(c => c.Account.Role.RoleName != "Admin").Select(c => new UserModel { UserName = c.Account.UserName, Email = c.Email, Password = c.Account.Password }).ToList();

            return View(users);
        }

        // GET: Admin/User/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/User/Create
        [HttpPost]
        public ActionResult Create(UserModel model)
        {
            if (ModelState.IsValid)
            {
                var check = db.Accounts.FirstOrDefault(s => s.UserName == model.UserName);
                if (check == null)
                {
                    model.Password = md5helper.string2md5(model.Password);

                    var newAccount = db.Accounts.Create();
                    newAccount.UserName = model.UserName;
                    newAccount.Password = model.Password;
                    db.Accounts.Add(newAccount);
                    db.SaveChanges();

                    var newCustomer = db.Customers.Create();
                    newCustomer.Account = db.Accounts.FirstOrDefault(s => s.UserName == model.UserName);
                    newCustomer.Account.Customer = newCustomer;
                    newCustomer.Name = model.UserName;
                    newCustomer.Email = model.Email;
                    db.Customers.Add(newCustomer);
                    db.SaveChanges();

                    return RedirectToAction("Index", "User");
                }
                else
                {
                    ModelState.AddModelError("", "Tên người dùng đã tồn tại!!");
                }
            }
            return View();
        }

        // GET: Admin/User/Edit/5
        public ActionResult Edit()
        {
            return View();
        }

        // POST: Admin/User/Edit/5
        [HttpPost]
        public ActionResult Edit( FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        // POST: Admin/User/Delete/5
        [HttpPost]
        public ActionResult Delete(FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
