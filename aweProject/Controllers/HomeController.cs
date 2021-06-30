using aweProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace aweProject.Controllers
{
    public class HomeController : Controller
    {

        private readonly AppDbContext _context;


        public HomeController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


        public async Task<IActionResult> Index()
        {
            return View(await _context.SiteManagement.ToListAsync());
        }

        public async Task<IActionResult> GetCreate(Guid id)
        {
            return PartialView("SiteFormPartial", new SiteRessource(id, await _context.Ressources.ToListAsync()));
        }

        public async Task<IActionResult> Checkout(Guid RessourceId, Guid SiteId)
        {
            var session = await _context.Ressources.FindAsync(RessourceId);
            session.SiteId = SiteId;
            session.IsInStorage = false;
            session.OrderLog = session.OrderLog + DateTime.Now.ToString() + ", ";
            await _context.SaveChangesAsync();

            return PartialView("SiteFormPartial", new SiteRessource(SiteId, await _context.Ressources.ToListAsync()));
        }
    }
}

