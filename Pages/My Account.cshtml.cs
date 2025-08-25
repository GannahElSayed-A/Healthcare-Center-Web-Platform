using GUI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GUI.Pages
{
    public class MyaccountModel : PageModel
    {
        public List<SetSession> DoctorPatients { get; set; }
        public string UserType { get; set; }
        public string Name { get; set; }
        public string Nationality { get; set; }
        public List<GUI.Models.History> History { get; set; }
        public List<Problem> PhysicalProblems { get; set; }
        public List<Problem> BehaviorProblems { get; set; }

        private readonly DB _db;

        public MyaccountModel(DB db)
        {
            _db = db;
        }

        public void OnGet(string username)
        {
            UserType = HttpContext.Session.GetString("userType");
            username= HttpContext.Session.GetString("username");

            if (UserType=="Doctor")
            {
                Name = username;
                DoctorPatients = _db.GetDoctorPatients(username);
            }
            else
            {
                var (member, histories, problems) = _db.GetUserProfile(username);

                if (member != null)
                {
                    Name = $"{member.Username}";
                    Nationality = member.Nationality;
                }

                History = histories;

                PhysicalProblems = problems?.Where(p => p.ProblemType == "Physical").ToList() ?? new List<Problem>();
                BehaviorProblems = problems?.Where(p => p.ProblemType == "behaviour").ToList() ?? new List<Problem>();
            }
        }

        public IActionResult OnPostLogout()
        {
            TempData["Message"] = "Logout successful!";
            return RedirectToPage("/Index");
        }

    }
}