using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Gms.Auth.Api.SignIn;
using Android.OS;
using Firebase;
using System;

namespace ContinueWithGoogle
{
    [Activity(Theme = "@style/Maui.SplashTheme", MainLauncher = true, LaunchMode = LaunchMode.SingleTop, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize | ConfigChanges.Density)]
    public class MainActivity : MauiAppCompatActivity
    {
        public static Action<GoogleSignInAccount> OnGoogleSignInCompleted;

        protected override void OnCreate(Bundle? savedInstanceState)
        {
            try
            {                             
                base.OnCreate(savedInstanceState);

                Plugin.CurrentActivity.CrossCurrentActivity.Current.Init(this, savedInstanceState);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Firebase initialization error: {ex.Message}");

                base.OnCreate(savedInstanceState);
            }
        }

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);

            if (requestCode == 9001)
            {
                var task = GoogleSignIn.GetSignedInAccountFromIntent(data);
                try
                {
                    var account = task.Result;
                    OnGoogleSignInCompleted?.Invoke((GoogleSignInAccount)account);
                }
                catch (Exception)
                {
                    OnGoogleSignInCompleted?.Invoke(null);
                }
            }
        }
    }
}
