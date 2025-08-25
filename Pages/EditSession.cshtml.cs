using GUI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GUI.Pages
{
    public class EditSessionModel : PageModel
    {

        [BindProperty(SupportsGet = true)]

        public string Uname { get; set; }

        [BindProperty(SupportsGet = true)]

        public string Sdate { get; set; }

        [BindProperty]
        public Session S { get; set; }
        [BindProperty(SupportsGet = true)]
        public int Req { get; set; } = 0;
        public string errMsg { get; set; } = null;
        public DB db { get; set; }
        public EditSessionModel(DB db)
        {
            this.db = db;
        }
        public void OnGet()
        {
        }
        public IActionResult OnPost()
        {
            S.Uname = Uname;
            S.Sdate = Sdate;
            //S.Place=place;
            //S.price=price;
            //S.fd = "";
            //S.Dname = "";
            if (db.EditSession(S) == 1)
            {
                return RedirectToPage("/Schedule");
            }
            else
            {
                errMsg = S.Place;
                return Page();
            }
        }
    }
}

