using DQQ.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DQQ.Commons
{
	public class ParameterFilterDTO<T>
	{

		public static D New<D>(T? value, bool selected) where D : ParameterFilterDTO<T>, new()
		{
			var result = new D()
			{
				Value = value,
				Selected = selected
			};
			return result;
		}
		public T? Value { get; set; }
		public bool Selected { get; set; }
	}

	public class SkillBindingTypeFilter: ParameterFilterDTO<EnumSkillBindingType>
	{

	}
}
