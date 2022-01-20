using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Threading.Tasks;


using System.Data.Entity;
using System.ComponentModel.DataAnnotations;


namespace Final_OOP_PROJECT.Models
{
    public class Cart
    {
        [Key]
        public int IdCart { get; set; }
        public System.DateTime CheckOutTime { get; set; }
        public int IdUtilizator { get; set; }
        public int IdGame { get; set; }
        public string DenumJoc { get; set; }
        public int PretJoc { get; set; }
       
    }
}