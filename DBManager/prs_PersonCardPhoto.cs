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
    
    public partial class prs_PersonCardPhoto
    {
        public int Id { get; set; }
        public int IdPersonCard { get; set; }
        public int IdFileInfo { get; set; }
    
        public virtual pms_FileInfo pms_FileInfo { get; set; }
        public virtual prs_PersonCard prs_PersonCard { get; set; }
    }
}