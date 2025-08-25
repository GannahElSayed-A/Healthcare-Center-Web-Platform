//using GUI.Models;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Mvc.RazorPages;

//namespace GUI.Pages
//{
//    public class MangementModel : PageModel
//    {


//        private readonly MangementModel mangement;
//        public DB _db { get; set; }


//        public List<User> Users { get; set; }


//        public MangementModel(DB db)
//        {
//            _db = db;

//            Users = new List<User>();

//        }

//        public void OnGet()
//        {

//            Users = _db.GetAllUsers();
//        }
//        public IActionResult OnPostUserManagement()
//        {
//            return RedirectToPage("/UserManagement");
//        }




//        public IActionResult OnPostUserDetails()
//        {


//            TempData["Error"] = "User not found.";
//            return RedirectToPage("/Management");

//        }
//        public IActionResult OnPostLogout()
//        {
//            return RedirectToPage("/Index");
//        }
//    }
//}
using GUI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GUI.Pages
{
    public class MangementModel : PageModel
    {


        private readonly MangementModel mangement;
        public DB _db { get; set; }


        public List<User> Users { get; set; }


        public MangementModel(DB db)
        {
            _db = db;

            Users = new List<User>();

        }

        public void OnGet()
        {

            Users = _db.GetAllUsers();
        }
        public IActionResult OnPostUserManagement()
        {
            return RedirectToPage("/UserManagement");
        }





        public IActionResult OnPostLogout()
        {
            return RedirectToPage("/Index");
        }
    }
}