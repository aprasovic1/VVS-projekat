namespace TestProject1
{
    [TestClass]
    public class BookControllerTests
    {
        private ApplicationDbContext _dbContext;

        [TestInitialize]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _dbContext = new ApplicationDbContext(options);

            var mockBooks = new List<Book>
            {
                new Book { BookId = 1, Title = "Book 1", Author = "Author 1", Genre = "Genre 1", Price = 10, Status = "Available" },
                new Book { BookId = 2, Title = "Book 2", Author = "Author 2", Genre = "Genre 2", Price = 15, Status = "Available" }
            };
            var mockReservations = new List<Reservation>
            {
                new Reservation { ReservationId = 1, IssuedDate = DateTime.Now, Status = "Book"},
                new Reservation { ReservationId = 2, IssuedDate = DateTime.Now, Status = "Book"}
            };

            _dbContext.Book.AddRange(mockBooks);
            _dbContext.Reservation.AddRange(mockReservations);

            _dbContext.SaveChanges();
        }

        [TestMethod]
        public async Task Index_ReturnsViewResult()
        {
            var bookController = new BookController(_dbContext);

            var result = await bookController.Index();

            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }

        [TestMethod]
        public async Task Details_ReturnsViewResult_WithBook()
        {
            var bookController = new BookController(_dbContext);

            var result = await bookController.Details(1);

            Assert.IsInstanceOfType(result, typeof(ViewResult));
            var viewResult = (ViewResult)result;
            Assert.IsInstanceOfType(viewResult.Model, typeof(Book));
        }

        [TestMethod]
        public async Task Create_ReturnsViewResult()
        {
            var bookController = new BookController(_dbContext);

            var result = bookController.Create();

            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }

        [TestMethod]
        public async Task Edit_ReturnsViewResult_WithBook()
        {
            var bookController = new BookController(_dbContext);

            var result = await bookController.Edit(1);

            Assert.IsInstanceOfType(result, typeof(ViewResult));
            var viewResult = (ViewResult)result;
            Assert.IsInstanceOfType(viewResult.Model, typeof(Book));
        }


        [TestMethod]
        public async Task Delete_ReturnsViewResult_WithBook()
        {
            var bookController = new BookController(_dbContext);

            var result = await bookController.Delete(1);

            Assert.IsInstanceOfType(result, typeof(ViewResult));
            var viewResult = (ViewResult)result;
            Assert.IsInstanceOfType(viewResult.Model, typeof(Book));
        }

        [TestMethod]
        public async Task DeleteConfirmed_DeletesBook_RedirectsToIndex()
        {
            var bookController = new BookController(_dbContext);

            var result = await bookController.DeleteConfirmed(1);

            Assert.IsInstanceOfType(result, typeof(RedirectToActionResult));
            var redirectResult = (RedirectToActionResult)result;
            Assert.AreEqual("Index", redirectResult.ActionName);

            // Verify that the book is deleted
            Assert.IsFalse(_dbContext.Book.Any(b => b.BookId == 1));
        }

        [TestMethod]
        public async Task Create_Post_ValidModelState_RedirectsToIndex()
        {
            var bookController = new BookController(_dbContext);
            var book = new Book { Title = "New Book", Author = "New Author", Genre = "New Genre", Price = 20, Status = "Available" };

            var result = await bookController.Create(book);

            Assert.IsInstanceOfType(result, typeof(RedirectToActionResult));
            var redirectResult = (RedirectToActionResult)result;
            Assert.AreEqual("Index", redirectResult.ActionName);

            // Verify that the book is added
            Assert.IsTrue(_dbContext.Book.Any(b => b.Title == "New Book"));
        }

        [TestMethod]
        public async Task Index_ReturnsAllBooks_WhenSearchQueryIsNull()
        {
            var bookController = new BookController(_dbContext);

            var result = await bookController.Index();

            Assert.IsInstanceOfType(result, typeof(ViewResult));
            var viewResult = (ViewResult)result;
            var model = viewResult.Model as IEnumerable<Book>;

            Assert.IsNotNull(model);
            Assert.AreEqual(2, model.Count()); // Assuming you have two books in the seed data
        }

        [TestMethod]
        public async Task Index_ReturnsFilteredBooks_WhenSearchQueryIsProvided()
        {
            var bookController = new BookController(_dbContext);

            var result = await bookController.Index("Book 1");

            Assert.IsInstanceOfType(result, typeof(ViewResult));
            var viewResult = (ViewResult)result;
            var model = viewResult.Model as IEnumerable<Book>;

            Assert.IsNotNull(model);
            Assert.AreEqual(1, model.Count()); // Assuming the search query filters to one book
        }

        [TestMethod]
        public async Task Index_ReturnsEmptyResult_WhenSearchQueryDoesNotMatch()
        {
            var bookController = new BookController(_dbContext);

            var result = await bookController.Index("Nonexistent Book");

            Assert.IsInstanceOfType(result, typeof(ViewResult));
            var viewResult = (ViewResult)result;
            var model = viewResult.Model as IEnumerable<Book>;

            Assert.IsNotNull(model);
            Assert.AreEqual(0, model.Count());
        }
    }
}
