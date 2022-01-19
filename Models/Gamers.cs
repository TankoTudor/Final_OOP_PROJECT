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
    public class Gamers
    {
        [Key]
        public int IdGame { get; set; }
        public string Denumire { get; set; }
        public int Pret { get; set; }
        public string Publisher { get; set; }
        public string Descriere { get; set; }
        public DateTime DataLansare { get; set; }
    }
    public class GamersDbContext : DbContext
    {
        public DbSet<Gamers> Produse { get; set; }
    }
}