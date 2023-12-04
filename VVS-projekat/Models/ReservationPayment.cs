using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VVS_projekat.Models
{
    public class ReservationPayment
    {
        [Key, ForeignKey("Reservation")]
        public int PaymentId { get; set; } // Unique identifier for the payment
        public DateTime PaymentDate { get; set; } // Date of payment
        public decimal Amount { get; set; } // Amount of the payment

        public Reservation Reservation { get; set; }

        public ReservationPayment() { }
    }
}
