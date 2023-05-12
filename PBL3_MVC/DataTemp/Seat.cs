namespace PBL3_MVC.DataTemp
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Seat")]
    public partial class Seat
    {
        public int SeatID { get; set; }

        public int ScheduleID { get; set; }

        public int SeatNumber { get; set; }

        public double Price { get; set; }

        public bool Status { get; set; }

        public int? BillID { get; set; }

        public virtual Bill Bill { get; set; }

        public virtual Schedule Schedule { get; set; }
    }
}
