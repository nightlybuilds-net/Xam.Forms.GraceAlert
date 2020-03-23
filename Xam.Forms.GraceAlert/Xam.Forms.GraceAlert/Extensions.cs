using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Xam.Forms.GraceAlert
{
    public static class Extensions
    {
        /// <summary>
        /// Return the first instance of GraceAlrtView found on this page
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        public static GraceAlertView GraceAlert(this Page page)
        {
            return page.LogicalChildren.OfType<GraceAlertView>().FirstOrDefault();
        }
    }
}