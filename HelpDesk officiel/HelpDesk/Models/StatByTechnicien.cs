using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace HelpDesk.Models
{
    public class StatByTechnicien : HelpDeskDB
    {
        public Utilisateur tichnicien { get; set; }
        public int NbTick { get; set; }
        [Display(Name = "Date :")]
        public String date { get; set; }



        public StatByTechnicien()
        {
          


        }
        public StatByTechnicien(Utilisateur T , int nb)
        {
            this.tichnicien = T;
            this.NbTick = nb;



        }

       

        public SqlDataReader select(String S)
        {
            
            string query = "SELECT  [IdUser],count(IdTicket) as nb FROM [dbo].[Technicien_Ticket] where Date like '%" + S+ "%' group by IdUser";
               
            
            SqlCommand cmd = new SqlCommand(query, this.connexion);
            SqlDataReader result = cmd.ExecuteReader();
            return result;

        }

        public List<StatByTechnicien> find_all(String dat)
        {
            Utilisateur tech = new Utilisateur();

            
            Dictionary<string, string> id2 = new Dictionary<string, string>();
            Dictionary<string, string> id3 = new Dictionary<string, string>();

            List<StatByTechnicien> stat = new List<StatByTechnicien>();
            
            this.connect();
            SqlDataReader result = this.select(dat);
            while (result.Read())
            {

                id2["IdUser"] = "" + result.GetInt32(0);
               tech=tech.find_by_id(id2);
                stat.Add(new StatByTechnicien(
                    tech,
                    result.GetInt32(1)
                    ));
            }
            return stat;
        }
    }
}