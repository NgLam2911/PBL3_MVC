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
        [Required]
        public int Id { get; set; }
        [Required]
        public string BusName { get; set; }
        [Required]
        public string RouteName { get; set; }
        [Required]
        public string Departure { get; set; }
        [Required]
        public string Destination { get; set; }
        [Required]
        public DateTime DepatureTime { get; set; }
        [Required]
        public DateTime DestinationTime { get; set; }
        [Required]
        public double Price { get; set; }
        [Required]
        [Range(0, int.MaxValue)]
        public int NumberOfSeat { get; set; }
        public List<Seat> SeatList { get; set; }
        [Required]
        public bool Status { get; set; }
    }
}