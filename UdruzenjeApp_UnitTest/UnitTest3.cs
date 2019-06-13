using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections;
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
   public class UnitTest3
    {
        public  UserManager<ApplicationUser> _userManager;
        ApplicationDbContext db = new ApplicationDbContext();
        private IHostingEnvironment he;

        [TestMethod]
        [Timeout(3000)]
        public void Prikazi_TestBrzine_Izvrsavanja()
        {
            
            DrzavaController dc = new DrzavaController();
            dc.Prikazi();
        }

        public class Test
        {
            public int DrzavaID { get; set; }
            public string Naziv { get; set; }
        }
        [TestMethod]
        public void Test_Prikaz_Drzava()
        {
            DrzavaController dc = new DrzavaController();
            List<Drzava> drzave = db.drzava.ToList();
            List<Test> ocekivani = new List<Test>();

            ocekivani = drzave.Select(x => new Test()
            {
                DrzavaID=x.ID,
                Naziv=x.Naziv
            }).ToList();

            List<Test> rezultat = new List<Test>();
            ViewResult vr = dc.Prikazi() as ViewResult;
           List<DrzavaDodajVM> model = vr.Model as List<DrzavaDodajVM>;

            rezultat = model.Select(x => new Test()
            {
                DrzavaID=x.DrzavaID,
                Naziv=x.Naziv
            }).ToList();

            CollectionAssert.AreEqual(ocekivani, rezultat,
                Comparer<Test>.Create(
                    (prvi, drugi) => prvi.Naziv==drugi.Naziv
                    && prvi.DrzavaID==drugi.DrzavaID? 0 : 1
                    ));



        }

    }
}
