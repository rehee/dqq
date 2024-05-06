using DQQ.Attributes;
using DQQ.Profiles.Items;
using DQQ.Profiles.Mobs;
using DQQ.Profiles.Skills;
using ReheeCmf.Helpers;
using ReheeCmf.Utility.CmfRegisters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DQQ.Pools
{
  public static partial class DQQPool
  {

    public static void InitPool()
    {
      AppDomain currentDomain = AppDomain.CurrentDomain;
      Assembly[] assemblies = currentDomain.GetAssemblies();
      var pooledType = typeof(PooledAttribute);
      foreach (var assembly in assemblies)
      {
        foreach (var type in assembly.GetTypes())
        {
          if (type.CustomAttributes.Any(b => b.AttributeType == pooledType))
          {
            var instance = Activator.CreateInstance(type);

            if (instance is SkillProfile sp)
            {
              DQQPool.SkillPool.TryAdd(sp.SkillNumber, sp);
            }
            if (instance is MobProfile mp)
            {
              DQQPool.MobPool.TryAdd(mp.ProfileNumber, mp);
            }
            if (instance is ItemProfile ip)
            {
              DQQPool.ItemPool.TryAdd(ip.ProfileNumber, ip);
            }
          }

        }
      }
    }
  }
}
