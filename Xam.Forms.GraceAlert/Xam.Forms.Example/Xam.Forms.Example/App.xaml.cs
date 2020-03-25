using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]

namespace Xam.Forms.Example
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            
#if DEBUG
            HotReloader.Current.Run(this);
#endif

            MainPage = new MainPage();
            
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}