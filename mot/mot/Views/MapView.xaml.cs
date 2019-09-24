using mot.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
        public MapView()
        {
            InitializeComponent();
            Task.Run(async () => await GetLocation());
            BindingContext = new MapViewModel();
            
        }

        private async Task GetLocation()
        {
            var request = new GeolocationRequest(GeolocationAccuracy.Medium);
            var position = await Geolocation.GetLocationAsync(request);
            Map.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(position.Latitude, position.Longitude), Distance.FromMiles(1)));
            var zoomLevel = 9;
            var latlongdegrees = 360 / (Math.Pow(2, zoomLevel));
            Map.MoveToRegion(new MapSpan(Map.VisibleRegion.Center, latlongdegrees, latlongdegrees));
        }
    }
}