using System;
using System.Collections.Generic;
using System.Text;

namespace mot.Services.Auth
{
    public static class GoogleOAuthManager
    {
        public static string AppName = "OAuthNativeFlow";

        public static string iOSClientId = "<insert IOS client ID here>";
        public static string AndroidClientId = "848493642769-cf3kmpnq5ev3d4mlta7urt5fpn5hn3k2.apps.googleusercontent.com";

        public static string Scope = "https://www.googleapis.com/auth/userinfo.email";
        public static string AuthorizeUrl = "https://accounts.google.com/o/oauth2/auth";
        public static string AccessTokenUrl = "https://www.googleapis.com/oauth2/v4/token";
        public static string UserInfoUrl = "https://www.googleapis.com/oauth2/v2/userinfo";

        public static string iOSRedirectUrl = "<insert IOS redirect URL here>:/oauth2redirect";
        public static string AndroidRedirectUrl = "com.googleusercontent.apps.848493642769-cf3kmpnq5ev3d4mlta7urt5fpn5hn3k2:/oauth2redirect";
    }
}
