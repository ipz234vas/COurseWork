using BattleCity.Types;
using BattleCity.View.UnitViews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace BattleCity.View.ObjectViews
{
    class BrickWallView : BaseObjectView
    {

        public TypeBrickWall TypeBrickWall
        {
            set
            {
                switch (value)
                {
                    case TypeBrickWall.Whole:
                        Source = new BitmapImage(new Uri(@"../../Resources/Images/Blocks/BrickWall/BrickWall.png", UriKind.Relative));
                        Height = GameConfiguration.ObjectHeight;
                        Width = GameConfiguration.ObjectWidth;
                        break;
                    case TypeBrickWall.Top:
                        Source = new BitmapImage(new Uri(@"../../Resources/Images/Blocks/BrickWall/Top.png", UriKind.Relative));
                        Height = GameConfiguration.ObjectHeight / 2;
                        Width = GameConfiguration.ObjectWidth;
                        break;
                    case TypeBrickWall.Bottom:
                        Source = new BitmapImage(new Uri(@"../../Resources/Images/Blocks/BrickWall/Bottom.png", UriKind.Relative));
                        Height = GameConfiguration.ObjectHeight / 2;
                        Width = GameConfiguration.ObjectWidth;
                        break;
                    case TypeBrickWall.Left:
                        Source = new BitmapImage(new Uri(@"../../Resources/Images/Blocks/BrickWall/Left.png", UriKind.Relative));
                        Height = GameConfiguration.ObjectHeight;
                        Width = GameConfiguration.ObjectWidth / 2;
                        break;
                    case TypeBrickWall.Right:
                        Source = new BitmapImage(new Uri(@"../../Resources/Images/Blocks/BrickWall/Right.png", UriKind.Relative));
                        Height = GameConfiguration.ObjectHeight;
                        Width = GameConfiguration.ObjectWidth / 2;
                        break;
                    case TypeBrickWall.TopLeft:
                        Source = new BitmapImage(new Uri(@"../../Resources/Images/Blocks/BrickWall/TopLeft.png", UriKind.Relative));
                        Height = GameConfiguration.ObjectHeight /2;
                        Width = GameConfiguration.ObjectWidth / 2;
                        break;
                    case TypeBrickWall.TopRight:
                        Source = new BitmapImage(new Uri(@"../../Resources/Images/Blocks/BrickWall/TopRight.png", UriKind.Relative));
                        Height = GameConfiguration.ObjectHeight / 2;
                        Width = GameConfiguration.ObjectWidth / 2;
                        break;
                    case TypeBrickWall.BottomLeft:
                        Source = new BitmapImage(new Uri(@"../../Resources/Images/Blocks/BrickWall/BottomLeft.png", UriKind.Relative));
                        Height = GameConfiguration.ObjectHeight / 2;
                        Width = GameConfiguration.ObjectWidth / 2;
                        break;
                    case TypeBrickWall.BottomRight:
                        Source = new BitmapImage(new Uri(@"../../Resources/Images/Blocks/BrickWall/BottomRight.png", UriKind.Relative));
                        Height = GameConfiguration.ObjectHeight / 2;
                        Width = GameConfiguration.ObjectWidth / 2;
                        break;
                }
            }
        }

        public override void Update(PropertiesType prop, object value)
        {
            if (prop == PropertiesType.TypeBrickWall)
                TypeBrickWall = (TypeBrickWall)value;
            else
                base.Update(prop, value);
        }
    }
}
