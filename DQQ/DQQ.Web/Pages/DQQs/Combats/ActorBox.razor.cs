using BootstrapBlazor.Components;
using DQQ.Components;
using DQQ.Components.Items;
using DQQ.Components.Stages.Actors;
using DQQ.Enums;
using DQQ.Profiles.Skills.Buffs;
using DQQ.TickLogs;
using DQQ.Web.Pages.DQQs.Combats.Parts;
using DQQ.Web.Services.RenderServices;
using Microsoft.AspNetCore.Components;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using static System.Net.Mime.MediaTypeNames;

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
					case Enums.EnumTargetLevel.Guardian:
						return "col-md-6";
					case Enums.EnumTargetLevel.Champion:
					case Enums.EnumTargetLevel.Elite:
					case Enums.EnumTargetLevel.Magic:
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
			if (Item != null)
			{
				if (LastDamageId == null)
				{
					LastDamageId = Item.Id;
				}
				else
				{
					if (LastDamageId != Item.Id)
					{
						LastDamageId= Item.Id;
						var textPass = TextFlootCheck.New(Item, Actor?.Id);
						DealDamage(textPass);
					}
				}
				
			}

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
			if (check == null)
			{
				return Task.CompletedTask;
			}
			if (Count > 500)
			{
				Count = 1;
				DealDamage(check);
				return Task.CompletedTask;
			}
			var index = Count / 100;

			DamageNumbers[index].Add(check!.GetParameter());
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

			if (check.Damage != null)
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
				TakeSlash = true;
				StateHasChanged();
				Task.Run(async () =>
				{
					await Task.Delay(500);
					TakeSlash = false;
					StateHasChanged();
				});
			}
			if (check.Skill != null)
			{
				CastSkill(check?.Skill);
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
			switch (item.LogType)
			{
				case EnumLogType.DamageTaken:
					if (item.Target?.Id == actorId)
					{
						return new TextFlootCheck
						{
							Damage = item.Damage,
						};
					}
					break;
				case EnumLogType.HealingTaken:
					if (item.Target?.Id == actorId)
					{
						return new TextFlootCheck
						{
							Healing = item.Healing,
						};
					}
					break;
				case EnumLogType.CastSkill:
					if (item.From?.Id == actorId)
					{
						return new TextFlootCheck
						{
							Skill = item.Skill,
						};
					}
					break;
			}
			
			return null;

		}
		public TickLogSkill? Skill { get; set; }
		public TicklogDamage? Damage { get; set; }
		public TickLogHealing? Healing { get; set; }

		public TextFloatParameter GetParameter()
		{
			return new TextFloatParameter
			{
				Id = Guid.NewGuid(),
				CreateDate = DateTime.Now,
				Color = Skill != null ? Color.Warning : Healing != null ? Color.Success : Color.Danger,
				Text = Skill != null ? Skill?.SkillName : Healing != null ? $"{Healing?.HealingDone}" : Damage?.DisplayDamage ,
			};
		}
	}
	public struct TextFloatParameter
	{
		public Guid Id { get; set; }
		public string? Text { get; set; }
		public DateTime CreateDate { get; set; }
		public Color Color {  get; set; }
	}
}

