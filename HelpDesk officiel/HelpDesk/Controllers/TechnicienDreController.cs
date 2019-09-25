using HelpDesk.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace HelpDesk.Controllers
{
    public class TechnicienDreController : Controller
    {
        // GET: TechnicienDre
        public ActionResult Index()
        {
            if (Session["user"] == null)
                return RedirectToAction("Index", "Home");
            if ((Session["user"] as Utilisateur).EtatUser != "Superviseur")
                return RedirectToAction("InvalidAccess", "Home");

            TechnicienDre tech = new TechnicienDre();
            return View(tech);
        }

        // GET: TechnicienDre/Details/5
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
            TechnicienDre tech = new TechnicienDre();
           tech=tech.find_by_id(data);

            if (tech == null)
            {
                return HttpNotFound();
            }
            return View(tech);
        }

        // GET: TechnicienDre/Create
        public ActionResult Create()
        {
            if (Session["user"] == null)
                return RedirectToAction("Index", "Home");
            if ((Session["user"] as Utilisateur).EtatUser != "Superviseur")
                return RedirectToAction("InvalidAccess", "Home");
            return View();
           
        }

        // POST: TechnicienDre/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TechnicienDre tech , DRE dre)
        {
            if (Session["user"] == null)
                return RedirectToAction("Index", "Home");
            if ((Session["user"] as Utilisateur).EtatUser != "Superviseur")
                return RedirectToAction("InvalidAccess", "Home");

            try
            {
                if (ModelState.IsValid)
                {
                    tech.dre = dre;
                    tech.save();
                    return RedirectToAction("index");
                }
                
            }
            catch
            {

            }
            
            return View();


        }

        // GET: TechnicienDre/Edit/5
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
            TechnicienDre tech = new TechnicienDre();
            tech=tech.find_by_id(data);
            if (tech == null)
            {
                return HttpNotFound();
            }
            return View(tech);
        }

        // POST: TechnicienDre/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit( TechnicienDre tech, DRE _dre)
        {
            if (Session["user"] == null)
                return RedirectToAction("Index", "Home");
            if ((Session["user"] as Utilisateur).EtatUser != "Superviseur")
                return RedirectToAction("InvalidAccess", "Home");


            try
            {
                if (ModelState.IsValid)
                {
                    tech.dre = _dre;



                     tech.edit_by_id();
                    return Content(""+_dre.NomDRE);
                    //return RedirectToAction("Index");
                }
                return View(tech);
            }
            catch
            {
                return View();
            }
        }


        // GET: TechnicienDre/Delete/5
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
            TechnicienDre tech = new TechnicienDre();
           tech=tech.find_by_id(data);
            if (tech == null)
            {
                return HttpNotFound();
            }
            return View(tech);
        }

        // POST: TechnicienDre/Delete/5
        [HttpPost]
        public ActionResult Delete(int ? id, FormCollection collection)
        {
            if (Session["user"] == null)
                return RedirectToAction("Index", "Home");
            if ((Session["user"] as Utilisateur).EtatUser != "Superviseur")
                return RedirectToAction("InvalidAccess", "Home");


            try
            {
                Dictionary<String, String> data = new Dictionary<string, string>();
                data["IdUser"] = "" + id;
                TechnicienDre tech = new TechnicienDre();
                tech=tech.find_by_id(data);
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
