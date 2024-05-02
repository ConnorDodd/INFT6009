using INFT6009.Services.PartialMethods;

namespace INFT6009
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            WindowSizeHandler.CallSetWindowSize();

            MainPage = new AppShell();
        }
    }
}
