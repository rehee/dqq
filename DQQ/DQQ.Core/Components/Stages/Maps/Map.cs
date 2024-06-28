using DQQ.Components.Items;
using DQQ.Components.Stages.Actors;
using DQQ.Components.Stages.Actors.Mobs;
using DQQ.Consts;
using DQQ.Entities;
using DQQ.Pools;
using DQQ.TickLogs;
using DQQ.Helper;
using System.Numerics;
using DQQ.Profiles;
using ReheeCmf.Responses;
using DQQ.Components.Parameters;
using DQQ.Profiles.Mobs;
using DQQ.Enums;
using DQQ.Commons.DTOs;
using DQQ.Profiles.Maps;

namespace DQQ.Components.Stages.Maps
{
	public class Map : IMap
	{
		public int MapLevel { get; set; }
		public int Tier { get; set; }
		public int SubTier { get; set; }
		public decimal DropQuality { get; set; }

		public decimal DropQuantity { get; set; }

		public int? limitSeconds { get; set; }

		public List<IActor>? Players { get; set; } = new List<IActor>();

		public List<List<IActor>>? MobPool { get; set; } = new List<List<IActor>>();



		public IEnumerable<IItem>? ItemPool { get; set; }

		public Guid? DisplayId { get; set; }

		public string? DisplayName { get; set; }

		public IDQQEntity? Entity { get; set; }

		public DQQProfile? Profile => MapProfile;

		public MapProfile? MapProfile => DQQPool.TryGet<MapProfile, EnumMapNumber>(MapNumber ?? EnumMapNumber.None);

		public ComponentTickParameter? TickParameter { get; set; }


		public async Task Initialize(CombatRequestDTO dto)
		{
			await Task.CompletedTask;
			var seed = RandomHelper.GetRandomSeed(dto.RandomGuid);
			TickParameter = ComponentTickParameter.New(seed);
			Tier = dto.MapLevel;
			SubTier = dto.SubMapLevel;
			var mapProfile = DQQPool.TryGet<MapProfile, EnumMapNumber>(dto.MapNumber);
			MapNumber = mapProfile?.ProfileNumber;
			limitSeconds = mapProfile?.MaxCombatSecond;
			Playable = true;
			if (Tier > 0)
			{
				MapLevel = Math.Min(mapProfile?.MaxLevel ?? 1, DQQGeneral.MinimunMapLevel + DQQGeneral.LevelPerTier * Tier + DQQGeneral.LevelPerSubTier * SubTier);
			}
			else
			{
				if (dto.Creator is Actor createActors)
				{
					MapLevel = createActors.Level <= 0 ? 1 : createActors.Level;
				}
				else
				{
					MapLevel = 1;
				}
			}
			if (dto.Creator is Actor createActor)
			{
				Players!.Add(createActor);
			}
			MobPool = mapProfile?.GenerateMonster(this);
		}

		public void Initialize(IDQQEntity entity, DQQComponent? parent)
		{

		}
		public int TickCount { get; set; } = 0;
		public int WaveTickCount { get; set; } = 0;
		public bool Playing { get; set; }
		public bool Playable { get; set; }
		public DateTime? PlayTime { get; set; }
		public decimal PlayMins => TickCount / (DQQGeneral.TickPerSecond * 60m);
		public bool ReopenBlocked => Playing || (PlayTime != null && PlayTime?.AddMinutes((double)PlayMins) <= DateTime.UtcNow);
		public List<TickLogItem>? Logs { get; set; }
		public List<TickLogTimeLineItem>? TimeLines { get; set; }
		public decimal PlayingCurrentSecond { get; set; }
		public List<ItemComponent>? Drops { get; set; }
		public Int64 XP { get; set; }

		public int WaveIndex { get; set; }
		public int TotalTick { get; set; }
		public async Task Play()
		{
			if (!Playable)
			{
				return;
			}
			PlayTime = DateTime.UtcNow;
			Playable = false;
			Playing = true;
			TickCount = 0;
			Logs = new List<TickLogItem>();
			Drops = new List<ItemComponent>();
			TimeLines = new List<TickLogTimeLineItem>();
			WaveIndex = -1;
			TotalTick = this.TotalTick();
			IActor[] playerPack = [];
			IActor[]? enemyPack = [];
			while (TickCount < TotalTick)
			{
				TickCount++;
				WaveTickCount++;
				PlayingCurrentSecond = TickCount / (decimal)DQQGeneral.TickPerSecond;
				if (Players == null || MobPool == null)
				{
					break;
				}
				playerPack = Players.ToArray();
			
				var currentPack = MobPool.FirstOrDefault(b => b != null && b.Any(m => m.Alive));
				var currentIndex = MobPool.IndexOf(currentPack);
				
				
				if (WaveIndex != currentIndex)
				{
					WaveIndex = currentIndex;
					TickLogHelper.AddMapLogNewWave(this);
					WaveTickCount = 0;
					if (playerPack?.Any() == true)
					{
						foreach (var p in playerPack.Where(b => b.Alive).ToArray())
						{
							p.ResetWave();
						}
					}
					if (currentPack?.Any() != null)
					{
						foreach (var m in currentPack.Where(b => b.Alive).ToArray())
						{
							m.ResetWave();
						}
					}
				}
				if (currentPack == null || currentPack.Any() != true)
				{
					break;
				}
				try
				{
					enemyPack = currentPack.ToArray();
					this.AddTimeLine(TickCount, true, playerPack, enemyPack);
					if (playerPack?.Any() == true)
					{
						foreach (var p in playerPack)
						{
							if (p.TargetPriority != null)
							{
								p.SelectTarget(p.TargetPriority.SelectTargetByPriority(currentPack));
							}
							else
							{
								if (p.Target == null || p.Target.Alive == false)
								{
									p.SelectTarget(currentPack.Where(b => b.Targetable && b.Alive).FirstOrDefault());
								}
							}
							await p.OnTick(ComponentTickParameter.New(TickParameter, p, playerPack, enemyPack, this));
						}
					}

					foreach (var p in enemyPack)
					{
						if (!p.Alive)
						{
							continue;
						}
						if (p.Target == null)
						{
							p.SelectTarget(Players?.FirstOrDefault());
						}
						await p.OnTick(ComponentTickParameter.New(TickParameter, p, enemyPack, playerPack, this));
						if (Players?.All(b => b.Alive == false) == true)
						{
							break;
						}
					}
					if (Players?.All(b => b.Alive == false) == true)
					{
						break;
					}
					this.AddTimeLine(TickCount, false, playerPack, enemyPack);
				}
				catch
				{
					break;
				}

			}
			if(TimeLines?.Any(b=>b.ActionTick == TickCount&& !b.IsStart) != true)
			{
				this.AddTimeLine(TickCount, false, playerPack, enemyPack);
			}
			
			Playing = false;
				
		}

		public async Task<ContentResponse<bool>> OnTick(ComponentTickParameter parameter)
		{
			await Task.CompletedTask;
			return new ContentResponse<bool>();
		}

		public bool IsDisposed { get; protected set; }

		public bool MapClear => MobPool?.All(b => b.All(c => c.Alive != true)) ?? false;

		public EnumMapNumber? MapNumber { get; set; }

		

		public async ValueTask DisposeAsync()
		{
			await Task.CompletedTask;
			if (IsDisposed)
			{
				return;
			}
			IsDisposed = true;
		}

		public void BeforeDamageReduction(ComponentTickParameter parameter)
		{
			throw new NotImplementedException();
		}

		public void DamageReduction(ComponentTickParameter parameter)
		{
			throw new NotImplementedException();
		}

		public void BeforeTakeDamage(ComponentTickParameter parameter)
		{
			throw new NotImplementedException();
		}

		public void AfterTakeDamage(ComponentTickParameter parameter)
		{
			throw new NotImplementedException();
		}

		public Task<ContentResponse<bool>> AfterDealingDamage(ComponentTickParameter parameter)
		{
			throw new NotImplementedException();
		}
	}
}
