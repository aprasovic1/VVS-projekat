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
    public class ReservationPaymentController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ReservationPaymentController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ReservationPayment
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.ReservationPayment.Include(r => r.Reservation);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: ReservationPayment/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reservationPayment = await _context.ReservationPayment
                .Include(r => r.Reservation)
                .FirstOrDefaultAsync(m => m.PaymentId == id);
            if (reservationPayment == null)
            {
                return NotFound();
            }

            return View(reservationPayment);
        }

        // GET: ReservationPayment/Create
        public IActionResult Create()
        {
            ViewData["PaymentId"] = new SelectList(_context.Reservation, "ReservationId", "ReservationId");
            return View();
        }

        // POST: ReservationPayment/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PaymentId,PaymentDate,Amount")] ReservationPayment reservationPayment)
        {
            if (ModelState.IsValid)
            {
                _context.Add(reservationPayment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PaymentId"] = new SelectList(_context.Reservation, "ReservationId", "ReservationId", reservationPayment.PaymentId);
            return View(reservationPayment);
        }

        // GET: ReservationPayment/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reservationPayment = await _context.ReservationPayment.FindAsync(id);
            if (reservationPayment == null)
            {
                return NotFound();
            }
            ViewData["PaymentId"] = new SelectList(_context.Reservation, "ReservationId", "ReservationId", reservationPayment.PaymentId);
            return View(reservationPayment);
        }

        // POST: ReservationPayment/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PaymentId,PaymentDate,Amount")] ReservationPayment reservationPayment)
        {
            if (id != reservationPayment.PaymentId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(reservationPayment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReservationPaymentExists(reservationPayment.PaymentId))
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
            ViewData["PaymentId"] = new SelectList(_context.Reservation, "ReservationId", "ReservationId", reservationPayment.PaymentId);
            return View(reservationPayment);
        }

        // GET: ReservationPayment/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reservationPayment = await _context.ReservationPayment
                .Include(r => r.Reservation)
                .FirstOrDefaultAsync(m => m.PaymentId == id);
            if (reservationPayment == null)
            {
                return NotFound();
            }

            return View(reservationPayment);
        }

        // POST: ReservationPayment/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var reservationPayment = await _context.ReservationPayment.FindAsync(id);
            _context.ReservationPayment.Remove(reservationPayment);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReservationPaymentExists(int id)
        {
            return _context.ReservationPayment.Any(e => e.PaymentId == id);
        }
    }
}
