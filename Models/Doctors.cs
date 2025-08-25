using System.ComponentModel.DataAnnotations;

namespace GUI.Models
{
    public class Doctors
    {

        [Key]
        public string Username { get; set; } // Primary Key

        [Phone]
        public string PhoneNumber { get; set; }

        [Required]
        public string DName { get; set; }
    }
}
