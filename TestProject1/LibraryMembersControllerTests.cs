namespace TestProject1
{
    [TestClass]
    public class LibraryMembersControllerTests
    {
        private ApplicationDbContext _dbContext;

        [TestInitialize]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _dbContext = new ApplicationDbContext(options);

            var mockMembers = new List<LibraryMember>
            {
                new LibraryMember { LibraryMemberId = 1, FirstName = "Sead", LastName = "Fejzagić", EmailAddress = "sfejzagic1@etf.unsa.ba" },
                new LibraryMember { LibraryMemberId = 2, FirstName = "Emina", LastName = "Selimbegović", EmailAddress = "selimbegovicemina@gmail.com" }
            };

            _dbContext.LibraryMember.AddRange(mockMembers);
            _dbContext.SaveChanges();
        }

        [TestMethod]
        public async Task Index_ReturnsViewResult()
        {
            var membersController = new LibraryMembersController(_dbContext);

            var result = await membersController.Index();

            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }

        [TestMethod]
        public async Task Details_ReturnsViewResult_WithMember()
        {
            var membersController = new LibraryMembersController(_dbContext);

            var result = await membersController.Details(1);

            Assert.IsInstanceOfType(result, typeof(ViewResult));
            var viewResult = (ViewResult)result;
            Assert.IsInstanceOfType(viewResult.Model, typeof(LibraryMember));
        }

        [TestMethod]
        public void Create_ReturnsViewResult()
        {
            var membersController = new LibraryMembersController(_dbContext);

            var result = membersController.Create();

            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }

        [TestMethod]
        public async Task Edit_ReturnsViewResult_WithMember()
        {
            var membersController = new LibraryMembersController(_dbContext);

            var result = await membersController.Edit(1);

            Assert.IsInstanceOfType(result, typeof(ViewResult));
            var viewResult = (ViewResult)result;
            Assert.IsInstanceOfType(viewResult.Model, typeof(LibraryMember));
        }

        [TestMethod]
        public async Task Delete_ReturnsViewResult_WithMember()
        {
            var membersController = new LibraryMembersController(_dbContext);

            var result = await membersController.Delete(1);

            Assert.IsInstanceOfType(result, typeof(ViewResult));
            var viewResult = (ViewResult)result;
            Assert.IsInstanceOfType(viewResult.Model, typeof(LibraryMember));
        }

        [TestMethod]
        public async Task DeleteConfirmed_DeletesMember_RedirectsToIndex()
        {
            var membersController = new LibraryMembersController(_dbContext);

            var result = await membersController.DeleteConfirmed(1);

            Assert.IsInstanceOfType(result, typeof(RedirectToActionResult));
            var redirectResult = (RedirectToActionResult)result;
            Assert.AreEqual("Index", redirectResult.ActionName);

            // Verify that the member is deleted
            Assert.IsFalse(_dbContext.LibraryMember.Any(m => m.LibraryMemberId == 1));
        }

        [TestMethod]
        public async Task Create_Post_ValidModelState_RedirectsToIndex()
        {
            var membersController = new LibraryMembersController(_dbContext);
            var member = new LibraryMember
            {
                FirstName = "New",
                LastName = "Member",
                EmailAddress = "new.member@example.com"
            };

            var result = await membersController.Create(member);

            Assert.IsInstanceOfType(result, typeof(RedirectToActionResult));
            var redirectResult = (RedirectToActionResult)result;
            Assert.AreEqual("Index", redirectResult.ActionName);

            // Verify that the member is added
            Assert.IsTrue(_dbContext.LibraryMember.Any(m => m.FirstName == "New"));
        }
    }
}
