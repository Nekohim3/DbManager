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
    
    public partial class dms_smsPartition
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public dms_smsPartition()
        {
            this.dms_smsDocument = new HashSet<dms_smsDocument>();
            this.dms_smsDocument_Ship_NotVisible = new HashSet<dms_smsDocument_Ship_NotVisible>();
            this.dms_smsPartition1 = new HashSet<dms_smsPartition>();
        }
    
        public int Id { get; set; }
        public string Name_rus { get; set; }
        public string Name_en { get; set; }
        public string Number { get; set; }
        public Nullable<int> IDOuter { get; set; }
        public int IdChangeInfo { get; set; }
        public Nullable<int> ParentId { get; set; }
        public Nullable<int> NumberPP { get; set; }
    
        public virtual C_ChangeInfo C_ChangeInfo { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<dms_smsDocument> dms_smsDocument { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<dms_smsDocument_Ship_NotVisible> dms_smsDocument_Ship_NotVisible { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<dms_smsPartition> dms_smsPartition1 { get; set; }
        public virtual dms_smsPartition dms_smsPartition2 { get; set; }
    }
}
