﻿using DQQ.Combats;
using DQQ.Commons;
using DQQ.Commons.DTOs;
using DQQ.Components.Items.Equips;
using DQQ.Components.Parameters;
using DQQ.Components.Skills;
using DQQ.Components.Stages.Maps;
using DQQ.Entities;
using DQQ.Enums;
using DQQ.Helper;
using DQQ.Profiles.Skills;
using DQQ.TickLogs;
using ReheeCmf.Responses;
using System.Collections.Concurrent;
using System.ComponentModel.DataAnnotations.Schema;
using System.Numerics;
using System.Text.Json.Serialization;

namespace DQQ.Components.Stages.Actors.Characters
{
  public class Character : Actor, IEquippableCharacter
  {
    public Character()
    {
      Equips = new ConcurrentDictionary<EnumEquipSlot, IEquptment?>();
    }
    public override EnumTargetLevel PowerLevel => EnumTargetLevel.Elite;
    public string? OwnerId { get; set; }
    public BigInteger CurrentXP { get; set; }
    public BigInteger NextLevelXP { get; set; }
    [NotMapped]
    [JsonIgnore]
    public ConcurrentDictionary<EnumEquipSlot, IEquptment?> Equips { get; set; }

    public Dictionary<int, SkillDTO>? SkillMap { get; set; }



    public ActorEntity ToActorEntity()
    {
      var entity = new ActorEntity();


      return entity;
    }
    public override async Task<ContentResponse<bool>> OnTick(ComponentTickParameter parameter)
    {
      var result = await base.OnTick(parameter);
      if (!result.Success)
      {
        return result;
      }
      var equips = Equips?.Select(b => b.Value).Where(b => b != null).Select(b => b!).ToArray();
      if (equips?.Any() == true)
      {
        foreach (var e in equips)
        {
          await e.OnTick(parameter);
        }
      }
      return result;
    }
    public override void Initialize(IDQQEntity entity)
    {
      base.Initialize(entity);

      SkillMap = Skills?.DistinctBy(b => b.Slot).OrderBy(b => b.Slot).ToDictionary(b => b.Slot, b => new SkillDTO
      {
        SkillName = b.SkillProfile?.SkillName,
        SkillNumber = b.SkillProfile?.SkillNumber ?? EnumSkill.NormalAttack,
        SkillStrategies = b.SkillStrategies?.OrderBy(b => b.Property).ToList() ?? new List<Strategies.SkillStrategies.SkillStrategy>()
      }) ?? new Dictionary<int, SkillDTO>();

      if (entity is ActorEntity ae)
      {
        var equips = ae.Equips!.DistinctBy(b => b.EquipSlot).ToArray();

        foreach (var equip in equips)
        {
          if (equip?.Item == null)
          {
            continue;
          }
          var equipComponent = equip.Item.GenerateTypedComponent<EquipComponent>();
          if (equipComponent == null)
          {
            continue;
          }
          Equips.AddOrUpdate(equip.EquipSlot!.Value, equipComponent!, (a, b) => equipComponent);
        }
      }
      this.TotalEquipProperty();
    }


    protected override void SelfBeforeDamageReduction(BeforeDamageTakenParameter parameter)
    {
      var equips = Equips?.Select(b => b.Value).Where(b => b != null).Select(b => b!).ToArray();
      if (equips?.Any() == true)
      {
        foreach (var e in equips)
        {
          e.BeforeDamageReduction(parameter);
        }
      }
      base.SelfBeforeDamageReduction(parameter);
    }
    protected override void SelfDamageReduction(BeforeDamageTakenParameter parameter)
    {
      
      var equips = Equips?.Select(b => b.Value).Where(b => b != null).Select(b => b!).ToArray();
      if (equips?.Any() == true)
      {
        foreach (var e in equips)
        {
          e.DamageReduction(parameter);
        }
      }
      base.SelfDamageReduction(parameter);
    }


    protected override void SelfBeforeTakeDamage(DamageTakenParameter parameter)
    {
      var equips = Equips?.Select(b => b.Value).Where(b => b != null).Select(b => b!).ToArray();
      if (equips?.Any() == true)
      {
        foreach (var e in equips)
        {
          e.BeforeTakeDamage(parameter);
        }
      }
      base.SelfBeforeTakeDamage(parameter);
    }
    protected override void SelfAfterTakeDamage(DamageTakenParameter parameter)
    {
      var equips = Equips?.Select(b => b.Value).Where(b => b != null).Select(b => b!).ToArray();
      if (equips?.Any() == true)
      {
        foreach (var e in equips)
        {
          e.AfterTakeDamage(parameter);
        }
      }
      base.SelfAfterTakeDamage(parameter);
    }

  }
}
