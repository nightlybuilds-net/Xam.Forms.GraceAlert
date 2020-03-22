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

        public static readonly BindableProperty TimeProperty = BindableProperty.Create(nameof(Time),
            typeof(int), typeof(GraceAlertView),1000);
            
        public static readonly BindableProperty ErrorColorProperty = BindableProperty.Create(nameof(ErrorColor),
            typeof(Color), typeof(GraceAlertView),Color.Red);
        
        public static readonly BindableProperty WarningColorProperty = BindableProperty.Create(nameof(WarningColor),
            typeof(Color), typeof(GraceAlertView),Color.Yellow);
        
        public static readonly BindableProperty InfoColorProperty = BindableProperty.Create(nameof(InfoColor),
            typeof(Color), typeof(GraceAlertView),Color.LightGray);

        
        public ContentView BodyContent
        {
            get => (ContentView) this.GetValue(BodyContentProperty);
            set => this.SetValue(BodyContentProperty, value);
        }
        
        public int Time
        {
            get => (int) this.GetValue(TimeProperty);
            set => this.SetValue(TimeProperty, value);
        }
        
        public Color ErrorColor
        {
            get => (Color) this.GetValue(ErrorColorProperty);
            set => this.SetValue(ErrorColorProperty, value);
        }
        
        public Color WarningColor
        {
            get => (Color) this.GetValue(WarningColorProperty);
            set => this.SetValue(WarningColorProperty, value);
        }
        
        public Color InfoColor
        {
            get => (Color) this.GetValue(InfoColorProperty);
            set => this.SetValue(InfoColorProperty, value);
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


        #region METHODS
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
            await Task.Delay(this.Time);
            await this.Notification.TranslateTo(this.Notification.X, -this.Notification.Height);
        }

        private Color TypeToColor(NotificationType type)
        {
            switch (type)
            {
                case NotificationType.Error:
                    return this.ErrorColor;
                case NotificationType.Warning:
                    return this.WarningColor;
                case NotificationType.Info:
                    return this.InfoColor;
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
        

        #endregion
        
       
    }
}