using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace HelpDesk.Models
{
    public class allobjet : HelpDeskDB
    {
        public Agence agence { get; set; }
        public DRE dre { get; set; }
        public Categorie categorie { get; set; }
         public Ticket ticket { get; set; }
     public Probleme probleme { get; set; }
     public Utilisateur user { get; set; }
     public TechnicienHelpDesk techhelp { get; set; }
     public TechnicienDre techdre { get; set; }

     public Direction direction { get; set; }
        public EmployeAgence employer { get; set; }
        public Statistic stat{ get; set; }
        public Ticket_Historique ticket_his { get; set; }
        public Solution solution { get; set; }
        public StatByProbleme StatByProbleme { get; set; }
        public StatByTechnicien StatByTechnicien { get; set; }
        public StatByTicket StatByTicket { get; set; }
        public Superviseur super { get; set; }
        public String day { get; set; }
        public String month { get; set; }
        public String year { get; set; }


        public allobjet()
        {

            agence = new Agence();
            dre = new DRE();
            categorie = new Categorie();
            ticket = new Ticket();
            probleme = new Probleme();
            user = new Utilisateur();
            techhelp = new TechnicienHelpDesk();
            techdre = new TechnicienDre();
            direction = new Direction();
            employer = new EmployeAgence();
            stat = new Statistic();
            ticket_his = new Ticket_Historique();
            solution = new Solution();
            super = new Superviseur();
            StatByTicket = new StatByTicket();
            StatByTechnicien = new StatByTechnicien();
            StatByProbleme = new StatByProbleme();
            day = null;
            month = null;
            year = null;





        }
        public List<String> dayliste()
        {
            List<String> list = new List<String>();
            
            for(int i = 1; i < 32; i++)
            {

                if (i < 10) { list.Add("0" + i); } else { list.Add("" + i); }
            }
            return list;
        }
        public List<String> monthliste()
        {
            List<String> list = new List<String>();
            for (int i = 1; i < 13; i++)
            {

                if (i < 10) { list.Add("0"+i); } else { list.Add(""+i); }
            }
            return list;
        }
        public List<String> yearliste()
        {
            List<String> list = new List<String>();
            for (int i = 2000; i < 2500; i++)
            {

                list.Add(""+i);
            }
            return list;
        }
        public void save(String ch)
        {
            this.connect();
            this.table = "[dbo].[datstat]";

            Dictionary<string, string> data = new Dictionary<string, string>();


         
            data["dates"] = "'" + ch + "'";
         


            this.insert(data);
            this.disconnect();

        }

        public String find()
        {
            String ch= "";
            this.table = "[datstat]";
            this.connect();
            SqlDataReader result = this.select();
            while (result.Read())
            {
                ch = result.GetString(1);
            }




            String query = "DELETE FROM [dbo].[datstat] WHERE idd >0 ";
           

            SqlCommand cmd = new SqlCommand(query, this.connexion);
            this.disconnect();
            return  ch;
        }

    }
}