using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using UdruzenjeApp.Data;
using UdruzenjeApp.Models;
using UdruzenjeApp.ViewModels;

namespace UdruzenjeApp.Controllers
{
    public class DrzavaController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        public IActionResult Prikazi()
        {
            
            List<DrzavaDodajVM> model = db.drzava
                .Select(k => new DrzavaDodajVM
                {
                    Naziv = k.Naziv,
                    DrzavaID = k.ID
                }).ToList();
            ViewData["podaci"] = model;

            return View(model);
        }
        public IActionResult Delete(int TrazeniID)
        {
            
            Drzava temp = db.drzava.Where(k => k.ID == TrazeniID).SingleOrDefault();
            if (temp == null)
                return View("Error");
            db.Remove(temp);
            db.SaveChanges();
            return RedirectToAction(nameof(Prikazi));
        }

        public IActionResult Dodaj()
        {
            
            DrzavaDodajVM model = new DrzavaDodajVM();
            return PartialView("uredi", model);
        }

        public IActionResult Uredi(int DRZAVAID)
        {
            
            Drzava c = db.drzava.SingleOrDefault(b => b.ID == DRZAVAID);
            if(c==null)
            {
                TempData["porukaError"] = "Greska prilikom uredjivanja Drzave";
                return View("Error");
            }
            DrzavaDodajVM model = new DrzavaDodajVM();
            model.DrzavaID = c.ID;
            model.Naziv = c.Naziv; 

            return PartialView("uredi", model);
        }

        public IActionResult Snimi(DrzavaDodajVM model)
        {
          
            Drzava c;
            if (model.DrzavaID == 0)
            {
                c = new Drzava();
                db.Add(c);
                TempData["porukaSucces"] = "Uspjesno ste dodali Drzavu";
            }
            else
            {
                c = db.drzava.FirstOrDefault(x => x.ID == model.DrzavaID);
                TempData["porukaSucces"] = "Uspjesno ste dodali Drzavu";
            }
            c.ID = model.DrzavaID;
            c.Naziv = model.Naziv;

            db.SaveChanges();
            db.Dispose();

            return RedirectToAction(nameof(Prikazi));
        }
    }
}