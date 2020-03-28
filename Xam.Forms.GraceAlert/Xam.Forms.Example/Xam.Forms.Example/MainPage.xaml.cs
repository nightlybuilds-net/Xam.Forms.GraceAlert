using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xam.Forms.GraceAlert;
using Xamarin.Forms;

namespace Xam.Forms.Example
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }
        
        private async void Block_Error_OnClicked(object sender, EventArgs e)
        {
            await this.Error("Error","Not very well and.. blocked", true);
        }

        private async void Error_OnClicked(object sender, EventArgs e)
        {
            await this.Error("Error","Not very well.");
        }
        
        private async void Warning_OnClicked(object sender, EventArgs e)
        {
            await this.Warning("Warning","You could do better");
        }

        private async void Info_OnClicked(object sender, EventArgs e)
        {
            await this.Info("Info","Don't say I didn't tell you");
        }
        
        private async void Success_OnClicked(object sender, EventArgs e)
        {
            await this.Success("Success","You did it!");
        }
       
    }
}