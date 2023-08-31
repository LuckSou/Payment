using Payment.Api.Models.EntityModel;

namespace Payment.Api.Models.ResultModel
{
    public class AnticipationTransactionJson
    {
        public AnticipationTransactionJson() { }

        public AnticipationTransactionJson(AnticipationTransaction anticipationTransaction)
        {
            TransactionId = anticipationTransaction.TransactionId;
            AnticipatedValue = anticipationTransaction.AnticipatedValue;
        }

        public int TransactionId { get; set; }
        public decimal AnticipatedValue { get; set; }
    }
}
