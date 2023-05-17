using PBL3_MVC.Data;
using PBL3_MVC.Data.Tables;
using PBL3_MVC.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Security.AccessControl;
using System.Web;
using System.Web.Mvc;

namespace PBL3_MVC.Areas.BusStationArea.Controllers
{
    public class SchedulesController : Controller
    {
        private Db db = new Db();
        // GET: BusStationArea/Schedules
        public ActionResult Index()
        {
            var userSession = Session["User"] as PBL3_MVC.Data.Tables.Account;
            List<ScheduleModel> schedules = db.Schedules.Where(s => s.Bus.BusStationID == userSession.AccountID).Select(s => new ScheduleModel { Id = s.ScheduleID, BusName = s.Bus.BusName, RouteName = s.Route.RouteName, Departure = s.Route.Departure, Destination = s.Route.Destination, DepatureTime = s.DepartureTime, DestinationTime = s.DestinationTime, NumberOfSeat = s.Bus.NumberOfSeats, Status = s.Status }).ToList();
            for (int i = 0; i < schedules.Count; i++)
            {
                var scheduleId = schedules[i].Id;
                schedules[i].SeatList = db.Seats.Where(seat => seat.ScheduleID == scheduleId).ToList();
            }

            return View(schedules);
        }

        // GET: BusStationArea/Schedules/Details/5
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
                return RedirectToAction("Details", new {id = seat.ScheduleID});
            }
            return View(seat);
        }

        // GET: BusStationArea/Schedules/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: BusStationArea/Schedules/Create
        [HttpPost]
        public ActionResult Create(ScheduleModel schedule)
        {
            if (schedule.DestinationTime > schedule.DepatureTime && schedule.DepatureTime > DateTime.Now)
            {
                if (ModelState.IsValid)
                {
                    var userSession = Session["User"] as PBL3_MVC.Data.Tables.Account;
                    //Init db
                    var bus = db.Buses.Where(b => b.BusName == schedule.BusName && b.BusStation.BusStationID == userSession.AccountID).FirstOrDefault();
                    var route = db.Routes.Where(r => r.RouteName == schedule.RouteName).FirstOrDefault();

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
            return View(schedule);
        }

        // GET: BusStationArea/Schedules/Edit/5
        public ActionResult Edit(int id)
        {

            return View();
        }

        // POST: BusStationArea/Schedules/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
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

        // GET: BusStationArea/Schedules/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }
    }
}
