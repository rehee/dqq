using ReheeCmf.Entities;
using ReheeCmf.Responses;

namespace DQQ.Web.Datas
{
	public interface IIndexRepostory
	{
		Task<ContentResponse<bool>> Create<T>(T? item) where T : EntityBase<Guid>;
		Task<IEnumerable<T>> Read<T>() where T : EntityBase<Guid>;
		Task<ContentResponse<bool>> Update<T>(Guid? id, Action<T> update) where T : EntityBase<Guid>;
		Task<ContentResponse<bool>> Update<T>(Guid? id, T update) where T : EntityBase<Guid>;
		Task<ContentResponse<bool>> Delete<T>(T? item) where T : EntityBase<Guid>;
	}
}
