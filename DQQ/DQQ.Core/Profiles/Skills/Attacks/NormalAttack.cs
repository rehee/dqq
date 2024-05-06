using DQQ.Attributes;
using DQQ.Components.Stages;
using DQQ.Components.Stages.Actors;
using DQQ.Components.Stages.Maps;
using DQQ.Enums;
using DQQ.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace DQQ.Profiles.Skills.Attacks
{
  [Pooled]
  public class NormalAttack : SkillProfile
  {


    public override decimal CastTime => 1.2m;
    public override decimal CoolDown => 0m;
    public override decimal DamageRate => 1m;
    public override string? Discription => "";
    public override bool CastWithWeaponSpeed => true;

    public override EnumSkill ProfileNumber => EnumSkill.NormalAttack;

    public override string? Name => "Normal Attack";


  }
}