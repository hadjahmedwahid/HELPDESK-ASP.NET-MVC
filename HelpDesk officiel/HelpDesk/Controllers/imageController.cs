using HelpDesk.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HelpDesk.Controllers
{
    public class imageController : Controller
    {
        // GET: image
        public ActionResult Index()
        {
            foreach (string upload in Request.Files)
            {
                if (Request.Files[upload].FileName != "")
                {
                    string path = AppDomain.CurrentDomain.BaseDirectory + "/App_Data/uploads/";
                    string filename = Path.GetFileName(Request.Files[upload].FileName);
                    Request.Files[upload].SaveAs(Path.Combine(path, filename));
                }
            }
            return View("Upload");
        }

        public ActionResult Downloads()
        {
            var dir = new System.IO.DirectoryInfo(Server.MapPath("~/App_Data/uploads/"));
            System.IO.FileInfo[] fileNames = dir.GetFiles("*.*"); List<string> items = new List<string>();
            foreach (var file in fileNames)
            {
                items.Add(file.Name);
            }
            return View(items);
        }

        public FileResult Download(string ImageName)
        {
            var FileVirtualPath = "~/App_Data/uploads/" + ImageName;
            return File(FileVirtualPath, "application/force-download", Path.GetFileName(FileVirtualPath));
        }

        // GET: image/Create
        public ActionResult addimage()
        {
            PieceJoint b1 = new PieceJoint();

            List<file> list = new List<file>();



            const string connect = @"Server=WAHID;Database=HELPDESK3;Trusted_Connection=True;";

            using (var conn = new SqlConnection(connect))
            {
                var qry = "SELECT [IdPiece],[filename] FROM PieceJoint ";
                var cmd = new SqlCommand(qry, conn);
               
                conn.Open();
               SqlDataReader result  = cmd.ExecuteReader();
                while (result.Read())
            {
                    list.Add(new file(
                        result.GetInt32(0),
                        result.GetString(1)
                      ));
                 
                    
            }
            }


            ViewBag.list = list;
             
           




            return View(b1);
        }

        // POST: image/Create
        [HttpPost]
        public ActionResult addimage(PieceJoint model , HttpPostedFileBase image1)
        {
            var db = new HELPDESK3Entities();
            if(image1 != null)
            {
                
                model.img = new byte[image1.ContentLength];
                model.filename = image1.FileName;
                model.type = ""+image1.GetType();
                image1.InputStream.Read(model.img, 0, image1.ContentLength);
                
            }
            
            db.PieceJoint.Add(model);
            db.SaveChanges();
            ViewBag.filename = image1.FileName;
            List<file> list = new List<file>();

            const string connect = @"Server=WAHID;Database=HELPDESK3;Trusted_Connection=True;";

            using (var conn = new SqlConnection(connect))
            {
                var qry = "SELECT [IdPiece],[filename] FROM PieceJoint ";
                var cmd = new SqlCommand(qry, conn);

                conn.Open();
                SqlDataReader result = cmd.ExecuteReader();
                while (result.Read())
                {
                    list.Add(new file(
                        result.GetInt32(0),
                        result.GetString(1)
                      ));


                }
            }


            ViewBag.list = list;


            return View(model);
        }
        public FileContentResult GetFile(int id)
        {
            SqlDataReader rdr; byte[] fileContent = null;
            string mimeType = ""; string fileName = "";
            const string connect = @"Server=WAHID;Database=HELPDESK3;Trusted_Connection=True;";

            using (var conn = new SqlConnection(connect))
            {
                var qry = "SELECT [img] FROM PieceJoint WHERE IdPiece = @IdPiece";
                var cmd = new SqlCommand(qry, conn);
                cmd.Parameters.AddWithValue("@IdPiece", id);
                conn.Open();
                rdr = cmd.ExecuteReader();
                if (rdr.HasRows)
                {
                    rdr.Read();
                    fileContent = (byte[])rdr["img"];
                //    mimeType = rdr["MimeType"].ToString();
                 //   fileName = rdr["FileName"].ToString();
                }
            }
            return File(fileContent,"png");
        }


    }
}
