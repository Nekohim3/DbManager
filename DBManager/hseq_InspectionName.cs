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
    
    public partial class hseq_InspectionName
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public hseq_InspectionName()
        {
            this.hseq_Inspection = new HashSet<hseq_Inspection>();
            this.hseq_InspectionPlan = new HashSet<hseq_InspectionPlan>();
        }
    
        public int Id { get; set; }
        public string Name { get; set; }
        public int Type { get; set; }
        public bool IsSIRE { get; set; }
        public bool IsFlotInspection { get; set; }
        public bool IsShowInPlan { get; set; }
        public bool IsActive { get; set; }
        public Nullable<int> IDOuter { get; set; }
        public int IdChangeInfo { get; set; }
    
        public virtual C_ChangeInfo C_ChangeInfo { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<hseq_Inspection> hseq_Inspection { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<hseq_InspectionPlan> hseq_InspectionPlan { get; set; }
    }
}
