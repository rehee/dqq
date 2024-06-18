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
	public class BuildManagement : ProgressProfile
	{
		public override EnumProgress ProfileNumber => EnumProgress.BuildManagement;

		public override string? Name => "构筑(BD)管理";

		public override string? Discription => "构筑(BD)管理";

		public override bool AvaliableCheck(Character? character)
		{
			return character?.Level > 30;
		}
	}
}
