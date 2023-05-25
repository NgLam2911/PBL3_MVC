using PBL3_MVC.Data;
using PBL3_MVC.Data.Tables;
using PBL3_MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PBL3_MVC.Areas.Customer.Controllers
{
    public class BookingController : Controller
    {
        private Db db = new Db();

        // GET: Customer/Booking
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(BookingModel model)
        {
            if (model.DepartureTime > DateTime.Now)
            {
                if (ModelState.IsValid)
                {
                    return RedirectToAction("ListSchedule", "Booking", model);
                }
            }
            else
            {
                ModelState.AddModelError("", "Thời gian cho lịch trình không phù hợp!!");
                return View(model);
            }
            return View();
        }
        public ActionResult ListSchedule(BookingModel model)
        {
            List<ScheduleModel> schedules = db.Schedules.Where(s => s.Route.Departure == model.Departure && s.Route.Destination == model.Destination && s.DepartureTime >= model.DepartureTime && s.Status == true).Select(s => new ScheduleModel { Id = s.ScheduleID, BusName = s.Bus.BusName, RouteName = s.Route.RouteName, Departure = s.Route.Departure, Destination = s.Route.Destination, DepatureTime = s.DepartureTime, DestinationTime = s.DestinationTime, NumberOfSeat = s.Bus.NumberOfSeats, Status = s.Status }).ToList();
            for (int i = 0; i < schedules.Count; i++)
            {
                var scheduleId = schedules[i].Id;
                schedules[i].SeatList = db.Seats.Where(seat => seat.ScheduleID == scheduleId).ToList();
            }

            return View(schedules);
        }
        public ActionResult ListSeat(int id)
        {
            var scheduleID = id;
            List<Seat> seats = db.Seats.Where(s => s.ScheduleID == scheduleID).ToList();
            return View(seats);
        }
        public ActionResult Details(int id)
        {
            var userSession = Session["User"] as PBL3_MVC.Data.Tables.Account;
            var seat = db.Seats.Find(id);

            BillModel model = new BillModel();
            model.SeatID = seat.SeatID;
            model.CustomerName = userSession.UserName;
            model.BusStationName = seat.Schedule.Bus.BusStation.Name;
            model.BusName = seat.Schedule.Bus.BusName;
            model.SeatNumber = seat.SeatNumber;
            model.Departure = seat.Schedule.Route.Departure;
            model.Destination = seat.Schedule.Route.Destination;
            model.DepartureTime = seat.Schedule.DepartureTime;
            model.DestinationTime = seat.Schedule.DestinationTime;
            model.Price = seat.Price;
            model.OrderDate = DateTime.Now;

            Session["Bill"] = model;

            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Details()
        {
            var userSession = Session["User"] as PBL3_MVC.Data.Tables.Account;
            var model = Session["Bill"] as BillModel;

            var bill = db.Bills.Create();
            bill.CustomerID = userSession.AccountID;
            bill.OrderDate = model.OrderDate;
            db.Bills.Add(bill);
            db.SaveChanges();

            int billid = bill.BillID;
            var seat = db.Seats.Find(model.SeatID);
            seat.Bill = bill;
            seat.Status = true;
            db.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}