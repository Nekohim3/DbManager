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
    
    public partial class prs_PersonCard_InnerInfo
    {
        public int Id { get; set; }
        public int IdPersonCard { get; set; }
        public Nullable<System.DateTime> ReadyWorkDate { get; set; }
        public string AdditionalInfo { get; set; }
        public Nullable<int> FamilyStatus { get; set; }
        public string PhoneNumber { get; set; }
        public string PhoneNumberAdditional { get; set; }
        public Nullable<int> ChildrenNumber { get; set; }
        public Nullable<int> EnglishKnowledge { get; set; }
        public string OtherLanguagesKnowledge { get; set; }
        public string Email { get; set; }
        public string RegistrationAddress { get; set; }
        public string ResidentialAddress { get; set; }
        public string NextOfKinAndRelationship { get; set; }
        public string NextOfKinAddress { get; set; }
        public int IdChangeInfo { get; set; }
    
        public virtual C_ChangeInfo C_ChangeInfo { get; set; }
        public virtual prs_PersonCard prs_PersonCard { get; set; }
    }
}