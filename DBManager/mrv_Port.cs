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
    
    public partial class mrv_Port
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public mrv_Port()
        {
            this.mrv_Voyage = new HashSet<mrv_Voyage>();
            this.mrv_VoyagePeriod = new HashSet<mrv_VoyagePeriod>();
            this.mrv_VoyagePeriod1 = new HashSet<mrv_VoyagePeriod>();
        }
    
        public int Id { get; set; }
        public string Name_RU { get; set; }
        public bool IsEC { get; set; }
        public bool IsActive { get; set; }
        public Nullable<int> IDOuter { get; set; }
        public int IdChangeInfo { get; set; }
        public string Name_EN { get; set; }
    
        public virtual C_ChangeInfo C_ChangeInfo { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<mrv_Voyage> mrv_Voyage { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<mrv_VoyagePeriod> mrv_VoyagePeriod { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<mrv_VoyagePeriod> mrv_VoyagePeriod1 { get; set; }
    }
}
