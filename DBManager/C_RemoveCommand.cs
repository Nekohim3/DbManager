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
    
    public partial class C_RemoveCommand
    {
        public int Id { get; set; }
        public Nullable<int> IdShip { get; set; }
        public int EntityType { get; set; }
        public Nullable<int> IdEntity { get; set; }
        public Nullable<int> IDEntityOuter { get; set; }
        public System.DateTime CommandDate { get; set; }
        public bool IsReject { get; set; }
        public int ProgramVersion_1 { get; set; }
        public int ProgramVersion_2 { get; set; }
        public int ProgramVersion_3 { get; set; }
    
        public virtual C_Ship C_Ship { get; set; }
    }
}
