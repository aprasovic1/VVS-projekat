using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VVS_projekat.Models
{
    public class Librarian: LibraryUser
    {
        [ForeignKey("LibraryUser")]
        public string LibraryUserId { get; set; }
        public LibraryUser LibraryUser { get; set; }

        //[Key]
        //public int LibrarianId { get; set; }
        public decimal Salary { get; set; }
        public string LibrarianPhoneNumber { get; set; }


        public Librarian() { }

    }
}
