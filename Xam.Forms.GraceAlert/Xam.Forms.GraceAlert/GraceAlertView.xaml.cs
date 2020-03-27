using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;

namespace Xam.Forms.GraceAlert
{
    public partial class GraceAlertView : Grid
    {
        private TaskCompletionSource<bool> _dismissTask;
        private int _defaultTranslation = -44;
        
        private bool _isShowing;
        private readonly ConcurrentQueue<GraceRequest> _requests = new ConcurrentQueue<GraceRequest>();

        private static readonly Color DefaultWarningColor = Color.FromHex("F6CF46");
        private static readonly Color DefaultErrorColor = Color.FromHex("E5465C");
        private static readonly Color DefaultInfoColor = Color.LightGray;

        private static readonly Color DarkTextColor = Color.FromHex("323232");
        private static readonly Color LightTextColor = Color.WhiteSmoke;
        
        
        private static readonly Color DefaultInverseColor = Color.WhiteSmoke;
        

        public GraceAlertView()
        {
            this.InitializeComponent();
        }
        
        public static readonly BindableProperty BodyContentProperty = BindableProperty.Create(nameof(BodyContent),
            typeof(ContentView), typeof(GraceAlertView), coerceValue: BodyContentCoerceValue);

        public static readonly BindableProperty DismissTimeProperty = BindableProperty.Create(nameof(DismissTime),
            typeof(int), typeof(GraceAlertView),2000);
        
        public static readonly BindableProperty ErrorColorProperty = BindableProperty.Create(nameof(ErrorColor),
            typeof(Color), typeof(GraceAlertView),DefaultErrorColor);
        
        public static readonly BindableProperty WarningColorProperty = BindableProperty.Create(nameof(WarningColor),
            typeof(Color), typeof(GraceAlertView),DefaultWarningColor);
        
        public static readonly BindableProperty InfoColorProperty = BindableProperty.Create(nameof(InfoColor),
            typeof(Color), typeof(GraceAlertView),DefaultInfoColor);
        
        public static readonly BindableProperty InverseColorProperty = BindableProperty.Create(nameof(InverseColor),
            typeof(Color), typeof(GraceAlertView),DefaultInverseColor);
        
        public static readonly BindableProperty UseLightColorForErrorProperty = BindableProperty.Create(nameof(UseLightColorForError),
            typeof(bool), typeof(GraceAlertView),true);
        
        public static readonly BindableProperty UseLightColorForWarningProperty = BindableProperty.Create(nameof(UseLightColorForWarning),
            typeof(bool), typeof(GraceAlertView),false);
        
        public static readonly BindableProperty UseLightColorForInfoProperty = BindableProperty.Create(nameof(UseLightColorForInfo),
            typeof(bool), typeof(GraceAlertView),false);


        public ContentView BodyContent
        {
            get => (ContentView) this.GetValue(BodyContentProperty);
            set => this.SetValue(BodyContentProperty, value);
        }
        
        public int DismissTime
        {
            get => (int) this.GetValue(DismissTimeProperty);
            set => this.SetValue(DismissTimeProperty, value);
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
        
      

        public Color InverseColor
        {
            get => (Color) this.GetValue(InverseColorProperty);
            set => this.SetValue(InverseColorProperty, value);
        }
        
        public bool UseLightColorForError
        {
            get => (bool) this.GetValue(UseLightColorForErrorProperty);
            set => this.SetValue(UseLightColorForErrorProperty, value);
        }
        
        public bool UseLightColorForWarning
        {
            get => (bool) this.GetValue(UseLightColorForWarningProperty);
            set => this.SetValue(UseLightColorForWarningProperty, value);
        }
        
        public bool UseLightColorForInfo
        {
            get => (bool) this.GetValue(UseLightColorForInfoProperty);
            set => this.SetValue(UseLightColorForInfoProperty, value);
        }
        
        
        
        /// <summary>
        /// This property is setted by extension method GraceAlert()
        /// </summary>
        public bool PageUseSafeArea { get; set; }

        /// <summary>
        /// True iif the page is in potrait mode
        /// </summary>
        public bool IsPotrait { get; set; }

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
        
        public async Task Show(NotificationType type, string title, string message, bool block = false)
        {
            var request = new GraceRequest(type,title,message, block);
            this._requests.Enqueue(request);
            
            await this.InnerShow();
        }

        private async Task InnerShow()
        {
            // notificatioin is showing skip
            if (this._isShowing)
                return;

            // no request in queue skip
            if (!this._requests.Any()) return;

            // notification is showing
            this._isShowing = true;

            this._dismissTask = null;

            var requestFound = this._requests.TryDequeue(out var request);
            if (!requestFound) return;
            
            // manage translation
            var translation = _defaultTranslation;
            if (!this.PageUseSafeArea && this.IsPotrait)
                translation = 0;

            this.Title.TextColor = this.TypeToTextColor(request.Type);
            this.Message.TextColor = this.TypeToTextColor(request.Type);

            this.Notification.BackgroundColor = this.TypeToColor(request.Type);
            this.Title.Text = request.Title;
            this.Message.Text = request.Message;
            
            this.Notification.IsVisible = true;
            await this.Notification.TranslateTo(this.Notification.X, translation);
            
            // dismissmode
            if (request.Block)
            {
                this._dismissTask = new TaskCompletionSource<bool>();
                await this._dismissTask.Task;
            }
            else
                await Task.Delay(this.DismissTime);
            
            await this.Notification.TranslateTo(this.Notification.X, -this.Notification.Height + translation);
            this.Notification.IsVisible = false;

            this._isShowing = false;
            await this.InnerShow();
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

        private Color TypeToTextColor(NotificationType type)
        {
            switch (type)
            {
                case NotificationType.Error:
                    return this.UseLightColorForError ? LightTextColor : DarkTextColor;
                case NotificationType.Warning:
                    return this.UseLightColorForWarning ? LightTextColor : DarkTextColor;
                case NotificationType.Info:
                    return this.UseLightColorForInfo ? LightTextColor : DarkTextColor;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }

        #endregion

        private void TapGestureRecognizer_OnTapped(object sender, EventArgs e)
        {
            this._dismissTask?.SetResult(true);
        }
    }
}