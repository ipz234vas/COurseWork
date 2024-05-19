using BattleCity.Model.UnitModels;
using BattleCity.Services;
using BattleCity.Types;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BattleCity.Model.ObjectModels
{
    public class Tank : MoveableObject
    {
        public Bullet bullet { get; set; }
        private int bulletSpeed;
        //private MovementDirection lastMovementDirection;
        public int BulletSpeed
        {
            get { return bulletSpeed; }
            set
            {
                if (bulletSpeed != value)
                {
                    bulletSpeed = value;
                    OnPropertyChanged(PropertiesType.Speed, value);
                }
            }
        }
        public virtual bool IsMoving
        {
            get { return (bool)Properties[PropertiesType.IsMoving]; }
            set { Properties[PropertiesType.IsMoving] = value; }
        }
        public virtual bool IsOnIce { get; set; } = false;

        public event EventHandler TankCreated;
        public event EventHandler TankDeleted;
        public event EventHandler BulletCreated;
        public event EventHandler BulletDeleted;
        public Tank(int id, int x, int y, int width, int height, TypeObject type) : base(id, x, y, width, height, type, TypeObjectService.GetSpeed(type))
        {
            BulletSpeed = TypeObjectService.GetBulletSpeed(type);
            Properties[PropertiesType.IsMoving] = false;
        }
        public virtual void GetDamage()
        {
            Delete();
        }
        public virtual bool TryFire()
        {
            if (bullet != null) return false;

            Fire();
            return true;
        }

        protected virtual void Fire()
        {
            bullet = CreateBullet(BulletSpeed);
            bullet.Properties[PropertiesType.MovementDirection] = Properties[PropertiesType.MovementDirection];
            OnBulletCreated(bullet);
        }
        protected virtual Bullet CreateBullet(int speed)
        {
            MovementDirection direction = (MovementDirection)Properties[PropertiesType.MovementDirection];
            Point position = GetPositionForNewBullet(direction);
            int height = GameConfiguration.BulletHeight, width = GameConfiguration.BulletWidth;
            if(direction == MovementDirection.Up || direction == MovementDirection.Down)
            {
                int tmp = height;
                height = width;
                width = tmp;
            }
            return new Bullet(BaseObjectModel.NextID, position.X, position.Y, width, height, TypeObject.Bullet, speed, TypeObjectService.GetOwnerByType(Type));
        }
        protected Point GetPositionForNewBullet(MovementDirection direction)
        {
            int x = 0;
            int y = 0;
            switch (direction)
            {
                case MovementDirection.Up:
                    x = X + GameConfiguration.ObjectWidth - GameConfiguration.BulletWidth / 2;
                    y = Y + GameConfiguration.BulletHeight;
                    break;
                case MovementDirection.Right:
                    x = X + GameConfiguration.TankWidth - GameConfiguration.BulletWidth;
                    y = Y + GameConfiguration.ObjectHeight - GameConfiguration.BulletHeight / 2;
                    break;
                case MovementDirection.Down:
                    x = X + GameConfiguration.ObjectWidth - GameConfiguration.BulletWidth / 2;
                    y = Y + GameConfiguration.TankHeight - GameConfiguration.BulletHeight;
                    break;
                case MovementDirection.Left:
                    x = X + GameConfiguration.BulletWidth;
                    y = Y + GameConfiguration.ObjectHeight - GameConfiguration.BulletHeight / 2;
                    break;
            }
            return new Point(x, y);
        }
        //public void OnIce()
        //{
        //    IsOnIce = true;
        //}
        //private void OffIce()
        //{
        //    IsOnIce = false;
        //}
        //public bool CanMove(MovementDirection movement)
        //{
        //    Properties[PropertiesType.MovementDirection] = movement;
        //    MoveableObject testCollision = new MoveableObject(ID, X, Y, Width, Height, Type, Speed);

        //    switch (movement)
        //    {
        //        case MovementDirection.Left:
        //            testCollision.X -= Speed;
        //            break;

        //        case MovementDirection.Right:
        //            testCollision.X += Speed;
        //            break;

        //        case MovementDirection.Up:
        //            testCollision.Y -= Speed;
        //            break;

        //        case MovementDirection.Down:
        //            testCollision.Y += Speed;
        //            break;
        //    }

        //    if (CheckCollisionService.CheckCollisionWithBorder(testCollision))
        //        return false;

        //    bool isOnIce = false;
        //    foreach (var obj in CheckCollisionService.objects)
        //    {
        //        if (ID != obj.ID && CheckCollisionService.CheckCollision(testCollision, obj))
        //        {
        //            if (obj is Bullet)
        //                return true;

        //            if (obj.Type == TypeObject.Forest)
        //                return true;

        //            if (obj.Type == TypeObject.Ice)
        //            {
        //                isOnIce = true;
        //            }
        //            else
        //            {
        //                return false;
        //            }
        //        }
        //    }

        //    if (isOnIce)
        //    {
        //        OnIce();
        //    }
        //    else if (IsOnIce)
        //    {
        //        OffIce();
        //    }

        //    return true;
        //}

        public override void Move(MovementDirection movement)
        {
            IsMoving = true;
            //lastMovementDirection = movement;
            base.Move(movement);
        }
        //public void TryStop()
        //{
        //    if(IsOnIce)
        //    {
        //        if (IsOnIce)
        //        {
        //            if (!CanMove(lastMovementDirection))
        //            {
        //                Stop();
        //            }
        //            else
        //            {
        //                Move(lastMovementDirection);
        //            }
        //        }
        //        else
        //        {
        //            Stop();
        //        }
        //    }
        //}
        public virtual void Stop()
        {
            IsMoving = false;
        }
        protected virtual void OnBulletCreated(Bullet bullet)
        {
            bullet.BulletDestroyed += (sender, args) => OnBulletDeleted();
            BulletCreated?.Invoke(bullet, EventArgs.Empty);
        }
        protected virtual void OnBulletDeleted()
        {
            BulletDeleted?.Invoke(bullet, EventArgs.Empty);
            bullet = null;
        }
        public void Create()
        {
            TankCreated?.Invoke(this, EventArgs.Empty);
        }
        public void Delete()
        {
            TankDeleted?.Invoke(this, EventArgs.Empty);
        }
    }
}
