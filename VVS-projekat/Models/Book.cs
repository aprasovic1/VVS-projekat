using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VVS_projekat.Models
{
    public class Book
    {
        [Key]
        public int BookId { get; set; } // Unique identifier for the book
        public string Author { get; set; } // Author of the book
        public string Title { get; set; } // Title of the book
        public string Genre { get; set; } // Genre of the book
        public decimal Price { get; set; } // Price of the book
        public string Status { get; set; } // Condition/Status of the book


        [ForeignKey("Publisher")]
        public int PublisherFk { set; get; }
        public Publisher Publisher { get; set; }


        [ForeignKey("Reservation")]
        public int ReservationFk { set; get; }
        public Reservation Reservation { get; set; }


        public Book() { }


    }
}
