using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using VVS_projekat.Data;
using VVS_projekat.Models;

namespace VVS_projekat.Controllers
{
    public class VoucherController : Controller
    {
        private readonly ApplicationDbContext _context;

        public VoucherController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Voucher
        public async Task<IActionResult> Index()
        {
            return View(await _context.Voucher.ToListAsync());
        }

        // GET: Voucher/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var voucher = await _context.Voucher
                .FirstOrDefaultAsync(m => m.VoucherID == id);
            if (voucher == null)
            {
                return NotFound();
            }

            return View(voucher);
        }

        // GET: Voucher/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Voucher/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("VoucherID,VoucherAmount,VoucherDiscount")] Voucher voucher)
        {
            if (ModelState.IsValid)
            {
                voucher.VoucherDate = DateTime.Now;
                voucher.VoucherCode = GenerateVoucherCode();
                _context.Add(voucher);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(voucher);
        }

        // GET: Voucher/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var voucher = await _context.Voucher.FindAsync(id);
            if (voucher == null)
            {
                return NotFound();
            }
            return View(voucher);
        }

        // POST: Voucher/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("VoucherID,VoucherDate,VoucherCode,VoucherAmount,VoucherDiscount")] Voucher voucher)
        {
            if (id != voucher.VoucherID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(voucher);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VoucherExists(voucher.VoucherID))
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
            return View(voucher);
        }

        // GET: Voucher/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var voucher = await _context.Voucher
                .FirstOrDefaultAsync(m => m.VoucherID == id);
            if (voucher == null)
            {
                return NotFound();
            }

            return View(voucher);
        }

        // POST: Voucher/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var voucher = await _context.Voucher.FindAsync(id);
            _context.Voucher.Remove(voucher);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VoucherExists(int id)
        {
            return _context.Voucher.Any(e => e.VoucherID == id);
        }
        private static string GenerateVoucherCode()
        {
            Random Random = new Random();
            const string characters = "0123456789";
            StringBuilder voucherCode = new StringBuilder();
            int randomIndex = Random.Next(characters.Length);
            char nextChar = characters[randomIndex];
            voucherCode.Append(nextChar);
            for (int i = 0; i < 11; i++)
            {
                randomIndex = Random.Next(characters.Length);
                nextChar = characters[randomIndex];
                if (i % 4 == 0)
                {
                    int sum = 0;
                    foreach (char j in voucherCode.ToString())
                    {
                        sum += (int)Char.GetNumericValue(j);
                    }
                    voucherCode.Append((sum % 10).ToString());
                    i++;
                }
                voucherCode.Append(nextChar);
            }

            return voucherCode.ToString();
        }

    }
}
