using System.Web;
using System.Web.Optimization;

namespace Feeddit
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js", "~/Scripts/jquery-ui-1.12.1.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));


            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));
            
            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                    "~/Scripts/popper.min.js",
                    "~/Scripts/bootstrap.min.js",
                    "~/Scripts/jquery.dataTables.min.js",
                    "~/Scripts/dataTables.bootstrap4.min.js",
                    "~/Scripts/dataTables.buttons.min.js",
                    "~/Scripts/buttons.bootstrap4.min.js",
                    "~/Scripts/vfs_fonts.js",
                    "~/Scripts/buttons.html5.min.js",
                    "~/Scripts/respond.js",
                    "~/Scripts/datepicker.js",
                    "~/Scripts/select2.min.js", "~/Scripts/toastr.min.js",
                    "~/Scripts/scripts.js"));


            bundles.Add(new StyleBundle("~/Content/css").Include(
                   "~/Content/bootstrap.min.css", "~/Content/jquery-ui.css",
                   "~/Content/all.min.css",
                    "~/Content/style.css",
                   "~/Content/dataTables.bootstrap4.min.css", "~/Content/PagedList.css",
                   "~/Content/select2.min.css", "~/Content/toastr.min.css",
                   "~/Content/datepicker.css"));
        }
    }
}
