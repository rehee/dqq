using DQQ.Attributes;
using DQQ.Commons;
using DQQ.Components.Stages;
using DQQ.Components.Stages.Maps;
using DQQ.Enums;
using DQQ.Helper;
using ReheeCmf.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DQQ.Profiles.Skills.Buffs
{
  [Pooled]
  public class HealProfile : AbHealing
  {
    public override bool NoPlayerSkill => false;
    protected override bool SelfTarget => true;
    public override decimal CastTime => 0m;

    public override decimal CoolDown => 30m;

    public override decimal DamageRate => 0m;

    public override bool CastWithWeaponSpeed => false;

    public override EnumSkill ProfileNumber => EnumSkill.Healing;

    public override string? Name => "快速治疗";

    public override string? Discription => "快速治疗自身. 回复最大生命60%的生命";
    public override DamageDeal[] CalculateDamage(ITarget? caster, IMap? map)
    {
      return [];
    }

    protected override void DealingDamage(ITarget? caster, ITarget? skillTarget, DamageDeal[] damageDeals, IMap? map)
    {

    }
    protected override HealingDeal[] CalculateHealing(ITarget? caster, IMap? map)
    {
      return [HealingDeal.New((long)((caster?.CombatPanel?.DynamicPanel?.MaximunLife).DefaultValue() * 0.6m))];
    }

  }
}
