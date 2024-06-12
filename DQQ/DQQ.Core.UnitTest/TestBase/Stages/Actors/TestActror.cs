using DQQ.Commons;
using DQQ.Components;
using DQQ.Components.Parameters;
using DQQ.Components.Stages;
using DQQ.Components.Stages.Actors;
using DQQ.Components.Stages.Maps;
using DQQ.Entities;
using ReheeCmf.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DQQ.UnitTest.TestBase.Stages.Actors
{
	public class TestActror : Actor
	{
		public int TickCount { get; set; }

		public TestActror()
		{
			DisplayId = Guid.NewGuid();
		}
		public override void Initialize(IDQQEntity profile, DQQComponent? parent)
		{
			throw new NotImplementedException();
		}

		public override async Task<ContentResponse<bool>> OnTick(ComponentTickParameter parameter)
		{
			var parent = await base.OnTick(parameter);

			if (parent.Success)
			{
				TickCount++;
			}
			return parent;
		}
	}
}
