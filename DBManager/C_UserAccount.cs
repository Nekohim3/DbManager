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
    
    public partial class C_UserAccount
    {
        public int Id { get; set; }
        public int IdUser { get; set; }
        public int IdRole { get; set; }
    
        public virtual C_Role C_Role { get; set; }
        public virtual C_User C_User { get; set; }
    }
}