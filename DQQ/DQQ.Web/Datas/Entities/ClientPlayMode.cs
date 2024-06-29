using DQQ.Web.Enums;
using ReheeCmf.Entities;

namespace DQQ.Entities
{
	public class ClientPlayMode : EntityBase<Guid>
	{
		public ClientPlayMode()
		{
			
		}
		public string? FirstName { get; set; }
		public string? LastName { get; set; }
		public EnumPlayMode? PlayMode { get; set; }
	}
}
