using System;
using mot.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace mot
{
    public partial class App : Application
    {
        public bool IsLoggedIn = true;
        public App()
        {
            InitializeComponent();

            
            if (IsLoggedIn)
            {
                MainPage = new MainShell();
            }
            else
            {
                MainPage = new LoginView();
            }
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
