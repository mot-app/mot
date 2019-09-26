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
    public partial class ProfileView : ContentPage
    {
        private ProfileViewModel profile;

        public ProfileView()
        {
            InitializeComponent();

            BindingContext = profile = new ProfileViewModel();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            await profile.GetUser();
        }
    }
}