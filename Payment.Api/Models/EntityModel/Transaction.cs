using System.ComponentModel.DataAnnotations.Schema;

namespace Payment.API.Models.EntityModel
{
    public class Transaction
    {
        public int Id { get; set; }
        public string NSU { get; set; }
        public DateTime TransactionDate { get; set; }
        public DateTime? ApprovalDate { get; set; }
        public DateTime? RejectionDate { get; set; }
        public bool Anticipated { get; set; }
        public bool AcquirerConfirmation { get; set; }
        public decimal GrossAmount { get; set; }
        public decimal NetAmount { get; set; }
        public int Installments { get; set; }
        public string LastFourDigits { get; set; }

        [InverseProperty("Transaction")]
        public List<Installment> InstallmentsList { get; set; }
    }
}
