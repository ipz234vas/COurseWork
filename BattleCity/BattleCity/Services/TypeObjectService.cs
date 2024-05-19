using BattleCity.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace BattleCity.Services
{
    public static class TypeObjectService
    {
        public static ObjectOwner GetOwnerByType(TypeObject type)
        {
            switch (type)
            {
                case TypeObject.SmallTankPlayer:
                case TypeObject.MediumTankPlaeyr:
                case TypeObject.HeavyTankPlaeyr:
                case TypeObject.LightTankPlaeyr:
                    return ObjectOwner.Player;
                case TypeObject.BasicTank:
                case TypeObject.FastTank:
                case TypeObject.ArmorTank:
                case TypeObject.PowerTank:
                    return ObjectOwner.Enemy;
                default:
                    return 0;
            }
        }
        public static TypeObject GetEnemyType(char typeChar)
        {
            // Визначте тип ворога на основі символу
            switch (typeChar)
            {
                case 'A': return TypeObject.ArmorTank;
                case 'B': return TypeObject.BasicTank;
                case 'F': return TypeObject.FastTank;
                case 'P': return TypeObject.PowerTank;
                default: return TypeObject.BasicTank;
            }
        }
        public static int GetSpeed(TypeObject type)
        {
            switch (type)
            {
                case TypeObject.SmallTankPlayer:
                case TypeObject.LightTankPlaeyr:
                case TypeObject.HeavyTankPlaeyr:
                case TypeObject.MediumTankPlaeyr:
                case TypeObject.PowerTank:
                case TypeObject.ArmorTank:
                    return GameConfiguration.NormalTankSpeed;
                case TypeObject.BasicTank:
                    return GameConfiguration.SlowTankSpeed;
                case TypeObject.FastTank:
                    return GameConfiguration.FastTankSpeed;
                default:
                    return 0;
            }
        }

        public static int GetBulletSpeed(TypeObject type)
        {
            switch (type)
            {
                case TypeObject.LightTankPlaeyr:
                case TypeObject.HeavyTankPlaeyr:
                case TypeObject.MediumTankPlaeyr:
                case TypeObject.FastTank:
                case TypeObject.ArmorTank:
                    return GameConfiguration.NormalBulletSpeed;
                case TypeObject.BasicTank:
                case TypeObject.SmallTankPlayer:
                    return GameConfiguration.SlowBulletSpeed;
                case TypeObject.PowerTank:
                    return GameConfiguration.FastBulletSpeed;
                default:
                    return 0;
            }
        }
        public static TypeObject GetTypeUnitBySymbol(char symbol)
        {
            TypeObject type = 0;
            switch (symbol)
            {
                case GameConfiguration.BrickWallSymbol:
                    type = TypeObject.BrickWall;
                    break;
                case GameConfiguration.ConcreteWallSymbol:
                    type = TypeObject.ConcreteWall;
                    break;
                case GameConfiguration.WaterSymbol:
                    type = TypeObject.Water;
                    break;
                case GameConfiguration.ForestSymbol:
                    type = TypeObject.Forest;
                    break;
                default:
                    break;
            }
            return type;
        }
    }
}
