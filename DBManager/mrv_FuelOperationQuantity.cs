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
    
    public partial class mrv_FuelOperationQuantity
    {
        public int Id { get; set; }
        public int IdFuelType { get; set; }
        public int IdFuelOperation { get; set; }
        public decimal Quantity { get; set; }
        public Nullable<decimal> Cost { get; set; }
        public Nullable<int> IDOuter { get; set; }
        public int IdChangeInfo { get; set; }
    
        public virtual C_ChangeInfo C_ChangeInfo { get; set; }
        public virtual mrv_FuelOperation mrv_FuelOperation { get; set; }
        public virtual mrv_FuelType mrv_FuelType { get; set; }
    }
}
