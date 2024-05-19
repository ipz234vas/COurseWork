using BattleCity.Types;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleCity.Model.UnitModels
{
    public class BaseObjectModel
    {
        private static int nextId = 0;
        private Rectangle boundingBox;
        private TypeObject type;
        private readonly ObjectProperties properties;
        private readonly int id;

        public BaseObjectModel(int id, int x, int y, int width, int height, TypeObject type)
        {
            this.id = id;
            this.type = type;
            boundingBox = new Rectangle(x, y, width, height);
            properties = new ObjectProperties(this);
            nextId++;
        }

        public event Action<int, PropertiesType, object> PropertyChanged;
        public event EventHandler Remove;

        public static int NextID { get { return nextId; } }

        public int ID { get { return id; } }
        public int X
        {
            get { return boundingBox.X; }
            set
            {
                if (boundingBox.X != value)
                {
                    boundingBox.X = value;
                    OnPropertyChanged(PropertiesType.X, value);
                }
            }
        }
        public int Y
        {
            get { return boundingBox.Y; }
            set
            {
                if (boundingBox.Y != value)
                {
                    boundingBox.Y = value;
                    OnPropertyChanged(PropertiesType.Y, value);
                }
            }
        }
        public int Width
        {
            get { return boundingBox.Width; }
            set
            {
                if (value != boundingBox.Width)
                {
                    boundingBox.Width = value;
                }
            }
        }
        public int Height
        {
            get { return boundingBox.Height; }
            set
            {
                if (value != boundingBox.Height)
                    boundingBox.Height = value;
            }
        }
        public Rectangle BoundingBox { get { return boundingBox; } }
        public TypeObject Type { get { return type; } }
        public IDictionary<PropertiesType, object> Properties { get { return properties; } }
        public void OnPropertyChanged(PropertiesType type, Object value)
        {
            PropertyChanged?.Invoke(ID, type, value);
        }
        public virtual void OnRemove()
        {
            Remove?.Invoke(this, EventArgs.Empty);
        }
    }
}
