using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace PBL3_MVC.Data.Tables
{
    [Table("Schedule")]
    public partial class Schedule
    {
        public Schedule()
        {
            Seats = new HashSet<Seat>();
        }
        [Key]
        public int ScheduleID { get; set; }

        public int BusID { get; set; }

        public int RouteID { get; set; }

        public DateTime DepartureTime { get; set; }

        public DateTime DestinationTime { get; set; }

        public int Weekday { get; set; }

        public bool Status { get; set; }

        public virtual Bus Bus { get; set; }

        public virtual Route Route { get; set; }

        public virtual ICollection<Seat> Seats { get; set; }
    }
}
