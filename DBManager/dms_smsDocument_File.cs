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
    
    public partial class dms_smsDocument_File
    {
        public int Id { get; set; }
        public int IdFileInfo { get; set; }
        public int IdSmsDocument { get; set; }
    
        public virtual dms_FileInfo dms_FileInfo { get; set; }
        public virtual dms_smsDocument dms_smsDocument { get; set; }
    }
}