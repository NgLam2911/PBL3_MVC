using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PBL3_MVC.Data.Tables
{
    [Table("Location")]
    public partial class Location
    {
        public Location()
        {
            DeparturesRoute = new HashSet<Route>();
            DestinationsRoute = new HashSet<Route>();
        }
        [Key]
        [Required]
        public int LocationID { get; set; }
        
        [Required]
        [StringLength(128)]
        public string LocationName { get; set; }

        public virtual ICollection<Route> DeparturesRoute { get; set; }

        public virtual ICollection<Route> DestinationsRoute { get; set; }
    }
}
