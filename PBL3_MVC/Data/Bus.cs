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
    
    public partial class Bus
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Bus()
        {
            this.Schedules = new HashSet<Schedule>();
        }
    
        public string BusID { get; set; }
        public string BusStationID { get; set; }
        public string BusName { get; set; }
        public int NumberOfSeats { get; set; }
        public string Description { get; set; }
    
        public virtual BusStation BusStation { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Schedule> Schedules { get; set; }
    }
}
