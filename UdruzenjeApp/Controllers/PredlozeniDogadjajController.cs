using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using UdruzenjeApp.Data;
using UdruzenjeApp.Models;
using UdruzenjeApp.ViewModels;

namespace UdruzenjeApp.Controllers
{
    [Authorize(Roles ="Admin,Clan")]
    public class PredlozeniDogadjajController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        ApplicationDbContext db = new ApplicationDbContext();
        public PredlozeniDogadjajController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }


        public IActionResult Index()
        {
            PrijedlogDogadjajaPrikaziVM model = new PrijedlogDogadjajaPrikaziVM();
            model.podaci = db.predlozeniDogadjaj
                .Select(d => new PrijedlogDogadjajaPrikaziVM.nekaKlasa
                {
                    NazivPrijedloga = d.NazivPrijedloga,
                    OpisPredlozenogDogadjaja = d.OpisPredlozenogDogadjaja,
                    PredlozeniDogadjajID = d.PredlozeniDogadjajID,
                    ClanID = d.ClanID

                }).ToList();
            model.clanStavke = db.Users.ToList();
            return View(model);
        }
        public IActionResult Dodaj()
        {
            PrijedlogDogadjajaVM model = new PrijedlogDogadjajaVM();
            int idc = int.Parse(_userManager.GetUserId(principal: HttpContext.User));
            model.ImeClana = db.Users.FirstOrDefault(x => x.Id == idc).Ime;
            // model.clanStavke = db.Users.Select(c => new SelectListItem(c.Ime + " " + c.Prezime, c.Id.ToString())).ToList();
            return View("uredi", model);
        }
        [Authorize(Roles ="Admin")]
        public IActionResult Uredi(int PredlozeniDogadjajID)
        {
            PredlozeniDogadjaj d = db.predlozeniDogadjaj.FirstOrDefault(x => x.PredlozeniDogadjajID == PredlozeniDogadjajID);
            if(d==null)
            {
                TempData["porukaError"] = "Greskaa prilikom uredjivanja PrijedlogaDogadjaja";
                return View("Error");
            }
            PrijedlogDogadjajaVM model = new PrijedlogDogadjajaVM();
            model.NazivPrijedloga = d.NazivPrijedloga;
            model.OpisPredlozenogDogadjaja = d.OpisPredlozenogDogadjaja;
            model.PredlozeniDogadjajID = d.PredlozeniDogadjajID;
            model.ClanID = d.ClanID;
            int idc = int.Parse(_userManager.GetUserId(principal: HttpContext.User));
            model.ImeClana = db.Users.FirstOrDefault(x => x.Id == idc).Ime;
            //model.clanStavke = db.Users.Select(c => new SelectListItem(c.Ime + " " + c.Prezime, c.Id.ToString())).ToList();
            return PartialView("uredi", model);

        }
        [Authorize(Roles ="Admin")]
        public IActionResult Obrisi(int PredlozeniDogadjajID)
        {
            PredlozeniDogadjaj d = db.predlozeniDogadjaj.FirstOrDefault(x => x.PredlozeniDogadjajID == PredlozeniDogadjajID);
            if (d == null)
            {
                TempData["porukaError"] = "Greskaa prilikom brisanja PrijedlogaDogadjaja";
                return View("Error");
            }
            db.predlozeniDogadjaj.Remove(d);
            db.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Snimi(PrijedlogDogadjajaVM model)
        {
            PredlozeniDogadjaj d;
            if(model.PredlozeniDogadjajID==0)
            {
                d = new PredlozeniDogadjaj();
                db.Add(d);
            }
            else
            {
                d = db.predlozeniDogadjaj.FirstOrDefault(x => x.PredlozeniDogadjajID == model.PredlozeniDogadjajID);
            }
            d.PredlozeniDogadjajID = model.PredlozeniDogadjajID;
            d.NazivPrijedloga = model.NazivPrijedloga;
            d.OpisPredlozenogDogadjaja = model.OpisPredlozenogDogadjaja;
            d.ClanID =int.Parse( _userManager.GetUserId(principal: HttpContext.User));
            db.SaveChanges();
            db.Dispose();
            return RedirectToAction(nameof(Index));
        }
    }
}