namespace CardServices.Api.Models.DTOs
{
    public class CardRequestDto
    {
        public string CardNumber { get; set; }
        public string ExpiryDate { get; set; }
        public string CardHolderName { get; set; }
        public Guid UserId { get; set; }
        public string CardType { get; set; }
    }
}