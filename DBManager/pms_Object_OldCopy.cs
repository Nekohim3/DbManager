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
    
    public partial class pms_Object_OldCopy
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Producer { get; set; }
        public string Type { get; set; }
        public Nullable<bool> IsCritical { get; set; }
        public Nullable<bool> IsSystem { get; set; }
        public string SerialNumber { get; set; }
        public int IdObject { get; set; }
        public string CatalogueNumber { get; set; }
        public string ProductionYear { get; set; }
    
        public virtual pms_Object pms_Object { get; set; }
    }
}
