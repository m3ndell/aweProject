﻿using System;
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
        public Ressources Ressource { get; set; }
        public DateTime CheckOutTime { get; set; }
        public Boolean IsActive { get; set; }
    }
}