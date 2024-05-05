using BattleCity.Commands;
using BattleCity.Services;
using BattleCity.Stores;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BattleCity.ViewModel
{
	public class SettingsAccountDeleteViewModel : BaseViewModel, INotifyDataErrorInfo
	{
		private readonly ErrorsViewModel _errorsViewModel;

		private readonly AccountValidationService validationService;

		private string password;
		public string Password
		{
			get => password;
			set
			{
				password = value;

				validationService.ValidatePassword(nameof(Password), password);

				OnPropertyChanged(nameof(Password));
			}
		}
		private string settingsAccountDeleteErrorMessage;
		public string SettingsAccountDeleteErrorMessage
		{
			get => settingsAccountDeleteErrorMessage;
			set
			{
				settingsAccountDeleteErrorMessage = value;
				OnPropertyChanged(nameof(SettingsAccountDeleteErrorMessage));
			}
		}
		private bool _isAccountDeletionConfirmed;
		public bool IsAccountDeletionConfirmed
		{
			get { return _isAccountDeletionConfirmed; }
			set
			{
				if (_isAccountDeletionConfirmed != value)
				{
					_isAccountDeletionConfirmed = value;
					OnPropertyChanged(nameof(IsAccountDeletionConfirmed));
					OnPropertyChanged(nameof(CanDelete));
				}
			}
		}
		public bool HasErrors => _errorsViewModel.HasErrors || !IsAccountDeletionConfirmed;
		public bool CanDelete => !HasErrors;
		public ICommand NavigateSettingsAccountCommand { get; }
		public ICommand DeleteAccountCommand { get; }
		public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;
		public SettingsAccountDeleteViewModel(AccountStore accountStore, NavigationStore navigationStore, NavigationStore settingsNavigationStore)
		{
			_errorsViewModel = new ErrorsViewModel();
			_errorsViewModel.ErrorsChanged += ErrorsViewModel_ErrorsChanged;
			validationService = new AccountValidationService(_errorsViewModel);

			Password = "";
			IsAccountDeletionConfirmed = false;
			NavigateSettingsAccountCommand = new NavigationCommand<SettingsAccountViewModel>(new NavigationService<SettingsAccountViewModel>(settingsNavigationStore, () => new SettingsAccountViewModel(accountStore, navigationStore, settingsNavigationStore)));
			DeleteAccountCommand = new DeleteAccountCommand(this, accountStore, new NavigationService<SettingsAccountViewModel>(settingsNavigationStore, () => new SettingsAccountViewModel(accountStore, navigationStore, settingsNavigationStore)));
		}
		public IEnumerable GetErrors(string propertyName)
		{
			return _errorsViewModel.GetErrors(propertyName);
		}

		private void ErrorsViewModel_ErrorsChanged(object sender, DataErrorsChangedEventArgs e)
		{
			ErrorsChanged?.Invoke(this, e);
			OnPropertyChanged(nameof(CanDelete));
		}
	}
}
