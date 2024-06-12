using DQQ.Commons;
using DQQ.Components.Items;
using DQQ.Components.Parameters;
using DQQ.Components.Skills;
using DQQ.Components.Stages.Maps;
using DQQ.Entities;
using DQQ.Enums;
using DQQ.Helper;
using DQQ.Profiles;
using DQQ.TickLogs;
using ReheeCmf.Helpers;
using ReheeCmf.Responses;
using System.Numerics;
using System.Text.Json.Serialization;

namespace DQQ.Components.Stages.Actors
{
	public class Actor : TargetBase, IActor
	{
		public Int64 BasicDamage { get; set; }

		public override EnumTargetLevel PowerLevel => EnumTargetLevel.NotSpecified;

		[JsonIgnore]
		public IEnumerable<SkillComponent>? Skills { get; set; }

		public override void Initialize(IDQQEntity entity, DQQComponent? parent)
		{
			base.Initialize(entity, parent);
			if (entity is ActorEntity ae)
			{
				TargetPriority = ae.TargetPriority;
				CombatPanel.StaticPanel.MaximunLife = ae.MaxHP ?? 0;
				CurrentHP = CombatPanel.StaticPanel.MaximunLife ?? 0;
				BasicDamage = ae.BasicDamage ?? 0;
				var list = new List<SkillComponent>();
				Skills = list;
				if (ae.Skills?.Any() == true)
				{
					var withTwoHand = ae.Equips?.Any(b => b.Item?.EquipProfile?.EquipType == EnumEquipType.TwoHandWeapon) == true;
					var mainHand = !withTwoHand && ae.Equips?.Any(b => b.EquipSlot == EnumEquipSlot.MainHand) == true;
					var offHand = !withTwoHand && ae.Equips?.Any(b => b.EquipSlot == EnumEquipSlot.OffHand) == true;

					var skills = ae.Skills
						.Where(b => b.SlotCheck(withTwoHand, mainHand, offHand))
						.Where(b => (b.SkillNumber ?? EnumSkillNumber.NotSpecified).AvaliablePlayerSkill(Level))
						.DistinctBy(b => b.Slot)
						.ToArray();
					foreach (var skill in skills)
					{
						var s = skill.GenerateComponent(this);
						list.Add(s);
					}
				}
			}
		}

		public override async Task<ContentResponse<bool>> OnTick(ComponentTickParameter parameter)
		{
			var result = await base.OnTick(parameter);
			if (!result.Success)
			{
				return result;
			}
			if (Skills != null)
			{
				foreach (var skill in Skills.Where(b => b != null))
				{
					var skillResult = await skill!.OnTick(parameter);
				}
			}
			return result;
		}

		public void ResetWave()
		{
			if (Skills?.Any() == true)
			{
				foreach (var skill in Skills)
				{
					skill.WaveCount = 0;
				}
			}
		}
	}
}
