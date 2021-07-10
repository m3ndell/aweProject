using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using aweProject.Models;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace aweProject.Controllers
{
    public class SiteManagementsController : Controller
    {
        private readonly AppDbContext _context;

        public SiteManagementsController(AppDbContext context)
        { 
            _context = context;
        }

        // GET: SiteManagements
        [Authorize(Roles = "Baustellenleiter, Administrator")]
        public async Task<IActionResult> Index()
        {
            ViewBag.UserList = _context.Users.ToList();
            return View(await _context.SiteManagement.ToListAsync());
        }

        // GET: SiteManagements/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var siteManagement = await _context.SiteManagement
                .FirstOrDefaultAsync(m => m.Id == id);
            if (siteManagement == null)
            {
                return NotFound();
            }

            return View(siteManagement);
        }

        // GET: SiteManagements/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: SiteManagements/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description,ManagerId")] SiteManagement siteManagement)
        {
            if (ModelState.IsValid)
            {
                ClaimsPrincipal currentUser = this.User;
                var currentUserID = currentUser.FindFirst(ClaimTypes.NameIdentifier).Value;
                Guid IdToString =  Guid.Parse(currentUserID);

                siteManagement.Id = Guid.NewGuid();
                siteManagement.ManagerId = IdToString;
                _context.Add(siteManagement);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(siteManagement);
        }

        // GET: SiteManagements/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var siteManagement = await _context.SiteManagement.FindAsync(id);
            if (siteManagement == null)
            {
                return NotFound();
            }
            return View(siteManagement);
        }

        // POST: SiteManagements/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Name,Description,ManagerId")] SiteManagement siteManagement)
        {
            if (id != siteManagement.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(siteManagement);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SiteManagementExists(siteManagement.Id))
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
            return View(siteManagement);
        }

        // GET: SiteManagements/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var siteManagement = await _context.SiteManagement
                .FirstOrDefaultAsync(m => m.Id == id);
            if (siteManagement == null)
            {
                return NotFound();
            }

            return View(siteManagement);
        }

        // POST: SiteManagements/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var siteManagement = await _context.SiteManagement.FindAsync(id);
            _context.SiteManagement.Remove(siteManagement);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SiteManagementExists(Guid id)
        {
            return _context.SiteManagement.Any(e => e.Id == id);
        }
    }
}
