using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PBL3_MVC.Models
{
    public class BusModel
    {
        public int BusID { get; set; }
        public string BusName { get; set; }
        public int NumberOfSeats { get; set; }
        public string BusStationName { get; set; }
    }
}