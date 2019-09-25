using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace HelpDesk.Models
{
    public class Superviseur : Utilisateur
    {
        // Attributs :
        [Required]
        [Display(Name = "Direction")]
        public Direction direction { set; get;}

        

        /************** Declaration des Attributs BD: *****************/
        private string IdDirectionColmn = "IdDirection";
        /******************** Fin declaration *************************/

        // Constructeur 1 :
        public Superviseur()
        {
            this.table = "Superviseur";
        }


        // Constructeur 2 :
        public Superviseur(int id, string login, string password, string typeuser, string nom, string prenom, string email, string phone, int idDirection)
        {
            this.table = "Superviseur";

            this.IdUser = id;
            this.Login = login;
            this.Password = password;
            this.EtatUser = typeuser;
            this.Nom = nom;
            this.Prenom = prenom;
            this.Email = email;
            this.Phone = phone;
            this.direction.IdDirection = idDirection;
        }

        // Constructeur 3 :
        public Superviseur(Utilisateur utilisateur , Direction _direction)
        {
            this.table = "Superviseur";
            this.IdUser = utilisateur.IdUser;
            this.Login = utilisateur.Login;
            this.Password = utilisateur.Password;
            this.EtatUser = utilisateur.EtatUser;
            this.Nom = utilisateur.Nom;
            this.Prenom = utilisateur.Prenom;
            this.Email = utilisateur.Email;
            this.Phone = utilisateur.Phone;
            this.EtatUser = utilisateur.EtatUser;
            this.direction = _direction; 
        }

        public new void save()
        {
            this.connect();
            
            Dictionary<string, string> data = new Dictionary<string, string>();

            Utilisateur utilisateur = new Utilisateur(this.IdUser ,this.Login, this.Password ,"Superviseur" ,this.Nom ,this.Prenom ,this.Email ,this.Phone );

            this.connect();
            data[IdUserColmn] = "" + this.IdUser;
            data[IdDirectionColmn] = "" + this.direction.IdDirection;

            utilisateur.save();
            this.insert(data);
            this.disconnect();
        }

        public new List<Superviseur> find_all()
        {
            List<Superviseur> superviseurs = new List<Superviseur>();
            Dictionary<string, string> id = new Dictionary<string, string>();
            Dictionary<string, string> id2 = new Dictionary<string, string>();

            Utilisateur utilisateur  = new Utilisateur();
            this.direction = new Direction();

            
            this.connect();
            SqlDataReader result = this.select();
            while (result.Read())
            {
                id2["IdUser"] = "" + result.GetInt32(0);
                utilisateur = utilisateur.find_by_id(id2);

                id["IdDirection"] = "" + result.GetInt32(1);
                this.direction = this.direction.find_by_id(id);

                superviseurs.Add(new Superviseur(
                   utilisateur,
                   this.direction
                    ));
            }
            this.disconnect();
            return superviseurs;
        }

        public new Superviseur find_by_id(Dictionary<string,string> data)
        {
            Superviseur superviseur;
            Dictionary<string, string> id = new Dictionary<string, string>();
            Dictionary<string, string> id2 = new Dictionary<string, string>();

            Utilisateur utilisateur = new Utilisateur();
            this.direction = new Direction();

            
            this.connect();
            SqlDataReader result = this.select(data);
            while (result.Read())
            {
                id2[IdUserColmn] = "" + result.GetInt32(0);
                utilisateur = utilisateur.find_by_id(id2);

                id[IdDirectionColmn] = "" + result.GetInt32(1);
                this.direction = this.direction.find_by_id(id);

                superviseur = new Superviseur(
                   utilisateur,
                   this.direction
                   );
                this.disconnect();
                return superviseur;
            }
            return null;
        }

        public new void remove(Dictionary<string, string> data)
        {
            Utilisateur utilisateur = new Utilisateur();

            this.connect();
            this.delete(data);
            utilisateur.remove(data);
            this.disconnect();

        }

        public new void edit_by_id()
        {
            this.connect();

            Dictionary<string, string> data = new Dictionary<string, string>();
            Dictionary<string, string> id = new Dictionary<string, string>();

            Utilisateur utilisateur = new Utilisateur(this.IdUser, this.Login, this.Password, this.EtatUser, this.Nom, this.Prenom, this.Email, this.Phone);
            

            
            data[IdDirectionColmn] = "" + this.direction.IdDirection;

            id[IdUserColmn] = "" + this.IdUser;

            utilisateur.edit_by_id();
            this.update(data, id);
            this.disconnect();
        }
    }
}