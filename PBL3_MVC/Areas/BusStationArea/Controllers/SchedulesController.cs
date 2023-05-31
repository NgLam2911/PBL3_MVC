using PBL3_MVC.Data;
using PBL3_MVC.Data.Tables;
using PBL3_MVC.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
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
                return RedirectToAction("Details", new { id = seat.ScheduleID });
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
            var schedule = db.Schedules.Find(id);
            ScheduleModel model = new ScheduleModel();
            model.Id = id;
            model.BusName = schedule.Bus.BusName;
            model.RouteName = schedule.Route.RouteName;
            model.DepatureTime = schedule.DepartureTime;
            model.DestinationTime = schedule.DestinationTime;
            model.Price = 100000;
            model.Status = schedule.Status;
            return View(model);
        }

        // POST: BusStationArea/Schedules/Edit/5
        [HttpPost]
        public ActionResult Edit(ScheduleModel scheduleModel)
        {
            if (scheduleModel.DestinationTime > scheduleModel.DepatureTime && scheduleModel.DepatureTime > DateTime.Now)
            {
                if (ModelState.IsValid)
                {
                    var userSession = Session["User"] as PBL3_MVC.Data.Tables.Account;
                    //Init db
                    var bus = db.Buses.Where(b => b.BusName == scheduleModel.BusName && b.BusStation.BusStationID == userSession.AccountID).FirstOrDefault();
                    var route = db.Routes.Where(r => r.RouteName == scheduleModel.RouteName).FirstOrDefault();

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

                    for (int i = 0; i < bus.NumberOfSeats; i++)
                    {
                        var seat = db.Seats.Create();
                        seat.Schedule = scheduleEdit;
                        seat.SeatNumber = i + 1;
                        seat.Status = false;
                        seat.Price = scheduleModel.Price;
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
            return View(scheduleModel);
        }

        // GET: BusStationArea/Schedules/Delete/5
        public ActionResult Delete(int id)
        {
            List<Seat> seats = db.Seats.Where(s => s.ScheduleID == id).ToList();
            db.Seats.RemoveRange(seats);

            Schedule schedule = db.Schedules.Find(id);
            db.Schedules.Remove(schedule);
            db.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
