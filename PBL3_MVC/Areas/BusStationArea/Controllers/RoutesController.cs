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
    public class RoutesController : Controller
    {
        private Db db = new Db();

        // GET: BusStationArea/Routes
        public ActionResult Index()
        {
            return View(db.Routes.ToList());
        }

        //// GET: BusStationArea/Routes/Create
        //public ActionResult Create()
        //{
        //    return View();
        //}

        //// POST: BusStationArea/Routes/Create
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "RouteID,RouteName,Departure,Destination")] Route route)
        //{
        //    var checkName = db.Routes.FirstOrDefault(b => b.RouteName == route.RouteName);
        //    if (checkName == null)
        //    {
        //        if (ModelState.IsValid)
        //        {
        //            db.Routes.Add(route);
        //            db.SaveChanges();
        //            return RedirectToAction("Index");
        //        }
        //    }
        //    else
        //    {
        //        ModelState.AddModelError("", "Tên tuyến đường đã tồn tại!!");
        //    }

        //    return View(route);
        //}

        //// GET: BusStationArea/Routes/Edit/5
        //public ActionResult Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Route route = db.Routes.Find(id);
        //    if (route == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(route);
        //}

        //// POST: BusStationArea/Routes/Edit/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "RouteID,RouteName,Departure,Destination")] Route route)
        //{
        //    var checkName = db.Routes.FirstOrDefault(b => b.RouteName == route.RouteName && b.RouteID != route.RouteID);
        //    if (checkName == null)
        //    {
        //        if (ModelState.IsValid)
        //        {
        //            db.Entry(route).State = EntityState.Modified;
        //            db.SaveChanges();
        //            return RedirectToAction("Index");
        //        }
        //    }
        //    else
        //    {
        //        ModelState.AddModelError("", "Tên tuyến đường đã tồn tại!!");
        //    }

        //    return View(route);
        //}

        //public ActionResult Delete(int id)
        //{
        //    Route route = db.Routes.Find(id);
        //    db.Routes.Remove(route);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

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
