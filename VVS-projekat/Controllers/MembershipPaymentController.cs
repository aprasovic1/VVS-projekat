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
    public class MembershipPaymentController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MembershipPaymentController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: MembershipPayment
        public async Task<IActionResult> Index()
        {
            return View(await _context.MembershipPayment.ToListAsync());
        }

        // GET: MembershipPayment/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var membershipPayment = await _context.MembershipPayment
                .FirstOrDefaultAsync(m => m.MembershipPaymentId == id);
            if (membershipPayment == null)
            {
                return NotFound();
            }

            return View(membershipPayment);
        }

        // GET: MembershipPayment/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: MembershipPayment/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MembershipPaymentId,Amount,Discount,CardNumber,LibraryMemberFk")] MembershipPayment membershipPayment)
        {
            if (ModelState.IsValid)
            {
                var card = _context.Card.Where(c => c.CardNumber == membershipPayment.CardNumber).FirstOrDefault();
                if (!CardisValid(membershipPayment.CardNumber) || !VerifyCardAmount(card, membershipPayment.Amount))
                {
                    return RedirectToAction(nameof(Create));
                }
                card.CardAmount -= (int)membershipPayment.Amount;
                membershipPayment.PaymentDate = DateTime.Now;
                _context.Add(membershipPayment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["LibraryMemberFk"] = new SelectList(_context.LibraryMember, "LibraryMemberId", "LibraryMemberId", membershipPayment.LibraryMemberFk);
            return View(membershipPayment);
        }

        // GET: MembershipPayment/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var membershipPayment = await _context.MembershipPayment.FindAsync(id);
            if (membershipPayment == null)
            {
                return NotFound();
            }
            return View(membershipPayment);
        }

        // POST: MembershipPayment/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MembershipPaymentId,PaymentDate,Amount,Discount,LibraryMemberFk")] MembershipPayment membershipPayment)
        {
            if (id != membershipPayment.MembershipPaymentId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(membershipPayment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MembershipPaymentExists(membershipPayment.MembershipPaymentId))
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
            return View(membershipPayment);
        }

        // GET: MembershipPayment/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var membershipPayment = await _context.MembershipPayment
                .FirstOrDefaultAsync(m => m.MembershipPaymentId == id);
            if (membershipPayment == null)
            {
                return NotFound();
            }

            return View(membershipPayment);
        }

        // POST: MembershipPayment/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var membershipPayment = await _context.MembershipPayment.FindAsync(id);
            _context.MembershipPayment.Remove(membershipPayment);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MembershipPaymentExists(int id)
        {
            return _context.MembershipPayment.Any(e => e.MembershipPaymentId == id);
        }
        public static bool CardisValid(string number)
        {
            long cache;
            bool check = long.TryParse(number, out cache);
            if (!check) return false;
            int checksum = int.Parse(number[number.Length - 1].ToString());
            int total = 0;

            for (int i = number.Length - 2; i >= 0; i--)
            {
                int sum = 0;
                int digit;
                check = int.TryParse((number[i].ToString()), out digit);
                if (!check) return false;
                if (i % 2 == 0)
                {
                    digit *= 2;
                }

                sum = digit / 10 + digit % 10;
                total += sum;
            }

            return (10 - total % 10) == checksum;
        }

        public static bool VerifyCardAmount(Card card, decimal amount)
        {
            if (card == null || card.CardAmount < amount)
            {
                return false;
            }
            return true;
        }
    }
    }

