using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UdruzenjeApp.ViewModels
{
    public class DogadjajDodajVM
    {
        public int DogadjadajID { get; set; }
        public string Naziv { get; set; }
        public DateTime VrijemeOdrzavanja { get; set; }
        public string opis { get; set; }
        public bool OgranicenoMjesta { get; set; }
        public int brojMjesta { get; set; }
        public int UpraviteljID { get; set; }
        public int GradID { get; set; }
        public string ImeClana { get; set; }

        public List<SelectListItem> gradStavke { get; set; }
    }
}
