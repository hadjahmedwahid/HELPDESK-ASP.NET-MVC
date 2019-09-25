using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HelpDesk.Models;
using System.Web.Helpers;

namespace HelpDesk.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            if (Session["user"] == null)
                return RedirectToAction("Login", "Utilisateur");
            ViewBag.date = "" + DateTime.Now.Year;

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

            if ((Session["user"] as Utilisateur).EtatUser == "employerAgence")
            {
                Dictionary<String, String> data = new Dictionary<string, string>();
                data["IdUser"] = "" + (Session["user"] as Utilisateur).IdUser;
                Ticket t = new Ticket();
                return View("ticket_em", t);
                //  Utilisateur technicienHelpDesk = new Utilisateur();
                //  Session["user"] = technicienHelpDesk.find_by_id(data);
            }
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult InvalidAccess()
        {
            return View();
        }

        public ActionResult charttechniciencolumn()
        {


            


            String id ="" + DateTime.Now.Year;





            StatByTechnicien s = new StatByTechnicien();
            List<String> name = new List<string>();



            foreach (var i in s.find_all(id))
            {
                name.Add(i.tichnicien.Nom);

            }
            List<int> nb = new List<int>();
            foreach (var i in s.find_all(id))
            {
                nb.Add(i.NbTick);

            }

            new Chart(width: 350, height: 350).AddSeries(

                chartType: "column",
                xValue: name,
                yValues: nb
                ).Write("png");






            return null;
        }

        public ActionResult tick()
        {


           

            String id = ""  + DateTime.Now.Year;

            StatByTicket s = new StatByTicket();
            List<String> name = new List<string>();
            if (s.find_all(id) == null) { ViewBag.val1 = 0; }
            foreach (var i in s.find_all(id))
            {
                name.Add(i.etatticket);

            }
            List<int> nb = new List<int>();
            foreach (var i in s.find_all(id))
            {
                nb.Add(i.NbTick);

            }



            new Chart(width: 400, height: 400).AddSeries(

                chartType: "bar",
                xValue: name,
                yValues: nb
                ).Write("png");






            return null;
        }

        public ActionResult propie()
        {
            

          
            String id = "" + DateTime.Now.Year;

            StatByProbleme s = new StatByProbleme();
            List<String> name = new List<string>();
            foreach (var i in s.find_all(id))
            {
                name.Add(i.probleme.ObjetProbleme);

            }
            List<int> nb = new List<int>();
            foreach (var i in s.find_all(id))
            {
                nb.Add(i.Nbp);

            }

             new Chart(width: 400, height: 400).AddSeries(

                chartType: "doughnut",
                
                xValue: name,
                yValues: nb
                ).Write("bmp");

            




            return null;
        }

    }
}