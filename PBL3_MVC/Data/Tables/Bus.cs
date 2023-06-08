using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PBL3_MVC.Data.Tables
{
    [Table("Bus")]
    public partial class Bus
    {
        public Bus()
        {
            Schedules = new HashSet<Schedule>();
        }
        [Key]
        public int BusID { get; set; }

        public int BusStationID { get; set; }

        [Required]
        [StringLength(50)]
        public string BusName { get; set; }

        public string BusNumber { get; set; } // Biển số xe

        public string RegisterPerson { get; set; } // Người đăng ký

        public string Driver { get; set; } // Tài xế

        public DateTime LicenseDate { get; set; } // Ngày cấp

        public DateTime RegDate { get; set; } // Ngày đăng ký

        public int NumberOfSeats { get; set; }

        public virtual BusStation BusStation { get; set; }

        public virtual ICollection<Schedule> Schedules { get; set; }
    }
}
