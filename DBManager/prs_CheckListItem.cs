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
    
    public partial class prs_CheckListItem
    {
        public int Id { get; set; }
        public int IdCheckList { get; set; }
        public int IdDocumentType { get; set; }
        public int IdRank { get; set; }
        public Nullable<int> IDOuter { get; set; }
        public int IdChangeInfo { get; set; }
    
        public virtual C_ChangeInfo C_ChangeInfo { get; set; }
        public virtual C_Rank C_Rank { get; set; }
        public virtual prs_CheckList prs_CheckList { get; set; }
        public virtual prs_DocumentType prs_DocumentType { get; set; }
    }
}
