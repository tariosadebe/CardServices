//using CardServices.Api.Models;
//using CardServices.Api.Services;
//using Microsoft.AspNetCore.Mvc;

//namespace CardServices.Api.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class TransactionController : ControllerBase
//    {
//        private readonly ICardService _cardService;

//        public TransactionController(ICardService cardService)
//        {
//            _cardService = cardService;
//        }

//        [HttpPost]
//        public async Task<ActionResult<Transaction>> ProcessTransaction([FromBody] Transaction transaction)
//        {
//            try
//            {
//                var processedTransaction = await _cardService.ProcessTransactionAsync(transaction);
//                return Ok(processedTransaction);
//            }
//            catch (Exception ex)
//            {
//                return BadRequest($"An error occurred while processing the transaction: {ex.Message}");
//            }
//        }
//    }
//}
