using DQQ.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DQQ.Combats
{
	public interface IWIthAttackTypeAndArea
	{
		EnumAttackType AttackTypes { get; set; }
		EnumAreaLevel AreaLevel { get; set; }
		int ExtraAttackNumber { get; set; }
	}
	public interface ISetAttackTypeAndArea
	{
		void SetAttackTypeAndArea(IWIthAttackTypeAndArea input);
	}
}
