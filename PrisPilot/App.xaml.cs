using PrisPilot.Services;
using PrisPilot.Stores;
using PrisPilot.ViewModels;
using System.Configuration;
using System.Data;
using System.Windows;

namespace PrisPilot
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private NavigationStore _navigationStore;
        private FileDialogService _fileDialogService;
        protected override void OnStartup(StartupEventArgs e)
        {
            _navigationStore = new NavigationStore();
            _fileDialogService = new FileDialogService();
            _navigationStore.CurrentViewModel = new HomeViewModel(_navigationStore, _fileDialogService);
            MainWindow = new MainWindow()
            {
                DataContext = new MainViewModel(_navigationStore)
            };
            MainWindow.Show();

            base.OnStartup(e);
        }


    }

}
