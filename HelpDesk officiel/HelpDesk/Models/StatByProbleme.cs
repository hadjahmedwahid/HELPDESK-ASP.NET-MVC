using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace HelpDesk.Models
{
    public class StatByProbleme : HelpDeskDB
    {

        public Probleme probleme { get; set; }

        [Display(Name = "Nombre de fois")]
        public int Nbp { get; set; }
        [Display(Name = "Donner La Date De Recherch sous Form DD/MM/YY :")]
        public String date { get; set; }
        public int IdTicket { get; set; }



        public StatByProbleme()
        {



        }
        public StatByProbleme(Probleme p, int nb)
        {
            this.probleme = p;
            this.Nbp = nb;



        }



        public SqlDataReader select(String S)
        {

            string query = "SELECT  [IdProbleme],count([IdTicket]) FROM [Ticket_Probleme] where date_p like '%"+S+"%' group by  [IdProbleme]";



            SqlCommand cmd = new SqlCommand(query, this.connexion);
            SqlDataReader result = cmd.ExecuteReader();
            return result;

        }

        public List<StatByProbleme> find_all(String dat)
        {
            Probleme p = new Probleme();


            Dictionary<string, string> id2 = new Dictionary<string, string>();
            Dictionary<string, string> id3 = new Dictionary<string, string>();

            List<StatByProbleme> stat = new List<StatByProbleme>();

            this.connect();
            SqlDataReader result = this.select(dat);
            while (result.Read())
            {

                id2["IdProbleme"] = "" + result.GetInt32(0);
                p=p.find_by_id(id2);




                stat.Add(new StatByProbleme(
                    p,
                    result.GetInt32(1)
                    ));
            }
            return stat;
        }

        public void save()
        {
            this.connect();
            this.table = "[dbo].[Ticket_Probleme]";
            Dictionary<string, string> data = new Dictionary<string, string>();

            data["[IdTicket]"] = "" + this.IdTicket;
            data["[IdProbleme]"] = "" + this.probleme.IdProbleme;
            data["[date_p]"] = "'" + this.date + "'";
            

            this.insert(data);
            this.disconnect();

        }
    }
}