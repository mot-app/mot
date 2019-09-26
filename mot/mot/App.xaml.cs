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

            if (Current.Properties.ContainsKey("ID"))
            {
                string id = Current.Properties["ID"].ToString();
                Task.Run(async () => await LoggedIn(id));
                MainPage = new MainShell();

            } else
            {
                MainPage = new LoginView();
            }
        }

        private async Task LoggedIn(string id)
        {
            var Uri = new Uri("https://server-cy3lzdr3na-uc.a.run.app/user/" + id);
            string data = await RestService.Read(Uri);
            var users = JsonConvert.DeserializeObject<List<User>>(data);
            var User = users.Find(user => user.Id == id);
            if (User != null)
            {
                var position = await Geolocation.GetLocationAsync(new GeolocationRequest(GeolocationAccuracy.Medium));
                User.Latitude = position.Latitude;
                User.Longitude = position.Longitude;
                var Uri2 = new Uri("https://server-cy3lzdr3na-uc.a.run.app/user");
                await RestService.Update(User, Uri2);
            }
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
