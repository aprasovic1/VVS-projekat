using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VVS_projekat.Models
{
    public class LibraryMember
    {
        [Key]
        public int LibraryMemberId { get; set; }
        public string FirstName { get; set; } // First name of the Library User
        public string LastName { get; set; } // Last name of the Library User
        public string EmailAddress { get; set; } // E-mail address of the Library User
        public string LibraryUsername { get; set; } // Username of the Library User
        public string LibraryUserPassword { get; set; }
        public DateTime MembershipExpirationDate { get; set; }


        public LibraryMember() { }
    }
}
