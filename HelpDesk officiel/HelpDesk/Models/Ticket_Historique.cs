using HelpDesk.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace HelpDesk.Models
{
    public class Ticket_Historique : HelpDeskDB
    {
        // Attributs :
        [Required]
        public Utilisateur Technicien { get; set; }

        [Required]
        public Ticket Ticket { get; set; }

        [Required]
        public string Etat { get; set; }

        [Required]
        public DateTime Date { get; set; }


        // BD Attributs :
        public const string IdTechnicienColmn = "[IdUser]";
        public const string IdTicketColmn = "[IdTicket]";
        public const string EtatColumn = "[Etat]";
        public const string DateColumn = "[Date]";



        public Ticket_Historique()
        {
            this.table = "Technicien_Ticket";
        }

        public Ticket_Historique(Utilisateur technicien,Ticket ticket,string etat,String date ) {

            this.table = "Technicien_Ticket";

            this.Technicien = technicien;
            this.Ticket = ticket;
            this.Etat = etat;
            DateTime dt = Convert.ToDateTime(date);
            this.Date = dt;
        }

        public Ticket_Historique(int idTechnicien, int idTicket, string etat, DateTime date)
        {
            this.table = "Technicien_Ticket";

            this.Technicien.IdUser = idTechnicien;
            this.Ticket.IdTicket = idTicket;
            this.Etat = etat;
            this.Date = date;
        }


        public void save()
        {
            this.connect();

            Dictionary<string, string> data = new Dictionary<string, string>();

            data[IdTechnicienColmn] = "" + this.Technicien.IdUser;
            data[IdTicketColmn] = "" + this.Ticket.IdTicket;
            data[DateColumn] = "'"+this.Date+"'";
            data[EtatColumn] = "'" + this.Etat+"'";

            this.insert(data);
            this.disconnect();

        }

        public List<Ticket_Historique> find_all()
        {
            List<Ticket_Historique> ticket_historiques = new List<Ticket_Historique>();
            Utilisateur technicienHD = new Utilisateur();

            Dictionary<string, string> id = new Dictionary<string, string>();
            Dictionary<string, string> id2 = new Dictionary<string, string>();
            
            this.Ticket = new Ticket();

            this.connect();
            SqlDataReader result = this.select();
            while (result.Read())
            {
                id["[IdUser]"] = "" + result.GetInt32(0);
                technicienHD= technicienHD.find_by_id(id);

                id2["[IdTicket]"] = "" + result.GetInt32(1);
                this.Ticket = this.Ticket.find_by_id(id2);

                ticket_historiques.Add(new Ticket_Historique(
                   technicienHD,
                   this.Ticket,
                   result.GetString(2),
                   result.GetString(3)
                    ));
            }
            this.disconnect();

            return ticket_historiques;
        }
        public List<Ticket_Historique> find_all( int idticket)
        {
            List<Ticket_Historique> ticket_historiques = new List<Ticket_Historique>();
            Utilisateur technicienHD = new Utilisateur();

            Dictionary<string, string> id = new Dictionary<string, string>();
            Dictionary<string, string> id2 = new Dictionary<string, string>();

            this.Ticket = new Ticket();

            this.connect();
            SqlDataReader result = this.select();
            while (result.Read())
            {
                id["[IdUser]"] = "" + result.GetInt32(0);
                technicienHD = technicienHD.find_by_id(id);

                id2["[IdTicket]"] = "" + result.GetInt32(1);
                this.Ticket = this.Ticket.find_by_id(id2);
                if (result.GetInt32(1)== idticket) { 
                ticket_historiques.Add(new Ticket_Historique(
                   technicienHD,
                   this.Ticket,
                   result.GetString(2),
                   result.GetString(3)
                    ));}
            }
            this.disconnect();

            return ticket_historiques;
        }


        public Ticket_Historique find_by_id(Dictionary<string, string> data)
        {
            Ticket_Historique ticket_historique = new Ticket_Historique();
            Utilisateur technicienHD = new Utilisateur();

            Dictionary<string, string> id = new Dictionary<string, string>();
            Dictionary<string, string> id2 = new Dictionary<string, string>();
            
            this.Ticket = new Ticket();

            this.connect();
            SqlDataReader result = this.select();

            while (result.Read())
            {
                id["[IdUser]"] = "" + result.GetInt32(0);
                technicienHD = technicienHD.find_by_id(id);
                id2["[IdTicket]"] = "" + result.GetInt32(1);

                this.Ticket = this.Ticket.find_by_id(id2);

                ticket_historique =  new Ticket_Historique(
                   technicienHD,
                   this.Ticket,
                   result.GetString(2),
                   result.GetString(3)
                    );
                this.disconnect();
                return ticket_historique;
            }
            return null;
        }
    }
}