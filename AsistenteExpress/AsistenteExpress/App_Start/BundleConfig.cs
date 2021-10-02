using System.Web;
using System.Web.Optimization;

namespace AsistenteExpress
{
    public class BundleConfig
    {
        // Para obtener más información sobre las uniones, visite https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Utilice la versión de desarrollo de Modernizr para desarrollar y obtener información. De este modo, estará
            // para la producción, use la herramienta de compilación disponible en https://modernizr.com para seleccionar solo las pruebas que necesite.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));

            bundles.Add(new StyleBundle("~/Content/Kendo").Include(
                      "~/Content/Kendo/kendo.common.min.css",
                      "~/Content/Kendo/kendo.default.min.css",
                      "~/Content/Kendo/kendo.default.mobile.min.css"));

            bundles.Add(new ScriptBundle("~/bundles/Kendo").Include(
                     "~/Scripts/Kendo/kendo.all.min.js",
                     "~/Scripts/Kendo/jszip.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/SweetAlert").Include(
                     "~/Scripts/utils/SweetAlert2/dist/sweetalert2.all.js"));

            bundles.Add(new StyleBundle("~/Content/SweetAlert").Include(
                     "~/Scripts/utils/SweetAlert2/dist/sweetalert2.min.css"));
        }
    }
}
