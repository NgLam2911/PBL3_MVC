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
            Routes = new HashSet<Route>();
        }
        [Key]
        [Required]
        public int LocationID { get; set; }
        
        [Required]
        [StringLength(128)]
        public string LocationName { get; set; }

        public virtual ICollection<Route> Routes { get; set; }
    }
}
