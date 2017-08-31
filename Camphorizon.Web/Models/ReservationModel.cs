using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Camphorizon.Web.Models
{
    public class ReservationModel
    {
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public int Adults { get; set; }
        public int Children { get; set; }
        public string Email { get; set; }
        public string Contact { get; set; }
    }
}