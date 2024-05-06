using DQQ.Pools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DQQ.UnitTest
{
  public class BaseTest
  {
    [SetUp]
    public virtual async Task Setup()
    {
      await Task.CompletedTask;
      DQQPool.InitPool();
    }
  }
}
