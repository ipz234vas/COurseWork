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
	public class ChangePasswordCommand : BaseCommand
	{
		private readonly SettingsAccountPasswordViewModel _settingsAccountPasswordViewModel;
		private readonly NavigationService<SettingsAccountViewModel> _navigationService;
		private readonly AccountStore _accountStore;

		public ChangePasswordCommand(SettingsAccountPasswordViewModel settingsAccountPasswordViewModel, AccountStore accountStore, NavigationService<SettingsAccountViewModel> navigationService)
		{
			_settingsAccountPasswordViewModel = settingsAccountPasswordViewModel;
			_navigationService = navigationService;
			_accountStore = accountStore;
		}

		public override void Execute(object parameter)
		{
			using (var context = new ApplicationDbContext())
			{
				Account _account = context.Accounts.Find(_accountStore.CurrentAccount.Id);
				if (PasswordHashService.VerifyPassword(_settingsAccountPasswordViewModel.Password, _account.PasswordHash))
				{
					_account.PasswordHash = PasswordHashService.HashPassword(_settingsAccountPasswordViewModel.NewPassword);
					context.SaveChanges();

					_navigationService.Navigate();
				}
				else _settingsAccountPasswordViewModel.WrongPasswordErrorMessage = "Wrong password";
			}
		}
	}
}
