using Biblioteca.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Biblioteca.Controllers
{
    public class HomeController : Controller
    {
        [Authorize]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet][ActionName("Registrazione")]
        public ActionResult Registrazione() 
        {
            return View();
        }

        [HttpPost][ActionName("Registrazione")]
        public ActionResult Registrazione(Studenti studenti) 
        {
            TryUpdateModel(studenti);
            if (ModelState.IsValid) 
            {
                DAO dao = new DAO();
                dao.Registrazione(studenti);
                return RedirectToAction("Login", "Home");
            }
            
            return View();
        }

        [HttpGet][ActionName("Login")]
        public ActionResult Login() 
        {
            return View();
        }

        [HttpPost][ActionName("Login")]
        public ActionResult Login(Studenti studenti, string returnUrl) 
        {
            DAO dao = new DAO();
            Studenti studenti1 = dao.Login(studenti);
            // var studenti1 = dao.Studenti.Where(x => x.Email == studenti.Email && x.Password == studenti.Password).First();

            if (studenti1 != null)
            {
                FormsAuthentication.SetAuthCookie(studenti1.Email, false);
                if (Url.IsLocalUrl(returnUrl) && returnUrl.Length > 1
                        && returnUrl.StartsWith("/") && !returnUrl.StartsWith("//")
                        && !returnUrl.StartsWith("/\\"))
                {
                    return Redirect(returnUrl);
                }
                else if(studenti1.Ruolo == "ADMIN")
                {
                    return RedirectToAction("Dashboard", "Amminitsratore");
                }
                else
                {
                    return RedirectToAction("Index");
                }
            }
            else 
            {
                ModelState.AddModelError("", "Invalid Email/ Password");
                return View();
            }
        }

        [Authorize]
        public ActionResult SignOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Home");
        }
    }
}