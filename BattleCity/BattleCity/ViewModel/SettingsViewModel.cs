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
	public class SettingsViewModel : BaseViewModel
	{
		public ICommand NavigateMenuCommand { get; }

		public SettingsViewModel(AccountStore accountStore, NavigationStore navigationStore)
		{
			NavigateMenuCommand = new NavigationCommand<MenuViewModel>(new NavigationService<MenuViewModel>(navigationStore, () => new MenuViewModel(accountStore, navigationStore)));
		}
	}
}
