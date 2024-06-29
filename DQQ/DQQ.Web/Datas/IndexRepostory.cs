using DQQ.Web.Datas.Entities;
using ReheeCmf.Entities;
using ReheeCmf.Helpers;
using ReheeCmf.Responses;
using TG.Blazor.IndexedDB;

namespace DQQ.Web.Datas
{
	public class IndexRepostory : IIndexRepostory
	{
		private readonly IndexedDBManager dbManager;

		public IndexRepostory(IndexedDBManager dbManager)
		{
			this.dbManager = dbManager;
		}

		public async Task<ContentResponse<bool>> Create<T>(T? item) where T : EntityBase<Guid>
		{
			var result = new ContentResponse<bool>();
			if (item == null)
			{
				return result;
			}
			var entity = IndexEntity<T>.New(item);
			item!.Id=Guid.NewGuid();
			var record = new StoreRecord<IndexEntity<T>>()
			{
				Data = entity,
				Storename = typeof(T).Name,
			};
			await dbManager.AddRecord< IndexEntity<T>>(record);
			result.SetSuccess(true);
			return result;
		}

		public async Task<ContentResponse<bool>> Delete<T>(T? item) where T : EntityBase<Guid>
		{
			var result = new ContentResponse<bool>();
			var entity = (await Query<T>()).Where(b=>b.Entity?.Id == item?.Id).FirstOrDefault();
			if (entity == null) 
			{
				return result;
			}
			await dbManager.DeleteRecord<int>(typeof(T).Name,entity.Id??0);
			return result;
		}


		public async Task<IEnumerable<IndexEntity<T>>> Query<T>() where T : EntityBase<Guid>
		{
			return await dbManager.GetRecords<IndexEntity<T>>(typeof(T).Name);
		}

		public async Task<IEnumerable<T>> Read<T>() where T : EntityBase<Guid>
		{
			var result = await Query<T>();
			return result.Select(b => b.Entity).Where(b=>b!=null).Select(b=> b!);
		}

		public async Task<ContentResponse<bool>> Update<T>(T? item) where T : EntityBase<Guid>
		{
			var result = new ContentResponse<bool>();
			var entity = (await Query<T>()).Where(b => b.Entity?.Id == item?.Id).FirstOrDefault();
			if (entity == null)
			{
				return result;
			}
			entity.Entity = item;
			var storeRecord = new StoreRecord<IndexEntity<T>>()
			{
				Data= entity,
				Storename =typeof(T).Name,
			};
			await dbManager.UpdateRecord<IndexEntity<T>>(storeRecord);
			return result;
		}
	}
}
