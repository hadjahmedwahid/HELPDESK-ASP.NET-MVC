using HelpDesk.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HelpDesk.Controllers
{
    public class StatByProblemeController : Controller
    {
        public ActionResult Index()
        {
            StatByProbleme s = new StatByProbleme();
            ViewBag.date = "" + DateTime.Now.Year;
            return View(s);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(StatByProbleme s)
        {
            
            ViewBag.date = s.date;
            return View(s);
        }


    }
}
