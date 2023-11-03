using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VVS_projekat.Models
{
    public class Member
    {
        public int ID { get; set; } // Unique identifier for the member
        public string FirstName { get; set; } // First name of the member
        public string LastName { get; set; } // Last name of the member
        public string Address { get; set; } // Address of the member
        public DateTime MembershipExpirationDate { get; set; } // Date when the membership expires
        public int FK_Reservation { get; set; } // Foreign key for Reservation
    }

}
