using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.DynamicData;

namespace Biblioteca.Models
{
    [Table("Libri")]
    public class Libri
    {
        [Key]
        public string Codice { set; get; }
        public string Autore { set; get; }
        public string Titolo { set; get; }
        public string Editore { set; get; }
        public int Anno { set; get; }
        public string Luogo { set; get; }
        public string Pagine { set; get; }
        public string Classificazione { set; get; }
        public string Collocazione { set; get; }

        public Libri()
        {
        }

        public Libri(string codice, string autore, string titolo, string editore, int anno, string luogo, string pagine, string classificazione, string collocazione)
        {
            Codice = codice;
            Autore = autore;
            Titolo = titolo;
            Editore = editore;
            Anno = anno;
            Luogo = luogo;
            Pagine = pagine;
            Classificazione = classificazione;
            Collocazione = collocazione;
        }
    }
}