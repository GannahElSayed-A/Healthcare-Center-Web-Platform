using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace GUI.Models
{
    public class SetSession
    {
        [Key, Column(Order = 0)]
        public string MUsername { get; set; } // Foreign Key to Member.Username

        [Key, Column(Order = 1)]
        public DateTime SessionDateTime { get; set; } // Composite Key

        [Required]
        public string DUsername { get; set; } // Foreign Key to Doctor.Username

        public string Feedback { get; set; }

        [Required]
        public decimal Price { get; set; }

        public string Place { get; set; }
       
        public string? PatientUsername { get; internal set; }
       
    }
}
