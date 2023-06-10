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
            List<UserModel> users = db.Customers
                .Where(c => c.Account.Role.RoleName != "Admin")
                .Select(c => new UserModel
                {
                    Id = c.Account.AccountID,
                    UserName = c.Account.UserName,
                    Email = c.Email,
                    Password = c.Account.Password
                }).ToList();

            return View(users);
        }

        // GET: Admin/User/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/User/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
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
        public ActionResult Edit(int id)
        {
            var customer = db.Customers.Find(id);
            UserModel user = new UserModel { Id = customer.CustomerID, Email = customer.Email, UserName = customer.Account.UserName };
            return View(user);
        }

        // POST: Admin/User/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(UserModel model)
        {
            if (ModelState.IsValid)
            {
                var check = db.Accounts.FirstOrDefault(s => s.UserName == model.UserName && s.AccountID != model.Id);
                if (check == null)
                {
                    model.Password = md5helper.string2md5(model.Password);

                    var accountEdit = db.Accounts.FirstOrDefault(s => s.AccountID == model.Id);
                    accountEdit.UserName = model.UserName;
                    accountEdit.Password = model.Password;
                    db.SaveChanges();

                    var customerEdit = db.Customers.FirstOrDefault(s => s.CustomerID == model.Id);
                    customerEdit.Name = model.UserName;
                    customerEdit.Email = model.Email;
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
        public ActionResult Delete(int id)
        {
            var customer = db.Customers.Find(id);
            db.Customers.Remove(customer);

            var account = db.Accounts.Find(id);
            db.Accounts.Remove(account);
            db.SaveChanges();

            return RedirectToAction("Index", "User");
        }
    }
}
