using System.ComponentModel.DataAnnotations;
using System;

namespace VVS_projekat.Models
{
    public class Voucher
    {
        [Key]
        public int VoucherID { get; set; } // Unique identifier for the voucher
        public DateTime VoucherDate { get; set; } // Date of purchase
        public string VoucherCode { get; set; } // Code of the voucher
        public int VoucherAmount { get; set; } // Amount of the voucher
        public int VoucherDiscount { get; set; } // Discount of the voucher
        public Voucher() { }
    }
}
