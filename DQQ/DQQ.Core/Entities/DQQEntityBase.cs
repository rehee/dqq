using DQQ.Components;
using ReheeCmf.Entities;

namespace DQQ.Entities
{
	public abstract class DQQEntityBase : EntityBase<Guid>
	{
		public string? Name { get; set; }
	}


	public abstract class DQQEntityBase<T> : DQQEntityBase, IDQQEntity<T> where T : IDQQComponent, new()
	{


		public virtual T GenerateComponent(DQQComponent? parent)
		{
			var instance = new T();
			instance.Initialize(this, parent);
			return instance;
		}
		public virtual TIDQQComponent GenerateTypedComponent<TIDQQComponent>(DQQComponent? parent) where TIDQQComponent : IDQQComponent, new()
		{
			var instance = new TIDQQComponent();
			instance.Initialize(this, parent);
			return instance;
		}
	}
}
