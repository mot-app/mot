using mot.Models;
using mot.Services.Api;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace mot.ViewModels
{
    class OverViewModel
    {
        public OverViewModel()
        {
            Task.Run(async () => await GetMeetups());
            Task.Run(async () => await GetAvailableUsers());
        }

        public ObservableCollection<Meetup> Meetups { get; set; }
        public ObservableCollection<User> Users { get; set; }

        private async Task GetMeetups()
        {
            var Uri = new Uri("https://server-cy3lzdr3na-uc.a.run.app/meetup/");
            string data = await RestService.Read(Uri);
            Meetups = new ObservableCollection<Meetup>(JsonConvert.DeserializeObject<List<Meetup>>(data));
        }

        private async Task GetAvailableUsers()
        {
            var Uri = new Uri("https://server-cy3lzdr3na-uc.a.run.app/user");
            string data = await RestService.Read(Uri);
            var users = JsonConvert.DeserializeObject<List<User>>(data);
            string id = await SecureStorage.GetAsync("id");
            users.RemoveAll(user => !user.Available || !user.Busy || user.Id == id);
            Users = new ObservableCollection<User>(users);
        }

    }
}
