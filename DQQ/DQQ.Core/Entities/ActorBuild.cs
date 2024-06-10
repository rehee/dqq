using DQQ.Commons.DTOs;
using DQQ.Components.Stages.Actors.Characters;
using DQQ.Enums;
using ReheeCmf.Commons.Jsons.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace DQQ.Entities
{
	public class ActorBuild : DQQEntityBase
	{
		public static ActorBuild New(Character? cha, BuildDTO? dto)
		{
			var result = new ActorBuild
			{
				ActorId = cha?.DisplayId,
				Name = dto?.BuildName,
				BuildDescribe = dto?.BuildDescription,
			};
			SetActorBuild(result, cha);
			return result;
		}

		public static void SetActorBuild(ActorBuild? build, Character? cha)
		{
			if (build == null)
			{
				return;
			}
			try
			{
				build.SkillAndStrategyJson = JsonSerializer.Serialize(cha?.SkillMap, JsonOption.DefaultOption);
			}
			catch
			{
				build.SkillAndStrategyJson = null;
			}
			build.HeadId = null;
			build.BodyId = null;
			build.GlovesId = null;
			build.BootsId = null;
			build.MainHandId = null;
			build.OffHandId = null;
			build.AmuletId = null;
			build.BeltId = null;
			build.LeftRingId = null;
			build.RightRingId = null;
			build.TargetPriority = cha?.TargetPriority;
			if (cha?.EquipItems != null)
			{
				foreach (var c in cha!.EquipItems)
				{
					switch (c.Key)
					{
						case EnumEquipSlot.Head: build.HeadId = c.Value?.Id; break;
						case EnumEquipSlot.Body: build.BodyId = c.Value?.Id; break;
						case EnumEquipSlot.Gloves: build.GlovesId = c.Value?.Id; break;
						case EnumEquipSlot.Boots: build.BootsId = c.Value?.Id; break;
						case EnumEquipSlot.MainHand: build.MainHandId = c.Value?.Id; break;
						case EnumEquipSlot.OffHand: build.OffHandId = c.Value?.Id; break;
						case EnumEquipSlot.Amulet: build.AmuletId = c.Value?.Id; break;
						case EnumEquipSlot.Belt: build.BeltId = c.Value?.Id; break;
						case EnumEquipSlot.LeftRing: build.LeftRingId = c.Value?.Id; break;
						case EnumEquipSlot.RightRing: build.RightRingId = c.Value?.Id; break;
					}
				}
			}

		}
		public Guid? ActorId { get; set; }
		public string? BuildDescribe { get; set; }

		public Guid? HeadId { get; set; }
		public Guid? BodyId { get; set; }
		public Guid? GlovesId { get; set; }
		public Guid? BootsId { get; set; }
		public Guid? MainHandId { get; set; }
		public Guid? OffHandId { get; set; }
		public Guid? AmuletId { get; set; }
		public Guid? BeltId { get; set; }
		public Guid? LeftRingId { get; set; }
		public Guid? RightRingId { get; set; }
		public EnumTargetPriority? TargetPriority { get; set; }
		public string? SkillAndStrategyJson { get; set; }

		public (EnumEquipSlot Slot, Guid? Id)[] Equips => [
			 (EnumEquipSlot.Head,HeadId),
			 (EnumEquipSlot.Body,BodyId),
			 (EnumEquipSlot.Gloves,GlovesId),
			 (EnumEquipSlot.Boots,BootsId),
			 (EnumEquipSlot.MainHand ,MainHandId),
			 (EnumEquipSlot.OffHand,OffHandId),
			 (EnumEquipSlot.Amulet,AmuletId),
			 (EnumEquipSlot.Belt,BeltId),
			 (EnumEquipSlot.LeftRing,LeftRingId),
			 (EnumEquipSlot.RightRing,RightRingId),
			];

		public Dictionary<EnumSkillSlot, SkillDTO>? SkillMap
		{
			get
			{
				try
				{
					if (String.IsNullOrEmpty(SkillAndStrategyJson))
					{
						return null;
					}
					return JsonSerializer.Deserialize<Dictionary<EnumSkillSlot, SkillDTO>>(
						SkillAndStrategyJson, JsonOption.DefaultOption);
				}
				catch
				{
					return null;
				}
			}
		}
	}
}
