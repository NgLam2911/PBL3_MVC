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
    public class BusesController : Controller
    {
        private Db db = new Db();

        // GET: Admin/Buses
        public ActionResult Index()
        {
            var buses = db.Buses.Include(b => b.BusStation);
            return View(buses.ToList());
        }

        // GET: Admin/Buses/Create
        public ActionResult Create()
        {
            ViewBag.BusStationID = new SelectList(db.BusStations, "BusStationID", "Name");
            return View();
        }

        // POST: Admin/Buses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "BusID,BusStationID,BusName,BusNumber,RegisterPerson,Driver,LicenseDate,NumberOfSeats")] Bus bus)
        {
            if (DateTime.Now < bus.LicenseDate)
            {
                ModelState.AddModelError("", "Ngày cấp phép không phù hợp!!");
            }
            if (bus.NumberOfSeats <= 0)
            {
                ModelState.AddModelError("", "Số lượng ghế không thể nhỏ hơn 0!!");
            }
            var checkName = db.Buses.FirstOrDefault(b => b.BusNumber == bus.BusNumber);
            if (checkName == null)
            {
                if (ModelState.IsValid)
                {
                    bus.RegDate = DateTime.Now.Date;
                    db.Buses.Add(bus);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            else
            {
                ModelState.AddModelError("", "Biển số xe đã tồn tại!!");
            }

            ViewBag.BusStationID = new SelectList(db.BusStations, "BusStationID", "Name", bus.BusStationID);
            return View(bus);
        }

        // GET: Admin/Buses/Edit/5
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

        // POST: Admin/Buses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Bus bus)
        {
            if (bus.NumberOfSeats <= 0)
            {
                ModelState.AddModelError("", "Số lượng ghế không thể nhỏ hơn 0!!");
                return View(bus);
            }
            var checkName = db.Buses.FirstOrDefault(b => b.BusNumber == bus.BusNumber && b.BusID != bus.BusID);
            if (checkName == null)
            {
                if (ModelState.IsValid)
                {
                    db.Entry(bus).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            else
            {
                ModelState.AddModelError("", "Tên xe đã tồn tại!!");
            }

            ViewBag.BusStationID = new SelectList(db.BusStations, "BusStationID", "Name", bus.BusStationID);
            return View(bus);
        }

        // GET: Admin/Buses/Delete/5
        public ActionResult Delete(int? id)
        {
            Bus bus = db.Buses.Find(id);
            db.Buses.Remove(bus);
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
