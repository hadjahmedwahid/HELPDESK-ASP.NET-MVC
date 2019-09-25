using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace HelpDesk.Models
{
    public class TechnicienHelpDesk : Technicien
    {
        // Attributs :
        
        [Display(Name = "Direction")]
        public Direction direction { set; get; }

        
        [Display(Name = "Categorie")]
        public Categorie categorie { get; set; }

        
        [Display(Name = "Expérience")]
        public String experience { get; set; }


        /************** Declaration des Attributs BD: *****************/
        private string IdDirectionColmn = "IdDirection";
        private string IdCategorieColmn = "[IdCategorie]";
        private string IdexpColmn = "[Experience]";
        /******************** Fin declaration *************************/

        // Constructeur 1 :
        public TechnicienHelpDesk()
        {
            this.table = "[dbo].[TechnicienHelpDesk]";
        }


        // Constructeur 2 :
        public TechnicienHelpDesk(int id, string login, string password, string typeuser, string nom, string prenom, string email, string phone, int idDirection,int idcategorie,string exp)
        {
            this.table = "TechnicienHelpDesk";

            this.IdUser = id;
            this.Login = login;
            this.Password = password;
            this.EtatUser = typeuser;
            this.Nom = nom;
            this.Prenom = prenom;
            this.Email = email;
            this.Phone = phone;
            this.direction.IdDirection = idDirection;
            this.categorie.IdCategorie = idcategorie;
            this.experience = exp;
        }

        // Constructeur 3 :
        public TechnicienHelpDesk(Utilisateur utilisateur, Direction _direction,Categorie c,string exp)
        {
            this.table = "TechnicienHelpDesk";
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
            this.categorie = c;
            this.experience = exp;


        }

        public new void save()
        {
            this.connect();

            Dictionary<string, string> data = new Dictionary<string, string>();

            Utilisateur utilisateur = new Utilisateur(this.IdUser, this.Login, this.Password, "	TechnicienHelpDesk", this.Nom, this.Prenom, this.Email, this.Phone);

            this.connect();
            data[IdUserColmn] = "" + this.IdUser;
            data[IdDirectionColmn] = "" + this.direction.IdDirection;
            data[IdCategorieColmn] = "" + this.categorie.IdCategorie;
            data[IdexpColmn] = "'" + this.experience + "'";

            utilisateur.save();
            this.insert(data);
            this.disconnect();
        }

        public new List<TechnicienHelpDesk> find_all()
        {
            List<TechnicienHelpDesk> techs = new List<TechnicienHelpDesk>();
            Dictionary<string, string> id = new Dictionary<string, string>();
            Dictionary<string, string> id2 = new Dictionary<string, string>();
            Dictionary<string, string> id3 = new Dictionary<string, string>();

            Utilisateur utilisateur = new Utilisateur();
            this.direction = new Direction();
            this.categorie = new Categorie();


            this.connect();
            SqlDataReader result = this.select();
            while (result.Read())
            {
                id2["IdUser"] = "" + result.GetInt32(0);
                utilisateur = utilisateur.find_by_id(id2);

                id["IdDirection"] = "" + result.GetInt32(1);
                this.direction = this.direction.find_by_id(id);
                id3["[IdCategorie]"] = "" + result.GetInt32(3);
                this.categorie = this.categorie.find_by_id(id3);

                techs.Add(new TechnicienHelpDesk(
                   utilisateur,
                   this.direction,
                   this.categorie,
                   result.GetString(2)

                    ));
            }
            this.disconnect();
            return techs;
        }

        public new TechnicienHelpDesk find_by_id(Dictionary<string, string> data)
        {
            TechnicienHelpDesk tech;
            Dictionary<string, string> id = new Dictionary<string, string>();
            Dictionary<string, string> id2 = new Dictionary<string, string>();
            Dictionary<string, string> id3 = new Dictionary<string, string>();

            Utilisateur utilisateur = new Utilisateur();
            this.direction = new Direction();
            this.categorie = new Categorie();


            this.connect();
            SqlDataReader result = this.select(data);
            while (result.Read())
            {
                id2["IdUser"] = "" + result.GetInt32(0);
                utilisateur = utilisateur.find_by_id(id2);

                id["IdDirection"] = "" + result.GetInt32(1);
                this.direction = this.direction.find_by_id(id);

                id3["[IdCategorie]"] = "" + result.GetInt32(3);
                this.categorie = this.categorie.find_by_id(id3);

                tech = new TechnicienHelpDesk(
                  utilisateur,
                  this.direction,
                  this.categorie,
                  result.GetString(2));

               this.disconnect();
                return tech;
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

            Utilisateur utilisateur = new Utilisateur(this.IdUser, this.Login, this.Password, "	TechnicienHelpdesk", this.Nom, this.Prenom, this.Email, this.Phone);



            data[IdDirectionColmn] = "" + this.direction.IdDirection;
            data[IdCategorieColmn] = "" + this.categorie.IdCategorie;
           data[IdexpColmn] = "'" + this.experience+"'";

            id[IdUserColmn] = "" + this.IdUser;

            utilisateur.edit_by_id();
            this.update(data, id);
            this.disconnect();
        }


    }

}