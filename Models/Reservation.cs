using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VVS_projekat.Models
{
    public class Reservation
    {
        public int ID_Reservations { get; set; } // Unique identifier for the reservation
        public DateTime DateIssued { get; set; } // Date of issuance
        public DateTime ReturnDate { get; set; } // Date of return
        public string Status { get; set; } // Status of the reservation
        public int FK_Book { get; set; } // Foreign key for Book
        public int FK_Reservation { get; set; } // Foreign key for Reservation
    }

}
