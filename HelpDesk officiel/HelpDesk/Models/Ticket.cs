using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace HelpDesk.Models
{
    
    public class Ticket : HelpDeskDB
    {
        //   les Attribut
        [Display(Name = "Id Ticket")]
        public int IdTicket { get; set; }

        
        [Display(Name = "Date d'Ouverture")]
        public DateTime DateOuverture{ get; set; }

        
        [Display(Name = "Date de Fermeture")]
        [Timestamp]
        public DateTime DateFermeture { get; set; }

        
        [Display(Name = "Objet")]
        public string Objet { get; set; }

        
        [Display(Name = "Description")]
        public string Description { get; set; }

        
        [Display(Name = "Priorité")]
        public string Priorite { get; set; }

        
        [Display(Name = "Expéditeur")]
        public Utilisateur Expediteur { get; set; }

        [Display(Name = "Catégorie")]
        public Categorie Categorie  { get; set; }

        [Display(Name = "Etat de Ticket")]
        public string EtatTicket { get; set; }

        public List<Message> Messages { set; get; }

        //  les attribute de bdd
        public const string IdTicketColmn   = "[IdTicket]";
        public const string DateOuvertureColmn = "[DateOuverture]";
        public const string DateFermetureColmn = "[DateFermeture]";
        public const string ObjetColmn       = " [Objet]";
        public const string DescriptionColmn = "[Description]";
        public const string PrioriteColmn = "[Priorite]";
        public const string IdUserColmn = "[IdUser]";
        public const string IdCategorieColmn = "[IdCategorie]";
        public const string EtatTicketColmn = "[EtatTicket]";

        public Ticket()
        {
            this.table = "[dbo].[Ticket]";
        }

        public Ticket(int idTicket, DateTime dateOuverture, DateTime dateFermeture, String objet, String description, String priorite, int idExpediteur, int idCategorie, string etatTicket)
        {
            this.table = "[dbo].[Ticket]";

            this.Expediteur = new Utilisateur();
            this.Categorie = new Categorie();

            this.IdTicket = idTicket;
            this.DateOuverture = dateOuverture;
            this.DateFermeture = dateFermeture;
            this.Objet = objet;
            this.Description = description;
            this.Priorite = priorite;
            this.Expediteur.IdUser = idExpediteur;
            this.Categorie.IdCategorie = idCategorie;
            this.EtatTicket = etatTicket;
        }

        public Ticket( int idTicket,DateTime dateOuverture,DateTime dateFermeture, String objet, String description , String priorite , Utilisateur expediteur,Categorie categorie,string etatTicket)
                                                                                         
        {
            
            this.table = "[dbo].[Ticket]";

            this.IdTicket = idTicket;
            this.DateOuverture = dateOuverture;
            this.DateFermeture = dateFermeture;
            this.Objet = objet;
            this.Description = description;
            this.Priorite = priorite;
            this.Expediteur = expediteur;
            this.Categorie = categorie;
            this.EtatTicket = etatTicket;
        }

        public void save()

        {
            this.connect();

            Dictionary<string, string> data = new Dictionary<string, string>();
            
            //data[IdTicketColmn] = "Null";
            data[DateOuvertureColmn] = "Getdate()";
            data[DateFermetureColmn] = "Getdate()";
            data[ObjetColmn] = "'" + this.Objet + "'";
            data[DescriptionColmn] = "'" + this.Description + "'";
            data[PrioriteColmn] = "'" + this.Priorite + "'";
            data[IdUserColmn] = "" + this.Expediteur.IdUser ;
            data[IdCategorieColmn] = "" + this.Categorie.IdCategorie ;
            data[EtatTicketColmn] = "'"+this.EtatTicket+"'";

            this.insert(data);
            this.disconnect();

        }

        public void save(Probleme probleme)
        {
            this.connect();
            this.table = "[dbo].[Ticket_Probleme]";
            Dictionary<string, string> data = new Dictionary<string, string>();

            data[IdTicketColmn] = "" + this.IdTicket;
            data["[IdProbleme]"] = "" + probleme.IdProbleme;
            data["[date_p]"] = "'" + this.DateFermeture + "'";
            this.insert(data);
            this.disconnect();

        }

        public void save(Probleme probleme , Solution solution)
        {
            this.connect();
            this.table = "[dbo].[Ticket_Proleme_Solution]";
            Dictionary<string, string> data = new Dictionary<string, string>();

            data[IdTicketColmn] = "" + this.IdTicket;
            data["[IdProbleme]"] = "" + probleme.IdProbleme;
            data["[IdSolution]"] = "" + solution.IdSolution;

            this.insert(data);
            this.disconnect();

        }

        public List<Ticket> find_all()
        {
            Utilisateur utilisateur = new Utilisateur();
            this.Categorie = new Categorie();

            Dictionary<string, string> id2 = new Dictionary<string, string>();
            Dictionary<string, string> id3 = new Dictionary<string, string>();

            List<Ticket> tickets = new List<Ticket>();
            
            this.connect();
            SqlDataReader result = this.select();
            while (result.Read())
            {

                id2["IdUser"] = "" + result.GetInt32(7);
                utilisateur = utilisateur.find_by_id(id2);

                id3["[IdCategorie]"] = "" + result.GetInt32(6);
                this.Categorie = this.Categorie.find_by_id(id3);

                tickets.Add(new Ticket(
                    result.GetInt32(0),
                    result.GetDateTime(1),
                    result.GetDateTime(2),
                    result.GetString(3),
                    result.GetString(4),
                    result.GetString(5),
                    utilisateur,
                    this.Categorie,
                    result.GetString(8)
                       ));
            }
            this.disconnect();
            return tickets;
        }

        public Ticket find_by_id(Dictionary<string, string> data)
        {

            Utilisateur utilisateur = new Utilisateur();
            this.Categorie = new Categorie();

            Dictionary<string, string> id2 = new Dictionary<string, string>();
            Dictionary<string, string> id3 = new Dictionary<string, string>();

            Ticket ticket;
            
            this.connect();
            SqlDataReader result = this.select(data);

            while (result.Read())
            {
                id2["IdUser"] = "" + result.GetInt32(7);
                utilisateur = utilisateur.find_by_id(id2);
                
                id3["[IdCategorie]"] = "" + result.GetInt32(6);
                this.Categorie = this.Categorie.find_by_id(id3);

                ticket = new Ticket(
                     result.GetInt32(0),
                     result.GetDateTime(1),
                     result.GetDateTime(2),
                     result.GetString(3),
                     result.GetString(4),
                     result.GetString(5),
                     utilisateur,
                     this.Categorie,
                     result.GetString(8)
                    );
                this.disconnect();
                return ticket;
            }
            return null;
        }

        public void remove(Dictionary<string, string> data)
        {
            Ticket_Historique ticket_historique = new Ticket_Historique();
            ticket_historique.connect();
            ticket_historique.delete(data);

            this.connect();  
            this.delete(data);
            this.disconnect();

        }

        public void edit_by_id()
        {
            this.connect();

            Dictionary<string, string> data = new Dictionary<string, string>();
            Dictionary<string, string> id = new Dictionary<string, string>();

          
            data[DateFermetureColmn] = "Getdate()";
            data[ObjetColmn] = "'" + this.Objet + "'";
            data[DescriptionColmn] = "'" + this.Description + "'";
            data[PrioriteColmn] = "'" + this.Priorite + "'";
          //  data[IdUserColmn] = "" + this.Expediteur.IdUser;
            data[IdCategorieColmn] = "" + this.Categorie.IdCategorie;
            data[EtatTicketColmn] = "'" + EtatTicket + "'";

            id[IdTicketColmn] = "" + this.IdTicket;

            this.update(data, id);
            this.disconnect();
        }

        public List<Ticket> find_by_search(Dictionary<string,string> data)
        {
            Utilisateur utilisateur = new Utilisateur();
            this.Categorie = new Categorie();

            Dictionary<string, string> id2 = new Dictionary<string, string>();
            Dictionary<string, string> id3 = new Dictionary<string, string>();

            List<Ticket> tickets = new List<Ticket>();

            this.connect();
            SqlDataReader result = this.search(data);
            while (result.Read())
            {

                id2["IdUser"] = "" + result.GetInt32(7);
                utilisateur = utilisateur.find_by_id(id2);

                id3["[IdCategorie]"] = "" + result.GetInt32(6);
                this.Categorie = this.Categorie.find_by_id(id3);

                tickets.Add(new Ticket(
                    result.GetInt32(0),
                    result.GetDateTime(1),
                    result.GetDateTime(2),
                    result.GetString(3),
                    result.GetString(4),
                    result.GetString(5),
                    utilisateur,
                    this.Categorie,
                    result.GetString(8)
                       ));
            }
            this.disconnect();
            return tickets;
        }



        public List<Message> find_messages()
        {
            List<Message> messages = new List<Message>();
            return messages;
        }
    }
}