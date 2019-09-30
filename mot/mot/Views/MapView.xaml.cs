using mot.Models;
using mot.Services.Api;
using mot.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Essentials;

using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;

namespace mot.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MapView : ContentPage
    {
        private MapViewModel viewmodel;

        public MapView()
        {
            InitializeComponent();

            BindingContext = viewmodel = new MapViewModel();

            Task.Run(async () => await SetMapLocation());
        }

        private async Task SetMapLocation()
        {
            string id = await SecureStorage.GetAsync("ID");
            var Uri = new Uri("https://server-cy3lzdr3na-uc.a.run.app/user/" + id);
            string data = await RestService.Read(Uri);
            var users = JsonConvert.DeserializeObject<List<User>>(data);
            var User = users.Find(user => user.Id == id);
            Map.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(User.Latitude, User.Longitude), Distance.FromMiles(1)));
            var zoomLevel = 9;
            var latlongdegrees = 360 / (Math.Pow(2, zoomLevel));
            Map.MoveToRegion(new MapSpan(Map.VisibleRegion.Center, latlongdegrees, latlongdegrees));
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            await viewmodel.GetMeetups();

        }
    }
}