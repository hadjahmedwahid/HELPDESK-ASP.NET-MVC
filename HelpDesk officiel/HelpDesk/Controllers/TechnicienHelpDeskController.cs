using HelpDesk.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace HelpDesk.Controllers
{
    public class TechnicienHelpDeskController : Controller
    {
        // GET: TechnicienHelpDesk
        public ActionResult Index()
        {
            if (Session["user"] == null)
                return RedirectToAction("Index", "Home");
            if ((Session["user"] as Utilisateur).EtatUser != "Superviseur")
                return RedirectToAction("InvalidAccess", "Home");

            TechnicienHelpDesk technicien = new TechnicienHelpDesk();
            return View(technicien);
        }

        // GET: TechnicienHelpDesk/Details/5
        public ActionResult Details(int ? id)
        {
            if (Session["user"] == null)
                return RedirectToAction("Index", "Home");

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Dictionary<String, String> data = new Dictionary<string, string>();
            data["IdUser"] = "" + id;
            TechnicienHelpDesk tech = new TechnicienHelpDesk();
           
            tech=tech.find_by_id(data);

            if (tech == null)
            {
                return HttpNotFound();
            }
            return View(tech);

        }

        // GET: TechnicienHelpDesk/Create
        public ActionResult Create()
        {
            if (Session["user"] == null)
                return RedirectToAction("Index", "Home");
            if ((Session["user"] as Utilisateur).EtatUser != "Superviseur")
                return RedirectToAction("InvalidAccess", "Home");
            return View();
        }

        // POST: TechnicienHelpDesk/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TechnicienHelpDesk technicien,Categorie categorie,Direction direction )
        {
            if (Session["user"] == null)
                return RedirectToAction("Index", "Home");
            if ((Session["user"] as Utilisateur).EtatUser != "Superviseur")
                return RedirectToAction("InvalidAccess", "Home");

            try
            {
               if (ModelState.IsValid)
               {
                    technicien.direction = direction;
                    technicien.categorie = categorie;
                    technicien.save();
                    return RedirectToAction("index");
               }
           }
            catch
            {
            }
            return View();    
        }

        // GET: TechnicienHelpDesk/Edit/5
        
        public ActionResult Edit(int ? id)
        {
            if (Session["user"] == null)
                return RedirectToAction("Index", "Home");
            if ((Session["user"] as Utilisateur).EtatUser != "Superviseur")
                return RedirectToAction("InvalidAccess", "Home");

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Dictionary<String, String> data = new Dictionary<string, string>();
            data["IdUser"] = "" + id;
            TechnicienHelpDesk tech = new TechnicienHelpDesk();
            tech = tech.find_by_id(data);
            if (tech == null)
            {
                return HttpNotFound();
            }
            return View(tech);
        }

        // POST: TechnicienHelpDesk/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit( TechnicienHelpDesk tech, Categorie categorie, Direction direction)
        {
            if (Session["user"] == null)
                return RedirectToAction("Index", "Home");
            if ((Session["user"] as Utilisateur).EtatUser != "Superviseur")
                return RedirectToAction("InvalidAccess", "Home");


            try
            {
                if (ModelState.IsValid)
                {
                    tech.categorie = categorie;
                    tech.direction = direction;
                    tech.edit_by_id();
                    return RedirectToAction("Index");
                }
                return View(tech);
            }
            catch
            {
                return View();
            }
        }

        // GET: TechnicienHelpDesk/Delete/5
        public ActionResult Delete(int ? id)
        {
            if (Session["user"] == null)
                return RedirectToAction("Index", "Home");
            if ((Session["user"] as Utilisateur).EtatUser != "Superviseur")
                return RedirectToAction("InvalidAccess", "Home");


            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Dictionary<String, String> data = new Dictionary<string, string>();
            data["IdUser"] = "" + id;
            TechnicienHelpDesk tech = new TechnicienHelpDesk();
            tech = tech.find_by_id(data);
            if (tech == null)
            {
                return HttpNotFound();
            }
            return View(tech);
        }

        // POST: TechnicienHelpDesk/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            if (Session["user"] == null)
                return RedirectToAction("Index", "Home");
            if ((Session["user"] as Utilisateur).EtatUser != "Superviseur")
                return RedirectToAction("InvalidAccess", "Home");


            try
            {
                Dictionary<String, String> data = new Dictionary<string, string>();
                data["IdUser"] = "" + id;
                TechnicienHelpDesk tech = new TechnicienHelpDesk();
                tech = tech.find_by_id(data);
                tech.remove(data);
                return RedirectToAction("Index");

            }
            catch
            {
                return View();
            }
        }
    }
}
