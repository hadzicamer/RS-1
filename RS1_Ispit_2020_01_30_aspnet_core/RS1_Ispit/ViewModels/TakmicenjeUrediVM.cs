using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RS1_Ispit_asp.net_core.ViewModels
{
    public class TakmicenjeUrediVM
    {
        public int ucesnikID { get; set; }
        public List<SelectListItem> ucesnici { get; set; }
        public string imePrezime { get; set; }
        public int bodovi { get; set; }

    }
}
