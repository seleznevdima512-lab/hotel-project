using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using hotel.Models;

namespace hotel.Data;

public partial class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Accommodation> Accommodations { get; set; }

    public virtual DbSet<Booking> Bookings { get; set; }

    public virtual DbSet<Guest> Guests { get; set; }

    public virtual DbSet<Invoice> Invoices { get; set; }

    public virtual DbSet<Organization> Organizations { get; set; }

    public virtual DbSet<ProvidedService> ProvidedServices { get; set; }

    public virtual DbSet<Room> Rooms { get; set; }

    public virtual DbSet<RoomCategory> RoomCategories { get; set; }

    public virtual DbSet<Service> Services { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb3_general_ci")
            .HasCharSet("utf8mb3");

        modelBuilder.Entity<Accommodation>(entity =>
        {
            entity.HasKey(e => e.Record).HasName("PRIMARY");

            entity.ToTable("accommodation");

            entity.HasIndex(e => e.BookingNo, "fk_accommodation_booking1_idx");

            entity.HasIndex(e => e.RoomDesigination, "fk_accommodation_room1_idx");

            entity.Property(e => e.Record)
                .HasMaxLength(3)
                .HasColumnName("record");
            entity.Property(e => e.Arrival)
                .HasColumnType("datetime")
                .HasColumnName("arrival");
            entity.Property(e => e.BookingNo).HasColumnName("booking_no");
            entity.Property(e => e.Exit)
                .HasColumnType("datetime")
                .HasColumnName("exit");
            entity.Property(e => e.RoomDesigination)
                .HasMaxLength(4)
                .HasColumnName("room_desigination");

            entity.HasOne(d => d.BookingNoNavigation).WithMany(p => p.Accommodations)
                .HasForeignKey(d => d.BookingNo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_accommodation_booking1");

            entity.HasOne(d => d.RoomDesiginationNavigation).WithMany(p => p.Accommodations)
                .HasForeignKey(d => d.RoomDesigination)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_accommodation_room1");
        });

        modelBuilder.Entity<Booking>(entity =>
        {
            entity.HasKey(e => e.BookingNo).HasName("PRIMARY");

            entity.ToTable("booking");

            entity.HasIndex(e => e.OrganizationCard, "fk_booking_organization_idx");

            entity.Property(e => e.BookingNo).HasColumnName("booking_no");
            entity.Property(e => e.ApplicationDate)
                .HasColumnType("datetime")
                .HasColumnName("application_date");
            entity.Property(e => e.OrganizationCard).HasColumnName("organization_card");
            entity.Property(e => e.Person)
                .HasMaxLength(35)
                .HasColumnName("person");

            entity.HasOne(d => d.OrganizationCardNavigation).WithMany(p => p.Bookings)
                .HasForeignKey(d => d.OrganizationCard)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_booking_organization");
        });

        modelBuilder.Entity<Guest>(entity =>
        {
            entity.HasKey(e => e.GuestCard).HasName("PRIMARY");

            entity.ToTable("guest");

            entity.HasIndex(e => e.Record, "fk_guest_accommodation1_idx");

            entity.HasIndex(e => e.GuestCard, "guest_card_UNIQUE").IsUnique();

            entity.Property(e => e.GuestCard).HasColumnName("guest_card");
            entity.Property(e => e.BirthDay)
                .HasColumnType("datetime")
                .HasColumnName("birth_day");
            entity.Property(e => e.FullName)
                .HasMaxLength(45)
                .HasColumnName("full_name");
            entity.Property(e => e.Record)
                .HasMaxLength(3)
                .HasColumnName("record");

            entity.HasOne(d => d.RecordNavigation).WithMany(p => p.Guests)
                .HasForeignKey(d => d.Record)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_guest_accommodation1");
        });

        modelBuilder.Entity<Invoice>(entity =>
        {
            entity.HasKey(e => e.InvoiceNo).HasName("PRIMARY");

            entity.ToTable("invoice");

            entity.HasIndex(e => e.BookingNo, "fk_invoice_booking1_idx");

            entity.Property(e => e.InvoiceNo)
                .ValueGeneratedNever()
                .HasColumnName("invoice_no");
            entity.Property(e => e.AmountDue)
                .HasPrecision(10, 2)
                .HasColumnName("amount_due");
            entity.Property(e => e.BookingNo).HasColumnName("booking_no");
            entity.Property(e => e.IssueData)
                .HasColumnType("datetime")
                .HasColumnName("issue_data");

            entity.HasOne(d => d.BookingNoNavigation).WithMany(p => p.Invoices)
                .HasForeignKey(d => d.BookingNo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_invoice_booking1");
        });

        modelBuilder.Entity<Organization>(entity =>
        {
            entity.HasKey(e => e.OrganizationCard).HasName("PRIMARY");

            entity.ToTable("organization");

            entity.HasIndex(e => e.OrganizationCard, "organization_card_UNIQUE").IsUnique();

            entity.Property(e => e.OrganizationCard).HasColumnName("organization_card");
            entity.Property(e => e.Name)
                .HasMaxLength(45)
                .HasColumnName("name");
        });

        modelBuilder.Entity<ProvidedService>(entity =>
        {
            entity.HasKey(e => e.DocumentNo).HasName("PRIMARY");

            entity.ToTable("provided_service");

            entity.HasIndex(e => e.BookingNo, "fk_provided_service_booking1_idx");

            entity.HasIndex(e => e.ServiceCode, "fk_provided_service_service1_idx");

            entity.Property(e => e.DocumentNo).HasColumnName("document_no");
            entity.Property(e => e.BookingNo).HasColumnName("booking_no");
            entity.Property(e => e.Date)
                .HasColumnType("datetime")
                .HasColumnName("date");
            entity.Property(e => e.Qauntity).HasColumnName("qauntity");
            entity.Property(e => e.ServiceCode).HasColumnName("service_code");

            entity.HasOne(d => d.BookingNoNavigation).WithMany(p => p.ProvidedServices)
                .HasForeignKey(d => d.BookingNo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_provided_service_booking1");

            entity.HasOne(d => d.ServiceCodeNavigation).WithMany(p => p.ProvidedServices)
                .HasForeignKey(d => d.ServiceCode)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_provided_service_service1");
        });

        modelBuilder.Entity<Room>(entity =>
        {
            entity.HasKey(e => e.Desigination).HasName("PRIMARY");

            entity.ToTable("room");

            entity.HasIndex(e => e.Category, "fk_room_room_category1_idx");

            entity.Property(e => e.Desigination)
                .HasMaxLength(4)
                .HasColumnName("desigination");
            entity.Property(e => e.Address)
                .HasMaxLength(45)
                .HasColumnName("address");
            entity.Property(e => e.Building)
                .HasMaxLength(25)
                .HasColumnName("building");
            entity.Property(e => e.Capacity).HasColumnName("capacity");
            entity.Property(e => e.Category)
                .HasMaxLength(10)
                .HasColumnName("category");
            entity.Property(e => e.RoomNo).HasColumnName("room_no");

            entity.HasOne(d => d.CategoryNavigation).WithMany(p => p.Rooms)
                .HasForeignKey(d => d.Category)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_room_room_category1");
        });

        modelBuilder.Entity<RoomCategory>(entity =>
        {
            entity.HasKey(e => e.Category).HasName("PRIMARY");

            entity.ToTable("room_category");

            entity.Property(e => e.Category)
                .HasMaxLength(10)
                .HasColumnName("category");
            entity.Property(e => e.PriceNight)
                .HasPrecision(10, 2)
                .HasColumnName("price_night");
        });

        modelBuilder.Entity<Service>(entity =>
        {
            entity.HasKey(e => e.ServiceCode).HasName("PRIMARY");

            entity.ToTable("service");

            entity.Property(e => e.ServiceCode).HasColumnName("service_code");
            entity.Property(e => e.Name)
                .HasMaxLength(45)
                .HasColumnName("name");
            entity.Property(e => e.Price)
                .HasPrecision(10, 2)
                .HasColumnName("price");
            entity.Property(e => e.Quantity).HasColumnName("quantity");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PRIMARY");

            entity.ToTable("user");

            entity.HasIndex(e => e.Login, "login_UNIQUE").IsUnique();

            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.FailedAttempts).HasColumnName("failed_attempts");
            entity.Property(e => e.IsBlocked).HasColumnName("is_blocked");
            entity.Property(e => e.Login)
                .HasMaxLength(100)
                .HasColumnName("login");
            entity.Property(e => e.PasswordHash)
                .HasMaxLength(255)
                .HasColumnName("password_hash");
            entity.Property(e => e.Role)
                .HasMaxLength(20)
                .HasColumnName("role");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
