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
    class TankView : AnimatedObjectView
    {
        public TankView(MovementDirection direction, Dictionary<MovementDirection, ImageSource[]> imgSources, int updateInterval)
            : base(updateInterval, imgSources[direction])
        {
            this.ImagesWithDirection = imgSources;
            Direction = direction;
        }

        public Dictionary<MovementDirection, ImageSource[]> ImagesWithDirection
        {
            get { return imagesWithDirection; }
            set
            {
                imagesWithDirection = value;
                ImagesSources = imagesWithDirection[this.Direction];
            }
        }

        public MovementDirection Direction
        {
            get { return (MovementDirection)GetValue(DirectionProperty); }
            set { SetValue(DirectionProperty, value); }
        }

        public static readonly DependencyProperty DirectionProperty =
            DependencyProperty.Register("MovementDirection", typeof(MovementDirection), typeof(TankView), new PropertyMetadata(DirectionChangedCallback));

        private static void DirectionChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            TankView tv = (TankView)d;
            tv.ImagesSources = tv.ImagesWithDirection[(MovementDirection)e.NewValue];
        }

        public bool IsMoving
        {
            get { return (bool)GetValue(IsMovingProperty); }
            set { SetValue(IsMovingProperty, value); }
        }

        public static readonly DependencyProperty IsMovingProperty =
            DependencyProperty.Register("IsMoving", typeof(bool), typeof(TankView), new PropertyMetadata(false, IsMovingChangedCallback));
        private Dictionary<MovementDirection, ImageSource[]> imagesWithDirection;

        private static void IsMovingChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue.Equals(false))
                ((TankView)d).AnimationStop();
            else ((TankView)d).AnimationStart();
        }

        public override void Update(PropertiesType prop, object value)
        {
            if (prop == PropertiesType.MovementDirection)
                this.SetValue(DirectionProperty, value);
            else if (prop == PropertiesType.IsMoving)
                this.SetValue(IsMovingProperty, value);
            else
                base.Update(prop, value);
        }
    }
}
