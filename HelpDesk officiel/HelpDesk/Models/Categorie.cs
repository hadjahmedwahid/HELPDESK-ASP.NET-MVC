using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace HelpDesk.Models
{
    public class Categorie : HelpDeskDB
    {

        // Attributs :
        
        
        [Display(Name = "Id Module")]
        public int IdCategorie { set; get; }

        
        [Display(Name = "Nom Module")]
        public string NomCategorie { set; get; }

        
        [Display(Name = "Nom de Module Parent")]
        public Categorie ParentCategorie { set; get; }

        
        [Display(Name = "Description de Module")]
        public string DescCategorie { set; get; }

        [Display(Name = "Direction")]
        public Direction direction { set; get; }


        public List<Categorie> SubCategorie { set; get; }

        // DB Categorie :
        public const string IdCategorieColmn = "IdCategorie";
        public const string NomCategorieColmn = "NomCategorie";
        public const string IdParentCategorieColmn = "IdParent";
        public const string DescCategorieColmn = "DescCategorie";
        public const string IdDirectionColmn = "IdDirection"; 
        // Constructeur 1 :
        public Categorie()
        {
            this.table = "Categorie";
        }

        // Constructeur 2 :
        public Categorie (int idCategorie, string nomCategorie, int idParentCategorie, string descCategorie)
        {
            this.table = "Categorie";

            this.ParentCategorie = new Categorie();

            this.IdCategorie = idCategorie;
            this.NomCategorie = nomCategorie;
            this.ParentCategorie.IdCategorie = (int) idParentCategorie; 
            this.DescCategorie = descCategorie;
            
        }

        // constructeur 3 :
        public Categorie(int idCategorie, string nomCategorie,Categorie parentCategorie,string descCategorie,Direction _direction)
        {
            this.table = "Categorie";

            this.IdCategorie = idCategorie;
            this.NomCategorie = nomCategorie;
            this.ParentCategorie = parentCategorie;
            this.DescCategorie = descCategorie;
            this.direction = _direction;

        }

        public void save()
        {
            this.connect();

            Dictionary<string, string> data = new Dictionary<string, string>();
            
            data[IdCategorieColmn] = "" + this.IdCategorie;
            data[NomCategorieColmn] = "'" + this.NomCategorie + "'";
            data[IdParentCategorieColmn] = "" + this.ParentCategorie.IdCategorie ;
            data[DescCategorieColmn] = "'" + this.DescCategorie+"'";
            data[IdDirectionColmn] = "" + this.direction.IdDirection ;
            this.insert(data);
            this.disconnect();

        }

        public List<Categorie> find_all()
        {
            List<Categorie> categories = new List<Categorie>();
            Dictionary<string, string> id = new Dictionary<string, string>();
            Dictionary<string, string> id2 = new Dictionary<string, string>();

            

            this.connect();
            SqlDataReader result = this.select();
            while (result.Read())
            {
                this.ParentCategorie = new Categorie();
                this.direction = new Direction();

                if (result.GetInt32(2) != 0)
                {
                    id["IdCategorie"] = "" + result.GetInt32(2);
                    this.ParentCategorie = this.ParentCategorie.find_by_id(id);
                }
                id2["IdDirection"] = "" + result.GetInt32(4);
                this.direction = this.direction.find_by_id(id2);

                categories.Add(new Categorie(
                    result.GetInt32(0),
                    result.GetString(1),
                    this.ParentCategorie,
                    result.GetString(3),
                    this.direction
                    ));
            }
            this.disconnect();
            return categories;
        }

        
        public Categorie find_by_id(Dictionary<string, string> data)
        {
            Categorie _categorie;
            Dictionary<string, string> id = new Dictionary<string, string>();
            Dictionary<string, string> id2 = new Dictionary<string, string>();


            this.connect();    
            SqlDataReader result = this.select(data);

            while (result.Read())
            {
                this.ParentCategorie = new Categorie();
                this.direction = new Direction();


                if (result.GetInt32(2) != 0)
                {
                    id["IdCategorie"] = "" + result.GetInt32(2);
                    this.ParentCategorie = this.ParentCategorie.find_by_id(id);
                }

                id2["IdDirection"] = "" + result.GetInt32(4);
                this.direction = this.direction.find_by_id(id2);

                _categorie = new Categorie(
                    result.GetInt32(0),
                    result.GetString(1),
                    this.ParentCategorie,
                    result.GetString(3),
                    this.direction
                    );
                this.disconnect();
                return _categorie;
            }
            return null;
        }

        public List<Categorie>  find_sub_categorie()
        {
            this.SubCategorie = new List<Categorie>();
            Dictionary<string, string> id = new Dictionary<string, string>();
            Dictionary<string, string> id2 = new Dictionary<string, string>();

            
            id["IdParent"] = "" + this.IdCategorie;

            
            this.connect();

            SqlDataReader result = this.select(id);

            while (result.Read())
            {
                this.direction = new Direction();

                id2["IdDirection"] = "" + result.GetInt32(4);
                this.direction = this.direction.find_by_id(id2);

                this.SubCategorie.Add(new Categorie(
                    result.GetInt32(0),
                    result.GetString(1),
                    this,
                    result.GetString(3),
                    this.direction
                    ));
                
            }
            this.disconnect();
            return this.SubCategorie;
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

            data[NomCategorieColmn] = "'" + this.NomCategorie + "'";
            data[IdParentCategorieColmn] = "" + this.ParentCategorie.IdCategorie ;
            data[DescCategorieColmn] = "'" + this.DescCategorie + "'";
            data[IdDirectionColmn] = "" + this.direction.IdDirection;

            id[IdCategorieColmn] = "" + this.IdCategorie;

            this.update(data, id);
            this.disconnect();
        }
    }
}