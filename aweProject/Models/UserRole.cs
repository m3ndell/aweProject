using aweProject.Areas.Identity.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace aweProject.Models
{
    public class UserRole
    {
        public AppUser applicationUser { get; set; }
        public List<SelectListItem> ApplicationRoles { get; set; }
    }
}
