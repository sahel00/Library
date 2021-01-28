using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Biblioteca.Models
{
    [Table("Prestiti")]
    public class Prestiti
    {
        public int Id { set; get; }
        public string CodiceLibro { set; get; }
        public int MatricolaStudente { set; get; }
        public DateTime Data_Inizio_Prestito { set; get; }
        public DateTime Data_Fine_Prestito { set; get; }

        public Boolean Riportato { set; get; }
    }
}