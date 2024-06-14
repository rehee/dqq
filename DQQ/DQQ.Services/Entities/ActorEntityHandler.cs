using DQQ.Entities.ChangeHandlers;
using DQQ.Entities;
using ReheeCmf.Commons;
using ReheeCmf.Components.ChangeComponents;
using ReheeCmf.Helpers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReheeCmf.Entities;

namespace DQQ.Services.Entities
{
	[EntityChangeTracker<ActorEntity>]

	public class ActorEntityHandler : BaseHandler<ActorEntity>
	{
		public override async Task<IEnumerable<ValidationResult>> ValidationAsync(CancellationToken ct = default)
		{
			var baseResult = await base.ValidationAsync(ct);
			var current = new List<ValidationResult>();

			if (String.IsNullOrEmpty(entity.Name))
			{
				current.Add(ValidationResultHelper.New("Name is required", nameof(entity.Name)));
				goto ToReturn;
			}

			var sameActor = await query.FirstOrDefaultAsync(context.Query<ActorEntity>(true)
				.Where(b => b.Name == entity.Name && b.Id != entity.Id), ct);
			if (sameActor != null)
			{
				current.Add(ValidationResultHelper.New("Name already used", nameof(entity.Name)));
				goto ToReturn;
			}
		ToReturn:
			return current.Concat(baseResult);
		}

		public override async Task BeforeCreateAsync(CancellationToken ct = default)
		{
			await base.BeforeCreateAsync(ct);
			var existngs = await query.ToArrayAsync(context.Query<ActorEntity>(true).Where(b => b.OwnerId == UserId).Select(b => b.Id), ct);
			if (existngs?.Count() >= 3)
			{
				StatusException.Throw(System.Net.HttpStatusCode.BadRequest, "already with 3 chars");
			}
			if (entity?.Level <= 0)
			{
				entity.Level = 1;
			}
		}

		public override async Task BeforeUpdateAsync(EntityChanges[] propertyChange, CancellationToken ct = default)
		{
			await base.BeforeUpdateAsync(propertyChange, ct);
			if (entity?.Level <= 0)
			{
				entity.Level = 1;
			}
		}
	}
}
