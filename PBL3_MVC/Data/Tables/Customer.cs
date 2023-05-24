using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PBL3_MVC.Data.Tables
{
    [Table("Customer")]
    public partial class Customer
    {
        public Customer()
        {
            Bills = new HashSet<Bill>();
        }

        [Key]
        public int CustomerID { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        [StringLength(50)]
        public string Email { get; set; }

        public virtual Account Account { get; set; }

        public virtual ICollection<Bill> Bills { get; set; }
    }
}
