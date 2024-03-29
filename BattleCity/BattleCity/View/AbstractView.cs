using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;

namespace BattleCity.View
{
    public abstract class AbstractView : UserControl
    {
        public AbstractView() =>
            this.Loaded += ViewBase_Loaded;

        protected abstract void Initialize();

        protected virtual void ViewBase_Loaded(object sender, RoutedEventArgs e) =>
            Initialize();
    }
}
