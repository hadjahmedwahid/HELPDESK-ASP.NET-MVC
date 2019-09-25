using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HelpDesk.Models
{
    public class Agence : HelpDeskDB
    {
        
        [Required]
        [Key]
        [Display(Name = "Numéro d'Agence")]
        public int IdAgence { get; set; }

        
        [Display(Name = "Wilaya d'Agence")]
        public String WilayaAgence { get; set; }

        [Display(Name = "DRE")]
        public DRE Dre { get; set; }

        
        [Display(Name = "Adresse d'Agence")]
        public String AdresseAgence { get; set; }

        
        [Display(Name = "Nom d'Agence")]
        public String NomAgence { get; set; }

        IEnumerable<Agence> List { get; set; }

        public const string IdAgenceColmn = "[NumeroAgence]";
        public const string WilayaAgenceColmn = "[Wilaya]";
        public const string AdresseAgenceColmn = "[adresseAgence]";
        public const string NomAgenceColmn = "[NomAgence]";
        public const string NumeroDREColmn = "[NumeroDRE]";

        public Agence()
        {
            this.table = "Agence";
        }

        public Agence(int idAgence, string wilaya,int numeroDRE,string adresseAgence,string nomAgence) {
            this.table = "Agence";

            this.IdAgence = idAgence;
            this.WilayaAgence = wilaya;
            this.Dre.NumeroDRE = numeroDRE;
            this.AdresseAgence = adresseAgence;
            this.NomAgence = nomAgence;  
        }

        public Agence(int idAgence, string wilaya, DRE dre, string adresseAgence, string nomAgence)
        {
            this.table = "Agence";

            this.IdAgence = idAgence;
            this.WilayaAgence = wilaya;
            this.Dre = dre;
            this.AdresseAgence = adresseAgence;
            this.NomAgence = nomAgence;
        }

        public void save()
        {
            this.connect();

            Dictionary<string, string> data = new Dictionary<string, string>();
            
            data[IdAgenceColmn] = "" + this.IdAgence;
            data[WilayaAgenceColmn] = "'" + this.WilayaAgence + "'";
            data[NumeroDREColmn] = "" + this.Dre.NumeroDRE;
            data[AdresseAgenceColmn] = "'" + this.AdresseAgence + "'";
            data[NomAgenceColmn] = "'" + this.NomAgence + "'";

            this.insert(data);
            this.disconnect();
        }

        public List<Agence> find_all()
        {
            List<Agence> agences = new List<Agence>();
            Dictionary<string, string> id = new Dictionary<string, string>();
            this.Dre = new DRE();

            this.connect();
            SqlDataReader result = this.select();
            while (result.Read())
            {
                id[NumeroDREColmn] = "" + result.GetInt32(2);
                this.Dre = this.Dre.find_by_id(id);

                agences.Add(new Agence(
                    result.GetInt32(0),
                    result.GetString(1),
                    this.Dre,
                    result.GetString(3),
                    result.GetString(4)
                    ));
            }
            this.disconnect();

            return agences;
        }

        public Agence find_by_id(Dictionary<string, string> data)
        {
            Agence agence;
            Dictionary<string, string> id = new Dictionary<string, string>();
            this.Dre = new DRE();

            
            this.connect();
            SqlDataReader result = this.select();
            while (result.Read())
            {
                id[NumeroDREColmn] = "" + result.GetInt32(2);
                this.Dre = this.Dre.find_by_id(id);

                agence = new Agence(
                    result.GetInt32(0),
                    result.GetString(1),
                    this.Dre,
                    result.GetString(3),
                    result.GetString(4)
                    );
                this.disconnect();
                return agence;
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

            

            data[WilayaAgenceColmn] = "'" + this.WilayaAgence + "'";
            data[NumeroDREColmn] = "" + this.Dre.NumeroDRE;
            data[AdresseAgenceColmn] = "'" + this.AdresseAgence + "'";
            data[NomAgenceColmn] = "'" + this.NomAgence + "'";

            id[IdAgenceColmn] = "" + this.IdAgence;

            this.update(data, id);
            this.disconnect();
        }

    }
}