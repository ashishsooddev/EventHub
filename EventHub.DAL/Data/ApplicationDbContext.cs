using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventHub.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EventHub.DAL.Data;

public class ApplicationDbContext : IdentityDbContext<IdentityUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Category> Categories { get; set; }

    public DbSet<Event> Events { get; set; }

    public DbSet<Registration> Registrations { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Category>()
            .HasKey(c => c.CategoryId);

        modelBuilder.Entity<Category>()
            .Property(c => c.Name)
            .IsRequired()
            .HasMaxLength(100);

        modelBuilder.Entity<Category>()
            .Property(c => c.Description)
            .HasMaxLength(500);

        modelBuilder.Entity<Event>()
            .HasKey(e => e.EventId);

        modelBuilder.Entity<Event>()
            .Property(e => e.Title)
            .IsRequired()
            .HasMaxLength(150);

        modelBuilder.Entity<Event>()
            .Property(e => e.Description)
            .HasMaxLength(1000);

        modelBuilder.Entity<Event>()
            .Property(e => e.Location)
            .IsRequired()
            .HasMaxLength(200);

        modelBuilder.Entity<Event>()
            .Property(e => e.Capacity)
            .IsRequired();

        modelBuilder.Entity<Event>()
            .HasMany(e => e.Categories)
            .WithMany(c => c.Events)
            .UsingEntity(j => j.ToTable("EventCategories"));

        modelBuilder.Entity<Registration>()
            .HasKey(r => r.RegistrationId);

        modelBuilder.Entity<Registration>()
            .Property(r => r.UserId)
            .IsRequired()
            .HasMaxLength(450);

        modelBuilder.Entity<Registration>()
            .HasOne(r => r.Event)
            .WithMany(e => e.Registrations)
            .HasForeignKey(r => r.EventId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Registration>()
            .Property(r => r.RegistrationDate)
            .IsRequired();
    }
}
