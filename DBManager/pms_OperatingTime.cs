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
    
    public partial class pms_OperatingTime
    {
        public int Id { get; set; }
        public int IdRepair { get; set; }
        public int IdCatalog { get; set; }
        public string LastOverhaul { get; set; }
        public string TypeLast { get; set; }
        public string Condition { get; set; }
        public string RepairType { get; set; }
        public string Note { get; set; }
        public Nullable<int> IdCounterType { get; set; }
        public Nullable<int> IDOuter { get; set; }
        public int IdChangeInfo { get; set; }
    
        public virtual C_ChangeInfo C_ChangeInfo { get; set; }
        public virtual pms_CounterType pms_CounterType { get; set; }
        public virtual pms_Object pms_Object { get; set; }
        public virtual pms_RepairList pms_RepairList { get; set; }
    }
}