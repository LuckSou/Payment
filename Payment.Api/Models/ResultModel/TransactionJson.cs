using Microsoft.AspNetCore.Mvc;
using Payment.API.Models.EntityModel;

namespace Payment.API.Models.ResultModel
{
    namespace api.Models.ResultModel
    {
        public class TransactionJson : IActionResult
        {
            public TransactionJson() { }

            public TransactionJson(Transaction transaction)
            {
                NSU = transaction.NSU;
                TransactionDate = transaction.TransactionDate;
                ApprovalDate = transaction.ApprovalDate;
                RejectionDate = transaction.RejectionDate;
                Anticipated = transaction.Anticipated;
                AcquirerConfirmation = transaction.AcquirerConfirmation;
                GrossAmount = transaction.GrossAmount;
                NetAmount = transaction.NetAmount;
                InstallmentsCount = transaction.Installments;
                LastFourDigits = transaction.LastFourDigits;

                Installments = transaction.InstallmentsList.Select(installment => new InstallmentJson(installment)).ToList();
            }

            public string NSU { get; set; }
            public DateTime TransactionDate { get; set; }
            public DateTime? ApprovalDate { get; set; }
            public DateTime? RejectionDate { get; set; }
            public bool Anticipated { get; set; }
            public bool AcquirerConfirmation { get; set; }
            public decimal GrossAmount { get; set; }
            public decimal NetAmount { get; set; }
            public int InstallmentsCount { get; set; }
            public string LastFourDigits { get; set; }

            public List<InstallmentJson> Installments { get; set; }

            public Task ExecuteResultAsync(ActionContext context)
            {
                return new JsonResult(this).ExecuteResultAsync(context);
            }
        }
    }

}
