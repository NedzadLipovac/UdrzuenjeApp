using System.Collections.Generic;
using UdruzenjeApp.Models;

namespace UdruzenjeApp.ViewModels
{
    public class PrijedlogDogadjajaPrikaziVM
    {
        public List<ApplicationUser> clanStavke { get; set; }
        public List<nekaKlasa> podaci { get; set; }
       

        public class nekaKlasa
        {
            public int PredlozeniDogadjajID { get; set; }
            public string NazivPrijedloga { get; set; }
            public string OpisPredlozenogDogadjaja { get; set; }
            public int ClanID { get; set; }
        }
    }
}
