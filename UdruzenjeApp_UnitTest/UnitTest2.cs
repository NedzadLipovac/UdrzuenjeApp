 using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UdruzenjeApp.Controllers;
using UdruzenjeApp.Data;
using UdruzenjeApp.Models;
using UdruzenjeApp.ViewModels;

namespace UdruzenjeApp_UnitTest
{
    [TestClass]
    public class UnitTest2
    {
        ApplicationDbContext db = new ApplicationDbContext();

        public readonly UserManager<ApplicationUser> _userManager;

        [TestMethod]
        [Timeout(3000)]
        public void Index_TestBrzine_Izvrsavanja()
        {
            PredlozeniDogadjajController pc = new PredlozeniDogadjajController(_userManager);
            pc.Index();
        }

        public class Row
        {
            public int PredlozeniDogadjajID { get; set; }
            public string NazivPrijedloga { get; set; }
            public string OpisPredlozenogDogadjaja { get; set; }
            public int ClanID { get; set; }
        }
        [TestMethod]
        public void Prikazi_PredlozeniDogadjaj_Model()
        {

            PredlozeniDogadjajController pc = new PredlozeniDogadjajController(_userManager);
            List<PredlozeniDogadjaj> predlozeniDogadjai = db.predlozeniDogadjaj.ToList();
            List<Row> ocekivani = new List<Row>();

            ocekivani = predlozeniDogadjai.Select(x => new Row()
            {
                PredlozeniDogadjajID=x.PredlozeniDogadjajID,
                NazivPrijedloga=x.NazivPrijedloga,
                OpisPredlozenogDogadjaja=x.OpisPredlozenogDogadjaja,
                ClanID=x.ClanID
            }).ToList();

            ViewResult vr = pc.Index() as ViewResult;
            PrijedlogDogadjajaPrikaziVM model = vr.Model as PrijedlogDogadjajaPrikaziVM;

            List<Row> rezultat = model.podaci.Select(x => new Row()
            {
                PredlozeniDogadjajID = x.PredlozeniDogadjajID,
                NazivPrijedloga = x.NazivPrijedloga,
                OpisPredlozenogDogadjaja = x.OpisPredlozenogDogadjaja,
                ClanID = x.ClanID
            }).ToList();

            CollectionAssert.AreEqual(ocekivani, rezultat,
                Comparer<Row>.Create(
                    (prvi, drugi) => prvi.PredlozeniDogadjajID == drugi.PredlozeniDogadjajID
                    && prvi.OpisPredlozenogDogadjaja == drugi.OpisPredlozenogDogadjaja
                    && prvi.NazivPrijedloga == drugi.NazivPrijedloga
                    && prvi.ClanID == drugi.ClanID ? 0 : 1
                    ));
           
        }
    }
}
