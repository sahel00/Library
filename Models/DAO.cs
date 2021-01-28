using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Biblioteca.Models
{
    public class DAO: DbContext
    {
        public DbSet<Libri> Libri { set; get; }
        public DbSet<Studenti> Studenti { set; get; }
        public DbSet<Prestiti> Prestiti { set; get; }


        /*Prestiti Operazioni*/
        public void AggiungiPrestito(Prestiti prestiti) 
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DAO"].ConnectionString;
            DateTime dataInizio = DateTime.Now;
            DateTime dataFine = dataInizio.AddDays(30);

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("spAggiungiPrestito", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                SqlParameter paramCodice = new SqlParameter();
                paramCodice.ParameterName = "@Codice";
                paramCodice.Value = prestiti.CodiceLibro;
                cmd.Parameters.Add(paramCodice);

                SqlParameter paramMatricola = new SqlParameter();
                paramMatricola.ParameterName = "@Matricola";
                paramMatricola.Value = prestiti.MatricolaStudente;
                cmd.Parameters.Add(paramMatricola);

                SqlParameter paramDataInizio = new SqlParameter();
                paramDataInizio.ParameterName = "@DataInizio";
                paramDataInizio.Value = dataInizio;
                cmd.Parameters.Add(paramDataInizio);

                SqlParameter paramDataFine = new SqlParameter();
                paramDataFine.ParameterName = "@DataFine";
                paramDataFine.Value = dataFine;
                cmd.Parameters.Add(paramDataFine);

                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void Riporta(int id)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DAO"].ConnectionString;
            DateTime dataInizio = DateTime.Now;
            DateTime dataFine = dataInizio.AddDays(30);

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("spRiportaLibro", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                SqlParameter paramId = new SqlParameter();
                paramId.ParameterName = "@Id";
                paramId.Value = id;
                cmd.Parameters.Add(paramId);


                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void ModificaPrestito(Prestiti prestiti, int id)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DAO"].ConnectionString;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("spModificaPrestito", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                SqlParameter paramCodiceLibro = new SqlParameter();
                paramCodiceLibro.ParameterName = "@CodiceLibro";
                paramCodiceLibro.Value = prestiti.CodiceLibro;
                cmd.Parameters.Add(paramCodiceLibro);

                SqlParameter paramMatricolaStudente = new SqlParameter();
                paramMatricolaStudente.ParameterName = "@MatricolaStudente";
                paramMatricolaStudente.Value = prestiti.MatricolaStudente;
                cmd.Parameters.Add(paramMatricolaStudente);

                SqlParameter paramDataInizio = new SqlParameter();
                paramDataInizio.ParameterName = "@DataInizio";
                paramDataInizio.Value = prestiti.Data_Inizio_Prestito;
                cmd.Parameters.Add(paramDataInizio);


                SqlParameter paramDataFine = new SqlParameter();
                paramDataFine.ParameterName = "@DataFine";
                paramDataFine.Value = prestiti.Data_Fine_Prestito;
                cmd.Parameters.Add(paramDataFine);

                SqlParameter paramRiportato = new SqlParameter();
                paramRiportato.ParameterName = "@Riportato";
                paramRiportato.Value = prestiti.Riportato;
                cmd.Parameters.Add(paramRiportato);

                SqlParameter paramId = new SqlParameter();
                paramId.ParameterName = "@Id";
                paramId.Value = id;
                cmd.Parameters.Add(paramId);

                con.Open();
                cmd.ExecuteNonQuery();
            }
        }
        /*Libri Operazioni*/
        public void ModificaLibro(Libri libri, string codice)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DAO"].ConnectionString;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("spModificaLibro", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                SqlParameter paramCodice = new SqlParameter();
                paramCodice.ParameterName = "@Codice";
                paramCodice.Value = libri.Codice;
                cmd.Parameters.Add(paramCodice);

                SqlParameter paramAutore = new SqlParameter();
                paramAutore.ParameterName = "@Autore";
                paramAutore.Value = libri.Autore;
                cmd.Parameters.Add(paramAutore);

                SqlParameter paramTitolo = new SqlParameter();
                paramTitolo.ParameterName = "@Titolo";
                paramTitolo.Value = libri.Titolo;
                cmd.Parameters.Add(paramTitolo);

                SqlParameter paramEditore = new SqlParameter();
                paramEditore.ParameterName = "@Editore";
                paramEditore.Value = libri.Editore;
                cmd.Parameters.Add(paramEditore);

                SqlParameter paramAnno = new SqlParameter();
                paramAnno.ParameterName = "@Anno";
                paramAnno.Value = libri.Anno;
                cmd.Parameters.Add(paramAnno);

                SqlParameter paramLuogo = new SqlParameter();
                paramLuogo.ParameterName = "@Luogo";
                paramLuogo.Value = libri.Luogo;
                cmd.Parameters.Add(paramLuogo);

                SqlParameter paramPagine = new SqlParameter();
                paramPagine.ParameterName = "@Pagine";
                paramPagine.Value = libri.Pagine;
                cmd.Parameters.Add(paramPagine);

                SqlParameter paramClassificazione = new SqlParameter();
                paramClassificazione.ParameterName = "@Classificazione";
                paramClassificazione.Value = libri.Classificazione;
                cmd.Parameters.Add(paramClassificazione);

                SqlParameter paramCollocazione = new SqlParameter();
                paramCollocazione.ParameterName = "@Collocazione";
                paramCollocazione.Value = libri.Collocazione;
                cmd.Parameters.Add(paramCollocazione);


                SqlParameter paramCodiceDaModicare = new SqlParameter();
                paramCodiceDaModicare.ParameterName = "@CodiceDaModicare";
                paramCodiceDaModicare.Value = codice;
                cmd.Parameters.Add(paramCodiceDaModicare);

                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void DeleteLibro(string codice)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DAO"].ConnectionString;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("spEliminaLibro", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                SqlParameter paramCodice = new SqlParameter();
                paramCodice.ParameterName = "@Codice";
                paramCodice.Value = codice;
                cmd.Parameters.Add(paramCodice);

                con.Open();
                cmd.ExecuteNonQuery();
            }
        }


        /*DashBoard Operazioni*/
        public List<Libri_Prestiti_Studenti> LibriPrestito() 
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DAO"].ConnectionString;
            List<Libri_Prestiti_Studenti> listaPrestiti = new List<Libri_Prestiti_Studenti>();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("spLibriPrestito", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    Libri_Prestiti_Studenti libriprestito = new Libri_Prestiti_Studenti();
                    libriprestito.CodiceLibro = rdr["CodiceLibro"].ToString();
                    libriprestito.MatricolaStudente = Convert.ToInt32(rdr["MatricolaStudente"]);
                    libriprestito.Titolo = rdr["Titolo"].ToString();

                    listaPrestiti.Add(libriprestito);
                }
            }

            return listaPrestiti;
        }

        public List<Libri_Prestiti_Studenti> StudentiPrestito()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DAO"].ConnectionString;
            List<Libri_Prestiti_Studenti> listaStudentiPrestito = new List<Libri_Prestiti_Studenti>();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("spStudentiPrestiti", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    Libri_Prestiti_Studenti  studentiPrestito = new Libri_Prestiti_Studenti();
                    studentiPrestito.Nome = rdr["Nome"].ToString();
                    studentiPrestito.Cognome = rdr["Cognome"].ToString();
                    studentiPrestito.Titolo = rdr["Titolo"].ToString();

                    listaStudentiPrestito.Add(studentiPrestito);
                }
            }

            return listaStudentiPrestito;
        }

        public List<Libri_Prestiti_Studenti> PrestitoStorico()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DAO"].ConnectionString;
            List<Libri_Prestiti_Studenti> storicoPrestiti = new List<Libri_Prestiti_Studenti>();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("spPrestitiStorico", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    Libri_Prestiti_Studenti storico = new Libri_Prestiti_Studenti();
                    storico.Nome = rdr["Nome"].ToString();
                    storico.Cognome = rdr["Cognome"].ToString();
                    storico.Id = Convert.ToInt32(rdr["Id"]);

                    storicoPrestiti.Add(storico);
                }
            }

            return storicoPrestiti;
        }

        public List<Libri_Prestiti_Studenti> prestitiScaduti() 
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DAO"].ConnectionString;
            List<Libri_Prestiti_Studenti> scaduti1 = new List<Libri_Prestiti_Studenti>();
            DateTime data = DateTime.Now;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("spPrestitoScaduto", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                con.Open();

                SqlParameter paramData = new SqlParameter();
                paramData.ParameterName = "@Data";
                paramData.Value = data;
                cmd.Parameters.Add(paramData);
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    Libri_Prestiti_Studenti scaduti = new Libri_Prestiti_Studenti();
                    scaduti.Nome = rdr["Nome"].ToString();
                    scaduti.Matricola = Convert.ToInt32(rdr["Matricola"]);
                    scaduti.CodiceLibro = rdr["CodiceLibro"].ToString();
                    scaduti1.Add(scaduti);
                }
            }

            return scaduti1;
        }
        /*Studenti Operazioni*/

        public void Registrazione(Studenti studenti)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DAO"].ConnectionString;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("spRegistrazione", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                SqlParameter paramNome = new SqlParameter();
                paramNome.ParameterName = "@Nome";
                paramNome.Value = studenti.Nome;
                cmd.Parameters.Add(paramNome);

                SqlParameter paramCognome = new SqlParameter();
                paramCognome.ParameterName = "@Cognome";
                paramCognome.Value = studenti.Cognome;
                cmd.Parameters.Add(paramCognome);

                SqlParameter paramEmail = new SqlParameter();
                paramEmail.ParameterName = "@Email";
                paramEmail.Value = studenti.Email;
                cmd.Parameters.Add(paramEmail);

                SqlParameter paramClasse = new SqlParameter();
                paramClasse.ParameterName = "@Classe";
                paramClasse.Value = studenti.Classe;
                cmd.Parameters.Add(paramClasse);

                SqlParameter paramPassword = new SqlParameter();
                paramPassword.ParameterName = "@Password";
                paramPassword.Value = studenti.Password;
                cmd.Parameters.Add(paramPassword);

                con.Open();
                cmd.ExecuteNonQuery();
            }

        }

        public Studenti Login(Studenti studenti)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DAO"].ConnectionString;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("spLogin", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                SqlParameter paramEmail = new SqlParameter();
                paramEmail.ParameterName = "@Email";
                paramEmail.Value = studenti.Email;
                cmd.Parameters.Add(paramEmail);

                SqlParameter paramPassword = new SqlParameter();
                paramPassword.ParameterName = "@Password";
                paramPassword.Value = studenti.Password;
                cmd.Parameters.Add(paramPassword);


                con.Open();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        return new Studenti(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetString(4), reader.GetString(5), reader.GetString(6));
                    }
                }
                return null;
            }
        }

        public void ModificaStudente(Studenti studenti, string email)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DAO"].ConnectionString;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("spModificaStudente", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                SqlParameter paramNome = new SqlParameter();
                paramNome.ParameterName = "@Nome";
                paramNome.Value = studenti.Nome;
                cmd.Parameters.Add(paramNome);

                SqlParameter paramCognome = new SqlParameter();
                paramCognome.ParameterName = "@Cognome";
                paramCognome.Value = studenti.Cognome;
                cmd.Parameters.Add(paramCognome);

                SqlParameter paramEmail = new SqlParameter();
                paramEmail.ParameterName = "@Email";
                paramEmail.Value = studenti.Email;
                cmd.Parameters.Add(paramEmail);

                SqlParameter paramClasse = new SqlParameter();
                paramClasse.ParameterName = "@Classe";
                paramClasse.Value = studenti.Classe;
                cmd.Parameters.Add(paramClasse);

                SqlParameter paramEmailModificare = new SqlParameter();
                paramEmailModificare.ParameterName = "@EmailDaModicare";
                paramEmailModificare.Value = email;
                cmd.Parameters.Add(paramEmailModificare);

                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void DeleteStudente(string email)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DAO"].ConnectionString;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("spEliminaStudente", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                SqlParameter paramEmail = new SqlParameter();
                paramEmail.ParameterName = "@Email";
                paramEmail.Value = email;
                cmd.Parameters.Add(paramEmail);

                con.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}