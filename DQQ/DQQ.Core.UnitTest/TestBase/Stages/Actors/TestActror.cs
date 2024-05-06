using DQQ.Commons;
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

    public override Task Initialize(IDQQEntity profile)
    {
      throw new NotImplementedException();
    }

    public override async Task<ContentResponse<bool>> OnTick(IEnumerable<ITarget>? targets, IMap? map)
    {
      var parent = await base.OnTick(targets, map);

      if (parent.Success)
      {
        TickCount++;
      }
      return parent;
    }
  }
}
