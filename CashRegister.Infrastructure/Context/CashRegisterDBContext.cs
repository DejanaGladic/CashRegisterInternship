﻿using CashRegister.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
namespace CashRegister.Infrastructure.Context
{
    public class CashRegisterDBContext : DbContext
    {
        public CashRegisterDBContext(DbContextOptions<CashRegisterDBContext> options) : base(options)
        {
        }
        public DbSet<Product> Products { get; set; }
        public DbSet<Bill> Bills { get; set; }
        public DbSet<ProductBill> ProductBills { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProductBill>().HasKey(x => new { x.BillNumber, x.ProductId });
        }
    }
}
