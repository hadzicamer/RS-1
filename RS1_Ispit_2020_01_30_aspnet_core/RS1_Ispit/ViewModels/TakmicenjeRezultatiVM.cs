using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RS1_Ispit_asp.net_core.ViewModels
{
    public class TakmicenjeRezultatiVM
    {
        public int TakmicenjeID { get; set; }
        public string skola { get; set; }
        public string predmet { get; set; }
        public int razred{ get; set; }
        public string datum { get; set; }
        public List<Row> row { get; set; }
        public int skolaID { get; set; }
        public bool zaklj { get; set; }

        public class Row
        {
            public int TakmicenjeID { get; set; }

            public int TakmicenjeUcesnikID { get; set; }
            public string odjeljenje { get; set; }
            public int brojUDnevniku{ get; set; }
            public bool isPristupio{ get; set; }
            public int bodovi { get; set; }
            public bool zaklj { get; set; }

        }
    }
}
