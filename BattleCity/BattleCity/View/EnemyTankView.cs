using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace BattleCity.View
{
    public class EnemyTankView : TankView
    {
        public EnemyTankView(Grid parentPanel) : base(parentPanel)
        {

        }
        protected override void Initialize()
        {
            _borderTank = new Border();

            _borderTank.BorderBrush = Brushes.Black;
            _borderTank.BorderThickness = new Thickness(1);
            _borderTank.Margin = new Thickness(0, 0, 0, 0);
            _borderTank.Background = Brushes.Red;
            _borderTank.Height = 40;
            _borderTank.Width = 40;

            _parentPanel.Children.Add(_borderTank);
        }
    }
}
