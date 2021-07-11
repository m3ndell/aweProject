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
    public class RessourcesController : Controller
    {
        private readonly AppDbContext _context;

        public RessourcesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Ressources
        [Authorize(Roles = "Lagerist, Administrator")]
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
        public async Task<IActionResult> PostInsert([Bind("Id,Name,Type,BuyDate,NextMaintenance,Standort, SiteId, OrderLog, IsInStorage")] Ressources ressources)
        {
            if (!ModelState.IsValid) return PartialView("CreatePartial", ressources);

            ressources.Id = Guid.NewGuid();
            ressources.SiteId = Guid.Empty;
            ressources.OrderLog = DateTime.Now.ToString() + ", ";
            ressources.Standort = "Lager";
            //ressources.OrderLog = "10/05/2021 14:11:31 ,14/05/2021 13:10:10, 29/06/2021 19:11:53, 30/06/2021 19:11:53, 07/07/2021 19:11:53";
            ressources.IsInStorage = true;
            _context.Add(ressources);
            await _context.SaveChangesAsync();
            return PartialView("IndexNewPartial", ressources);

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
        public async Task<IActionResult> PostUpdate([Bind("Id,Name,Type,BuyDate,NextMaintenance,Standort, SiteId, OrderLog, IsInStorage")] Ressources ressources)
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
            var orderList = await _context.Order.ToListAsync();
            foreach (Order order in orderList)
            {
                if (order.RessourceId == id)
                {
                    _context.Order.Remove(order);
                    await _context.SaveChangesAsync();
                }
            }

            var retourenList = await _context.Retouren.ToListAsync();
            foreach (Retouren retoure in retourenList)
            {
                if (retoure.RessourceId == id)
                {
                    _context.Retouren.Remove(retoure);
                    await _context.SaveChangesAsync();
                }
            }

            var session = await _context.Ressources.FindAsync(id);
            _context.Ressources.Remove(session);
            await _context.SaveChangesAsync();
            return new EmptyResult();
        }

        private bool RessourceExists(Guid id)
        {
            return _context.Ressources.Any(e => e.Id == id);
        }

        // GET: GetDetailsPartial
        public async Task<IActionResult> GetDetails(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //string dates = "29/06/2021 19:11:53, 30/06/2021 19:11:53, 07/07/2021 19:11:53, 14/07/2021 16:52:13, 18/07/2021 14:01:33, ";
            var session = await _context.Ressources.FindAsync(id);
            //session.OrderLog = dates;
            if (session == null)
            {
                return NotFound();
            }

            ViewBag.Occupancy = CalculateTime(session.OrderLog);
            return PartialView("DetailsPartial", session);
        }

        public static double CalculateTime(String OrderLog)
        {
            string[] dateString = OrderLog.Split(',');          //split String into array
            List<DateTime> dates = new List<DateTime>();

            foreach (var date in dateString)
            {                   //convert String to DateTime List
                if (date.Equals(" ") | date.Equals(""))
                {
                    break;
                }
                else
                {
                    dates.Add(Convert.ToDateTime(date));
                }
            }

            double TotalTimeStorage = 0;
            double TotalTimeAway = 0;

            DateTime LastDate = DateTime.Now;
            int i = 0;
            TimeSpan ts = TimeSpan.Zero;

            foreach (var date in dates)
            {                        //count time between dates and calculate TotalTimeAway and TotalTimeStorage
                if (i == 0)
                {
                    LastDate = date;
                }
                else if (i % 2 == 0)
                {
                    ts = date - LastDate;
                    TotalTimeAway += ts.TotalSeconds;
                    LastDate = date;

                }
                else
                {
                    ts = date - LastDate;
                    TotalTimeStorage += ts.TotalSeconds;
                    LastDate = date;

                }
                i++;
            }
            return Math.Round(((Double)TotalTimeAway / (TotalTimeAway + TotalTimeStorage)) * 100, 2);  // Rückgabe Auslastung in % auf zwei Nachkommastellen gerundet
        }
    }
}
