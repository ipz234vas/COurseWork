using BattleCity.Model;
using BattleCity.Services;
using BattleCity.Stores;
using BattleCity.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

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
			using(var context = new ApplicationDbContext())
			{
				Account account = context.Accounts.FirstOrDefault(Account => Account.Username == _logInViewModel.Username);
				if (account != null)
				{
					if (PasswordHashService.VerifyPassword(_logInViewModel.Password, account.PasswordHash))
					{
						Account _account = new Account(account.Id, account.Username, account.CurrentLevel);

						_accountStore.CurrentAccount = _account;

						_navigationService.Navigate();
					}
					else _logInViewModel.LoginErrorMessage = "Wrong password";
				}
				else _logInViewModel.LoginErrorMessage = "Username is not found";
			}
		}
	}
}
