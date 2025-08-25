using System.Data;
using GUI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.IdentityModel.Tokens;

namespace GUI.Pages
{
    public class FeedbacksModel : PageModel
    {
        public string username { get; set; } 
        public string UserType { get; set; }
        [BindProperty]
        public string Du { get; set; }

        public DataTable Doc { get; set; }
        public DataTable AllPatient { get; set; }
        public DataTable Fd { get; set; } = new DataTable();
        public DB Db { get; set; }
        public FeedbacksModel(DB db)
        {
            Db = db;
        }
        public void OnGet()
        {
            UserType = HttpContext.Session.GetString("userType");
            username = HttpContext.Session.GetString("username");


            if (UserType == "Admin" )
            {
                if (Du.IsNullOrEmpty())
                {
                    Fd = Db.GetFeedbacks("john_doe");
                    Doc = Db.GetAllDoc("john_doe");
                    
                }
                else
                {
                  
                    Doc = Db.GetAllDoc(Du);
                    Fd = Db.GetFeedbacks(Du);
                }
                AllPatient = Db.GetAllPatient();

            }
            else if (UserType =="Member")
            {
                Doc = Db.GetAllDoc(username);
                Fd = Db.GetFeedbacks(username);
            }
           
        }
        public IActionResult OnPost()
        {
            if (UserType == "Admin")
            {
                Du = Request.Form["choose a day"];

            }
            return RedirectToPage();
        }
        public IActionResult OnPostAdd()
        {

            return RedirectToPage("/AddF");
        }
    }
}
