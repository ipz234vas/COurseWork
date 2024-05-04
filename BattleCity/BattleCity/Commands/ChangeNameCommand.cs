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
	public class ChangeNameCommand : BaseCommand
	{
		private readonly SettingsAccountNameViewModel _settingsAccountNameViewModel;
		private readonly NavigationService<SettingsAccountViewModel> _navigationService;
		private readonly AccountStore _accountStore;

		public ChangeNameCommand(SettingsAccountNameViewModel settingsAccountNameViewModel, AccountStore accountStore, NavigationService<SettingsAccountViewModel> navigationService)
		{
			_settingsAccountNameViewModel = settingsAccountNameViewModel;
			_navigationService = navigationService;
			_accountStore = accountStore;
		}

		public override void Execute(object parameter)
		{
			using (var context = new ApplicationDbContext())
			{
				Account account = context.Accounts.FirstOrDefault(Account => Account.Username == _settingsAccountNameViewModel.Username);
				if (account == null)
				{
					Account _account = context.Accounts.Find(_accountStore.CurrentAccount.Id);
					if (PasswordHashService.VerifyPassword(_settingsAccountNameViewModel.Password, _account.PasswordHash))
					{
						_account.Username = _settingsAccountNameViewModel.Username;
						context.SaveChanges();

						_accountStore.CurrentAccount.Username = _settingsAccountNameViewModel.Username;

						_navigationService.Navigate();
					}
					else _settingsAccountNameViewModel.ChangeNameErrorMessage = "Wrong password";
				}
				else _settingsAccountNameViewModel.ChangeNameErrorMessage = "Username is already taken";
			}
		}
	}
}
