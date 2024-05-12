using BattleCity.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleCity.ViewModel
{
	public class GamePlayViewModel : BaseViewModel
	{
		private GameFieldViewModel gameFieldViewModel;
		public GameFieldViewModel GameFieldViewModel
		{
			get { return gameFieldViewModel; }
			set
			{
				gameFieldViewModel = value;
				OnPropertyChanged(nameof(GameFieldViewModel));
			}
		}
		public GamePlayViewModel(AccountStore accountStore, NavigationStore navigationStore, LevelStore levelStore)
		{
			GameFieldViewModel = new GameFieldViewModel();
		}
	}
}
