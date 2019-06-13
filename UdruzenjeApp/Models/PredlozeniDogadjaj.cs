using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace UdruzenjeApp.Models
{
    public class PredlozeniDogadjaj
    {
        [Key]
        public int PredlozeniDogadjajID {get;set;}
        public string NazivPrijedloga { get; set; }
        public string OpisPredlozenogDogadjaja { get; set; }

        [ForeignKey("Clan")]
        public int ClanID { get; set; }
        public ApplicationUser clan { get; set; }


       
    }
}
