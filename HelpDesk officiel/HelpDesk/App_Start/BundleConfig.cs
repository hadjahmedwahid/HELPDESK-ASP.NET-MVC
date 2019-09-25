using System.Web;
using System.Web.Optimization;

namespace HelpDesk
{
    public class BundleConfig
    {
        // Pour plus d'informations sur le regroupement, visitez http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-3.1.1.js",
                        "~/Scripts/jquery-3.1.1.min.js",
                       "~/Scripts/jquery.unobtrusive-ajax.min.js"
                        ));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Utilisez la version de développement de Modernizr pour le développement et l'apprentissage. Puis, une fois
            // prêt pour la production, utilisez l'outil de génération (bluid) sur http://modernizr.com pour choisir uniquement les tests dont vous avez besoin.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/bootstrap.min.js",
                      "~/Scripts/respond.js"));
            bundles.Add(new ScriptBundle("~/bundles/javascript").Include(
                     "~/Scripts/Template_Script/Chart.js",
                     "~/Scripts/Template_Script/classie.js",
                     "~/Scripts/Template_Script/clndr.js",
                     "~/Scripts/Template_Script/custom.js",
                     "~/Scripts/Template_Script/metisMenu.min.js",
                     "~/Scripts/Template_Script/modernizr.custom.js",
                     "~/Scripts/Template_Script/moment-2.2.1.js",
                     "~/Scripts/Template_Script/scripts.js",
                     "~/Scripts/Template_Script/site.js",
                     "~/Scripts/Template_Script/skycons.js",
                     "~/Scripts/Template_Script/underscore-min.js"
                     

                ));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/Site.css",
                      "~/Content/TemplateStyle/clndr.css",
                      "~/Content/TemplateStyle/custom.css",
                      "~/Content/TemplateStyle/font-awesome.css",
                      "~/Content/TemplateStyle/jqvmap.css",
                      "~/Content/TemplateStyle/style.css",
                      "~/Content/TemplateStyle/animate.css"
                 ));
        }
    }
}
