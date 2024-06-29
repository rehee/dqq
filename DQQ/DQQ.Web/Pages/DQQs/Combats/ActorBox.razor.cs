using BootstrapBlazor.Components;
using DQQ.Enums;
using DQQ.TickLogs;
using DQQ.Web.Services.RenderServices;
using Microsoft.AspNetCore.Components;
using System.Diagnostics.CodeAnalysis;

namespace DQQ.Web.Pages.DQQs.Combats
{
	public class ActorBoxPage : DQQPageBase
	{
		[Parameter]
		[NotNull]
		public TickLogActor? Actor { get; set; }

		[Parameter]
		public TickLogItem? Item { get; set; }

		public bool IsMob => Actor?.MobNumber != null;
		public bool TakenDamage { get; set; }
		public bool TakeSlash { get; set; }
		public bool Attacking { get; set; }
		public string AttackClass => Attacking ? IsMob ? "attack_enemy" : "attack_player" : "";

		public string ShackClass => TakenDamage ? "take_damage" : "";
		public string CardClass => @$"{(IsMob ? "mob_box" : "")}  {AttackClass}";

		public string SlashCss => $"{(TakeSlash ? "animated-sprite" : "")} sword-animation";

		public Color ThisColor => IsMob ? Actor.GetTargetPowerLevelColor() : Color.None;

		public string ColumnNumber
		{
			get
			{
				if (!IsMob)
				{
					return "col-md-6";
				}
				switch (Actor.PowerLevel)
				{
					case EnumTargetLevel.Guardian:
						return "col-md-6";
					case EnumTargetLevel.Champion:
					case EnumTargetLevel.Elite:
					case EnumTargetLevel.Magic:
						return "col-6 col-md-3 col-lg-4";
					default:
						return "col-6 col-md-3 col-lg-2";
				}

			}
		}

		protected Guid? LastDamageId { get; set; } = null;
		protected override async Task OnParametersSetAsync()
		{
			await base.OnParametersSetAsync();
			DealDamageChange();
		}

		public Task DealDamageChange()
		{
			if (Item != null)
			{

				if (LastDamageId == null || LastDamageId != Item.Id)
				{
					LastDamageId = Item.Id;
					var textPass = TextFlootCheck.New(Item, Actor?.Id);
					DealDamage(textPass);
					var AvaliableParameter = GetFLoatParameter(textPass);
					foreach(var a in AvaliableParameter)
					{
						AddingFlotText(a);
					}
					
				}
			}
			return Task.CompletedTask;
		}

		public TextFloatParameter[] GetFLoatParameter(TextFlootCheck? check)
		{
			if (check == null)
			{
				return [];
			}
			var getTextNumber = check?.GetParameter();
			var getAnimation = check?.GetAnimation();
			return (new TextFloatParameter?[] { getTextNumber, getAnimation }).Where(b=>b != null).Select(b=>b!.Value).ToArray();
		}



		[Inject]
		[NotNull]
		public IRenderService? RenderService { get; set; }


		//public List<DamageParameter> DamageNumbers { get; set; } = new List<DamageParameter>();
		public Dictionary<int, List<TextFloatParameter>> DamageNumbers { get; set; } = new Dictionary<int, List<TextFloatParameter>>
		{
			[0] = new List<TextFloatParameter>(),
			[1] = new List<TextFloatParameter>(),
			[2] = new List<TextFloatParameter>(),
			[3] = new List<TextFloatParameter>(),
			[4] = new List<TextFloatParameter>(),
		};
		public int Count = 0;

		public Task CastSkill(TickLogSkill? skillNumber)
		{
			Attacking = true;
			StateHasChanged();
			Task.Run(async () =>
			{
				await Task.Delay(500);
				Attacking = false;
				StateHasChanged();
			});
			return Task.CompletedTask;
		}
		public int totalDamage {get;set;}
		public Task DealDamage(TextFlootCheck? check)
		{
			
			
			if (check?.Damage != null)
			{
				
				if(check?.Damage?.HitCheck== EnumHitCheck.Hit)
				{
					TakenDamage = true;
					StateHasChanged();
					Task.Run(async () =>
					{
						await Task.Delay(500);
						TakenDamage = false;
						StateHasChanged();
					});
				}
			}
			if (check?.Skill != null)
			{
				CastSkill(check?.Skill);
			}
			
			return Task.CompletedTask;
		}
		public Task AddingFlotText(TextFloatParameter? parameter)
		{
			if (parameter == null)
			{
				return Task.CompletedTask;
			}
			if (Count > 500)
			{
				Count = 1;
				AddingFlotText(parameter);
				return Task.CompletedTask;
			}
			var index = Count / 100;
			DamageNumbers[index].Add(parameter!.Value);
			if (Count % 100 == 0)
			{
				switch (index)
				{
					case 2:
						DamageNumbers[0].Clear();
						break;
					case 3:
						DamageNumbers[1].Clear();
						break;
					case 4:
						DamageNumbers[2].Clear();
						break;
					case 0:
						DamageNumbers[3].Clear();
						break;
					case 1:
						DamageNumbers[4].Clear();
						break;
				}
			}
			return Task.CompletedTask;
		}
		
		protected override async Task OnInitializedAsync()
		{
			await base.OnInitializedAsync();
			
		}

		public List<Guid> IdNeedRemove { get; set; } = new List<Guid>();
		public Task RemoveComponent(Guid id)
		{

			Count++;
			return Task.CompletedTask;

		}

		protected override async Task OnDisposeAsync()
		{
			await base.OnDisposeAsync();
			foreach(var damage in DamageNumbers)
			{
				damage.Value.Clear();
			};
			DamageNumbers.Clear();
		}
	}
	public class TextFlootCheck
	{
		public static TextFlootCheck? New(TickLogItem? item, Guid? actorId)
		{
			if (item == null || actorId == null) return null;
			var result = new TextFlootCheck();
			result.SkillNumber = item.Skill?.SkillNumber;
			result.DurationNumber = item.Skill?.DurationNumber;
			switch (item.LogType)
			{
				case EnumLogType.DamageTaken:
					
					if (item.Target?.Id == actorId)
					{
						result.Damage= item.Damage;
						return result;
					}
					break;
				case EnumLogType.HealingTaken:
					if (item.Target?.Id == actorId)
					{
						result.Healing = item.Healing;
						return result;
					}
					break;
				case EnumLogType.CastSkill:
					if (item.From?.Id == actorId)
					{
						result.Skill = item.Skill;
						return result;
					}
					break;
			}
			
			return null;

		}
		public TickLogSkill? Skill { get; set; }
		public TicklogDamage? Damage { get; set; }
		public TickLogHealing? Healing { get; set; }
		public EnumSkillNumber? SkillNumber { get; set; }
		public EnumDurationNumber? DurationNumber { get; set; }

		public TextFloatParameter GetParameter()
		{
			return new TextFloatParameter
			{
				Id = Guid.NewGuid(),
				CreateDate = DateTime.Now,
				Color = Skill != null ? Color.Warning : Healing != null ? Color.Success : Color.Danger,
				Text = Skill != null ? Skill?.SkillName : Healing != null ? $"{Healing?.HealingDone} {(Healing?.Absorbe>0?$"Absorbe({Healing?.Absorbe})":"")} {(Healing?.OverHeal>0 && DurationNumber==null ? $"Over Heal({Healing?.OverHeal})":"")}" : Damage?.DisplayDamage ,
			};
		}
		public TextFloatParameter? GetAnimation()
		{
			var result = new TextFloatParameter
			{
				Id = Guid.NewGuid(),
				CreateDate = DateTime.Now,
			};

			if (Damage != null)
			{
				if (DurationNumber != null)
				{
					return null;
				}
				result.AnimationType = EnumAnimationType.Slash;
				if (SkillNumber == EnumSkillNumber.MightySmash)
				{
					result.AnimationType = EnumAnimationType.Slash2;
				}

				
				result.Color = Color.Warning;
				return result;
			}
			return null;
		}
	}
	public struct TextFloatParameter
	{
		public Guid Id { get; set; }
		public string? Text { get; set; }
		public DateTime CreateDate { get; set; }
		public Color Color {  get; set; }
		public EnumAnimationType AnimationType { get; set; }
	}
}

