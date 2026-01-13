using ContinueWithGoogle.Helper;
using ContinueWithGoogle.Platforms.Android;
using Microsoft.Extensions.Logging;

namespace ContinueWithGoogle
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();

            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

#if DEBUG
            builder.Logging.AddDebug();
#endif

#if ANDROID
            builder.Services.AddSingleton<IGoogleAuthService, GoogleAuthService>();
#endif

            builder.Services.AddSingleton<MainPage>(provider =>
                new MainPage(provider.GetRequiredService<IGoogleAuthService>()));

            return builder.Build();
        }
    }
}
