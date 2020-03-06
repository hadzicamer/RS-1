using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RS1_Ispit_asp.net_core.ViewModels
{
    public class TakmicenjePregledVM
    {
        public string skola { get; set; }
        public int skolaID { get; set; }
        public int razred { get; set; }
        public List<Row> row { get; set; }
        public class Row
        {
            public int TakmicenjeID { get; set; }
            public string predmet { get; set; }
            public int razred { get; set; }
            public string datum { get; set; }
            public int brojUcesnikaNijePristupio { get; set; }
            public string skola { get; set; }
            public string odjeljenje { get; set; }
            public string imePrezime { get; set; }
        }
    }
}
