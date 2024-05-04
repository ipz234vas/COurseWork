using BattleCity.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BattleCity.Services
{
	public class AccountValidationService
	{
		private readonly ErrorsViewModel _errorsViewModel;

		public AccountValidationService(ErrorsViewModel errorsViewModel)
		{
			_errorsViewModel = errorsViewModel;
		}

		public void ValidateUsername(string propertyName, string username)
		{
			_errorsViewModel.ClearErrors(propertyName);
			if (!Regex.IsMatch(username, @"^[a-zA-Z0-9_-]{3,16}$"))
			{
				_errorsViewModel.AddError(propertyName, "Invalid Username. You can enter 3-16 characters and use '-', '_'.");
			}
		}

		public void ValidatePassword(string propertyName, string password)
		{
			_errorsViewModel.ClearErrors(propertyName);
			if (!Regex.IsMatch(password, "^(?=.*[A-Za-z])(?=.*\\d)[A-Za-z\\d]{8,}$"))
			{
				_errorsViewModel.AddError(propertyName, "Invalid. Password must contain at least 8 characters, at least 1 letter and 1 number.");
			}
		}

		public void ValidateConfirmPassword(string propertyName, string password, string confirmPassword)
		{
			_errorsViewModel.ClearErrors(propertyName);
			if (confirmPassword != password)
			{
				_errorsViewModel.AddError(propertyName, "Invalid. Passwords do not match!");
			}
		}
	}
}
