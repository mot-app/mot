using mot.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace mot.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OverView : ContentPage
    {
        OverViewModel overview;
        public OverView()
        {
            InitializeComponent();

            BindingContext = overview = new OverViewModel();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            await overview.GetAvailableUsers();
            await overview.GetMeetups();
        }
    }
}