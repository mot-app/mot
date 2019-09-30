using mot.Models;
using mot.Services.Api;
using MvvmHelpers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms.Maps;

namespace mot.ViewModels
{
    class MapViewModel : BaseViewModel
    {
        public MapViewModel()
        {
            Task.Run(async () => await GetMeetups());
        }

        public ObservableCollection<MapPin> Pins { get; set; }

        public async Task GetMeetups()
        {
            Pins.Clear();

            var Uri = new Uri("https://server-cy3lzdr3na-uc.a.run.app/meetup/");
            string data = await RestService.Read(Uri);
            var meetups = JsonConvert.DeserializeObject<List<Meetup>>(data);
            string id = await SecureStorage.GetAsync("ID");
            meetups.RemoveAll(m => m.User1 != id && m.User2 != id);

            foreach (var meetup in meetups)
            {
                string id1 = meetup.User1;
                var Uri1 = new Uri("https://server-cy3lzdr3na-uc.a.run.app/user/" + id1);
                string Data1 = await RestService.Read(Uri1);
                var users1 = JsonConvert.DeserializeObject<List<User>>(Data1);
                var User1 = users1.Find(user => user.Id == id1);

                var Pin1 = new MapPin();
                Pin1.Position = new Position(User1.Latitude, User1.Longitude);
                Pins.Add(Pin1);

                string id2 = meetup.User2;
                var Uri2 = new Uri("https://server-cy3lzdr3na-uc.a.run.app/user/" + id2);
                string Data2 = await RestService.Read(Uri2);
                var users2 = JsonConvert.DeserializeObject<List<User>>(Data2);
                var User2 = users2.Find(user => user.Id == id2);

                var Pin2 = new MapPin();
                Pin2.Position = new Position(User2.Latitude, User2.Longitude);
                Pins.Add(Pin2);
            }
        }
    }
}
