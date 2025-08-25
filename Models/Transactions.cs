namespace GUI.Models
{
    public class Transaction
    {
        //to be added
        //public required string Status { get; set; }
        // Completed, Pending, Failed
        public string username { get; set; }
        public required int TransactionId { get; set; }
        public DateTime Date { get; set; }
        public Decimal Amount { get; set; }
        // Completed, Pending, Failed
    }
}
