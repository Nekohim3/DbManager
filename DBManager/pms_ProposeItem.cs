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
    
    public partial class pms_ProposeItem
    {
        public int Id { get; set; }
        public Nullable<int> IdShip { get; set; }
        public int EntityType { get; set; }
        public int IdEntity { get; set; }
        public int Type { get; set; }
        public System.DateTime ProposeDate { get; set; }
        public string PropositionText { get; set; }
        public string UserPropositionText { get; set; }
        public Nullable<System.DateTime> AnswerDate { get; set; }
        public Nullable<int> Result { get; set; }
        public string AnswerText { get; set; }
        public string UserOuter { get; set; }
        public Nullable<int> IdUser { get; set; }
        public Nullable<int> IDOuter { get; set; }
    
        public virtual C_Ship C_Ship { get; set; }
        public virtual C_User C_User { get; set; }
    }
}
