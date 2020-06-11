using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;

namespace Xam.Forms.GraceAlert
{
    public static class Extensions
    {
        /// <summary>
        /// true if page is in  potrait
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        public static bool IsPotrait(this Page page)
        {
            return page.Height > page.Width;
        }

        /// <summary>
        /// Return the first instance of GraceAlrtView found on this page
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        public static GraceAlertView GraceAlert(this Page page)
        {
            var view = page.LogicalChildren.OfType<GraceAlertView>().FirstOrDefault();
            if (view == null)
            {
                Debug.WriteLine("GraceAlertView not found on page");
                return null;
            }

            // add pixel for not safe area on ios
            AdjustForIos(view, page);
            return view;
        }

        /// <summary>
        /// Hide grace alert
        /// </summary>
        public static void HideGrace(this Page page)
        {
            var graceAlert = page.GraceAlert();
            graceAlert.Hide();
            graceAlert.Hide();
        }

        /// <summary>
        /// Show error using graceview on this page
        /// </summary>
        /// <param name="page"></param>
        /// <param name="title"></param>
        /// <param name="text"></param>
        /// <param name="block"></param>
        /// <returns></returns>
        public static async Task Error(this Page page, string title, string text, bool block = false)
        {
            var graceAlert = page.GraceAlert();
            if (graceAlert == null)
            {
                Debug.WriteLine("GraceAlert not found on page");
                return;
            }

            await graceAlert.Show(NotificationType.Error, title, text, block);
        }

        /// <summary>
        /// Show warning using graceview on this page
        /// </summary>
        /// <param name="page"></param>
        /// <param name="title"></param>
        /// <param name="text"></param>
        /// <returns></returns>
        public static async Task Warning(this Page page, string title, string text, bool block = false)
        {
            var graceAlert = page.GraceAlert();
            if (graceAlert == null)
            {
                Debug.WriteLine("GraceAlert not found on page");
                return;
            }

            await graceAlert.Show(NotificationType.Warning, title, text,block);
        }

        /// <summary>
        /// Show Info using graceview on this page
        /// </summary>
        /// <param name="page"></param>
        /// <param name="title"></param>
        /// <param name="text"></param>
        /// <returns></returns>
        public static async Task Info(this Page page, string title, string text, bool block = false)
        {
            var graceAlert = page.GraceAlert();
            if (graceAlert == null)
            {
                Debug.WriteLine("GraceAlert not found on page");
                return;
            }

            await graceAlert.Show(NotificationType.Info, title, text,block);
        }
        
        /// <summary>
        /// Show Info using graceview on this page
        /// </summary>
        /// <param name="page"></param>
        /// <param name="title"></param>
        /// <param name="text"></param>
        /// <returns></returns>
        public static async Task Success(this Page page, string title, string text, bool block = false)
        {
            var graceAlert = page.GraceAlert();
            if (graceAlert == null)
            {
                Debug.WriteLine("GraceAlert not found on page");
                return;
            }

            await graceAlert.Show(NotificationType.Success, title, text,block);
        }

        /// <summary>
        /// Adjust insets for ios 
        /// </summary>
        /// <param name="graceView"></param>
        /// <param name="page"></param>
        private static void AdjustForIos(GraceAlertView graceView, Page page)
        {
            if (Device.RuntimePlatform != Device.iOS) return;

            graceView.IsPotrait = page.IsPotrait();
            graceView.PageUseSafeArea =
                Xamarin.Forms.PlatformConfiguration.iOSSpecific.Page.UsingSafeArea(page.On<iOS>());
        }
    }
}