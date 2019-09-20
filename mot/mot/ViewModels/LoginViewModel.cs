﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using mot.Models;
using mot.Services;
using mot.Services.Api;
using mot.Services.Auth;
using MvvmHelpers;
using Newtonsoft.Json;
using Xamarin.Auth;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace mot.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        public User User;
        public string Name;
        public LoginViewModel()
        {
            Login = new Command(OnLoginClicked, () => !IsBusy);
        }

        public Command Login { get; }

        void OnLoginClicked()
        {
            string clientId = null;
            string redirectUri = null;


            switch (Device.RuntimePlatform)
            {
                case Device.iOS:
                    clientId = GoogleOAuthManager.iOSClientId;
                    redirectUri = GoogleOAuthManager.iOSRedirectUrl;
                    break;

                case Device.Android:
                    clientId = GoogleOAuthManager.AndroidClientId;
                    redirectUri = GoogleOAuthManager.AndroidRedirectUrl;
                    break;
            }

            var authenticator = new OAuth2Authenticator(
                clientId,
                null,
                GoogleOAuthManager.Scope,
                new Uri(GoogleOAuthManager.AuthorizeUrl),
                new Uri(redirectUri),
                new Uri(GoogleOAuthManager.AccessTokenUrl),
                null,
                true);

            authenticator.Completed += OnAuthCompleted;
            authenticator.Error += OnAuthError;

            AuthenticationState.Authenticator = authenticator;

            var presenter = new Xamarin.Auth.Presenters.OAuthLoginPresenter();
            presenter.Login(authenticator);
        }

        async void OnAuthCompleted(object sender, AuthenticatorCompletedEventArgs e)
        {
            if (!e.IsAuthenticated) return;

            var token = e.Account.Properties["access_token"];
            await SecureStorage.SetAsync("access_token", token);
            Application.Current.MainPage = new MainShell();

            var request = new OAuth2Request("GET", new Uri(GoogleOAuthManager.UserInfoUrl), null, e.Account);
            var response = await request.GetResponseAsync();

            if (response != null)
            {
                string userJson = await response.GetResponseTextAsync();
                User = JsonConvert.DeserializeObject<User>(userJson);
                //var Uri = new Uri("http://localhost:8080/user");
                //await RestService.Create(User, Uri);
            };
        }

        void OnAuthError(object sender, AuthenticatorErrorEventArgs e)
        {
            Debug.WriteLine("Authentication error: " + e.Message);
        }
    }
}