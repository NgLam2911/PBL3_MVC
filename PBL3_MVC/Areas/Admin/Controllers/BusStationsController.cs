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

namespace PBL3_MVC.Areas.Admin.Controllers
{
    public class BusStationsController : Controller
    {
        private Db db = new Db();

        // GET: Admin/BusStations
        public ActionResult Index()
        {
            var busStations = db.BusStations.Include(b => b.Account);
            return View(busStations.ToList());
        }

        // GET: Admin/BusStations/Create
        public ActionResult Create()
        {
            ViewBag.BusStationID = new SelectList(db.Accounts, "AccountID", "UserName");
            return View();
        }

        // POST: Admin/BusStations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "BusStationID,Name")] BusStation busStation)
        {
            if (ModelState.IsValid)
            {
                db.BusStations.Add(busStation);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.BusStationID = new SelectList(db.Accounts, "AccountID", "UserName", busStation.BusStationID);
            return View(busStation);
        }

        // GET: Admin/BusStations/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BusStation busStation = db.BusStations.Find(id);
            if (busStation == null)
            {
                return HttpNotFound();
            }
            ViewBag.BusStationID = new SelectList(db.Accounts, "AccountID", "UserName", busStation.BusStationID);
            return View(busStation);
        }

        // POST: Admin/BusStations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BusStationID,Name")] BusStation busStation)
        {
            if (ModelState.IsValid)
            {
                db.Entry(busStation).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.BusStationID = new SelectList(db.Accounts, "AccountID", "UserName", busStation.BusStationID);
            return View(busStation);
        }

        // GET: Admin/BusStations/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BusStation busStation = db.BusStations.Find(id);
            if (busStation == null)
            {
                return HttpNotFound();
            }
            return View(busStation);
        }

        // POST: Admin/BusStations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BusStation busStation = db.BusStations.Find(id);
            db.BusStations.Remove(busStation);
            db.SaveChanges();
            return RedirectToAction("Index");
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
