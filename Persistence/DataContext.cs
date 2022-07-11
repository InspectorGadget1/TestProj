using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace Persistence
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Unique fields
            modelBuilder.Entity<Contact>()
                .HasIndex(c => c.Email)
                .IsUnique();

            modelBuilder.Entity<Account>()
                .HasIndex(a => a.name)
                .IsUnique();

            // Primary key
            modelBuilder.Entity<Incident>()
                .HasKey(i => i.Name);

            // Relationships one to many
            modelBuilder.Entity<Contact>()
                .HasOne<Account>(c => c.Account)
                .WithMany(a => a.Contacts)
                .HasForeignKey(c => c.AccountId);

            modelBuilder.Entity<Account>()
                .HasOne<Incident>(a => a.Incident)
                .WithMany(i => i.Accounts)
                .HasForeignKey(a => a.IncidentName);
        }

        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Incident> Incidents { get; set; }
    }
}