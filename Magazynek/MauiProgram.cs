using Microsoft.Extensions.Logging;

namespace Magazynek
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            // DB ERRORS WORKAROUND
#if ANDROID && DEBUG
            Platforms.Android.DangerousTrustProvider.Register();
#endif
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

            return builder.Build();
        }
    }
}