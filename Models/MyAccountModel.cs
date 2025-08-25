using Microsoft.AspNetCore.Mvc.RazorPages;

public class MyaccountModel : PageModel
{
    private readonly DatabaseHelper _dbHelper;

    public string Name { get; set; }
    public string Nationality { get; set; }
    public List<History> History { get; set; }
    public List<Problem> PhysicalProblems { get; set; }
    public List<Problem> BehaviorProblems { get; set; }


}
public class History
{
    public int No { get; set; }
    public string Type { get; set; }
    public string Issue { get; set; }
}

public class Problem
{
    public int No { get; set; }
    public string Type { get; set; }
    public string ProblemDescription { get; set; }
    public DateTime StartDate { get; set; }
    public string? ProblemType { get; internal set; }
    public string? Description { get; internal set; }
}