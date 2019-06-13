using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace UdruzenjeApp.Models
{
    public class Dogadjaj
    {
        [Key]
        public int ID { get; set; }
        public string Naziv { get; set; }
        public DateTime VrijemeOdrzavanja { get; set; }
        public string opis { get; set; }
        public bool OgranicenoMjesta { get; set; }
        public int brojMjesta { get; set; }

        public int UpraviteljID { get; set; }
        [ForeignKey("UpraviteljID")]
        public ApplicationUser Upravitelj { get; set; }

        [ForeignKey("Grad")]
        public int GradID { get; set; }
        public Grad grad { get; set; }

    }
}
