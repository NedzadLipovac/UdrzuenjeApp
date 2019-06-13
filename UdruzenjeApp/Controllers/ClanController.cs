using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using UdruzenjeApp.Data;
using UdruzenjeApp.Models;
using UdruzenjeApp.ViewModels;

namespace UdruzenjeApp.Controllers
{
    [Authorize (Roles ="Admin,Clan")]
    public class ClanController : Controller
    {
        ApplicationDbContext db;
        private IHostingEnvironment he;
        private readonly UserManager<ApplicationUser> _userManager;
        public ClanController(UserManager<ApplicationUser>userManager,IHostingEnvironment _he,ApplicationDbContext db)
        {
            _userManager = userManager;
            he = _he;
            this.db = db;

        }

        public IActionResult Prikazi()
        {
            var i = _userManager.GetUserId(principal: HttpContext.User);
            var b = int.Parse(i);
            List<ClanDodajVM> model = db.Users
                .Select(d => new ClanDodajVM
                {
                    Ime = d.Ime,
                    Prezime = d.Prezime,
                    DatumRodjenja = d.DatumRodjenja,
                    Email = d.Email,
                    BrojTelefona = d.brojTelefona,
                    Adresa = d.Adresa,
                    ClanId = d.Id
                }).ToList();
            ViewData["podaci"] = model;
            ViewData["k"] = b;
            return View(model);
        }
        [Authorize(Roles ="Admin")]
        public IActionResult Delete(int TrazeniID)
        {
            
            ApplicationUser temp = db.Users.Where(d => d.Id == TrazeniID).SingleOrDefault();
            if (temp == null)
                return View("Error");
            db.Remove(temp);
            db.SaveChanges();
            return RedirectToAction(nameof(Prikazi));
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Dodaj()
        {
           
            ClanDodajVM model = new ClanDodajVM();
            model.GradStavke = db.grad.Select(j => new SelectListItem(j.Naziv, j.ID.ToString())).ToList();
            return PartialView("uredi", model);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Uredi(int CLANID)
        {
        
            ApplicationUser c = db.Users.SingleOrDefault(b => b.Id == CLANID);
            if(c==null)
            {
                TempData["porukaError"] = "Greska prilikom uredjivanja Clana";
                return View("Error");
            }
            ClanDodajVM model = new ClanDodajVM();
            model.ClanId = c.Id;
            model.GradStavke = db.grad.Select(j => new SelectListItem(j.Naziv, j.ID.ToString())).ToList();
            model.Ime = c.Ime;
            model.Prezime = c.Prezime;
            model.DatumRodjenja = c.DatumRodjenja;
            model.Email = c.Email;
            model.BrojTelefona = c.brojTelefona;
            model.Adresa = c.Adresa;
            model.GradID = c.GradID;
            var roleId = db.UserRoles.Where(x => x.UserId == CLANID).FirstOrDefault().RoleId;
            model.TipKorisnika = db.Roles.Where(x => x.Id == roleId).FirstOrDefault().Name;
            return PartialView("uredi", model);
        }

        public IActionResult Snimi(ClanDodajVM model)
        {
            
            ApplicationUser c;
            if (model.ClanId == 0)
            {
                c = new ApplicationUser();
                db.Add(c);
                TempData["porukaSucces"] = "Uspjesno ste dodali Clana";
            }
            else
            {
                c = db.Users.FirstOrDefault(x => x.Id == model.ClanId);
                TempData["porukaSucces"] = "Uspjesno ste dodali Clana";
            }
            c.Id = model.ClanId;
            c.Ime = model.Ime;
            c.Prezime = model.Prezime;
            c.DatumRodjenja = model.DatumRodjenja;
            c.Email = model.Email;
            c.brojTelefona = model.BrojTelefona;
            c.Adresa = model.Adresa;
            c.GradID = model.GradID;

            db.SaveChanges();
            db.Dispose();

            return RedirectToAction(nameof(Prikazi));
        }

      
        [HttpGet]
        public IActionResult UrediProfil()
        {
           ClanUrediProfilVM model = new ClanUrediProfilVM();
            var i = _userManager.GetUserId(principal: HttpContext.User);
            var b = int.Parse(i);
            ApplicationUser k = new ApplicationUser();
            k = db.Users.Where(x => x.Id == b).FirstOrDefault();
            model.Id = k.Id;
            model.Ime = k.Ime;
            model.Prezime = k.Prezime;
           
            model.SlikaURL = k.SlikaURL;
            model.Adresa = k.Adresa;
            model.BrojTelefona = k.brojTelefona;
            model.Email = k.Email;
            model.DatumRodjenja = k.DatumRodjenja;
            ViewData["m"] = k.SlikaURL;
            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult>UrediProfil(ClanUrediProfilVM model,IFormFile SlikaURL)
        {
            ApplicationUser k = new ApplicationUser();
            k = db.Users.Where(x => x.Id == model.Id).FirstOrDefault();
            k.Ime = model.Ime;
            k.Prezime = model.Prezime;
            k.Adresa = model.Adresa;
            k.Email = model.Email;
            k.brojTelefona = model.BrojTelefona;
            k.DatumRodjenja = model.DatumRodjenja;
            if(SlikaURL!=null)
            {
                k.SlikaURL = SlikaURL.FileName;
                var filePath = Path.Combine(he.WebRootPath + "\\images\\User", SlikaURL.FileName);


                if (System.IO.File.Exists(filePath))
                {
                    db.Dispose();

                    System.IO.File.Delete(filePath);
                    db = new ApplicationDbContext();
                }
                SlikaURL.CopyTo(new FileStream(filePath, FileMode.Create));

            }


            if (k != null)
            {
                db.Users.Update(k);
               await db.SaveChangesAsync();
                db.Dispose();
            }
            //return Redirect("/Home/Index");
            return RedirectToAction(nameof(UrediProfil));
        }
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> DodijelaRole(int KorisnikID)
        {
            int userID = KorisnikID;
            int roleID = db.Roles.Where(x => x.Name == "Admin").FirstOrDefault().Id;
            var UserRole = db.UserRoles.Where(x => x.UserId == userID).FirstOrDefault();
            db.UserRoles.Remove(UserRole);

            IdentityUserRole<int> newUserRole = new IdentityUserRole<int>
            {
                RoleId = roleID,
                UserId = userID,

            };
            db.UserRoles.Add(newUserRole);
            await db.SaveChangesAsync();

            return RedirectToAction(nameof(Uredi), new { CLANID = userID });
        }
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UkloniRole(int KorisnikID)
        {
            int userID = KorisnikID;
            int roleID = db.Roles.Where(x => x.Name == "Clan").FirstOrDefault().Id;
            var UserRole = db.UserRoles.Where(x => x.UserId == userID).FirstOrDefault();
            db.UserRoles.Remove(UserRole);

            IdentityUserRole<int> newUserRole = new IdentityUserRole<int>
            {
                RoleId = roleID,
                UserId = userID,

            };
            db.UserRoles.Add(newUserRole);
            await db.SaveChangesAsync();

            return RedirectToAction(nameof(Uredi), new { CLANID = userID });
        }
    }
}