using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PBL3_MVC.Data.Tables
{
    [Table("Route")]
    public partial class Route
    {
        public Route()
        {
            Schedules = new HashSet<Schedule>();
        }
        [Key]
        public int RouteID { get; set; }

        [Required]
        [StringLength(50)]
        public string RouteName { get; set; }

        [Required]
        public int DepartureID { get; set; }

        [Required]
        public int DestinationID { get; set; }

        public virtual Location Departure { get; set; }

        public virtual Location Destination { get; set; }

        public virtual ICollection<Schedule> Schedules { get; set; }
    }
}
