using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VVS_projekat.Models
{
    public class Rating
    {
        public int RatingID { get; set; } // Unique identifier for the rating
        public int FK_Book { get; set; } // Foreign key for Book
        public int FK_Member { get; set; } // Foreign key for Member
        public int RatingValue { get; set; } // Numeric value of the rating
        public string Comment { get; set; } // Comment associated with the rating
    }

}
