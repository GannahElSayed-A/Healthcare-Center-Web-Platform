using Microsoft.AspNetCore.Mvc.RazorPages;
using GUI.Models;  // Make sure to include this if your Transaction class is inside the Models folder

using Microsoft.AspNetCore.Mvc;
using System.Data;


namespace GUI.Pages
{
    [BindProperties]
    public class TransactionPageModel : PageModel
    {
        // This is the list of transactions that will be displayed on the page
        public List<Transaction> Transactions { get; set; }
        public DB db { get; set; }
        public string UserType { get; set; }
        
        [BindProperty]
        public Transaction trans { get; set; }

        [BindProperty]
        public string username { get; set; }
        [BindProperty]
        public DataTable dt { get; set; }
        public required int TransactionId { get; set; }
        public DateTime Date { get; set; }
        public Decimal Amount { get; set; }
        // This is the list of transactions that will be displayed on the page
        public String Type { get; set; }

        public TransactionPageModel(DB db) { this.db = db; }
        public void OnGet()
        {
            UserType = HttpContext.Session.GetString("userType");
            username = HttpContext.Session.GetString("username");
            // Example data, in a real application this would come from a database
            // Example data, in a real application this comes from a database
            //For Admin
            if (UserType == "Admin")
            { dt = db.get_all_transactions(); }

            if (UserType == "Member")
            {
                dt = db.get_user_transactions(username);

            }
            Transactions = new List<Transaction>();
            {
                if (UserType == "Admin")
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        Transactions.Add(new Transaction { TransactionId = (int)row["Number"], Date = (DateTime)row["TransactionDateTime"], Amount = (Decimal)row["Amount"], username = (string)row["M_username"] });
                    }
                }
                if (UserType == "Member")
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        Transactions.Add(new Transaction { TransactionId = (int)row["Number"], Date = (DateTime)row["TransactionDateTime"], Amount = (Decimal)row["Amount"] });
                    }
                }
                //new Transaction { TransactionId = "TX12346", Date = "ui", Amount = 788}

            }





        }
        //public void OnPostAdd() { db.add_transactions(user_name, Date, Amount, TransactionId); }
        public IActionResult OnpostEdit(int TransID)
        { return RedirectToPage("/EditTransaction", new { TransID = TransID }); }

        public IActionResult OnpostAdd()
        { return RedirectToPage("/AddTransaction"); }
        public IActionResult OnpostDelete(int TransID)
        {
            db.delete_transactions(TransID);
            return RedirectToPage(Page());
        }


    }

}
