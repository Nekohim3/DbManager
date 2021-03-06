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
    
    public partial class prs_PersonCard
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public prs_PersonCard()
        {
            this.prs_PersonCard_File = new HashSet<prs_PersonCard_File>();
            this.prs_PersonCard_InnerInfo = new HashSet<prs_PersonCard_InnerInfo>();
            this.prs_PersonCard_ShipType = new HashSet<prs_PersonCard_ShipType>();
            this.prs_PersonCardPhoto = new HashSet<prs_PersonCardPhoto>();
            this.prs_PersonContract = new HashSet<prs_PersonContract>();
            this.prs_PersonDocument = new HashSet<prs_PersonDocument>();
        }
    
        public int Id { get; set; }
        public string Name_RU { get; set; }
        public string Name_EN { get; set; }
        public string Surname_RU { get; set; }
        public string Surname_EN { get; set; }
        public string MiddleName_RU { get; set; }
        public string MiddleName_EN { get; set; }
        public int IdRank { get; set; }
        public Nullable<int> IdQualification { get; set; }
        public Nullable<int> IdCountry { get; set; }
        public System.DateTime BirthDay { get; set; }
        public string BirthPlace { get; set; }
        public string BirthPlaceShort { get; set; }
        public int Sex { get; set; }
        public Nullable<int> IDOuter { get; set; }
        public int IdChangeInfo { get; set; }
    
        public virtual C_ChangeInfo C_ChangeInfo { get; set; }
        public virtual C_Country C_Country { get; set; }
        public virtual C_Rank C_Rank { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<prs_PersonCard_File> prs_PersonCard_File { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<prs_PersonCard_InnerInfo> prs_PersonCard_InnerInfo { get; set; }
        public virtual prs_Qualification prs_Qualification { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<prs_PersonCard_ShipType> prs_PersonCard_ShipType { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<prs_PersonCardPhoto> prs_PersonCardPhoto { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<prs_PersonContract> prs_PersonContract { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<prs_PersonDocument> prs_PersonDocument { get; set; }
    }
}
