using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using UdruzenjeApp.Data;
using UdruzenjeApp.Models;
using UdruzenjeApp.ViewModels;

namespace UdruzenjeApp.Controllers
{
    public class HomeController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        private readonly SignInManager<ApplicationUser> _signInManager;

        public IActionResult Index()
        {
            DogadjajiViewModel model = new DogadjajiViewModel();
                
                model.rows = db.dogadjaj
                .Select(d => new DogadjajiViewModel.Row
                {
                    Naziv = d.Naziv,
                    opis = d.opis,
                    OgranicenoMjesta = d.OgranicenoMjesta,
                    brojMjesta = d.brojMjesta,
                    VrijemeOdrzavanja = d.VrijemeOdrzavanja,
                    DogadjajID = d.ID,
                    grad=db.grad.FirstOrDefault(x=>x.ID==d.GradID).Naziv
                    
                }).ToList();
            return View(model);
        }
        public IActionResult PrikaziJedanDogadjaj(int dogadjajId)
        {
            DogadjajiViewModel model = new DogadjajiViewModel();
            Dogadjaj d = db.dogadjaj.Where(x => x.ID == dogadjajId).FirstOrDefault();
            model.rows = db.dogadjaj.Where(x => x.ID == d.ID)
                .Select(k => new DogadjajiViewModel.Row
                {
                    Naziv = d.Naziv,
                    opis = d.opis,
                    grad = db.grad.FirstOrDefault(x => x.ID == d.GradID).Naziv
                }
                ).ToList();
                
            return View(model);
        }
        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        //redirecta sa nav bara na razor pages za login i reg 
        public IActionResult RedirectToRegister()
        {
            return LocalRedirect("/Identity/Account/Register");
        }
        public IActionResult RedirectToLogin()
        {
            return LocalRedirect("/Identity/Account/Login");
        }
        public IActionResult RedirectToLogout()
        {
            return LocalRedirect("/Identity/Account/Logout");

        }

        public IActionResult RedirectTo2Factor()
        {
            return LocalRedirect("/Identity/Account/Manage/TwoFactorAuthentication");

        }
        public IActionResult RedirectToChange()
        {
            return LocalRedirect("/Identity/Account/Manage/ChangePassword");

        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });

        }
    }
}
