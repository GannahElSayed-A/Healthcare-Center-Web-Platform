using System.ComponentModel.DataAnnotations;
using GUI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GUI.Pages
{
    public class AddSessionModel : PageModel
    {


        [BindProperty]
        public Session S { get; set; }
        //[BindProperty(SupportsGet = true)]
        //public int Req { get; set; } = 0;
        public string errMsg { get; set; } = null;
        public DB db { get; set; }
        public AddSessionModel(DB db)
        {
            this.db = db;
        }
        public void OnGet()
        {

        }
        public IActionResult OnPost()
        {
            //S.Sdate = S.d + S.Stime;
            //implement in db
            int x = db.AddSession(S);
            if (x == 1)
            {
                return RedirectToPage("/Schedule");
            }
            else
            {
                errMsg = "Error!,Try Again";
                return RedirectToPage("/AddSession"); ;


            }
        }

    }
}
