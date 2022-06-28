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
    
    public partial class C_ExportImportInfo
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public C_ExportImportInfo()
        {
            this.C_ChangeInfo = new HashSet<C_ChangeInfo>();
            this.C_ChangeInfo1 = new HashSet<C_ChangeInfo>();
            this.C_ExportImportStepStatistic = new HashSet<C_ExportImportStepStatistic>();
        }
    
        public int Id { get; set; }
        public int Number { get; set; }
        public Nullable<int> IdShip { get; set; }
        public System.DateTime CreateDate { get; set; }
        public Nullable<System.DateTime> LoadDate { get; set; }
        public Nullable<int> IdUserCreator { get; set; }
        public Nullable<int> IdUserLoader { get; set; }
        public string UserOuterCreator { get; set; }
        public bool IsExport { get; set; }
        public int IdPackage { get; set; }
        public int PackageSize { get; set; }
        public Nullable<int> LastExecutedStep { get; set; }
        public int ProgramVersion_1 { get; set; }
        public int ProgramVersion_2 { get; set; }
        public int ProgramVersion_3 { get; set; }
        public int ExecutedPercent { get; set; }
        public bool HasFiles { get; set; }
        public int RecoveryNumber { get; set; }
        public bool IsRecoveryPackage { get; set; }
        public Nullable<bool> IsUploaded { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<C_ChangeInfo> C_ChangeInfo { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<C_ChangeInfo> C_ChangeInfo1 { get; set; }
        public virtual C_ExportImportPackage C_ExportImportPackage { get; set; }
        public virtual C_Ship C_Ship { get; set; }
        public virtual C_User C_User { get; set; }
        public virtual C_User C_User1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<C_ExportImportStepStatistic> C_ExportImportStepStatistic { get; set; }
    }
}
