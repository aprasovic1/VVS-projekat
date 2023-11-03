using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VVS_projekat.Models
{
    public class Book
    {
        public int ID { get; set; } // Unique identifier for the book
        public string ISBN { get; set; } // International Standard Book Number
        public string Author { get; set; } // Author of the book
        public string Title { get; set; } // Title of the book
        public string Genre { get; set; } // Genre of the book
        public decimal Price { get; set; } // Price of the book
        public string Status { get; set; } // Condition/Status of the book
        public int FK_Publisher { get; set; } // Foreign key for Publisher
        public int FK_Rating { get; set; } // Foreign key for Rating
    }

}
