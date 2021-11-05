using System.Windows;
using Labb3.ViewModels;

namespace Labb3
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            MainWindow mainWindow = new()
            {
                DataContext = new MainViewModel()
            };
            mainWindow.Show();
        }
    }
}
