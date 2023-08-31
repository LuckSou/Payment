using Microsoft.EntityFrameworkCore;
using Payment.Api.Data;
using Payment.Api.Models.EntityModel;
using Payment.Api.Models.EntityModel.Enum;
using Payment.Api.Models.ResultModel;
using Payment.Api.Models.ViewModel;
using Payment.API.Models.EntityModel;

namespace Payment.API.Models.ServiceModel
{
    public class AnticipationService
    {
        private readonly ApiDbContext _dbContext;

        public AnticipationService(ApiDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task<AnticipationJson> CreateAnticipation(AnticipationModel anticipationModel)
        {
            var transactions = await GetTransactionsByNsu(anticipationModel.Nsu).ToListAsync();

            if (!transactions.Any())
            {
                throw new InvalidOperationException("No transactions found.");
            }

            var anticipation = CreateAnticipationInstance();

            foreach (var transaction in transactions)
            {
                var anticipationTransaction = CreateAnticipationTransaction(transaction, anticipation);
                anticipation.AnticipationTransactions.Add(anticipationTransaction);
            }

            SaveAnticipation(anticipation);

            return new AnticipationJson(anticipation);
        }


        public async Task<Anticipation> GetAnticipation(int anticipationId)
        {
            return await _dbContext.Anticipations
                .Include(a => a.AnticipationTransactions)
                    .ThenInclude(at => at.Transaction)
                        .ThenInclude(t => t.InstallmentsList)
                .FirstOrDefaultAsync(a => a.Id == anticipationId);
        }

        public async Task UpdateAnticipationAnalysis(int anticipationId, AnticipationAnalysisResult analysisResult)
        {
            var anticipation = await GetAnticipationWithTransactionsAndInstallments(anticipationId);

            anticipation.AnalysisResult = analysisResult;

            if (analysisResult == AnticipationAnalysisResult.Approved)
            {
                UpdateAnticipatedValuesAndInstallments(anticipation);
            }

            SaveAnticipationChanges(anticipation);
        }

        private IQueryable<Transaction> GetTransactionsByNsu(List<string> nsuList)
        {
            return _dbContext.Transactions
                .Where(t => nsuList.Contains(t.NSU));
        }


        private Anticipation CreateAnticipationInstance()
        {
            return new Anticipation
            {
                RequestDate = DateTime.UtcNow,
                AnalysisResult = AnticipationAnalysisResult.Pending,
                AnticipationTransactions = new List<AnticipationTransaction>()
            };
        }

        private AnticipationTransaction CreateAnticipationTransaction(Transaction transaction, Anticipation anticipation)
        {
            return new AnticipationTransaction
            {
                Transaction = transaction,
                Anticipation = anticipation,
                AnticipationId = anticipation.Id,
                TransactionId = transaction.Id,
                AnticipatedValue = CalculateAnticipatedValue(transaction.NetAmount)
            };
        }

        private void SaveAnticipation(Anticipation anticipation)
        {
            _dbContext.Anticipations.Add(anticipation);
            _dbContext.SaveChanges();
        }

        private async Task<Anticipation> GetAnticipationWithTransactionsAndInstallments(int anticipationId)
        {
            return await _dbContext.Anticipations
                .Include(a => a.AnticipationTransactions)
                    .ThenInclude(at => at.Transaction)
                        .ThenInclude(t => t.InstallmentsList)
                .FirstOrDefaultAsync(a => a.Id == anticipationId);
        }

        private void UpdateAnticipatedValuesAndInstallments(Anticipation anticipation)
        {
            anticipation.AnticipatedValue = anticipation.AnticipationTransactions.Sum(at => at.AnticipatedValue);

            foreach (var anticipationTransaction in anticipation.AnticipationTransactions)
            {
                var transaction = anticipationTransaction.Transaction;
                transaction.Anticipated = true;

                foreach (var installment in transaction.InstallmentsList)
                {
                    UpdateInstallmentForAnticipation(installment, anticipationTransaction);
                }
            }
        }

        private void UpdateInstallmentForAnticipation(Installment installment, AnticipationTransaction anticipationTransaction)
        {
            installment.RepassedDate = DateTime.UtcNow;
            installment.AnticipatedValue = anticipationTransaction.AnticipatedValue;
        }

        private void SaveAnticipationChanges(Anticipation anticipation)
        {
            _dbContext.Update(anticipation);
            _dbContext.SaveChanges();
        }

        private decimal CalculateAnticipatedValue(decimal netAmount)
        {
            const decimal feePercentage = 0.038m;
            return netAmount * (1 - feePercentage);
        }
    }
}
