using Biblioteca.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Biblioteca.Controllers
{
    public class AmminitsratoreController : Controller
    {
   
        [Authorize(Roles="ADMIN")]
        [HttpGet]
        public ActionResult Dashboard() 
        {
            DAO dao = new DAO();

            List<Libri_Prestiti_Studenti> storicoPrestiti = dao.PrestitoStorico();

            List<Libri_Prestiti_Studenti> listaStudentiPrestito = dao.StudentiPrestito();

            List<Libri_Prestiti_Studenti> listaPrestiti = dao.LibriPrestito();

            List<Libri_Prestiti_Studenti> scaduti = dao.prestitiScaduti();

            ViewBag.ListaPrestiti = listaPrestiti;
            ViewBag.StoricoPrestiti = storicoPrestiti;
            ViewBag.ListaStudentiPrestito = listaStudentiPrestito;
            ViewBag.PrestitiScaduti = scaduti;

            return View();
        }

        [HttpGet]
        public ActionResult AllStudenti() 
        {
            DAO dao = new DAO();
            List<Studenti> studenti = dao.Studenti.ToList();
            return View(studenti);
        }

       
        [HttpGet][ActionName("UpdateStudenti")]
        [Authorize(Roles = "ADMIN")]
        public ActionResult UpdateStudenti_Get(string email)
        {
            DAO dao = new DAO();
            Studenti studenti = dao.Studenti.Single( stud => stud.Email == email);

            return View(studenti);
        }

       
        [HttpPost][ActionName("UpdateStudenti")]
        [Authorize(Roles = "ADMIN")]
        public ActionResult UpdateStudenti_Post(Studenti studenti, string email) 
        {
            DAO dao = new DAO();
            dao.ModificaStudente(studenti, email);

            return RedirectToAction("AllStudenti");
        }

        [HttpPost]
        [Authorize(Roles = "ADMIN")]
        public ActionResult DeleteStudenti(string email)
        {
            DAO dao = new DAO();
            dao.DeleteStudente(email);

            return Redirect(Request.UrlReferrer.ToString());
        }

        [HttpGet]
        [ActionName("UpdateLibro")]
        [Authorize(Roles = "ADMIN")]
        public ActionResult UpdateLibro_Get(string codice)
        {
            DAO dao = new DAO();
            Libri libri = dao.Libri.Single( lib => lib.Codice == codice);

            return View(libri);
        }

        [HttpPost]
        [ActionName("UpdateLibro")]
        [Authorize(Roles = "ADMIN")]
        public ActionResult UpdateLibro_Get(Libri libri, string codice)
        {
            DAO dao = new DAO();
            dao.ModificaLibro(libri, codice);

            return RedirectToAction("Index", "Libri");
        }

        [HttpPost]
        [Authorize(Roles = "ADMIN")]
        public ActionResult DeleteLibro(string codice)
        {
            DAO dao = new DAO();
            dao.DeleteLibro(codice);

            return Redirect(Request.UrlReferrer.ToString());
        }
    }
}