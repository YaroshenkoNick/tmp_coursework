using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace goldbach;

public partial class GoldbachContext : DbContext
{
    public GoldbachContext()
    {
    }

    public GoldbachContext(DbContextOptions<GoldbachContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Running> Runnings { get; set; }

    public virtual DbSet<UserException> UserExceptions { get; set; }

    public Form1 Form1
    {
        get => default;
        set
        {
        }
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=Goldbach;Username=postgres;Password=1234");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Running>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Running_pkey");

            entity.ToTable("Running");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.DateTime).HasColumnType("timestamp without time zone");
            entity.Property(e => e.Input)
                .HasMaxLength(128)
                .HasColumnName("input");
            entity.Property(e => e.Mode)
                .HasMaxLength(128)
                .HasColumnName("mode");
            entity.Property(e => e.Output)
                .HasMaxLength(128)
                .HasColumnName("output");
        });

        modelBuilder.Entity<UserException>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("UserException_pkey");

            entity.ToTable("UserException");

            entity.Property(e => e.DateTimeExc)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("dateTimeExc");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
