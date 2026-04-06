using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using MyApp.Shared.Models;

namespace MyApp.Shared.Data;

public partial class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Paymenttype> Paymenttypes { get; set; }

    public virtual DbSet<Recurrencetype> Recurrencetypes { get; set; }

    public virtual DbSet<Recurringexpense> Recurringexpenses { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Paymenttype>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("paymenttypes_pkey");

            entity.ToTable("paymenttypes");

            entity.HasIndex(e => e.Name, "paymenttypes_name_key").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Recurrencetype>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("recurrencetypes_pkey");

            entity.ToTable("recurrencetypes");

            entity.HasIndex(e => e.Name, "recurrencetypes_name_key").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Recurringexpense>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("recurringexpenses_pkey");

            entity.ToTable("recurringexpenses");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Amount)
                .HasPrecision(10, 2)
                .HasColumnName("amount");
            entity.Property(e => e.Automatic).HasColumnName("automatic");
            entity.Property(e => e.Date).HasColumnName("date");
            entity.Property(e => e.Description)
                .HasMaxLength(255)
                .HasColumnName("description");
            entity.Property(e => e.Paymenttypeid).HasColumnName("paymenttypeid");
            entity.Property(e => e.Recurrencetypeid).HasColumnName("recurrencetypeid");

            entity.HasOne(d => d.Paymenttype).WithMany(p => p.Recurringexpenses)
                .HasForeignKey(d => d.Paymenttypeid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("recurringexpenses_paymenttypeid_fkey");

            entity.HasOne(d => d.Recurrencetype).WithMany(p => p.Recurringexpenses)
                .HasForeignKey(d => d.Recurrencetypeid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("recurringexpenses_recurrencetypeid_fkey");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
