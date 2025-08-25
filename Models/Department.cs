using System.ComponentModel.DataAnnotations;

namespace GUI.Models
{
    public class Department
    {
        [Key]
        public string Name { get; set; } // Primary Key

        public string Description { get; set; }
    }
}
