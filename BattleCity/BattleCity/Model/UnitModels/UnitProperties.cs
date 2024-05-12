using BattleCity.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleCity.Model.UnitModels
{
	public class UnitProperties : Dictionary<PropertiesType, object>, IDictionary<PropertiesType, object>
	{
		private Unit unit;

		public new object this[PropertiesType key]
		{
			get
			{
				return base[key];
			}
			set
			{
				object obj;
				unit.Properties.TryGetValue(key, out obj);
				if (!value.Equals(obj))
				{
					base[key] = value;
					unit.OnPropertyChanged(key, value);
				}
			}
		}

		public UnitProperties(Unit unit)
		{
			this.unit = unit;
		}
	}
}
