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
	public class SettingsGameViewModel : BaseViewModel
	{
		public ICommand NavigateSettingsMenuCommand { get; }

		public SettingsGameViewModel(AccountStore accountStore, NavigationStore navigationStore, NavigationStore settingsNavigationStore) 
		{
			NavigateSettingsMenuCommand = new NavigationCommand<SettingsMenuViewModel>(new NavigationService<SettingsMenuViewModel>(settingsNavigationStore, () => new SettingsMenuViewModel(accountStore, navigationStore, settingsNavigationStore)));
		}
	}
}
