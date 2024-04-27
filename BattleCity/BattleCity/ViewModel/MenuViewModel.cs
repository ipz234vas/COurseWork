using BattleCity.Commands;
using BattleCity.Model;
using BattleCity.Stores;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Navigation;

namespace BattleCity.ViewModel
{
    public class MenuViewModel : BaseViewModel
    {
        public ObservableCollection<MenuItemModel> menuList { get; }
        public MenuViewModel(NavigationStore navigationStore) 
        {
            menuList = new ObservableCollection<MenuItemModel>
	{
		new MenuItemModel("PLAY", new NavigationCommand<SettingsViewModel>(navigationStore, () => new SettingsViewModel(navigationStore))),
		new MenuItemModel("CONSTRUCTION", new NavigationCommand<SettingsViewModel>(navigationStore, () => new SettingsViewModel(navigationStore))),
		new MenuItemModel("SETTINGS", new NavigationCommand<SettingsViewModel>(navigationStore, () => new SettingsViewModel(navigationStore))),
		new MenuItemModel("LEADERBOARD", new NavigationCommand<SettingsViewModel>(navigationStore, () => new SettingsViewModel(navigationStore)))
	};
		}
	}
}
