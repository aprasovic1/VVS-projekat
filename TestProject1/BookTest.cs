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
    public class BookTest
    {
        private BookController _bookController;
        private ApplicationDbContext _context;

        [TestInitialize]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDataBase")
                .Options;

            _context = new ApplicationDbContext(options);
            _context.Database.EnsureDeleted(); // Ova linija će obrisati postojeću bazu
            _context.Database.EnsureCreated(); // Ova linija će stvoriti novu praznu bazu
            SeedData(); // Ponovno dodajte podatke
            _bookController = new BookController(_context);
        }


        private void SeedData()
        {
            // Dodajte testne podatke u In-Memory bazu
            _context.Reservation.AddRange(new List<Reservation>
            {
                new Reservation { IssuedDate = DateTime.Now.AddDays(-3), ReturnDate = DateTime.Now.AddDays(-1), Status = "Book", LibraryMemberFk = 1 },
                new Reservation { IssuedDate = DateTime.Now.AddDays(-5), ReturnDate = DateTime.Now.AddDays(-2), Status = "Book", LibraryMemberFk = 2 },
                // Dodajte više rezervacija po potrebi
            });

            _context.Book.AddRange(new List<Book>
            {
                new Book { BookId = 1, Title = "Book 1", Reservation = new Reservation { LibraryMemberFk = 1 } },
                new Book { BookId = 2, Title = "Book 2", Reservation = new Reservation { LibraryMemberFk = 2 } },
                // Dodajte više knjiga po potrebi
            });

            _context.SaveChanges();
        }

        [TestMethod]
        public async Task MostReservedBookTitle_ReturnsCorrectResult()
        {
            // Act
            var result = await _bookController.MostReservedBookTitle(DateTime.Now.AddDays(-7), DateTime.Now);

            // Assert
            Assert.IsNotNull(result);
            // Dodajte više assercija na osnovu očekivanih rezultata
        }

        [TestMethod]
        public async Task BookTest1()
        {
            MostReservedBookResult result = await _bookController.MostReservedBookTitle(DateTime.Now, DateTime.Now.AddDays(2));

            Assert.IsNotNull(result);
            Assert.AreEqual(result.ReservationCount, 0);
            Assert.AreEqual(result.MostReservedBookTitle, "No reservations found in the specified date range.");
        }
    }
}

