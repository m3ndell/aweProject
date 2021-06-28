using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using aweProject.Models;

namespace aweProject.Controllers
{
    public class RetourensController : Controller
    {
        private readonly AppDbContext _context;

        public RetourensController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Retourens
        public async Task<IActionResult> Index()
        {
            return View(await _context.Retouren.ToListAsync());
        }

        // GET: Retourens/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var retouren = await _context.Retouren
                .FirstOrDefaultAsync(m => m.Id == id);
            if (retouren == null)
            {
                return NotFound();
            }

            return View(retouren);
        }

        // GET: Retourens/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Retourens/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,DeliveryTime")] Retouren retouren)
        {
            if (ModelState.IsValid)
            {
                retouren.Id = Guid.NewGuid();
                _context.Add(retouren);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(retouren);
        }

        // GET: Retourens/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var retouren = await _context.Retouren.FindAsync(id);
            if (retouren == null)
            {
                return NotFound();
            }
            return View(retouren);
        }

        // POST: Retourens/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,DeliveryTime")] Retouren retouren)
        {
            if (id != retouren.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(retouren);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RetourenExists(retouren.Id))
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
            return View(retouren);
        }

        // GET: Retourens/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var retouren = await _context.Retouren
                .FirstOrDefaultAsync(m => m.Id == id);
            if (retouren == null)
            {
                return NotFound();
            }

            return View(retouren);
        }

        // POST: Retourens/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var retouren = await _context.Retouren.FindAsync(id);
            _context.Retouren.Remove(retouren);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RetourenExists(Guid id)
        {
            return _context.Retouren.Any(e => e.Id == id);
        }
    }
}
