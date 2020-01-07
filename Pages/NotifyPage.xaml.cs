using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TPSoftware.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NotifyPage : ContentPage
    {
        public new static readonly BindableProperty Content = BindableProperty.Create("Content", typeof(View), typeof(NotifyPage));
        public static readonly BindableProperty Notify = BindableProperty.Create("Notify", typeof(View), typeof(NotifyPage));
        public static readonly BindableProperty NotifyStyle = BindableProperty.Create("NotifyStile", typeof(Style), typeof(NotifyPage));

        public event Action OnButtonYesClicked;
        public event Action OnButtonNoClicked;
        protected bool isNotifyOpen = true;
        public View ContentView {
            get { return GetValue(Content) as View; }
            set { 
                SetValue(Content, value);
                this.framePage.Content = value;
            }
        }
        public View NotifyView {
            get { return GetValue(Notify) as View; }
            set {
                SetValue(Notify, value);
                stackNotify.Children.RemoveAt(0);
                stackNotify.Children.Insert(0,value);
                SetupNotify(value.HeightRequest);
            }
        }
        public Style NotifyViewStyle {
            set {
                SetValue(NotifyStyle, value);
                frameNotify.Style = value;
            }
        }
        public NotifyPage()
        {
            InitializeComponent();
            
        }

        protected void SetupNotify(double contentHeight)
        {
            frameNotify.ForceLayout();
            AbsoluteLayout.SetLayoutFlags(frameNotify, AbsoluteLayoutFlags.PositionProportional | AbsoluteLayoutFlags.WidthProportional);
            AbsoluteLayout.SetLayoutBounds(frameNotify, new Rectangle(0, 1, 1, stackNotify.Height));              
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            frameNotify.TranslationY = 500;

            SetupNotify(0);

        }

        public async void OpenNotify(uint time)
        {
            //if (isNotifyOpen)
            //    return;
            double delta = frameNotify.Height;
            await this.frameNotify.TranslateTo(0,0, time);
            isNotifyOpen = true;
            Console.Write(frameNotify.Y);
        }
        public async void CloseNotify(uint time)
        {
            //if (!isNotifyOpen)
            //    return;
            double delta = frameNotify.Height;
            await this.frameNotify.TranslateTo(0, delta, time);
            isNotifyOpen = false;
        }

        private void ButtonNoClicked(object sender, EventArgs e)
        {
            CloseNotify(150);
            OnButtonNoClicked?.Invoke();
        }

        private void ButtonYesClicked(object sender, EventArgs e)
        {
            CloseNotify(150);
            OnButtonYesClicked?.Invoke();
        }
    }
}