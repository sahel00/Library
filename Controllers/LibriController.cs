using Biblioteca.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Biblioteca.Controllers
{
    public class LibriController : Controller
    {
        // GET: Libri
        public ActionResult Index()
        {
            DAO dao = new DAO();
            List<Libri> libri = dao.Libri.ToList();

            return View(libri);
        }

    }
}