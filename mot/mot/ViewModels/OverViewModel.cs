using mot.Models;
using mot.Services.Api;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;

namespace mot.ViewModels
{
    class OverViewModel
    {
        public OverViewModel()
        {
            Task.Run(async () => await GetMeetups());
        }

        public ObservableCollection<Meetup> Meetups { get; set; }

        private async Task GetMeetups()
        {
            var Uri = new Uri("https://server-cy3lzdr3na-uc.a.run.app/meetup/");
            string data = await RestService.Read(Uri);
            Meetups = new ObservableCollection<Meetup>(JsonConvert.DeserializeObject<List<Meetup>>(data));
        }
    }
}
