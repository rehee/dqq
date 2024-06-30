using DQQ.Commons.DTOs;
using DQQ.Components.Parameters;
using DQQ.Components.Stages;
using DQQ.Components.Stages.Actors;
using DQQ.Components.Stages.Actors.Characters;
using DQQ.Consts;
using DQQ.Entities;
using DQQ.Enums;
using DQQ.Helper;
using DQQ.Pools;
using DQQ.Profiles;
using DQQ.Profiles.Mobs;
using DQQ.Profiles.Skills;
using DQQ.Strategies;
using DQQ.Strategies.SkillStrategies;
using ReheeCmf.Commons.Jsons.Options;
using ReheeCmf.Helpers;
using ReheeCmf.Responses;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace DQQ.Components.Skills
{
	public class SkillComponent : DQQComponent
	{
		public static SkillComponent NewSupportSkill(SkillComponent? parent, EnumSkillNumber skill, int level = 0)
		{
			var result = SkillComponent.New(skill);
			result.SetParent(parent, level);
			parent?.AdditionalSkillTags(result?.SkillProfile);
			return result!;
		}
		public static SkillComponent NewTriggerSkill(SkillComponent? parent, EnumSkillNumber skill)
		{
			var result = SkillComponent.New(skill);
			result.SetParent(parent);
			return result!;
		}
		public static SkillComponent NewSubActionSkill(SkillComponent? parent, EnumSkillNumber skill, IEnumerable<SkillProfile>? profiles)
		{
			var result = SkillComponent.New(skill);
			result.SetParent(parent);
			if (profiles?.Any() != true)
			{
				return result;
			}
			foreach (var profile in profiles.Select(b => (b.IsSupportAvaliable(result.SkillTags), b)).OrderByDescending(b => b.Item1).Select(b => b.b))
			{
				if (profile.IsSupportAvaliable(result.SkillTags) != true)
				{
					continue;
				}
				result.AddSupportSkillComponent(NewSupportSkill(result, profile.SkillNumber, 1));

			}

			return result!;
		}
		public static SkillComponent New(EnumSkillNumber skill, EnumSkillSlot skillSlot = EnumSkillSlot.NotSpecified)
		{
			var skillComponent = new SkillComponent();
			var skillProfile = DQQPool.TryGet<SkillProfile, EnumSkillNumber?>(skill);
			skillComponent.InitSkillProfile(skillProfile, skillSlot);
			return skillComponent;
		}
		public SkillStrategy[]? SkillStrategies { get; set; }
		public static SkillComponent New(MobSkill skill, EnumSkillSlot skillSlot = EnumSkillSlot.NotSpecified)
		{
			var skillComponent = new SkillComponent();
			var skillProfile = DQQPool.TryGet<SkillProfile, EnumSkillNumber?>(skill.SkillNumber);
			skillComponent.InitSkillProfile(skillProfile, skillSlot);
			skillComponent.SkillStrategies = skill.Strategies;
			return skillComponent;
		}
		public EnumSkillSlot Slot { get; set; }
		public decimal CastTime { get; set; }
		public decimal Cooldown { get; set; }
		public bool CastWithWeaponSpeed => SkillProfile?.CastWithWeaponSpeed ?? false;
		public decimal DamageRate { get; set; }
		[JsonIgnore]
		public ITarget? SkillTarget { get; set; }
		public bool IsCasting { get; protected set; }
		public int MaxSupportSkill => SkillSlotHelper.MaxSkillNumber(Slot);

		public int CastTick => Convert.ToInt32(CastTime * DQQGeneral.TickPerSecond);
		public int CDTick => Convert.ToInt32(Cooldown * DQQGeneral.TickPerSecond);
		public int CastTickCount { get; set; }
		public int CDTickCount { get; set; }

		public int CastWithWeaponSpeedTick(IActor? from)
		{
			if (SkillProfile?.CastWithWeaponSpeed == true)
			{
				return WeaponSpeedTick(from);
			}
			return CastTick;
		}

		public int WeaponSpeedTick(IActor? from)
		{
			var attackPersecond = from?.CombatPanel?.DynamicPanel?.AttackPerSecond ?? 1;
			var attackTime = 1 / attackPersecond;
			var tick = attackTime * DQQGeneral.TickPerSecond;
			return (int)Math.Round(tick, 0);
		}


		[JsonIgnore]
		public SkillProfile? SkillProfile => DQQPool.TryGet<SkillProfile, EnumSkillNumber>(SkillNumber ?? EnumSkillNumber.NotSpecified);
		[JsonIgnore]
		public override DQQProfile? Profile => SkillProfile;
		public SkillDTO[]? SupportSkills { get; set; }


		public List<SkillComponent>? SupportSkillComponent { get; set; }
		public List<SkillComponent>? TriggerSkillComponent { get; set; }
		public List<SkillComponent>? SubActionSkillComponent { get; set; }

		public int TotalCount { get; set; } = 0;
		public int WaveCount { get; set; } = 0;

		public void AddSupportSkillComponent(SkillComponent? skillComponent)
		{
			if (skillComponent == null)
			{
				return;
			}
			if (SupportSkillComponent?.Any() != true)
			{
				SupportSkillComponent = new List<SkillComponent>();
			}
			SupportSkillComponent.Add(skillComponent);
		}
		
		protected HashSet<EnumSkillTag> skillTags { get; set; } = new HashSet<EnumSkillTag>();
		public IEnumerable<EnumSkillTag> SkillTags => skillTags.Distinct();

		public void InitializeSkillTags()
		{
			foreach (var t in SkillProfile?.OriginalTag ?? [])
			{
				skillTags.Add(t);
			}
		}
		public void AdditionalSkillTags(SkillProfile? profile)
		{
			if (profile?.BindingType != EnumSkillBindingType.Support || profile?.ExtureTagAdded?.Any() != true)
			{
				return;
			}
			foreach (var tag in profile.ExtureTagAdded)
			{
				skillTags.Add(tag);
			}
		}

		public EnumSkillNumber? SkillNumber { get; set; }

		public override void Initialize(IDQQEntity entity, DQQComponent? parent)
		{
			
			if (entity is SkillEntity sp)
			{
				try
				{
					
					SkillNumber = sp.SkillNumber;
					base.Initialize(entity, parent);
					var skillProfile = DQQPool.TryGet<SkillProfile, EnumSkillNumber?>(sp.SkillNumber);
					InitSkillProfile(skillProfile, sp.Slot);
					SkillStrategies = String.IsNullOrEmpty(sp.SkillStrategy) ? null :
					JsonSerializer.Deserialize<SkillStrategy[]?>(sp.SkillStrategy ?? "", JsonOption.DefaultOption);
					var supportSkillNumers = JsonSerializer.Deserialize<EnumSkillNumber[]?>(sp.SupportSkills ?? "", JsonOption.DefaultOption);
					int? level = null;
					Character? character  = null;
					if (Parent is Character chh)
					{
						character = chh;
						level = character.Level;
					}
					var array = supportSkillNumers?.Where(b => b.IsPlayerAvaliableSkill(character)).Distinct().Take(MaxSupportSkill).ToArray();
					var skillList = new List<SkillDTO>();
					for(var i =0;i< MaxSupportSkill; i++)
					{
						var skills = array?.Where((b, index) => index == i).FirstOrDefault();
						skillList.Add(SkillDTO.New(skills ?? EnumSkillNumber.NotSpecified));

					}
					SupportSkills = skillList.ToArray();

					SupportSkillComponent = new List<SkillComponent>();

					var supportedSkills = SupportSkills?.Where(b => b.Profile?.BindingType == EnumSkillBindingType.Support)
						.Select(b => (b.Profile?.IsSupportAvaliable(SkillTags) == true, b)).OrderByDescending(b => b.Item1).Select(b => b.b).ToArray();

					foreach (var dto in supportedSkills ?? [])
					{
						if (dto.Profile?.IsSupportAvaliable(SkillTags) != true)
						{
							continue;
						}
						var skillComponent = SkillComponent.NewSupportSkill(this, dto.SkillNumber);
						SupportSkillComponent.Add(skillComponent);
					}
					TriggerSkillComponent = new List<SkillComponent>();
					var triggerActive = SupportSkills?.Where(b => b.Profile?.BindingType == EnumSkillBindingType.Trigger).ToArray();
					foreach (var trigger in triggerActive ?? [])
					{
						var skillComponent = SkillComponent.NewTriggerSkill(this, trigger.SkillNumber);
						TriggerSkillComponent?.Add(skillComponent);
					}
					var subActive = SupportSkills?.Where(b => b.Profile?.BindingType == EnumSkillBindingType.Active).ToArray();
					SubActionSkillComponent = new List<SkillComponent>();
					foreach (var sub in subActive ?? [])
					{
						var subAction = NewSubActionSkill(this, sub.SkillNumber, supportedSkills?.Select(b => b.Profile!));
						SubActionSkillComponent.Add(subAction);
					}


				}
				catch
				{

				}

			}
		}

		public void InitSkillProfile(SkillProfile? profile, EnumSkillSlot skillSlot = EnumSkillSlot.NotSpecified)
		{

			SkillNumber = profile?.SkillNumber;
			CastTime = profile?.CastTime ?? 0;
			Cooldown = profile?.CoolDown ?? 0;
			DamageRate = profile?.DamageRate ?? 0;
			DisplayName = profile?.Name;
			Slot = skillSlot;
			InitializeSkillTags();
		}

		public int GetCastWithWeaponSpeedTick(ITarget? caster)
		{
			var profile = this.SkillProfile;
			if (CastWithWeaponSpeed != true || caster?.CombatPanel?.DynamicPanel.AttackPerSecond == null || caster?.CombatPanel?.DynamicPanel.AttackPerSecond == 0)
			{
				return CastTick;
			}

			return (int)((1 / caster?.CombatPanel?.DynamicPanel.AttackPerSecond!) * DQQGeneral.TickPerSecond);

		}

		protected void StartCasting(ComponentTickParameter? parameter, ITarget? target = null,bool isCasting=false)
		{
			SkillTarget = target;
			IsCasting = isCasting;

		}

		protected async Task<ContentResponse<bool>> CastingSkill(ComponentTickParameter parameter)
		{
			ContentResponse<bool> result;
			if (SkillProfile != null)
			{

				parameter.SetSupportSkill(this, SupportSkillComponent, TriggerSkillComponent, SubActionSkillComponent);
				result = await SkillProfile.CastSkill(parameter);
			}
			else
			{
				result = new ContentResponse<bool>();
				result.SetSuccess(true);

			}
			TotalCount++;
			WaveCount++;
			CastTickCount = 0;
			if (CastWithWeaponSpeedTick(parameter?.From as IActor) <= 0 && CDTick <= 0)
			{
				CDTickCount = (int)(DQQGeneral.MinCooldown * DQQGeneral.TickPerSecond);
			}
			else
			{
				CDTickCount = CDTick;
			}
			castingThisTick = true;
			return result;
		}
		private bool castingThisTick { get; set; }
		public override async Task<ContentResponse<bool>> OnTick(ComponentTickParameter parameter)
		{
			var result = await base.OnTick(parameter);
			
			if (CDTickCount > 0)
			{
				result.SetError();
				CDTickCount--;
				goto FinalSteps;
			}
			if (SkillProfile?.BindingType != EnumSkillBindingType.Active)
			{
				return result;
			}
			if (Slot == EnumSkillSlot.NotSpecified)
			{
				return result;
			}
			if (!result.Success)
			{
				return result;
			}
			result.SetError();
			if (Parent != null && Parent is not Character)
			{
				goto FinalSteps;
			}
			
			if (!IsCasting)
			{
				
				var matchCondition = StrategeCheckResult.New(false, null);
				if (SkillStrategies?.Any() == true)
				{
					matchCondition = StrategyHelper.MatchSkillStrategy(SkillStrategies, parameter, this);
				}
				else
				{
					matchCondition = StrategeCheckResult.New(true, null);
				}

				if (!matchCondition.Matched)
				{
					goto FinalSteps;
				}
				
				StartCasting(parameter, matchCondition.MatchedTarget,true);
			}



			if (CastTickCount < CastWithWeaponSpeedTick(parameter?.From as IActor))
			{
				result.SetError();
				
				CastTickCount++;
				goto FinalSteps;
			}
			result = await CastingSkill(ComponentTickParameter.New(parameter, SkillTarget));
			StartCasting(null);





		FinalSteps:
			if (SupportSkillComponent?.Any() == true)
			{
				foreach (var s in SupportSkillComponent)
				{
					await s.OnTick(parameter);
				}
			}
			if (TriggerSkillComponent?.Any() == true)
			{
				foreach (var s in TriggerSkillComponent)
				{
					await s.OnTick(parameter);
				}
			}
			if (SubActionSkillComponent?.Any() == true)
			{
				foreach (var s in SubActionSkillComponent)
				{
					await s.OnTick(parameter);
				}
			}
			
			
			return result;
		}
		public virtual SkillEntity ToSkillEntity(Guid? actorId = null)
		{
			var result = new SkillEntity();
			result.Id = DisplayId ?? Guid.NewGuid();
			result.Name = DisplayName;
			result.SkillNumber = SkillProfile?.SkillNumber;
			result.ActorId = actorId;
			return result;
		}
		public bool AvaliableForUser { get; set; } = true;

		public bool CheckAvaliableForCharacter(Character? actor)
		{
			if (Slot == EnumSkillSlot.NotSpecified || actor == null)
			{
				return false;
			}
			var slotAvaliable = true;
			switch (Slot)
			{
				case EnumSkillSlot.WeaponSlotTH: slotAvaliable = actor?.WithTwoHandWeapon == true; break;
				case EnumSkillSlot.WeaponSlot1: slotAvaliable = actor?.WithWeapon1 == true; break;
				case EnumSkillSlot.WeaponSlot2: slotAvaliable = actor?.WithWeapon2 == true; break;
			}
			return slotAvaliable;
		}
		public ContentResponse<bool> CheckAndSetAvaliableForUser(Character? actor)
		{
			var result = new ContentResponse<bool>();
			AvaliableForUser = CheckAvaliableForCharacter(actor);
			result.SetSuccess(AvaliableForUser);
			return result;
		}
	}
}
