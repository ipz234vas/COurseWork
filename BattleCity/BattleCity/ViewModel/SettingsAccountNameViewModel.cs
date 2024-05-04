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
	public class SettingsAccountNameViewModel : BaseViewModel, INotifyDataErrorInfo
	{
		private readonly ErrorsViewModel _errorsViewModel;

		private readonly AccountValidationService validationService;

		private string username;
		public string Username
		{
			get => username;
			set
			{
				username = value;
				ChangeNameErrorMessage = null;

				validationService.ValidateUsername(nameof(Username), username);

				OnPropertyChanged(nameof(Username));
			}
		}

		private string password;
		public string Password
		{
			get => password;
			set
			{
				password = value;
				ChangeNameErrorMessage = null;

				validationService.ValidatePassword(nameof(Password), password);

				OnPropertyChanged(nameof(Password));
			}
		}
		private string changeNameErrorMessage;

		public string ChangeNameErrorMessage
		{
			get => changeNameErrorMessage;
			set
			{
				changeNameErrorMessage = value;
				OnPropertyChanged(nameof(ChangeNameErrorMessage));
			}
		}

		public bool HasErrors => _errorsViewModel.HasErrors;
		public bool CanChangeName => !HasErrors;
		public ICommand NavigateSettingsAccountCommand { get; }
		public ICommand ChangeNameCommand { get; }

		public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

		public SettingsAccountNameViewModel(AccountStore accountStore, NavigationStore navigationStore, NavigationStore settingsNavigationStore)
		{
			_errorsViewModel = new ErrorsViewModel();
			_errorsViewModel.ErrorsChanged += ErrorsViewModel_ErrorsChanged;
			validationService = new AccountValidationService(_errorsViewModel);

			Username = "";
			Password = "";

			NavigateSettingsAccountCommand = new NavigationCommand<SettingsAccountViewModel>(new NavigationService<SettingsAccountViewModel>(settingsNavigationStore, () => new SettingsAccountViewModel(accountStore, navigationStore, settingsNavigationStore)));
			ChangeNameCommand = new ChangeNameCommand(this, accountStore, new NavigationService<SettingsAccountViewModel>(settingsNavigationStore, () => new SettingsAccountViewModel(accountStore, navigationStore, settingsNavigationStore)));
		}
		public IEnumerable GetErrors(string propertyName)
		{
			return _errorsViewModel.GetErrors(propertyName);
		}

		private void ErrorsViewModel_ErrorsChanged(object sender, DataErrorsChangedEventArgs e)
		{
			ErrorsChanged?.Invoke(this, e);
			OnPropertyChanged(nameof(CanChangeName));
		}
	}
}
