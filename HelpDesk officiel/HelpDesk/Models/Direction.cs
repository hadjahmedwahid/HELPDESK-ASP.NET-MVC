using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.ComponentModel.DataAnnotations;

namespace HelpDesk.Models
{
    public class Direction : HelpDeskDB
    {
        // Attributes :
        
        
        [Display(Name = "Id Direction")]
        public int IdDirection { set; get; }

        
        [Display(Name = "Nom de Direction")]
        public string NomDirection { set; get; }

        
        [Display(Name = "Libellé de Direction")]
        public string Libelle { set; get; }

        // DB Attributs : 
        public const string IdDirectionColmn = "IdDirection";
        public const string NomDirectionColmn = "NomDirection";
        public const string LibelleColmn = "Libellé";


        public Direction ()
        {
            this.table = "Direction";
        }

        public Direction(int idDirection , string nomDirection ,string libelle)
        {
            this.table = "Direction";

            this.IdDirection = idDirection;
            this.NomDirection = nomDirection;
            this.Libelle = libelle;
        }

        public void save()
        {
            this.connect();

            Dictionary<string, string> data = new Dictionary<string, string>();
          
            data[IdDirectionColmn] = "" + this.IdDirection;
            data[NomDirectionColmn] = "'" + this.NomDirection+"'";
            data[LibelleColmn] = "'" + this.Libelle + "'";
            this.insert(data);
            this.disconnect();

        }

          public List<Direction> find_all()
            {
                List<Direction> directions = new List<Direction>();
                
                this.connect();
                SqlDataReader result = this.select();
                while (result.Read())
                {
                    directions.Add(new Direction(
                        result.GetInt32(0),
                        result.GetString(1),
                        result.GetString(2)
                        ));
                }
                this.disconnect();
                return directions;
            }

        public Direction find_by_id(Dictionary<string, string> data)
        {
            Direction direction;
            
            this.connect();
            SqlDataReader result = this.select(data);

            while (result.Read())
            {
                direction = new Direction(
                    result.GetInt32(0),
                    result.GetString(1),
                    result.GetString(2)
                    );
                this.disconnect();
                return direction;
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

            

            data[NomDirectionColmn] = "'" + this.NomDirection + "'";
            data[LibelleColmn] = "'" + this.Libelle + "'";
            
            id[IdDirectionColmn] = "" + this.IdDirection;

            this.update(data, id);
            this.disconnect();
        }

    }
}