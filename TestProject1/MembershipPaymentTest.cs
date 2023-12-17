using Microsoft.CodeAnalysis.CSharp.Syntax;
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
    public class MembershipPaymentTest
    {
        private MembershipPaymentController _MembershipPaymentController;
        private ApplicationDbContext _context;
        private List<Card> validCardList, invalidCardList;

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
            _MembershipPaymentController = new MembershipPaymentController(_context);
            validCardList = new List<Card>
            {
                new Card { CardID = 1, CardExpirationDate = DateTime.Now.AddDays(20), CardAmount = 50, CardNumber = "6306468728056281" },
                new Card { CardID = 2, CardExpirationDate = DateTime.Now.AddDays(10), CardAmount = 51, CardNumber = "9678897487074339" },
                new Card { CardID = 3, CardExpirationDate = DateTime.Now.AddDays(30), CardAmount = 300, CardNumber = "7224541373508322" },
            };
            invalidCardList = new List<Card>
            {
                new Card { CardID = 1, CardExpirationDate = DateTime.Now.AddDays(20), CardAmount = 49, CardNumber = "468728056281" },
                new Card { CardID = 2, CardExpirationDate = DateTime.Now.AddDays(10), CardAmount = -10, CardNumber = "0" },
                new Card { CardID = 3, CardExpirationDate = DateTime.Now.AddDays(30), CardAmount = 0, CardNumber = "-1" },
                new Card { CardID = 3, CardExpirationDate = DateTime.Now.AddDays(30), CardAmount = 0, CardNumber = "-a" },
            };
        }


        private void SeedData()
        {
            // Dodajte testne podatke u In-Memory bazu
            _context.MembershipPayment.AddRange(new List<MembershipPayment>
            {
                new MembershipPayment { PaymentDate = DateTime.Now.AddDays(-3), Amount = 50, Discount = 0, CardNumber = "6306468728056281", LibraryMemberFk = 1 },
                new MembershipPayment { PaymentDate = DateTime.Now.AddDays(-5), Amount = 51, Discount = 10, CardNumber = "9678897487074339", LibraryMemberFk = 2 },
                new MembershipPayment { PaymentDate = DateTime.Now.AddDays(-2), Amount = 100, Discount = 5, CardNumber = "7224541373508322", LibraryMemberFk = 1 },
            }); ;

            _context.SaveChanges();
        }

        [TestMethod]
        public async Task CardisValidTest()
        {
            foreach (var card in validCardList)
            {
                // Assert
                var result = MembershipPaymentController.CardisValid(card.CardNumber);
                Assert.AreEqual(result, true);
            }

            foreach (var card in invalidCardList)
            {
                // Assert
                var result = MembershipPaymentController.CardisValid(card.CardNumber);
                Assert.AreEqual(result, false);
            }
        }

        [TestMethod]
        public async Task VerifyCardAmount()
        {
            // Act
            foreach (var card in validCardList)
            {
                // Assert
                var result = MembershipPaymentController.VerifyCardAmount(card, 50);
                Assert.AreEqual(result, true);
            }

            foreach (var card in invalidCardList)
            {
                // Assert
                var result = MembershipPaymentController.VerifyCardAmount(card, 50);
                Assert.AreEqual(result, false);
            }
        }

    }
}
