using System.Web.Optimization;

using BundleNames = V6Soft.Web.Accounting.Constants.Names.Bundles;


namespace V6Soft.Web.Accounting.Configs
{
    public class BundleConfig
    {
        /// <summary>
        ///     Registers bundling and optimizations.
        /// </summary>
        /// <param name="bundles"></param>
        public static void Register(BundleCollection bundles)
        {
            RegisterScriptBundles(bundles);
            RegisterStyleBundles(bundles);
        }

        private static void RegisterScriptBundles(BundleCollection bundles)
        {
            const string Script = "~/Scripts/",
                PluginScript = "~/Plugins/";
            
            bundles.Add(new ScriptBundle(BundleNames.Scripts.Core).Include(
                //Script + "core/modernizr-{version}.js",
                Script + "core/jquery/jquery-{version}.js",
                //Script + "core/jquery/jquery-migrate-{version}.js",
                Script + "core/underscore/underscore-{version}.js"
                //Script + "core/underscore/underscore.string-{version}.js"
            ));

            bundles.Add(new ScriptBundle(BundleNames.Scripts.Lib).Include(
                Script + "lib/bootstrap/bootstrap-{version}.js",
                Script + "lib/bootstrap/bootstrap-modal.js",
                Script + "lib/bootstrap/bootstrap-modalmanager.js",
                PluginScript + "bootstrap-hover-dropdown/bootstrap-hover-dropdown.min.js",
                PluginScript + "jquery-slimscroll/jquery.slimscroll.min.js",
                PluginScript + "jquery.blockui.min.js",
                PluginScript + "jquery.cokie.min.js",
                //PluginScript + "uniform/jquery.uniform.min.js",
                PluginScript + "bootstrap-datepicker/js/bootstrap-datepicker.min.js",
                PluginScript + "bootstrap-datepicker/locales/bootstrap-datepicker.vi.min.js",

                Script + "lib/angularjs/angular-{version}.js",
                Script + "lib/angularjs/angular-sanitize.min.js",
                Script + "lib/angularjs/angular-touch.min.js",
                Script + "lib/angularjs/i18n/angular-locale_vi-vn.js",
                Script + "lib/angularjs/plugins/angular-ui-router-{version}.js",
                Script + "lib/angularjs/plugins/ocLazyLoad.min.js",
                Script + "lib/q.min.js",
                //Script + "lib/typeahead/bloodhound.js",
                //Script + "lib/typeahead/typeahead.bundle.js",
                //Script + "lib/breeze/breeze.debug.js",
                //Script + "lib/breeze/breeze.directives.js",
                //Script + "lib/breeze/breeze.bridge.angular.js",
                //Script + "lib/angularjs-ui/ui-bootstrap-{version}.js",
                //Script + "lib/angularjs-ui/ui-bootstrap-tpls-{version}.js",
                //Script + "lib/angularjs-ui/ui-grid.js"
                PluginScript + "kendo/kendo.all.min.js"
            ));

            bundles.Add(new ScriptBundle(BundleNames.Scripts.App).Include(
                Script + "app/constants.js",
                Script + "app/app.module.js",
                Script + "app/app.config.js",
                Script + "app/app.route.js",
                Script + "app/app.js",
                Script + "app/controls/lookup-modal.js",
                Script + "app/shared/shared.module.js",
                //Script + "app/modules/components/v6Grid/breezeDatasource.js",
                //Script + "app/modules/components/component.module.js",
                Script + "app/shared/directives/ngSpinnerBar.js",
                //Script + "core/breezeDataSource/BreezeDataSource",
                //Script + "app/modules/components/v6Grid/v6Grid.js",
                //Script + "app/modules/components/v6Column/v6Column.js",
                //Script + "app/shared/directives/v6Grid.js",
                //Script + "app/shared/directives/v6Form.js",
                //Script + "app/shared/directives/v6FlexInput.js",
                

                //Script + "app/shared/directives/v6TypeAhead.js",
                //Script + "app/shared/directives/dumGrid.js",

                //Script + "app/shared/factories/gridOptions.js",
                //Script + "app/shared/factories/bzEntityManager.js",
                //Script + "app/shared/factories/bzDataService.js",
                //Script + "app/shared/factories/dataServiceFactory.js",
                Script + "app/shared/factories/logger.js",
                Script + "app/shared/factories/httpDataService.js",


                //Script + "app/modules/component/component.module.js",
                //Script + "app/modules/dashboard/dashboard.module.js",
                //Script + "app/modules/dashboard/dashboard.js",
                //Script + "app/modules/customer/customer.js",
                //Script + "app/modules/customer/v6-customer-detail.js",
                //Script + "app/modules/receipt/receipt.js",
                //Script + "app/modules/receipt/v6-receipt-detail.js",
                //Script + "app/modules/components/v6Grid/v6Grid.js",
                //Script + "app/modules/components/v6GridColumn.js",

                Script + "lib/metronic/metronic.js",
                PluginScript + "layout3/layout.js",
                PluginScript + "layout3/demo.js"
            ));
        }

        private static void RegisterStyleBundles(BundleCollection bundles)
        {
            const string RsrcStyle = "~/Content/css/",
                PluginStyle = "~/Plugins/";

            bundles.Add(new StyleBundle(BundleNames.Styles.Core).Include(
                RsrcStyle + "helper/font-awesome/font-awesome.css",
                //RsrcStyle + "helper/angularjs-ui/ui-grid.css",
                RsrcStyle + "layout/bootstrap/*.css",
                PluginStyle + "simple-line-icons/simple-line-icons.css",
                //PluginStyle + "uniform/css/uniform.default.css",
                PluginStyle + "bootstrap-datepicker/css/bootstrap-datepicker3.min.css"
            ));

            bundles.Add(new StyleBundle(BundleNames.Styles.Theme).Include(
                RsrcStyle + "layout/metro/components-md.css",
                //RsrcStyle + "layout/metro/components-rounded.css",
                RsrcStyle + "layout/metro/plugins-md.css",
                RsrcStyle + "layout/metro/layout.css",
                //RsrcStyle + "layout/metro/themes/default.css",
                PluginStyle + "kendo/kendo.common.min.css",
                PluginStyle + "kendo/kendo.material.min.css"
                //RsrcStyle + "layout/metro/custom.css"
            ));

        }
    }
}