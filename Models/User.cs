using Microsoft.Data.SqlClient;

namespace GUI.Models { 
    public class User

    {
        public string Username { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string SecondName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public int LoginAttempts { get; set; }
        public bool IsLocked { get; set; }


        public class UserManagemen
        {
            private DB db = new DB();

            // Get all users
            public List<User> GetUser()
            {
                List<User> users = new List<User>();
                string query = "SELECT  First_name, Nationality FROM Users"; 

                try
                {
                    db.con.Open();
                    SqlCommand cmd = new SqlCommand(query, db.con);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        users.Add(new User
                        {
                            Username = reader["Username"].ToString()?? string.Empty,
                            FirstName = reader["First_name"].ToString() ?? string.Empty,
                           

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

                return users;
            }


            // Search user by username
            //public List<User> SearchUsers(string username)
            //{
            //    List<User> users = new List<User>();
            //    string query = "SELECT Username, First_name, Second_name, Last_name, Email, Role FROM Users WHERE Username LIKE @username";

            //    try
            //    {
            //        db.con.Open();
            //        SqlCommand cmd = new SqlCommand(query, db.con);
            //        cmd.Parameters.AddWithValue("@username", $"%{username}%");
            //        SqlDataReader reader = cmd.ExecuteReader();

            //        while (reader.Read())
            //        {
            //            users.Add(new User
            //            {
            //                Username = reader["Username"].ToString(),
            //                FirstName = $"{reader["First_name"]} {reader["Second_name"]} {reader["Last_name"]}",
            //                Email = reader["Email"].ToString(),

            //            });
            //        }

            //        reader.Close();
            //    }
            //    catch (SqlException ex)
            //    {
            //        Console.WriteLine($"Error: {ex.Message}");
            //    }
            //    finally
            //    {
            //        db.con.Close();
            //    }

            //    return users;
            //}


            //// Add user

            //public bool AddUser(User user, string role, string department = null)
            //{
            //    string queryUser = "INSERT INTO Users (Username, First_name, Second_name, Last_name, Email, Password, LoginAttempts, IsLocked) " +
            //                       "VALUES (@Username, @FirstName, @SecondName, @LastName, @Email, @Password, 0, 0)";
            //    string queryRole = "";

            //    if (role == "Member")
            //    {
            //        queryRole = "INSERT INTO Member(Username, Nationality, Boosters, Birth_date, ST_Date, Sex, Time_Age, Arrangement_Between_brothers, Address_place, Living_with, Total_Financial_History) " +
            //                    "VALUES (@Username, @Nationality,@Boosters, @Birth_date, @ST_Date, @Sex, @Time_Age, @Arrangement_Between_brothers, @Address_place, @Living_with,@Total_Financial_History)";
            //    }
            //    else if (role == "Doctor")
            //    {
            //        queryRole = "INSERT INTO Doctor (Username, Phone_number, D_name) " +
            //                    "VALUES (@Username, @PhoneNumber, @Department)";
            //    }

            //    try
            //    {
            //        db.con.Open();
            //        SqlCommand cmdUser = new SqlCommand(queryUser, db.con);
            //        cmdUser.Parameters.AddWithValue("@Username", user.Username);
            //        cmdUser.Parameters.AddWithValue("@FirstName", user.FirstName);
            //        cmdUser.Parameters.AddWithValue("@SecondName", user.SecondName);
            //        cmdUser.Parameters.AddWithValue("@LastName", user.LastName);
            //        cmdUser.Parameters.AddWithValue("@Email", user.Email);
            //        cmdUser.Parameters.AddWithValue("@Password", user.Password);
            //        cmdUser.ExecuteNonQuery();

            //        if (!string.IsNullOrEmpty(queryRole))
            //        {
            //            SqlCommand cmdRole = new SqlCommand(queryRole, db.con);
            //            cmdRole.Parameters.AddWithValue("@Username", user.Username);

            //            if (role == "Member")
            //            {
            //                cmdRole.Parameters.AddWithValue("@Nationality", "Nationality");
            //                cmdRole.Parameters.AddWithValue("@Boosters", "Boosters");

            //                cmdRole.Parameters.AddWithValue("@BirthDate", DateTime.Now);
            //                cmdRole.Parameters.AddWithValue("@StartDate", DateTime.Now);
            //                cmdRole.Parameters.AddWithValue("@Sex", "Male");
            //                cmdRole.Parameters.AddWithValue("@TimeAge", "Time_Age");
            //                cmdRole.Parameters.AddWithValue("@Arrangement_Between_brothers", "Arrangement_Between_brothers");
            //                cmdRole.Parameters.AddWithValue("@Address_place", "Address_place");
            //                cmdRole.Parameters.AddWithValue("@Living_with", "Living_with");
            //                cmdRole.Parameters.AddWithValue("@Arrangement_Between_brothers", "Arrangement_Between_brothers");
            //                cmdRole.Parameters.AddWithValue("@Total_Financial_History", "Total_Financial_History");

            //            }
            //            else if (role == "Doctor")
            //            {
            //                cmdRole.Parameters.AddWithValue("@PhoneNumber", "01234567890");
            //                cmdRole.Parameters.AddWithValue("@Department", department);
            //            }

            //            cmdRole.ExecuteNonQuery();
            //        }

            //        return true;
            //    }
            //    catch (SqlException ex)
            //    {
            //        Console.WriteLine(ex.Message);
            //        return false;
            //    }
            //    finally
            //    {
            //        db.con.Close();
            //    }
            //}



            //// Delete user
            //public bool DeleteUser(int userId)
            //{
            //    string query = "DELETE FROM Users WHERE Id = @id";
            //    try
            //    {
            //        db.con.Open();
            //        SqlCommand cmd = new SqlCommand(query, db.con);
            //        cmd.Parameters.AddWithValue("@id", userId);
            //        cmd.ExecuteNonQuery();
            //        return true;
            //    }
            //    catch (SqlException ex)
            //    {
            //        Console.WriteLine(ex.Message);
            //        return false;
            //    }
            //    finally
            //    {
            //        db.con.Close();
            //    }
            //}

        }
    }
}    

