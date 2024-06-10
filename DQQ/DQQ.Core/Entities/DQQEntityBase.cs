using DQQ.Components;
using ReheeCmf.Entities;

namespace DQQ.Entities
{
  public abstract class DQQEntityBase: EntityBase<Guid>
  {
		public string? Name { get; set; }
	}


	public abstract class DQQEntityBase<T> : DQQEntityBase, IDQQEntity<T> where T : IDQQComponent, new()
  {
    

    public virtual T GenerateComponent()
    {
      var instance = new T();
      instance.Initialize(this);
      return instance;
    }
    public virtual TIDQQComponent GenerateTypedComponent<TIDQQComponent>() where TIDQQComponent : IDQQComponent, new()
    {
      var instance = new TIDQQComponent();
      instance.Initialize(this);
      return instance;
    }
  }
}
