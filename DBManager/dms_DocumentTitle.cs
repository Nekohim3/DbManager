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
    
    public partial class dms_DocumentTitle
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public dms_DocumentTitle()
        {
            this.dms_Document = new HashSet<dms_Document>();
            this.dms_DocumentTitle_File = new HashSet<dms_DocumentTitle_File>();
        }
    
        public int Id { get; set; }
        public string Name_rus { get; set; }
        public string Name_en { get; set; }
        public string FormNumber { get; set; }
        public string Number { get; set; }
        public int IdPartition { get; set; }
        public Nullable<int> IDOuter { get; set; }
        public int IdChangeInfo { get; set; }
        public Nullable<int> Notification { get; set; }
    
        public virtual C_ChangeInfo C_ChangeInfo { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<dms_Document> dms_Document { get; set; }
        public virtual dms_Partition dms_Partition { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<dms_DocumentTitle_File> dms_DocumentTitle_File { get; set; }
    }
}