using Balance_History.src;
using Balance_History.ViewModels;
using Microsoft.Extensions.Logging;
using Microsoft.Maui.LifecycleEvents;
using System.Text;

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
                })
            
#if DEBUG
            builder.Logging.AddDebug();
#endif

            builder.Services.AddSingleton<DatabaseContex>();
            builder.Services.AddSingleton<RecordViewModel>();
            builder.Services.AddSingleton<MainPage>();

            
            return builder.Build();
        }


        private static async void SaveAppData()
        {
            using (BinaryWriter writer = new BinaryWriter(File.Open("category.dat", FileMode.OpenOrCreate)))
            {
                foreach (var cat in AppData.Categories)
                {
                    writer.Write(cat);
                }
            }
        }
        private static async void ReadAppData()
        {
            using(BinaryReader reader = new BinaryReader(File.Open("category.dat", FileMode.Open)))
            {
                while(reader.PeekChar() > -1) {
                AppData.Categories.Add(reader.ReadString());
                }
            }
        }
    }
}