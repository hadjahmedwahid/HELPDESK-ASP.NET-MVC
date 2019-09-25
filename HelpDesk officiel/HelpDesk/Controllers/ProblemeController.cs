using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HelpDesk.Models;
using System.Net;
using System.Threading.Tasks;

namespace HelpDesk.Controllers
{
    public class ProblemeController : Controller
    {
        // GET: Probleme
        public ActionResult Index()
        {
            if (Session["user"] == null)
                return RedirectToAction("Index", "Home");
            if ((Session["user"] as Utilisateur).EtatUser == "EmployeAgence")
                return RedirectToAction("InvalidAccess", "Home");
            if ((Session["user"] as Utilisateur).EtatUser == "Superviseur")
            {
                Dictionary<String, String> data = new Dictionary<string, string>();
                data["IdUser"] = "" + (Session["user"] as Utilisateur).IdUser;
                Superviseur superviseur = new Superviseur();
                Session["user"] = superviseur.find_by_id(data);
            }
            if ((Session["user"] as Utilisateur).EtatUser == "TechnicienHelpdesk")
            {
                Dictionary<String, String> data = new Dictionary<string, string>();
                data["IdUser"] = "" + (Session["user"] as Utilisateur).IdUser;
                TechnicienHelpDesk technicienHelpDesk = new TechnicienHelpDesk();
                Session["user"] = technicienHelpDesk.find_by_id(data);
            }


            Probleme probleme = new Probleme();
            return View(probleme);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(Probleme probleme)
        {
            Dictionary<string, string> data = new Dictionary<string, string>();
            data["ObjetProbleme"] = probleme.ObjetProbleme;
            data["DescreptionProbleme"] = probleme.ObjetProbleme;
             
            return View("Search",probleme.find_by_search(data));
        }

        // GET: Probleme/Details/5
        public ActionResult Details(int? id)
        {
            if (Session["user"] == null)
                return RedirectToAction("Index", "Home");

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Dictionary<String, String> data = new Dictionary<string, string>();
            data["IdProbleme"] = "" + id;
            Probleme probleme = new Probleme();
            probleme = probleme.find_by_id(data);

            if (probleme == null)
            {
                return HttpNotFound();
            }
            return View(probleme);
        }

        // GET: Probleme/Create
        public ActionResult Create()
        {
            if (Session["user"] == null)
                return RedirectToAction("Index", "Home");
            if ((Session["user"] as Utilisateur).EtatUser == "EmployeAgence")
                return RedirectToAction("InvalidAccess", "Home");


            return View();
        }

        // POST: Probleme/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdProbleme,ObjetProbleme,DescreptionProbleme,IdCategorie")] Probleme probleme,Categorie categorie)
        {
            if (Session["user"] == null)
                return RedirectToAction("Index", "Home");
            if ((Session["user"] as Utilisateur).EtatUser == "EmployeAgence")
                return RedirectToAction("InvalidAccess", "Home");

            try
            {
                probleme.categorie = categorie;
                probleme.save();
                
            }
            catch
            {

            }
            if (ModelState.IsValid)
            {
                return RedirectToAction("index");
            }
            return View();
        }

        // GET: Probleme/Edit/5
        public ActionResult Edit(int? id)
        {
            if (Session["user"] == null)
                return RedirectToAction("Index", "Home");
            if ((Session["user"] as Utilisateur).EtatUser != "Superviseur" )
                return RedirectToAction("InvalidAccess", "Home");

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Dictionary<String, String> data = new Dictionary<string, string>();
            data["IdProbleme"] = "" + id;
            Probleme probleme = new Probleme();
            probleme = probleme.find_by_id(data);
            if (probleme == null)
            {
                return HttpNotFound();
            }
            return View(probleme);

        }

        // POST: Probleme/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "IdProbleme,ObjetProbleme,DescreptionProbleme,IdCategorie")] Probleme probleme,Categorie categorie)
        {
            if (Session["user"] == null)
                return RedirectToAction("Index", "Home");
            if ((Session["user"] as Utilisateur).EtatUser != "Superviseur")
                return RedirectToAction("InvalidAccess", "Home");

            try
            {
                if (ModelState.IsValid)
                {
                    probleme.categorie = categorie;
                    probleme.edit_by_id();
                    return RedirectToAction("Index");
                }
                return View(probleme);
            }
            catch
            {
                return View();
            }
        }

        // GET: Probleme/Delete/5
        public ActionResult Delete(int id)
        {
            if (Session["user"] == null)
                return RedirectToAction("Index", "Home");
            if ((Session["user"] as Utilisateur).EtatUser != "Superviseur")
                return RedirectToAction("InvalidAccess", "Home");

            return View();
        }

        // POST: Probleme/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            if (Session["user"] == null)
                return RedirectToAction("Index", "Home");
            if ((Session["user"] as Utilisateur).EtatUser != "Superviseur")
                return RedirectToAction("InvalidAccess", "Home");


            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
