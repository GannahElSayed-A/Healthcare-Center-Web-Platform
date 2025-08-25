using GUI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Transaction = GUI.Models.Transaction;
namespace GUI.Pages
{
    public class AddTransactionModel : PageModel
    {
        public DB db { get; set; }
        [BindProperty]
        public Transaction trans { get; set; }
        public AddTransactionModel(DB db)
        { this.db = db; }
        public void OnGet()
        {
        }
        public IActionResult OnPostAdd()
        {
            db.add_transactions(trans);
            return (RedirectToPage("/Privacy"));
        }
    }
}
