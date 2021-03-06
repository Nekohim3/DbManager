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
    
    public partial class pms_TJob
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public pms_TJob()
        {
            this.pms_Job = new HashSet<pms_Job>();
            this.pms_TJob_File = new HashSet<pms_TJob_File>();
            this.pms_TJobPeriod = new HashSet<pms_TJobPeriod>();
            this.pms_TJob_Spare = new HashSet<pms_TJob_Spare>();
        }
    
        public int Id { get; set; }
        public string Description { get; set; }
        public Nullable<int> IdResponsibleRank { get; set; }
        public string InstructionRef { get; set; }
        public string Instruction { get; set; }
        public string AdditionalInfo { get; set; }
        public int IdTObject { get; set; }
        public Nullable<int> NormPerformance { get; set; }
        public Nullable<int> IdJobType { get; set; }
        public bool IsOneTime { get; set; }
        public bool IsSupervised { get; set; }
        public int IdChangeInfo { get; set; }
        public bool IsInMonitoring { get; set; }
        public string ShortNameForMonitoring { get; set; }
    
        public virtual C_ChangeInfo C_ChangeInfo { get; set; }
        public virtual C_Rank C_Rank { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<pms_Job> pms_Job { get; set; }
        public virtual pms_JobType pms_JobType { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<pms_TJob_File> pms_TJob_File { get; set; }
        public virtual pms_TObject pms_TObject { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<pms_TJobPeriod> pms_TJobPeriod { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<pms_TJob_Spare> pms_TJob_Spare { get; set; }
    }
}
