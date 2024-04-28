using BattleCity.Model;
using BattleCity.Services;
using BattleCity.Stores;
using BattleCity.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleCity.Commands
{
	public class LogInCommand : BaseCommand
	{
		private readonly LogInViewModel _logInViewModel;
		private readonly NavigationService<MenuViewModel> _navigationService;
		private readonly AccountStore _accountStore;

		public LogInCommand(LogInViewModel logInViewModel, AccountStore accountStore, NavigationService<MenuViewModel> navigationService)
		{
			_logInViewModel = logInViewModel;
			_accountStore = accountStore;
			_navigationService = navigationService;
		}

		public override void Execute(object parameter)
		{
			Account account = new Account()
			{
				Username = _logInViewModel.Username,
				Password = _logInViewModel.Password
			};

			_accountStore.CurrentAccount = account;

			_navigationService.Navigate();
		}
	}
}
