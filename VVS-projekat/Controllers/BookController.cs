using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using VVS_projekat.Data;
using VVS_projekat.Models;

namespace VVS_projekat.Controllers
{
    public class BookController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BookController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Book
        public async Task<IActionResult> Index(string searchQuery = null)
        {
            IQueryable<Book> booksQuery = _context.Book.Include(b => b.Publisher);

            if (!String.IsNullOrEmpty(searchQuery))
            {
                booksQuery = booksQuery.Where(book =>
                    book.Title.Contains(searchQuery, StringComparison.OrdinalIgnoreCase) ||
                    book.Author.Contains(searchQuery, StringComparison.OrdinalIgnoreCase) ||
                    book.Genre.Contains(searchQuery, StringComparison.OrdinalIgnoreCase) ||
                    book.Publisher.PublisherName.Contains(searchQuery, StringComparison.OrdinalIgnoreCase) ||
                    book.Status.Contains(searchQuery, StringComparison.OrdinalIgnoreCase));
            }

            return View(await booksQuery.ToListAsync());
        }

        /*
        // GET: Book2
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Book.Include(b => b.Publisher).Include(b => b.Reservation);
            return View(await applicationDbContext.ToListAsync());
        }
        */

        // GET: Book/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.Book
    .Include(b => b.Publisher)
    .Include(b => b.Reservation)
    .FirstOrDefaultAsync(m => m.BookId != id);

            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        // GET: Book/Create
        public IActionResult Create()
        {
            ViewData["PublisherFk"] = new SelectList(_context.Publisher, "PublisherId", "PublisherId");
            ViewData["ReservationFk"] = new SelectList(_context.Reservation, "ReservationId", "ReservationId");
            return View();
        }

        // POST: Book/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BookId,Author,Title,Genre,Price,Status,PublisherFk,ReservationFk")] Book book)
        {
            if (ModelState.IsValid)
            {
                _context.Add(book);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PublisherFk"] = new SelectList(_context.Publisher, "PublisherId", "PublisherId", book.PublisherFk);
            ViewData["ReservationFk"] = new SelectList(_context.Reservation, "ReservationId", "ReservationId", book.ReservationFk);
            return View(book);
        }

        // GET: Book/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.Book.FindAsync(id);
            if (book == null)
            {
                return NotFound();
            }
            ViewData["PublisherFk"] = new SelectList(_context.Publisher, "PublisherId", "PublisherId", book.PublisherFk);
            ViewData["ReservationFk"] = new SelectList(_context.Reservation, "ReservationId", "ReservationId", book.ReservationFk);
            return View(book);
        }

        // POST: Book/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BookId,Author,Title,Genre,Price,Status,PublisherFk,ReservationFk")] Book book)
        {
            if (id != book.BookId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(book);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookExists(book.BookId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["PublisherFk"] = new SelectList(_context.Publisher, "PublisherId", "PublisherId", book.PublisherFk);
            ViewData["ReservationFk"] = new SelectList(_context.Reservation, "ReservationId", "ReservationId", book.ReservationFk);
            return View(book);
        }

        // GET: Book/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.Book
                .Include(b => b.Publisher)
                .Include(b => b.Reservation)
                .FirstOrDefaultAsync(m => m.BookId == id);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        // POST: Book/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var book = await _context.Book.FindAsync(id);
            _context.Book.Remove(book);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BookExists(int id)
        {
            return _context.Book.Any(e => e.BookId == id);
        }


        // GET: Reservation/MostReservedBookTitle
        // Displays the title of the book that has been most reserved in the reservation list
        // within a specified time frame.
        [HttpGet]
        [Route("MostReservedBook")]
        public async Task<MostReservedBookResult> MostReservedBookTitle(DateTime startDate, DateTime endDate)
        {
            var mostReservedBook = await _context.Reservation
                .Where(r => r.IssuedDate >= startDate && r.IssuedDate <= endDate && r.Status == "Book")
                .GroupBy(r => r.LibraryMemberFk)
                .Select(g => new
                {
                    LibraryMemberId = g.Key,
                    ReservationCount = g.Count()
                })
                .OrderByDescending(g => g.ReservationCount)
                .FirstOrDefaultAsync();

            var result = new MostReservedBookResult();

            if (mostReservedBook != null)
            {
                var bookTitle = await _context.Book
                    .Where(b => b.Reservation.LibraryMemberFk == mostReservedBook.LibraryMemberId)
                    .Select(b => b.Title)
                    .FirstOrDefaultAsync();

                result.MostReservedBookTitle = bookTitle;
                result.ReservationCount = mostReservedBook.ReservationCount;
            }
            else
            {
                result.MostReservedBookTitle = "No reservations";
                result.ReservationCount = 0;
            }


            return result;
        }
    }
}