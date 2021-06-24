using System.Web;
using System.Web.Optimization;

namespace Web
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            // --- Script ---
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                       "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*", "~/Scripts/jquery.unobtrusive-ajax.min.js"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            //CORE PLUGINS
            bundles.Add(new ScriptBundle("~/bundles/core-plugins").Include(
                      "~/assets/global/plugins/jquery.min.js",
                      "~/assets/global/plugins/bootstrap/js/bootstrap.min.js",
                      "~/assets/global/plugins/js.cookie.min.js",
                      "~/assets/global/plugins/jquery-slimscroll/jquery.slimscroll.min.js",
                      "~/assets/global/plugins/jquery.blockui.min.js",
                      "~/assets/global/plugins/bootstrap-switch/js/bootstrap-switch.min.js"));

            //THEME LAYOUT SCRIPTS
            bundles.Add(new ScriptBundle("~/bundles/theme-layout").Include(
                      "~/assets/layouts/layout/scripts/layout.min.js",
                      "~/assets/layouts/layout/scripts/demo.min.js",
                      "~/assets/layouts/global/scripts/quick-sidebar.min.js",
                      "~/assets/layouts/global/scripts/quick-nav.min.js"));

            //GLOBAL MANDATORY SCRIPTS
            bundles.Add(new ScriptBundle("~/bundles/mandatory-plugins").Include(
                       "~/Scripts/bootstrap-datetimepicker.js",
                       //"~/assets/global/plugins/select2/js/select2.min.js",
                       //"~/assets/global/plugins/select2/js/select2.full.js",
                       "~/assets/global/plugins/bootstrap-toastr/toastr.min.js",
                       "~/assets/global/plugins/bootstrap-sweetalert/sweetalert.min.js",
                       "~/assets/global/plugins/icheck/icheck.min.js",
                       "~/assets/global/plugins/bootstrap-datepicker/js/bootstrap-datepicker.min.js",
                       "~/assets/global/plugins/bootstrap-daterangepicker/daterangepicker.min.js",
                       "~/assets/global/plugins/bootstrap-datetimepicker/js/bootstrap-datetimepicker.min.js", 
                       "~/assets/global/plugins/bootstrap-modal/js/bootstrap-modalmanager.js",
                       "~/assets/global/plugins/bootstrap-modal/js/bootstrap-modal.js",
                       "~/assets/pages/scripts/ui-extended-modals.min.j",
                       "~/assets/global/plugins/fancybox/source/jquery.fancybox.pack.js",
                       "~/Scripts/moment.min.js"));

                        //Angular
                        bundles.Add(new ScriptBundle("~/bundles/angular-js").Include(
                       "~/assets/global/plugins/angularjs/angular.min.js",
                       "~/assets/global/plugins/angularjs/angular-sanitize.min.js",
                       "~/assets/global/plugins/angularjs/plugins/ui-bootstrap-tpls.min.js",
                       //"~/assets/global/plugins/angularjs/angular-ui-validate.js",
                       "~/assets/global/plugins/jquery-file-upload/js/jquery.fileupload-angular.js",
                       "~/assets/global/plugins/angularjs/angular-filter.js"));

            // --- CSS ---
            //GLOBAL MANDATORY STYLES
            bundles.Add(new StyleBundle("~/css/mandatory").Include(
                      "~/assets/global/plugins/font-awesome/css/font-awesome.min.css",
                      "~/assets/global/plugins/simple-line-icons/simple-line-icons.min.css",
                      "~/assets/global/plugins/bootstrap/css/bootstrap.min.css",
                      "~/assets/global/plugins/bootstrap-switch/css/bootstrap-switch.min.css",
                      "~/assets/global/plugins/select2/css/select2.min.css",
                      "~/assets/global/plugins/select2/css/select2-bootstrap.min.css",
                      "~/Content/bootstrap-datetimepicker.css",
                      "~/assets/global/plugins/bootstrap-toastr/toastr.min.css",
                      "~/assets/global/plugins/bootstrap-sweetalert/sweetalert.css",
                      "~/assets/global/plugins/bootstrap-datepicker/css/bootstrap-datepicker.min.css",
                      "~/assets/global/plugins/bootstrap-daterangepicker/daterangepicker.css",
                      "~/assets/global/plugins/bootstrap-datetimepicker/css/bootstrap-datetimepicker.min.css",
                      "~/assets/global/plugins/fancybox/source/jquery.fancybox.csss",
                      "~/assets/global/plugins/icheck/skins/all.css",
                      "~/assets/global/css/plugins.css",
                      "~/assets/global/css/plugins.min.css"));

            //THEME GLOBAL STYLES
            bundles.Add(new StyleBundle("~/css/theme-global").Include(
                      "~/assets/global/css/components-md.min.css",
                      "~/assets/global/css/plugins-md.min.css"));

            //THEME LAYOUT STYLES
            bundles.Add(new StyleBundle("~/css/theme-layout").Include(
                      "~/assets/layouts/layout/css/layout.min.css",
                      "~/assets/layouts/layout/css/themes/darkblue.min.css",
                      "~/assets/layouts/layout/css/custom.min.css",
                      "~/Content/Site.css"));

            //THEME LAYOUT STYLES
            bundles.Add(new StyleBundle("~/css/consumer-account-details").Include(
                      "~/assets/pages/css/profile.min.css"
                      ));


            /*bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));*/

            #region Custom

            // File upload
            bundles.Add(new StyleBundle("~/css/fileupload").Include(
                    "~/assets/global/plugins/jquery-file-upload/css/jquery.fileupload.css",
                    "~/assets/global/plugins/jquery-file-upload/css/jquery.fileupload-ui.css",
                    "~/assets/global/plugins/jquery-file-upload/blueimp-gallery/blueimp-gallery.min.css"));

            bundles.Add(new ScriptBundle("~/bundles/fileupload").Include(
                    "~/assets/global/plugins/jquery-file-upload/js/vendor/jquery.ui.widget.js",
                    "~/assets/global/plugins/jquery-file-upload/js/vendor/load-image.min.js",
                    "~/assets/global/plugins/jquery-file-upload/js/vendor/canvas-to-blob.min.js",
                    "~/assets/global/plugins/jquery-file-upload/blueimp-gallery/jquery.blueimp-gallery.min.js",
                    "~/assets/global/plugins/jquery-file-upload/js/jquery.iframe-transport.js",
                    "~/assets/global/plugins/jquery-file-upload/js/jquery.fileupload.js",
                    "~/assets/global/plugins/jquery-file-upload/js/jquery.fileupload-process.js",
                    "~/assets/global/plugins/jquery-file-upload/js/jquery.fileupload-image.js",
                    "~/assets/global/plugins/jquery-file-upload/js/jquery.fileupload-audio.js",
                    "~/assets/global/plugins/jquery-file-upload/js/jquery.fileupload-video.js",
                    "~/assets/global/plugins/jquery-file-upload/js/jquery.fileupload-validate.js",
                    "~/assets/global/plugins/jquery-file-upload/js/jquery.fileupload-ui.js"));


            //DatePicker
            bundles.Add(new StyleBundle("~/css/datepicker").Include(
                     "~/assets/global/plugins/bootstrap-datepicker/css/bootstrap-datepicker.min.css"));
            bundles.Add(new ScriptBundle("~/bundles/datepicker").Include(
                      "~/assets/global/plugins/bootstrap-datepicker/js/bootstrap-datepicker.min.js"));

            //DateRangePicker
            bundles.Add(new StyleBundle("~/css/daterangepicker").Include(
                     "~/assets/global/plugins/daterangepicker/daterangepicker.min.css"));
            bundles.Add(new ScriptBundle("~/bundles/daterangepicker").Include(
                      "~/assets/global/plugins/daterangepicker/daterangepicker.min.js",
                      "~/Scripts/angular/angular-daterangepicker.min.js"));

            // jasnyBootstrap styles
            bundles.Add(new StyleBundle("~/css/jasny").Include(
                "~/assets/global/plugins/jasny/jasny-bootstrap.min.css"));
            bundles.Add(new ScriptBundle("~/bundles/jasny").Include(
                "~/assets/global/plugins/jasny/jasny-bootstrap.min.js"));

            #endregion

            BundleTable.EnableOptimizations = true;
        }
    }
}
