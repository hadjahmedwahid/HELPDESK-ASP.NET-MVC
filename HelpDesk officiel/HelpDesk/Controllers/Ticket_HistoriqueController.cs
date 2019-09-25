using HelpDesk.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace HelpDesk.Controllers
{
    public class Ticket_HistoriqueController : Controller
    {
        // GET: Ticket_Historique
        public ActionResult Index()
        {
            Ticket_Historique t = new Ticket_Historique();
            return View(t);
        }

        // GET: Ticket_Historique/Details/5
        public ActionResult Details(int ? iduser,int  ? idticket , DateTime date)
        {

            if ((iduser == null) || (idticket == null) || (date == null))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Dictionary<String, String> data = new Dictionary<string, string>();
            data["IdUser"] = "" + iduser;
            data["IdTicket"] = "" + idticket;
            data["[Date]"] = "" + date;
            Ticket_Historique t = new Ticket_Historique();

           t=t.find_by_id(data);

            if (t == null)
            {
                return HttpNotFound();
            }
            return View(t);
            
        }

        // GET: Ticket_Historique/Create
        public ActionResult Create(int ? id) 
        {

            return View();
        }

        // POST: Ticket_Historique/Create
        [HttpPost]
        public ActionResult Create(Ticket_Historique t)
        {
            try
            {
                t.Date = DateTime.Now;
                t.save();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Ticket_Historique/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Ticket_Historique/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Ticket_Historique/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Ticket_Historique/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
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
