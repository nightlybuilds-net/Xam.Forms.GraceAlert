using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Xam.Forms.Example
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private async void Button_OnClicked(object sender, EventArgs e)
        {
            await this.GraceAlertView.Error("Errore","Non va bene cosi!");
        }
        
        private async void Warning_OnClicked(object sender, EventArgs e)
        {
            await this.GraceAlertView.Warning("Attenzione","Aggiungi il dato che ben sai!");
        }

        private async void Info_OnClicked(object sender, EventArgs e)
        {
            await this.GraceAlertView.Info("Info","Info carina ma non obbligatoria");
        }
       
    }
}