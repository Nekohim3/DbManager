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
    
    public partial class pms_RepairListRank
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public pms_RepairListRank()
        {
            this.C_Person = new HashSet<C_Person>();
            this.pms_RepairListApprovalStage = new HashSet<pms_RepairListApprovalStage>();
        }
    
        public int Id { get; set; }
        public string Name_ru { get; set; }
        public string Name_en { get; set; }
        public bool ForShip { get; set; }
        public int SortNum { get; set; }
        public bool IsActive { get; set; }
        public int IdChangeInfo { get; set; }
        public Nullable<int> IDOuter { get; set; }
    
        public virtual C_ChangeInfo C_ChangeInfo { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<C_Person> C_Person { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<pms_RepairListApprovalStage> pms_RepairListApprovalStage { get; set; }
    }
}