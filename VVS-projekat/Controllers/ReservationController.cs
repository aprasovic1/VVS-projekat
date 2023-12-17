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
    public class ReservationController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ReservationController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Book Catalog
        public async Task<IActionResult> BookCatalog()
        {
            var books = await _context.Book.Include(b => b.Reservation).ToListAsync();
            return View(books);
        }


        private SelectList BookSelectList()
        {
            return new SelectList(_context.Book, "BookId", "Title");
        }


        // GET: Reservation
        public async Task<IActionResult> Index()
        {
            return View(await _context.Reservation.ToListAsync());
        }

        // GET: Reservation/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reservation = await _context.Reservation
                .FirstOrDefaultAsync(m => m.ReservationId == id);
            if (reservation == null)
            {
                return NotFound();
            }

            return View(reservation);
        }

        // GET: Reservation/Create
        public IActionResult Create()
        {
            ViewBag.Books = new SelectList(_context.Book.Where(b => b.ReservationFk == null), "BookId", "Title");
            return View();
        }


        // POST: Reservation/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ReservationId,IssuedDate,ReturnDate,Status,LibraryMemberFk")] Reservation reservation, int? BookId)
        {
            if (ModelState.IsValid)
            {
                if (BookId.HasValue)
                {
                    var book = await _context.Book.FindAsync(BookId.Value);
                    if (book != null && book.ReservationFk == null)
                    {
                        book.ReservationFk = reservation.ReservationId;
                        _context.Update(book);
                    }
                }

                _context.Add(reservation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Books = _context.Book.Where(b => b.ReservationFk == null).Select(b => new { b.BookId, b.Title }).ToList();
            return View(reservation);
        }
/*
        // POST: Reservation/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ReservationId,IssuedDate,ReturnDate,Status,LibraryMemberFk")] Reservation reservation)
        {
            if (ModelState.IsValid)
            {
                _context.Add(reservation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(reservation);
        }


        */

        // GET: Reservation/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reservation = await _context.Reservation.FindAsync(id);
            if (reservation == null)
            {
                return NotFound();
            }
            return View(reservation);
        }

        // POST: Reservation/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ReservationId,IssuedDate,ReturnDate,Status,LibraryMemberFk")] Reservation reservation)
        {
            if (id != reservation.ReservationId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(reservation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReservationExists(reservation.ReservationId))
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
            return View(reservation);
        }

        // GET: Reservation/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reservation = await _context.Reservation
                .FirstOrDefaultAsync(m => m.ReservationId == id);
            if (reservation == null)
            {
                return NotFound();
            }

            return View(reservation);
        }

        // POST: Reservation/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var reservation = await _context.Reservation.FindAsync(id);
            _context.Reservation.Remove(reservation);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReservationExists(int id)
        {
            return _context.Reservation.Any(e => e.ReservationId == id);
        }


        // CountReservation
        // GET: Reservation/CountReservation
        // Counts the total number of books in the library.
        [HttpGet]
        [Route("CountReservation")]
        public async Task<CountReservationResult> CountReservation()
        {
            int reservationNumber = await _context.Reservation.CountAsync();
            var result = new CountReservationResult
            {
                numberOfReservations = reservationNumber
            };
            return result;
        }

        // GET: Reservation/LateActivatedReservations
        // Displays reservations that were activated after the reservation date.
        [HttpGet]
        [Route("LateActivatedReservations")]
        public async Task<LateActivatedResult> LateActivatedReservations()
        {
            var lateActivatedReservations = _context.Reservation
                .Where(r => r.Status == "Activated" && r.IssuedDate < r.ReturnDate)
                .ToListAsync();

            var result = new LateActivatedResult
            {
                LateActivatedReservations = await lateActivatedReservations
            };

            return result;
        }
    }
}