using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DQQ.Profiles
{
  public interface IDQQProfile
  {
    string? Name { get; }
    string? Discription { get; }
    int AfterDealingDamageCount { get; }
    int BeforeDamageReductionCount { get; }
    int DamageReductionCount { get; }
    int BeforeTakeDamageCount { get; }
    int AfterTakeDamageCount { get; }
  }

  public abstract class DQQProfile<T> : IDQQProfile where T : Enum
  {
    public abstract T ProfileNumber { get; }
    public abstract string? Name { get; }
    public abstract string? Discription { get; }

    public virtual int AfterDealingDamageCount => 15;

    public int BeforeDamageReductionCount => 0;

    public int DamageReductionCount => 0;

    public int BeforeTakeDamageCount => 0;

    public int AfterTakeDamageCount => 0;
  }
}
