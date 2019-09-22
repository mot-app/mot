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

namespace mot.ViewModels
{
    class ProfileViewModel : BaseViewModel
    {

        public ProfileViewModel()
        {
            Task.Run(async () => await GetUser());
        }

        private User user;
        public User User
        {
            get => user;
            set
            {
                SetProperty(ref user, value);
                OnPropertyChanged(nameof(User));
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


        async Task GetUser()
        {
            var Uri = new Uri("https://server-cy3lzdr3na-uc.a.run.app/user/" + "116466181289590143131");
            string data = await RestService.Read(Uri);
            var users = JsonConvert.DeserializeObject<List<User>>(data);
            User = users.First();
            Name = User.Name;
        }
    }
}
