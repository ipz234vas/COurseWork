using BattleCity.Model.UnitModels;
using BattleCity.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleCity.Model.ObjectModels
{
    public class Tank : MoveableObject
    {
        private int bulletSpeed;
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
        public event EventHandler TankCreated;
        public event EventHandler TankDeleted;
        public Tank(int id, int x, int y, int width, int height, TypeObject type, int speed, int bulletSpeed) : base(id, x, y, width, height, type, speed)
        {
            BulletSpeed = bulletSpeed;
            Properties[PropertiesType.IsMoving] = false;
        }
        public override void Move(MovementDirection movement)
        {
            IsMoving = true;
            base.Move(movement);
        }
        public virtual void Stop()
        {
            IsMoving = false;
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
