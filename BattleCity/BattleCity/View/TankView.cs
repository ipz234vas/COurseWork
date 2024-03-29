using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace BattleCity.View
{
    public abstract class TankView : AbstractView
    {
        public Border _borderTank;
        public Grid _parentPanel;

        public TankView(Grid parentPanel) : base()
        {
            _parentPanel = parentPanel;
            Initialize();
        }

        public void InPosition(int XPosition, int YPosition)
        {
            _borderTank.Margin = new Thickness(XPosition, YPosition, 0, 0);
        }

        protected abstract override void Initialize();
    }
}
