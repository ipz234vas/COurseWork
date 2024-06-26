﻿using BattleCity.Commands;
using BattleCity.Services;
using BattleCity.Stores;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Windows.Documents;
using System.Windows.Input;

namespace BattleCity.ViewModel
{
	public class LogInViewModel : BaseViewModel, INotifyDataErrorInfo
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
				LoginErrorMessage = null;

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
				LoginErrorMessage = null;

				validationService.ValidatePassword(nameof(Password), password);

				OnPropertyChanged(nameof(Password));
			}
		}
		private string loginErrorMessage;

		public string LoginErrorMessage
		{
			get => loginErrorMessage;
			set
			{
				loginErrorMessage = value;
				OnPropertyChanged(nameof(LoginErrorMessage));
			}
		}

		public bool HasErrors => _errorsViewModel.HasErrors;
		public bool CanLogIn => !HasErrors;

		public ICommand LoginCommand { get; }
		public ICommand NavigateSignUpCommand { get; }
		public ICommand NavigateMenuCommand { get; }

		public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

		public LogInViewModel(AccountStore accountStore, NavigationStore navigationStore)
		{
			_errorsViewModel = new ErrorsViewModel();
			_errorsViewModel.ErrorsChanged += ErrorsViewModel_ErrorsChanged;
			validationService = new AccountValidationService(_errorsViewModel);

			Username = "";
			Password = "";

			LoginCommand = new LogInCommand(this, accountStore, new NavigationService<MenuViewModel>(navigationStore, () => new MenuViewModel(accountStore, navigationStore)));
			NavigateSignUpCommand = new NavigationCommand<SignUpViewModel>(new NavigationService<SignUpViewModel>(navigationStore, () => new SignUpViewModel(accountStore, navigationStore)));
			NavigateMenuCommand = new NavigationCommand<MenuViewModel>(new NavigationService<MenuViewModel>(navigationStore, () => new MenuViewModel(accountStore, navigationStore)));
		}

		public IEnumerable GetErrors(string propertyName)
		{
			return _errorsViewModel.GetErrors(propertyName);
		}

		private void ErrorsViewModel_ErrorsChanged(object sender, DataErrorsChangedEventArgs e)
		{
			ErrorsChanged?.Invoke(this, e);
			OnPropertyChanged(nameof(CanLogIn));
		}
	}
}