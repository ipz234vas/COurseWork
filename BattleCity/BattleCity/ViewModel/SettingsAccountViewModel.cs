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
	public class SettingsAccountViewModel : BaseViewModel
	{
		private readonly AccountStore _accountStore;
		public ICommand NavigateSettingsMenuCommand { get; }
		public ICommand NavigateSettingsAccountNameCommand { get; }
		public ICommand NavigateSettingsAccountPasswordCommand { get; }
		public ICommand NavigateSettingsAccountResetCommand { get; }
		public ICommand NavigateSettingsAccountDeleteCommand { get; }
		public ICommand LogInCommand { get; }
		public ICommand LogOutCommand { get; }

		private ICommand _logActionCommand;
		public ICommand LogActionCommand
		{
			get => _logActionCommand;
			set
			{
				if (_logActionCommand != value)
				{
					_logActionCommand = value;
					OnPropertyChanged(nameof(LogActionCommand));
				}
			}
		}
		public bool IsLoggedIn => _accountStore.IsLoggedIn;
		public string LogActionButtonText => _accountStore.IsLoggedIn ? "LOG OUT" : "LOG IN";
		public SettingsAccountViewModel(AccountStore accountStore, NavigationStore navigationStore, NavigationStore settingsNavigationStore)
		{
			_accountStore = accountStore;
			AccountStore_AccountChanged();
			LogInCommand = new NavigationCommand<LogInViewModel>(new NavigationService<LogInViewModel>(navigationStore, () => new LogInViewModel(_accountStore, navigationStore)));
			LogOutCommand = new LogOutCommand(_accountStore);
			UpdateLogActionCommand();
			_accountStore.CurrentAccountChanged += AccountStore_AccountChanged;

			NavigateSettingsMenuCommand = new NavigationCommand<SettingsMenuViewModel>(new NavigationService<SettingsMenuViewModel>(settingsNavigationStore, () => new SettingsMenuViewModel(accountStore, navigationStore, settingsNavigationStore)));
			NavigateSettingsAccountNameCommand = new NavigationCommand<SettingsAccountNameViewModel>(new NavigationService<SettingsAccountNameViewModel>(settingsNavigationStore, () => new SettingsAccountNameViewModel(accountStore, navigationStore, settingsNavigationStore)));
			NavigateSettingsAccountPasswordCommand = new NavigationCommand<SettingsAccountPasswordViewModel>(new NavigationService<SettingsAccountPasswordViewModel>(settingsNavigationStore, () => new SettingsAccountPasswordViewModel(accountStore, navigationStore, settingsNavigationStore)));
			NavigateSettingsAccountResetCommand = new NavigationCommand<SettingsAccountResetViewModel>(new NavigationService<SettingsAccountResetViewModel>(settingsNavigationStore, () => new SettingsAccountResetViewModel(accountStore, navigationStore, settingsNavigationStore)));
			NavigateSettingsAccountDeleteCommand = new NavigationCommand<SettingsAccountDeleteViewModel>(new NavigationService<SettingsAccountDeleteViewModel>(settingsNavigationStore, () => new SettingsAccountDeleteViewModel(accountStore, navigationStore, settingsNavigationStore)));
		}
		private void UpdateLogActionCommand()
		{
			LogActionCommand = _accountStore.IsLoggedIn ? LogOutCommand : LogInCommand;
		}

		private void AccountStore_AccountChanged()
		{
			OnPropertyChanged(nameof(LogActionButtonText));
			OnPropertyChanged(nameof(IsLoggedIn));
			UpdateLogActionCommand();
		}
	}
}
