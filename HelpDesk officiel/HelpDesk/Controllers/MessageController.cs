using HelpDesk.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HelpDesk.Controllers
{
    public class MessageController : Controller
    {
        // GET: Message
        public ActionResult Index()
        {
           return View();
        }

        // GET: Message/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Message/Create
        public ActionResult Create()
        {
            if (Session["user"] == null)
                return RedirectToAction("Index", "Home");
            return View();
        }

        // POST: Message/Create
        [HttpPost]
        public ActionResult Create(Message message)
        {
            if (Session["user"] == null)
                return RedirectToAction("Index", "Home");
            try
            {
                if (ModelState.IsValid)
                {
                    message.DateMessage = DateTime.Now;
                    message.Envoyeur = (Session["user"] as Utilisateur);
                    message.EtatMessage = "non lue";
                    message.Ticket = new Ticket();
                    message.Ticket.IdTicket = 31;
                    message.save();
                }

                    return RedirectToAction("Index");
           }
            catch
            {
                return View();
            }
        }

        // GET: Message/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Message/Edit/5
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

        // GET: Message/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Message/Delete/5
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

        public ActionResult GetMessages()
        {
            Message message = new Message(); 
            return PartialView("_MessagesList", message.GetAllMessages());
        }

        public ActionResult Messenger()
        {
            Message message = new Message();
            return PartialView("_Messenger", message.GetAllMessages());
        }

        public ActionResult GetMessagesNotification()
        {
            Message message = new Message();
            return PartialView("_NotificationMessages", message.GetAllMessages());
            
        }
    }
}
