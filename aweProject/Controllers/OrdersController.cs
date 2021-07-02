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
    public class OrdersController : Controller
    {
        private readonly AppDbContext _context;

        public OrdersController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Orders
        public async Task<IActionResult> Index()
        {
            return View(new SiteOrderRessources(await _context.Ressources.ToListAsync(), await _context.Order.ToListAsync(), await _context.SiteManagement.ToListAsync(), await _context.Retouren.ToListAsync()));
        }

        private bool OrderExists(Guid id)
        {
            return _context.Order.Any(e => e.OrderId == id);
        }

        public async Task<IActionResult> Checkout(Guid RessourceId, Guid SiteId, Guid OrderId) {
            Ressources session = await _context.Ressources.FindAsync(RessourceId);
            Order order = await _context.Order.FindAsync(OrderId);

            session.SiteId = SiteId;
            session.IsInStorage = false;
            session.OrderLog = session.OrderLog + DateTime.Now.ToString() + ", ";
            order.IsActive = true;
            order.IsClosed = false;
            order.CheckOutTime = DateTime.Now;

            await _context.SaveChangesAsync();

            return PartialView("OrderPartial", new SiteOrderRessources(await _context.Ressources.ToListAsync(), await _context.Order.ToListAsync(), await _context.SiteManagement.ToListAsync(), await _context.Retouren.ToListAsync()));
        }
    }
}
