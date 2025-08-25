using Microsoft.Data.SqlClient;
using System.ComponentModel.DataAnnotations;

namespace GUI.Models
{
    public class Problems
    {
       
        
            public int No { get; set; }
            public string ProblemType { get; set; } = string.Empty; 
            public string Description { get; set; } = string.Empty;
            public DateTime StartDate { get; set; }

        private DB db = new DB();

        // Get all users
        public List<Problems> GetUser()
        {
            List<Problems> Problems = new List<Problems>();
            string query = "SELECT  No, Type,Issue FROM History";

            try
            {
                db.con.Open();
                SqlCommand cmd = new SqlCommand(query, db.con);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Problems.Add(new Problems
                    {
                        No = (int)reader["No"],
                        ProblemType = reader["ProblemType"].ToString() ?? string.Empty,
                        Description = reader["Description"].ToString() ?? string.Empty,
                        StartDate = (DateTime)reader["StartDate"],


                    });
                }

                reader.Close();
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            finally
            {
                db.con.Close();
            }

            return Problems;
        }
    }

}
