using Balance_History.src;
using System.Text.Json;

namespace Balance_History
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new AppShell();
        }
    }
}