using HelpDesk.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HelpDesk.Controllers
{
    public class RechercheController : Controller
    {
        // GET: Recherche
        public ActionResult Index()
        {
            if (Session["user"] == null)
                return RedirectToAction("Index", "Home");
            if ((Session["user"] as Utilisateur).EtatUser == "employerAgence")
                return RedirectToAction("InvalidAccess", "Home");

            recherche r = new recherche();
            return View(r);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(recherche r)
        {






            recherche ch = new recherche();

            ch = r.find(r.chaine);

            return View(ch);
        }


    }
}
