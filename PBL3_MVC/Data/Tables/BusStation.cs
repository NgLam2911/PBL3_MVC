using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PBL3_MVC.Data.Tables
{
    [Table("BusStation")]
    public partial class BusStation
    {
        public BusStation()
        {
            Buses = new HashSet<Bus>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int AccountID { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        public virtual Account Account { get; set; }

        public virtual ICollection<Bus> Buses { get; set; }
    }
}
