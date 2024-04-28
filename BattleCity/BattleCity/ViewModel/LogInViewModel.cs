using BattleCity.Commands;
using BattleCity.Services;
using BattleCity.Stores;
using System.Windows.Input;

namespace BattleCity.ViewModel
{
	public class LogInViewModel : BaseViewModel
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

		public ICommand LoginCommand { get; }
		public ICommand NavigateSignUpCommand { get; }
		public ICommand NavigateMenuCommand { get; }

		public LogInViewModel(AccountStore accountStore, NavigationStore navigationStore)
		{
			Username = accountStore.CurrentAccount?.Username;
			Password = accountStore.CurrentAccount?.Password;
			LoginCommand = new LogInCommand(this, accountStore, new NavigationService<MenuViewModel>(navigationStore, () => new MenuViewModel(accountStore, navigationStore)));
			NavigateSignUpCommand = new NavigationCommand<SignUpViewModel>(new NavigationService<SignUpViewModel>(navigationStore, () => new SignUpViewModel(accountStore, navigationStore)));
			NavigateMenuCommand = new NavigationCommand<MenuViewModel>(new NavigationService<MenuViewModel>(navigationStore, () => new MenuViewModel(accountStore, navigationStore)));
		}
	}
}
