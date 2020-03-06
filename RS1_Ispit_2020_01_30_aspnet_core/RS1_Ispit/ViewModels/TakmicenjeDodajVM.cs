﻿using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RS1_Ispit_asp.net_core.ViewModels
{
    public class TakmicenjeDodajVM
    {
        public int TakmicenjeID { get; set; }
        public string skola { get; set; }
        public int skolaID { get; set; }
        public List<SelectListItem> predmeti { get; set; }
        public int predmetID { get; set; }
        public List<SelectListItem> razredi { get; set; }
        public int razred { get; set; }
        [DataType(DataType.Date)]
public DateTime datum { get; set; }

    }
}
