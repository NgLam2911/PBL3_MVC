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

namespace PBL3_MVC.Areas.BusStationArea.Controllers
{
    public class BusesController : Controller
    {
        private Db db = new Db();

        // GET: BusStation/Buses
        public ActionResult Index()
        {
            var userSession = Session["User"] as PBL3_MVC.Data.Tables.Account;
            var buses = db.Buses.Include(b => b.BusStation).Where(b => b.BusStation.Account.UserName == userSession.UserName);
            return View(buses.ToList());
        }

        // GET: BusStation/Buses/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: BusStation/Buses/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "BusID,BusStationID,BusName,NumberOfSeats")] Bus bus)
        {
            if (bus.NumberOfSeats <= 0)
            {
                ModelState.AddModelError("", "Số lượng ghế không thể nhỏ hơn 0!!");
                return View(bus);
            }
            var checkName = db.Buses.FirstOrDefault(b => b.BusName == bus.BusName);
            if (checkName == null)
            {
                if (ModelState.IsValid)
                {
                    var userSession = Session["User"] as PBL3_MVC.Data.Tables.Account;
                    bus.BusStationID = userSession.AccountID;
                    db.Buses.Add(bus);
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
            return View(bus);
        }

        // POST: BusStation/Buses/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BusID,BusStationID,BusName,NumberOfSeats")] Bus bus)
        {
            if (bus.NumberOfSeats <= 0)
            {
                ModelState.AddModelError("", "Số lượng ghế không thể nhỏ hơn 0!!");
                return View(bus);
            }
            var checkName = db.Buses.FirstOrDefault(b => b.BusName == bus.BusName && b.BusID != bus.BusID);
            if (checkName == null)
            {
                if (ModelState.IsValid)
                {
                    var userSession = Session["User"] as PBL3_MVC.Data.Tables.Account;
                    bus.BusStationID = userSession.AccountID;
                    db.Entry(bus).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            else
            {
                ModelState.AddModelError("", "Tên xe đã tồn tại!!");
            }

            return View(bus);
        }

        public ActionResult Delete(int? id)
        {
            Bus bus = db.Buses.Find(id);
            db.Buses.Remove(bus);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
