using System.Web.Mvc;

namespace PBL3_MVC.Areas.BusStationArea
{
    public class BusStationAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "BusStationArea";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "BusStationArea_default",
                "BusStationArea/{controller}/{action}/{id}",
                new {controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}