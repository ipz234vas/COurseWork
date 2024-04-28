using BattleCity.Stores;
using BattleCity.View.Windows;
using BattleCity.ViewModel;
using System.Configuration;
using System.Data;
using System.Windows;

namespace BattleCity
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
		protected override void OnStartup(StartupEventArgs e)
		{
			AccountStore accountStore = new AccountStore();
			NavigationStore navigationStore = new NavigationStore();
			navigationStore.CurrentViewModel = new LogInViewModel(accountStore, navigationStore);
			MainWindow = new MainWindow()
			{
				DataContext = new MainViewModel(navigationStore)
			};

			MainWindow.Show();

			base.OnStartup(e);
		}
	}
}
