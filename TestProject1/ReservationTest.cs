using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VVS_projekat.Controllers;
using VVS_projekat.Data;
using VVS_projekat.Models;

namespace TestProject1
{
    [TestClass]
    public class ReservationTest
    {
        private ReservationController _reservationController;
        private ApplicationDbContext _context;

        [TestInitialize]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDataBase")
                .Options;

            _context = new ApplicationDbContext(options);
            _context.Database.EnsureDeleted(); // Obrisati postojeću bazu
            _context.Database.EnsureCreated(); // Ponovno je stvoriti
            SeedData(); // Ponovno dodajte podatke
            _reservationController = new ReservationController(_context);
        }


        private void SeedData()
        {
            // Dodajte testne podatke u In-Memory bazu
            _context.Reservation.AddRange(new List<Reservation>
            {
                new Reservation { IssuedDate = DateTime.Now.AddDays(-3), ReturnDate = DateTime.Now.AddDays(-1), Status = "Book", LibraryMemberFk = 1 },
                new Reservation { IssuedDate = DateTime.Now.AddDays(-5), ReturnDate = DateTime.Now.AddDays(-2), Status = "Book", LibraryMemberFk = 2 },
                new Reservation { IssuedDate = DateTime.Now.AddDays(-2), ReturnDate = DateTime.Now.AddDays(1), Status = "Book", LibraryMemberFk = 1 },
            });

            _context.SaveChanges();
        }

        [TestMethod]
        public async Task CountReservation_MultipleReservations_ReturnsCorrectCount()
        {
            // Act
            var result = await _reservationController.CountReservation();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(result.numberOfReservations, 3);
        }

        [TestMethod]
        public async Task LateActivatedReservations_ActivatedReservationsAfterIssuedDate_ReturnsCorrectReservations()
        {
            // Arrange
            _context.Reservation.RemoveRange(_context.Reservation);
            await _context.SaveChangesAsync();

            // Dodajte nekoliko rezervacija koje su aktivirane nakon datuma izdavanja
            _context.Reservation.AddRange(new List<Reservation>
            {
                new Reservation { IssuedDate = DateTime.Now.AddDays(-5), ReturnDate = DateTime.Now.AddDays(-3), Status = "Activated" },
                new Reservation { IssuedDate = DateTime.Now.AddDays(-3), ReturnDate = DateTime.Now.AddDays(-1), Status = "Activated" },
                // Dodajte više rezervacija po potrebi
            });
            await _context.SaveChangesAsync();

            // Act
            var result = await _reservationController.LateActivatedReservations();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(result.LateActivatedReservations.Count, 2); // Promenite broj prema očekivanim rezultatima
        }

    }
}
