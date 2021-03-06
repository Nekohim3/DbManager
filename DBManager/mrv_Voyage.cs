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
    
    public partial class mrv_Voyage
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public mrv_Voyage()
        {
            this.mrv_VoyagePeriod = new HashSet<mrv_VoyagePeriod>();
        }
    
        public int Id { get; set; }
        public Nullable<int> IdShip { get; set; }
        public int Number { get; set; }
        public bool IsClose { get; set; }
        public Nullable<int> IdPortEnd { get; set; }
        public string PortEndOther { get; set; }
        public string Description_Ship { get; set; }
        public string Description_Company { get; set; }
        public Nullable<int> IDOuter { get; set; }
        public int IdChangeInfo { get; set; }
    
        public virtual C_ChangeInfo C_ChangeInfo { get; set; }
        public virtual C_Ship C_Ship { get; set; }
        public virtual mrv_Port mrv_Port { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<mrv_VoyagePeriod> mrv_VoyagePeriod { get; set; }
    }
}
