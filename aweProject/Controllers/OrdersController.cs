using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using aweProject.Models;
using Microsoft.AspNetCore.Authorization;

namespace aweProject.Controllers
{
    public class OrdersController : Controller
    {
        private readonly AppDbContext _context;

        public OrdersController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Orders
        [Authorize(Roles = "Lagerist, Administrator")]
        public async Task<IActionResult> Index()
        {
            return View(new SiteOrderRessources(await _context.Ressources.ToListAsync(), await _context.Order.ToListAsync(), await _context.SiteManagement.ToListAsync(), await _context.Retouren.ToListAsync()));
        }

        private bool OrderExists(Guid id)
        {
            return _context.Order.Any(e => e.OrderId == id);
        }

        public async Task<IActionResult> Checkout(Guid RessourceId, Guid SiteId, Guid OrderId) {
            Ressources ressource = await _context.Ressources.FindAsync(RessourceId);
            Order order = await _context.Order.FindAsync(OrderId);
            SiteManagement site = await _context.SiteManagement.FindAsync(SiteId);

            ressource.SiteId = SiteId;
            ressource.IsInStorage = false;
            ressource.OrderLog = ressource.OrderLog + DateTime.Now.ToString() + ", ";
            ressource.Standort = site.Name;
            order.IsActive = true;
            order.IsClosed = false;
            order.CheckOutTime = DateTime.Now;

            await _context.SaveChangesAsync();

            return PartialView("OrderPartial", new SiteOrderRessources(await _context.Ressources.ToListAsync(), await _context.Order.ToListAsync(), await _context.SiteManagement.ToListAsync(), await _context.Retouren.ToListAsync()));
        }
    }
}
