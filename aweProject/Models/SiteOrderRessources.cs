using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace aweProject.Models {
    public class SiteOrderRessources {

        public List<Ressources> RessourcesList { get; set; }
        public List<Order> OrderList { get; set; }
        public List<SiteManagement> SiteList { get; set; }
        public List<Retouren> RetourenList { get; set; }

        public SiteOrderRessources(List<Ressources> Ressources, List<Order> Orders, List<SiteManagement> SiteList, List<Retouren> RetourenList) {
            this.RessourcesList = Ressources;
            this.OrderList = Orders;
            this.SiteList = SiteList;
            this.RetourenList = RetourenList;
        }
    }
}
