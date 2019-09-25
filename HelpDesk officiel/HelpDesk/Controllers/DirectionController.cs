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
    public class DirectionController : Controller
    {
        // GET: Direction
        public ActionResult Index()
        {
            if (Session["user"] == null)
                return RedirectToAction("Index", "Home");
            if ((Session["user"] as Utilisateur).EtatUser != "Superviseur")
                return RedirectToAction("InvalidAccess", "Home");

            Direction direction = new Direction();
            return View(direction);
        }

        // GET: Direction/Details/5
        public ActionResult Details(int? id)
        {
            if (Session["user"] == null)
                return RedirectToAction("Index", "Home");

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Dictionary<String, String> data = new Dictionary<string, string>();
            data["IdDirection"] = "" + id;
            Direction direction = new Direction();
            direction = direction.find_by_id(data);


            if (direction == null)
            {
                return HttpNotFound();
            }
            return View(direction);
        }

        // GET: Direction/Create
        public ActionResult Create()
        {
            if (Session["user"] == null)
                return RedirectToAction("Index", "Home");
            if ((Session["user"] as Utilisateur).EtatUser != "Superviseur")
                return RedirectToAction("InvalidAccess", "Home");

            return View();
        }

        // POST: Direction/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdDirection,NomDirection,Libelle")] Direction direction)
        {
            if (Session["user"] == null)
                return RedirectToAction("Index", "Home");
            if ((Session["user"] as Utilisateur).EtatUser != "Superviseur")
                return RedirectToAction("InvalidAccess", "Home");

            try
            {
                if (ModelState.IsValid)
                {
                    direction.save();
                    return RedirectToAction("index");
                }
                
            }
            catch
            {

            }
            
            return View();
        }

        // GET: Direction/Edit/5
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
            data["IdDirection"] = "" + id;
            Direction direction = new Direction();
            direction = direction.find_by_id(data);
            if (direction == null)
            {
                return HttpNotFound();
            }
            return View(direction);
        }

        // POST: Direction/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdDirection,NomDirection,Libelle")] Direction direction)
        {
            if (Session["user"] == null)
                return RedirectToAction("Index", "Home");
            if ((Session["user"] as Utilisateur).EtatUser != "Superviseur")
                return RedirectToAction("InvalidAccess", "Home");

            try
            {
                direction.edit_by_id();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Direction/Delete/5
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
            data["IdDirection"] = "" + id;
            Direction direction = new Direction();
            direction = direction.find_by_id(data);
            if (direction == null)
            {
                return HttpNotFound();
            }
            return View(direction);
        }

        // POST: Direction/Delete/5
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
                data["IdDirection"] = "" + id;
                Direction direction = new Direction();
                direction = direction.find_by_id(data);
                direction.remove(data);
                return RedirectToAction("Index");

            }
            catch
            {
                return View();
            }
        }
    }
}
