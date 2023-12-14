using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using Moq;
using System.Collections.Generic;
using System.Linq;
using VVS_projekat.Models;
using Microsoft.AspNetCore.Mvc;
using VVS_projekat.Controllers;
using VVS_projekat.Data;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using Microsoft.AspNetCore.Http;
using System.Linq.Expressions;
using System;
using System.Reflection.Metadata;

namespace Testovi1

    
{
    
    [TestClass]
    public class UnitTest1
    {
        
        private ApplicationDbContext _dbContext;
  

        [TestInitialize]
        public  void Setup()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _dbContext = new ApplicationDbContext(options);

            //Act



            var mockBooks = new List<Book>
            {
                new Book { BookId = 1, Title = "Book 1", Author = "Author 1", Genre = "Genre 1",Price=10,Status="Available" },
                new Book { BookId = 2, Title = "Book 2", Author = "Author 2", Genre = "Genre 2" ,Price=15,Status="Available"}
            };
            var mockRatings = new List<Rating>
            {
                new Rating { RatingValue = 4, BookFk = 1 },
                new Rating { RatingValue = 5, BookFk = 1 }
            };
            _dbContext.Book.AddRange(mockBooks);
            _dbContext.Rating.AddRange(mockRatings);

            _dbContext.SaveChanges();



        }


        [TestMethod]
        public async Task Index_ReturnsViewResult()
        {
            var recommendationsController = new RecommendationsController(_dbContext);

            var result = await recommendationsController.Index();

            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }

        [TestMethod]
        public async Task GetAllBooksAsync_ReturnsListOfBooks()
        {
            var recommendationsController = new RecommendationsController(_dbContext);


            var mockBooks = new List<Book>
            {
                new Book { BookId = 1, Title = "Book 1", Author = "Author 1", Genre = "Genre 1",Price=10,Status="Available" },
                new Book { BookId = 2, Title = "Book 2", Author = "Author 2", Genre = "Genre 2" ,Price=15,Status="Available"}
            };

            var knjigeFromController = await recommendationsController.GetAllBooksAsync();
            Assert.IsInstanceOfType(knjigeFromController, typeof(List<Book>));

        }

        [TestMethod]
        public void CalculateAverageRatings_ReturnsCorrectAverage()
        {
            var recommendationsController = new RecommendationsController(_dbContext);


            var mockBooks = new List<Book>
            {
                new Book { BookId = 1, Title = "Book 1", Author = "Author 1", Genre = "Genre 1",Price=10,Status="Available" },
                new Book { BookId = 2, Title = "Book 2", Author = "Author 2", Genre = "Genre 2" ,Price=15,Status="Available"}
            };

            var mockRatings = new List<Rating>
            {
                new Rating { RatingValue = 4, BookFk = 1 },
                new Rating { RatingValue = 5, BookFk = 1 }
            };


            var result = recommendationsController.CalculateAverageRatings(mockBooks);


            Assert.AreEqual(4.5, result.First().AverageRating);
        }


        

    }


    
    
}
