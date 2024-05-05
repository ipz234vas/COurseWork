using BattleCity.Model;
using BattleCity.Services;
using BattleCity.Stores;
using BattleCity.ViewModel;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;

namespace BattleCity.Commands
{
	public class ResetAccountCommand : BaseCommand
	{
		private readonly SettingsAccountResetViewModel _settingsAccountResetViewModel;
		private readonly NavigationService<SettingsAccountViewModel> _navigationService;
		private readonly AccountStore _accountStore;


		public ResetAccountCommand(SettingsAccountResetViewModel settingsAccountResetViewModel, AccountStore accountStore, NavigationService<SettingsAccountViewModel> navigationService)
		{
			_settingsAccountResetViewModel = settingsAccountResetViewModel;
			_navigationService = navigationService;
			_accountStore = accountStore;
		}

		public override void Execute(object parameter)
		{
			if(_accountStore.CurrentAccount.Id == 1)
			{
				_settingsAccountResetViewModel.SettingsAccountResetErrorMessage = "Cannot reset admin account";
				return;
			}
			using (var context = new ApplicationDbContext())
			{
				Account _account = context.Accounts.Find(_accountStore.CurrentAccount.Id);
				if (PasswordHashService.VerifyPassword(_settingsAccountResetViewModel.Password, _account.PasswordHash))
				{
					if(_settingsAccountResetViewModel.IsResetLevelProgressChecked)
					{ 
						_account.CurrentLevel = 1;
						_accountStore.CurrentAccount.CurrentLevel = _account.CurrentLevel;
					}
					if (_settingsAccountResetViewModel.IsRemoveAllCustomLevelsChecked)
					{
						var levelsToRemove = context.Levels.Where(level => level.CreatorId == _accountStore.CurrentAccount.Id);

						if(levelsToRemove != null) context.Levels.RemoveRange(levelsToRemove);
					}
					context.SaveChanges();

					_navigationService.Navigate();
				}
				else _settingsAccountResetViewModel.SettingsAccountResetErrorMessage = "Wrong password";
			}
		}
	}
}
