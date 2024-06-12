using DQQ.Components.Stages.Maps;
using DQQ.Components.Stages;
using DQQ.Entities;
using DQQ.Profiles;
using ReheeCmf.Responses;
using System.Text.Json.Serialization;
using ReheeCmf.Helpers;
using DQQ.Components.Parameters;
using DQQ.Profiles.Skills;

namespace DQQ.Components
{
	public abstract class DQQComponent : IDQQComponent
	{
		public virtual Guid? DisplayId { get; set; }
		public virtual string? DisplayName { get; set; }

		[JsonIgnore]
		public IDQQEntity? Entity { get; set; }
		[JsonIgnore]
		public DQQProfile? Profile { get; set; }

		[JsonIgnore]
		public DQQComponent? Parent { get; protected set; }

		public virtual void SetParent(DQQComponent? Parent, int level = 0)
		{
			this.Parent = Parent;
		}

		bool IsDisposed { get; set; }
		public virtual async ValueTask DisposeAsync()
		{
			await Task.CompletedTask;
			if (IsDisposed)
			{
				return;
			}
			IsDisposed = true;
			Entity = null;
			Profile = null;
		}

		public virtual void Initialize(IDQQEntity entity, DQQComponent? parent)
		{
			Entity = entity;
			DisplayId = entity.Id;
			DisplayName = entity.Name;
			SetParent(parent);
		}
		public virtual async Task<ContentResponse<bool>> OnTick(ComponentTickParameter parameter)
		{
			await Task.CompletedTask;
			var result = new ContentResponse<bool>();
			result.SetSuccess(parameter.From?.Alive == true);
			if (result.Success)
			{
				if (AfterDealingDamageCount > 0)
				{
					AfterDealingDamageCount--;
				}
				if (BeforeDamageReductionCount > 0)
				{
					BeforeDamageReductionCount--;
				}
				if (DamageReductionCount > 0)
				{
					DamageReductionCount--;
				}
				if (BeforeTakeDamageCount > 0)
				{
					BeforeTakeDamageCount--;
				}
				if (AfterTakeDamageCount > 0)
				{
					AfterTakeDamageCount--;
				}
			}
			return result;
		}
		protected int AfterDealingDamageCount { get; set; }
		protected int BeforeDamageReductionCount { get; set; }
		protected int DamageReductionCount { get; set; }
		protected int BeforeTakeDamageCount { get; set; }
		protected int AfterTakeDamageCount { get; set; }
		public void BeforeDamageReduction(BeforeDamageTakenParameter parameter)
		{
			if (BeforeDamageReductionCount > 0)
			{
				return;
			}
			BeforeDamageReductionCount = Profile?.BeforeDamageReductionCount ?? 0;
			SelfBeforeDamageReduction(parameter);
		}
		protected virtual void SelfBeforeDamageReduction(BeforeDamageTakenParameter parameter)
		{

		}
		public void DamageReduction(BeforeDamageTakenParameter parameter)
		{
			if (DamageReductionCount > 0)
			{
				return;
			}
			DamageReductionCount = Profile?.DamageReductionCount ?? 0;
			SelfDamageReduction(parameter);
		}
		protected virtual void SelfDamageReduction(BeforeDamageTakenParameter parameter)
		{

		}
		public void BeforeTakeDamage(DamageTakenParameter parameter)
		{
			if (BeforeTakeDamageCount > 0)
			{
				return;
			}
			BeforeTakeDamageCount = Profile?.BeforeTakeDamageCount ?? 0;
			SelfBeforeTakeDamage(parameter);
		}
		protected virtual void SelfBeforeTakeDamage(DamageTakenParameter parameter)
		{

		}
		public void AfterTakeDamage(DamageTakenParameter parameter)
		{
			if (AfterTakeDamageCount > 0)
			{
				return;
			}
			AfterTakeDamageCount = Profile?.AfterTakeDamageCount ?? 0;
			SelfAfterTakeDamage(parameter);
		}
		protected virtual void SelfAfterTakeDamage(DamageTakenParameter parameter)
		{

		}

		public virtual async Task<ContentResponse<bool>> AfterDealingDamage(AfterDealingDamageParameter parameter)
		{
			var result = new ContentResponse<bool>();
			if (AfterDealingDamageCount > 0 || Profile?.AfterDealingDamageCheck(parameter) != true)
			{
				return result;
			}
			
			AfterDealingDamageCount = Profile.AfterDealingDamageCount;
			return await Profile.ActionOnAfterDealingDamage(parameter);
		}

	}
}
