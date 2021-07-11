using aweProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace aweProject.Controllers
{
    public class UserManagementController : Microsoft.AspNetCore.Mvc.Controller
    {

        private  AppDbContext _context;


        public UserManagementController(AppDbContext context)
        {
            _context = context;
        }

        [Microsoft.AspNetCore.Authorization.Authorize(Roles ="Administrator")]
        public async Task<IActionResult> Index()
        {
            ViewBag.Roles = await _context.Roles.ToListAsync();
            return View(_context.Users.ToList());
        }

        [Microsoft.AspNetCore.Mvc.HttpGet]
        public async Task<IActionResult> Edit(string Id)
        {
            Console.WriteLine("Test123" + DateTime.Now.ToString());
            Console.WriteLine(Id);
            UserRole userRole = new UserRole();
            var users = _context.Users.Where(x => x.Id == Id).SingleOrDefault();
            var userInRole = _context.UserRoles.Where(x => x.UserId == Id).Select(x => x.RoleId).ToList();
            userRole.applicationUser = users;

            userRole.ApplicationRoles = await _context.Roles.Select(x => new SelectListItem()
            {
                Text = x.Name,
                Value = x.Id,
                Selected = userInRole.Contains(x.Id)
            }).ToListAsync();
            Console.WriteLine("Test1234");
            return View(userRole);
        }

        [Microsoft.AspNetCore.Mvc.HttpPost]
        public async Task<IActionResult> Edit (UserRole model)
        {
            var applicationUser = await _context.Users.FindAsync(model.applicationUser.Id);
            var selectedRoleId = model.ApplicationRoles.Where(x => x.Selected).Select(x => x.Value);
            var AlreadyExists = _context.UserRoles.Where(x => x.UserId == model.applicationUser.Id).Select(x => x.RoleId).ToList();
            var toAdd = selectedRoleId.Except(AlreadyExists);
            var toRemove = AlreadyExists.Except(selectedRoleId);
            foreach (var item in toRemove)
            {
                _context.UserRoles.Remove(new Microsoft.AspNetCore.Identity.IdentityUserRole<string>
                {
                    RoleId = item,
                    UserId = model.applicationUser.Id
                });
            }
            foreach (var item in toAdd)
            {
                _context.UserRoles.Add(new Microsoft.AspNetCore.Identity.IdentityUserRole<string>
                {
                    RoleId = item,
                    UserId = model.applicationUser.Id
                });
            }
            _context.SaveChanges();
            return RedirectToAction("Index");

        }
    }
}

