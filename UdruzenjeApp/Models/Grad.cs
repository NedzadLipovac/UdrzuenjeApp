using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace UdruzenjeApp.Models
{
    public class Grad
    {
        [Key]
        public int ID { get; set; }
        public string Naziv { get; set; }
        
        [ForeignKey("Drzava")]
        public int DrzavaID { get; set; }
        public Drzava drzava { get; set; }
    }
}
