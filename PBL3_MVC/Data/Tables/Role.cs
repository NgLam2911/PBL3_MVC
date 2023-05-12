using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PBL3_MVC.Data.Tables
{
    [Table("Role")]
    public partial class Role
    {
        public Role()
        {
            Accounts = new HashSet<Account>();
        }

        public int RoleID { get; set; }

        [Required]
        [StringLength(50)]
        public string NameRole { get; set; }

        public virtual ICollection<Account> Accounts { get; set; }
    }
}
