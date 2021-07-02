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
        public List<Order> OrderList { get; set; }
        public List<Retouren> RetourenList { get; set; }

        public SiteRessource(Guid Id, List<Ressources> Ressources, List<Order> Orders, List<Retouren> RetourenList) 
        {
            this.Id = Id;
            this.RessourcesList = Ressources;
            this.OrderList = Orders;
            this.RetourenList = RetourenList;
        }
    }
}
