using Microsoft.AspNetCore.Mvc;
using Payment.Api.Models.EntityModel;
using Payment.Api.Models.EntityModel.Enum;

namespace Payment.Api.Models.ResultModel
{
    public class AnticipationJson : IActionResult
    {
        public AnticipationJson() { }

        public AnticipationJson(Anticipation anticipation)
        {
            Id = anticipation.Id;
            RequestDate = anticipation.RequestDate;
            AnalysisStartDate = anticipation.AnalysisStartDate;
            AnalysisEndDate = anticipation.AnalysisEndDate;
            AnalysisResult = anticipation.AnalysisResult;
            RequestedValue = anticipation.RequestedValue;
            AnticipatedValue = anticipation.AnticipatedValue;

            AnticipationTransactions = anticipation.AnticipationTransactions.Select(ant => new AnticipationTransactionJson(ant)).ToList();
        }

        public int Id { get; set; }
        public DateTime RequestDate { get; set; }
        public DateTime? AnalysisStartDate { get; set; }
        public DateTime? AnalysisEndDate { get; set; }
        public AnticipationAnalysisResult AnalysisResult { get; set; }
        public decimal RequestedValue { get; set; }
        public decimal AnticipatedValue { get; set; }

        public List<AnticipationTransactionJson> AnticipationTransactions { get; set; }

        public Task ExecuteResultAsync(ActionContext context)
        {
            return new JsonResult(this).ExecuteResultAsync(context);
        }
    }
}
