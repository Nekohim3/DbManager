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
    
    public partial class pms_StoreReserveForJobPlan
    {
        public int Id { get; set; }
        public int IdStoreItem { get; set; }
        public Nullable<int> IdShip { get; set; }
        public decimal Number { get; set; }
        public int IdJobPlan { get; set; }
    
        public virtual C_Ship C_Ship { get; set; }
        public virtual pms_JobPlan pms_JobPlan { get; set; }
        public virtual pms_StoreItem pms_StoreItem { get; set; }
    }
}
