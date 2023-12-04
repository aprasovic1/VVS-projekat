using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VVS_projekat.Models
{
    public class LibraryMember: LibraryUser
    {
        [ForeignKey("LibraryUser")]
        public string LibraryUserId { get; set; }
        public LibraryUser LibraryUser { get; set; }


        //[Key]
        // public int LibraryMemberId { get; set; }
        public DateTime MembershipExpirationDate { get; set; }


        public LibraryMember() { }
    }
}
