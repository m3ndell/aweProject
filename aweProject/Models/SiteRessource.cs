using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace aweProject.Models
{
    public class SiteRessource
    {
        public Guid Id { get; set; }

        public List<Ressources> RessourcesList { get; set; }

        public SiteRessource(Guid Id, List<Ressources> Ressources) 
        {
            this.Id = Id;
            this.RessourcesList = Ressources;
        }
    }
}
