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

namespace PBL3_MVC.Areas.BusStation.Controllers
{
    public class BusesController : Controller
    {
        private Db db = new Db();

        // GET: BusStation/Buses
        public ActionResult Index()
        {
            var buses = db.Buses.Select(b => new BusModel() {BusID = b.BusID,BusName = b.BusName,NumberOfSeats = b.NumberOfSeats,BusStationName = b.BusStation.Name});
            return View(buses.ToList());
        }

        // GET: BusStation/Buses/Create
        public ActionResult Create()
        {
            ViewBag.BusStationID = new SelectList(db.BusStations, "BusStationID", "Name");
            return View();
        }

        // POST: BusStation/Buses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "BusID,BusStationID,BusName,NumberOfSeats")] Bus bus)
        {
            if (ModelState.IsValid)
            {
                db.Buses.Add(bus);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.BusStationID = new SelectList(db.BusStations, "BusStationID", "Name", bus.BusStationID);
            return View(bus);
        }

        // GET: BusStation/Buses/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Bus bus = db.Buses.Find(id);
            if (bus == null)
            {
                return HttpNotFound();
            }
            ViewBag.BusStationID = new SelectList(db.BusStations, "BusStationID", "Name", bus.BusStationID);
            return View(bus);
        }

        // POST: BusStation/Buses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BusID,BusStationID,BusName,NumberOfSeats")] Bus bus)
        {
            if (ModelState.IsValid)
            {
                db.Entry(bus).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.BusStationID = new SelectList(db.BusStations, "BusStationID", "Name", bus.BusStationID);
            return View(bus);
        }

        // GET: BusStation/Buses/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Bus bus = db.Buses.Find(id);
            if (bus == null)
            {
                return HttpNotFound();
            }
            return View(bus);
        }

        // POST: BusStation/Buses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Bus bus = db.Buses.Find(id);
            db.Buses.Remove(bus);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
