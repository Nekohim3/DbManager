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
    
    public partial class prs_PersonContract
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public prs_PersonContract()
        {
            this.prs_PersonContract_File = new HashSet<prs_PersonContract_File>();
        }
    
        public int Id { get; set; }
        public int IdPersonCard { get; set; }
        public Nullable<int> IdShip { get; set; }
        public int IdRank { get; set; }
        public Nullable<int> IdСrewСhangePlan { get; set; }
        public Nullable<int> IdCheckList { get; set; }
        public Nullable<System.DateTime> ContractStartDate { get; set; }
        public Nullable<System.DateTime> ContractEndDate { get; set; }
        public Nullable<int> IDOuter { get; set; }
        public int IdChangeInfo { get; set; }
    
        public virtual C_ChangeInfo C_ChangeInfo { get; set; }
        public virtual C_Rank C_Rank { get; set; }
        public virtual C_Ship C_Ship { get; set; }
        public virtual prs_CheckList prs_CheckList { get; set; }
        public virtual prs_PersonCard prs_PersonCard { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<prs_PersonContract_File> prs_PersonContract_File { get; set; }
        public virtual prs_СrewСhangePlan prs_СrewСhangePlan { get; set; }
    }
}