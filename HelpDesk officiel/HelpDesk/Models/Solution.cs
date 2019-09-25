using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace HelpDesk.Models
{
    public class Solution :HelpDeskDB
    {
        public int IdSolution { get; set; }
        public String Description  { get; set; }
        public Probleme probleme { get; set; }

        public const string IdSolutionColmn = "IdSolution";
        public const string DescreptionSolutionColmn = "DescriptionSolution";
        public const string IdProblemeColmn = "IdProbleme";

        // Constructeur 1 :
        public Solution()
        {
            this.table = "Solution";
        }

        // Constructeur 2:
        public Solution(int idSolution,  string descriptionSolution, Probleme probleme)
        {
            this.table = "Solution";

            this.IdSolution = idSolution;
            this.Description = descriptionSolution;
            this.probleme = probleme;
        }

        
        public void save()
        {
            this.connect();

            Dictionary<string, string> data = new Dictionary<string, string>();
            data[DescreptionSolutionColmn] = "'" + this.Description + "'";
            data[IdProblemeColmn] = "" + this.probleme.IdProbleme;
            
            this.insert(data);
            this.disconnect();

        }

        public List<Solution> find_all()
        {
            List<Solution> solutions = new List<Solution>();
            Dictionary<string, string> id = new Dictionary<string, string>();
            this.probleme = new Probleme();

            this.connect();
            SqlDataReader result = this.select();
            while (result.Read())
            {
                id["IdProbleme"] = "" + result.GetInt32(2);
                this.probleme = this.probleme.find_by_id(id);

                solutions.Add(new Solution(
                    result.GetInt32(0),
                    result.GetString(1),
                    this.probleme
                    ));
            }
            this.disconnect();
            
            return solutions;
            
        }

        public Solution find_by_id(Dictionary<string, string> data)
        {
            Solution solution;
            Dictionary<string, string> id = new Dictionary<string, string>();
            Dictionary<string, string> id2 = new Dictionary<string, string>();
            this.probleme = new Probleme();

            this.connect();
            id2[IdSolutionColmn] = "" + this.IdSolution;
            SqlDataReader result = this.select(id2);
            while (result.Read())
            {
                id["IdProbleme"] = "" + result.GetInt32(2);
                this.probleme = this.probleme.find_by_id(id);

                solution = new Solution(
                    result.GetInt32(0),
                    result.GetString(1),
                    this.probleme
                    );
                this.disconnect();

                return solution;
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


            
            data[DescreptionSolutionColmn] = "'" + this.Description + "'";
            data[IdProblemeColmn] = "" + this.probleme.IdProbleme;

            id[IdSolutionColmn] = "" + this.IdSolution;

            this.update(data, id);
            this.disconnect();
        }

    }
}