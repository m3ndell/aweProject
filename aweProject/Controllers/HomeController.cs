using aweProject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
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
            if (User.Identity.IsAuthenticated)
            {
                ClaimsPrincipal currentUser = this.User;
                var currentUserID = currentUser.FindFirst(ClaimTypes.NameIdentifier).Value;
                Guid IdToString = Guid.Parse(currentUserID);
                ViewBag.UserId = IdToString;
                var userName = "Gast";
                if (_context.Users.Find(currentUserID) != null)
                {
                    userName = _context.Users.Find(currentUserID).Name;
                }
                
                ViewBag.UserName = userName;

                var users = _context.Users.Where(x => x.Id == currentUserID).SingleOrDefault();
                var userInRole = _context.UserRoles.Where(x => x.UserId == currentUserID).Select(x => x.RoleId).ToList();
                var RoleName = "Gast";
                if (userInRole.Count()!=0)
                {
                    var Role = _context.Roles.Find(userInRole.First().ToString());
                    RoleName = Role.Name;
                }
                ViewBag.UserRole = RoleName;
            }
            else
            {
                ViewBag.UserId = Guid.Empty;
            }
            return View(await _context.SiteManagement.ToListAsync());
        }

        public async Task<IActionResult> GetCreate(Guid id)
        {
            Console.WriteLine(id);
            return PartialView("SiteFormPartial", new SiteRessource(id, await _context.Ressources.ToListAsync(), await _context.Order.ToListAsync(), await _context.Retouren.ToListAsync()));
        }

        public async Task<IActionResult> RequestRessource(Guid RessourceId, Guid SiteId)
        {
            Order order = new Order(Guid.NewGuid(), DateTime.Now, RessourceId, SiteId, DateTime.Now, false, false);
            _context.Add(order);
            await _context.SaveChangesAsync();

            return await GetCreate(SiteId);
        }

        public async Task<IActionResult> RequestRetoure(Guid RessourceId, Guid SiteId)
        {
            Retouren retouren = new Retouren(Guid.NewGuid(), DateTime.Now, RessourceId, SiteId, DateTime.Now, false);
            _context.Add(retouren);
            await _context.SaveChangesAsync();

            return await GetCreate(SiteId);
        }
    }
}

