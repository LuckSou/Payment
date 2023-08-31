using Payment.Api.Models.EntityModel;

namespace Payment.Api.Models.ResultModel
{
    public class AnticipationTransactionJson
    {
        public AnticipationTransactionJson() { }

        public AnticipationTransactionJson(AnticipationTransaction anticipationTransaction)
        {
            Id = anticipationTransaction.Id;
            TransactionId = anticipationTransaction.TransactionId;
            AnticipatedValue = anticipationTransaction.AnticipatedValue;
        }

        public int Id { get; set; }
        public int TransactionId { get; set; }
        public decimal AnticipatedValue { get; set; }
    }
}
