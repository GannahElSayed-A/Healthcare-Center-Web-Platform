using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace GUI.Models
{
    public class Session
    {
        [BindProperty]
        [StringLength(20,ErrorMessage ="Name shouldn't Exceed 20 charecters")]
        public string Dname { get; set; }
        [BindProperty]
        [StringLength(20, ErrorMessage = "Name shouldn't Exceed 20 charecters")]
        public string Uname { get; set; }
        [BindProperty]
        [StringLength(100, ErrorMessage = "Feedback shouldn't Exceed 100 charecters")]
        public string fd { get; set; }
        [BindProperty]
        
        public DateTime sdate { get; set; }
        [BindProperty]
        public string Sdate { get; set; }
        [BindProperty]
        public string Place { get; set; }
        [BindProperty]
        public int price { get; set; }
        
    }
}
