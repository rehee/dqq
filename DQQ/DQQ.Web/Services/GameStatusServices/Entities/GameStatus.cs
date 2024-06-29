using DQQ.Enums;
using DQQ.Web.Enums;
using ReheeCmf.Commons.DTOs;
using ReheeCmf.Entities;

namespace DQQ.Entities
{
	public class GameStatus:EntityBase<Guid>
	{
		public EnumPlayMode PlayMode { get; set; }
		public string? OwnerId { get; set; }
		public EnumCombatPlayType CombatPlayType { get; set; }
		public Guid? CurrentCharId {  get; set; }
		public TokenDTO? Token { get; set; }
		public string? Culture {  get; set; }

		public void Set(GameStatus? input)
		{
			PlayMode = input?.PlayMode ?? EnumPlayMode.NotSpecified;
			OwnerId = input?.OwnerId;
			CombatPlayType = input?.CombatPlayType?? EnumCombatPlayType.NotSpecified;
			CurrentCharId = input?.CurrentCharId;
			Token = input?.Token;
			Culture = input?.Culture;
		}
	}
}
