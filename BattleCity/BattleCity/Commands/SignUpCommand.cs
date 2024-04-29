using BattleCity.Model;
using BattleCity.Services;
using BattleCity.Stores;
using BattleCity.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace BattleCity.Commands
{
	public class SignUpCommand : BaseCommand
	{
		private readonly SignUpViewModel _signUpViewModel;
		private readonly NavigationService<LogInViewModel> _navigationService;
		private readonly AccountStore _accountStore;

		public SignUpCommand(SignUpViewModel signUpViewModel, AccountStore accountStore, NavigationService<LogInViewModel> navigationService)
		{
			_signUpViewModel = signUpViewModel;
			_navigationService = navigationService;
			_accountStore = accountStore;
		}

		public override void Execute(object parameter)
		{
			using (var context = new ApplicationDbContext())
			{
				if (context.Accounts.FirstOrDefault(Account => Account.Username == _signUpViewModel.Username) != null)
				{
					_signUpViewModel.UserExistErrorMessage = "Username is already taken";
					return;
				}
				Account _account = new Account(_signUpViewModel.Username, _signUpViewModel.Password, 1);
				context.Accounts.Add(_account);
				context.SaveChanges();

				_navigationService.Navigate();
			}
		}
	}
}
