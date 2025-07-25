using Android.App;
using Android.Content.PM;
using Android.OS;
using Plugin.Firebase.Core.Platforms.Android;
using Firebase;
using System;

namespace ContinueWithGoogle
{
    [Activity(Theme = "@style/Maui.SplashTheme", MainLauncher = true, LaunchMode = LaunchMode.SingleTop, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize | ConfigChanges.Density)]
    public class MainActivity : MauiAppCompatActivity
    {
        protected override void OnCreate(Bundle? savedInstanceState)
        {
            try
            {                             
                base.OnCreate(savedInstanceState);
                
                CrossFirebase.Initialize(this);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Firebase initialization error: {ex.Message}");

                base.OnCreate(savedInstanceState);
            }
        }
    }
}
