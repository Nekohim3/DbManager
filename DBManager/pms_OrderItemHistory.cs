//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DBManager
{
    using System;
    using System.Collections.Generic;
    
    public partial class pms_OrderItemHistory
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public pms_OrderItemHistory()
        {
            this.pms_JobOrderItem = new HashSet<pms_JobOrderItem>();
            this.pms_SpareOrderItem = new HashSet<pms_SpareOrderItem>();
        }
    
        public int Id { get; set; }
        public string History_RU { get; set; }
        public string History_EN { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<pms_JobOrderItem> pms_JobOrderItem { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<pms_SpareOrderItem> pms_SpareOrderItem { get; set; }
    }
}
