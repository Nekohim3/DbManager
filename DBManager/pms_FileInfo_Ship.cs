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
    
    public partial class pms_FileInfo_Ship
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public pms_FileInfo_Ship()
        {
            this.pms_Job_File = new HashSet<pms_Job_File>();
            this.pms_Object_File = new HashSet<pms_Object_File>();
            this.pms_Spare_File = new HashSet<pms_Spare_File>();
        }
    
        public int Id { get; set; }
        public Nullable<int> IdShip { get; set; }
        public int IdFileInfo { get; set; }
        public Nullable<int> IDOuter { get; set; }
        public int IdChangeInfo { get; set; }
        public int IdChangeInfoFileData { get; set; }
    
        public virtual C_ChangeInfo C_ChangeInfo { get; set; }
        public virtual C_ChangeInfo C_ChangeInfo1 { get; set; }
        public virtual C_Ship C_Ship { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<pms_Job_File> pms_Job_File { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<pms_Object_File> pms_Object_File { get; set; }
        public virtual pms_FileLibInfo pms_FileLibInfo { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<pms_Spare_File> pms_Spare_File { get; set; }
    }
}
