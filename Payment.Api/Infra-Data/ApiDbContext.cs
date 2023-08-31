using Microsoft.EntityFrameworkCore;
using Payment.Api.Models.EntityModel;
using Payment.API.Models.EntityModel;

namespace Payment.Api.Data
{
    public class ApiDbContext : DbContext
    {
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<Installment> Installments { get; set; }
        public DbSet<Anticipation> Anticipations { get; set; }
        public DbSet<AnticipationTransaction> AnticipationTransactions { get; set; }

        public ApiDbContext(DbContextOptions<ApiDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Transaction>(entity =>
            {
                entity.Property(e => e.Id)
                    .IsRequired()
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.NSU)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.TransactionDate)
                    .IsRequired();

                entity.Property(e => e.ApprovalDate);

                entity.Property(e => e.RejectionDate);

                entity.Property(e => e.Anticipated)
                    .IsRequired();

                entity.Property(e => e.AcquirerConfirmation)
                    .IsRequired();

                entity.Property(e => e.GrossAmount)
                    .IsRequired();

                entity.Property(e => e.NetAmount)
                    .IsRequired();

                entity.Property(e => e.Installments)
                    .IsRequired();

                entity.Property(e => e.LastFourDigits)
                    .IsRequired()
                    .HasMaxLength(4);

                entity.HasMany(e => e.InstallmentsList)
                    .WithOne(e => e.Transaction)
                    .HasForeignKey(e => e.TransactionId);
            });

            modelBuilder.Entity<Installment>(entity =>
            {
                entity.Property(e => e.Id)
                    .IsRequired()
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.TransactionId)
                    .IsRequired();

                entity.Property(e => e.GrossAmount)
                    .IsRequired();

                entity.Property(e => e.NetAmount)
                    .IsRequired();

                entity.Property(e => e.Number)
                    .IsRequired();

                entity.Property(e => e.AnticipatedValue);

                entity.Property(e => e.ExpectedPaymentDate);

                entity.Property(e => e.RepassedDate);

                entity.HasOne(e => e.Transaction)
                    .WithMany(e => e.InstallmentsList)
                    .HasForeignKey(e => e.TransactionId);
            });

            modelBuilder.Entity<Anticipation>(entity =>
            {
                entity.Property(e => e.Id)
                    .IsRequired()
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.RequestDate)
                    .IsRequired();

                entity.Property(e => e.AnalysisStartDate);

                entity.Property(e => e.AnalysisEndDate);

                entity.Property(e => e.AnalysisResult)
                    .IsRequired();

                entity.Property(e => e.RequestedValue)
                    .IsRequired();

                entity.Property(e => e.AnticipatedValue)
                    .IsRequired();

                entity.HasMany(e => e.AnticipationTransactions)
                    .WithOne(e => e.Anticipation)
                    .HasForeignKey(e => e.AnticipationId);
            });

            modelBuilder.Entity<AnticipationTransaction>(entity =>
            {
                entity.Property(e => e.Id)
                    .IsRequired()
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.AnticipationId)
                    .IsRequired();

                entity.Property(e => e.TransactionId)
                    .IsRequired();

                entity.Property(e => e.AnticipatedValue)
                    .IsRequired();

                entity.HasOne(e => e.Anticipation)
                    .WithMany(e => e.AnticipationTransactions)
                    .HasForeignKey(e => e.AnticipationId);

                entity.HasOne(e => e.Transaction)
                    .WithMany()
                    .HasForeignKey(e => e.TransactionId);
            });
        }
    }
}
