using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace UdruzenjeApp.Models
{
    public class Anketa
    {
        [Key]
        public int AnketaID { get; set; }
        public string nazivAnkete { get; set; }
       


        public int UpraviteljID { get; set; }
        [ForeignKey("UpraviteljID")]
        public ApplicationUser Upravitelj { get; set; }
    }
}
