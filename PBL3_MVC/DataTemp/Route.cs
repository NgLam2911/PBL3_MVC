namespace PBL3_MVC.DataTemp
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Route")]
    public partial class Route
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Route()
        {
            Schedules = new HashSet<Schedule>();
        }

        public int RouteID { get; set; }

        [Required]
        [StringLength(50)]
        public string RouteName { get; set; }

        [Required]
        [StringLength(50)]
        public string Departure { get; set; }

        [Required]
        [StringLength(50)]
        public string Destination { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Schedule> Schedules { get; set; }
    }
}
