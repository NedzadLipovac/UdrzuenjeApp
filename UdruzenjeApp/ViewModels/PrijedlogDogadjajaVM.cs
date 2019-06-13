using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UdruzenjeApp.ViewModels
{
    public class PrijedlogDogadjajaVM
    {
        public int PredlozeniDogadjajID { get; set; }

        public string NazivPrijedloga { get; set; }
        public string OpisPredlozenogDogadjaja { get; set; }
        public int ClanID { get; set; }
        public string ImeClana { get; set; }
    }
}
