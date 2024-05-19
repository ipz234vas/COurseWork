using BattleCity.Model.UnitModels;
using BattleCity.Services;
using BattleCity.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleCity.Model.ObjectModels
{
    public class Bullet : MoveableObject
    {
        public event EventHandler BulletDestroyed;
        public event EventHandler BulletCreated;

        public ObjectOwner Owner;
        private int delayDetonation = 0;
        protected bool IsDetonation
        {
            set { Properties[PropertiesType.Detonation] = value; }
            get { return (bool)Properties[PropertiesType.Detonation]; }
        }
        public Bullet(int id, int x, int y, int width, int height, TypeObject type, int speed, ObjectOwner owner)
            : base(id, x, y, width, height, type, speed)
        {
            Owner = owner;
            IsDetonation = false;
        }

        public override void Move(MovementDirection movement)
        {
            if (IsDetonation)
            {
                delayDetonation++;
                if (delayDetonation == GameConfiguration.TimeDetonation)
                    Delete();
                return;
            }
            base.Move(movement);

            // Перевірка на зіткнення
            if (CheckCollision())
            {
                IsDetonation = true;
            }
        }

        private bool CheckCollision()
        {
            // Логіка для перевірки зіткнення
            if (CheckCollisionService.CheckCollisionWithBorder(this))
            {
                return true;
            }

            bool IsCollision = false;

            for (int i = CheckCollisionService.objects.Count - 1; i >= 0; i--)
            {
                var currentObject = CheckCollisionService.objects[i];

                if (ID != currentObject.ID && CheckCollisionService.CheckCollision(this, currentObject))
                {
                    if (currentObject is Bullet bullet)
                    {
                        if (bullet.Owner != this.Owner)
                        {
                            bullet.Delete();
                            IsCollision = true;
                        }
                        continue;
                    }

                    if (currentObject is Tank tank)
                    {
                        if (TypeObjectService.GetOwnerByType(tank.Type) != this.Owner)
                        {
                            tank.GetDamage();
                            IsCollision = true;
                        }
                        continue;
                    }
                    if (currentObject is Eagle eagle)
                    {
                        eagle.Destroyed();
                        IsCollision = true;
                        continue;
                    }
                    if (currentObject.Type == TypeObject.Water)
                    {
                        continue;
                    }
                    if (currentObject.Type == TypeObject.Forest)
                    {
                        continue;
                    }
                    if (currentObject.Type == TypeObject.BrickWall)
                    {
                        DestroyBrickWall(currentObject);
                    }
                    IsCollision = true;
                }
            }

            return IsCollision;
        }

        public virtual void Delete()
        {
            IsDetonation = false;
            BulletDestroyed?.Invoke(this, EventArgs.Empty);
        }
        public virtual void Create()
        {
            BulletCreated?.Invoke(this, EventArgs.Empty);
        }
        private void DestroyBrickWall(BaseObjectModel _object)
        {
            MovementDirection direction = (MovementDirection)Properties[PropertiesType.MovementDirection];
            switch ((TypeBrickWall)_object.Properties[PropertiesType.TypeBrickWall])
            {
                case TypeBrickWall.Whole:
                    switch (direction)
                    {
                        case MovementDirection.Up:
                            SetTypeBrickWall(_object, TypeBrickWall.Top);
                            _object.Height /= 2;
                            break;
                        case MovementDirection.Right:
                            SetTypeBrickWall(_object, TypeBrickWall.Right);
                            _object.Width /= 2;
                            _object.X += _object.Width;
                            break;
                        case MovementDirection.Down:
                            SetTypeBrickWall(_object, TypeBrickWall.Bottom);
                            _object.Height /= 2;
                            _object.Y += _object.Height;
                            break;
                        case MovementDirection.Left:
                            SetTypeBrickWall(_object, TypeBrickWall.Left);
                            _object.Width /= 2;
                            break;
                    }
                    break;
                case TypeBrickWall.Top:
                    switch (direction)
                    {
                        case MovementDirection.Up:
                        case MovementDirection.Down:
                            _object.OnRemove();
                            return;
                        case MovementDirection.Right:
                            SetTypeBrickWall(_object, TypeBrickWall.TopRight);
                            _object.X += GameConfiguration.ObjectWidth / 2;
                            break;
                        case MovementDirection.Left:
                            SetTypeBrickWall(_object, TypeBrickWall.TopLeft);
                            break;
                    }
                    _object.Width /= 2;
                    break;
                case TypeBrickWall.Bottom:
                    switch (direction)
                    {
                        case MovementDirection.Up:
                        case MovementDirection.Down:
                            _object.OnRemove();
                            return;
                        case MovementDirection.Right:
                            SetTypeBrickWall(_object, TypeBrickWall.BottomRight);
                            _object.X += GameConfiguration.ObjectWidth / 2;
                            break;
                        case MovementDirection.Left:
                            SetTypeBrickWall(_object, TypeBrickWall.BottomLeft);
                            break;
                    }
                    _object.Width /= 2;
                    break;
                case TypeBrickWall.Left:
                    switch (direction)
                    {
                        case MovementDirection.Right:
                        case MovementDirection.Left:
                            _object.OnRemove();
                            return;
                        case MovementDirection.Up:
                            SetTypeBrickWall(_object, TypeBrickWall.TopLeft);
                            break;
                        case MovementDirection.Down:
                            SetTypeBrickWall(_object, TypeBrickWall.BottomLeft);
                            _object.Y += GameConfiguration.ObjectHeight / 2;
                            break;
                    }
                    _object.Height /= 2;
                    break;
                case TypeBrickWall.Right:
                    switch (direction)
                    {
                        case MovementDirection.Right:
                        case MovementDirection.Left:
                            _object.OnRemove();
                            return;
                        case MovementDirection.Up:
                            SetTypeBrickWall(_object, TypeBrickWall.TopRight);
                            break;
                        case MovementDirection.Down:
                            SetTypeBrickWall(_object, TypeBrickWall.BottomRight);
                            _object.Y += GameConfiguration.ObjectHeight / 2;
                            break;
                    }
                    _object.Height /= 2;
                    _object.OnRemove();
                    break;
                case TypeBrickWall.TopLeft:
                    _object.OnRemove();
                    break;
                case TypeBrickWall.TopRight:
                    _object.OnRemove();
                    break;
                case TypeBrickWall.BottomLeft:
                    _object.OnRemove();
                    break;
                case TypeBrickWall.BottomRight:
                    _object.OnRemove();
                    break;
            }
        }

        private static void SetTypeBrickWall(BaseObjectModel _object, TypeBrickWall typeBrickWall)
        {
            _object.Properties[PropertiesType.TypeBrickWall] = typeBrickWall;
        }
    }
}
