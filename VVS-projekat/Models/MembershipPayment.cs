using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VVS_projekat.Models
{
    public class MembershipPayment
    {
        [Key]
        public int MembershipPaymentId { get; set; } // Unique identifier for the membership payment
        public DateTime PaymentDate { get; set; } // Date of payment
        public decimal Amount { get; set; } // Amount of the payment
        public decimal Discount { get; set; } // Discount applied



        [ForeignKey("LibraryUser")]
        public int LibraryMemberFk { set; get; }
        public LibraryMember LibraryMember { set; get; }



        public MembershipPayment() { }
    }
}
