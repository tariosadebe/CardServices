using CardServices.Api.Models.DTOs;
using CardServices.Api.Models;

public interface ICardService
{
    Card CreateCard(CardRequestDto cardRequest);
    Card GetCard(Guid cardId);
    List<Card> GetAllCards(Guid userId);  // Change to List<Card>
    Card UpdateCard(Guid cardId, CardRequestDto cardRequest);
    bool DeleteCard(Guid cardId);
    Card ActivateCard(Guid cardId);
    Card DeactivateCard(Guid cardId);
}