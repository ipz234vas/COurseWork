using BattleCity.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleCity.Stores
{
	public class NavigationStore
	{
		public event Action CurrentViewModelChanged;

		private BaseViewModel _CurrentViewModel;
		public BaseViewModel CurrentViewModel
		{
			get => _CurrentViewModel;
			set
			{
				_CurrentViewModel = value;
				OnCurrentViewModelChanged();
			}
		}

		private void OnCurrentViewModelChanged()
		{
			CurrentViewModelChanged?.Invoke();
		}
	}
}
