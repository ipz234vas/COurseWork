using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BattleCity.View.Pages
{
    /// <summary>
    /// Interaction logic for PageMenu.xaml
    /// </summary>
    public partial class PageMenu : Page
    {
        public PageMenu()
        {
            InitializeComponent();
        }

        private void menuList_MouseEnter(object sender, MouseEventArgs e)
        {
            if (sender is ListBoxItem menuListItem)
            {
                menuListItem.Focus();
            }
        }

        private void menuList_Loaded(object sender, RoutedEventArgs e)
        {
            var menuList = sender as ListBox;
            if (menuList != null && menuList.Items.Count > 0)
            {
                var firstItem = menuList.ItemContainerGenerator.ContainerFromIndex(0) as ListBoxItem;
                if (firstItem != null)
                {
                    firstItem.Focus();
                    menuList.SelectedItem = firstItem;
                }
            }
        }
        private void ListBoxItem_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                ListBoxItem listBoxItem = sender as ListBoxItem;
                if (listBoxItem != null)
                {
                    Button button = FindVisualChild<Button>(listBoxItem);
                    if (button != null)
                    {
                        button.Focus();
                        ICommand command = button.Command;
                        if (command != null && command.CanExecute(button.CommandParameter))
                        {
                            command.Execute(button.CommandParameter);
                        }
                    }
                }
            }
        }

        private T FindVisualChild<T>(DependencyObject obj) where T : DependencyObject
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(obj); i++)
            {
                DependencyObject child = VisualTreeHelper.GetChild(obj, i);
                if (child != null && child is T)
                {
                    return (T)child;
                }
                else
                {
                    T childOfChild = FindVisualChild<T>(child);
                    if (childOfChild != null)
                    {
                        return childOfChild;
                    }
                }
            }
            return null;
        }
    }
}
