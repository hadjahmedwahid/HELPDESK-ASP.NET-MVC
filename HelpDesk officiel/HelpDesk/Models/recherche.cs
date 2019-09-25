using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace HelpDesk.Models
{
    public class recherche : HelpDeskDB
    {
        [Display(Name = "Rechercher")]
        public String chaine { get; set; }
        public List<Ticket> tickets { get; set; }
        public List<Solution> solutions { get; set; }
        public List<Probleme> problemes { get; set; }

        public recherche()
        {
            tickets = new List<Ticket>();
            solutions = new List<Solution>();
            problemes = new List<Probleme>();



        }
        public recherche find(String s) {
            recherche r = new recherche();
            string query1 = "select * from ticket where [Objet] like '%"+s+"%' or [Description] like '%"+s+"%'";
            string query2 = "select * from Probleme where [ObjetProbleme] like '%"+s+"%' or[DescreptionProbleme] like '%"+s+"%'";
            string query3 = "select * from Solution where [DescriptionSolution] like '%"+s+"%'";

            this.connect();
            SqlCommand cmd1 = new SqlCommand(query1, this.connexion);
            SqlDataReader result1 = cmd1.ExecuteReader();

            r.tickets = this.find_ticket(result1);
            this.connect();
            SqlCommand cmd2 = new SqlCommand(query2, this.connexion);
            SqlDataReader result2 = cmd2.ExecuteReader();

            r.problemes = this.find_probleme(result2);
            this.connect();
            SqlCommand cmd3 = new SqlCommand(query3, this.connexion);
            SqlDataReader result3 = cmd3.ExecuteReader();

            r.solutions = this.find_solution(result3);

            this.disconnect();



            return r;
        }
        public List<Ticket> find_ticket(SqlDataReader sql)
        {
            Utilisateur utilisateur = new Utilisateur();
            Categorie c = new Categorie();

            Dictionary<string, string> id2 = new Dictionary<string, string>();
            Dictionary<string, string> id3 = new Dictionary<string, string>();

            List<Ticket> tickets = new List<Ticket>();

            this.connect();
            SqlDataReader result = sql;
            while (result.Read())
            {

                id2["IdUser"] = "" + result.GetInt32(7);
                utilisateur = utilisateur.find_by_id(id2);

                id3["[IdCategorie]"] = "" + result.GetInt32(6);
                c=c.find_by_id(id3);

                tickets.Add(new Ticket(
                    result.GetInt32(0),
                    result.GetDateTime(1),
                    result.GetDateTime(2),
                    result.GetString(3),
                    result.GetString(4),
                    result.GetString(5),
                    utilisateur,
                    c,
                    result.GetString(8)
                       ));
            }
            this.disconnect();
            return tickets;
        }

        public List<Solution> find_solution(SqlDataReader sql)
        {
            List<Solution> solutions = new List<Solution>();
            Dictionary<string, string> id = new Dictionary<string, string>();
            Probleme p = new Probleme();

            this.connect();
            SqlDataReader result = sql;
            while (result.Read())
            {
                id["IdProbleme"] = "" + result.GetInt32(2);
                p=p.find_by_id(id);

                solutions.Add(new Solution(
                    result.GetInt32(0),
                    result.GetString(1),
                    p
                    ));
            }
            this.disconnect();

            return solutions;

        }

        public List<Probleme> find_probleme(SqlDataReader sql)
        {
            List<Probleme> problemes = new List<Probleme>();
            Dictionary<string, string> id = new Dictionary<string, string>();
            Categorie c = new Categorie();

            this.connect();
            SqlDataReader result = sql;
            while (result.Read())
            {
                id["IdCategorie"] = "" + result.GetInt32(3);
                c=c.find_by_id(id);

                problemes.Add(new Probleme(
                    result.GetInt32(0),
                    result.GetString(1),
                    result.GetString(2),
                    c
                    ));
            }
            this.disconnect();

            return problemes;

        }


    }
}