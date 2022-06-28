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
    
    public partial class C_Rank
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public C_Rank()
        {
            this.C_Person = new HashSet<C_Person>();
            this.hseq_Observation = new HashSet<hseq_Observation>();
            this.pms_Job = new HashSet<pms_Job>();
            this.pms_JobPlan = new HashSet<pms_JobPlan>();
            this.pms_StoreInventoryRecord = new HashSet<pms_StoreInventoryRecord>();
            this.pms_TechPlace = new HashSet<pms_TechPlace>();
            this.pms_TJob = new HashSet<pms_TJob>();
            this.prs_CheckListItem = new HashSet<prs_CheckListItem>();
            this.prs_PersonCard = new HashSet<prs_PersonCard>();
            this.prs_PersonContract = new HashSet<prs_PersonContract>();
        }
    
        public int Id { get; set; }
        public string Name_RU { get; set; }
        public bool IsResponsible { get; set; }
        public Nullable<int> IDOuter { get; set; }
        public int IdChangeInfo { get; set; }
        public Nullable<int> templdb_Code { get; set; }
        public Nullable<int> templdb_Version { get; set; }
        public string Name_EN { get; set; }
        public int SortNum { get; set; }
        public bool IsFlotRank { get; set; }
        public Nullable<int> СontractDuration { get; set; }
        public Nullable<int> СontractCountDaysForEarlyCompletion { get; set; }
        public Nullable<int> СontractCountDaysForDelayCompletion { get; set; }
    
        public virtual C_ChangeInfo C_ChangeInfo { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<C_Person> C_Person { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<hseq_Observation> hseq_Observation { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<pms_Job> pms_Job { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<pms_JobPlan> pms_JobPlan { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<pms_StoreInventoryRecord> pms_StoreInventoryRecord { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<pms_TechPlace> pms_TechPlace { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<pms_TJob> pms_TJob { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<prs_CheckListItem> prs_CheckListItem { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<prs_PersonCard> prs_PersonCard { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<prs_PersonContract> prs_PersonContract { get; set; }
    }
}
