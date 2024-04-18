using BattleCity.Commands;
using BattleCity.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Navigation;

namespace BattleCity.ViewModel
{
    public class MenuViewModel : ViewModel
    {
        
        public ObservableCollection<MenuItemModel> menuList { get; } = new ObservableCollection<MenuItemModel>
    {
        new MenuItemModel("1 PLAYER", new NavigationCommand(NavigateToPage, new Uri("View/Pages/PageSettings.xaml", UriKind.Relative))),
        new MenuItemModel("2 PLAYERS", new NavigationCommand(NavigateToPage, new Uri("View/Pages/PageSettings.xaml", UriKind.Relative))),
        new MenuItemModel("CONSTRUCTION", new NavigationCommand(NavigateToPage, new Uri("View/Pages/PageSettings.xaml", UriKind.Relative))),
        new MenuItemModel("SETTINGS", new NavigationCommand(NavigateToPage, new Uri("View/Pages/PageSettings.xaml", UriKind.Relative))),
        new MenuItemModel("LEADERBOARD", new NavigationCommand(NavigateToPage, new Uri("View/Pages/PageSettings.xaml", UriKind.Relative)))
    };
    }
}
