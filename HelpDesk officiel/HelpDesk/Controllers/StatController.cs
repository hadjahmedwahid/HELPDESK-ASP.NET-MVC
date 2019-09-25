using HelpDesk.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace HelpDesk.Controllers
{
    public class StatController : Controller
    {
        // GET: Stat
        public ActionResult Index()
        {
            allobjet o = new allobjet();
            ViewBag.date = "" + DateTime.Now.Year;
            


            return View(o);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(allobjet o )
        {
            
            ViewBag.date = o.StatByTechnicien.date;
            String date;

            if ((o.day == null) && (o.month == null)) { date = o.year; } else { if (o.day == null) { date = o.month + "/" + o.year; } else { date = o.day + "/" + o.month + "/" + o.year; } }
            ViewBag.date = date;
            
            o.save(date);

            String id = date;
            
            StatByProbleme s = new StatByProbleme();
            
            ViewBag.val2 = 1;

            if (s.find_all(id).Count == 0) {

                ViewBag.val2 = 0; }

            ViewBag.val3 = 1;
            StatByTechnicien s1 = new StatByTechnicien();
            if (s1.find_all(id).Count == 0)
            {

                ViewBag.val3 = 0;
            }

            ViewBag.val4 = 1;

            StatByTicket s2 = new StatByTicket();
            if (s2.find_all(id).Count == 0)
            {

                ViewBag.val4 = 0;
            }



            return View(o);


        }




        public ActionResult charttechnicienpie()
        {
            allobjet o = new allobjet();


            String id = o.find();
            

           
            StatByTechnicien s = new StatByTechnicien();
            List<String> name = new List<string>();
            Ticket t = new Ticket();
            int pors = 0;



            foreach (var i in s.find_all(id))
            {
                 pors = pors + i.NbTick;
                

            }

            foreach (var i in s.find_all(id))
            {
               int  p = (i.NbTick * 100) / pors;
                name.Add(i.tichnicien.Nom+""+p+"%");

            }
            List<int> nb = new List<int>();
            foreach (var i in s.find_all(id))
            {
                nb.Add(i.NbTick);

            }

            new Chart(width: 350, height: 350).AddSeries(

                chartType: "pie",
                xValue: name,
                yValues: nb
                ).Write("png");





            return null;
        }


        public ActionResult charttechniciencolumn()
        {


            allobjet o = new allobjet();
            
            
            String id = o.find();

         



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

        public ActionResult TicketPie()
        {
            allobjet o = new allobjet();

            String mytheme = @"
                <Chart BackColor = ""Transparent"">

        <ChartAreas><ChartArea Name=""Defult"" BackColor=""Transparent""></ChartArea>
        </ChartAreas>

                </Chart>";
            String id = o.find();
            
            StatByTicket s = new StatByTicket();
            List<String> name = new List<string>();

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

                chartType: "pie",
                xValue: name,
                yValues: nb
                ).Write("png");






            return null;
        }
        public ActionResult tick()
        {
            allobjet o = new allobjet();


            String id = o.find();

           
            StatByTicket s = new StatByTicket();
            List<String> name = new List<string>();
            
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

                chartType: "column",
                xValue: name,
                yValues: nb
                ).Write("png");






            return null;
        }

        public ActionResult propie()
        {
            allobjet o = new allobjet();

            String mytheme = @"
                <Chart BackColor = ""Transparent"">

        <ChartAreas><ChartArea Name=""Defult"" BackColor=""Transparent""></ChartArea>
        </ChartAreas>

                </Chart>";
            String id = o.find();
           
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

                chartType: "pie",
                xValue: name,
                yValues: nb
                ).Write("png");






            return null;
        }
        public ActionResult pro()
        {
            allobjet o = new allobjet();


            String id = o.find();
           
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
            if (s.find_all(id).Count()==0) { ViewBag.val2 = 0; }
            new Chart(width: 400, height: 400).AddSeries(

                chartType: "column",
                xValue: name,
                yValues: nb
                ).Write("png");






            return null;
        }












    }



}