using BattleCity.Commands;
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

		public SettingsViewModel(NavigationStore navigationStore)
		{
			NavigateMenuCommand = new NavigationCommand<MenuViewModel>(navigationStore, () => new MenuViewModel(navigationStore));
		}
	}
}
