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
    
    public partial class pms_Spare_Ship
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public pms_Spare_Ship()
        {
            this.pms_Job_Spare = new HashSet<pms_Job_Spare>();
            this.pms_Object_Spare = new HashSet<pms_Object_Spare>();
            this.pms_RepairListSpare = new HashSet<pms_RepairListSpare>();
            this.pms_Spare_File = new HashSet<pms_Spare_File>();
            this.pms_SpareOrderItem = new HashSet<pms_SpareOrderItem>();
            this.pms_StoreItem = new HashSet<pms_StoreItem>();
        }
    
        public int Id { get; set; }
        public int IdSpareLib { get; set; }
        public Nullable<int> IdShip { get; set; }
        public Nullable<int> IDOuter { get; set; }
        public int IdChangeInfo { get; set; }
    
        public virtual C_ChangeInfo C_ChangeInfo { get; set; }
        public virtual C_Ship C_Ship { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<pms_Job_Spare> pms_Job_Spare { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<pms_Object_Spare> pms_Object_Spare { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<pms_RepairListSpare> pms_RepairListSpare { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<pms_Spare_File> pms_Spare_File { get; set; }
        public virtual pms_SpareLib pms_SpareLib { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<pms_SpareOrderItem> pms_SpareOrderItem { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<pms_StoreItem> pms_StoreItem { get; set; }
    }
}