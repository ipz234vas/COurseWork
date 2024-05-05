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
	public class SettingsAccountResetViewModel : BaseViewModel, INotifyDataErrorInfo
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
		private string settingsAccountResetErrorMessage;
		public string SettingsAccountResetErrorMessage
		{
			get => settingsAccountResetErrorMessage;
			set
			{
				settingsAccountResetErrorMessage = value;
				OnPropertyChanged(nameof(SettingsAccountResetErrorMessage));
			}
		}
		private bool _isResetLevelProgressChecked;
		public bool IsResetLevelProgressChecked
		{
			get { return _isResetLevelProgressChecked; }
			set
			{
				if (_isResetLevelProgressChecked != value)
				{
					_isResetLevelProgressChecked = value;
					OnPropertyChanged(nameof(IsResetLevelProgressChecked));
					OnPropertyChanged(nameof(CanReset));
				}
			}
		}

		private bool _isRemoveAllCustomLevelsChecked;
		public bool IsRemoveAllCustomLevelsChecked
		{
			get { return _isRemoveAllCustomLevelsChecked; }
			set
			{
				if (_isRemoveAllCustomLevelsChecked != value)
				{
					_isRemoveAllCustomLevelsChecked = value;
					OnPropertyChanged(nameof(IsRemoveAllCustomLevelsChecked));
					OnPropertyChanged(nameof(CanReset));
				}
			}
		}
		public bool HasErrors => _errorsViewModel.HasErrors || (!IsRemoveAllCustomLevelsChecked && !IsResetLevelProgressChecked);
		public bool CanReset => !HasErrors;
		public ICommand NavigateSettingsAccountCommand { get; }
		public ICommand ResetAccountCommand { get; }

		public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;
		
		public SettingsAccountResetViewModel(AccountStore accountStore, NavigationStore navigationStore, NavigationStore settingsNavigationStore)
		{
			_errorsViewModel = new ErrorsViewModel();
			_errorsViewModel.ErrorsChanged += ErrorsViewModel_ErrorsChanged;
			validationService = new AccountValidationService(_errorsViewModel);

			Password = "";
			IsResetLevelProgressChecked = true; 
			IsRemoveAllCustomLevelsChecked = false;

			NavigateSettingsAccountCommand = new NavigationCommand<SettingsAccountViewModel>(new NavigationService<SettingsAccountViewModel>(settingsNavigationStore, () => new SettingsAccountViewModel(accountStore, navigationStore, settingsNavigationStore)));
			ResetAccountCommand = new ResetAccountCommand(this, accountStore, new NavigationService<SettingsAccountViewModel>(settingsNavigationStore, () => new SettingsAccountViewModel(accountStore, navigationStore, settingsNavigationStore)));
		}
		public IEnumerable GetErrors(string propertyName)
		{
			return _errorsViewModel.GetErrors(propertyName);
		}

		private void ErrorsViewModel_ErrorsChanged(object sender, DataErrorsChangedEventArgs e)
		{
			ErrorsChanged?.Invoke(this, e);
			OnPropertyChanged(nameof(CanReset));
		}
	}
}
