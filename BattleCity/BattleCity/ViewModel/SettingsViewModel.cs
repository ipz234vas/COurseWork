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
	public class SettingsViewModel : BaseViewModel
	{
		private NavigationStore _settingsNavigationStore;
		public BaseViewModel CurrentSettingsViewModel => _settingsNavigationStore.CurrentViewModel;
		public SettingsViewModel(AccountStore accountStore, NavigationStore navigationStore, NavigationStore settingsNavigationStore = null)
		{
			_settingsNavigationStore = settingsNavigationStore ?? new NavigationStore();
			if (_settingsNavigationStore.CurrentViewModel == null)
				_settingsNavigationStore.CurrentViewModel = new SettingsMenuViewModel(accountStore, navigationStore, _settingsNavigationStore);
			_settingsNavigationStore.CurrentViewModelChanged += OnCurrentSettingsViewModelChanged;
		}
		private void OnCurrentSettingsViewModelChanged()
		{
			OnPropertyChanged(nameof(CurrentSettingsViewModel));
		}
	}
}
