using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HelpDesk.Models
{
    public class file
    {
        public int idf { get; set; }
        public String nf { get; set; }


        public file(int id , String nom)
        {
            idf = id;
            nf = nom;


        }
    }
}