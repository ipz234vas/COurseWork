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

		private string username;
		public string Username
		{
			get => username;
			set
			{
				username = value;
				UserExistErrorMessage = null;

				_errorsViewModel.ClearErrors(nameof(Username));
				if (!Regex.IsMatch(username, @"^[a-zA-Z0-9_-]{3,16}$"))
				{
					_errorsViewModel.AddError(nameof(Username), "Invalid Username. You can enter 3-16 characters and use '-', '_'.");
				}

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

				_errorsViewModel.ClearErrors(nameof(Password));
				if (!Regex.IsMatch(password, "^(?=.*[A-Za-z])(?=.*\\d)[A-Za-z\\d]{8,}$"))
				{
					_errorsViewModel.AddError(nameof(Password), "Invalid. Password must contain at least 8 characters, at least 1 letter and 1 number.");
				}

				_errorsViewModel.ClearErrors(nameof(ConfirmPassword));
				if (confirmPassword != password)
				{
					_errorsViewModel.AddError(nameof(ConfirmPassword), "Invalid. Passwords do not match!");
				}

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

				_errorsViewModel.ClearErrors(nameof(ConfirmPassword));
				if (confirmPassword != password)
				{
					_errorsViewModel.AddError(nameof(ConfirmPassword), "Invalid. Passwords do not match!");
				}

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
