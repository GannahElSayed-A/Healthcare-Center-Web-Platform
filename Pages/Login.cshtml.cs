using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text;
using Microsoft.Data.SqlClient;
using System.Net.Mail;
using System.Net;


namespace GUI.Pages
{
    public class LoginModel : PageModel
    {
        [BindProperty]
        public string Username { get; set; } = default!;

        [BindProperty]
        public string Password { get; set; } = default!;

        public string? ErrorMessage { get; set; }


        private readonly string connectionString = "Server=NESMA;Database=project9FINAL;Trusted_Connection=True;TrustServerCertificate=True;";

        public void OnGet()
        {
            // Reset error messages
            ErrorMessage = null;
        }

        public IActionResult OnPost()
        {
            if (string.IsNullOrEmpty(Username) || string.IsNullOrEmpty(Password))
            {
                ErrorMessage = "Username and password are required.";
                return Page();
            }

            try
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Fetch user details
                    string query = "SELECT Password, LoginAttempts, IsLocked FROM Users WHERE Username = @Username";
                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Username", Username);

                        using (var reader = command.ExecuteReader())
                        {
                            if (!reader.Read())
                            {
                                ErrorMessage = "Invalid username or password.";
                                return Page();
                            }

                            // Retrieve user data
                            string storedPassword = reader["Password"].ToString()!;
                            int loginAttempts = Convert.ToInt32(reader["LoginAttempts"]);
                            bool isLocked = Convert.ToBoolean(reader["IsLocked"]);

                            // Check if account is locked
                            if (isLocked)
                            {
                                ErrorMessage = "Your account is locked due to too many failed login attempts.";
                                return Page();
                            }

                            // Validate password
                            if (storedPassword != Password)
                            {
                                reader.Close(); // Close reader before running another query
                                IncrementLoginAttempts(connection);
                                ErrorMessage = "Invalid username or password.";
                                return Page();
                            }
                        } // Automatically closes the reader here

                        // Successful login
                        ResetLoginAttempts(connection);

                        string userType = DetermineUserType(connection, Username);
                        if (!string.IsNullOrEmpty(userType))
                        {
                            // Set user type in the session
                            HttpContext.Session.SetString("username", Username);
                            HttpContext.Session.SetString("userType", userType);
                        }
                        else
                        {
                            ErrorMessage = "Unable to determine user type.";
                            return Page();
                        }

                        // Redirect to the main page or specific dashboard
                        if (userType == "Member")
                            return RedirectToPage("/My Account");
                        else if (userType == "Doctor")
                            return RedirectToPage("/My Account");
                        else
                        {
                            return RedirectToPage("/Management");
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = $"An error occurred: {ex.Message}";
                return Page();
            }
        }
        private string DetermineUserType(SqlConnection connection, string username)
        {
            // Check Member table
            string query = "SELECT * FROM Member WHERE username = @username";
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@username", username);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return "Member"; // User is a Member
                    }
                }
            }

            // Check Doctor table
            query = "SELECT * FROM Doctor WHERE username = @username";
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@username", username);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return "Doctor"; // User is a Doctor
                    }
                }
            }

            // If not found in Member or Doctor, check if the user exists in Users table (Admin case)
            query = "SELECT * FROM Users WHERE Username = @username";
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@username", username);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return "Admin"; // User is an Admin
                    }
                }
            }

            // User type not found
            return null;
        }

        private void IncrementLoginAttempts(SqlConnection connection)
        {
            string updateQuery = @"
                UPDATE Users
                SET LoginAttempts = LoginAttempts + 1,
                    IsLocked = CASE WHEN LoginAttempts + 1 >= 3 THEN 1 ELSE IsLocked END
                WHERE Username = @Username";

            using (var command = new SqlCommand(updateQuery, connection))
            {
                command.Parameters.AddWithValue("@Username", Username);
                command.ExecuteNonQuery();
            }
        }

        private void ResetLoginAttempts(SqlConnection connection)
        {
            string resetQuery = @"
                UPDATE Users
                SET LoginAttempts = 0, IsLocked = 0
                WHERE Username = @Username";

            using (var command = new SqlCommand(resetQuery, connection))
            {
                command.Parameters.AddWithValue("@Username", Username);
                command.ExecuteNonQuery();
            }
        }

        public IActionResult ValidateResetCode(string codeInput, string newPassword)
        {
            try
            {
                // Retrieve the code and email from session
                int? resetCode = HttpContext.Session.GetInt32("ResetCode");
                string? resetEmail = HttpContext.Session.GetString("ResetEmail");

                if (resetCode == null || resetEmail == null)
                {
                    return new JsonResult(new { success = false, message = "Invalid or expired reset session." });
                }

                // Validate the code
                if (codeInput != resetCode.ToString())
                {
                    return new JsonResult(new { success = false, message = "Invalid reset code." });
                }

                // Reset the user's password
                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string query = "UPDATE Users SET Password = @Password WHERE Email = @Email";
                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Password", newPassword); // Hash the password!
                        command.Parameters.AddWithValue("@Email", resetEmail);
                        command.ExecuteNonQuery();
                    }
                }

                // Clear session
                HttpContext.Session.Remove("ResetCode");
                HttpContext.Session.Remove("ResetEmail");

                return new JsonResult(new { success = true, message = "Password has been reset successfully." });
            }
            catch (Exception ex)
            {
                return new JsonResult(new { success = false, message = $"An error occurred: {ex.Message}" });
            }
        }

        public IActionResult ForgotPassword(string email)
        {
            try
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Check if the user exists
                    string query = "SELECT Username FROM Users WHERE Email = @Email";
                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Email", email);

                        var username = command.ExecuteScalar()?.ToString();
                        if (username == null)
                        {
                            return new JsonResult(new { success = false, message = "Email not found." });
                        }

                        // Generate a random reset code
                        Random random = new Random();
                        int resetCode = random.Next(100000, 999999); // 6-digit code

                        // Save code and email in session
                        HttpContext.Session.SetInt32("ResetCode", resetCode);
                        HttpContext.Session.SetString("ResetEmail", email);

                        // Send email
                        try
                        {
                            var smtpClient = new SmtpClient("smtp.gmail.com")
                            {
                                Port = 587,
                                Credentials = new NetworkCredential("the.life.center.0.0@gmail.com", "bkwvkfacoyshecjl"),
                                EnableSsl = true,
                            };

                            var mailMessage = new MailMessage
                            {
                                From = new MailAddress("the.life.center.0.0@gmail.com"),
                                Subject = "Password Reset Code",
                                Body = $"Your password reset code is: {resetCode}",
                                IsBodyHtml = false,
                            };

                            mailMessage.To.Add(email);

                            smtpClient.Send(mailMessage);
                        }
                        catch (Exception ex)
                        {
                            return new JsonResult(new { success = false, message = $"Failed to send email: {ex.Message}" });
                        }

                        return new JsonResult(new { success = true, message = "Password reset code has been sent to your email." });
                    }
                }
            }
            catch (Exception ex)
            {
                return new JsonResult(new { success = false, message = $"An error occurred: {ex.Message}" });
            }
        }


    }
}


/*
 Password hashing logic incase we decide to implement it for data security later


 using System.Security.Cryptography;
using System.Text;

public static class PasswordHasher
{
    public static string HashPassword(string password)
    {
        using var sha256 = SHA256.Create();
        var salt = "YourSaltHere"; // A static salt for simplicity; replace with a unique per-user salt for better security.
        var combinedPassword = password + salt;
        var hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(combinedPassword));
        return Convert.ToBase64String(hashBytes);
    }
}

*/