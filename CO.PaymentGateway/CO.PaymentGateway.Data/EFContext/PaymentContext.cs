﻿using CO.PaymentGateway.Business.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace CO.PaymentGateway.Data.EFContext
{
    public class PaymentContext : DbContext
    {
        public PaymentContext(DbContextOptions<PaymentContext> options):base(options)
        {

        }

        public DbSet<PaymentProcessEntity> ProcessPaymentEntities { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase(databaseName: "DBInMemory");
        }
    }
}