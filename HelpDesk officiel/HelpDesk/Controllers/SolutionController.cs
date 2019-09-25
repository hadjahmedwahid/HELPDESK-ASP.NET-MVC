using HelpDesk.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace HelpDesk.Controllers
{
    public class SolutionController : Controller
    {
        // GET: Solution
        public ActionResult Index()
        {
            if (Session["user"] == null)
                return RedirectToAction("Index", "Home");
            if ((Session["user"] as Utilisateur).EtatUser == "employerAgence")
                return RedirectToAction("InvalidAccess", "Home");

            Solution solution = new Solution();
            return View(solution);
        }

        // GET: Solution/Details/5
        public ActionResult Details(int? id)
        {
            if (Session["user"] == null)
                return RedirectToAction("Index", "Home");

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Dictionary<String, String> data = new Dictionary<string, string>();
            data["IdSolution"] = "" + id;
            Solution solution = new Solution();
            solution=solution.find_by_id(data);

            if (solution == null)
            {
                return HttpNotFound();
            }
            return View(solution);
        }

        // GET: Solution/Create
        public ActionResult Create()
        {
            if (Session["user"] == null)
                return RedirectToAction("Index", "Home");
            if ((Session["user"] as Utilisateur).EtatUser == "employerAgence")
                return RedirectToAction("InvalidAccess", "Home");
                      
           return View();
        }

        // POST: Solution/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Solution solution)
        {
            if (Session["user"] == null)
                return RedirectToAction("Index", "Home");
            if ((Session["user"] as Utilisateur).EtatUser == "employerAgence" )
                return RedirectToAction("InvalidAccess", "Home");

            try
            {
                if (ModelState.IsValid)
                {
                    solution.save();
                    return RedirectToAction("index");
                }                
            }
            catch
            {

            }
            
            return View();
        }

        // GET: Solution/Edit/5
        public ActionResult Edit(int? id)
        {
            if (Session["user"] == null)
                return RedirectToAction("Index", "Home");
            if ((Session["user"] as Utilisateur).EtatUser == "employerAgence")
                return RedirectToAction("InvalidAccess", "Home");
                    
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Dictionary<String, String> data = new Dictionary<string, string>();
            data["IdSolution"] = "" + id;
            Solution s = new Solution();
          s=s.find_by_id(data);
            if (s == null)
            {
                return HttpNotFound();
            }
            return View(s);
        }

        // POST: Solution/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Solution solution)
        {
            if (Session["user"] == null)
                return RedirectToAction("Index", "Home");
            if ((Session["user"] as Utilisateur).EtatUser == "employerAgence")
                return RedirectToAction("InvalidAccess", "Home");

            try
            {
                if (ModelState.IsValid)
                {
                    solution.edit_by_id();
                    return RedirectToAction("Index");
                }
                return View(solution);
            }
            catch
            {
                return View();
            }
        }

        // GET: Solution/Delete/5
        public ActionResult Delete(int ? id)
        {
            if (Session["user"] == null)
                return RedirectToAction("Index", "Home");
            if ((Session["user"] as Utilisateur).EtatUser == "employerAgence")
                return RedirectToAction("InvalidAccess", "Home");

            return View();
        }

        // POST: Solution/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            if (Session["user"] == null)
                return RedirectToAction("Index", "Home");
            if ((Session["user"] as Utilisateur).EtatUser == "employerAgence")
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
