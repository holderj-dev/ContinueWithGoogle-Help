using Firebase.Auth;
using Microsoft.Maui.Dispatching;
using Plugin.Firebase.Auth;
using System.Threading.Tasks;

namespace ContinueWithGoogle
{
    public partial class MainPage : ContentPage
    {
        int count = 0;

        public MainPage()
        {
            InitializeComponent();
        }

        private async void ContinueWithGoogleClicked(object? sender, EventArgs e)
        {
            try
            {
                 var googleAuth = await Plugin.Firebase.Auth.Google.CrossFirebaseAuthGoogle.Current.SignInWithGoogleAsync();

                if (googleAuth != null)
                {

                    await MainThread.InvokeOnMainThreadAsync(async () => {
                        await DisplayAlert("Success", $"Welcome {googleAuth.DisplayName}!", "OK");
                        userInfoText.Text = $"Welcome: \nName: {googleAuth.DisplayName}\nEmail: {googleAuth.Email}";
                    });
                 
                }
                else
                {
                    await MainThread.InvokeOnMainThreadAsync(async () => {
                        await DisplayAlert("Error", "Google sign-in was unsuccessful.", "OK");
                    });
                }
            }
            catch (Exception ex)
            {
                await MainThread.InvokeOnMainThreadAsync(async () => {
                    await DisplayAlert("Error", ex.Message, "OK");
                });
            }
        }
    }
}
