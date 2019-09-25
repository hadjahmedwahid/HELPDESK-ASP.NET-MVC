using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HelpDesk.Models
{
    public class Message : HelpDeskDB
    {
        //Attributes :
        
        public int IdMessage { set; get; }
        
        public string ContentMessage { set; get; }

        public string EtatMessage { set; get; }

        public DateTime DateMessage { set; get; }  
        
        public Utilisateur Envoyeur { set; get; }

        public Ticket Ticket { set; get; }

        // DB Attributes :
        public const string IdMessageColmn = "[IdMessage]";
        public const string ContentMessageColmn = "[ContentMessage]";
        public const string DateMessageColmn = "[DateMessage]";
        public const string EtatMessageColmn = "[EtatMessage]";
        public const string IdUserColmn = "[IdUser]";
        public const string IdTicketColmn = "[IdTicket]";

        // Constructer 1 :
        public Message()
        {
            this.table = "[Message]";
        }

        // Constructer :
        public Message(int idMessage ,string contentMessage ,string etatMessage, DateTime dateMessage, Utilisateur envoyeur,Ticket ticket)
        {
            this.table = "[Message]";

            this.IdMessage = idMessage;
            this.ContentMessage = contentMessage;
            this.DateMessage = dateMessage;
            this.EtatMessage = etatMessage;
            this.Envoyeur = envoyeur;
            this.Ticket = ticket;

        }

        public void save()
        {
            this.connect();

            Dictionary<string, string> data = new Dictionary<string, string>();

            //data[IdMessageColmn] = "Null";
            data[ContentMessageColmn] = "'" + this.ContentMessage + "'";
            data[DateMessageColmn] = "Getdate()";
            data[EtatMessageColmn] = "'" + this.EtatMessage + "'";
            data[IdUserColmn] = "" + this.Envoyeur.IdUser;
            data[IdTicketColmn] = "" + this.Ticket.IdTicket;
            
            // insert data into data base :
            this.insert(data);
            this.disconnect();
        }
    }
}