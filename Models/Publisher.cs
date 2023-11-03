using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VVS_projekat.Models
{
    public class Publisher
    {
        public int PublisherID { get; set; } // Unique identifier for the publisher
        public string PublisherName { get; set; } // Name of the publisher
        public string Address { get; set; } // Address of the publisher
        public DateTime DatePublished { get; set; } // Date of publication
    }

}
