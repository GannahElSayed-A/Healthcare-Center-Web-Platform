using System.ComponentModel.DataAnnotations;

namespace GUI.Models
{
    public class Ancestry
    {
        [Key]
        public string MUsername { get; set; } // Primary Key and Foreign Key to Users.Username

        public string KinshipParent { get; set; }

        public string GeneticHistory { get; set; }

        [Required]
        public string BirthHistory { get; set; }
    }
}
