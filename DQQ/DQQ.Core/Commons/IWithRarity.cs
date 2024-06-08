using DQQ.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DQQ
{
	public interface IWithRarity
	{
		EnumRarity Rarity { get; set; }
	}
}
