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
    
    public partial class pms_RepairListPartition
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int SortOrder { get; set; }
        public bool IsActive { get; set; }
        public int IdChangeInfo { get; set; }
        public Nullable<int> IDOuter { get; set; }
    
        public virtual C_ChangeInfo C_ChangeInfo { get; set; }
    }
}
