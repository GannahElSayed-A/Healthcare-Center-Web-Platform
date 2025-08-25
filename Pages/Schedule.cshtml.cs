using System.Data;
using GUI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GUI.Pages
{
    public class ScheduleModel : PageModel
    {
        public int TUE { get; set; } = 0;
        public int SUN { get; set; } = 0;
        public int MON { get; set; } = 0;
        public int WED { get; set; } = 0;
        public int THURS { get; set; } = 0;
        public string UserType { get; set; }
        public string username { get; set; }
        [BindProperty(SupportsGet = true)]

        public DateOnly sdate { get; set; }
        public string date { get; set; }
        public string currtime { get; set; }
        public DataTable Sessions { get; set; }
        public DataTable Rooms { get; set; }


        public DB db { get; set; }
        public ScheduleModel(DB db)
        {
            this.db = db;
        }
        public void OnGet()
        {
            UserType = HttpContext.Session.GetString("userType");
            username = HttpContext.Session.GetString("username");

            if (db == null)
            {
                throw new InvalidOperationException("DB instance is not initialized before calling AllSession.");

            }

            
            if (UserType == "Admin")
            {

                if (string.IsNullOrEmpty(HttpContext.Session.GetString("date")))
                {
                    currtime = db.GetCurrent();
                    sdate = DateOnly.Parse(currtime);

                }
                else
                {
                    sdate = DateOnly.Parse(date);

                }
                string t = "2024-03-21";
                sdate = DateOnly.Parse(t);
                Sessions = db.AllSession(sdate);
                //Sessions = db.AllSession(21,"March");
                Rooms = db.GetAllRooms();
            }
            else
            {
                Sessions = new DataTable();
                
                Sessions = db.GetSessions(username);
                for (int i = 0; i < Sessions.Rows.Count; i++)
                {
                    if (Sessions.Rows[i][0].ToString() == "Sunday")
                    {
                        SUN++;
                    }
                    if (Sessions.Rows[i][0].ToString() == "Monday")
                    {
                        MON++;
                    }
                    if (Sessions.Rows[i][0].ToString() == "Tuesday")
                    {
                        TUE++;
                    }
                    if (Sessions.Rows[i][0].ToString() == "Wednesday")
                    {
                        WED++;
                    }
                    if (Sessions.Rows[i][0].ToString() == "Thursday")
                    {
                        THURS++;
                    }
                }
            }
        }
        public IActionResult OnPost()
        {
            date = sdate.ToString();
            HttpContext.Session.SetString("date", date);
            return RedirectToPage("/Schedule");
        }
        public IActionResult OnPostAdd()
        {

            return RedirectToPage("/AddSession");
        }
        public IActionResult OnPostEdit(string Muser, string Date)
        {

            return RedirectToPage("/EditSession", new { Uname = Muser, Sdate = Date });
        }
        public IActionResult OnPostDelete(string Muser, string Date)
        {
            db.DeleteSession(Muser, Date);
            return RedirectToPage("/Schedule", new { username = 1 });
        }
    }
}
