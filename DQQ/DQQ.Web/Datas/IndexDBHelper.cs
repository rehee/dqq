using DQQ.Entities;
using DQQ.Web.Datas.Entities;
using ReheeCmf.Helpers;
using System.Xml.Linq;
using TG.Blazor.IndexedDB;

namespace DQQ.Web.Datas
{
	public static class IndexDBHelper
	{
		public static IServiceCollection AddMyIndexDB(this IServiceCollection services)
		{
			services.AddIndexedDB(dbStore =>
			{
				dbStore.DbName = "dqq_db";
				dbStore.Version = 1;
				
				dbStore.Stores.AddIndexDBSchema<GameStatus>();
				dbStore.Stores.AddIndexDBSchema<ClientPlayMode>();
			});
			
			return services;
		}

		public static void AddIndexDBSchema<T>(this List<StoreSchema> list) 
		{
			var type = typeof(T);
			list.Add(new StoreSchema
			{
				Name = type.Name,
				PrimaryKey = new IndexSpec { Name = "id", KeyPath = "id", Auto = true, Unique = true },
			});
		}

		
	}
}
