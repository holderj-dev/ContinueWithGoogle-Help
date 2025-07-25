using Plugin.Firebase.Auth.Google;
using Plugin.Firebase.Core.Platforms.Android;

namespace ContinueWithGoogle
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
#if IOS
            // FirebaseAuthGoogleImplementation.Initialize();
#elif ANDROID
            FirebaseAuthGoogleImplementation.Initialize("292004549052-uasrrsbmof2mhiebp8smdq7ql5nejvid.apps.googleusercontent.com");
#endif
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            return new Window(new AppShell());
        }
    }
}