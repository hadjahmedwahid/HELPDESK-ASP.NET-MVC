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
    public class SuperviseurController : Controller
    {
        // GET: Superviseur
        public ActionResult Index()
        {
            if (Session["user"] == null)
                return RedirectToAction("Index", "Home");
            if ((Session["user"] as Utilisateur).EtatUser != "Superviseur")
                return RedirectToAction("InvalidAccess", "Home");


            Superviseur superviseur = new Superviseur();
            return View(superviseur);
        }

        // GET: Superviseur/Details/5
        public ActionResult Details(int? id)
        {
            if (Session["user"] == null)
                return RedirectToAction("Index", "Home");

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Dictionary<String, String> data = new Dictionary<string, string>();
            data["IdUser"] = "" + id;
            Superviseur superviseur = new Superviseur();
            superviseur = superviseur.find_by_id(data);

            if (superviseur == null)
            {
                return HttpNotFound();
            }
            return View(superviseur);

        }

        // GET: Superviseur/Create
        public ActionResult Create()
        {
            if (Session["user"] == null)
                return RedirectToAction("Index", "Home");
            if ((Session["user"] as Utilisateur).EtatUser != "Superviseur")
                return RedirectToAction("InvalidAccess", "Home");

            Direction direction = new Direction();
            ViewBag.direction = new SelectList(direction.find_all(), "IdDirection", "Libelle");
            return View();
        }

        // POST: Superviseur/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( Superviseur superviseur,Direction direction)
        {
            if (Session["user"] == null)
                return RedirectToAction("Index", "Home");
            if ((Session["user"] as Utilisateur).EtatUser != "Superviseur")
                return RedirectToAction("InvalidAccess", "Home");


            try
            {
                superviseur.direction = direction;
                superviseur.save();
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

        // GET: Superviseur/Edit/5
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
            data["IdUser"] = "" + id;
            Superviseur superviseur = new Superviseur();
            superviseur = superviseur.find_by_id(data);
            if (superviseur == null)
            {
                return HttpNotFound();
            }
            return View(superviseur);

        }


        // POST: Superviseur/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "IdUser,Login,Password,EtatUser,Nom,Prenom,Email,Phone,IdDirection")] Superviseur superviseur, Direction direction)
        {
            if (Session["user"] == null)
                return RedirectToAction("Index", "Home");
            if ((Session["user"] as Utilisateur).EtatUser != "Superviseur")
                return RedirectToAction("InvalidAccess", "Home");


            try
            {
                if (ModelState.IsValid)
                {
                    superviseur.direction = direction;
                    superviseur.edit_by_id();
                    return RedirectToAction("Index");
                }
                return View(superviseur);
            }
            catch
            {
                return View();
            }
        }


        // GET: Superviseur/Delete/5
        public ActionResult Delete(int? id)
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
            Superviseur superviseur = new Superviseur();
            superviseur = superviseur.find_by_id(data);
            if (superviseur == null)
            {
                return HttpNotFound();
            }
            return View(superviseur);

        }


        // POST: Superviseur/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (Session["user"] == null)
                return RedirectToAction("Index", "Home");
            if ((Session["user"] as Utilisateur).EtatUser != "Superviseur")
                return RedirectToAction("InvalidAccess", "Home");


            try
            {
                Dictionary<String, String> data = new Dictionary<string, string>();
                data["IdUser"] = "" + id;
                Superviseur superviseur = new Superviseur();
                superviseur = superviseur.find_by_id(data);
                superviseur.remove(data);
                return RedirectToAction("Index");

            }
            catch
            {
                return View();
            }
        }

    }
}
