//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace camphorizon.Data.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class PackageFacilityMapping
    {
        public int Id { get; set; }
        public Nullable<int> PackageID { get; set; }
        public Nullable<int> FacilityID { get; set; }
    
        public virtual Package Package { get; set; }
    }
}
