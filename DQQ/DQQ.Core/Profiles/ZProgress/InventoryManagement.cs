using DQQ.Attributes;
using DQQ.Components.Stages.Actors.Characters;
using DQQ.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DQQ.Profiles.ZProgress
{
	[Pooled]
	public class InventoryManagement : ProgressProfile
	{
		public override EnumProgress ProfileNumber => EnumProgress.InventoryManagement;

		public override string? Name => "物品管理";

		public override string? Discription => "物品管理";

		public override bool AvaliableCheck(Character? character)
		{
			return character?.Level > 1;
		}
	}
}
