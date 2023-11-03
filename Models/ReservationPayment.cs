using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VVS_projekat.Models
{
    public class ReservationPayment
    {
        public int PaymentID { get; set; } // Unique identifier for the payment
        public int FK_Reservation { get; set; } // Foreign key for Reservation
        public DateTime PaymentDate { get; set; } // Date of payment
        public decimal Amount { get; set; } // Amount of the payment
    }

}
