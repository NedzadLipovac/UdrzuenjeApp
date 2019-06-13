using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using UdruzenjeApp.Controllers;
using UdruzenjeApp.Data;
using UdruzenjeApp.Models;
using UdruzenjeApp.ViewModels;

namespace UdruzenjeApp_UnitTest
{
    [TestClass]
    public class UnitTest1
    {
        public readonly UserManager<ApplicationUser> _userManager;
        ApplicationDbContext db = new ApplicationDbContext();

        public class Row
        {
            public string Naziv { get; set; }
            public DateTime VrijemeOdrzavanja { get; set; }
            public string opis { get; set; }
            public int DogadjajID { get; set; }
            public string grad { get; set; }
        }

        [TestMethod]
        public void Index_Vraca_Li_Tacan_Broj_Dogadjaja()
        {
            DogadjajController dc = new DogadjajController(null);

            int? ocekivani = dc.getBrojDogadjaja() as int?;

            ViewResult vr = dc.Prikazi("",1) as ViewResult;
            DogadjajiViewModel model = vr.Model as DogadjajiViewModel;

            Assert.AreEqual(1, model.rows.Count);

        }

 
        [TestMethod]
        [Timeout(3000)]
        public void Index_TestBrzine_Izvrsavanja()
        {
            DogadjajController pc = new DogadjajController(null);
            pc.Prikazi("",null);
        }


        [TestMethod]
        public void Index_Model_Lista_Dogadjaja()
        {
            DogadjajController dc = new DogadjajController(null);

            List<Dogadjaj> dogadjaji = db.dogadjaj.
                      Include(x => x.grad)
                      .Where(x => x.GradID == 1).ToList();
            List<Row> ocekivani = new List<Row>();
            //lista ocekivanih
            ocekivani = dogadjaji.Select(x => new Row()
            {
                Naziv=x.Naziv,
                VrijemeOdrzavanja=x.VrijemeOdrzavanja,
                opis=x.opis,
                DogadjajID=x.ID,
                grad=x.grad.Naziv
            }).ToList();

            //rezultat lista
            ViewResult vr = dc.Prikazi("", 1) as ViewResult;
            DogadjajiViewModel model = vr.Model as DogadjajiViewModel;
            List<Row> rezultat = new List<Row>();
            rezultat = model.rows.Select(x => new Row()
            {
                Naziv = x.Naziv,
                VrijemeOdrzavanja = x.VrijemeOdrzavanja,
                opis = x.opis,
                DogadjajID = x.DogadjajID,
                grad = x.grad
            }).ToList();

            CollectionAssert.AreEqual(ocekivani, rezultat,
                Comparer<Row>.Create(
                    (prvi, drugi) => prvi.grad == drugi.grad
                    && prvi.Naziv == drugi.Naziv
                    && prvi.opis == drugi.opis
                    && prvi.VrijemeOdrzavanja == drugi.VrijemeOdrzavanja
                    && prvi.DogadjajID == drugi.DogadjajID ? 0 : 1
                    )
                );

        }
    }
}
