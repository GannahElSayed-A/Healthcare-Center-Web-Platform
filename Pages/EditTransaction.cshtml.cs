using GUI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Transaction = GUI.Models.Transaction;
namespace GUI.Pages
{
    public class EditTransactionModel : PageModel
    {
        public DB db { get; set; }
        [BindProperty]
        public Transaction trans { get; set; }
        [BindProperty(SupportsGet = true)]
        public int TransID { get; set; }
        public EditTransactionModel(DB db)
        { this.db = db; }
        public void OnGet()
        {

        }

        public void OnPost()
        {
            trans.TransactionId = TransID;
            db.Edit_transactions(trans);

        }

    }

}
