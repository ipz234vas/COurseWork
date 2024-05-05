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
	public class DeleteAccountCommand : BaseCommand
	{

		private readonly SettingsAccountDeleteViewModel _settingsAccountDeleteViewModel;
		private readonly NavigationService<SettingsAccountViewModel> _navigationService;
		private readonly AccountStore _accountStore;

		public DeleteAccountCommand(SettingsAccountDeleteViewModel settingsAccountDeleteViewModel, AccountStore accountStore, NavigationService<SettingsAccountViewModel> navigationService)
		{
			_settingsAccountDeleteViewModel = settingsAccountDeleteViewModel;
			_accountStore = accountStore;
			_navigationService = navigationService;
		}

		public override void Execute(object parameter)
		{
			if (_accountStore.CurrentAccount.Id == 1)
			{
				_settingsAccountDeleteViewModel.SettingsAccountDeleteErrorMessage = "Cannot delete admin account";
				return;
			}
			using (var context = new ApplicationDbContext())
			{
				Account _accountToDelete = context.Accounts.Find(_accountStore.CurrentAccount.Id);
				if (PasswordHashService.VerifyPassword(_settingsAccountDeleteViewModel.Password, _accountToDelete.PasswordHash))
				{
					context.Accounts.Remove(_accountToDelete);
					context.SaveChanges();
					_accountStore.Logout();
					_navigationService.Navigate();
				}
				else _settingsAccountDeleteViewModel.SettingsAccountDeleteErrorMessage = "Wrong password";
			}
		}
	}
}