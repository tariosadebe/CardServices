namespace CardServices.Api.Models
{
    public class Card
    {
        public Guid CardId { get; set; }  // Changed to GUID for unique identification
        public string CardNumber { get; set; }
        public string CardHolderName { get; set; }
        public DateTime ExpiryDate { get; set; }
        public decimal Balance { get; set; }
        public string CardType { get; set; }  // Credit or Debit
        public bool IsActive { get; set; }
        public Guid UserId { get; set; }

        // Example: You might also have other properties like a card pin, CVV, etc.
    }
}
