using System.ComponentModel.DataAnnotations;

namespace GUI.Models
{
    public class DepartmentRooms
    {
        [Key]
        public int RoomID { get; set; } // Primary Key

        [Required]
        public string DName { get; set; } // Foreign Key to Department.Name
    }
}
