using mot.Models;
using mot.Services.Api;
using MvvmHelpers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace mot.ViewModels
{
    class OverViewModel : BaseViewModel
    {
        public OverViewModel()
        {
            Task.Run(async () => await GetMeetups());
            Task.Run(async () => await GetAvailableUsers());

            Users = new ObservableCollection<User>();
            RefreshUsers = new Command(async () => await GetAvailableUsers());
        }

        public Command RefreshUsers { get; set; }

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
            if (IsBusy)
                return;

            IsBusy = true;
            Users.Clear();

            var Uri = new Uri("https://server-cy3lzdr3na-uc.a.run.app/user");
            string data = await RestService.Read(Uri);
            var users = JsonConvert.DeserializeObject<List<User>>(data);
            string id = await SecureStorage.GetAsync("id");
            users.RemoveAll(user => !user.Available || user.Busy || user.Id == id);

            foreach (var user in users)
            {
                Users.Add(user);
            };

            IsBusy = false;
        }

    }
}
