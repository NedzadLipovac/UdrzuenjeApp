using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using UdruzenjeApp.Data;
using UdruzenjeApp.Models;
using UdruzenjeApp.ViewModels;

namespace RSI_Seminarski_V1.Controllers
{
   
    public class GradController : Controller
    {
            ApplicationDbContext db = new ApplicationDbContext();

        public IActionResult Prikazi()
        {
            List<GradDodajVM> model = db.grad
                .Select(k => new GradDodajVM
                {
                    Naziv = k.Naziv,
                    GradID = k.ID
                }).ToList();
            ViewData["podaci"] = model;

            return View(model);
        }
        public IActionResult Delete(int TrazeniID)
        {
           
            Grad temp = db.grad.Where(k => k.ID == TrazeniID).SingleOrDefault();
            if (temp == null)
                return View("Error");
            db.Remove(temp);
            db.SaveChanges();
            return RedirectToAction(nameof(Prikazi));
        }

        public IActionResult Dodaj()
        {
            
            GradDodajVM model = new GradDodajVM();
            model.DrzavaStavke = db.drzava.Select(j => new SelectListItem(j.Naziv, j.ID.ToString())).ToList();
            return PartialView("uredi", model);
        }

        public IActionResult Uredi(int GRADID)
        {
           
            Grad c = db.grad.SingleOrDefault(b => b.ID == GRADID);
            if(c==null)
            {
                TempData["porukaError"] = "Greska prilikom uredjivanja Grada";
                return View("Error");
            }
            GradDodajVM model = new GradDodajVM();
            model.GradID = c.ID;
            model.DrzavaStavke = db.drzava.Select(j => new SelectListItem(j.Naziv, j.ID.ToString())).ToList();
            model.Naziv = c.Naziv;
            model.DrzavaID = c.DrzavaID;

            return PartialView("uredi", model);
        }

        public IActionResult Snimi(GradDodajVM model)
        {
            
            Grad c;
            if (model.GradID == 0)
            {
                c = new Grad();
                db.Add(c);
                TempData["porukaSucces"] = "Uspjesno ste dodali Grad";
            }
            else
            {
                c = db.grad.FirstOrDefault(x => x.ID == model.GradID);
                TempData["porukaSucces"] = "Uspjesno ste dodali Grad";
            }
            c.ID = model.GradID;
            c.Naziv = model.Naziv;
            c.DrzavaID = model.DrzavaID;

            db.SaveChanges();
            db.Dispose();

            return RedirectToAction(nameof(Prikazi));
        }
    }
}