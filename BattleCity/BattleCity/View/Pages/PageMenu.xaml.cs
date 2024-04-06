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

        private void ListBoxItem_MouseEnter(object sender, MouseEventArgs e)
        {
            if (sender is ListBoxItem listBoxItem)
            {
                listBoxItem.Focus();
            }
        }

        private void menuList_Loaded(object sender, RoutedEventArgs e)
        {
            var listBox = sender as ListBox;
            if (listBox != null && listBox.Items.Count > 0)
            {
                var firstItem = listBox.ItemContainerGenerator.ContainerFromIndex(0) as ListBoxItem;
                if (firstItem != null)
                {
                    firstItem.Focus();
                }
            }
        }
    }
}
