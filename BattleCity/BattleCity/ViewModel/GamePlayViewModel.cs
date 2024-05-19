using BattleCity.Commands;
using BattleCity.Model;
using BattleCity.Services;
using BattleCity.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Navigation;

namespace BattleCity.ViewModel
{
	public class GamePlayViewModel : BaseViewModel
	{
        private Visibility pauseVisibility = Visibility.Hidden;

        public Visibility PauseVisibility
        {
            get => pauseVisibility;
            set
            {
                if (pauseVisibility != value)
                {
                    pauseVisibility = value;
                    OnPropertyChanged();
                }
            }
        }
        public ICommand Command { get; }
		public Account CurrentAccount { get; set; }
        public void GameOver(bool IsWon)
		{
			string MessageText;
			if(IsWon)
			{
				MessageText = "You are winner!";
				if (CurrentAccount.CurrentLevel != null) CurrentAccount.CurrentLevel++;
				else SessionUserStore.CurrentLevel++;
			}
			else MessageText = "Enemies won!";
			MessageBox.Show("Game Over! " + MessageText);
			Command?.Execute(null);
        }
		public void OnPausedChanged()
		{
			if(PauseVisibility == Visibility.Hidden) PauseVisibility = Visibility.Visible;
			else if(PauseVisibility == Visibility.Visible) PauseVisibility = Visibility.Hidden;
		}
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
			CurrentAccount = accountStore.CurrentAccount;
			GameFieldViewModel = new GameFieldViewModel(levelStore);
			GameFieldViewModel.OnGameOver += GameOver;
			GameFieldViewModel.PausedChanged += OnPausedChanged;
			Command = new NavigationCommand<MenuViewModel>(new NavigationService<MenuViewModel>(navigationStore, () => new MenuViewModel(accountStore, navigationStore)));
		}
	}
}
