using CardServices.Api.Models;
using CardServices.Api.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CardServices.Api.Services
{
    public class CardService : ICardService
    {
        private readonly List<Card> _cards = new List<Card>();

        public Card CreateCard(CardRequestDto cardRequest)
        {
            try
            {
                if (cardRequest == null)
                {
                    throw new ArgumentNullException(nameof(cardRequest), "Card request cannot be null.");
                }

                DateTime expiryDate;
                bool isValidDate = DateTime.TryParseExact(cardRequest.ExpiryDate, "MM/yy", null, System.Globalization.DateTimeStyles.None, out expiryDate);

                if (!isValidDate)
                {
                    throw new ArgumentException("Invalid expiry date format. Please use MM/yy format.");
                }

                // If CardType is not being passed in the request, you could set a default value here.
                var newCard = new Card
                {
                    CardId = Guid.NewGuid(),
                    CardNumber = cardRequest.CardNumber,
                    ExpiryDate = expiryDate,
                    CardHolderName = cardRequest.CardHolderName,
                    IsActive = false,
                    UserId = cardRequest.UserId,  // Ensure UserId is correctly assigned
                    CardType = cardRequest.CardType ?? "Default"  // Set a default card type if not passed
                };

                _cards.Add(newCard);
                return newCard;
            }
            catch (ArgumentNullException ex)
            {
                throw new Exception($"Invalid input: {ex.Message}", ex);
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while creating the card. Details: {ex.Message}", ex);
            }
        }

        public Card GetCard(Guid cardId)
        {
            try
            {
                if (cardId == Guid.Empty)
                {
                    throw new ArgumentException("Card ID cannot be empty.", nameof(cardId));
                }

                var card = _cards.FirstOrDefault(c => c.CardId == cardId);
                if (card == null)
                {
                    throw new KeyNotFoundException($"Card with ID {cardId} was not found.");
                }

                return card;
            }
            catch (ArgumentException ex)
            {
                throw new Exception($"Invalid input: {ex.Message}", ex);
            }
            catch (KeyNotFoundException ex)
            {
                throw new Exception($"Card not found: {ex.Message}", ex);
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while retrieving the card. Details: {ex.Message}", ex);
            }
        }

        public List<Card> GetAllCards(Guid userId)  // Updated to return a list of cards
        {
            try
            {
                var userCards = _cards.Where(c => c.UserId == userId).ToList();
                if (userCards == null || userCards.Count == 0)
                {
                    throw new KeyNotFoundException($"No cards found for User ID {userId}.");
                }

                return userCards;
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while retrieving the cards. Details: {ex.Message}", ex);
            }
        }

        public Card UpdateCard(Guid cardId, CardRequestDto cardRequest)
        {
            try
            {
                if (cardRequest == null)
                {
                    throw new ArgumentNullException(nameof(cardRequest), "Card request cannot be null.");
                }

                var card = _cards.FirstOrDefault(c => c.CardId == cardId);
                if (card == null)
                {
                    throw new KeyNotFoundException($"Card with ID {cardId} was not found.");
                }

                card.CardNumber = cardRequest.CardNumber;
                card.ExpiryDate = DateTime.Parse(cardRequest.ExpiryDate);  // Ensure proper date parsing
                card.CardHolderName = cardRequest.CardHolderName;

                return card;
            }
            catch (ArgumentNullException ex)
            {
                throw new Exception($"Invalid input: {ex.Message}", ex);
            }
            catch (KeyNotFoundException ex)
            {
                throw new Exception($"Card not found: {ex.Message}", ex);
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while updating the card. Details: {ex.Message}", ex);
            }
        }

        public bool DeleteCard(Guid cardId)
        {
            try
            {
                if (cardId == Guid.Empty)
                {
                    throw new ArgumentException("Card ID cannot be empty.", nameof(cardId));
                }

                var card = _cards.FirstOrDefault(c => c.CardId == cardId);
                if (card == null)
                {
                    throw new KeyNotFoundException($"Card with ID {cardId} was not found.");
                }

                _cards.Remove(card);
                return true;
            }
            catch (ArgumentException ex)
            {
                throw new Exception($"Invalid input: {ex.Message}", ex);
            }
            catch (KeyNotFoundException ex)
            {
                throw new Exception($"Card not found: {ex.Message}", ex);
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while deleting the card. Details: {ex.Message}", ex);
            }
        }

        public Card ActivateCard(Guid cardId)
        {
            try
            {
                if (cardId == Guid.Empty)
                {
                    throw new ArgumentException("Card ID cannot be empty.", nameof(cardId));
                }

                var card = _cards.FirstOrDefault(c => c.CardId == cardId);
                if (card == null)
                {
                    throw new KeyNotFoundException($"Card with ID {cardId} was not found.");
                }

                card.IsActive = true;
                return card;
            }
            catch (ArgumentException ex)
            {
                throw new Exception($"Invalid input: {ex.Message}", ex);
            }
            catch (KeyNotFoundException ex)
            {
                throw new Exception($"Card not found: {ex.Message}", ex);
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while activating the card. Details: {ex.Message}", ex);
            }
        }

        public Card DeactivateCard(Guid cardId)
        {
            try
            {
                if (cardId == Guid.Empty)
                {
                    throw new ArgumentException("Card ID cannot be empty.", nameof(cardId));
                }

                var card = _cards.FirstOrDefault(c => c.CardId == cardId);
                if (card == null)
                {
                    throw new KeyNotFoundException($"Card with ID {cardId} was not found.");
                }

                card.IsActive = false;
                return card;
            }
            catch (ArgumentException ex)
            {
                throw new Exception($"Invalid input: {ex.Message}", ex);
            }
            catch (KeyNotFoundException ex)
            {
                throw new Exception($"Card not found: {ex.Message}", ex);
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while deactivating the card. Details: {ex.Message}", ex);
            }
        }
    }
}