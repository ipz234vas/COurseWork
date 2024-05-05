using BattleCity.Commands;
using BattleCity.Services;
using BattleCity.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BattleCity.ViewModel
{
	public class SettingsMenuViewModel : BaseViewModel
	{
		public ICommand NavigateMenuCommand { get; }
		public ICommand NavigateSettingsAccountCommand { get; }
		public ICommand NavigateSettingsGameCommand { get; }
		public SettingsMenuViewModel(AccountStore accountStore, NavigationStore navigationStore, NavigationStore settingsNavigationStore) 
		{
			NavigateMenuCommand = new NavigationCommand<MenuViewModel>(new NavigationService<MenuViewModel>(navigationStore, () => new MenuViewModel(accountStore, navigationStore)));
			NavigateSettingsAccountCommand = new NavigationCommand<SettingsAccountViewModel>(new NavigationService<SettingsAccountViewModel>(settingsNavigationStore, () => new SettingsAccountViewModel(accountStore, navigationStore, settingsNavigationStore)));
			NavigateSettingsGameCommand = new NavigationCommand<SettingsGameViewModel>(new NavigationService<SettingsGameViewModel>(settingsNavigationStore, () => new SettingsGameViewModel(accountStore, navigationStore, settingsNavigationStore)));
		}
	}
}
