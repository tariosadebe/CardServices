namespace CardServices.Api.Models
{
    public class Transaction
    {
        public int Id { get; set; }
        public int CardId { get; set; }
        public decimal Amount { get; set; }
        public DateTime TransactionDate { get; set; }
        public string Type { get; set; } // Credit or Debit
        public string Status { get; set; } // Success or Failed
    }
}
