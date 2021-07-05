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
            return View(new SiteRetourenRessources(await _context.Ressources.ToListAsync(), await _context.Retouren.ToListAsync(), await _context.SiteManagement.ToListAsync(), await _context.Order.ToListAsync()));
        }

        private bool RetourenExists(Guid id)
        {
            return _context.Retouren.Any(e => e.RetourenId == id);
        }

        // TODO: Order löschen
        public async Task<IActionResult> CheckIn(Guid RessourceId, Guid SiteId, Guid RetourenId, Guid OrderId) {
            Ressources ressource = await _context.Ressources.FindAsync(RessourceId);
            Retouren retouren = await _context.Retouren.FindAsync(RetourenId);
            Order order = await _context.Order.FindAsync(OrderId);

            order.IsClosed = true;
            order.IsActive = false;
            ressource.SiteId = SiteId;
            ressource.IsInStorage = true;
            ressource.OrderLog = ressource.OrderLog + DateTime.Now.ToString() + ", ";
            ressource.Standort = "Lager";
            retouren.IsActive = true;
            retouren.CheckInTime = DateTime.Now;
            
            await _context.SaveChangesAsync();

            return PartialView("RetourenPartial", new SiteRetourenRessources(await _context.Ressources.ToListAsync(), await _context.Retouren.ToListAsync(), await _context.SiteManagement.ToListAsync(), await _context.Order.ToListAsync()));
        }
    }
}
