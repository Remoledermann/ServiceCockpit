﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServiceCockpit.Models
{
    public class ModelViewServiceCockpit
    {
        public List<Servicerapport> Servicerapports = new List<Servicerapport>();
        public Servicerapport Servicerapport = new Servicerapport();
        public List<Wochenrapport> Wochenrapports = new List<Wochenrapport>();

    }
}