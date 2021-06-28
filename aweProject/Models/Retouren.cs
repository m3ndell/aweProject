using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace aweProject.Models
{
    public class Retouren
    {
        public Guid Id { get; set; }

        public DateTime DeliveryTime { get; set; }

        public Ressources Ressources { get; set; }

    }
}
