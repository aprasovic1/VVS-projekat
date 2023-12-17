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
    public class CardTest
    {
        private CardController _cardController;
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
            _cardController = new CardController(_context);

        }


        [TestMethod]
        public async Task CardGeneratorLengthTest()
        {
            for (int i = 0; i < 10; i++)
            {
                var result = CardController.GenerateCardNumber().Length;
                Assert.AreEqual(result, 16);
            }
        }

    }
}
