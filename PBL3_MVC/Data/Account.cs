//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PBL3_MVC.Data
{
    using System;
    using System.Collections.Generic;
    
    public partial class Account
    {
        public string AccountID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string RoleID { get; set; }
    
        public virtual Role Role { get; set; }
        public virtual BusStation BusStation { get; set; }
        public virtual Customer Customer { get; set; }
    }
}
