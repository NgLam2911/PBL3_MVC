namespace PBL3_MVC.DataTemp
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Schedule")]
    public partial class Schedule
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Schedule()
        {
            Seats = new HashSet<Seat>();
        }

        public int ScheduleID { get; set; }

        public int BusID { get; set; }

        public int RouteID { get; set; }

        public DateTime DepartureTime { get; set; }

        public DateTime DestinationTime { get; set; }

        public int Weekday { get; set; }

        public bool Status { get; set; }

        public virtual Bus Bus { get; set; }

        public virtual Route Route { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Seat> Seats { get; set; }
    }
}
