using Microsoft.AspNetCore.Mvc.Rendering;

namespace TestProject1
{
    [TestClass]
    public class ReservationControllerTests
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
        public async Task BookCatalog_ReturnsViewResult_WithListOfBooks()
        {
            var reservationController = new ReservationController(_dbContext);

            var result = await reservationController.BookCatalog();

            Assert.IsInstanceOfType(result, typeof(ViewResult));
            var viewResult = (ViewResult)result;
            Assert.IsInstanceOfType(viewResult.Model, typeof(List<Book>));
        }

        [TestMethod]
        public async Task Index_ReturnsViewResult_WithListOfReservations()
        {
            var reservationController = new ReservationController(_dbContext);

            var result = await reservationController.Index();

            Assert.IsInstanceOfType(result, typeof(ViewResult));
            var viewResult = (ViewResult)result;
            Assert.IsInstanceOfType(viewResult.Model, typeof(List<Reservation>));
        }

        [TestMethod]
        public async Task Details_ReturnsViewResult_WithReservation()
        {
            var reservationController = new ReservationController(_dbContext);

            var result = await reservationController.Details(1);

            Assert.IsInstanceOfType(result, typeof(ViewResult));
            var viewResult = (ViewResult)result;
            Assert.IsInstanceOfType(viewResult.Model, typeof(Reservation));
        }

        [TestMethod]
        public void Create_ReturnsViewResult_WithBooksSelectList()
        {
            var reservationController = new ReservationController(_dbContext);

            var result = reservationController.Create();

            Assert.IsInstanceOfType(result, typeof(ViewResult));
            var viewResult = (ViewResult)result;
            Assert.IsInstanceOfType(viewResult.ViewData["Books"], typeof(SelectList));
        }

        [TestMethod]
        public async Task Create_Post_ValidModelState_RedirectsToIndex()
        {
            var reservationController = new ReservationController(_dbContext);
            var reservation = new Reservation { IssuedDate = DateTime.Now, ReturnDate = DateTime.Now.AddDays(7), Status = "Active" };
            var bookId = 1;

            var result = await reservationController.Create(reservation, bookId);

            Assert.IsInstanceOfType(result, typeof(RedirectToActionResult));
            var redirectResult = (RedirectToActionResult)result;
            Assert.AreEqual("Index", redirectResult.ActionName);

            // Verify that the reservation and associated book are added
            Assert.IsTrue(_dbContext.Reservation.Any(r => r.IssuedDate == reservation.IssuedDate));
        }

        [TestMethod]
        public async Task Edit_ReturnsViewResult_WithReservation()
        {
            var reservationController = new ReservationController(_dbContext);

            var result = await reservationController.Edit(1);

            Assert.IsInstanceOfType(result, typeof(ViewResult));
            var viewResult = (ViewResult)result;
            Assert.IsInstanceOfType(viewResult.Model, typeof(Reservation));
        }


        [TestMethod]
        public async Task Delete_ReturnsViewResult_WithReservation()
        {
            var reservationController = new ReservationController(_dbContext);

            var result = await reservationController.Delete(1);

            Assert.IsInstanceOfType(result, typeof(ViewResult));
            var viewResult = (ViewResult)result;
            Assert.IsInstanceOfType(viewResult.Model, typeof(Reservation));
        }

        [TestMethod]
        public async Task DeleteConfirmed_DeletesReservation_RedirectsToIndex()
        {
            var reservationController = new ReservationController(_dbContext);

            var result = await reservationController.DeleteConfirmed(1);

            Assert.IsInstanceOfType(result, typeof(RedirectToActionResult));
            var redirectResult = (RedirectToActionResult)result;
            Assert.AreEqual("Index", redirectResult.ActionName);

            // Verify that the reservation is deleted
            Assert.IsFalse(_dbContext.Reservation.Any(r => r.ReservationId == 1));
        }

        [TestMethod]
        public async Task CountReservation_ReturnsCountReservationResult()
        {
            var reservationController = new ReservationController(_dbContext);

            var result = await reservationController.CountReservation();

            Assert.IsInstanceOfType(result, typeof(CountReservationResult));
            Assert.AreEqual(2, result.numberOfReservations);
        }

        [TestMethod]
        public async Task LateActivatedReservations_ReturnsLateActivatedResult()
        {
            var reservationController = new ReservationController(_dbContext);

            var result = await reservationController.LateActivatedReservations();

            Assert.IsInstanceOfType(result, typeof(LateActivatedResult));
            Assert.AreEqual(0, result.LateActivatedReservations.Count);
        }
    }
}
