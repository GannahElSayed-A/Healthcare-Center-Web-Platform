using System.Data;

using Microsoft.Data.SqlClient;
namespace GUI.Models
{
    public class DB
    {
        private string ConnectionString = "Data Source=NESMA;Initial Catalog=project9FINAL;Integrated Security=True;Trust Server Certificate=True";
        public SqlConnection con { get; set; }

        public DB() {

            con = new SqlConnection(ConnectionString);
        }
        public string GetCurrent()
        {
            string t = "";
            DataTable dt = new DataTable();
            string Query = "SELECT CONVERT(DATE, GETDATE()) AS CurrentDate; ";
            SqlCommand cmd = new SqlCommand(Query, con);
            try
            {
                con.Open();
                dt.Load(cmd.ExecuteReader());
                t = Convert.ToDateTime(dt.Rows[0][0]).ToString("yyyy-MM-dd");

            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);

            }
            finally
            {
                con.Close();
            }

            return t;

        }
        public DataTable GetSessions(string Musername)
        {

            DataTable dt = new DataTable();
            string Query = $"select DATENAME(WEEKDAY,SessionDateTime ) AS 'WeekDay', DATENAME(HOUR,SessionDateTime ) As T,DATENAME(MONTH, SessionDateTime) AS 'Month',DATENAME(DAY, SessionDateTime) AS 'Day',First_name,Last_name,Place, SessionDateTime,price\r\nfrom Set_Session join Users on D_username=Username\r\nwhere M_username='{Musername}' and SessionDateTime >'2024-03-20 02:00:00' and SessionDateTime<'2024-03-25 10:30:00' \r\nOrder by T Asc ";
            SqlCommand cmd = new SqlCommand(Query, con);
            try
            {
                con.Open();
                dt.Load(cmd.ExecuteReader());
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);

            }
            finally
            {
                con.Close();
            }
            return dt;
        }
        public int EditSession(Session s)
        {
            int result = 0;
            string Query = $"Update Set_Session Set Place='{s.Place}',Price={s.price} where M_username='{s.Uname}' and SessionDateTime='{s.Sdate}' ";

            //$"Update Set_Session\r\nSet Place='{s.Place}',Price={s.price}\r\nwhere M_username='{s.Uname}' and SessionDateTime='{s.Sdate}'\r\n";
            SqlCommand cmd = new SqlCommand(Query, con);
            try
            {
                con.Open();
                result = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                result = 0;
                Console.WriteLine(ex.Message);
            }
            finally
            {
                con.Close();
            }
            return result;

        }
        public int DeleteSession(string Musername, string sessionDate)
        {
            int result = 0;
            string Query = $"Delete from Set_Session where SessionDateTime='{sessionDate}' and M_username='{Musername}'";
            SqlCommand cmd = new SqlCommand(Query, con);
            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
                result = 1;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                result = 0;
            }
            finally
            {
                con.Close();
            }
            return result;
        }
        public DataTable AllSession(DateOnly sessionDate)
        {
            string Query = $"select DATENAME(HOUR,SessionDateTime ) As T,DATENAME(DAY, SessionDateTime) AS 'Day', DATENAME(WEEKDAY,SessionDateTime ) AS 'WeekDay',DATENAME(MONTH, SessionDateTime) AS 'Month',u1.First_name,u1.Last_name,Place,u2.First_name,u2.Last_name,price,M_username,SessionDateTime\r\nfrom (Set_Session join Users u1 on D_username=u1.Username)join Users u2 on M_username=u2.Username \r\nwhere   SessionDateTime >'{sessionDate} 02:00:00' and SessionDateTime<'{sessionDate} 10:30:00'\r\nOrder by T  Asc";
            SqlCommand cmd = new SqlCommand(Query, con);
            DataTable dt = new DataTable();
            try
            {
                con.Open();
                dt.Load(cmd.ExecuteReader());
            }
            catch (Exception ex)
            {
            }
            finally
            {
                con.Close();
            }
            return dt;
        }

        public int AddSession(Session s)
        {
            int result = 0;
            string Query = "INSERT INTO Set_Session values (@M_username, @SessionDateTime, @D_username, null, @Price, @Place)";
            SqlCommand cmd = new SqlCommand(Query, con);
            try
            {
                con.Open();
                cmd.Parameters.AddWithValue("@M_username", s.Uname);
                cmd.Parameters.AddWithValue("@SessionDateTime", s.sdate);
                cmd.Parameters.AddWithValue("@D_username", s.Dname);
                cmd.Parameters.AddWithValue("@Price", s.price);
                cmd.Parameters.AddWithValue("@Place", s.Place);
                cmd.ExecuteNonQuery();

                result = 1;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                result = 0;
            }
            finally
            {
                con.Close();
            }
            return result;

        }
        public DataTable GetAllRooms()
        {
            string Query = "select* from Department_Rooms;";
            SqlCommand cmd = new SqlCommand(Query, con);
            DataTable dt = new DataTable();
            try
            {
                con.Open();

                dt.Load(cmd.ExecuteReader());
            }
            catch (Exception ex)
            {
            }
            finally
            {
                con.Close();
            }
            return dt;


        }
        //Feedback
        public DataTable GetAllDoc(string M_usernaem)
        {
            string Query = $"select distinct D_username from Set_Session join Users on D_username=Username where M_username='{M_usernaem}'";
            SqlCommand cmd = new SqlCommand(Query, con);
            DataTable dt = new DataTable();
            try
            {
                con.Open();

                dt.Load(cmd.ExecuteReader());
            }
            catch (Exception ex)
            {
            }
            finally
            {
                con.Close();
            }
            return dt;


        }
        public DataTable GetAllFeebacks()
        {
            string Query = "select  DATENAME(HOUR,SessionDateTime ) As T,DATENAME(DAY, SessionDateTime) AS 'Day', DATENAME(WEEKDAY,SessionDateTime ) AS 'WeekDay',DATENAME(MONTH, SessionDateTime) AS 'Month',Feedback,First_name,Last_name\r\nfrom Set_Session join Users on D_username=Username\r\nwhere M_username='john_doe' and D_username='{D_Name}'\r\nOrder by T,SessionDateTime Asc";
            SqlCommand cmd = new SqlCommand(Query, con);
            DataTable dt = new DataTable();
            try
            {
                con.Open();

                dt.Load(cmd.ExecuteReader());
            }
            catch (Exception ex)
            {
            }
            finally
            {
                con.Close();
            }
            return dt;


        }
        public DataTable GetAllPatient()
        {
            string Query = "select M_username from Set_Session";
            SqlCommand cmd = new SqlCommand(Query, con);
            DataTable dt = new DataTable();
            try
            {
                con.Open();

                dt.Load(cmd.ExecuteReader());
            }
            catch (Exception ex)
            {
            }
            finally
            {
                con.Close();
            }
            return dt;


        }
        public DataTable GetFeedbacks(string Musername)
        {
            string Query = $"select  DATENAME(HOUR,SessionDateTime ) As T,DATENAME(DAY, SessionDateTime) AS 'Day', DATENAME(WEEKDAY,SessionDateTime ) AS 'WeekDay',DATENAME(MONTH, SessionDateTime) AS 'Month',Feedback,First_name,Last_name,D_username,SessionDateTime\r\nfrom Set_Session join Users on D_username=Username\r\nwhere M_username='{Musername}' \r\nOrder by D_username,SessionDateTime,T Asc";
            SqlCommand cmd = new SqlCommand(Query, con);
            DataTable dt = new DataTable();
            try
            {
                con.Open();
                dt.Load(cmd.ExecuteReader());
            }
            catch (Exception ex)
            {
            }
            finally
            {
                con.Close();
            }
            return dt;

        }


        public void EditFeedBacks(string Musername, string sessionDate, string Fd)
        {

            string Query = $"Update Set_Session\r\nSet Feedback='{Fd}'\r\nwhere M_username='{Musername}' and SessionDateTime='{sessionDate}'\r\n";
            SqlCommand cmd = new SqlCommand(Query, con);
            try
            {
                con.Open();
                cmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
            }
            finally
            {
                con.Close();
            }

        }
        public DataTable SearchFeedBacks(string Musername, string sessionDate, string Duser)
        {
            DataTable dt = new DataTable();
            string Query = $"select Feedback\r\nfrom Set_Session\r\nwhere M_username='{Musername}' and SessionDateTime='{sessionDate}'and D_username='{Duser}'";
            SqlCommand cmd = new SqlCommand(Query, con);

            try
            {
                con.Open();
                dt.Load(cmd.ExecuteReader());
            }
            catch (Exception ex)
            {
            }
            finally
            {
                con.Close();
            }
            return dt;
        }
        //Transaction
        public DataTable get_all_transactions()
        {
            DataTable dt = new DataTable();
            string query = "select Number, M_username, TransactionDateTime, Amount from Transactions order by Number ASC;";
            SqlCommand cmd = new SqlCommand(query, con);
            try { con.Open(); dt.Load(cmd.ExecuteReader()); }
            catch (Exception err) { Console.WriteLine(err); }
            finally { con.Close(); }
            return dt;

        }
        public DataTable get_user_transactions(string username)

        {
            DataTable dt = new DataTable();
            string query = $"select TransactionDateTime,Amount,Number from Transactions where M_username='{username}' order by Number ASC;";

            SqlCommand cmd = new SqlCommand(query, con);
            try { con.Open(); dt.Load(cmd.ExecuteReader()); }
            catch (Exception err) { Console.WriteLine(err); }
            finally { con.Close(); }
            return dt;

        }
        public void add_transactions(Transaction trans)
        {
            string query = $"INSERT INTO Transactions (M_username, TransactionDateTime, Amount, Number) VALUES ('{trans.username}', '{trans.Date}', '{trans.Amount}','{trans.TransactionId}');";
            SqlCommand cmd = new SqlCommand(query, con);
            try
            {
                con.Open();
                cmd.ExecuteNonQuery();

            }
            catch (Exception err) { Console.WriteLine(err); }
            finally { con.Close(); }


        }
        public void delete_transactions(int TransID)
        {
            string query = $"Delete from Transactions where  Number='{TransID}' ";

            SqlCommand cmd = new SqlCommand(query, con);
            try { con.Open(); cmd.ExecuteNonQuery(); }

            catch (Exception err) { Console.WriteLine(err); }
            finally { con.Close(); }


        }

        public void Edit_transactions(Transaction trans)
        {
            string query = $"Update Transactions Set Amount = '{trans.Amount}', TransactionDateTime ='{trans.Date}' Where Number='{trans.TransactionId}';";

            SqlCommand cmd = new SqlCommand(query, con);
            try { con.Open(); cmd.ExecuteNonQuery(); }

            catch (Exception err) { Console.WriteLine(err); }
            finally { con.Close(); }


        }
        //public void search_for_transaction(string username)
        //{
        //    string query = "";
        //    SqlCommand cmd = new SqlCommand(query, con);
        //    try { con.open(); dt.load(cmd.ExcuteReader()); }
        //    catch (Exception err) { }
        //    finally { con.Close(); }


        //}
        // Get all users

        public List<User> GetAllUsers()
        {
            List<User> users = new List<User>();
            string query = "SELECT Username, First_name, Second_name, Last_name, Email FROM Users";

            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    users.Add(new User
                    {
                        Username = reader["Username"].ToString() ?? string.Empty,
                        FirstName = $"{reader["First_name"]} {reader["Second_name"]} {reader["Last_name"]}",
                        Email = reader["Email"].ToString() ?? string.Empty,


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
                con.Close();
            }

            return users;
        }
        public Member GetMemberDetails(string username)
        {
            Member member = null;
            string query = "SELECT Nationality FROM Member WHERE Username = @Username";

            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@Username", username);

                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    member = new Member
                    {
                        Nationality = reader["Nationality"].ToString()
                    };
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            finally
            {
                con.Close();
            }
            return member;
        }
        public int GetUserHistoryCount(string username)
        {
            int count = 0;
            string query = "SELECT COUNT(*) AS HistoryCount FROM History WHERE Username = @Username";

            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@Username", username);

                count = (int)cmd.ExecuteScalar();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            finally
            {
                con.Close();
            }
            return count;
        }

        /////////////////////////////////////////////mangement//////////////////////////////////////////////////////////////////


        /////////////////////////////////////////////myaccount//////////////////////////////////////////////////////////////////


        public User GetUserByCredentials(string username, string password)
        {
            User user = null;
            string query = "SELECT Username, First_name, Second_name, Last_name, Email " +
                           "FROM Users WHERE Username = @Username AND Password = @Password";

            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@Username", username);
                cmd.Parameters.AddWithValue("@Password", password);

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    user = new User
                    {
                        Username = reader["Username"].ToString(),
                        FirstName = reader["First_name"].ToString(),
                        LastName = reader["Last_name"].ToString(),
                        Email = reader["Email"].ToString(),
                    };
                }

                reader.Close();
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            finally
            {
                con.Close();
            }

            return user;
        }

        public (Member, List<History>, List<Problem>) GetUserProfile(string username)
        {
            Member member = null;
            List<History> histories = new List<History>();
            List<Problem> problems = new List<Problem>();

            string query = @"
 SELECT M.Username, M.Nationality
 FROM Member M
 WHERE M.Username = @Username;

 SELECT H.ID, H.Type, H.Issue
 FROM History H
 WHERE H.Username = @Username;

 SELECT P.Type AS ProblemType, P.P_Descrption AS Description, P.problem_start_date AS StartDate
 FROM Problems P
 WHERE P.Username = @Username;
     ";

            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@Username", username);
                SqlDataReader reader = cmd.ExecuteReader();


                if (reader.Read())
                {
                    member = new Member
                    {
                        Username = reader["Username"].ToString(),
                        Nationality = reader["Nationality"].ToString(),
                    };
                }


                if (reader.NextResult())
                {
                    while (reader.Read())
                    {
                        histories.Add(new History
                        {
                            No = Convert.ToInt32(reader["ID"]),
                            Type = reader["Type"].ToString(),
                            Issue = reader["Issue"].ToString()
                        });
                    }
                }


                if (reader.NextResult())
                {
                    while (reader.Read())
                    {
                        problems.Add(new Problem
                        {
                            ProblemType = reader["ProblemType"].ToString(),
                            ProblemDescription = reader["Description"].ToString(),
                            StartDate = Convert.ToDateTime(reader["StartDate"])
                        });
                    }
                }

                reader.Close();
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            finally
            {
                con.Close();
            }

            return (member, histories, problems);
        }


        public List<SetSession> GetDoctorPatients(string doctorUsername)
        {
            List<SetSession> patients = new List<SetSession>();
            string query = @"
                    SELECT 
                        M_username AS PatientUsername, 
                        SessionDateTime, 
                        Place, 
                        Price 
                    FROM Set_Session 
                    WHERE D_username = @DoctorUsername";

            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@DoctorUsername", doctorUsername);

                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    patients.Add(new SetSession
                    {
                        PatientUsername = reader["PatientUsername"].ToString(),
                        SessionDateTime = Convert.ToDateTime(reader["SessionDateTime"]),
                        Place = reader["Place"].ToString(),
                        Price = Convert.ToDecimal(reader["Price"])
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
                con.Close();
            }

            return patients;
        }


    }
}
