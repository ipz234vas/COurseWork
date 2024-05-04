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
	public class SettingsAccountPasswordViewModel : BaseViewModel, INotifyDataErrorInfo
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

		private string newPassword;
		public string NewPassword
		{
			get => newPassword;
			set
			{
				newPassword = value;

				validationService.ValidatePassword(nameof(NewPassword), newPassword);
				validationService.ValidateConfirmPassword(nameof(ConfirmNewPassword), newPassword, confirmNewPassword);

				OnPropertyChanged(nameof(NewPassword));
			}
		}
		private string confirmNewPassword;
		public string ConfirmNewPassword
		{
			get => confirmNewPassword;
			set
			{
				confirmNewPassword = value;

				validationService.ValidateConfirmPassword(nameof(ConfirmNewPassword), newPassword, confirmNewPassword);

				OnPropertyChanged(nameof(ConfirmNewPassword));
			}
		}
		private string wrongPasswordErrorMessage;
		public string WrongPasswordErrorMessage
		{
			get => wrongPasswordErrorMessage;
			set
			{
				wrongPasswordErrorMessage = value;
				OnPropertyChanged(nameof(WrongPasswordErrorMessage));
			}
		}
		public bool HasErrors => _errorsViewModel.HasErrors;
		public bool CanChangePassword => !HasErrors;
		public ICommand NavigateSettingsAccountCommand { get; }
		public ICommand ChangePasswordCommand { get; }

		public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;
		public SettingsAccountPasswordViewModel(AccountStore accountStore, NavigationStore navigationStore, NavigationStore settingsNavigationStore)
		{
			_errorsViewModel = new ErrorsViewModel();
			_errorsViewModel.ErrorsChanged += ErrorsViewModel_ErrorsChanged;
			validationService = new AccountValidationService(_errorsViewModel);

			Password = "";
			NewPassword = "";
			ConfirmNewPassword = "";

			NavigateSettingsAccountCommand = new NavigationCommand<SettingsAccountViewModel>(new NavigationService<SettingsAccountViewModel>(settingsNavigationStore, () => new SettingsAccountViewModel(accountStore, navigationStore, settingsNavigationStore)));
			ChangePasswordCommand = new ChangePasswordCommand(this, accountStore, new NavigationService<SettingsAccountViewModel>(settingsNavigationStore, () => new SettingsAccountViewModel(accountStore, navigationStore, settingsNavigationStore)));
		}
		public IEnumerable GetErrors(string propertyName)
		{
			return _errorsViewModel.GetErrors(propertyName);
		}

		private void ErrorsViewModel_ErrorsChanged(object sender, DataErrorsChangedEventArgs e)
		{
			ErrorsChanged?.Invoke(this, e);
			OnPropertyChanged(nameof(CanChangePassword));
		}

	}
}
