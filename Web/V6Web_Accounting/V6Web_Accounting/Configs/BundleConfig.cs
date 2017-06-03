using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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
            const string RsrcScript = "~/mod/resource/Scripts/",
                Script = "~/Scripts/";
            
            bundles.Add(new ScriptBundle(BundleNames.Scripts.Core).Include(
                RsrcScript + "core/*.js",
                RsrcScript + "core/jquery/*.js",
                RsrcScript + "core/underscore/*.js"
            ));

            bundles.Add(new ScriptBundle(BundleNames.Scripts.Lib).Include(
                RsrcScript + "lib/angularjs/*.js",
                RsrcScript + "lib/angularjs/i18n/angular-locale_vi-vn.js",
                RsrcScript + "lib/bootstrap/*.js",
                RsrcScript + "lib/flot/*.js",
                RsrcScript + "lib/jquery-ui/*.js",
                RsrcScript + "lib/jquery-validate/*.js",
                RsrcScript + "lib/tablesorter/*.js",
                RsrcScript + "lib/jquery.pjax.js"
            ));

            bundles.Add(new ScriptBundle(BundleNames.Scripts.App).Include(
                Script + "global/*.js",
                RsrcScript + "app/*.js",
                RsrcScript + "app/services/*.js",
                Script + "modules/Menu/models/*.js",
                Script + "modules/Menu/services/*.js",
                Script + "modules/Menu/controllers/*.js",
                Script + "modules/Menu/directives/*.js",
                Script + "modules/Menu/module_starter.js"
            ));
        }

        private static void RegisterStyleBundles(BundleCollection bundles)
        {
            const string RsrcStyle = "~/mod/resource/Content/css/";

            bundles.Add(new StyleBundle(BundleNames.Styles.Core).Include(
                RsrcStyle + "layout/bootstrap/*.css",
                //RsrcStyle + "themes/bootstrap/*.css",
                RsrcStyle + "helper/tablesorter/*.css",
                RsrcStyle + "themes/sbAdmin/sb-admin.css",
                RsrcStyle + "helper/font-awesome/font-awesome.css",
                "~/Content/css/layout/structure.css"
            ));
        }
    }
}