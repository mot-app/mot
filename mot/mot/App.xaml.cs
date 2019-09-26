using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using mot.Models;
using mot.Services.Api;
using mot.Views;
using Newtonsoft.Json;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace mot
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new MainShell();
        }

        protected override void OnStart()
        {
            //Handle when yout app starts
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
