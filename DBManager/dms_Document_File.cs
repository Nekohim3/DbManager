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
    
    public partial class dms_Document_File
    {
        public int Id { get; set; }
        public int IdFileInfo { get; set; }
        public int IdDocument { get; set; }
    
        public virtual dms_Document dms_Document { get; set; }
        public virtual dms_FileInfo dms_FileInfo { get; set; }
    }
}
