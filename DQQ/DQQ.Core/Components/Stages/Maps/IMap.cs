using DQQ.Commons.DTOs;
using DQQ.Components.Items;
using DQQ.Components.Parameters;
using DQQ.Components.Stages.Actors;
using DQQ.Enums;
using DQQ.Profiles.Maps;
using DQQ.TickLogs;
using System.Numerics;

namespace DQQ.Components.Stages.Maps
{
	public interface IMap
	{
		EnumMapNumber? MapNumber { get; }
		int MapLevel { get; }
		int Tier { get; }
		int SubTier { get; }
		decimal DropQuality { get; }
		decimal DropQuantity { get; }
		int? limitSeconds { get; }
		List<IActor>? Players { get; }
		List<List<IActor>>? MobPool { get; }
		int WaveIndex { get; }
		int TickCount { get; }
		int WaveTickCount { get; }
		bool Playable { get; }
		bool Playing { get; }
		DateTime? PlayTime { get; }
		decimal PlayMins { get; }
		bool ReopenBlocked { get; }
		Task Play();
		Task Initialize(CombatRequestDTO dto);
		IEnumerable<IItem>? ItemPool { get; }
		List<TickLogItem> Logs { get; }
		List<ItemComponent>? Drops { get; }
		decimal PlayingCurrentSecond { get; }
		Int64 XP { get; set; }
		ComponentTickParameter? TickParameter { get; set; }
		bool MapClear { get; }
		MapProfile? MapProfile { get; }
	}
}
