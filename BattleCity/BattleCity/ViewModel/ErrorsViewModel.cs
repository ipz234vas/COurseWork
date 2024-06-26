﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BattleCity.ViewModel
{
	public class ErrorsViewModel : INotifyDataErrorInfo
	{
		private readonly Dictionary<string, List<string>> _propertyErrors = new Dictionary<string, List<string>>();

		public bool HasErrors => _propertyErrors.Any();

		public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

		public IEnumerable GetErrors(string propertyName)
		{
			return _propertyErrors.GetValueOrDefault(propertyName, null);
		}

		public void AddError(string propertyName, string errorMessage)
		{
			if (!_propertyErrors.ContainsKey(propertyName))
			{
				_propertyErrors.Add(propertyName, new List<string>());
			}

			_propertyErrors[propertyName].Add(errorMessage);
			OnErrorsChanged(propertyName);
		}

		public void ClearErrors(string propertyName)
		{
			if (_propertyErrors.Remove(propertyName))
			{
				OnErrorsChanged(propertyName);
			}
		}

		private void OnErrorsChanged(string propertyName)
		{
			ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
		}

		public void ValidateAndAddError(string propertyName, string value, string regexPattern, string errorMessage)
		{
			ClearErrors(propertyName);
			if (!Regex.IsMatch(value, regexPattern))
			{
				AddError(propertyName, errorMessage);
			}
		}
	}
}
