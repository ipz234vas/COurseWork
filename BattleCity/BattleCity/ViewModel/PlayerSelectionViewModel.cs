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
	public class PlayerSelectionViewModel : BaseViewModel
	{
		public ICommand Is1PlayerCommand { get; }
		public ICommand Is2PlayerCommand { get; }
		public PlayerSelectionViewModel(AccountStore accountStore, NavigationStore navigationStore, LevelStore levelStore)
		{
			Is1PlayerCommand = new NavigationCommand<GamePlayViewModel>(new NavigationService<GamePlayViewModel>(navigationStore, () => new GamePlayViewModel(accountStore, navigationStore, new LevelStore { CurrentLevel = levelStore.CurrentLevel, Is2Players = false})));
			Is2PlayerCommand = new NavigationCommand<GamePlayViewModel>(new NavigationService<GamePlayViewModel>(navigationStore, () => new GamePlayViewModel(accountStore, navigationStore, new LevelStore { CurrentLevel = levelStore.CurrentLevel, Is2Players = true})));
		}
	}
}
