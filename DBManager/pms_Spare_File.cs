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
    
    public partial class pms_Spare_File
    {
        public int Id { get; set; }
        public int IdSpare { get; set; }
        public int IdShipFileInfo { get; set; }
        public int IdTSpare_File { get; set; }
        public int IdChangeInfo { get; set; }
        public Nullable<int> IDOuter { get; set; }
    
        public virtual C_ChangeInfo C_ChangeInfo { get; set; }
        public virtual pms_FileInfo_Ship pms_FileInfo_Ship { get; set; }
        public virtual pms_SpareLib_File pms_SpareLib_File { get; set; }
        public virtual pms_Spare_Ship pms_Spare_Ship { get; set; }
    }
}
