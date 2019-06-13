using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace UdruzenjeApp.Models
{
    public class Obavijest
    {
        [Key]
        public int ID { get; set; }
        public string NazivObavijesti { get; set; }
        public string TextObavijesti { get; set; }
        public DateTime datumObjave { get; set; }

        public int UpraviteljID { get; set; }
        [ForeignKey("UpraviteljID")]
        public ApplicationUser Upravitelj { get; set; }
    }
}
