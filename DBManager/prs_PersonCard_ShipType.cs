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
    
    public partial class prs_PersonCard_ShipType
    {
        public int Id { get; set; }
        public int IdPersonCard { get; set; }
        public int IdShipType { get; set; }
        public Nullable<int> IDOuter { get; set; }
        public int IdChangeInfo { get; set; }
    
        public virtual C_ChangeInfo C_ChangeInfo { get; set; }
        public virtual C_ShipType C_ShipType { get; set; }
        public virtual prs_PersonCard prs_PersonCard { get; set; }
    }
}