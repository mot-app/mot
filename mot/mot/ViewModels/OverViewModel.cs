using mot.Models;
using mot.Services.Api;
using MvvmHelpers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace mot.ViewModels
{
    class OverViewModel : BaseViewModel
    {
        public OverViewModel()
        {

            Refresh = new Command(async () =>
            {
                await GetAvailableUsers();
                await GetMeetups();
            });

            RequestMeetup = new Command(async obj => await SetMeetup(obj));

            Users = new ObservableCollection<User>();

            Meetups = new ObservableCollection<Meetup>();
        }

        public Command Refresh { get; }

        public Command RequestMeetup { get; }

        public ObservableCollection<Meetup> Meetups { get; set; }

        public ObservableCollection<User> Users { get; set; }

        public async Task GetMeetups()
        {
            if (IsBusy)
                return;

            IsBusy = true;
            Meetups.Clear();

            var Uri = new Uri("https://server-cy3lzdr3na-uc.a.run.app/meetup/");
            string data = await RestService.Read(Uri);
            var meetups = JsonConvert.DeserializeObject<List<Meetup>>(data);
            string id = await SecureStorage.GetAsync("ID");
            meetups.RemoveAll(meetup => meetup.User1 != id && meetup.User2 != id);

            foreach (var meetup in meetups)
            {
                Meetups.Add(meetup);
            }

            IsBusy = false;
        }

        public async Task GetAvailableUsers()
        {
            if (IsBusy)
                return;

            IsBusy = true;
            Users.Clear();

            var Uri = new Uri("https://server-cy3lzdr3na-uc.a.run.app/user");
            string data = await RestService.Read(Uri);
            var users = JsonConvert.DeserializeObject<List<User>>(data);
            string id = await SecureStorage.GetAsync("ID");
            users.RemoveAll(user => !user.Available || user.Busy || user.Id == id);

            foreach (var user in users)
            {
                Users.Add(user);
            };

            IsBusy = false;
        }

        private async Task SetMeetup(object obj)
        {
            string id1 = await SecureStorage.GetAsync("ID");
            var Uri1 = new Uri("https://server-cy3lzdr3na-uc.a.run.app/user/" + id1);
            string Data1 = await RestService.Read(Uri1);
            var users1 = JsonConvert.DeserializeObject<List<User>>(Data1);
            var User1 = users1.Find(user => user.Id == id1);

            string id2 = obj as string;
            var Uri2 = new Uri("https://server-cy3lzdr3na-uc.a.run.app/user/" + id2);
            string Data2 = await RestService.Read(Uri2);
            var users2 = JsonConvert.DeserializeObject<List<User>>(Data2);
            var User2 = users2.Find(user => user.Id == id2);
            User2.Busy = true;
            await RestService.Update(User2, Uri2);

            var Uri = new Uri("https://server-cy3lzdr3na-uc.a.run.app/meetup/");
            var Meetup = new Meetup();

            Meetup.Id = id1 + id2;
            Meetup.User1 = id1;
            Meetup.User2 = id2;
            Meetup.Time = DateTime.Now;
            Meetup.Active = true;
            Meetup.User1Name = User1.Name;
            Meetup.User2Name = User2.Name;

            await RestService.Create(Meetup, Uri);

            await GetAvailableUsers();
            await GetMeetups();
        }

    }
}
