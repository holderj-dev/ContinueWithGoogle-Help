using AndroidX.Annotations;
using ContinueWithGoogle.Helper;
using Firebase.Auth;
using Microsoft.Maui.Dispatching;
using Plugin.FirebaseAuth;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;

namespace ContinueWithGoogle
{
    public partial class MainPage : ContentPage
    {
        int count = 0;

        private PhoneNumberVerificationResult verificationResult;

        private readonly IGoogleAuthService _googleAuthService;

        public MainPage(IGoogleAuthService googleAuthService)
        {
            InitializeComponent();

            _googleAuthService = googleAuthService;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            var user = CrossFirebaseAuth.Current.Instance.CurrentUser;

            if (user != null)
            {
                userInfoText.Text = $"Welcome: \nName: {user.DisplayName}\nEmail: {user.Email}";

            }
        }

        private async void ContinueWithGoogleClicked(object? sender, EventArgs e)
        {
            try
            {
                string idToken = await _googleAuthService.SignInWithGoogleAsync();

                if (idToken != null)
                {
                    await DisplayAlert("Google Sign-In", $"ID Token: {idToken}", "OK");

                    var credential = CrossFirebaseAuth.Current.GoogleAuthProvider.GetCredential(idToken, null);
                    var result = await CrossFirebaseAuth.Current.Instance.SignInWithCredentialAsync(credential);

                    if (result != null)
                    {
                        await DisplayAlert("Success", $"Welcome {result.User.DisplayName}!", "OK");
                        userInfoText.Text = $"Welcome: \nName: {result.User.DisplayName}\nEmail: {result.User.Email}";
                    }
                    else
                    {
                        await DisplayAlert("Error", "Google sign-in was unsuccessful.", "OK");
                    }
                }
                else
                {
                    await DisplayAlert("Error", "Google Sign-In failed.", "OK");
                }               

            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", ex.Message, "OK");
            }
        }

        public async void VerifyPhoneNumberAsync(object? sender, EventArgs e)
        {
            try
            {
                var phoneNumber = phoneNumberEntry.Text;
               
                if (phoneNumber != null)
                {
                    verificationResult = await CrossFirebaseAuth.Current.PhoneAuthProvider.VerifyPhoneNumberAsync(phoneNumber);
                }

                return;
            }          
            catch (Exception ex)
            {
                await DisplayAlert("Error", ex.Message, "OK");
            }
        }

        private async void SigninWithPhoneNumberClicked(object? sender, EventArgs e)
        {
            try
            {
                var vCode = verificationCodeEntry.Text;
                if (vCode == null)
                    return;

                var credential = CrossFirebaseAuth.Current.PhoneAuthProvider.GetCredential(verificationResult.VerificationId, vCode);
                var result = await CrossFirebaseAuth.Current.Instance.SignInWithCredentialAsync(credential);

                if (result != null)
                {
                    await MainThread.InvokeOnMainThreadAsync(async () =>
                    {
                        await DisplayAlert("Success", "Phone number sign-in successful!", "OK");
                        userInfoText.Text = $"Phone Number: {vCode}";
                    });
                }
                else
                {
                    await MainThread.InvokeOnMainThreadAsync(async () =>
                    {
                        await DisplayAlert("Error", "Phone number sign-in was unsuccessful.", "OK");
                    });
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", ex.Message, "OK");
            }
        }

    }
}
