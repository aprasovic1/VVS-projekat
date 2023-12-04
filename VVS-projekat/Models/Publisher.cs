using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VVS_projekat.Models
{
    public class Publisher
    {
        [Key]
        public int PublisherId { get; set; } // Unique identifier for the publisher
        public string PublisherName { get; set; } // Name of the publisher
        public string Address { get; set; } // Address of the publisher
        public DateTime PublishedDate { get; set; } // Date of publication


        public Publisher() { }

    }
}

