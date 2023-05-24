using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PBL3_MVC.Data;
using PBL3_MVC.Data.Tables;
using PBL3_MVC.Models;
using PBL3_MVC.Utils;

namespace PBL3_MVC.Areas.Admin.Controllers
{
    public class BusStationsController : Controller
    {
        private Db db = new Db();

        // GET: Admin/BusStations
        public ActionResult Index()
        {
            List<BusStationModel> busStations = db.BusStations.Select(b => new BusStationModel { Id = b.Account.AccountID, Password = b.Account.Password, UserName = b.Name }).ToList();
            return View(busStations);
        }

        // GET: Admin/BusStations/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/BusStations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(BusStationModel model)
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
                    newAccount.RoleID = 2;
                    db.Accounts.Add(newAccount);
                    db.SaveChanges();

                    var newBusStation = db.BusStations.Create();
                    newBusStation.Account = db.Accounts.FirstOrDefault(s => s.UserName == model.UserName);
                    newBusStation.Name = model.UserName;
                    db.BusStations.Add(newBusStation);
                    db.SaveChanges();

                    return RedirectToAction("Index", "BusStations");
                }
                else
                {
                    ModelState.AddModelError("", "Tên bến xe đã tồn tại!!");
                }
            }
            return View();
        }

        // GET: Admin/BusStations/Edit/5
        public ActionResult Edit(int id)
        {
            var busStation = db.BusStations.Find(id);
            BusStationModel model = new BusStationModel { Id = id, UserName = busStation.Name};
            return View(model);
        }

        // POST: Admin/BusStations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(BusStationModel model)
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

                    var busStationEdit = db.BusStations.FirstOrDefault(s => s.BusStationID == model.Id);
                    busStationEdit.Name = model.UserName;
                    db.SaveChanges();

                    return RedirectToAction("Index", "BusStations");
                }
                else
                {
                    ModelState.AddModelError("", "Tên bến xe đã tồn tại!!");
                }
            }
            return View();
        }

        // GET: Admin/BusStations/Delete/5
        public ActionResult Delete(int id)
        {
            var model = db.BusStations.Find(id);
            db.BusStations.Remove(model);

            var account = db.Accounts.Find(id);
            db.Accounts.Remove(account);
            db.SaveChanges();

            return RedirectToAction("Index", "BusStations");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
