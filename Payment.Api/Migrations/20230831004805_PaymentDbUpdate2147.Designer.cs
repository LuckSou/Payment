﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Payment.Api.Data;

#nullable disable

namespace Payment.Api.Migrations
{
    [DbContext(typeof(ApiDbContext))]
    [Migration("20230831004805_PaymentDbUpdate2147")]
    partial class PaymentDbUpdate2147
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Payment.API.Models.EntityModel.Installment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<decimal?>("AnticipatedValue")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime?>("ExpectedPaymentDate")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("GrossAmount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("NetAmount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("Number")
                        .HasColumnType("int");

                    b.Property<DateTime?>("RepassedDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("TransactionId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("TransactionId");

                    b.ToTable("Installments");
                });

            modelBuilder.Entity("Payment.API.Models.EntityModel.Transaction", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool>("AcquirerConfirmation")
                        .HasColumnType("bit");

                    b.Property<bool>("Anticipated")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("ApprovalDate")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("GrossAmount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("Installments")
                        .HasColumnType("int");

                    b.Property<string>("LastFourDigits")
                        .IsRequired()
                        .HasMaxLength(4)
                        .HasColumnType("nvarchar(4)");

                    b.Property<string>("NSU")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<decimal>("NetAmount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime?>("RejectionDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("TransactionDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Transactions");
                });

            modelBuilder.Entity("Payment.API.Models.EntityModel.Installment", b =>
                {
                    b.HasOne("Payment.API.Models.EntityModel.Transaction", "Transaction")
                        .WithMany("InstallmentsList")
                        .HasForeignKey("TransactionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Transaction");
                });

            modelBuilder.Entity("Payment.API.Models.EntityModel.Transaction", b =>
                {
                    b.Navigation("InstallmentsList");
                });
#pragma warning restore 612, 618
        }
    }
}
