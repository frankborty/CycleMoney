using System;
using System.Collections.Generic;
using CycleMoney.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace CycleMoney.Shared.Data;

public partial class CycleMoneyRemoteDbContext : DbContext
{
    public CycleMoneyRemoteDbContext(DbContextOptions<CycleMoneyRemoteDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Expense> Expenses { get; set; }

    public virtual DbSet<PaymentType> PaymentTypes { get; set; }

    public virtual DbSet<RecurrenceType> RecurrenceTypes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Expense>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Expenses_pkey");

            entity.Property(e => e.Id).UseIdentityAlwaysColumn();
            entity.Property(e => e.Amount).HasPrecision(10, 2);
            entity.Property(e => e.Description).HasMaxLength(255);

            entity.HasOne(d => d.PaymentType).WithMany(p => p.Expenses)
                .HasForeignKey(d => d.PaymentTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("expenses_paymenttypeid_fkey");

            entity.HasOne(d => d.RecurrenceType).WithMany(p => p.Expenses)
                .HasForeignKey(d => d.RecurrenceTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("expenses_recurrencetypeid_fkey");
        });

        modelBuilder.Entity<PaymentType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("paymenttypes_pkey");

            entity.HasIndex(e => e.Name, "paymenttypes_name_key").IsUnique();

            entity.Property(e => e.Id).HasDefaultValueSql("nextval('paymenttypes_id_seq'::regclass)");
            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<RecurrenceType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("recurrencetypes_pkey");

            entity.HasIndex(e => e.Name, "recurrencetypes_name_key").IsUnique();

            entity.Property(e => e.Id).HasDefaultValueSql("nextval('recurrencetypes_id_seq'::regclass)");
            entity.Property(e => e.Name).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
