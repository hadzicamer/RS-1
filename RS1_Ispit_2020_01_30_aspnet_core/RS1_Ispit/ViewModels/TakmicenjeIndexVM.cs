﻿using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RS1_Ispit_asp.net_core.ViewModels
{
    public class TakmicenjeIndexVM
    {
        public List<SelectListItem> skole { get; set; }
        public int skolaID { get; set; }
        public List<SelectListItem> razredi { get; set; }
        public int razred { get; set; }

    }
}