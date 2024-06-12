using DQQ.Combats;
using DQQ.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DQQ.Helper
{
	public static class AttackTypeHelper
	{
		public static void AttackTypeModify(this IWIthAttackTypes input, bool increase, params EnumAttackType[] attackTypes)
		{

			input.AttackTypes = (increase ? (input.AttackTypes ?? []).Concat(attackTypes) :
				input.AttackTypes?.Where(b => !attackTypes.Contains(b)) ?? []).Distinct()
				.ToArray();


		}
	}
}
