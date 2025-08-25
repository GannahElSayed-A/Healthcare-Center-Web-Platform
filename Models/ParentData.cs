using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace GUI.Models
{
    public class ParentData
    {
        [Key, Column(Order = 0)]
        public string Phone { get; set; } // Composite Key

        [Key, Column(Order = 1)]
        public string MUsername { get; set; } // Composite Key and Foreign Key to Users.Username

        [Required]
        public string PName { get; set; }

        [Required]
        public string Sex { get; set; } // Check constraint: 'Male' or 'Female'

        public string Job { get; set; }

        public string EducationLevel { get; set; }

        [Required]
        public string Acception { get; set; }

        [Required]
        public int Age { get; set; } // Check constraint: Age > 0
    }
}
