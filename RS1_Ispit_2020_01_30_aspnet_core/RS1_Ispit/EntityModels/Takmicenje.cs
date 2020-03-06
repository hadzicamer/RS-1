using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RS1_Ispit_asp.net_core.EntityModels
{
    public class Takmicenje
    {
        public int TakmicenjeID { get; set; }
        public int PredmetID { get; set; }
        public virtual Predmet Predmet { get; set; }
        public int SkolaID { get; set; }
        public virtual Skola Skola { get; set; }
        public int razred { get; set; }
        public DateTime datum { get; set; }
        public bool zakljuceno { get; set; }
        public List<TakmicenjeUcesnik> ucesnici { get; set; }
        public Takmicenje()
        {
            ucesnici = new List<TakmicenjeUcesnik>();
        }
    }
}
