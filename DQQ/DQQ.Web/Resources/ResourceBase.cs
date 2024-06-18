using DQQ.Profiles;
using DQQ.Web.Pages;

namespace DQQ.Web.Resources
{

	public abstract class ResourceBase<T> : DQQPageBase where T : Enum
	{
		public abstract T Profile { get; }
	}
}
