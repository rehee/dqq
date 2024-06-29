using DQQ.Components.Stages.Actors.Characters;
using ReheeCmf.Entities;

namespace DQQ.Entities
{
	public class OfflineCharacter: EntityBase<Guid>
	{
		public Character? SelectedCharacter {  get; set; }
		
		public List<ItemEntity>? Backpack { get; set; }
		public List<ItemEntity>? Pickup { get; set; }
	}
}
