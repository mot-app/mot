using mot.Models;
using mot.Services.Api;
using MvvmHelpers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;
using Newtonsoft.Json;
using Xamarin.Essentials;

namespace mot.ViewModels
{
    public class ProfileViewModel : BaseViewModel
    {

        public ProfileViewModel()
        {
            ChangeAvailable = new Command<string>((string id) => ChangeAvailableCommand(id), (string id) => !IsBusy);
            FetchUser = new Command(async () => await GetUser());
        }

        public User User;

        public Command FetchUser { get; }

        private string id; 
        public string Id
        {
            get => id;
            set
            {
                SetProperty(ref id, value);
                OnPropertyChanged(nameof(Id));
            }
        }

        private string name;
        public string Name
        {
            get => name;
            set
            {
                SetProperty(ref name, value);
                OnPropertyChanged(nameof(Name));
            }
        }

        private string email;
        public string Email
        {
            get => email;
            set
            {
                SetProperty(ref email, value);
                OnPropertyChanged(nameof(Email));
            }
        }

        private string picture;
        public string Picture
        {
            get => picture;
            set
            {
                SetProperty(ref picture, value);
                OnPropertyChanged(nameof(Picture));
            }
        }

        private bool available;
        public bool Available
        {
            get => available;
            set
            {
                SetProperty(ref available, value);
                OnPropertyChanged(nameof(Available));
                OnPropertyChanged(nameof(DisplayAvailable));
                OnPropertyChanged(nameof(DisplayAvailableButton));
            }
        }

        public string DisplayAvailable => available ?  $"You are available" : $"You are not available";

        public string DisplayAvailableButton => available ? $"Make yourself hidden" : $"Make yourself available";

        private async void ChangeAvailableCommand(string id)
        {
            Available = !available;
            User.Available = Available;
            await UpdateUser(id);
        }

        public Command<string> ChangeAvailable { get; }

        public async Task GetUser(string id = null)
        {
            if(id == null)
            {
                id = await SecureStorage.GetAsync("ID");
            }
            var Uri = new Uri("https://server-cy3lzdr3na-uc.a.run.app/user/" + id);
            string data = await RestService.Read(Uri);
            var users = JsonConvert.DeserializeObject<List<User>>(data);
            User = users.Find(user => user.Id == id);
            Id = User.Id;
            Name = User.Name;
            Email = User.Email;
            Picture = User.Picture;
            Available = User.Available;
        }

        public async Task UpdateUser(string id = null)
        {
            if (id == null)
            {
                id = await SecureStorage.GetAsync("ID");
            }
            var Uri = new Uri("https://server-cy3lzdr3na-uc.a.run.app/user/" + id);
            await RestService.Update(User, Uri);
        }


    }
}
