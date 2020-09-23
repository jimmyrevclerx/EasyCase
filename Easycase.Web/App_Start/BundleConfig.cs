using System.Web;
using System.Web.Optimization;

namespace Easycase.Web
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));


            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js"));

            bundles.Add(new ScriptBundle("~/bundles/assets").Include(
                      "~/Content/assets/libs/jquery/dist/jquery.min.js",
                      "~/Content/assets/libs/popper.js/dist/umd/popper.min.js",
                      "~/Content/assets/libs/bootstrap/dist/js/bootstrap.min.js",
                      "~/Content/assets/libs/perfect-scrollbar/dist/perfect-scrollbar.jquery.min.js",
                      "~/Content/assets/extra-libs/sparkline/sparkline.js",
                      "~/Content/dist/js/waves.js",
                      "~/Content/dist/js/sidebarmenu.js",
                      "~/Content/dist/js/custom.min.js",
                      "~/Content/assets/libs/flot/excanvas.js",
                      "~/Content/assets/libs/flot/jquery.flot.js",
                      "~/Content/assets/libs/flot/jquery.flot.pie.js",
                      "~/Content/assets/libs/flot/jquery.flot.time.js",
                      "~/Content/assets/libs/flot/jquery.flot.stack.js",
                      "~/Content/assets/libs/flot/jquery.flot.crosshair.js",
                      "~/Content/assets/libs/flot.tooltip/js/jquery.flot.tooltip.min.js",
                      "~/Content/dist/js/pages/chart/chart-page-init.js",
                      "~/Content/assets/libs/toastr/build/toastr.min.js",
                      "~/Content/assets/libs/bootstrap-datepicker/dist/js/bootstrap-datepicker.min.js",
                      "~/Content/assets/libs/inputmask/dist/min/jquery.inputmask.bundle.min.js",
                      "~/Content/dist/js/pages/mask/mask.init.js",
                      "~/Content/assets/libs/select2/dist/js/select2.full.min.js",
                      "~/Content/assets/libs/bootstrap/dist/js/bootstrap-multiselect.js",
                      "~/Content/assets/libs/select2/dist/js/select2.min.js",
                      "~/Content/assets/libs/jquery-steps/build/jquery.steps.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/calendar").Include(
                      "~/Content/dist/js/pages/calendar/main.js"));

            bundles.Add(new ScriptBundle("~/bundles/datatables").Include(
                     "~/Content/assets/extra-libs/multicheck/datatable-checkbox-init.js",
                     "~/Content/assets/extra-libs/multicheck/jquery.multicheck.js",
                     "~/Content/assets/extra-libs/DataTables/datatables.min.js"));

            bundles.Add(new StyleBundle("~/Content/assets").Include(
                      "~/Content/assets/libs/flot/css/float-chart.css",
                      "~/Content/dist/css/style.min.css",
                      "~/Content/assets/libs/toastr/build/toastr.min.css",
                      "~/Content/assets/libs/bootstrap-datepicker/dist/css/bootstrap-datepicker.min.css",
                      "~/Content/assets/libs/select2/dist/css/select2.min.css",
                      "~/Content/assets/libs/bootstrap/dist/css/bootstrap-multiselect.css",
                      "~/Content/assets/libs/jquery-steps/jquery.steps.css",
                      "~/Content/assets/libs/jquery-steps/steps.css",
                      "~/Content/custom.css"));

            bundles.Add(new StyleBundle("~/Content/datatable").Include(
                      "~/Content/assets/extra-libs/multicheck/multicheck.css",
                      "~/Content/assets/libs/datatables.net-bs4/css/dataTables.bootstrap4.css"));

            bundles.Add(new StyleBundle("~/Content/calendar").Include(
                      "~/Content/dist/js/pages/calendar/main.css"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css"));
        }
    }
}
