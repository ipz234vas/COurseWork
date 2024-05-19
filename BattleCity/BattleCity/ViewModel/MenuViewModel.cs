using BattleCity.Commands;
using BattleCity.Model;
using BattleCity.Services;
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
        public ObservableCollection<ListItemModel> menuList { get; }
        public MenuViewModel(AccountStore accountStore, NavigationStore navigationStore) 
        {
            menuList = new ObservableCollection<ListItemModel>
	{
		new ListItemModel("PLAY", new NavigationCommand<LevelsViewModel>(new NavigationService<LevelsViewModel>(navigationStore, () => new LevelsViewModel(accountStore, navigationStore)))),
		new ListItemModel("CONSTRUCTION", new NavigationCommand<ConstructionViewModel>(new NavigationService<ConstructionViewModel>(navigationStore, () => new ConstructionViewModel(accountStore, navigationStore)))),
		new ListItemModel("SETTINGS", new NavigationCommand<SettingsViewModel>(new NavigationService<SettingsViewModel>(navigationStore, () => new SettingsViewModel(accountStore, navigationStore)))),
		new ListItemModel("LEADERBOARD", new NavigationCommand<LeaderboardViewModel>(new NavigationService<LeaderboardViewModel>(navigationStore, () => new LeaderboardViewModel(accountStore, navigationStore))))
	};
		}
	}
}
