using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace HelpDesk.Models
{
    public class Probleme : HelpDeskDB
    {
        // Attributs :
        
        [Key]
        [Display(Name = "Id Problème")]
        public int IdProbleme { set; get; }

        
        [Display(Name = "Objet de Problème")]
        public string ObjetProbleme { set; get; }

        
        [Display(Name = "Descriptin de Problème")]
        public string DescreptionProbleme { set; get; }

        
        [Display(Name = "Categorie")]
        public Categorie categorie { set; get; }

        public List<Solution> Solutions { set; get; }

        //DB Attributs :
        public const string IdProblemeColmn = "IdProbleme";
        public const string ObjetProblemecolmn = "ObjetProbleme";
        public const string DescreptionProblemeColmn = "DescreptionProbleme";
        public const string IdCategorieColmn = "IdCategorie";

        // Constructeur 1 :
        public Probleme()
        {
            this.table = "Probleme";
        }

        // Constructeur 2:
        public Probleme(int idProblme , string objetProbleme , string descriptionProbleme,int idCategorie)
        {
            this.table = "Probleme";

            this.IdProbleme = idProblme;
            this.ObjetProbleme = objetProbleme;
            this.DescreptionProbleme = descriptionProbleme;
            this.categorie.IdCategorie = idCategorie;
        }

        // Constructeur 3:
        public Probleme(int idProblme, string objetProbleme, string descriptionProbleme,Categorie categorie)
        {
            this.table = "Probleme";

            this.IdProbleme = idProblme;
            this.ObjetProbleme = objetProbleme;
            this.DescreptionProbleme = descriptionProbleme;
            this.categorie = categorie; 
            
        }

        public void save()
        {
            this.connect();

            Dictionary<string, string> data = new Dictionary<string, string>();
            
            data[ObjetProblemecolmn] = "'" + this.ObjetProbleme + "'";
            data[DescreptionProblemeColmn] = "'" + this.DescreptionProbleme + "'";
            data[IdCategorieColmn] = ""+this.categorie.IdCategorie;
            this.insert(data);
            this.disconnect();

        }

        public List<Probleme> find_all()
        {
            List<Probleme> problemes = new List<Probleme>();
            Dictionary<string, string> id = new Dictionary<string, string>();
            this.categorie = new Categorie();

            this.connect();
            SqlDataReader result = this.select();
            while (result.Read())
            {
                id["IdCategorie"] = "" + result.GetInt32(3);
                this.categorie = this.categorie.find_by_id(id);

                problemes.Add(new Probleme(
                    result.GetInt32(0),
                    result.GetString(1),
                    result.GetString(2),
                    this.categorie
                    ));
            }
            this.disconnect();
            
            return problemes;
            
        }

        public Probleme find_by_id(Dictionary<string, string> data)
        {
            Probleme probleme;
            Dictionary<string, string> id = new Dictionary<string, string>();
            this.categorie = new Categorie();

            this.connect();
            SqlDataReader result = this.select(data);

            while (result.Read())
            {
                id["IdCategorie"] = "" + result.GetInt32(3);
                this.categorie = this.categorie.find_by_id(id);

                probleme = new Probleme(
                    result.GetInt32(0),
                    result.GetString(1),
                    result.GetString(2),
                    this.categorie

                    );
                this.disconnect();
                return probleme;
            }
            return null;
        }

        public List<Solution> find_solution()
        {
            Solution solution = new Solution();
            List<Solution> solutions = new List<Solution>();

            Dictionary<string, string> id = new Dictionary<string, string>();
            Dictionary<string, string> id2 = new Dictionary<string, string>();
            solution.probleme = new Probleme();

            solution.connect();
            id2[IdProblemeColmn] = "" + this.IdProbleme;
            SqlDataReader result = solution.select(id2);            
            while (result.Read())
            {
                
                solutions.Add( new Solution(
                    result.GetInt32(0),
                    result.GetString(1),
                    this
                    ));
                
            }
            solution.disconnect();

            return solutions;

        }

        public void remove(Dictionary<string, string> data)
        {
            this.table = "Probleme";
            this.connect();
            this.delete(data);
            this.disconnect();
        }

        public void edit_by_id()
        {
            this.connect();

            Dictionary<string, string> data = new Dictionary<string, string>();
            Dictionary<string, string> id = new Dictionary<string, string>();

            this.table = "Probleme";
            data[ObjetProblemecolmn] = "'" + this.ObjetProbleme+ "'";
            data[DescreptionProblemeColmn] = "'" + this.DescreptionProbleme + "'";
            data[IdCategorieColmn] = "'" + this.categorie.IdCategorie + "'";
            
            id[IdProblemeColmn] = "" + this.IdProbleme;

            this.update(data, id);
            this.disconnect();
        }

        public List<Probleme> find_by_search(Dictionary<string, string> data)
        {
            List<Probleme> problemes = new List<Probleme>();
            Dictionary<string, string> id = new Dictionary<string, string>();
            this.categorie = new Categorie();

            this.connect();
            SqlDataReader result = this.search(data);
            while (result.Read())
            {
                id["IdCategorie"] = "" + result.GetInt32(3);
                this.categorie = this.categorie.find_by_id(id);

                problemes.Add(new Probleme(
                    result.GetInt32(0),
                    result.GetString(1),
                    result.GetString(2),
                    this.categorie
                    ));
            }
            this.disconnect();

            return problemes;

        }

    }
    }