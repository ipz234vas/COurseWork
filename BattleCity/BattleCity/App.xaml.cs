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
			NavigationStore navigationStore = new NavigationStore();
			navigationStore.CurrentViewModel = new MenuViewModel(navigationStore);
			MainWindow = new MainWindow()
			{
				DataContext = new MainViewModel(navigationStore)
			};

			MainWindow.Show();

			base.OnStartup(e);
		}
	}

}
