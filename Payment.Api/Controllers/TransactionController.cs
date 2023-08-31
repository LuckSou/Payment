using Microsoft.AspNetCore.Mvc;
using Payment.Api.Models.ServiceModel;
using Payment.API.Models.ViewModel;

namespace Payment.Api.Controllers
{
    [ApiController]
    [Route("api/v1/transactions")]
    public class TransactionsController : ControllerBase
    {
        private readonly TransactionService _transactionProcessing;

        public TransactionsController(TransactionService transactionProcessing)
        {
            _transactionProcessing = transactionProcessing ?? throw new ArgumentNullException(nameof(transactionProcessing));
        }

        [HttpPost("process-transaction")]
        public IActionResult ProcessTransaction([FromBody] TransactionModel transactionModel)
        {
            if (transactionModel == null)
            {
                return BadRequest("Invalid transaction data.");
            }

            var transactionJson = _transactionProcessing.ProcessTransaction(transactionModel);

            return Ok(transactionJson);
        }

        [HttpGet("get-transaction/{nsu}")]
        public IActionResult GetTransactionByNsu(string nsu)
        {
            var transactionJson = _transactionProcessing.GetTransactionByNsuWithInstallments(nsu);

            if (transactionJson == null)
            {
                return NotFound("Transaction not found.");
            }

            return Ok(transactionJson);
        }
    }
}
