using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace aweProject.Models
{
    public class Retouren
    {
        public Guid RetourenId { get; set; }

        public DateTime RetourenTime { get; set; }
        public Guid RessourceId { get; set; }
        public Guid SiteId { get; set; }
        public DateTime CheckInTime { get; set; }
        public bool IsActive { get; set; }

        public Retouren(Guid retourenId, DateTime retourenTime, Guid ressourceId, Guid siteId, DateTime checkInTime, bool isActive)
        {
            RetourenId = retourenId;
            RetourenTime = retourenTime;
            RessourceId = ressourceId;
            SiteId = siteId;
            CheckInTime = checkInTime;
            IsActive = isActive;
        }
    }
}
