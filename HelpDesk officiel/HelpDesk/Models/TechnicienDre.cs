using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HelpDesk.Models
{
    public class TechnicienDre : Technicien
    {
        // Attributs :

        [Required]
        [Display(Name = "DRE")]
        public DRE dre { set; get; }

        public IEnumerable<SelectListItem> dreList { get; set; }

        /************** Declaration des Attributs BD: *****************/
        private string IddreColmn = "[NumeroDRE]";
        /******************** Fin declaration *************************/

        // Constructeur 1 :
        public TechnicienDre()
        {
            this.table = "[dbo].[TechnicienDRE]";
        }


        // Constructeur 2 :
        public TechnicienDre(int id, string login, string password, string typeuser, string nom, string prenom, string email, string phone, int idDre)
        {
            this.table = "[dbo].[TechnicienDRE]";

            this.IdUser = id;
            this.Login = login;
            this.Password = password;
            this.EtatUser = typeuser;
            this.Nom = nom;
            this.Prenom = prenom;
            this.Email = email;
            this.Phone = phone;
            this.dre.NumeroDRE = idDre;
        }

        // Constructeur 3 :
        public TechnicienDre(Utilisateur utilisateur, DRE d)
        {
            this.table = "TechnicienDre";
            this.IdUser = utilisateur.IdUser;
            this.Login = utilisateur.Login;
            this.Password = utilisateur.Password;
            this.EtatUser = utilisateur.EtatUser;
            this.Nom = utilisateur.Nom;
            this.Prenom = utilisateur.Prenom;
            this.Email = utilisateur.Email;
            this.Phone = utilisateur.Phone;
            this.EtatUser = utilisateur.EtatUser;
            this.dre = d;
        }

        public new void save()
        {
            this.connect();

            Dictionary<string, string> data = new Dictionary<string, string>();

            Utilisateur utilisateur = new Utilisateur(this.IdUser, this.Login, this.Password, "TechnicienDRE", this.Nom, this.Prenom, this.Email, this.Phone);

            this.connect();
            data[IdUserColmn] = "" + this.IdUser;
            data[IddreColmn] = "" + this.dre.NumeroDRE;

            utilisateur.save();
            this.insert(data);
            this.disconnect();
        }

        public new List<TechnicienDre> find_all()
        {
            List<TechnicienDre> TechnicienDres = new List<TechnicienDre>();
            Dictionary<string, string> id = new Dictionary<string, string>();
            Dictionary<string, string> id2 = new Dictionary<string, string>();

            Utilisateur utilisateur = new Utilisateur();
            this.dre = new DRE();


            this.connect();
            SqlDataReader result = this.select();
            while (result.Read())
            {
                id2["IdUser"] = "" + result.GetInt32(0);
                utilisateur = utilisateur.find_by_id(id2);

                id["[NumeroDRE]"] = "" + result.GetInt32(1);
                this.dre = this.dre.find_by_id(id);

                TechnicienDres.Add(new TechnicienDre(
                   utilisateur,
                   this.dre
                    ));
            }
            this.disconnect();
            return TechnicienDres;
        }

        public new TechnicienDre find_by_id(Dictionary<string, string> data)
        {
            TechnicienDre TechnicienDre;
            Dictionary<string, string> id = new Dictionary<string, string>();
            Dictionary<string, string> id2 = new Dictionary<string, string>();

            Utilisateur utilisateur = new Utilisateur();
            this.dre = new DRE();


            this.connect();
            SqlDataReader result = this.select(data);
            while (result.Read())
            {
                id2["IdUser"] = "" + result.GetInt32(0);
                utilisateur = utilisateur.find_by_id(id2);

                id["[NumeroDRE]"] = "" + result.GetInt32(1);
                this.dre = this.dre.find_by_id(id);

                TechnicienDre = new TechnicienDre(
                   utilisateur,
                   this.dre
                   );
                this.disconnect();
                return TechnicienDre;
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

            Utilisateur utilisateur = new Utilisateur(this.IdUser, this.Login, this.Password, "TechnicienDRE", this.Nom, this.Prenom, this.Email, this.Phone);



            data[IddreColmn] = "" + this.dre.NumeroDRE;

            id[IdUserColmn] = "" + this.IdUser;

            utilisateur.edit_by_id();
            this.update(data, id);
            this.disconnect();
        }










    }
}