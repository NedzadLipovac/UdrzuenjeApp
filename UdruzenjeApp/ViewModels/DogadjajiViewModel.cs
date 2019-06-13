using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UdruzenjeApp.ViewModels
{
    public class DogadjajiViewModel
    {
        public List<Row> rows { get; set; }
        public List<SelectListItem> gradovi { get; set; }
        public int GradId { get; set; }
        public string NazivDogadjaja { get; set; }
        public class Row {
            public string Naziv { get; set; }
            public DateTime VrijemeOdrzavanja { get; set; }
            public string opis { get; set; }
            public bool OgranicenoMjesta { get; set; }
            public int brojMjesta { get; set; }
            public int DogadjajID { get; set; }
            public string grad { get; set; }
        }
       
      

    }
}
