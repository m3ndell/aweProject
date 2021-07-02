using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace aweProject.Models {
    public class SiteRetourenRessources {

        public List<Ressources> RessourcesList { get; set; }
        public List<Retouren> RetourenList { get; set; }
        public List<SiteManagement> SiteList { get; set; }

        public SiteRetourenRessources(List<Ressources> ressourcesList, List<Retouren> retourenList, List<SiteManagement> siteList)
        {
            RessourcesList = ressourcesList;
            RetourenList = retourenList;
            SiteList = siteList;
        }
    }
}
