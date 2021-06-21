using System;
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
    public class RessourcesController : Controller
    {
        private readonly AppDbContext _context;

        public RessourcesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Ressources
        public async Task<IActionResult> Index()
        {
            return View(await _context.Ressources.ToListAsync());
        }

        // GET: Ressources/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ressources = await _context.Ressources
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ressources == null)
            {
                return NotFound();
            }

            return View(ressources);
        }

        // GET: Ressources/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Ressources/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Type,BuyDate,NextMaintenance,Standort")] Ressources ressources)
        {
            if (ModelState.IsValid)
            {
                ressources.Id = Guid.NewGuid();
                _context.Add(ressources);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(ressources);
        }

        // GET: Ressources/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ressources = await _context.Ressources.FindAsync(id);
            if (ressources == null)
            {
                return NotFound();
            }
            return View(ressources);
        }

        // POST: Ressources/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Name,Type,BuyDate,NextMaintenance,Standort")] Ressources ressources)
        {
            if (id != ressources.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ressources);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RessourcesExists(ressources.Id))
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
            return View(ressources);
        }

        // GET: Ressources/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ressources = await _context.Ressources
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ressources == null)
            {
                return NotFound();
            }

            return View(ressources);
        }

        // POST: Ressources/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var ressources = await _context.Ressources.FindAsync(id);
            _context.Ressources.Remove(ressources);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RessourcesExists(Guid id)
        {
            return _context.Ressources.Any(e => e.Id == id);
        }
    }
}
