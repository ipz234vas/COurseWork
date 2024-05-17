using BattleCity.Services;
using BattleCity.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleCity.Model.UnitModels
{
	public class MoveableObject : BaseObjectModel
	{
		private int speed;
		public int Speed
		{
			get { return speed; }
			set
			{
				if (speed != value)
				{
					speed = value;
					OnPropertyChanged(PropertiesType.Speed, value);
				}
			}
		}
		public MoveableObject(int id, int x, int y, int width, int height, TypeObject type, int speed) : base(id, x, y, width, height, type)
		{
			Speed = speed;
		}
		public bool CanMove(MovementDirection movement)
		{
            Properties[PropertiesType.MovementDirection] = movement;
            MoveableObject TestCollisia = new MoveableObject(ID, X, Y, Width, Height, Type, Speed);
			switch (movement)
			{
				case MovementDirection.Left:
					TestCollisia.X -= (int)speed;
					break;

				case MovementDirection.Right:
					TestCollisia.X += (int)speed;
					break;

				case MovementDirection.Up:
					TestCollisia.Y -= (int)speed;
					break;

				case MovementDirection.Down:
					TestCollisia.Y += (int)speed;
					break;
			}
			if (CheckCollisionService.CheckCollisionWithBorder(TestCollisia)) return false;
			foreach (var unit in CheckCollisionService.objects)
			{
				if (ID != unit.ID)
				{
					if (CheckCollisionService.CheckCollision(TestCollisia, unit)) return false;
				}
			}
			return true;
		}
		public virtual void Move(MovementDirection movement)
		{
			switch (movement)
			{
				case MovementDirection.Left:
					X -= speed;
					break;

				case MovementDirection.Right:
					X += speed;
					break;

				case MovementDirection.Up:
					Y -= speed;
					break;

				case MovementDirection.Down:
					Y += speed;
					break;
			}
		}
	}
}
