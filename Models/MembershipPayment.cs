using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VVS_projekat.Models
{
    public class MembershipPayment
    {
        public int MembershipPaymentID { get; set; } // Unique identifier for the membership payment
        public int FK_Member { get; set; } // Foreign key for Member
        public DateTime PaymentDate { get; set; } // Date of payment
        public decimal Amount { get; set; } // Amount of the payment
        public decimal Discount { get; set; } // Discount applied
    }

}
