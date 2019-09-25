using HelpDesk.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HelpDesk.Controllers
{
    public class StatByTicketController : Controller
    {
        // GET: StatByTicket
        public ActionResult Index()
        {
            ViewBag.date = "" + DateTime.Now.Year;
            StatByTicket s = new StatByTicket();
            return View(s);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(StatByTicket s)
        {
            
            ViewBag.date = s.date;
            return View(s);
        }


    }
}
