using HelpDesk.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace HelpDesk.Controllers
{
    public class DREController : Controller
    {
        // GET: DRE
        public ActionResult Index()
        {
            if (Session["user"] == null)
                return RedirectToAction("Index", "Home");
            if ((Session["user"] as Utilisateur).EtatUser != "Superviseur")
                return RedirectToAction("InvalidAccess", "Home");

            DRE dre = new DRE();
            return View(dre);
            
        }

        // GET: DRE/Details/5
        public ActionResult Details(int ?id)
        {
            if (Session["user"] == null)
                return RedirectToAction("Index", "Home");

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Dictionary<String, String> data = new Dictionary<string, string>();
            data["NumeroDRE"] = "" + id;
            DRE dre = new DRE();
            dre = dre.find_by_id(data);

            if (dre == null)
            {
                return HttpNotFound();
            }
            return View(dre);
        }

        // GET: DRE/Create
        public ActionResult Create()
        {
            if (Session["user"] == null)
                return RedirectToAction("Index", "Home");
            if ((Session["user"] as Utilisateur).EtatUser != "Superviseur")
                return RedirectToAction("InvalidAccess", "Home");

            return View();
        }

        // POST: DRE/Create
        [HttpPost]
        public ActionResult Create([Bind(Include = "NumeroDRE,NomDRE,Wilaya,AdresseDRE")] DRE dre1)
        {
            if (Session["user"] == null)
                return RedirectToAction("Index", "Home");
            if ((Session["user"] as Utilisateur).EtatUser != "Superviseur")
                return RedirectToAction("InvalidAccess", "Home");

            try
            {
                dre1.save();
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

        // GET: DRE/Edit/5
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
            data["NumeroDRE"] = "" + id;
            DRE d1 = new DRE();
            d1=d1.find_by_id(data);
            if (d1 == null)
            {
                return HttpNotFound();
            }
            return View(d1);
        }

        // POST: DRE/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "NumeroDRE,NomDRE,Wilaya,AdresseDRE")] DRE dre1)
        {
            if (Session["user"] == null)
                return RedirectToAction("Index", "Home");
            if ((Session["user"] as Utilisateur).EtatUser != "Superviseur")
                return RedirectToAction("InvalidAccess", "Home");

            try
            {
                // TODO: Add update logic here
                dre1.edit_by_id();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: DRE/Delete/5
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
            data["NumeroDRE"] = "" + id;
            DRE d1 = new DRE();
            d1 = d1.find_by_id(data);
            if (d1 == null)
            {
                return HttpNotFound();
            }
            return View(d1);
        }

        // POST: DRE/Delete/5
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
                data["NumeroDRE"] = "" + id;

                DRE d1 = new DRE();
                d1 = d1.find_by_id(data);
                d1.remove(data);
                return RedirectToAction("Index");

            }
            catch
            {
                return View();
            }
        }
    }
}
