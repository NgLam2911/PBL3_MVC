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

namespace PBL3_MVC.Areas.Admin.Controllers
{
    public class SchedulesController : Controller
    {
        private Db db = new Db();

        // GET: Admin/Schedules
        public ActionResult Index()
        {
            List<ScheduleModel> schedules = db.Schedules.Select(s => new ScheduleModel { Id = s.ScheduleID, Bus = new BusModel { BusName = s.Bus.BusName, BusID = s.Bus.BusID }, Route = s.Route, DepatureTime = s.DepartureTime, DestinationTime = s.DestinationTime, NumberOfSeat = s.Bus.NumberOfSeats, Status = s.Status }).ToList();
            for (int i = 0; i < schedules.Count; i++)
            {
                var scheduleId = schedules[i].Id;
                schedules[i].SeatList = db.Seats.Where(seat => seat.ScheduleID == scheduleId).ToList();
            }

            return View(schedules);
        }

        // GET: Admin/Schedules/Details/5
        public ActionResult Details(int id)
        {
            var scheduleID = id;
            List<Seat> seats = db.Seats.Where(s => s.ScheduleID == scheduleID).ToList();
            return View(seats);
        }
        public ActionResult EditSeat(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Seat seat = db.Seats.Find(id);
            if (seat == null)
            {
                return HttpNotFound();
            }
            return View(seat);
        }
        [HttpPost]
        public ActionResult EditSeat(Seat seat)
        {
            if (ModelState.IsValid)
            {
                db.Entry(seat).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Details", new { id = seat.ScheduleID });
            }
            return View(seat);
        }

        // GET: Admin/Schedules/Create
        public ActionResult Create()
        {
            HashSet<BusModel> busModels = new HashSet<BusModel>();
            foreach (Bus bus in db.Buses) {
                busModels.Add(new BusModel { BusID = bus.BusID, BusName = bus.BusNumber + " - " + bus.BusName });
            }
            ViewBag.BusNames = new SelectList(busModels, "BusID", "BusName");
            ViewBag.RouteNames = new SelectList(db.Routes, "RouteID", "RouteName");
            return View();
        }

        // POST: Admin/Schedules/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "BusNames,RouteNames,DepartureTime,DestinationTime,Price,NumberOfSeats,Status")]ScheduleModel schedule)
        {
            if (schedule.DestinationTime > schedule.DepatureTime && schedule.DepatureTime > DateTime.Now)
            {
                if (ModelState.IsValid)
                {
                    var bus = db.Buses.Where(b => b.BusID == schedule.Bus.BusID).FirstOrDefault();
                    var route = schedule.Route;

                    if (bus == null)
                    {
                        ModelState.AddModelError("", "Không có tên xe này!!");
                        return View(schedule);
                    }
                    else if (route == null)
                    {
                        ModelState.AddModelError("", "Không có tên tuyến đường này!!");
                        return View(schedule);
                    }

                    var newSchedule = db.Schedules.Create();
                    newSchedule.Bus = bus;
                    newSchedule.Route = route;
                    newSchedule.DepartureTime = schedule.DepatureTime;
                    newSchedule.DestinationTime = schedule.DestinationTime;
                    newSchedule.Status = schedule.Status;
                    db.Schedules.Add(newSchedule);

                    for (int i = 0; i < bus.NumberOfSeats; i++)
                    {
                        var seat = db.Seats.Create();
                        seat.Schedule = newSchedule;
                        seat.SeatNumber = i + 1;
                        seat.Status = false;
                        seat.Price = schedule.Price;
                        seat.Bill = null;
                        db.Seats.Add(seat);
                    }
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            else
            {
                ModelState.AddModelError("", "Thời gian cho lịch trình không phù hợp!!");
            }

            HashSet<BusModel> busModels = new HashSet<BusModel>();
            foreach (Bus bus in db.Buses)
            {
                busModels.Add(new BusModel { BusID = bus.BusID, BusName = bus.BusNumber + " - " + bus.BusName });
            }
            ViewBag.BusNames = new SelectList(busModels, "BusID", "BusName");
            ViewBag.RouteNames = new SelectList(db.Routes, "RouteID", "RouteName");
            return View(schedule);
        }

        // GET: Admin/Schedules/Edit/5
        public ActionResult Edit(int id)
        {
            Schedule schedule = db.Schedules.Find(id);
            if (schedule == null)
            {
                return HttpNotFound();
            }
            ScheduleModel model = new ScheduleModel();
            model.Id = id;
            model.Bus = new BusModel { BusID = schedule.BusID, BusName = schedule.Bus.BusNumber + " - " + schedule.Bus.BusName};
            model.Route = schedule.Route;
            model.DepatureTime = schedule.DepartureTime;
            model.DestinationTime = schedule.DestinationTime;
            model.Price = 0; //Deprecated
            model.Status = schedule.Status;

            HashSet<BusModel> busModels = new HashSet<BusModel>();
            foreach (Bus bus in db.Buses)
            {
                busModels.Add(new BusModel { BusID = bus.BusID, BusName = bus.BusNumber + " - " + bus.BusName });
            }
            ViewBag.BusNames = new SelectList(busModels, "BusID", "BusName", model.Bus.BusID);
            ViewBag.RouteNames = new SelectList(db.Routes, "RouteID", "RouteName", model.Route.RouteID);
            return View(model);
        }

        // POST: Admin/Schedules/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ScheduleModel scheduleModel)
        {
            if (scheduleModel.DestinationTime > scheduleModel.DepatureTime && scheduleModel.DepatureTime > DateTime.Now)
            {
                if (ModelState.IsValid)
                {
                    var bus = db.Buses.Where(b => b.BusID == scheduleModel.Bus.BusID).FirstOrDefault();
                    var route = scheduleModel.Route;

                    if (bus == null)
                    {
                        ModelState.AddModelError("", "Không có tên xe này!!");
                        return View(scheduleModel);
                    }
                    else if (route == null)
                    {
                        ModelState.AddModelError("", "Không có tên tuyến đường này!!");
                        return View(scheduleModel);
                    }

                    var scheduleEdit = db.Schedules.FirstOrDefault(sche => sche.ScheduleID == scheduleModel.Id);

                    List<Seat> seats = db.Seats.Where(s => s.ScheduleID == scheduleEdit.ScheduleID).ToList();
                    db.Seats.RemoveRange(seats);

                    scheduleEdit.Bus = bus;
                    scheduleEdit.Route = route;
                    scheduleEdit.DepartureTime = scheduleModel.DepatureTime;
                    scheduleEdit.DestinationTime = scheduleModel.DestinationTime;
                    scheduleEdit.Status = scheduleModel.Status;

                    db.Entry(scheduleEdit).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            else
            {
                ModelState.AddModelError("", "Thời gian cho lịch trình không phù hợp!!");
            }

            HashSet<BusModel> busModels = new HashSet<BusModel>();
            foreach (Bus bus in db.Buses)
            {
                busModels.Add(new BusModel { BusID = bus.BusID, BusName = bus.BusNumber + " - " + bus.BusName });
            }
            ViewBag.BusNames = new SelectList(busModels, "BusID", "BusName");
            ViewBag.RouteNames = new SelectList(db.Routes, "RouteID", "RouteName");
            return View(scheduleModel);
        }

        // GET: Admin/Schedules/Delete/5
        public ActionResult Delete(int? id)
        {
            List<Seat> seats = db.Seats.Where(s => s.ScheduleID == id).ToList();
            db.Seats.RemoveRange(seats);

            Schedule schedule = db.Schedules.Find(id);
            db.Schedules.Remove(schedule);
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
