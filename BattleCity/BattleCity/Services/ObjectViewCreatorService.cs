using BattleCity.Model.ObjectModels;
using BattleCity.Types;
using BattleCity.View.ObjectViews;
using BattleCity.View.UnitViews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace BattleCity.Services
{
    public static class ObjectViewCreatorService
    {
        public static BaseObjectView Create(int id, int x, int y, int width, int height, TypeObject typeUnit, IDictionary<PropertiesType, object> properties)
        {
            BaseObjectView view = null;

            switch (typeUnit)
            {
                case TypeObject.BrickWall:
                    view = new BaseObjectView { Source = new BitmapImage(new Uri(@"../../Resources/Images/Blocks/BrickWall.png", UriKind.Relative)) };
                    break;
                case TypeObject.ConcreteWall:
                    view = new BaseObjectView { Source = new BitmapImage(new Uri(@"../../Resources/Images/Blocks/ConcreteWall.png", UriKind.Relative)) };
                    break;
                case TypeObject.Ice:
                    view = new BaseObjectView { Source = new BitmapImage(new Uri(@"../../Resources/Images/Blocks/Ice.png", UriKind.Relative)) };
                    break;
                case TypeObject.Forest:
                    view = new BaseObjectView { Source = new BitmapImage(new Uri(@"../../Resources/Images/Blocks/Forest.png", UriKind.Relative)) };
                    break;
                case TypeObject.SmallTankPlayer:
                    Dictionary<MovementDirection, ImageSource[]> imgSoursec = new Dictionary<MovementDirection, ImageSource[]>
                    {
                        {MovementDirection.Up, new []
                        { new BitmapImage(new Uri(@"../../Resources/Images/Tanks/Up1.png", UriKind.Relative)), new BitmapImage(new Uri(@"../../Resources/Images/Tanks/Up2.png", UriKind.Relative)) } },
                        {MovementDirection.Down, new []
                        { new BitmapImage(new Uri(@"../../Resources/Images/Tanks/Down1.png", UriKind.Relative)), new BitmapImage(new Uri(@"../../Resources/Images/Tanks/Down2.png", UriKind.Relative)) } },
                        {MovementDirection.Left, new []
                        { new BitmapImage(new Uri(@"../../Resources/Images/Tanks/Left1.png", UriKind.Relative)), new BitmapImage(new Uri(@"../../Resources/Images/Tanks/Left2.png", UriKind.Relative)) } },
                        {MovementDirection.Right,new []
                        { new BitmapImage(new Uri(@"../../Resources/Images/Tanks/Right1.png", UriKind.Relative)), new BitmapImage(new Uri(@"../../Resources/Images/Tanks/Right2.png", UriKind.Relative)) } },
                    };
                    view = new TankView(MovementDirection.Up, imgSoursec, 6);
                    //view = new BaseObjectView { Source = new BitmapImage(new Uri(@"../../Resources/Images/Tanks/Tank.png", UriKind.Relative)) };
                    break;
                case TypeObject.Star:
                    ImageSource[] sources = new ImageSource[]
                    {
                        new BitmapImage(new Uri(@"../../Resources/Images/Effects/Stars/Star1.png", UriKind.Relative)),
                        new BitmapImage(new Uri(@"../../Resources/Images/Effects/Stars/Star2.png", UriKind.Relative)),
                        new BitmapImage(new Uri(@"../../Resources/Images/Effects/Stars/Star3.png", UriKind.Relative)),
                        new BitmapImage(new Uri(@"../../Resources/Images/Effects/Stars/Star4.png", UriKind.Relative))
                    };
                    view = new AnimatedObjectView(4, sources, true);
                    break;
                default:
                    view = new BaseObjectView();
                    break;
            }
            view.ID = id;
            view.X = x;
            view.Y = y;
            view.Width = width;
            view.Height = height;
            view.TypeUnit = typeUnit;
            return view;
        }
    }
}
