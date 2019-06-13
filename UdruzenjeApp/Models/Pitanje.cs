using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace UdruzenjeApp.Models
{
    public class Pitanje
    {
        [Key]
        public int PitanjeID { get; set; }
        public string TextPitanja { get; set; }

        [ForeignKey("Anketa")]
        public int AnketaID { get; set; }
        public Anketa anketa { get; set; }
    }
}
