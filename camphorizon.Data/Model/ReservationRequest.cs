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
    
    public partial class ReservationRequest
    {
        public int ID { get; set; }
        public Nullable<System.DateTime> CheckInDate { get; set; }
        public Nullable<System.DateTime> CheckOutDate { get; set; }
        public Nullable<int> ForDays { get; set; }
        public Nullable<int> Adults { get; set; }
        public Nullable<int> Children { get; set; }
        public string Email { get; set; }
        public string Contact { get; set; }
        public Nullable<System.DateTime> DateCreated { get; set; }
    }
}