using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;

namespace BattleCity.View.ObjectViews
{
    public class BigExplosionView : AnimatedObjectView
    {
        public double initialX;
        public double initialY;
        public double initialWidth;
        public double initialHeight;
        public BigExplosionView(int updateInterval, ImageSource[] _imagesSources, bool animationStart = false) : base(updateInterval, _imagesSources, animationStart)
        {
            
        }
        protected override void UpdateSource()
        {
            switch(base.frame)
            {
                case 0:
                case 1:
                case 2:
                case 9:
                case 10:
                case 11:
                    Height = initialHeight;
                    Width = initialWidth;
                    X = initialX;
                    Y = initialY;
                    break;
                case 3:
                case 4:
                case 5:
                case 6:
                case 7:
                case 8:
                    Height = initialHeight * 2;
                    Width = initialWidth * 2;
                    X = initialX - initialWidth / 2;
                    Y = initialY - initialHeight / 2;
                    break;
                default: break;
            }
            base.UpdateSource();
        }
    }
}
