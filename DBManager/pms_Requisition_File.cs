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
    
    public partial class pms_Requisition_File
    {
        public int Id { get; set; }
        public int IdFileInfo { get; set; }
        public int IdRequisition { get; set; }
    
        public virtual pms_FileInfo pms_FileInfo { get; set; }
        public virtual pms_Requisition pms_Requisition { get; set; }
    }
}