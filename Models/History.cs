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
    public class History
    {
        [Key]
        public int IdIstoric { get; set; } 
        public DateTime DataFinalizareTranzactie { get; set; }
        public int IdUserVanzare { get; set; }
        public int Id_Game { get; set; }
        public string Pret { get; set; }
       
    }
    public class IstoricVanzariDBContext : DbContext
    {
        public DbSet<History> Transaction { get; set; }
    }
}