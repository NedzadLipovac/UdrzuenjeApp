using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace UdruzenjeApp.Models
{
    public class Odgovor
    {
        [Key]
        public int OdgovorID { get; set; }
        public string TextOdgovora { get; set; }

        [ForeignKey("Pitanje")]
        public int PitanjeID { get; set; }
        public Pitanje pitanje { get; set; }
    }
}
