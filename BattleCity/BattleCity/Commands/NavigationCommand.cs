using BattleCity.Services;
using BattleCity.Stores;
using BattleCity.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace BattleCity.Commands
{
    public class NavigationCommand<TViewModel> : BaseCommand
        where TViewModel : BaseViewModel
    {
        private readonly NavigationService<TViewModel> _navigationService;

		public NavigationCommand(NavigationService<TViewModel> navigationService)
        {
            _navigationService = navigationService;
        }

		public override void Execute(object parameter)
		{
            _navigationService.Navigate();
		}
	}
}
