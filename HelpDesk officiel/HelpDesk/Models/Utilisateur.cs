using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.ComponentModel.DataAnnotations;

namespace HelpDesk.Models
{
    public class Utilisateur :HelpDeskDB
    {
        /************* Declaration des Attributs Class: ****************/

            
            [Key]
            [Display(Name = "Id Utilisateur")]
            public int IdUser { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
            [Display(Name = "Login")]
            public string Login { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 8)]
            [DataType(DataType.Password)]
            [Display(Name = "Mot de Passe")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirme Mot de Passe")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }

            
            [Display(Name = "Etat d'Utilisateur")]
            public string EtatUser { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 2)]
            [Display(Name = "Nom")]
            public string Nom { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 2)]
            [Display(Name = "Prénom")]
            public string Prenom { get; set; }

            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }
            
            [Required]
            [Phone]
            [Display(Name = "Téléphone")]
            public string Phone { get; set; }
            
            /******************* Fin declaration des attributs ************/

            /************** Declaration des Attributs BD: *****************/
            
            protected const string IdUserColmn = "IdUser";
            protected const string NomColmn = "Nom";
            protected const string PrenomColmn = "Prenom";
            protected const string LoginColmn = "Login";
            protected const string PasswordColmn = "Password";
            protected const string EmailColmn = "Email";
            protected const string PhoneColmn = "Phone";
            protected const string EtatUserColmn = "typeUser";
        
        /******************** Fin declaration *************************/


        /************* Declaration des constructeurs : ******************/

        // Constructeur 1 :
            public Utilisateur()
            {
            this.table = "Utilisateur";
            }

            // Constructeur 2 :
            public Utilisateur(int id,  string login, string password, string typeuser, string nom, string prenom, string email, string phone)
            {
                this.table = "Utilisateur";

                this.IdUser = id;
                this.Login = login;
                this.Password = password;
                this.EtatUser = typeuser;
                this.Nom = nom;
                this.Prenom = prenom;
                this.Email = email;
                this.Phone = phone;
            }

        /********************* Fin de declaration de constructeurs ********************/

            public void save()
            {
                this.connect();
            
                Dictionary<string, string> data = new Dictionary<string,string>();
                
                data[IdUserColmn] = "" + this.IdUser;
                data[LoginColmn] = "'" + this.Login + "'";
                data[PasswordColmn] = "'" + this.Password + "'";
                data[EtatUserColmn] = "'" + this.EtatUser + "'";
                data[NomColmn] = "'" + this.Nom + "'";
                data[PrenomColmn] = "'" + this.Prenom + "'";          
                data[EmailColmn] = "'" + this.Email + "'";
                data[PhoneColmn] = "'" + this.Phone + "'";

                this.insert(data);
                this.disconnect();

            }

            public List<Utilisateur> find_all()
            {
            List<Utilisateur> utilisateurs = new List<Utilisateur>();
            
            this.connect();
            SqlDataReader result = this.select();
                while(result.Read())
                {
                utilisateurs.Add(new Utilisateur(
                    result.GetInt32(0),
                    result.GetString(1),
                    result.GetString(2),
                    result.GetString(3),
                    result.GetString(4),
                    result.GetString(5),
                    result.GetString(6),
                    result.GetString(7)
                    ));
                }
            this.disconnect();
                return utilisateurs;    
           }

        public Utilisateur find_by_id(Dictionary<string, string> data)
        {
            Utilisateur utilisateur;
            
            this.connect();
            SqlDataReader result = this.select(data);

            while (result.Read())
            {
                utilisateur = new Utilisateur(
                    result.GetInt32(0),
                    result.GetString(1),
                    result.GetString(2),
                    result.GetString(3),
                    result.GetString(4),
                    result.GetString(5),
                    result.GetString(6),
                    result.GetString(7)
                    );
                this.disconnect();
                return utilisateur;
            }
            return null;
        }

        public void remove(Dictionary<string, string> data)
        {
            
            this.connect();
            this.delete(data);
            this.disconnect();
            
        }

        public void edit_by_id()
        {
            this.connect();

            Dictionary<string, string> data = new Dictionary<string, string>();
            Dictionary<string, string> id = new Dictionary<string, string>();

            
            data[LoginColmn] = "'" + this.Login + "'";
            data[PasswordColmn] = "'" + this.Password + "'";
            data[EtatUserColmn] = "'" + this.EtatUser + "'";
            data[NomColmn] = "'" + this.Nom + "'";
            data[PrenomColmn] = "'" + this.Prenom + "'";
            data[EmailColmn] = "'" + this.Email + "'";
            data[PhoneColmn] = "'" + this.Phone + "'";
            
            id[IdUserColmn] = "" + this.IdUser;

            this.update(data,id);
            this.disconnect();
        }

    }
} 