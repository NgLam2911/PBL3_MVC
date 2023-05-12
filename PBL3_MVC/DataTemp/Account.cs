namespace PBL3_MVC.DataTemp
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Account")]
    public partial class Account
    {
        public int AccountID { get; set; }

        [Required]
        [StringLength(50)]
        public string UserName { get; set; }

        [Required]
        [StringLength(50)]
        public string Password { get; set; }

        public int? RoleID { get; set; }

        public virtual Role Role { get; set; }

        public virtual BusStation BusStation { get; set; }

        public virtual Customer Customer { get; set; }
    }
}
