using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Data.Entity;
using HelpDesk.Hubs;
using Microsoft.AspNet.SignalR.Client;
using System.Configuration;

namespace HelpDesk.Models
{
    public class HelpDeskDB 
    {
        // Attributes :
        protected string serverName = @"WAHID";

        protected string databaseName = "HELPDESK3";
        readonly string _connString = ConfigurationManager.ConnectionStrings["sqlConString"].ConnectionString;

        // Attributes :
        public SqlConnection connexion;
        protected string table;


        public void connect()
        {

            this.connexion = new SqlConnection(@"Server=" + this.serverName + ";DataBase=" + this.databaseName + ";user id=sa;password=12345");
                connexion.Open();
            

        }

        public void disconnect()
        {
            this.connexion.Close();
            this.connexion.Dispose();
            SqlConnection.ClearPool(this.connexion);
            SqlConnection.ClearAllPools();

        }


        public int insert(Dictionary<string,string> data)
        {
            string query ="INSERT INTO "+ this.table+ " (";
            int dataLength = data.Count;
            foreach(string colmnName in data.Keys)
            {
                dataLength--;
                if (dataLength == 0)
                    query += colmnName + ")";
                else
                    query += colmnName + ",";
            }
            query += " values(";
            dataLength = data.Count;
            foreach (string colmnName in data.Keys)
            {
                dataLength--;
                if (dataLength == 0)
                    query += data[colmnName] + ")";
                else
                    query += data[colmnName] + ",";
            }
            SqlCommand cmd = new SqlCommand(query,this.connexion);

           return  cmd.ExecuteNonQuery();
        }

        public SqlDataReader select()
        {
            string query = "SELECT * FROM " + this.table;
            SqlCommand cmd = new SqlCommand(query, this.connexion);
            SqlDataReader result = cmd.ExecuteReader();
            return result;
        }
        
        public SqlDataReader select(Dictionary<string, string> data)
        {
            string query = "SELECT * FROM " + this.table + " WHERE ";
            int dataLength = data.Count;
            foreach (string colmnName in data.Keys)
            {
                dataLength--;
                if (dataLength == 0)
                    query += colmnName+" = "+data[colmnName] ;
                else
                    query += colmnName + " = " + data[colmnName] + " AND ";
            }

            SqlCommand cmd = new SqlCommand(query, this.connexion);
            SqlDataReader result = cmd.ExecuteReader();
            return result;

        }

        public int delete(Dictionary<string,string> id)
        {
            String query = "DELETE FROM " + this.table + " WHERE ";
            int idLength = id.Count;
            foreach (String colmnName in id.Keys)
            {
                idLength--;
                if (idLength == 0)
                    query += colmnName + " = " + id[colmnName];
                else
                    query += colmnName + " = " + id[colmnName] + " AND ";
            }

            SqlCommand cmd = new SqlCommand(query, this.connexion);
            return cmd.ExecuteNonQuery();

        }

        public void update(Dictionary<string,string> data , Dictionary<string,string> id)
        {
                String query;
                foreach (String colmnName in data.Keys)
                {
                    query = "UPDATE " + this.table + " SET " + colmnName + " = " + data[colmnName] + " WHERE ";
                    int idLength = id.Count;
                    foreach (String colmnName2 in id.Keys)
                    {
                        idLength--;
                        if (idLength == 0)
                            query += colmnName2 + " = " + id[colmnName2];
                        else
                            query += colmnName2 + " = " + id[colmnName2] + " AND ";
                    }
                SqlCommand cmd = new SqlCommand(query, this.connexion);
                 cmd.ExecuteNonQuery();
                }
            
        }

        public SqlDataReader search(Dictionary<string, string> data)
        {
            string query = "SELECT * FROM " + this.table + " WHERE ";
            int dataLength = data.Count;
            foreach (string colmnName in data.Keys)
            {
                dataLength--;
                if (dataLength == 0)
                    query += colmnName + " LIKE '%" + data[colmnName]+"%'";
                else
                    query += colmnName + " LIKE '%" + data[colmnName] + "%' OR ";
            }

            SqlCommand cmd = new SqlCommand(query, this.connexion);
            SqlDataReader result = cmd.ExecuteReader();
            return result;

        }


        public SqlDataReader select_where_id_not_in( Dictionary<string, string> id,string table)
        {
            string query = "SELECT * FROM " + this.table + " WHERE ";

            int dataLength = id.Count;
            foreach (string colmnName in id.Keys)
            {
                dataLength--;
                if (dataLength == 0)
                    query += colmnName ;
                else
                    query += id[colmnName] + " , ";
            }
            query += " NOT IN ( SELECT ";
            dataLength = id.Count;
            foreach (string colmnName in id.Keys)
            {
                dataLength--;
                if (dataLength == 0)
                    query += colmnName;
                else
                    query +=colmnName + " , ";
            }
            query += " FROM " + table+ " ) ORDER BY ";
            dataLength = id.Count;
            foreach (string colmnName in id.Keys)
            {
                dataLength--;
                if (dataLength == 0)
                    query += colmnName;
                else
                    query += colmnName + " , ";
            }
            query += "DESC";


            SqlCommand cmd = new SqlCommand(query, this.connexion);
            SqlDataReader result = cmd.ExecuteReader();
            return result;
        }


        

        public IEnumerable<Ticket> GetAllTickets()
        {
            var tickets = new List<Ticket>();
            using (this.connexion = new SqlConnection(_connString))
            {
                this.connexion.Open();
                using (var command = new SqlCommand(@"SELECT [IdTicket],[DateOuverture],[DateFermeture],[Objet],[Description],[Priorite],[IdCategorie],[IdUser],[EtatTicket] FROM [dbo].[Ticket] ORDER BY [IdTicket] DESC", this.connexion))
                {
                    command.Notification = null;

                    var dependency = new SqlDependency(command);
                    dependency.OnChange += new OnChangeEventHandler(dependency_OnChange);

                    if (this.connexion.State == System.Data.ConnectionState.Closed)
                        this.connexion.Open();

                    var reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        Utilisateur utilisateur = new Utilisateur();
                        Categorie categorie = new Categorie();

                        Dictionary<string, string> id2 = new Dictionary<string, string>();
                        Dictionary<string, string> id3 = new Dictionary<string, string>();

                        id2["IdUser"] = "" + (int)reader["IdUser"];
                        utilisateur = utilisateur.find_by_id(id2);

                        id3["[IdCategorie]"] = "" + (int)reader["IdCategorie"];
                        categorie = categorie.find_by_id(id3);

                        tickets.Add(new Ticket((int)reader["IdTicket"], Convert.ToDateTime(reader["DateOuverture"]), Convert.ToDateTime(reader["DateFermeture"]), (string)reader["Objet"], (string)reader["Description"], (string)reader["Priorite"], utilisateur, categorie, (string)reader["EtatTicket"]));

                        // tickets.Add(item: new Ticket { IdTicket = (int)reader["IdTicket"],DateOuverture = Convert.ToDateTime(reader["DateOuverture"]),DateFermeture = Convert.ToDateTime(reader["DateFermeture"]),Objet = (string)reader["Objet"],Description = (string)reader["Description"] });
                    }

                    this.connexion.Close();
                    this.connexion.Dispose();
                    SqlConnection.ClearPool(this.connexion);
                }

            }

            return tickets;
        }

        public IEnumerable<Message> GetAllMessages()
        {
            var messages = new List<Message>();
            using (this.connexion = new SqlConnection(_connString))
            {
                this.connexion.Open();
                using (var command = new SqlCommand(@"SELECT [IdMessage],[ContentMessage],[EtatMessage],[DateMessage],[IdUser],[IdTicket] FROM [dbo].[Message] ORDER BY [IdMessage] DESC", this.connexion))
                {
                    command.Notification = null;

                    var dependency = new SqlDependency(command);
                    dependency.OnChange += new OnChangeEventHandler(dependency_OnChange);

                    if (this.connexion.State == System.Data.ConnectionState.Closed)
                        this.connexion.Open();

                    var reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        Utilisateur utilisateur = new Utilisateur();
                        Ticket ticket = new Ticket();

                        Dictionary<string, string> _id2 = new Dictionary<string, string>();
                        Dictionary<string, string> _id3 = new Dictionary<string, string>();

                        _id2["IdUser"] = "" + (int)reader["IdUser"];
                        utilisateur = utilisateur.find_by_id(_id2);

                        _id3["[IdTicket]"] = "" + (int)reader["IdTicket"];
                        ticket = ticket.find_by_id(_id3);

                        messages.Add(new Message((int)reader["IdMessage"], (string)reader["ContentMessage"], (string)reader["EtatMessage"], Convert.ToDateTime(reader["DateMessage"]), utilisateur, ticket));
                    }
                    this.connexion.Close();
                    this.connexion.Dispose();
                    SqlConnection.ClearPool(this.connexion);
                }

            }
            return messages;


        }

        private void dependency_OnChange(object sender, SqlNotificationEventArgs e)
        {
            if (e.Type == SqlNotificationType.Change)
            {
                HelpDeskHub.RealTime();
            }
        }

    }
}