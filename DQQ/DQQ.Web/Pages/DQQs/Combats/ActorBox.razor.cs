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
	public partial class ActorBox
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
						return "col-6 col-md-4";
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
					await DealDamage(Item?.Damage?.DamagePoint ?? "");
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


		public List<DamageParameter> DamageNumbers { get; set; } = new List<DamageParameter>();

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

		public Task DealDamage(string damage)
		{
			DamageNumbers?.Add(new DamageParameter
			{
				CreateDate = DateTime.Now,
				Id = Guid.NewGuid(),
				Number = damage
			});

			TakenDamage = true;
			StateHasChanged();

			Task.Run(async () =>
			{
				await Task.Delay(500);
				TakenDamage = false;
				StateHasChanged();
			});
			return Task.CompletedTask;
		}
		protected override async Task OnInitializedAsync()
		{
			await base.OnInitializedAsync();

		}

		public List<Guid> IdNeedRemove { get; set; } = new List<Guid>();
		public Task RemoveComponent(Guid id)
		{

			IdNeedRemove.Add(id);
			return Task.CompletedTask;

		}
	}

	public struct DamageParameter
	{
		public Guid Id { get; set; }
		public string Number { get; set; }
		public DateTime CreateDate { get; set; }
	}
}

