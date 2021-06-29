﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace aweProject.Models
{
    public class Ressources
    {
        public Guid Id { get; set; }

        [Required]
        [MinLength(3)]
        public string Name { get; set; }

        [Required]
        [MinLength(3)]
        public string Type { get; set; }

        [Required]
        [DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime BuyDate { get; set; }

        
        [DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime NextMaintenance { get; set; }

        //[ReadOnly(true)]
        //[DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy}", ApplyFormatInEditMode = true)]
        //public DateTime LastLend { get; set; }
        //[ReadOnly(true)]
        //[DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy}", ApplyFormatInEditMode = true)]
        //public DateTime LastStored { get; set; }

        [ReadOnly(true)]
        public string Standort { get; set; }

        public Guid SiteId { get; set; }

        public string OrderLog { get; set; }  
    }
}
