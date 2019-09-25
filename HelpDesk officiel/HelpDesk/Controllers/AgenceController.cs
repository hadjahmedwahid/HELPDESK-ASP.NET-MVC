using HelpDesk.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace helpdeskv1.Controllers
{
    public class AgenceController : Controller
    {
        // GET: Agence
        
        public ActionResult Index()
        {
            if (Session["user"] == null)
                return RedirectToAction("Index", "Home");
            if((Session["user"] as Utilisateur).EtatUser != "Superviseur")
                return RedirectToAction("InvalidAccess", "Home");
            Agence agence = new Agence();
            return View(agence);
        }

        // GET: Agence/Details/5
        public ActionResult Details(int ? id)
        {

            if (Session["user"] == null)
                return RedirectToAction("Index", "Home");

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Dictionary<String, String> data = new Dictionary<string, string>();
            data["NumeroAgence"] = "" + id;
            Agence ag = new Agence();
            ag=ag.find_by_id(data);


            if (ag == null)
            {
                return HttpNotFound();
            }
            return View(ag);
        }

        // GET: Agence/Create
        public ActionResult Create()
        {
            if (Session["user"] == null)
                return RedirectToAction("Index", "Home");
            if ((Session["user"] as Utilisateur).EtatUser != "Superviseur")
                return RedirectToAction("InvalidAccess", "Home");

            
            return View();
        }

        // POST: Agence/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( Agence agence,DRE dre)
        {

            //[Bind(Include = "idag,nomag,wilayaag,addressag,iddre")]
            if (Session["user"] == null)
                return RedirectToAction("Index", "Home");
            if ((Session["user"] as Utilisateur).EtatUser != "Superviseur")
                return RedirectToAction("InvalidAccess", "Home");

            try
            {
                if (ModelState.IsValid)
                {
                    agence.Dre = dre;
                    agence.save();
                    return RedirectToAction("index");
                }
            }
            catch
            {

            }
            
            return View();
        }

        // GET: Agence/Edit/5
        public ActionResult Edit(int? id)
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
            data["NumeroAgence"] = "" + id;
            Agence d1 = new Agence();
            d1 = d1.find_by_id(data);
            if (d1 == null)
            {
                return HttpNotFound();
            }
            return View(d1);
        }

        // POST: Agence/Edit/5
        [HttpPost]
        public ActionResult Edit( Agence ag,DRE dre)
        {
            //[Bind(Include = "idag,nomag,wilayaag,addressag,iddre")]
            if (Session["user"] == null)
                return RedirectToAction("Index", "Home");
            if ((Session["user"] as Utilisateur).EtatUser != "Superviseur")
                return RedirectToAction("InvalidAccess", "Home");

            try
            {
                if (ModelState.IsValid)
                {
                    ag.Dre = dre;
                    ag.edit_by_id();
                    return RedirectToAction("Index");
                }
                return View(ag);
                
            }
            catch
            {
                return View();
            }
        }

        // GET: Agence/Delete/5
        public ActionResult Delete(int  ? id)
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
            data["[NumeroAgence]"] = "" + id;
            Agence ag = new Agence();
            ag = ag.find_by_id(data);
            if (ag == null)
            {
                return HttpNotFound();
            }
            return View(ag);
        }

        // POST: Agence/Delete/5
        [HttpPost]
        public ActionResult Delete(int  ? id, FormCollection collection)
        {
            if (Session["user"] == null)
                return RedirectToAction("Index", "Home");
            if ((Session["user"] as Utilisateur).EtatUser != "Superviseur")
                return RedirectToAction("InvalidAccess", "Home");

            try
            {
                Dictionary<String, String> data = new Dictionary<string, string>();
                data["NumeroAgence"] = "" + id;

                Agence ag = new Agence();
               ag=ag.find_by_id(data);
                ag.remove(data);
                return RedirectToAction("Index");

            }
            catch
            {
                return View();
            }

        }
        
    }
}
