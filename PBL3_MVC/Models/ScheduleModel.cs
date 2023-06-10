using PBL3_MVC.Data.Tables;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PBL3_MVC.Models
{
    public class ScheduleModel
    {
        public int Id { get; set; }
        [Required]
        public string BusName { get; set; }
        [Required]
        public string RouteName { get; set; }
        public string Departure { get; set; }
        public string Destination { get; set; }
        [DataType(DataType.DateTime), Required]
        public DateTime DepatureTime { get; set; }
        [DataType(DataType.DateTime), Required]
        public DateTime DestinationTime { get; set; }
        [Required]
        public double Price { get; set; }
        public int NumberOfSeat { get; set; }
        public List<Seat> SeatList { get; set; }
        [Required]
        public bool Status { get; set; }
    }
}