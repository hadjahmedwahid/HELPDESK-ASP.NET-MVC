using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.ComponentModel.DataAnnotations;
namespace HelpDesk.Models
{
    public class DRE : HelpDeskDB
    {
        
        [Key]
        [Display(Name = "Numéro DRE")]
        public int NumeroDRE { get; set; }

        
        [Display(Name = "Nom DRE")]
        public String NomDRE { get; set; }

        
        [Display(Name = "Wilaya de DRE")]
        public String Wilaya { get; set; }

        
        [Display(Name = "Adresse DRE")]
        public String AdresseDRE { get; set; }
        
        public List<Agence> agences { get; set; }

        // DB Attributs : 
        public const string IdDREColmn = "[NumeroDRE]";
        public const string NomDREColmn = "[NomDRE]";
        public const string wilayacolomn = "[Wilaya]";
        public const string addresscolomn = "[AdresseDRE]";

        public DRE()
        {
            this.table = "DRE";
        }

        public DRE(int numeroDRE, string wilaya, string adresseDRE, string nomDRE)
        {
            this.table = "DRE";

            this.NumeroDRE = numeroDRE;
            this.NomDRE = nomDRE;
            this.Wilaya = wilaya;
            this.AdresseDRE = adresseDRE;
        }

        public void save()
        {
            this.connect();

            Dictionary<string, string> data = new Dictionary<string, string>();
            

            data[IdDREColmn] = "" + this.NumeroDRE;
            data[wilayacolomn] = "'" + this.Wilaya + "'";
            data[addresscolomn] = "'" + this.AdresseDRE + "'";
            data[NomDREColmn] = "'" + this.NomDRE + "'";
            

            this.insert(data);
            this.disconnect();

        }

        public List<DRE> find_all() {
            List<DRE> DRES = new List<DRE>();
            
            this.connect();
            SqlDataReader result = this.select();
            while (result.Read())
            {
                DRES.Add(new DRE(
                    result.GetInt32(0),
                    result.GetString(1),
                    result.GetString(2),
                    result.GetString(3)
                    ));
            }
            this.disconnect();
            return DRES;
        }

        public DRE find_by_id(Dictionary<string, string> data)
        {
           DRE dre1 ;
            this.table = "DRE";
            this.connect();
            SqlDataReader result = this.select(data);

            while (result.Read())
            {
                dre1 = new DRE(
                    result.GetInt32(0),
                    result.GetString(1),
                    result.GetString(2),
                    result.GetString(3)
                    );
                this.disconnect();
                return dre1;
            }
            return null;
        }

        public List<Agence> find_agence()
        {
            this.agences = new List<Agence>();
            Dictionary<string, string> id = new Dictionary<string, string>();
            id["NumeroDRE"] = "" + this.NumeroDRE;

            Agence agence = new Agence();
            agence.connect();
            SqlDataReader result = agence.select(id);

            while (result.Read())
            {

                this.agences.Add(new Agence(
                    result.GetInt32(0),
                    result.GetString(1),
                    this,
                    result.GetString(3),
                    result.GetString(4)
                    ));

            }
            agence.disconnect();
            return this.agences;
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
                       
            data[wilayacolomn] = "'" + this.Wilaya + "'";
            data[addresscolomn] = "'" + this.AdresseDRE + "'";
            data[NomDREColmn] = "'" + this.NomDRE + "'";

            id[IdDREColmn] = "" + this.NumeroDRE;

            this.update(data, id);
            this.disconnect();
        }


    }
}