using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace HelpDesk.Models
{
    public class StatByTicket : HelpDeskDB
    {
        [Display(Name = "Etat De Ticket ")]
        public String etatticket { get; set; }
        public int NbTick { get; set; }
        [Display(Name = "Donner La Date De Recherch sous Form DD/MM/YY :")]
        public String date { get; set; }



        public StatByTicket()
        {



        }
        public StatByTicket(String T, int nb)
        {
            this.etatticket = T;
            this.NbTick = nb;



        }



        public SqlDataReader select(String S)
        {

            string query = "SELECT count( [IdTicket]) ,[EtatTicket] FROM [Ticket] where CONVERT(VARCHAR(10), DateOuverture, 103) like '%"+S+"%' group by [EtatTicket]";



            SqlCommand cmd = new SqlCommand(query, this.connexion);
            SqlDataReader result = cmd.ExecuteReader();
            return result;

        }

        public List<StatByTicket> find_all(String dat)
        {
            Ticket tick = new Ticket();


            Dictionary<string, string> id2 = new Dictionary<string, string>();
            Dictionary<string, string> id3 = new Dictionary<string, string>();

            List<StatByTicket> stat = new List<StatByTicket>();

            this.connect();
            SqlDataReader result = this.select(dat);
            while (result.Read())
            {

              




                stat.Add(new StatByTicket(
                    result.GetString(1),
                    result.GetInt32(0)
                    ));
            }
            return stat;
        }



















    }
}