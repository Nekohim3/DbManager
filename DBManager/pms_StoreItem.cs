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
    
    public partial class pms_StoreItem
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public pms_StoreItem()
        {
            this.pms_StoreInventoryRecord = new HashSet<pms_StoreInventoryRecord>();
            this.pms_StoreReserve = new HashSet<pms_StoreReserve>();
            this.pms_StoreReserveForJobPlan = new HashSet<pms_StoreReserveForJobPlan>();
        }
    
        public int Id { get; set; }
        public int IdSpareLib { get; set; }
        public string Note { get; set; }
        public Nullable<int> IdLocation { get; set; }
        public Nullable<int> IDOuter { get; set; }
        public int IdChangeInfo { get; set; }
        public decimal Number { get; set; }
        public decimal NumberMinAdditionaly { get; set; }
        public Nullable<int> IdStoreSection { get; set; }
        public Nullable<int> IdSpare_Ship { get; set; }
        public bool IsActive { get; set; }
    
        public virtual C_ChangeInfo C_ChangeInfo { get; set; }
        public virtual pms_Spare_Ship pms_Spare_Ship { get; set; }
        public virtual pms_SpareLib pms_SpareLib { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<pms_StoreInventoryRecord> pms_StoreInventoryRecord { get; set; }
        public virtual pms_StoreLocation pms_StoreLocation { get; set; }
        public virtual pms_StoreSection pms_StoreSection { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<pms_StoreReserve> pms_StoreReserve { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<pms_StoreReserveForJobPlan> pms_StoreReserveForJobPlan { get; set; }
    }
}