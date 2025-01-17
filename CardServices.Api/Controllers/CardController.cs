using CardServices.Api.Models;
using CardServices.Api.Models.DTOs;
using CardServices.Api.Services;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System;

namespace CardServices.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CardController : ControllerBase
    {
        private readonly ICardService _cardService;

        public CardController(ICardService cardService)
        {
            _cardService = cardService;
        }

        [HttpPost("create", Name = "CreateCard")]
        [SwaggerOperation(
            Summary = "Create a new card",
            Description = "Creates a new card with the details provided in the request body.")]
        [SwaggerResponse(201, "The card was successfully created.")]
        [SwaggerResponse(400, "Invalid request. The card data is missing or invalid.")]
        [SwaggerResponse(500, "An internal server error occurred.")]
        public IActionResult CreateCard([FromBody] CardRequestDto cardRequest)
        {
            try
            {
                if (cardRequest == null)
                {
                    return BadRequest("Card request body cannot be null. Please provide a valid card data.");
                }

                var result = _cardService.CreateCard(cardRequest);
                if (result == null)
                {
                    return NotFound("Card could not be created. Please try again or check the provided data.");
                }

                return CreatedAtAction(nameof(GetCard), new { id = result.CardId }, result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: An unexpected error occurred while trying to create the card. Details: {ex.Message}");
            }
        }

        [HttpGet("{id}", Name = "GetCard")]
        [SwaggerOperation(
            Summary = "Retrieve a specific card",
            Description = "Fetches the details of a card using its unique ID.")]
        [SwaggerResponse(200, "The card was successfully retrieved.")]
        [SwaggerResponse(404, "The specified card ID was not found.")]
        [SwaggerResponse(500, "An internal server error occurred.")]
        public IActionResult GetCard(Guid id)
        {
            try
            {
                var card = _cardService.GetCard(id);
                if (card == null)
                {
                    return NotFound($"Card with ID {id} was not found. Please ensure the ID is correct.");
                }

                return Ok(card);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: An unexpected error occurred while retrieving the card. Details: {ex.Message}");
            }
        }

        [HttpGet("user/{userId}", Name = "GetAllCards")]
        [SwaggerOperation(
    Summary = "Retrieve all cards for a specific user",
    Description = "Fetches all the cards associated with a specific user ID.")]
        [SwaggerResponse(200, "Cards were successfully retrieved.")]
        [SwaggerResponse(404, "No cards found for the specified user.")]
        [SwaggerResponse(500, "An internal server error occurred.")]
        public IActionResult GetAllCards(Guid userId)
        {
            try
            {
                var cards = _cardService.GetAllCards(userId);
                if (cards == null || cards.Count == 0)
                {
                    return NotFound($"No cards found for User ID {userId}. Please ensure the ID is correct.");
                }

                return Ok(cards);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: An unexpected error occurred while retrieving the cards. Details: {ex.Message}");
            }
        }

        [HttpPut("{id}", Name = "UpdateCard")]
        [SwaggerOperation(
            Summary = "Update an existing card",
            Description = "Updates the details of a card with the given ID using the new data provided in the request body.")]
        [SwaggerResponse(200, "The card was successfully updated.")]
        [SwaggerResponse(400, "Invalid request. The card data is missing or invalid.")]
        [SwaggerResponse(404, "The specified card ID was not found.")]
        [SwaggerResponse(500, "An internal server error occurred.")]
        public IActionResult UpdateCard(Guid id, [FromBody] CardRequestDto cardRequest)
        {
            try
            {
                if (cardRequest == null)
                {
                    return BadRequest("Card request body cannot be null. Please provide valid update data.");
                }

                var updatedCard = _cardService.UpdateCard(id, cardRequest);
                if (updatedCard == null)
                {
                    return NotFound($"Card with ID {id} was not found. Please verify the card ID and try again.");
                }

                return Ok(updatedCard);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: An error occurred while updating the card. Details: {ex.Message}");
            }
        }

        [HttpDelete("{id}", Name = "DeleteCard")]
        [SwaggerOperation(
            Summary = "Delete a card",
            Description = "Deletes the card with the specified ID from the system.")]
        [SwaggerResponse(204, "The card was successfully deleted.")]
        [SwaggerResponse(404, "The specified card ID was not found.")]
        [SwaggerResponse(500, "An internal server error occurred.")]
        public IActionResult DeleteCard(Guid id)
        {
            try
            {
                var isDeleted = _cardService.DeleteCard(id);
                if (!isDeleted)
                {
                    return NotFound($"Card with ID {id} was not found. Deletion failed.");
                }

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: An error occurred while attempting to delete the card. Details: {ex.Message}");
            }
        }

        [HttpPatch("{id}/activate", Name = "ActivateCard")]
        [SwaggerOperation(
            Summary = "Activate a specific card",
            Description = "Activates a card based on its unique ID, allowing it to be used.")]
        [SwaggerResponse(200, "The card was successfully activated.")]
        [SwaggerResponse(404, "The specified card ID was not found.")]
        [SwaggerResponse(500, "An internal server error occurred.")]
        public IActionResult ActivateCard(Guid id)
        {
            try
            {
                var card = _cardService.ActivateCard(id);
                if (card == null)
                {
                    return NotFound($"Card with ID {id} was not found. Unable to activate the card.");
                }

                return Ok(card);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: An error occurred while attempting to activate the card. Details: {ex.Message}");
            }
        }

        [HttpPatch("{id}/deactivate", Name = "DeactivateCard")]
        [SwaggerOperation(
            Summary = "Deactivate a specific card",
            Description = "Deactivates a card based on its unique ID, preventing further use of the card.")]
        [SwaggerResponse(200, "The card was successfully deactivated.")]
        [SwaggerResponse(404, "The specified card ID was not found.")]
        [SwaggerResponse(500, "An internal server error occurred.")]
        public IActionResult DeactivateCard(Guid id)
        {
            try
            {
                var card = _cardService.DeactivateCard(id);
                if (card == null)
                {
                    return NotFound($"Card with ID {id} was not found. Unable to deactivate the card.");
                }

                return Ok(card);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: An error occurred while attempting to deactivate the card. Details: {ex.Message}");
            }
        }
    }
}
