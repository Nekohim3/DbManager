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
    
    public partial class C_UserAccessByUnit
    {
        public int Id { get; set; }
        public int IdUser { get; set; }
        public int UnitTypeId { get; set; }
        public Nullable<int> IdUnit { get; set; }
    
        public virtual C_User C_User { get; set; }
    }
}
