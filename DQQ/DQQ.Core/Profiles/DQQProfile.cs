using DQQ.Components.Parameters;
using ReheeCmf.Helpers;
using ReheeCmf.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DQQ.Profiles
{
	public abstract class DQQProfile
	{

		public abstract string? Name { get; }
		public abstract string? Discription { get; }

		public virtual int AfterDealingDamageCount => 15;
		public virtual int BeforeDamageReductionCount => 0;
		public virtual int DamageReductionCount => 0;
		public virtual int BeforeTakeDamageCount => 0;
		public virtual int AfterTakeDamageCount => 0;

		public virtual Task<ContentResponse<bool>> ActionOnAfterDealingDamage(ComponentTickParameter parameter)
		{
			var result = new ContentResponse<bool>();
			result.SetSuccess(true);
			return Task.FromResult(result);
		}

		public virtual bool AfterDealingDamageCheck(ComponentTickParameter parameter)
		{
			return false;
		}
	}
	public abstract class DQQProfile<T> : DQQProfile where T : Enum
	{
		public abstract T ProfileNumber { get; }

	}
}
