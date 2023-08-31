using Payment.API.Models.EntityModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace Payment.Api.Models.EntityModel
{
    public class AnticipationTransaction
    {
        public int Id { get; set; }
        public decimal AnticipatedValue { get; set; }

        [ForeignKey("Anticipation")]
        public int AnticipationId { get; set; }
        public Anticipation Anticipation { get; set; }

        [ForeignKey("Transaction")]
        public int TransactionId { get; set; }
        public Transaction Transaction { get; set; }
    }
}
