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

        public async Task Show()
        {
            await this.Alert.TranslateTo(this.Alert.X, 0);
            await Task.Delay(1000);
            await this.Alert.TranslateTo(this.Alert.X, -this.Alert.Height);
        }
    }
}