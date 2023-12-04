using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VVS_projekat.Models
{
    public class Rating
    {
        [Key]
        public int RatingId { get; set; } // Unique identifier for the rating
        public int RatingValue { get; set; } // Numeric value of the rating
        public string Comment { get; set; } // Comment associated with the rating



        [ForeignKey("Book")]
        public int BookFk { get; set; }
        public Book Book { get; set; }


        [ForeignKey("LibraryUser")]
        public int LibraryMemberFk { set; get; }  
        public LibraryMember LibraryMember { get; set; }
        

        public Rating() { }


        

    }
}
