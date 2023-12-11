using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VVS_projekat.Models
{
    public class Reservation
    {
        [Key]
        public int ReservationId { get; set; } // Unique identifier for the reservation
        public DateTime IssuedDate { get; set; } // Date of issuance
        public DateTime ReturnDate { get; set; } // Date of return
        public string? Status { get; set; } // Status of the reservation


        [ForeignKey("LibraryMember")]
        public int? LibraryMemberFk { set; get; }
        public LibraryMember LibraryMember { get; set; }


        public Reservation() { }
        
    }
}
