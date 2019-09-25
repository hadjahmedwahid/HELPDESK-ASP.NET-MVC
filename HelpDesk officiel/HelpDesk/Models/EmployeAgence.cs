using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace HelpDesk.Models
{
    public class EmployeAgence : Utilisateur
    {
        // Attributs :

        [Display(Name = "Agence")]
        public Agence agence { set; get; }



        /************** Declaration des Attributs BD: *****************/
        private string NumeroAgenceColmn = "NumeroAgence";
        /******************** Fin declaration *************************/

        // Constructeur 1 :
        public EmployeAgence()
        {
            this.table = "EmployeAgence";
        }


        // Constructeur 2 :
        public EmployeAgence(int id, string login, string password, string typeuser, string nom, string prenom, string email, string phone, int numeroAgence)
        {
            this.table = "EmployeAgence";

            this.IdUser = id;
            this.Login = login;
            this.Password = password;
            this.EtatUser = typeuser;
            this.Nom = nom;
            this.Prenom = prenom;
            this.Email = email;
            this.Phone = phone;
            this.agence.IdAgence = numeroAgence;
        }

        // Constructeur 3 :
        public EmployeAgence(Utilisateur utilisateur, Agence _agence)
        {
            this.table = "EmployeAgence";

            this.IdUser = utilisateur.IdUser;
            this.Login = utilisateur.Login;
            this.Password = utilisateur.Password;
            this.EtatUser = utilisateur.EtatUser;
            this.Nom = utilisateur.Nom;
            this.Prenom = utilisateur.Prenom;
            this.Email = utilisateur.Email;
            this.Phone = utilisateur.Phone;
            this.EtatUser = utilisateur.EtatUser;
            this.agence = _agence;
        }

        public new void save()
        {
            this.connect();

            Dictionary<string, string> data = new Dictionary<string, string>();

            Utilisateur utilisateur = new Utilisateur(this.IdUser, this.Login, this.Password, "EmployeAgence", this.Nom, this.Prenom, this.Email, this.Phone);

            this.connect();
            data[IdUserColmn] = "" + this.IdUser;
            data[NumeroAgenceColmn] = "" + this.agence.IdAgence;

            utilisateur.save();
            this.insert(data);
            this.disconnect();
        }

          
        public new List<EmployeAgence> find_all()
        {
            List<EmployeAgence> employeAgences = new List<EmployeAgence>();
            Dictionary<string, string> id = new Dictionary<string, string>();
            Dictionary<string, string> id2 = new Dictionary<string, string>();

            Utilisateur utilisateur = new Utilisateur();
            this.agence = new Agence();


            this.connect();
            SqlDataReader result = this.select();
            while (result.Read())
            {
                id2["IdUser"] = "" + result.GetInt32(0);
                utilisateur = utilisateur.find_by_id(id2);

                id["NumeroAgence"] = "" + result.GetInt32(1);
                this.agence = this.agence.find_by_id(id);

                employeAgences.Add(new EmployeAgence(
                   utilisateur,
                   this.agence
                    ));
            }
            this.disconnect();
            return employeAgences;
        }

        public new EmployeAgence find_by_id(Dictionary<string, string> data)
        {
            EmployeAgence employeAgence;
            Dictionary<string, string> id = new Dictionary<string, string>();
            Dictionary<string, string> id2 = new Dictionary<string, string>();

            Utilisateur utilisateur = new Utilisateur();
            this.agence = new Agence();


            this.connect();
            SqlDataReader result = this.select(data);
            while (result.Read())
            {
                id2[IdUserColmn] = "" + result.GetInt32(0);
                utilisateur = utilisateur.find_by_id(id2);

                id[NumeroAgenceColmn] = "" + result.GetInt32(1);
                this.agence = this.agence.find_by_id(id);

                employeAgence = new EmployeAgence(
                   utilisateur,
                   this.agence
                   );
                this.disconnect();
                return employeAgence;
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



            data[NumeroAgenceColmn] = "" + this.agence.IdAgence;

            id[IdUserColmn] = "" + this.IdUser;

            utilisateur.edit_by_id();
            this.update(data, id);
            this.disconnect();
        }
    }
}