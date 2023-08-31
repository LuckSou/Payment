using Microsoft.AspNetCore.Mvc;
using Payment.API.Models.EntityModel;

namespace Payment.API.Models.ResultModel
{
    public class InstallmentJson : IActionResult
    {
        public InstallmentJson() { }

        public InstallmentJson(Installment installment)
        {
            GrossAmount = installment.GrossAmount;
            NetAmount = installment.NetAmount;
            InstallmentNumber = installment.Number;
            AnticipatedValue = installment.AnticipatedValue;
            ExpectedPaymentDate = installment.ExpectedPaymentDate;
            RepassedDate = installment.RepassedDate;

        }

        public decimal GrossAmount { get; set; }
        public decimal NetAmount { get; set; }
        public int InstallmentNumber { get; set; }
        public decimal? AnticipatedValue { get; set; }
        public DateTime? ExpectedPaymentDate { get; set; }
        public DateTime? RepassedDate { get; set; }

        public Task ExecuteResultAsync(ActionContext context)
        {
            return new JsonResult(this).ExecuteResultAsync(context);
        }
    }
}
