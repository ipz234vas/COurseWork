using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleCity.Model
{
    public class PositionModel
    {
        public int XPosition = 0;
        public int YPosition = 0;

        public delegate void Position(int XPosition, int YPosition);
        public event Position _position;

        public PositionModel() { }

        public PositionModel(int xPosition, int yPosition)
        {
            XPosition = xPosition;
            YPosition = yPosition;
        }
    }
}
