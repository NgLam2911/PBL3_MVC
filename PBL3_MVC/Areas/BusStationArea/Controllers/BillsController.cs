using PBL3_MVC.Data.Tables;
using PBL3_MVC.Data;
using PBL3_MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PBL3_MVC.Areas.BusStationArea.Controllers
{
    public class BillsController : Controller
    {
        private Db db = new Db();

        public ActionResult Index()
        {
            var userSession = Session["User"] as PBL3_MVC.Data.Tables.Account;
            List<BillModel> bills = db.Seats.Where(s => s.BillID != null && s.Schedule.Bus.BusStation.Name == userSession.UserName).Select(s => new BillModel { Id = (int)s.BillID, SeatID = s.SeatID, CustomerName = s.Bill.Customer.Name, BusStationName = s.Schedule.Bus.BusStation.Name, BusName = s.Schedule.Bus.BusName, SeatNumber = s.SeatNumber, Departure = s.Schedule.Route.Departure.LocationName, Destination = s.Schedule.Route.Destination.LocationName, DepartureTime = s.Schedule.DepartureTime, DestinationTime = s.Schedule.DestinationTime, Price = s.Price, OrderDate = s.Bill.OrderDate }).ToList();

            return View(bills);
        }

        public ActionResult Delete(int id, int seatid)
        {
            Bill bill = db.Bills.Find(id);
            Seat seat = db.Seats.Find(seatid);
            seat.BillID = null;
            seat.Status = false;
            db.Bills.Remove(bill);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}