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
    
    public partial class pms_Job_OldCopy
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public Nullable<int> IdResponsibleRank { get; set; }
        public string InstructionRef { get; set; }
        public string Instruction { get; set; }
        public string AdditionalInfo { get; set; }
        public string Note { get; set; }
        public Nullable<int> NormPerformance { get; set; }
        public Nullable<int> IdJobType { get; set; }
        public Nullable<bool> IsOneTime { get; set; }
        public Nullable<bool> IsSupervised { get; set; }
        public int IdJob { get; set; }
    
        public virtual pms_Job pms_Job { get; set; }
    }
}
