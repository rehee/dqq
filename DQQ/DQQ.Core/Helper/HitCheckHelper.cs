using DQQ.Commons;
using DQQ.Components.Stages;
using DQQ.Components.Stages.Maps;
using DQQ.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DQQ.Helper
{
  public static class HitCheckHelper
  {
    public static EnumHitCheck HitCheck(this ITarget? from, ITarget? to, IMap map, SkillHitCheck? skillCheck)
    {
      if (to == null)
      {
        return EnumHitCheck.Hit;
      }
      if (BlockCheck(to, map, skillCheck?.IgnoreCheck))
      {
        return EnumHitCheck.Block;
      }
      if (DodgeCheck(to, map, skillCheck?.IgnoreCheck))
      {
        return EnumHitCheck.Dodge;
      }
      if (MissCheck(from, to, map, skillCheck?.IgnoreCheck))
      {
        return EnumHitCheck.Miss;
      }
      return EnumHitCheck.Hit;
    }

    public static bool BlockCheck(ITarget? to, IMap map, EnumHitCheck[]? skillCheck)
    {

      return false;
    }
    public static bool DodgeCheck(ITarget? to, IMap map, EnumHitCheck[]? skillCheck)
    {
      return false;
    }
    public static bool MissCheck(ITarget? from, ITarget? to, IMap map, EnumHitCheck[]? skillCheck)
    {
      return false;
    }
    //check dodge

    //check miss


  }
}
