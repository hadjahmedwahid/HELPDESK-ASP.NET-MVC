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
    public class CategorieController : Controller
    {
        // GET: Categorie
        public ActionResult Index()
        {
            if (Session["user"] == null)
                return RedirectToAction("Index", "Home");
            if ((Session["user"] as Utilisateur).EtatUser != "Superviseur")
                return RedirectToAction("InvalidAccess", "Home");

            Categorie categorie = new Categorie();
            return View(categorie);
        }

        // GET: Categorie/Details/5
        public ActionResult Details(int? id)
        {
            if (Session["user"] == null)
                return RedirectToAction("Index", "Home");

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Dictionary<String, String> data = new Dictionary<string, string>();
            data["IdCategorie"] = "" + id;
            Categorie categorie = new Categorie();
            categorie = categorie.find_by_id(data);

            if (categorie == null)
            {
                return HttpNotFound();
            }
            return View(categorie);
        }

        // GET: Categorie/Create
        public ActionResult Create()
        {
            if (Session["user"] == null)
                return RedirectToAction("Index", "Home");
            if ((Session["user"] as Utilisateur).EtatUser != "Superviseur")
                return RedirectToAction("InvalidAccess", "Home");

            return View();
        }

        // POST: Categorie/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdCategorie,NomCategorie,IdParent,DescCategorie")] Categorie categorie,Categorie parentCategorie)
        {
            if (Session["user"] == null)
                return RedirectToAction("Index", "Home");
            if ((Session["user"] as Utilisateur).EtatUser != "Superviseur")
                return RedirectToAction("InvalidAccess", "Home");

            try
            {
                if (ModelState.IsValid)
                {
                    categorie.ParentCategorie = parentCategorie;
                    categorie.save();
                    return RedirectToAction("index");
                }
                
            }
            catch
            {

            }
            
            return View();
        }

        // GET: Categorie/Edit/5
        public ActionResult Edit(int? id)
        {
            if (Session["user"] == null)
                return RedirectToAction("Index", "Home");
            if ((Session["user"] as Utilisateur).EtatUser != "Superviseur")
                return RedirectToAction("InvalidAccess", "Home");

            Categorie categorie = new Categorie();
            
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Dictionary<String, String> data = new Dictionary<string, string>();
            data["IdCategorie"] = "" + id;
            
            categorie = categorie.find_by_id(data);
            if (categorie == null)
            {
                return HttpNotFound();
            }
            return View(categorie);

        }

        // POST: Categorie/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "IdCategorie,NomCategorie,IdParent,DescCategorie")] Categorie categorie,Categorie parentCategorie)
        {
            if (Session["user"] == null)
                return RedirectToAction("Index", "Home");
            if ((Session["user"] as Utilisateur).EtatUser != "Superviseur")
                return RedirectToAction("InvalidAccess", "Home");

            try
            {
                if (ModelState.IsValid)
                {
                    categorie.ParentCategorie = parentCategorie;
                    categorie.edit_by_id();
                    return RedirectToAction("Index");
                }
                return View(categorie);
            }
            catch
            {
                return View();
            }
        }

        // GET: Categorie/Delete/5
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
            data["IdCategorie"] = "" + id;
            Categorie categorie = new Categorie();
            categorie = categorie.find_by_id(data);
            if (categorie == null)
            {
                return HttpNotFound();
            }
            return View(categorie);

        }

        // POST: Categorie/Delete/5
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
                data["IdCategorie"] = "" + id;
                Categorie categorie = new Categorie();
                categorie = categorie.find_by_id(data);
                categorie.remove(data);
                return RedirectToAction("Index");

            }
            catch
            {
                return View();
            }
        }
    }
}
