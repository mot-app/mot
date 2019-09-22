using mot.Models;
using mot.Services.Api;
using MvvmHelpers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Linq;
using Newtonsoft.Json;
using Xamarin.Essentials;

namespace mot.ViewModels
{
    class ProfileViewModel : BaseViewModel
    {

        public ProfileViewModel()
        {
            Task.Run(async () => await GetUser());
        }

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

        private async Task GetUser()
        {
            string id = await SecureStorage.GetAsync("id");
            var Uri = new Uri("https://server-cy3lzdr3na-uc.a.run.app/user/" + id);
            string data = await RestService.Read(Uri);
            var users = JsonConvert.DeserializeObject<List<User>>(data);
            var User = users.First();
            Id = User.Id;
            Name = User.Name;
            Email = User.Email;
            Picture = User.Picture;
        }
    }
}
