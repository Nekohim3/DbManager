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
    
    public partial class pms_RequisitionRank
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public pms_RequisitionRank()
        {
            this.C_Person = new HashSet<C_Person>();
            this.pms_JobReqApprovalStage = new HashSet<pms_JobReqApprovalStage>();
            this.pms_SpareReqApprovalStage = new HashSet<pms_SpareReqApprovalStage>();
        }
    
        public int Id { get; set; }
        public string Name_RU { get; set; }
        public string Name_EN { get; set; }
        public int SortNum { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<C_Person> C_Person { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<pms_JobReqApprovalStage> pms_JobReqApprovalStage { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<pms_SpareReqApprovalStage> pms_SpareReqApprovalStage { get; set; }
    }
}