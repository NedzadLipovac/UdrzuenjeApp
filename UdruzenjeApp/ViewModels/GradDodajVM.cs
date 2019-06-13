using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UdruzenjeApp.ViewModels
{
    public class GradDodajVM
    {
        public int GradID { get; set; }
        public string Naziv { get; set; }
        public int DrzavaID { get; set; }

        public List<SelectListItem> DrzavaStavke { get; set; }
    }
}
