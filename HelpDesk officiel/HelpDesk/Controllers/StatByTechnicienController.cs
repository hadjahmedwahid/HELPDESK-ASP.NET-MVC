using HelpDesk.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace HelpDesk.Controllers
{
    public class StatByTechnicienController : Controller
    {
        // GET: StatByTechnicien
        public ActionResult Index()
        {
            StatByTechnicien s = new StatByTechnicien();
           
            ViewBag.date = "" + DateTime.Now.Year;

            return View(s);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index( StatByTechnicien s)
        {
            
            ViewBag.date = s.date;
            return View(s);
        }

       public  ActionResult chart()
        {
           

            String mytheme = @"
                <Chart BackColor = ""Transparent"">

        <ChartAreas><ChartArea Name=""Defult"" BackColor=""Transparent""></ChartArea>
        </ChartAreas>

                </Chart>";
            StatByTechnicien s = new StatByTechnicien();
            List<String> name = new List<string>();
            foreach (var i in s.find_all("2017"))
            {
                name.Add(i.tichnicien.Nom);

            }
            List<int> nb = new List<int>();
            foreach (var i in s.find_all("2017"))
            {
                nb.Add(i.NbTick);

            }

            new Chart(width: 500, height: 500, theme: mytheme).AddSeries(

                chartType: "pie",
                xValue: name,
                yValues: nb
                ).Write("png");





            return null;
        }


        public ActionResult chart1()
        {
          

            StatByTechnicien s = new StatByTechnicien();
            List<String> name = new List<string>();
            foreach (var i in s.find_all("2017"))
            {
                name.Add(i.tichnicien.Nom);

            }
            List<int> nb = new List<int>();
            foreach (var i in s.find_all("2017"))
            {
                nb.Add(i.NbTick);

            }

            new Chart(width: 500, height: 500).AddSeries(

                chartType: "column",
                xValue: name,
                yValues: nb
                ).Write("png");






            return null;
        }
    }
}
