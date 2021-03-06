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
    
    public partial class pms_JobHistoryRecord
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public pms_JobHistoryRecord()
        {
            this.pms_Job = new HashSet<pms_Job>();
            this.pms_JobHistoryCounterRecord = new HashSet<pms_JobHistoryCounterRecord>();
            this.pms_JobHistoryRecord_File = new HashSet<pms_JobHistoryRecord_File>();
            this.pms_JobHistoryRecord1 = new HashSet<pms_JobHistoryRecord>();
            this.pms_StoreInventoryRecord = new HashSet<pms_StoreInventoryRecord>();
        }
    
        public int Id { get; set; }
        public int IdJob { get; set; }
        public Nullable<int> IdJobPlan { get; set; }
        public System.DateTime ExecuteDate { get; set; }
        public int RecordType { get; set; }
        public string JobDoneInfo { get; set; }
        public string ControlState { get; set; }
        public Nullable<int> IdResponsible { get; set; }
        public int IdChangeInfo { get; set; }
        public Nullable<int> IDOuter { get; set; }
        public Nullable<int> IdJobHistoryRecordPreviousPostponed { get; set; }
        public Nullable<bool> IsNotOnTime_Do { get; set; }
        public Nullable<bool> IsNotOnTime_PreviousPostponed { get; set; }
        public Nullable<bool> IsNotOnTime_Result { get; set; }
        public string History_RU { get; set; }
        public string History_EN { get; set; }
    
        public virtual C_ChangeInfo C_ChangeInfo { get; set; }
        public virtual C_Person C_Person { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<pms_Job> pms_Job { get; set; }
        public virtual pms_Job pms_Job1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<pms_JobHistoryCounterRecord> pms_JobHistoryCounterRecord { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<pms_JobHistoryRecord_File> pms_JobHistoryRecord_File { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<pms_JobHistoryRecord> pms_JobHistoryRecord1 { get; set; }
        public virtual pms_JobHistoryRecord pms_JobHistoryRecord2 { get; set; }
        public virtual pms_JobPlan pms_JobPlan { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<pms_StoreInventoryRecord> pms_StoreInventoryRecord { get; set; }
    }
}
