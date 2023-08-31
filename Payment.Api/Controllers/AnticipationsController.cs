using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Payment.Api.Data;
using Payment.Api.Models.EntityModel.Enum;
using Payment.Api.Models.ResultModel;
using Payment.Api.Models.ViewModel;
using Payment.API.Models.ServiceModel;

namespace Payment.Api.Controllers
{
    [ApiController]
    [Route("api/v1/anticipations")]
    public class AnticipationsController : ControllerBase
    {
        private readonly ApiDbContext _dbContext;
        private readonly AnticipationService _anticipationService;

        public AnticipationsController(ApiDbContext dbContext, AnticipationService anticipationService)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _anticipationService = anticipationService ?? throw new ArgumentNullException(nameof(anticipationService));
        }

        [HttpGet("available-transactions")]
        public async Task<IActionResult> GetAvailableTransactions()
        {
            var availableTransactions = await _dbContext.Transactions
                .Where(t => !t.Anticipated)
                .ToListAsync();

            return Ok(availableTransactions);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAnticipation([FromBody] AnticipationModel anticipationModel)
        {
            var anticipation = await _anticipationService.CreateAnticipation(anticipationModel);

            return Ok(anticipation);
        }

        [HttpGet("{anticipationId}")]
        public async Task<IActionResult> GetAnticipation(int anticipationId)
        {
            var anticipation = await _anticipationService.GetAnticipation(anticipationId);

            if (anticipation == null)
            {
                return NotFound("Anticipation not found.");
            }

            var anticipationJson = new AnticipationJson(anticipation); // Create an instance of AnticipationJson with anticipation data

            return Ok(anticipationJson);
        }

        [HttpPost("{anticipationId}/approve")]
        public async Task<IActionResult> ApproveAnticipation(int anticipationId)
        {
            await _anticipationService.UpdateAnticipationAnalysis(anticipationId, AnticipationAnalysisResult.Approved);

            return Ok("Anticipation approved.");
        }

        [HttpPost("{anticipationId}/reject")]
        public async Task<IActionResult> RejectAnticipation(int anticipationId)
        {
            await _anticipationService.UpdateAnticipationAnalysis(anticipationId, AnticipationAnalysisResult.Rejected);

            return Ok("Anticipation rejected.");
        }
    }
}
