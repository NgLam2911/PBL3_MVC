using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PBL3_MVC.Models
{
    public class BillModel
    {
        public int Id { get; set; }
        public int SeatID { get; set; }
        public string CustomerName { get; set; }
        public string BusStationName { get; set; }
        public string BusName { get; set; }
        public int SeatNumber { get; set; }
        public string Departure { get; set; }
        public string Destination { get; set; }
        public DateTime DepartureTime { get; set; }
        public DateTime DestinationTime { get; set; }
        public double Price { get; set; }
        public DateTime OrderDate { get; set; }
    }
}