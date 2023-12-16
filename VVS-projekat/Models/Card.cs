using System;
using System.ComponentModel.DataAnnotations;

namespace VVS_projekat.Models
{
    public class Card
    {
        [Key]
        public int CardID { get; set; } 
        public string CardNumber { get; set; }
        public DateTime CardExpirationDate { get; set; }
        public int CardAmount { get; set; } // Amount of the voucher
        public Card() { }
    }
}
