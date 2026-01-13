using Android.Gms.Auth.Api.SignIn;
using Android.Gms.Tasks;
using ContinueWithGoogle.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContinueWithGoogle.Platforms.Android
{
    public class GoogleAuthService : Java.Lang.Object, IGoogleAuthService, IOnSuccessListener
    {
        TaskCompletionSource<string> _tcs;

        public Task<string> SignInWithGoogleAsync()
        {
            _tcs = new TaskCompletionSource<string>();

            var gso = new GoogleSignInOptions.Builder(GoogleSignInOptions.DefaultSignIn)
                .RequestIdToken("387800540318-255vh25ulv2i11brh5abg75q68qale52.apps.googleusercontent.com")
                .RequestEmail()
                .Build();

            var googleSignInClient = GoogleSignIn.GetClient(Platform.CurrentActivity, gso);

            var intent = googleSignInClient.SignInIntent;
            Platform.CurrentActivity.StartActivityForResult(intent, 9001);

            MainActivity.OnGoogleSignInCompleted = HandleSignInResult;

            return _tcs.Task;
        }

        void HandleSignInResult(GoogleSignInAccount account)
        {
            if (account != null)
            {
                string idToken = account.IdToken;
                _tcs.TrySetResult(idToken);
            }
            else
            {
                _tcs.TrySetResult(null);
            }
        }

        public void OnSuccess(Java.Lang.Object result) { }
    }
}
