using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PBL3_MVC.Models
{
    public class BookingModel
    {
        [Required]
        public string Departure { get; set; }
        [Required]
        public string Destination { get; set; }
        [DataType(DataType.DateTime), Required]
        public DateTime DepartureTime { get; set; }
    }
}