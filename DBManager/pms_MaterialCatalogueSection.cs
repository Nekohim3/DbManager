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
    
    public partial class pms_MaterialCatalogueSection
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public pms_MaterialCatalogueSection()
        {
            this.pms_SpareLib = new HashSet<pms_SpareLib>();
            this.pms_MaterialCatalogueSection1 = new HashSet<pms_MaterialCatalogueSection>();
        }
    
        public int Id { get; set; }
        public Nullable<int> IdParent { get; set; }
        public int IdMaterialCatalogue { get; set; }
        public string Name { get; set; }
        public Nullable<int> IDOuter { get; set; }
        public int IdChangeInfo { get; set; }
        public string Ref1C { get; set; }
    
        public virtual C_ChangeInfo C_ChangeInfo { get; set; }
        public virtual pms_MaterialCatalogue pms_MaterialCatalogue { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<pms_SpareLib> pms_SpareLib { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<pms_MaterialCatalogueSection> pms_MaterialCatalogueSection1 { get; set; }
        public virtual pms_MaterialCatalogueSection pms_MaterialCatalogueSection2 { get; set; }
    }
}