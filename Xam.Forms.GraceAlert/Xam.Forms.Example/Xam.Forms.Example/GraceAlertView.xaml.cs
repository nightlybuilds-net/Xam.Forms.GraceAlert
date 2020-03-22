using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace Xam.Forms.Example
{
    public partial class GraceAlertView : Grid
    {
        public GraceAlertView()
        {
            InitializeComponent();
        }
        
        public static readonly BindableProperty BodyContentProperty = BindableProperty.Create(nameof(BodyContent),
            typeof(ContentView), typeof(GraceAlertView), coerceValue: BodyContentCoerceValue);

        
        public ContentView BodyContent
        {
            get => (ContentView) this.GetValue(BodyContentProperty);
            set => this.SetValue(BodyContentProperty, value);
        }
        
        private static object BodyContentCoerceValue(BindableObject bindableObject, object value)
        {
            if (bindableObject != null && value is ContentView view)
            {
                var instance = (GraceAlertView) bindableObject;
                instance.Body.Content = view;
            }

            return value;
        }

        public Task Error(string title, string text)
        {
            return this.Show(NotificationType.Error, title, text);
        }
        
        public Task Warning(string title, string text)
        {
            return this.Show(NotificationType.Warning, title, text);
        }
        
        public Task Info(string title, string text)
        {
            return this.Show(NotificationType.Info, title, text);
        }
        
        private async Task Show(NotificationType type, string title, string message)
        {
            this.Notification.BackgroundColor = this.TypeToColor(type);
            this.Title.Text = title;
            this.Message.Text = message;
            
            await this.Notification.TranslateTo(this.Notification.X, 0);
            await Task.Delay(1000);
            await this.Notification.TranslateTo(this.Notification.X, -this.Notification.Height);
        }

        private Color TypeToColor(NotificationType type)
        {
            switch (type)
            {
                case NotificationType.Error:
                    return Color.Red;
                case NotificationType.Warning:
                    return Color.Yellow;
                case NotificationType.Info:
                    return Color.Beige;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }

        enum NotificationType
        {
            Error,
            Warning,
            Info
        }
    }
}