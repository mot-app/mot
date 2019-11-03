using mot.Models;
using mot.Services.Api;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace mot
{
    [DesignTimeVisible(false)]
    public partial class MainShell : Shell
    {
        public MainShell()
        {
            InitializeComponent();

            Task.Run(async () => await IsLoggedIn());
        }

        private async Task IsLoggedIn()
        {
            string id = await SecureStorage.GetAsync("ID");
            if(!string.IsNullOrEmpty(id))
            {
                await LoggedIn(id);
                await Current.GoToAsync("//main");
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
    }
}
