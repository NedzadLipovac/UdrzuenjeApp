using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UdruzenjeApp.ViewModels
{
    public class ClanDodajVM
    {
        public int ClanId { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public DateTime DatumRodjenja { get; set; }
        public string Email { get; set; }
        public string BrojTelefona { get; set; }
        public string Adresa { get; set; }
        public int? GradID { get; set; }
        public string TipKorisnika { get; set; }
        public List<SelectListItem> GradStavke { get; set; }
    }
}
