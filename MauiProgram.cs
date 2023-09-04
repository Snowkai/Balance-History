using Balance_History.src;
using Balance_History.ViewModels;
using Microsoft.Extensions.Logging;

namespace Balance_History
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

            builder.Services.AddSingleton<DatabaseContex>();
            builder.Services.AddSingleton<RecordViewModel>();
            builder.Services.AddSingleton<MainPage>();


            return builder.Build();
        }
    }
}