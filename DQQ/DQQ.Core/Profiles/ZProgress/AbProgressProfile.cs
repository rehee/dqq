using DQQ.Components.Stages.Actors.Characters;
using DQQ.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DQQ.Profiles.ZProgress
{
	public abstract class ProgressProfile : DQQProfile<EnumProgress>
	{
		public abstract bool AvaliableCheck(Character? character);
	}
}
