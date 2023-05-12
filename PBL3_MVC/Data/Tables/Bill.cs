using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace PBL3_MVC.Data.Tables
{
    [Table("Bill")]
    public partial class Bill
    {
        public Bill()
        {
            Seats = new HashSet<Seat>();
        }

        public int BillID { get; set; }

        public int CustomerID { get; set; }

        public DateTime OrderDate { get; set; }

        public bool Status { get; set; }

        public virtual Customer Customer { get; set; }

        public virtual ICollection<Seat> Seats { get; set; }
    }
}
