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
    
    public partial class pms_SpareLib_OldCopy
    {
        public int Id { get; set; }
        public string CatalogueNumber { get; set; }
        public string Name { get; set; }
        public string Parameters { get; set; }
        public Nullable<int> Unit { get; set; }
        public string AdditionalInfo { get; set; }
        public Nullable<int> IdMCatalogSection { get; set; }
        public int IdSpareLib { get; set; }
    
        public virtual pms_SpareLib pms_SpareLib { get; set; }
    }
}
