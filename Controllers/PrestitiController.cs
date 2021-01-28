using Biblioteca.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Biblioteca.Controllers
{
    public class PrestitiController : Controller
    {
        [HttpGet][ActionName("Inserisci_Prestito")][Authorize(Roles = "ADMIN")]
        public ActionResult Inserisci_Prestito_get()
        {
            ViewBag.DataInizio = DateTime.Now;
            ViewBag.DataFine = ViewBag.DataInizio.AddDays(30);
            return View();
        }

        [HttpPost]
        [ActionName("Inserisci_Prestito")]
        [Authorize(Roles = "ADMIN")]
        public ActionResult Inserisci_Prestito_post(Prestiti prestiti)
        {
            DAO dao = new DAO();
            dao.AggiungiPrestito(prestiti);

            return View();
        }

        [HttpGet]
        [Authorize(Roles = "ADMIN")]
        public ActionResult AllPrestiti()
        {
            DAO dao = new DAO();
            List<Prestiti> prestiti = dao.Prestiti.ToList();
            return View(prestiti);
        }

        [HttpGet]
        [Authorize(Roles = "ADMIN")]
        public ActionResult DetailsPrestito(int id)
        {
            DAO dao = new DAO();
            Prestiti prestiti = dao.Prestiti.Single(pre => pre.Id == id);
            return View(prestiti);
        }

        [HttpPost]
        [Authorize(Roles = "ADMIN")]
        public ActionResult deletePrestiti(int id)
        {
            DAO dao = new DAO();
            Prestiti prestiti = dao.Prestiti.Find(id);
            dao.Prestiti.Remove(prestiti);
            dao.SaveChanges();
            return RedirectToAction("AllPrestiti");
        }

        [HttpPost]
        [Authorize(Roles = "ADMIN")]
        public ActionResult riportaLibro(int id)
        {
            DAO dao = new DAO();
            dao.Riporta(id);
            return RedirectToAction("AllPrestiti");
        }

        [HttpGet]
        [ActionName("UpdatePrestito")]
        [Authorize(Roles = "ADMIN")]
        public ActionResult UpdatePrestito_get(int id)
        {
            DAO dao = new DAO();
            Prestiti prestiti = dao.Prestiti.Single(pr => pr.Id == id);
            return View(prestiti);
        }

        [HttpPost]
        [ActionName("UpdatePrestito")]
        [Authorize(Roles = "ADMIN")]
        public ActionResult UpdatePrestito_post(Prestiti prestiti, int id)
        {
            DAO dao = new DAO();
            dao.ModificaPrestito(prestiti, id);

            return RedirectToAction("AllPrestiti");
        }
    }
}