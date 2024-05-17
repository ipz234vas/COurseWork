using BattleCity.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleCity.Model.UnitModels
{
	public class ObjectProperties : Dictionary<PropertiesType, object>, IDictionary<PropertiesType, object>
	{
		private BaseObjectModel _object;

		public new object this[PropertiesType key]
		{
			get
			{
				return base[key];
			}
			set
			{
				object obj;
				_object.Properties.TryGetValue(key, out obj);
				if (!value.Equals(obj))
				{
					base[key] = value;
					_object.OnPropertyChanged(key, value);
				}
			}
		}

		public ObjectProperties(BaseObjectModel unit)
		{
			this._object = unit;
		}
	}
}
