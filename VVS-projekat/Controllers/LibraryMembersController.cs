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
    public class LibraryMembersController : Controller
    {
        private readonly ApplicationDbContext _context;


        public LibraryMembersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: LibraryMembers
        public async Task<IActionResult> Index()
        {

            return View(await _context.LibraryMember.ToListAsync());
        }

        // GET: LibraryMembers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var libraryMember = await _context.LibraryMember
                .FirstOrDefaultAsync(m => m.LibraryMemberId == id);
            if (libraryMember == null)
            {
                return NotFound();
            }
            id = libraryMember.LibraryMemberId;
            return View(libraryMember);
        }

        // GET: LibraryMembers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: LibraryMembers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("LibraryMemberId,FirstName,LastName,EmailAddress,LibraryUsername,LibraryUserPassword,MembershipExpirationDate")] LibraryMember libraryMember)
        {
            if (ModelState.IsValid)
            {
                _context.Add(libraryMember);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(libraryMember);
        }

        // GET: LibraryMembers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var libraryMember = await _context.LibraryMember.FindAsync(id);
            if (libraryMember == null)
            {
                return NotFound();
            }
            return View(libraryMember);
        }

        // POST: LibraryMembers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("LibraryMemberId,FirstName,LastName,EmailAddress,LibraryUsername,LibraryUserPassword,MembershipExpirationDate")] LibraryMember libraryMember)
        {
            if (id != libraryMember.LibraryMemberId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(libraryMember);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LibraryMemberExists(libraryMember.LibraryMemberId))
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
            return View(libraryMember);
        }

        // GET: LibraryMembers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var libraryMember = await _context.LibraryMember
                .FirstOrDefaultAsync(m => m.LibraryMemberId == id);
            if (libraryMember == null)
            {
                return NotFound();
            }

            return View(libraryMember);
        }

        // POST: LibraryMembers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var libraryMember = await _context.LibraryMember.FindAsync(id);
            _context.LibraryMember.Remove(libraryMember);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LibraryMemberExists(int id)
        {
            return _context.LibraryMember.Any(e => e.LibraryMemberId == id);
        }
    }
}
