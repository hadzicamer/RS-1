using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using RS1_Ispit_asp.net_core.EF;
using RS1_Ispit_asp.net_core.EntityModels;
using RS1_Ispit_asp.net_core.ViewModels;

namespace RS1_Ispit_asp.net_core.Controllers
{
    public class TakmicenjeController : Controller
    {
        private MojContext db;
        public TakmicenjeController(MojContext db)
        {
            this.db = db;
        }
        public IActionResult Index()
        {
            TakmicenjeIndexVM model = new TakmicenjeIndexVM();
            model.skole = db.Skola.Select(x => new SelectListItem
            {
                Value=x.Id.ToString(),
                Text=x.Naziv
            }).ToList();

            return View(model);
        }

        [HttpPost]
        public IActionResult Index(TakmicenjeIndexVM model)
        {
            return RedirectToAction("Pregled", new { skolaID = model.skolaID, razred = model.razred });
        }

        public IActionResult Pregled(int skolaID,int razred)
        {
            TakmicenjePregledVM model = db.Skola.Where(x => x.Id == skolaID).Select(x => new TakmicenjePregledVM
            {
                skolaID=x.Id,
                skola=x.Naziv,
                razred=razred
            }).FirstOrDefault();

            model.row = db.Takmicenje.Where(x => x.SkolaID == skolaID && x.razred==razred).Select(x => new TakmicenjePregledVM.Row
            {
                TakmicenjeID=x.TakmicenjeID,
                predmet=x.Predmet.Naziv,
                razred=x.razred,
                datum=x.datum.ToShortDateString(),
                brojUcesnikaNijePristupio=x.ucesnici.Where(s=>s.isPristupio==false).Count(),
                imePrezime=x.ucesnici.OrderByDescending(y=>y.bodovi).Select(y=>y.OdjeljenjeStavka.Ucenik.ImePrezime).FirstOrDefault(),
                odjeljenje = x.ucesnici.OrderByDescending(y => y.bodovi).Select(y => y.OdjeljenjeStavka.Odjeljenje.Oznaka).FirstOrDefault(),
                skola = x.ucesnici.OrderByDescending(y => y.bodovi).Select(y => y.OdjeljenjeStavka.Odjeljenje.Skola.Naziv).FirstOrDefault(),
            }).ToList();

            return View(model);
        }

        public IActionResult Dodaj(int skolaID)
        {
            TakmicenjeDodajVM model = db.Skola.Where(x => x.Id == skolaID).Select(x => new TakmicenjeDodajVM
            {
                skolaID=x.Id,
                skola=x.Naziv
            }).FirstOrDefault();

            model.predmeti = db.Predmet.Select(x => new SelectListItem
            {
                Value=x.Id.ToString(),
                Text=x.Naziv
            }).ToList();

            return View(model);
        }

        [HttpPost]
        public IActionResult Dodaj(TakmicenjeDodajVM model)
        {
            Takmicenje t = new Takmicenje
            {
                SkolaID=model.skolaID,
                PredmetID=model.predmetID,
                razred=model.razred,
                datum=model.datum
            };

            List<TakmicenjeUcesnik> lista = new List<TakmicenjeUcesnik>();
            lista = db.DodjeljenPredmet.Where(x => x.PredmetId == model.predmetID && x.ZakljucnoKrajGodine == 5).Select(x=>new TakmicenjeUcesnik() { 
            OdjeljenjeStavkaID=x.OdjeljenjeStavkaId,
            bodovi=0,
            isPristupio=false,
            }).ToList();

            foreach (var x in lista)
            {
                if (db.DodjeljenPredmet.Where(o => o.OdjeljenjeStavkaId == x.OdjeljenjeStavkaID).Select(o => o.ZakljucnoKrajGodine).Average() > 4)
                {
                    t.ucesnici.Add(x);
                }
            }

            db.Takmicenje.Add(t);
            db.SaveChanges();
            return RedirectToAction("Pregled",new {skolaID=model.skolaID,razred=model.razred});
        }

        public IActionResult Rezultati(int TakmicenjeID)
        {
            TakmicenjeRezultatiVM model = db.Takmicenje.Where(x => x.TakmicenjeID == TakmicenjeID).Select(x => new TakmicenjeRezultatiVM
            {
                TakmicenjeID=x.TakmicenjeID,
                skola=x.Skola.Naziv,
                predmet=x.Predmet.Naziv,
                razred=x.razred,
                datum=x.datum.ToShortDateString(),
               zaklj=x.zakljuceno
            }).FirstOrDefault();

            return View(model);
        }

        public IActionResult AjaxPregled(int TakmicenjeID)
        {
            TakmicenjeRezultatiVM model = new TakmicenjeRezultatiVM();
            model.zaklj = db.Takmicenje.Where(x => x.TakmicenjeID == TakmicenjeID).Select(x => x.zakljuceno).FirstOrDefault();

            model.row = db.TakmicenjeUcesnik.Where(x=>x.TakmicenjeID==TakmicenjeID).Select(x => new TakmicenjeRezultatiVM.Row
            {
               TakmicenjeUcesnikID=x.TakmicenjeUcesnikID,
                odjeljenje=x.OdjeljenjeStavka.Odjeljenje.Oznaka,
               brojUDnevniku=x.OdjeljenjeStavka.BrojUDnevniku,
               isPristupio=x.isPristupio,
               bodovi=x.bodovi,
             
            }).ToList();

            return PartialView(model);
        }

        public IActionResult Uredi(int TakmicenjeUcesnikID)
        {
            TakmicenjeUrediVM model = db.TakmicenjeUcesnik.Where(x => x.TakmicenjeUcesnikID == TakmicenjeUcesnikID).Select(x => new TakmicenjeUrediVM
            {
                ucesnikID=x.TakmicenjeUcesnikID,
                bodovi=x.bodovi
            }).FirstOrDefault();


            model.ucesnici = db.TakmicenjeUcesnik.Select(x => new SelectListItem
            {
             Value=x.TakmicenjeUcesnikID.ToString(), 
            Text= x.OdjeljenjeStavka.Odjeljenje.Oznaka+ " - "+ x.OdjeljenjeStavka.Ucenik.ImePrezime            
            }).ToList();
        

            return PartialView(model);
        }

        [HttpPost]
        public IActionResult Uredi(int bodovi, int TakmicenjeUcesnikID)
        {
            TakmicenjeUcesnik ucesnik = db.TakmicenjeUcesnik.Where(x => x.TakmicenjeUcesnikID == TakmicenjeUcesnikID).FirstOrDefault();
            if (ucesnik != null)
            {
                ucesnik.bodovi = bodovi > 100 ? 100 : bodovi;
                ucesnik.isPristupio = true;
            }
            db.SaveChanges();
            return RedirectToAction("AjaxPregled", new { TakmicenjeID = ucesnik.TakmicenjeID });
        }

        public IActionResult Zakljucaj(int TakmicenjeID)
        {

            Takmicenje takmicenje = db.Takmicenje.Where(x => x.TakmicenjeID == TakmicenjeID).FirstOrDefault();

            if (takmicenje != null)
            {
                takmicenje.zakljuceno = true;
            }

            db.SaveChanges();

            return RedirectToAction("Rezultati", "Takmicenje", new { TakmicenjeID = takmicenje.TakmicenjeID });
        }

    }
}