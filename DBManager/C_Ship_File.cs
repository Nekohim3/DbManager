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
    
    public partial class C_Ship_File
    {
        public int Id { get; set; }
        public int IdShip { get; set; }
        public int IdAttachedFileInfo { get; set; }
    
        public virtual C_AttachedFileInfo C_AttachedFileInfo { get; set; }
        public virtual C_Ship C_Ship { get; set; }
    }
}