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
    public class UtilisateurController : Controller
    {
        // GET: Utilisateur
        public ActionResult Index()
        {
            if (Session["user"] == null)
                return RedirectToAction("Index", "Home");
            if ((Session["user"] as Utilisateur).EtatUser != "Superviseur")
                return RedirectToAction("InvalidAccess", "Home");


            Utilisateur utilisateur = new Utilisateur();
            return View(utilisateur);
        }

        // GET: Utilisateur/Details/5
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
            Utilisateur utilisateur = new Utilisateur();
            utilisateur = utilisateur.find_by_id(data);

            if (utilisateur == null)
            {
                return HttpNotFound();
            }
            return View(utilisateur);
            
        }

        // GET: Utilisateur/Create
        public ActionResult Create()
        {
            if (Session["user"] == null)
                return RedirectToAction("Index", "Home");
            if ((Session["user"] as Utilisateur).EtatUser != "Superviseur")
                return RedirectToAction("InvalidAccess", "Home");

            return View();
        }

        // POST: Utilisateur/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdUser,Login,Password,EtatUser,Nom,Prenom,Email,Phone")] Utilisateur utilisateur)
        {
            if (Session["user"] == null)
                return RedirectToAction("Index", "Home");
            if ((Session["user"] as Utilisateur).EtatUser != "Superviseur")
                return RedirectToAction("InvalidAccess", "Home");


            try
            {
                if (ModelState.IsValid)
                {
                    utilisateur.save();
                    return RedirectToAction("index");
                }
                
            }
            catch
            {

            }
           
            return View();
    }

        // GET: /Utilisateur/Login
        [AllowAnonymous]
        public ActionResult Login()
        {
            if (Session["user"] != null)
                return RedirectToAction("Index", "Home");
            return View();
        }

        // POST: Utilisateur/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login([Bind(Include = "Email,Password")] Utilisateur utilisateur)
        {
            if (Session["user"] != null)
                return RedirectToAction("Index", "Home");

            try
            {
                Dictionary<String, String> id = new Dictionary<string, string>();
                id["Email"] = "'"+utilisateur.Email+"'";
                id["Password"] = "'"+utilisateur.Password+"'";
                Utilisateur user = utilisateur.find_by_id(id);
                if (user != null)
                {
                    Session["user"] = user;
                    return RedirectToAction("index","Home");
                }
            }
            catch
            {

            }
            
            return View();
        }

        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Index","Home");
        }

        // GET: Utilisateur/Edit/5
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
            Utilisateur utilisateur = new Utilisateur();
            utilisateur = utilisateur.find_by_id(data);
            if (utilisateur == null)
            {
                return HttpNotFound();
            }
            return View(utilisateur);

        }

        // POST: Utilisateur/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "IdUser,Login,Password,EtatUser,Nom,Prenom,Email,Phone")] Utilisateur utilisateur)
        {
            if (Session["user"] == null)
                return RedirectToAction("Index", "Home");
            if ((Session["user"] as Utilisateur).EtatUser != "Superviseur")
                return RedirectToAction("InvalidAccess", "Home");

            try
            {
                if (ModelState.IsValid)
                {
                    utilisateur.edit_by_id();
                    return RedirectToAction("Index");
                }
                return View(utilisateur);
            }
            catch
            {
                return View();
            }
        }

        // GET: Utilisateur/Delete/5
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
            Utilisateur utilisateur = new Utilisateur();
            utilisateur = utilisateur.find_by_id(data);
            if (utilisateur == null)
            {
                return HttpNotFound();
            }
            return View(utilisateur);
        }

        // POST: Utilisateur/Delete/5
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
                Utilisateur utilisateur = new Utilisateur();
                utilisateur = utilisateur.find_by_id(data);
                utilisateur.remove(data);
                return RedirectToAction("Index");

            }
            catch
            {
                return View();
            }
        }

    }
}
