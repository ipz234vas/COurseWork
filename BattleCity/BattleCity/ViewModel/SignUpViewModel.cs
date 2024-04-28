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
	public class SignUpViewModel : BaseViewModel
	{

		private string username;
		public string Username
		{
			get => username;
			set
			{
				username = value;
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
				OnPropertyChanged(nameof(ConfirmPassword));
			}
		}
		public ICommand SignUpCommand { get; }
		public ICommand NavigateLogInCommand { get; }
		public ICommand NavigateMenuCommand { get; }

		public SignUpViewModel(AccountStore accountStore, NavigationStore navigationStore)
		{
			SignUpCommand = new SignUpCommand(this, accountStore, new NavigationService<LogInViewModel>(navigationStore, () => new LogInViewModel(accountStore, navigationStore)));
			NavigateLogInCommand = new NavigationCommand<LogInViewModel>(new NavigationService<LogInViewModel>(navigationStore, () => new LogInViewModel(accountStore, navigationStore)));
			NavigateMenuCommand = new NavigationCommand<MenuViewModel>(new NavigationService<MenuViewModel>(navigationStore, () => new MenuViewModel(accountStore, navigationStore)));
		}
	}
}
