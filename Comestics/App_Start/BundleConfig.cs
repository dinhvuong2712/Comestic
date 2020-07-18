using System.Web;
using System.Web.Optimization;

namespace Comestics
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new Bundle("~/Scripts/ajax/updatecart").Include(
                                    "~/Scripts/ajax/updatecart.js"
                ));
            bundles.Add(new Bundle("~/Scripts/ajax/CheckUser").Include(
                                    "~/Scripts/ajax/CheckUser.js"
                ));
            bundles.Add(new Bundle("~/Scripts/ajax/PagedListPR").Include(
                                    "~/Scripts/ajax/PagedListPR.js"
                ));
        }
    }
}
