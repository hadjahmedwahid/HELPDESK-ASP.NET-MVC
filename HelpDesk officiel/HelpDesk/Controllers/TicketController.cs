using HelpDesk.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using static System.Net.Mime.MediaTypeNames;

namespace HelpDesk.Controllers
{
    public class TicketController : Controller
    {

        // GET: Ticket
        public ActionResult Index()
        {
            if (Session["user"] == null)
                return RedirectToAction("Index", "Home");
            if ((Session["user"] as Utilisateur).EtatUser == "employerAgence")
                return RedirectToAction("InvalidAccess", "Home");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(Ticket ticket)
        {
            Dictionary<string, string> data = new Dictionary<string, string>();
            data["Objet"] = ticket.Objet;
            data["Description"] = ticket.Objet;

            return View("Search", ticket.find_by_search(data));
        }


        // GET: Ticket/Details/5
        public ActionResult Details(int? id)
        {
            if (Session["user"] == null)
                return RedirectToAction("Index", "Home");

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            // Get the Ticket by id :
            Dictionary<String, String> data = new Dictionary<string, string>();
            data["IdTicket"] = "" + id;

            Ticket ticket = new Ticket();
            ticket = ticket.find_by_id(data);

            // Verify ticket exsistance
            if (ticket == null)
            {
                return HttpNotFound();
            }
            if (ticket.EtatTicket != "En cours" && ticket.EtatTicket != "Cloturé")
            {
                ticket.EtatTicket = "En attente";
                ticket.edit_by_id();
            }
            // Save ticket in the history :
            Ticket_Historique ticket_historique = new Ticket_Historique();
            ticket_historique.Date = DateTime.Now;
            ticket_historique.Ticket = ticket;

            Utilisateur technicienHD = new Utilisateur();
            Dictionary<String, String> data2 = new Dictionary<string, string>();

            data2["IdUser"] = "" + (Session["user"] as Utilisateur).IdUser;
            technicienHD = technicienHD.find_by_id(data2);

            ticket_historique.Technicien = technicienHD;
            ticket_historique.Etat = "En attente";
            ticket_historique.save();

            return View(ticket);
        }

        public ActionResult solv()
        {

            if (Session["user"] == null)
                return RedirectToAction("Index", "Home");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult solv(Probleme probleme , Solution solution)
        {

            if (Session["user"] == null)
                return RedirectToAction("Index", "Home");

            Ticket ticket = (Session["ticket"] as Ticket);

            if (ticket.EtatTicket == "Cloturé")
                return View("Error");

            ticket.DateFermeture = DateTime.Now;
            ticket.EtatTicket = "Cloturé";
            ticket.edit_by_id();

            ticket.save(probleme);

            solution.probleme = probleme;
            solution.save();

            Dictionary<string, string> id = new Dictionary<string, string>();
            id["[DescriptionSolution]"] = "'" + solution.Description + "'";
            solution = solution.find_by_id(id);
            if(solution!= null)
            ticket.save(probleme, solution);


            Ticket_Historique ticket_historique = new Ticket_Historique();

            ticket_historique.Date = DateTime.Now;
            ticket_historique.Ticket = ticket;

            Utilisateur technicienHD = new Utilisateur();
            Dictionary<String, String> data = new Dictionary<string, string>();

            data["IdUser"] = "" + (Session["user"] as Utilisateur).IdUser;
            technicienHD = technicienHD.find_by_id(data);
            ticket_historique.Technicien = technicienHD;
            ticket_historique.Etat = "Cloturé";

            // Save Historique :
            ticket_historique.save();

            

            return RedirectToAction("Index","Home");
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Details(Ticket ticket)
        {
            if (Session["user"] == null)
                return RedirectToAction("Index", "Home");

            try
            {
                Dictionary<String, String> _data = new Dictionary<string, string>();
                _data["IdTicket"] = "" + ticket.IdTicket;

                ticket = ticket.find_by_id(_data);
                if (ticket.EtatTicket != "Cloturé")
                {
                    ticket.EtatTicket = "En cours";
                    ticket.edit_by_id();
                }
                Session["ticket"] = ticket;
              
     
                // TODO: Add update logic here

                Ticket_Historique ticket_historique = new Ticket_Historique();

                ticket_historique.Date = DateTime.Now;
                ticket_historique.Ticket = ticket;

                Utilisateur technicienHD = new Utilisateur();
                Dictionary<String, String> data = new Dictionary<string, string>();

                data["IdUser"] = "" + (Session["user"] as Utilisateur).IdUser;
                technicienHD = technicienHD.find_by_id(data);

                ticket_historique.Technicien = technicienHD;
                ticket_historique.Etat = "En cours";

                // Save Historique :
                ticket_historique.save();

                return RedirectToAction("Resoudre");
            }
            catch
            {
                return View("Error");
            }
        }

        // GET: Ticket/Create
        public ActionResult Create()
        {
            if (Session["user"] == null)
                return RedirectToAction("Index", "Home");

            return View();
        }

        // POST: Ticket/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Ticket ticket, Categorie categorie)
        {
            if (Session["user"] == null)
                return RedirectToAction("Index", "Home");
            try
            {

                if (ModelState.IsValid)
                {
                    ticket.Expediteur = (Session["user"] as Utilisateur);
                    ticket.Categorie = categorie;
                    ticket.DateOuverture = DateTime.Now;
                    ticket.DateFermeture = DateTime.Now;
                    ticket.EtatTicket = "Nouveau";
                    // Save Ticket:
                    ticket.save();
                    return RedirectToAction("Index", "Home");
                }
            }
            catch
            {

            }

            return View();
        }















        public ActionResult routage(int? id)
        {
            if (Session["user"] == null)
                return RedirectToAction("Index", "Home");

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Dictionary<String, String> data = new Dictionary<string, string>();
            data["IdTicket"] = "" + id;
            Ticket d1 = new Ticket();
            d1 = d1.find_by_id(data);
            if (d1 == null)
            {
                return HttpNotFound();
            }
            return View(d1);

        }

        // POST: Ticket/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult routage(Ticket ticket, Categorie categorie)
        {
            if (Session["user"] == null)
                return RedirectToAction("Index", "Home");

            //try
            //{
                // TODO: Add update logic here
                ticket.Categorie = categorie;
                ticket.EtatTicket = "Nouveau";
                ticket.edit_by_id();
           // return Content(""+categorie.IdCategorie+" et   "+ticket.IdTicket);
               return RedirectToAction("Index");
            //}
            //catch
            //{
            //    return View();
            //}
        }








































        // GET: Ticket/Edit/5
        public ActionResult Edit(int? id)
        {
            if (Session["user"] == null)
                return RedirectToAction("Index", "Home");

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Dictionary<String, String> data = new Dictionary<string, string>();
            data["IdTicket"] = "" + id;
            Ticket d1 = new Ticket();
            d1 = d1.find_by_id(data);
            if (d1 == null)
            {
                return HttpNotFound();
            }
            return View(d1);

        }

        // POST: Ticket/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Ticket ticket, Categorie categorie)
        {
            if (Session["user"] == null)
                return RedirectToAction("Index", "Home");

            try
            {
                // TODO: Add update logic here
                ticket.Categorie = categorie;
                ticket.EtatTicket = "Nouveau";
                ticket.edit_by_id();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Ticket/Delete/5
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
            data["IdTicket"] = "" + id;
            Ticket ticket = new Ticket();
            ticket = ticket.find_by_id(data);
            if (ticket == null)
            {
                return HttpNotFound();
            }
            return View(ticket);
        }

        // POST: Ticket/Delete/5
        [HttpPost]
        public ActionResult Delete(int? id, FormCollection collection)
        {
            if (Session["user"] == null)
                return RedirectToAction("Index", "Home");
            if ((Session["user"] as Utilisateur).EtatUser != "Superviseur")
                return RedirectToAction("InvalidAccess", "Home");

            try
            {
                Dictionary<String, String> data = new Dictionary<string, string>();
                data["IdTicket"] = "" + id;

                Ticket ticket = new Ticket();
                ticket.find_by_id(data);
                ticket.remove(data);
                return RedirectToAction("Index");

            }
            catch
            {
                return View();
            }
        }

        public PartialViewResult newTickets()
        {
            Ticket ticket = new Ticket();
            return PartialView("NewTicket", ticket);
        }

        public ActionResult GetTickets()
        {
            if ((Session["user"] as Utilisateur).EtatUser == "Superviseur")
            {
                Dictionary<String, String> data = new Dictionary<string, string>();
                data["IdUser"] = "" + (Session["user"] as Utilisateur).IdUser;
                Superviseur superviseur = new Superviseur();
                Session["user"] = superviseur.find_by_id(data);
            }
            if ((Session["user"] as Utilisateur).EtatUser == "TechnicienHelpdesk")
            {
                Dictionary<String, String> data = new Dictionary<string, string>();
                data["IdUser"] = "" + (Session["user"] as Utilisateur).IdUser;
                TechnicienHelpDesk technicienHelpDesk = new TechnicienHelpDesk();
                Session["user"] = technicienHelpDesk.find_by_id(data);
            }

            Ticket ticket = new Ticket();
            return PartialView("_TicketsList", ticket.GetAllTickets());
        }

        public ActionResult GetMyTickets()
        {
            if ((Session["user"] as Utilisateur).EtatUser == "Superviseur")
            {
                Dictionary<String, String> data = new Dictionary<string, string>();
                data["IdUser"] = "" + (Session["user"] as Utilisateur).IdUser;
                Superviseur superviseur = new Superviseur();
                Session["user"] = superviseur.find_by_id(data);
            }
            if ((Session["user"] as Utilisateur).EtatUser == "TechnicienHelpdesk")
            {
                Dictionary<String, String> data = new Dictionary<string, string>();
                data["IdUser"] = "" + (Session["user"] as Utilisateur).IdUser;
                TechnicienHelpDesk technicienHelpDesk = new TechnicienHelpDesk();
                Session["user"] = technicienHelpDesk.find_by_id(data);
            }

            Ticket ticket = new Ticket();
            return PartialView("_MyTicketsList", ticket.GetAllTickets());
        }

        public ActionResult GetTicketsNotifications()
        {
            if ((Session["user"] as Utilisateur).EtatUser == "Superviseur")
            {
                Dictionary<String, String> data = new Dictionary<string, string>();
                data["IdUser"] = "" + (Session["user"] as Utilisateur).IdUser;
                Superviseur superviseur = new Superviseur();
                Session["user"] = superviseur.find_by_id(data);
            }
            if ((Session["user"] as Utilisateur).EtatUser == "TechnicienHelpdesk")
            {
                Dictionary<String, String> data = new Dictionary<string, string>();
                data["IdUser"] = "" + (Session["user"] as Utilisateur).IdUser;
                TechnicienHelpDesk technicienHelpDesk = new TechnicienHelpDesk();
                Session["user"] = technicienHelpDesk.find_by_id(data);
            }

            //TicketRepository _ticketRepository = new TicketRepository();
            Ticket ticket = new Ticket();
            return PartialView("_Notification", ticket.GetAllTickets());
        }

        public ActionResult GetTicketsNotificationsList()
        {
            if ((Session["user"] as Utilisateur).EtatUser == "Superviseur")
            {
                Dictionary<String, String> data = new Dictionary<string, string>();
                data["IdUser"] = "" + (Session["user"] as Utilisateur).IdUser;
                Superviseur superviseur = new Superviseur();
                Session["user"] = superviseur.find_by_id(data);
            }
            if ((Session["user"] as Utilisateur).EtatUser == "TechnicienHelpdesk")
            {
                Dictionary<String, String> data = new Dictionary<string, string>();
                data["IdUser"] = "" + (Session["user"] as Utilisateur).IdUser;
                TechnicienHelpDesk technicienHelpDesk = new TechnicienHelpDesk();
                Session["user"] = technicienHelpDesk.find_by_id(data);
            }

            //TicketRepository _ticketRepository = new TicketRepository();
            Ticket ticket = new Ticket();
            return PartialView("_NotificationList", ticket.GetAllTickets());
        }

        // GET: Ticket/Resoudre/5
        public ActionResult Resoudre()
        {
            if (Session["user"] == null)
                return RedirectToAction("Index", "Home");
            Message m = new Message();

            Dictionary<string, string> id = new Dictionary<string, string>();
            id["IdTicket"] = ""+ (Session["ticket"] as Ticket).IdTicket;

            m.EtatMessage = "lue";
            
           
            return View();
        }

        // Post: Ticket/Resoudre/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Resoudre(Message message)
        {
            try
            {
            if (ModelState.IsValid)
                {
                    message.DateMessage = DateTime.Now;
                    message.Envoyeur = (Session["user"] as Utilisateur);
                    message.EtatMessage = "non lue";
                    message.Ticket = (Session["ticket"] as Ticket);
                    message.save();
                }

                return View();
            }
            catch
            {
                return View();
            }
        }

        // GET: Probleme/Create
        public ActionResult CreateProbleme()
        {
            if (Session["user"] == null)
                return RedirectToAction("Index", "Home");
            if ((Session["user"] as Utilisateur).EtatUser == "EmployeAgence")
                return RedirectToAction("InvalidAccess", "Home");
            return View();
        }

        // POST: Probleme/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateProbleme([Bind(Include = "IdProbleme,ObjetProbleme,DescreptionProbleme,IdCategorie")] Probleme probleme, Categorie categorie)
        {
            if (Session["user"] == null)
                return RedirectToAction("Index", "Home");
            if ((Session["user"] as Utilisateur).EtatUser == "EmployeAgence")
                return RedirectToAction("InvalidAccess", "Home");

            try
            {
                probleme.categorie = categorie;
                probleme.save();

            }
            catch
            {
                
            }
            if (ModelState.IsValid)
            {
                return RedirectToAction("solv", "Ticket");
            }
            return RedirectToAction("solv","Ticket");
        }

    }
}
