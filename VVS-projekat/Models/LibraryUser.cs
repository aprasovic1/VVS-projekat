using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace VVS_projekat.Models
{
    public class LibraryUser: IdentityUser
    {
        public override string Id { get; set; }

       // [Key]
       // public int LibraryUserId { get; set; } // Unique identifier for the Library User
        public string FirstName { get; set; } // First name of the Library User
        public string LastName { get; set; } // Last name of the Library User
        public string EmailAddress { get; set; } // E-mail address of the Library User
        public string LibraryUsername { get; set; } // Username of the Library User
        public string LibraryUserPassword { get; set; } // Password of the Library User


        public LibraryUser() { }

    }
}
