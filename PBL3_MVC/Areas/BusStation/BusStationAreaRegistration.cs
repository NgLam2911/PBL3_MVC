using System.Web.Mvc;

namespace PBL3_MVC.Areas.BusStation
{
    public class BusStationAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "BusStation";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "BusStation_default",
                "BusStation/{controller}/{action}/{id}",
                new {controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}