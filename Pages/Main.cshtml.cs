using GUI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;


namespace GUI.Pages
{
    public class MainModel : PageModel
    {

        private readonly ILogger<MyaccountModel> _logger;
        private readonly User _user;
        private readonly Problem _problem;
        private readonly History _history;
        public int username { get; set; } = 0;
        public string Name { get; set; }
        public string Nationality { get; set; }
        public List<Problem> BehaviorProblems { get; set; }
        public List<HistoryRecord> history { get; set; }
        public List<Problem> PhysicalProblems { get; set; }

        public MainModel()
        {
            _user = new User();
            Name = _user.FirstName;

        }

        public void OnGet()
        {
            // Populate mock data for demonstration
            Name = "John Doe";
            Nationality = "American";

            history = new List<HistoryRecord>
            {
                new HistoryRecord  { No = 1, Type = "Surgery", Issue = "Appendectomy" },
                new HistoryRecord  { No = 2, Type = "Medicine", Issue = "Antibiotics" },
                new HistoryRecord  { No = 3, Type = "Allergy", Issue = "Pollen" }
            };

            PhysicalProblems = new List<Problem>
            {
                new Problem { No = 1, problem = "Back Pain", StartDate = "2021-01-15" },
                new Problem { No = 2, problem = "Knee Injury", StartDate = "2022-03-10" }
            };

            BehaviorProblems = new List<Problem>
            {
                new Problem { No = 1, problem = "Anxiety", StartDate = "2021-09-01" },
                new Problem { No = 2, problem = "Insomnia", StartDate = "2020-05-20" }
            };





        }

        // Nested classes for data modeling
        public class HistoryRecord
        {
            public int No { get; set; }

            public string Type { get; set; }
            public string Issue { get; set; }
        }

        public class Problem
        {
            public int No { get; set; }
            public string problem { get; set; }
            public string StartDate { get; set; }
        }
    }
}
