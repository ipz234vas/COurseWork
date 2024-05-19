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
            Dictionary<MovementDirection, ImageSource[]> imgSoursec = null;
            ImageSource[] sources = null;

            switch (typeUnit)
            {
                case TypeObject.BrickWall:
                    view = new BrickWallView { Source = new BitmapImage(new Uri(@"../../Resources/Images/Blocks/BrickWall/BrickWall.png", UriKind.Relative)) };
                    break;
                case TypeObject.ConcreteWall:
                    view = new BaseObjectView { Source = new BitmapImage(new Uri(@"../../Resources/Images/Blocks/ConcreteWall.png", UriKind.Relative)) };
                    break;
                case TypeObject.Ice:
                    view = new BaseObjectView { Source = new BitmapImage(new Uri(@"../../Resources/Images/Blocks/Ice.png", UriKind.Relative)) };
                    break;
                case TypeObject.Forest:
                    view = new BaseObjectView { Source = new BitmapImage(new Uri(@"../../Resources/Images/Blocks/Forest.png", UriKind.Relative)), ZIndex = 15 };
                    break;
                case TypeObject.Eagle:
                    view = new EagleView(new BitmapImage(new Uri(@"../../Resources/Images/Eagle/EagleDestroyed.png", UriKind.Relative))) { Source = new BitmapImage(new Uri(@"../../Resources/Images/Eagle/Eagle.png", UriKind.Relative)) };
                    break;
                case TypeObject.Bullet:
                    sources = new ImageSource[]
                    {
                        new BitmapImage(new Uri(@"../../Resources/Images/Effects/Explosions/Explosion1.png", UriKind.Relative)),
                        new BitmapImage(new Uri(@"../../Resources/Images/Effects/Explosions/Explosion1.png", UriKind.Relative)),
                        new BitmapImage(new Uri(@"../../Resources/Images/Effects/Explosions/Explosion1.png", UriKind.Relative)),
                        new BitmapImage(new Uri(@"../../Resources/Images/Effects/Explosions/Explosion2.png", UriKind.Relative)),
                        new BitmapImage(new Uri(@"../../Resources/Images/Effects/Explosions/Explosion2.png", UriKind.Relative)),
                        new BitmapImage(new Uri(@"../../Resources/Images/Effects/Explosions/Explosion3.png", UriKind.Relative))
                    };
                    view = new BulletView(1, sources) { Source = GetImageForBullet((MovementDirection)properties[PropertiesType.MovementDirection]), ZIndex = 10 };
                    break;
                case TypeObject.Star:
                    sources = new ImageSource[]
                    {
                        new BitmapImage(new Uri(@"../../Resources/Images/Effects/Stars/Star1.png", UriKind.Relative)),
                        new BitmapImage(new Uri(@"../../Resources/Images/Effects/Stars/Star2.png", UriKind.Relative)),
                        new BitmapImage(new Uri(@"../../Resources/Images/Effects/Stars/Star3.png", UriKind.Relative)),
                        new BitmapImage(new Uri(@"../../Resources/Images/Effects/Stars/Star4.png", UriKind.Relative))
                    };
                    view = new AnimatedObjectView(4, sources, true);
                    break;
                case TypeObject.BigExplosion:
                    sources = new ImageSource[]
                    {
                        new BitmapImage(new Uri(@"../../Resources/Images/Effects/Explosions/Explosion1.png", UriKind.Relative)),
                        new BitmapImage(new Uri(@"../../Resources/Images/Effects/Explosions/Explosion2.png", UriKind.Relative)),
                        new BitmapImage(new Uri(@"../../Resources/Images/Effects/Explosions/Explosion3.png", UriKind.Relative)),
                        new BitmapImage(new Uri(@"../../Resources/Images/Effects/Explosions/BigExplosion1.png", UriKind.Relative)),
                        new BitmapImage(new Uri(@"../../Resources/Images/Effects/Explosions/BigExplosion1.png", UriKind.Relative)),
                        new BitmapImage(new Uri(@"../../Resources/Images/Effects/Explosions/BigExplosion1.png", UriKind.Relative)),
                        new BitmapImage(new Uri(@"../../Resources/Images/Effects/Explosions/BigExplosion2.png", UriKind.Relative)),
                        new BitmapImage(new Uri(@"../../Resources/Images/Effects/Explosions/BigExplosion2.png", UriKind.Relative)),
                        new BitmapImage(new Uri(@"../../Resources/Images/Effects/Explosions/BigExplosion2.png", UriKind.Relative)),
                        new BitmapImage(new Uri(@"../../Resources/Images/Effects/Explosions/Explosion3.png", UriKind.Relative)),
                        new BitmapImage(new Uri(@"../../Resources/Images/Effects/Explosions/Explosion2.png", UriKind.Relative)),
                        new BitmapImage(new Uri(@"../../Resources/Images/Effects/Explosions/Explosion1.png", UriKind.Relative))
                    };
                    view = new BigExplosionView(1, sources, true) { ZIndex = 11, initialX = x, initialY = y, initialHeight = height, initialWidth = width};
                    break;
                case TypeObject.SmallTankPlayer:
                    if ((bool)properties[PropertiesType.IsSecondPlayer] == false)
                        imgSoursec = new Dictionary<MovementDirection, ImageSource[]>
                    {
                        {MovementDirection.Up, new []
                        { new BitmapImage(new Uri(@"../../Resources/Images/Tanks/Player1/SmallTank/Up1.png", UriKind.Relative)), new BitmapImage(new Uri(@"../../Resources/Images/Tanks/Player1/SmallTank/Up2.png", UriKind.Relative)) } },
                        {MovementDirection.Down, new []
                        { new BitmapImage(new Uri(@"../../Resources/Images/Tanks/Player1/SmallTank/Down1.png", UriKind.Relative)), new BitmapImage(new Uri(@"../../Resources/Images/Tanks/Player1/SmallTank/Down2.png", UriKind.Relative)) } },
                        {MovementDirection.Left, new []
                        { new BitmapImage(new Uri(@"../../Resources/Images/Tanks/Player1/SmallTank/Left1.png", UriKind.Relative)), new BitmapImage(new Uri(@"../../Resources/Images/Tanks/Player1/SmallTank/Left2.png", UriKind.Relative)) } },
                        {MovementDirection.Right,new []
                        { new BitmapImage(new Uri(@"../../Resources/Images/Tanks/Player1/SmallTank/Right1.png", UriKind.Relative)), new BitmapImage(new Uri(@"../../Resources/Images/Tanks/Player1/SmallTank/Right2.png", UriKind.Relative)) } },
                    };
                    else imgSoursec = new Dictionary<MovementDirection, ImageSource[]>
                    {
                        {MovementDirection.Up, new []
                        { new BitmapImage(new Uri(@"../../Resources/Images/Tanks/Player2/SmallTank/Up1.png", UriKind.Relative)), new BitmapImage(new Uri(@"../../Resources/Images/Tanks/Player2/SmallTank/Up2.png", UriKind.Relative)) } },
                        {MovementDirection.Down, new []
                        { new BitmapImage(new Uri(@"../../Resources/Images/Tanks/Player2/SmallTank/Down1.png", UriKind.Relative)), new BitmapImage(new Uri(@"../../Resources/Images/Tanks/Player2/SmallTank/Down2.png", UriKind.Relative)) } },
                        {MovementDirection.Left, new []
                        { new BitmapImage(new Uri(@"../../Resources/Images/Tanks/Player2/SmallTank/Left1.png", UriKind.Relative)), new BitmapImage(new Uri(@"../../Resources/Images/Tanks/Player2/SmallTank/Left2.png", UriKind.Relative)) } },
                        {MovementDirection.Right,new []
                        { new BitmapImage(new Uri(@"../../Resources/Images/Tanks/Player2/SmallTank/Right1.png", UriKind.Relative)), new BitmapImage(new Uri(@"../../Resources/Images/Tanks/Player2/SmallTank/Right2.png", UriKind.Relative)) } },
                    };
                    view = new TankView((MovementDirection)properties[PropertiesType.MovementDirection], imgSoursec, 6);
                    break;
                case TypeObject.BasicTank:
                    imgSoursec = new Dictionary<MovementDirection, ImageSource[]>
                    {
                        {MovementDirection.Up, new []
                        { new BitmapImage(new Uri(@"../../Resources/Images/Tanks/Enemy/BasicTank/Up1.png", UriKind.Relative)), new BitmapImage(new Uri(@"../../Resources/Images/Tanks/Enemy/BasicTank/Up2.png", UriKind.Relative)) } },
                        {MovementDirection.Down, new []
                        { new BitmapImage(new Uri(@"../../Resources/Images/Tanks/Enemy/BasicTank/Down1.png", UriKind.Relative)), new BitmapImage(new Uri(@"../../Resources/Images/Tanks/Enemy/BasicTank/Down2.png", UriKind.Relative)) } },
                        {MovementDirection.Left, new []
                        { new BitmapImage(new Uri(@"../../Resources/Images/Tanks/Enemy/BasicTank/Left1.png", UriKind.Relative)), new BitmapImage(new Uri(@"../../Resources/Images/Tanks/Enemy/BasicTank/Left2.png", UriKind.Relative)) } },
                        {MovementDirection.Right,new []
                        { new BitmapImage(new Uri(@"../../Resources/Images/Tanks/Enemy/BasicTank/Right1.png", UriKind.Relative)), new BitmapImage(new Uri(@"../../Resources/Images/Tanks/Enemy/BasicTank/Right2.png", UriKind.Relative)) } },
                    };
                    view = new TankView((MovementDirection)properties[PropertiesType.MovementDirection], imgSoursec, 6);
                    break;
                case TypeObject.FastTank:
                    imgSoursec = new Dictionary<MovementDirection, ImageSource[]>
                    {
                        {MovementDirection.Up, new []
                        { new BitmapImage(new Uri(@"../../Resources/Images/Tanks/Enemy/FastTank/Up1.png", UriKind.Relative)), new BitmapImage(new Uri(@"../../Resources/Images/Tanks/Enemy/FastTank/Up2.png", UriKind.Relative)) } },
                        {MovementDirection.Down, new []
                        { new BitmapImage(new Uri(@"../../Resources/Images/Tanks/Enemy/FastTank/Down1.png", UriKind.Relative)), new BitmapImage(new Uri(@"../../Resources/Images/Tanks/Enemy/FastTank/Down2.png", UriKind.Relative)) } },
                        {MovementDirection.Left, new []
                        { new BitmapImage(new Uri(@"../../Resources/Images/Tanks/Enemy/FastTank/Left1.png", UriKind.Relative)), new BitmapImage(new Uri(@"../../Resources/Images/Tanks/Enemy/FastTank/Left2.png", UriKind.Relative)) } },
                        {MovementDirection.Right,new []
                        { new BitmapImage(new Uri(@"../../Resources/Images/Tanks/Enemy/FastTank/Right1.png", UriKind.Relative)), new BitmapImage(new Uri(@"../../Resources/Images/Tanks/Enemy/FastTank/Right2.png", UriKind.Relative)) } },
                    };
                    view = new TankView((MovementDirection)properties[PropertiesType.MovementDirection], imgSoursec, 6);
                    break;
                case TypeObject.PowerTank:
                    imgSoursec = new Dictionary<MovementDirection, ImageSource[]>
                    {
                        {MovementDirection.Up, new []
                        { new BitmapImage(new Uri(@"../../Resources/Images/Tanks/Enemy/PowerTank/Up1.png", UriKind.Relative)), new BitmapImage(new Uri(@"../../Resources/Images/Tanks/Enemy/PowerTank/Up2.png", UriKind.Relative)) } },
                        {MovementDirection.Down, new []
                        { new BitmapImage(new Uri(@"../../Resources/Images/Tanks/Enemy/PowerTank/Down1.png", UriKind.Relative)), new BitmapImage(new Uri(@"../../Resources/Images/Tanks/Enemy/PowerTank/Down2.png", UriKind.Relative)) } },
                        {MovementDirection.Left, new []
                        { new BitmapImage(new Uri(@"../../Resources/Images/Tanks/Enemy/PowerTank/Left1.png", UriKind.Relative)), new BitmapImage(new Uri(@"../../Resources/Images/Tanks/Enemy/PowerTank/Left2.png", UriKind.Relative)) } },
                        {MovementDirection.Right,new []
                        { new BitmapImage(new Uri(@"../../Resources/Images/Tanks/Enemy/PowerTank/Right1.png", UriKind.Relative)), new BitmapImage(new Uri(@"../../Resources/Images/Tanks/Enemy/PowerTank/Right2.png", UriKind.Relative)) } },
                    };
                    view = new TankView((MovementDirection)properties[PropertiesType.MovementDirection], imgSoursec, 6);
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

        private static ImageSource GetImageForBullet(MovementDirection dir)
        {
            switch (dir)
            {
                case MovementDirection.Up:
                    return new BitmapImage(new Uri(@"../../Resources/Images/Bullets/BulletUp.png", UriKind.Relative));
                case MovementDirection.Right:
                    return new BitmapImage(new Uri(@"../../Resources/Images/Bullets/BulletRight.png", UriKind.Relative));
                case MovementDirection.Down:
                    return new BitmapImage(new Uri(@"../../Resources/Images/Bullets/BulletDown.png", UriKind.Relative));
                case MovementDirection.Left:
                    return new BitmapImage(new Uri(@"../../Resources/Images/Bullets/BulletLeft.png", UriKind.Relative));
            }
            return null;
        }
    }
}
