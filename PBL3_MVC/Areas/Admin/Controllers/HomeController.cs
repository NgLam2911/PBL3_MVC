using PBL3_MVC.Data;
using PBL3_MVC.Data.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PBL3_MVC.Areas.Admin.Controllers
{
    public class HomeController : Controller
    {
        // GET: Admin/Home
        public ActionResult Index()
        {
            Account userSession = (Account)Session["User"];
            using (var _db = new Db())
            {
                if (userSession == null)
                {
                    return Redirect("/");
                }
                else if (userSession != null)
                {
                    var count = _db.Accounts.Count(m => m.AccountID == userSession.AccountID & m.RoleID == 1);
                    if (count == 0)
                    {
                        return Redirect("/");
                    }
                }
            }
            return View();
        }
    }
}