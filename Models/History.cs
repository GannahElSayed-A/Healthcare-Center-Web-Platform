
using GUI.Models;
using Microsoft.Data.SqlClient;
using System.ComponentModel.DataAnnotations;

namespace GUI.Models
{
    public class History
    {


        public int No { get; set; }
        public string Type { get; set; } = string.Empty;
        public string Issue { get; set; } = string.Empty;



        
        
    }
}