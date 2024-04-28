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
			if (_signUpViewModel.Password == null || _signUpViewModel.Username == null || _signUpViewModel.Password == "")
			{
				MessageBox.Show("Some field is empty");
				return;
			}
			if (_signUpViewModel.Password != _signUpViewModel.ConfirmPassword) 
			{
				MessageBox.Show("Passwords do not match");
				return; 
			}
				Account account = new Account()
			{
				Username = _signUpViewModel.Username,
				Password = _signUpViewModel.Password
			};

			_accountStore.CurrentAccount = account;

			_navigationService.Navigate();
		}
	}
}
