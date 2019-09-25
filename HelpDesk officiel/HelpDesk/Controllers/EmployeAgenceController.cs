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
    public class EmployeAgenceController : Controller
    {
        // GET: EmployeAgence
        public ActionResult Index()
        {
            if (Session["user"] == null)
                return RedirectToAction("Index", "Home");
            if ((Session["user"] as Utilisateur).EtatUser != "Superviseur")
                return RedirectToAction("InvalidAccess", "Home");

            EmployeAgence employeAgence = new EmployeAgence();
            return View(employeAgence);
        }

        // GET: EmployeAgence/Details/5
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
            EmployeAgence employeAgence = new EmployeAgence();
            employeAgence = employeAgence.find_by_id(data);

            if (employeAgence == null)
            {
                return HttpNotFound();
            }
            return View();

        }


        // GET: EmployeAgence/Create
        public ActionResult Create()
        {
            if (Session["user"] == null)
                return RedirectToAction("Index", "Home");
            if ((Session["user"] as Utilisateur).EtatUser != "Superviseur")
                return RedirectToAction("InvalidAccess", "Home");
            
            return View();
        }

        // POST: EmployeAgence/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(EmployeAgence employeAgence, Agence agence)
        {
            if (Session["user"] == null)
                return RedirectToAction("Index", "Home");
            if ((Session["user"] as Utilisateur).EtatUser != "Superviseur")
                return RedirectToAction("InvalidAccess", "Home");

            try
            {
                employeAgence.agence = agence;
                employeAgence.save();
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

        // GET: EmployeAgence/Edit/5
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
            EmployeAgence employeAgence = new EmployeAgence();
            employeAgence = employeAgence.find_by_id(data);
            if (employeAgence == null)
            {
                return HttpNotFound();
            }
            return View(employeAgence);

        }

        // POST: EmployeAgence/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "IdUser,Login,Password,EtatUser,Nom,Prenom,Email,Phone,NumeroAgence")] EmployeAgence employeAgence, Agence agence)
        {
            if (Session["user"] == null)
                return RedirectToAction("Index", "Home");
            if ((Session["user"] as Utilisateur).EtatUser != "Superviseur")
                return RedirectToAction("InvalidAccess", "Home");

            try
            {
                if (ModelState.IsValid)
                {
                    employeAgence.agence = agence;
                    employeAgence.edit_by_id();
                    return RedirectToAction("Index");
                }
                return View(employeAgence);
            }
            catch
            {
                return View();
            }
        }

        // GET: EmployeAgence/Delete/5
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
            EmployeAgence employeAgence = new EmployeAgence();
            employeAgence = employeAgence.find_by_id(data);
            if (employeAgence == null)
            {
                return HttpNotFound();
            }
            return View(employeAgence);

        }

        // POST: EmployeAgence/Delete/5
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
            EmployeAgence employeAgence = new EmployeAgence();
            employeAgence = employeAgence.find_by_id(data);
            employeAgence.remove(data);
            return RedirectToAction("Index");

            }
            catch
            {
                return View();
            }
        }

    }
}
