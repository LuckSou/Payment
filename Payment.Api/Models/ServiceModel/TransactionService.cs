using Microsoft.EntityFrameworkCore;
using Payment.Api.Data;
using Payment.API.Models.EntityModel;
using Payment.API.Models.ResultModel.api.Models.ResultModel;
using Payment.API.Models.ViewModel;

namespace Payment.Api.Models.ServiceModel
{
    public class TransactionService
    {
        private const decimal FixedFee = 0.9M;
        private readonly ApiDbContext _dbContext;

        public TransactionService(ApiDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public TransactionJson ProcessTransaction(TransactionModel transactionModel)
        {
            if (transactionModel == null)
            {
                throw new ArgumentNullException(nameof(transactionModel));
            }

            var transaction = MapTransactionModelToEntity(transactionModel);

            if (IsCardNumberInvalid(transactionModel.CardNumber))
            {
                RejectTransaction(transaction);
            }
            else
            {
                ApproveTransaction(transaction);
            }

            return new TransactionJson(transaction);
        }
        public TransactionJson GetTransactionByNsuWithInstallments(string nsu)
        {
            var transaction = GetTransactionWithInstallmentsByNsu(nsu);

            if (transaction == null)
            {
                return null;
            }

            return new TransactionJson(transaction);
        }
        private Transaction MapTransactionModelToEntity(TransactionModel transactionModel)
        {
            var transaction = transactionModel.Map();
            transaction.NSU = GenerateCreditCardNSU();
            transaction.TransactionDate = DateTime.Now;

            return transaction;
        }

        private string GenerateCreditCardNSU()
        {
            var randomNumber = new Random().Next(100000, 999999);
            return $"{DateTime.Now:yyyyMMddHHmmss}{randomNumber}";
        }

        private bool IsCardNumberInvalid(string cardNumber)
        {
            return cardNumber.Length != 16 || cardNumber.StartsWith("5999");
        }

        private void RejectTransaction(Transaction transaction)
        {
            transaction.RejectionDate = DateTime.Now;
            transaction.AcquirerConfirmation = false;

            SaveTransactionAndInstallments(transaction);
        }

        private void ApproveTransaction(Transaction transaction)
        {
            transaction.ApprovalDate = DateTime.Now;
            transaction.AcquirerConfirmation = true;
            transaction.NetAmount = transaction.GrossAmount - FixedFee;

            SaveTransactionAndInstallments(transaction);
        }

        private void SaveTransactionAndInstallments(Transaction transaction)
        {
            _dbContext.Transactions.Add(transaction);
            _dbContext.SaveChanges();

            GenerateInstallments(transaction);
        }

        private void GenerateInstallments(Transaction transaction)
        {
            var installmentAmount = transaction.GrossAmount / (transaction.Installments == 0 ? 1 : transaction.Installments);
            var paymentDate = transaction.TransactionDate;

            for (int i = 1; i <= transaction.Installments; i++)
            {
                var installment = new Installment
                {
                    TransactionId = transaction.Id,
                    GrossAmount = installmentAmount,
                    NetAmount = transaction.NetAmount / (transaction.Installments == 0 ? 1 : transaction.Installments),
                    Number = i,
                    ExpectedPaymentDate = paymentDate.AddMonths(i),
                };

                _dbContext.Installments.Add(installment);
            }

            _dbContext.SaveChanges();
        }

        private Transaction GetTransactionWithInstallmentsByNsu(string nsu)
        {
            return _dbContext.Transactions
                .Include(t => t.InstallmentsList)
                .FirstOrDefault(t => t.NSU == nsu);
        }
    }
}
