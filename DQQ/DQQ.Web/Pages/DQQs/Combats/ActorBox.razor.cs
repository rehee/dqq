using BootstrapBlazor.Components;
using DQQ.Components;
using DQQ.Components.Stages.Actors;
using DQQ.Enums;
using DQQ.TickLogs;
using DQQ.Web.Pages.DQQs.Combats.Parts;
using DQQ.Web.Services.RenderServices;
using Microsoft.AspNetCore.Components;
using System.Collections.Concurrent;
using System.Collections.Generic;
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

		public bool Attacking { get; set; }
		public string AttackClass => Attacking ? IsMob ? "attack_enemy" : "attack_player" : "";

		public string ShackClass => TakenDamage ? "take_damage" : "";
		public string CardClass => @$"{(IsMob ? "mob_box" : "")}  {AttackClass}";

		public string SlashCss => $"{(TakenDamage ? "animated-sprite" : "")} sword-animation";

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

		protected override async Task OnParametersSetAsync()
		{
			await base.OnParametersSetAsync();
			if (Item != null)
			{
				if (Actor?.Id != null && Item.Damage != null && Item?.Target?.Id == Actor?.Id)
				{
					await DealDamage(Item?.Damage?.DisplayDamage ?? "");
				}
				if (Actor?.Id != null && Item.Healing != null && Item?.Target?.Id == Actor?.Id)
				{
					await DealDamage($"{Item?.Healing?.HealingDone}", true);
				}

				if (Actor?.Id != null && Item?.Skill != null && Item?.From?.Id == Actor?.Id)
				{
					await CastSkill(Item?.Skill);
				}
			}

		}



		[Inject]
		[NotNull]
		public IRenderService? RenderService { get; set; }


		//public List<DamageParameter> DamageNumbers { get; set; } = new List<DamageParameter>();
		public Dictionary<int, List<DamageParameter>> DamageNumbers { get; set; } = new Dictionary<int, List<DamageParameter>>
		{
			[0] = new List<DamageParameter>(),
			[1] = new List<DamageParameter>(),
			[2] = new List<DamageParameter>(),
			[3] = new List<DamageParameter>(),
			[4] = new List<DamageParameter>(),
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
		public Task DealDamage(string damage,bool isHealing=false)
		{
			if (Count <= 500)
			{
				var index = Count / 100;

				DamageNumbers[0].Add(new DamageParameter
				{
					CreateDate = DateTime.Now,
					Id = Guid.NewGuid(),
					Number = damage,
					IsHealing = isHealing
				});
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
				
			}
			else
			{
				Count = 1;
				DealDamage(damage, isHealing);
				return Task.CompletedTask;
			}
			

			if (isHealing != true)
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
	
	public struct DamageParameter
	{
		public Guid Id { get; set; }
		public string Number { get; set; }
		public DateTime CreateDate { get; set; }
		public bool IsHealing {  get; set; }
	}
}

