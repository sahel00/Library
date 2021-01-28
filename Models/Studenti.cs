using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Biblioteca.Models
{
    [Table("Studenti")]
    public class Studenti
    {

        [Key] 
        public int Matricola { set; get; }
        public string Nome { set; get; }
        public string Cognome { set; get; }
        public string Email { set; get; }
        public string Classe { set; get; }
        public string Password { set; get; }
        public string Ruolo { set; get; }

        public Studenti()
        {
        }

        public Studenti(int matricola, string nome, string cognome, string email, string classe, string password, string ruolo)
        {
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