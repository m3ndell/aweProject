using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace aweProject.Models
{
    public class Order
    {
        public Guid OrderId { get; set; }

        public DateTime OrderTime { get; set; }
        public Guid RessourceId { get; set; }
        public Guid SiteId { get; set; }
        public DateTime CheckOutTime { get; set; }
        public bool IsActive { get; set; }
        public bool IsClosed { get; set; }

        public Order(Guid orderId, DateTime orderTime, Guid ressourceId, Guid siteId, DateTime checkOutTime, bool isActive, bool IsClosed)
        {
            this.OrderId = orderId;
            this.OrderTime = orderTime;
            this.RessourceId = ressourceId;
            this.SiteId = siteId;
            this.CheckOutTime = checkOutTime;
            this.IsActive = isActive;
            this.IsClosed = IsClosed;
        }
    }
}
