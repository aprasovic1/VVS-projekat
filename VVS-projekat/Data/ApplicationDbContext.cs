using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using VVS_projekat.Models;

namespace VVS_projekat.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Book> Book { get; set; }
        public DbSet<Librarian> Librarian { get; set; }
        public DbSet<LibraryMember> LibraryMember { get; set; }
       // public DbSet<LibraryUser> LibraryUser { get; set; }
        public DbSet<MembershipPayment> MembershipPayment { get; set; }
        public DbSet<Publisher> Publisher { get; set; }
        public DbSet<Rating> Rating { get; set; }
        public DbSet<Reservation> Reservation { get; set; }
        public DbSet<ReservationPayment> ReservationPayment { get; set; }
        public DbSet<Voucher> Voucher { get; set; }
        public DbSet<Card> Card { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>().ToTable("Book");
            modelBuilder.Entity<Librarian>().ToTable("Librarian");
            modelBuilder.Entity<LibraryMember>().ToTable("LibraryMember");
            //modelBuilder.Entity<LibraryUser>().ToTable("LibraryUser");
            modelBuilder.Entity<MembershipPayment>().ToTable("MembershipPayment");
            modelBuilder.Entity<Publisher>().ToTable("Publisher");
            modelBuilder.Entity<Rating>().ToTable("Rating");
            modelBuilder.Entity<Reservation>().ToTable("Reservation");
            modelBuilder.Entity<ReservationPayment>().ToTable("ReservationPayment");
            modelBuilder.Entity<Voucher>().ToTable("Voucher");
            modelBuilder.Entity<Card>().ToTable("Card");
            base.OnModelCreating(modelBuilder);
        }

    }
}
