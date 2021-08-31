﻿using HMS.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HMS.Areas.Dashboard.ViewModels
{
    public class AccomodationTypeListingModel
    {
        public IEnumerable<AccomodationType> AccomodationTypes { get;  set; }
        public string SearchTerm { get;  set; }
    }

    public class AccomodationTypeActionModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}