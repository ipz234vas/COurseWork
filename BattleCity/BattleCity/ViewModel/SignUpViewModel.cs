using BattleCity.Commands;
using BattleCity.Services;
using BattleCity.Stores;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BattleCity.ViewModel
{
	public class SignUpViewModel : BaseViewModel, INotifyDataErrorInfo
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
				UserExistErrorMessage = null;

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

				validationService.ValidatePassword(nameof(Password), password);
				validationService.ValidateConfirmPassword(nameof(ConfirmPassword), password, confirmPassword);

				OnPropertyChanged(nameof(Password));
			}
		}
		private string confirmPassword;
		public string ConfirmPassword
		{
			get => confirmPassword;
			set
			{
				confirmPassword = value;

				validationService.ValidateConfirmPassword(nameof(ConfirmPassword), password, confirmPassword);

				OnPropertyChanged(nameof(ConfirmPassword));
			}
		}
		private string userExistErrorMessage;
		public string UserExistErrorMessage
		{
			get => userExistErrorMessage;
			set
			{
				userExistErrorMessage = value;
				OnPropertyChanged(nameof(UserExistErrorMessage));
			}
		}
		public bool HasErrors => _errorsViewModel.HasErrors;
		public bool CanSignUp => !HasErrors;

		public ICommand SignUpCommand { get; }
		public ICommand NavigateLogInCommand { get; }
		public ICommand NavigateMenuCommand { get; }

		public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

		public SignUpViewModel(AccountStore accountStore, NavigationStore navigationStore)
		{
			_errorsViewModel = new ErrorsViewModel();
			_errorsViewModel.ErrorsChanged += ErrorsViewModel_ErrorsChanged;
			validationService = new AccountValidationService(_errorsViewModel);

			Username = "";
			Password = "";
			ConfirmPassword = "";

			SignUpCommand = new SignUpCommand(this, accountStore, new NavigationService<LogInViewModel>(navigationStore, () => new LogInViewModel(accountStore, navigationStore)));
			NavigateLogInCommand = new NavigationCommand<LogInViewModel>(new NavigationService<LogInViewModel>(navigationStore, () => new LogInViewModel(accountStore, navigationStore)));
			NavigateMenuCommand = new NavigationCommand<MenuViewModel>(new NavigationService<MenuViewModel>(navigationStore, () => new MenuViewModel(accountStore, navigationStore)));
		}
		public IEnumerable GetErrors(string propertyName)
		{
			return _errorsViewModel.GetErrors(propertyName);
		}

		private void ErrorsViewModel_ErrorsChanged(object sender, DataErrorsChangedEventArgs e)
		{
			ErrorsChanged?.Invoke(this, e);
			OnPropertyChanged(nameof(CanSignUp));
		}
	}
}
