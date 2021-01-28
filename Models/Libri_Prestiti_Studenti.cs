using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Biblioteca.Models
{
    public class Libri_Prestiti_Studenti
    {
        //Libri
        public string Codice { set; get; }
        public string Autore { set; get; }
        public string Titolo { set; get; }
        public string Editore { set; get; }
        public int Anno { set; get; }
        public string Luogo { set; get; }
        public string Pagine { set; get; }
        public string Classificazione { set; get; }
        public string Collocazione { set; get; }

        //Prestiti
        public int Id { set; get; }
        public string CodiceLibro { set; get; }
        public int MatricolaStudente { set; get; }
        public Boolean Riportato { set; get; }
        public DateTime Data_Inizio_Prestito { set; get; }
        public DateTime DataFinePrestito { set; get; }

        //Studenti
        public int Matricola { set; get; }
        public string Nome { set; get; }
        public string Cognome { set; get; }
        public string Email { set; get; }
        public string Classe { set; get; }
        public string Password { set; get; }
        public string Ruolo { set; get; }

        public Libri_Prestiti_Studenti()
        {
        }

        public Libri_Prestiti_Studenti(string codice, string autore, string titolo, string editore, int anno, string luogo, string pagine, string classificazione, string collocazione, int id, string codiceLibro, int matricolaStudente, bool riportato, DateTime data_Inizio_Prestito, DateTime dataFinePrestito, int matricola, string nome, string cognome, string email, string classe, string password, string ruolo)
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
            Id = id;
            CodiceLibro = codiceLibro;
            MatricolaStudente = matricolaStudente;
            Riportato = riportato;
            Data_Inizio_Prestito = data_Inizio_Prestito;
            DataFinePrestito = dataFinePrestito;
            Matricola = matricola;
            Nome = nome;
            Cognome = cognome;
            Email = email;
            Classe = classe;
            Password = password;
            Ruolo = ruolo;
        }
    }
}