using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace WebApp.Models;

public partial class ServiseContext : DbContext
{
    public ServiseContext()
    {
    }

    public ServiseContext(DbContextOptions<ServiseContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Admin> Admins { get; set; }

    public virtual DbSet<Bill> Bills { get; set; }

    public virtual DbSet<MeterReading> MeterReadings { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP\\SQLEXPRESS;Database=servise;Trusted_Connection=True;Encrypt=False");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Admin>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Admins__3214EC075666FC94");

            entity.HasIndex(e => e.Email, "UQ__Admins__A9D1053486F55989").IsUnique();

            entity.Property(e => e.AdminName).HasMaxLength(100);
            entity.Property(e => e.Email).HasMaxLength(255);
            entity.Property(e => e.Password).HasMaxLength(255);
        });

        modelBuilder.Entity<Bill>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Bills__3214EC07D127DFE5");

            entity.Property(e => e.BillAmount).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.MeterReading).WithMany(p => p.Bills)
                .HasForeignKey(d => d.MeterReadingId)
                .HasConstraintName("FK__Bills__MeterRead__4222D4EF");
        });

        modelBuilder.Entity<MeterReading>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__MeterRea__3214EC07EB6EC475");

            entity.Property(e => e.MeterReading1)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("MeterReading");

            entity.HasOne(d => d.User).WithMany(p => p.MeterReadings)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__MeterRead__UserI__3F466844");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Users__3214EC071322D182");

            entity.HasIndex(e => e.NationalId, "UQ__Users__E9AA32FA29285AE8").IsUnique();

            entity.Property(e => e.Address).HasMaxLength(255);
            entity.Property(e => e.MeterNumber).HasMaxLength(100);
            entity.Property(e => e.NationalId).HasMaxLength(14);
            entity.Property(e => e.Password).HasMaxLength(255);
            entity.Property(e => e.Role)
                .HasMaxLength(20)
                .HasDefaultValue("User");
            entity.Property(e => e.UserName).HasMaxLength(100);

            entity.HasOne(d => d.CreatedByAdmin).WithMany(p => p.Users)
                .HasForeignKey(d => d.CreatedByAdminId)
                .HasConstraintName("FK__Users__CreatedBy__3B75D760");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
