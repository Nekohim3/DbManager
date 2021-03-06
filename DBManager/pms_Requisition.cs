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
    
    public partial class pms_Requisition
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public pms_Requisition()
        {
            this.pms_JobOrderItem = new HashSet<pms_JobOrderItem>();
            this.pms_Requisition_File = new HashSet<pms_Requisition_File>();
            this.pms_Requisition_InnerInfo = new HashSet<pms_Requisition_InnerInfo>();
            this.pms_SpareOrderItem = new HashSet<pms_SpareOrderItem>();
        }
    
        public int Id { get; set; }
        public System.DateTime Date { get; set; }
        public int Status { get; set; }
        public string Description { get; set; }
        public int IdRequisitionDepartment { get; set; }
        public Nullable<int> IdRequisitionType { get; set; }
        public bool IsUrgent { get; set; }
        public string CommentForShip { get; set; }
        public Nullable<System.DateTime> PerformToDate { get; set; }
        public Nullable<System.DateTime> ClosedDate { get; set; }
        public Nullable<int> IdShip { get; set; }
        public Nullable<int> IDOuter { get; set; }
        public int IdChangeInfo { get; set; }
        public int Kind { get; set; }
        public string Port { get; set; }
        public bool CreatedByCompany { get; set; }
        public Nullable<int> IdRequisitionDestination { get; set; }
        public string Number { get; set; }
        public Nullable<int> ApprovalType { get; set; }
    
        public virtual C_ChangeInfo C_ChangeInfo { get; set; }
        public virtual C_Ship C_Ship { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<pms_JobOrderItem> pms_JobOrderItem { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<pms_Requisition_File> pms_Requisition_File { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<pms_Requisition_InnerInfo> pms_Requisition_InnerInfo { get; set; }
        public virtual pms_RequisitionDepartment pms_RequisitionDepartment { get; set; }
        public virtual pms_RequisitionDestination pms_RequisitionDestination { get; set; }
        public virtual pms_RequisitionType pms_RequisitionType { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<pms_SpareOrderItem> pms_SpareOrderItem { get; set; }
    }
}
