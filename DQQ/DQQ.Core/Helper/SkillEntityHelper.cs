using DQQ.Combats;
using DQQ.Commons.DTOs;
using DQQ.Components;
using DQQ.Components.Skills;
using DQQ.Components.Stages.Actors;
using DQQ.Components.Stages.Actors.Characters;
using DQQ.Consts;
using DQQ.Entities;
using DQQ.Enums;
using ReheeCmf.Commons.Jsons.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace DQQ.Helper
{
	public static class SkillEntityHelper
	{
		public static SkillEntity? ToSkillEntity(this PickSkillDTO? dto, Character? actor)
		{
			if (dto == null || actor?.DisplayId == null || dto?.Slot == null || dto?.Slot == EnumSkillSlot.NotSpecified)
			{
				return null;
			}
			var array = dto?.Strategies?.OrderBy(b => b.Priority).ToArray();
			var str = JsonSerializer.Serialize(array, JsonOption.DefaultOption);
			var validSkillNumbers = dto?.SupportSkill?.Select(b => SkillDTO.New(b))
				.Where(b => b.Profile?.IsPlayerAvaliableSkill(actor) == true)
				.Select(b => b.SkillNumber).Distinct().Take((dto.Slot).MaxSkillNumber()).ToArray();
			var supportSkills = JsonSerializer.Serialize(validSkillNumbers, JsonOption.DefaultOption);

			var skillEntity = new SkillEntity()
			{
				ActorId = actor?.DisplayId,
				Slot = dto?.Slot ?? EnumSkillSlot.MainSlot,
				SkillNumber = dto?.SkillNumber,
				SkillStrategy = str,
				SupportSkills = supportSkills,
			};
			return skillEntity;
		}

		public static List<SkillComponent> ToSkillComponents(this IEnumerable<SkillEntity>? entities, DQQComponent? actor, bool? with2H = null, bool? withH1 = null, bool? withH2 = null)
		{
			var result = new List<SkillComponent>();
			if (entities?.Any()!=true)
			{
				return result;
			}
			var skills = entities.Select(b=> b.ToSkillComponent(actor, with2H, withH1, withH2))
						.Where(b=>b!=null)
						.DistinctBy(b => b.Slot)
						.Select(b=>b!)
						.ToList();
			
			return skills ?? result;
		}
		public static SkillComponent? ToSkillComponent(this SkillEntity entity, DQQComponent? actor, bool? with2H = null, bool? withH1 = null, bool? withH2 = null)
		{
			if (entity == null)
			{
				return null;
			}
			if(entity?.SlotCheck(with2H, withH1, withH2)!=true)
			{
				return null;
			}
			if(entity?.Profile?.IsPlayerAvaliableSkill(onlyCheckPlayer: true) != true)
			{
				return null;
			}
			return entity?.GenerateComponent(actor);
		}
	
		public static Dictionary<EnumSkillSlot, SkillDTO> ToSkillDictionary(
			this IEnumerable<SkillComponent>? components,Character? character
			)
		{
			foreach (var skil in components ?? [])
			{
				skil.CheckAndSetAvaliableForUser(character);
			}
			var skills = components?.ToDictionary(b => b.Slot, b => SkillDTO.New(b)) ?? [];
			var result = new Dictionary<EnumSkillSlot, SkillDTO>();
			foreach (EnumSkillSlot slot in Enum.GetValues(typeof(EnumSkillSlot)))
			{
				skills.TryGetValue(slot, out var skill);
				result.TryAdd(slot, skill ?? SkillDTO.New(EnumSkillNumber.NotSpecified, slot));
			}
			return result;
		}
	}
}
