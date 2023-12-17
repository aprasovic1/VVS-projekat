using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VVS_projekat.Controllers;
using VVS_projekat.Data;
using VVS_projekat.Models;

namespace VVS_projekat.Tests
{
    [TestClass]
    public class BookControllerTests
    {
        private Mock<ApplicationDbContext> _mockContext;
        private BookController _controller;
        private Mock<DbSet<Book>> _mockSet;
        private Mock<DbSet<Publisher>> _mockPublisherSet;
        private Mock<DbSet<Reservation>> _mockReservationSet;

        [TestInitialize]
        public void Initialize()
        {
            _mockContext = new Mock<ApplicationDbContext>();
            _mockSet = new Mock<DbSet<Book>>();
            _mockPublisherSet = new Mock<DbSet<Publisher>>();
            _mockReservationSet = new Mock<DbSet<Reservation>>();

            _mockContext.Setup(m => m.Book).Returns(_mockSet.Object);
            _mockContext.Setup(m => m.Publisher).Returns(_mockPublisherSet.Object);
            _mockContext.Setup(m => m.Reservation).Returns(_mockReservationSet.Object);

            _controller = new BookController(_mockContext.Object);
        }

        [TestMethod]
        public async Task Index_ReturnsViewResult_WithListOfBooks()
        {
            var books = new List<Book>
            {
                new Book { BookId = 1, Title = "Book 1", Author = "Author 1", Genre = "Genre 1", Price = 10, Status = "Available" },
                new Book { BookId = 2, Title = "Book 2", Author = "Author 2", Genre = "Genre 2", Price = 15, Status = "Unavailable" }
            };
            _mockSet.Setup(m => m.Include(It.IsAny<string>())).Returns(_mockSet.Object);
            _mockSet.Setup(m => m.ToListAsync()).ReturnsAsync(books);

            var result = await _controller.Index();

            Assert.IsInstanceOfType(result, typeof(ViewResult));
            var viewResult = result as ViewResult;
            Assert.IsInstanceOfType(viewResult.Model, typeof(List<Book>));
            var model = viewResult.Model as List<Book>;
            Assert.AreEqual(2, model.Count);
        }

        [TestMethod]
        public async Task Details_WithValidId_ReturnsBook()
        {
            var book = new Book { BookId = 1, Title = "Test Book", Author = "Test Author" };
            _mockSet.Setup(m => m.FindAsync(1)).ReturnsAsync(book);

            var result = await _controller.Details(1);

            Assert.IsInstanceOfType(result, typeof(ViewResult));
            var viewResult = result as ViewResult;
            Assert.AreEqual(book, viewResult.Model);
        }

        [TestMethod]
        public async Task Details_WithInvalidId_ReturnsNotFound()
        {
            _mockSet.Setup(m => m.FindAsync(It.IsAny<int>())).ReturnsAsync((Book)null);

            var result = await _controller.Details(999);

            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        [TestMethod]
        public async Task Create_Post_ValidBook_AddsBookAndRedirects()
        {
            var book = new Book { BookId = 3, Title = "New Book", Author = "New Author" };
            _mockSet.Setup(m => m.Add(It.IsAny<Book>()));

            var result = await _controller.Create(book);

            _mockSet.Verify(m => m.Add(It.IsAny<Book>()), Times.Once);
            Assert.IsInstanceOfType(result, typeof(RedirectToActionResult));
        }

        [TestMethod]
        public async Task Edit_Post_ValidBook_UpdatesAndRedirects()
        {
            var book = new Book { BookId = 1, Title = "Updated Book", Author = "Updated Author" };
            _mockSet.Setup(m => m.Update(It.IsAny<Book>()));
            _mockSet.Setup(m => m.FindAsync(1)).ReturnsAsync(book);

            var result = await _controller.Edit(1, book);

            _mockSet.Verify(m => m.Update(It.IsAny<Book>()), Times.Once);
            Assert.IsInstanceOfType(result, typeof(RedirectToActionResult));
        }

        [TestMethod]
        public async Task Delete_Post_ValidId_DeletesBookAndRedirects()
        {
            var book = new Book { BookId = 1, Title = "Book to Delete", Author = "Author" };
            _mockSet.Setup(m => m.FindAsync(1)).ReturnsAsync(book);
            _mockSet.Setup(m => m.Remove(It.IsAny<Book>()));

            var result = await _controller.DeleteConfirmed(1);

            _mockSet.Verify(m => m.Remove(It.IsAny<Book>()), Times.Once);
            Assert.IsInstanceOfType(result, typeof(RedirectToActionResult));
        }

        [TestMethod]
        public async Task MostReservedBookTitle_ReturnsCorrectResult()
        {
            // Mock data and logic for testing MostReservedBookTitle
            // This method will need to mock the logic within MostReservedBookTitle
            // as it involves complex queries and joins.
        }

        // Additional custom method tests as per your controller
    }
}
