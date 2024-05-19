using BattleCity.Types;
using BattleCity.View.UnitViews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace BattleCity.View.ObjectViews
{
    class EagleView : BaseObjectView
    {
        private ImageSource img;

        public EagleView(ImageSource img)
        {
            this.img = img;
        }

        public override void Update(PropertiesType prop, object value)
        {
            if (prop == PropertiesType.Detonation && value.Equals(true))
                Source = img;
            else
                base.Update(prop, value);
        }
    }
}
