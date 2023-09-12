using Balance_History.src;
using Balance_History.ViewModels;
using Microsoft.Extensions.Logging;
using Microsoft.Maui.LifecycleEvents;
using System.Reflection;
using System.Text;
using System.Text.Json;

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
                .ConfigureLifecycleEvents(events =>
                {
#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
                    events.AddWindows(windows => windows
                    .OnLaunching(async (windows, args) =>
                    {
                        string path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"\appdata.json";
                        if (File.Exists(path) == true)
                        {
                            SaveJsonToDisk data = new SaveJsonToDisk();
                            using (FileStream reader = new FileStream(path, FileMode.Open, FileAccess.Read))
                            {
                                data = JsonSerializer.Deserialize<SaveJsonToDisk>(reader);
                            }
                            AppData.ConvertToObserver(data.Categories);
                        }
                        else
                        {
                            AppData.SetDefault();
                        }
                    })
                    .OnClosed(async (windows, args) =>
                    {
                        string path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"\appdata.json";
                        SaveJsonToDisk sd = new SaveJsonToDisk();
                        sd.Categories = AppData.ConvertToList(AppData.Categories);
                        var saveData = JsonSerializer.Serialize(sd);
                        using (FileStream writer = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Write))
                        {
                            byte[] buffer = Encoding.UTF8.GetBytes(saveData);
                            await writer.WriteAsync(buffer, 0, buffer.Length);
                        }
                    }));
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
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