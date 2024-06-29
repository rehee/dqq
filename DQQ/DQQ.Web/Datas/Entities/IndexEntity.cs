using ReheeCmf.Entities;

namespace DQQ.Web.Datas.Entities
{
	public class IndexEntity<T> where T : EntityBase<Guid>
	{
		public static IndexEntity<T> New(T? entity)
		{
			return new IndexEntity<T>()
			{
				Entity = entity,
			};
		}
		public int? Id { get; set; }
		public T? Entity { get; set; }
	}
}
