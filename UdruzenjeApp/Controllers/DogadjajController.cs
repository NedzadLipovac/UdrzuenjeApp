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
    [Authorize (Roles ="Admin")]
    public class DogadjajController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        private readonly UserManager<ApplicationUser> _userManager;

        public DogadjajController(UserManager<ApplicationUser>userManager)
        {
            _userManager = userManager;       
        }

        public IActionResult Prikazi(string NazivDogadjaja, int? GradId)
        {
            DogadjajiViewModel model =new  DogadjajiViewModel();
                
                model.rows = db.dogadjaj
                .Where(y=>(y.GradID==GradId || GradId==null)&&(y.Naziv.Contains(NazivDogadjaja)||NazivDogadjaja==null))
                .Select(d => new DogadjajiViewModel.Row
                {
                    Naziv = d.Naziv,
                    opis = d.opis,
                    OgranicenoMjesta = d.OgranicenoMjesta,
                    brojMjesta = d.brojMjesta,
                    VrijemeOdrzavanja = d.VrijemeOdrzavanja,
                    DogadjajID = d.ID,
                    grad = db.grad.FirstOrDefault(x => x.ID == d.GradID).Naziv,
                   

                }).ToList();
            model.gradovi =new List<SelectListItem>()
            {
                new SelectListItem()
                {
                    Value=string.Empty,
                    Text="(Odaberite Grad)"
                }
            };
            model.gradovi.AddRange(db.grad.Select(g => new SelectListItem(g.Naziv, g.ID.ToString()))); 
            
            ViewData["podaci"] = model;

            return View(model);
        }

        public IActionResult Delete(int TrazeniID)
        {
            Dogadjaj temp = db.dogadjaj.Where(d => d.ID == TrazeniID).SingleOrDefault();
            if (temp == null)
                return View("Error");
            db.Remove(temp);
            db.SaveChanges();

            return Redirect("/Dogadjaj/Prikazi");
        }
        public IActionResult Dodaj()
        {
           
            DogadjajDodajVM model = new DogadjajDodajVM();
            int idc = int.Parse(_userManager.GetUserId(principal: HttpContext.User));
            model.ImeClana = db.Users.FirstOrDefault(x => x.Id == idc).Ime;
            model.UpraviteljID = idc;
            // model.upraviteljStavke = db.Users.Select(u =>new SelectListItem( u.Ime + " " + u.Prezime,u.Id.ToString())).ToList();
            model.gradStavke = db.grad.Select(g => new SelectListItem(g.Naziv, g.ID.ToString())).ToList();
            return PartialView("uredi",model);
        }
        public IActionResult Uredi(int DogadjajID)
        {
            Dogadjaj d = db.dogadjaj.SingleOrDefault(b => b.ID == DogadjajID);
            if(d==null)
            {
                TempData["porukaError"] = "Greskaa prilikom uredjivanja Dogadjaja";
                return View("Error");
            }
            DogadjajDodajVM model = new DogadjajDodajVM();
            int idc = int.Parse(_userManager.GetUserId(principal: HttpContext.User));
            model.ImeClana = db.Users.FirstOrDefault(x => x.Id == idc).Ime;
            model.DogadjadajID = d.ID;
            model.gradStavke = db.grad.Select(g => new SelectListItem(g.Naziv, g.ID.ToString())).ToList();
            model.opis = d.opis;
            model.Naziv = d.Naziv;
            model.OgranicenoMjesta = d.OgranicenoMjesta;
            model.VrijemeOdrzavanja = d.VrijemeOdrzavanja;
            model.brojMjesta = d.brojMjesta;
            model.GradID = d.GradID;
            model.UpraviteljID = idc;

            return PartialView("uredi", model);
        }
        public IActionResult Snimi(DogadjajDodajVM model)
        {
            Dogadjaj d;
            if(model.DogadjadajID == 0)
            {
                d = new Dogadjaj();
                db.Add(d);
                TempData["porukaSuccss"] = "Uspjesno ste Dodalli dogadjaj";
            }
            else
            {
                d = db.dogadjaj.FirstOrDefault(x => x.ID == model.DogadjadajID);
                TempData["porukaSuccss"] = "Uspjesno ste Izmjenili dogadjaj";

            }
            d.ID = model.DogadjadajID;
            d.opis = model.opis;
            d.Naziv = model.Naziv;
            d.OgranicenoMjesta = model.OgranicenoMjesta;
            d.VrijemeOdrzavanja = model.VrijemeOdrzavanja;
            d.brojMjesta = model.brojMjesta;
            d.UpraviteljID = model.UpraviteljID;
            d.GradID = model.GradID;

            
            db.SaveChanges();
            db.Dispose();
            return RedirectToAction(nameof(Prikazi));
        }
        public int getBrojDogadjaja() { return db.dogadjaj.ToList().Count(); }
    }
    
}