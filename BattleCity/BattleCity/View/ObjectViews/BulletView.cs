using BattleCity.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows;

namespace BattleCity.View.ObjectViews
{
    public class BulletView : AnimatedObjectView
    {
        public BulletView(int updateInterval, ImageSource[] detonation)
            : base(updateInterval, detonation, false)
        { }

        public bool Detonation
        {
            get { return (bool)GetValue(DetonationProperty); }
            set { SetValue(DetonationProperty, value); }
        }

        public static readonly DependencyProperty DetonationProperty =
            DependencyProperty.Register("Detonation", typeof(bool), typeof(BulletView), new PropertyMetadata(false, DetonationChangedCallback));

        private static void DetonationChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue.Equals(true))
            {
                BulletView bulletView = (BulletView)d;
                bulletView.Height = GameConfiguration.ObjectHeight;
                bulletView.Width = GameConfiguration.ObjectWidth;
                bulletView.X -= bulletView.Width / 2;
                bulletView.Y -= bulletView.Height / 2;
                bulletView.AnimationStart();
            }
        }

        public override void Update(PropertiesType prop, object value)
        {
            if (prop == PropertiesType.Detonation)
            {
                this.SetValue(DetonationProperty, value);
            }

            else
                base.Update(prop, value);
        }
    }
}
