using System.ComponentModel.DataAnnotations.Schema;

namespace Payment.API.Models.EntityModel
{
    public class Installment
    {
        public int Id { get; set; }
        public int TransactionId { get; set; }
        public decimal GrossAmount { get; set; }
        public decimal NetAmount { get; set; }
        public int Number { get; set; }
        public decimal? AnticipatedValue { get; set; }
        public DateTime? ExpectedPaymentDate { get; set; }
        public DateTime? RepassedDate { get; set; }

        [ForeignKey("TransactionId")]
        public Transaction Transaction { get; set; }
    }
}
