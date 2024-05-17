using BattleCity.Commands;
using BattleCity.Model;
using BattleCity.Services;
using BattleCity.Stores;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BattleCity.ViewModel
{
	public class LeaderboardViewModel : BaseViewModel
	{
		private ObservableCollection<Account> _accounts;
		public ObservableCollection<Account> Accounts
		{
			get => _accounts;
			set
			{
				_accounts = value;
				OnPropertyChanged(nameof(Accounts));
			}
		}
		public ICommand NavigateMenuCommand { get; }
		public LeaderboardViewModel(AccountStore accountStore, NavigationStore navigationStore)
		{
			NavigateMenuCommand = new NavigationCommand<MenuViewModel>(new NavigationService<MenuViewModel>(navigationStore, () => new MenuViewModel(accountStore, navigationStore)));
			LoadLeaderBoard();
		}
		private async void LoadLeaderBoard()
		{
			await Task.Run(() => {
                using (var context = new ApplicationDbContext())
                {
                    var Accounts = context.Accounts.Where(account => account.CurrentLevel != null).ToList();

                    var sortedAccounts = Accounts.OrderByDescending(account => account.CurrentLevel);

                    var selectedAccounts = sortedAccounts.Select(account => new Account
                    {
                        Username = account.Username,
                        CurrentLevel = account.CurrentLevel
                    });

                    this.Accounts = new ObservableCollection<Account>(selectedAccounts);
                }
            });
		}
	}
}
