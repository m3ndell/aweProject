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

        // GET: Create Partial View
        public ActionResult GetCreate()
        {
            return PartialView("CreatePartial");
        }

        // POST: Insert Data from Create Partial View
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PostInsert([Bind("Id,Name,Type,BuyDate,NextMaintenance,Standort, SiteId")] Ressources ressources)
        {
            if (ModelState.IsValid)
            {
                ressources.Id = Guid.NewGuid();
                ressources.SiteId = Guid.Empty;
                _context.Add(ressources);
                await _context.SaveChangesAsync();
                return PartialView("IndexNewPartial", ressources);
            }

            return PartialView("CreatePartial", ressources);
        }

        // GET: Edit Partial View
        public async Task<IActionResult> GetEdit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var session = await _context.Ressources.FindAsync(id);
            if (session == null)
            {
                return NotFound();
            }

            return PartialView("EditPartial", session);
        }

        // POST: Update Data from Create Partial View
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PostUpdate([Bind("Id,Name,Type,BuyDate,NextMaintenance,Standort")] Ressources ressources)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ressources);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RessourceExists(ressources.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return PartialView("IndexPartial", ressources);
            }
            return PartialView("EditPartial", ressources);
        }

        // GET: Get Index Partial (Edit Canceled)
        public async Task<IActionResult> GetCancelEdit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var session = await _context.Ressources.FirstOrDefaultAsync(m => m.Id == id);
            if (session == null)
            {
                return NotFound();
            }

            return PartialView("IndexPartial", session);
        }

        // GET: Get New Partial (Create Canceled)
        public ActionResult GetCancelCreate()
        {
            return PartialView("NewPartial");
        }

        // POST: Delete Data
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> PostDelete(Guid id)
        {
            var session = await _context.Ressources.FindAsync(id);
            _context.Ressources.Remove(session);
            await _context.SaveChangesAsync();
            return new EmptyResult();
        }

        private bool RessourceExists(Guid id)
        {
            return _context.Ressources.Any(e => e.Id == id);
        }

    }
}
