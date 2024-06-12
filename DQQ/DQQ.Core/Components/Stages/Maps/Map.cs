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

namespace DQQ.Components.Stages.Maps
{
	public class Map : IMap
	{
		public int MapLevel { get; set; }
		public int Tier { get; set; }
		public int SubTier { get; set; }
		public decimal DropQuality { get; set; }

		public decimal DropQuantity { get; set; }

		public int? limitMinute { get; set; }

		public IEnumerable<IActor>? Players { get; set; } = new List<IActor>();

		public List<List<IActor>?>? MobPool { get; set; } = new List<List<IActor>?>();



		public IEnumerable<IItem>? ItemPool { get; set; }

		public Guid? DisplayId { get; set; }

		public string? DisplayName { get; set; }

		public IDQQEntity? Entity { get; set; }

		public DQQProfile? Profile { get; set; }

		public async Task Initialize(IDQQComponent creator, int mapTier, int mapSubTier)
		{
			await Task.CompletedTask;
			Tier = mapTier;
			SubTier = mapSubTier;
			Playable = true;
			if (Tier > 0)
			{
				MapLevel = DQQGeneral.MinimunMapLevel + DQQGeneral.LevelPerTier * Tier + DQQGeneral.LevelPerSubTier * SubTier;
			}
			else
			{
				if (creator is Actor createActors)
				{
					MapLevel = createActors.Level <= 0 ? 1 : createActors.Level;
				}
				else
				{
					MapLevel = 1;
				}
			}
			if (creator is Actor createActor)
			{
				Players = new IActor[] { createActor };
			}
			var mobList = new List<List<IActor>>();
			MobPool = mobList;

			for (var i = 1; i < 10; i++)
			{
				var wave = new List<IActor>();
				mobList.Add(wave);
				var mob = DQQPool.MobPool.Where(b => b.Value.IsBoss != true).Select(b => new { r = RandomHelper.GetRandom(1), b = b }).OrderByDescending(b => b.r).Select(b => b.b.Value).FirstOrDefault();
				if (i % 4 == 0)
				{
					wave.Add(Monster.Create(mob, MapLevel, Enums.EnumMobRarity.Champion));
					continue;
				}
				wave.Add(Monster.Create(mob, MapLevel, Enums.EnumMobRarity.Normal));
			}
			var mobWithBoss = DQQPool.MobPool.Where(b => b.Value.IsBoss).Select(b => new { r = RandomHelper.GetRandom(1), b = b }).OrderByDescending(b => b.r).Select(b => b.b.Value).FirstOrDefault();

			var finalWave = new List<IActor>();
			var finalNormalMob = DQQPool.MobPool.Where(b => b.Value.IsBoss != true).Select(b => new { r = RandomHelper.GetRandom(1), b = b }).OrderByDescending(b => b.r).Select(b => b.b.Value).FirstOrDefault();
			finalWave.Add(Monster.Create(finalNormalMob, MapLevel, Enums.EnumMobRarity.Normal));
			finalWave.Add(Monster.Create(mobWithBoss, MapLevel));
			mobList.Add(finalWave);

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
		public decimal PlayingCurrentSecond { get; set; }
		public List<ItemComponent>? Drops { get; set; }
		public Int64 XP { get; set; }

		public int WaveIndex { get; set; }

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
			WaveIndex = -1;
			while (TickCount < this.TotalTick())
			{
				TickCount++;
				WaveTickCount++;
				PlayingCurrentSecond = TickCount / (decimal)DQQGeneral.TickPerSecond;
				if (Players == null || MobPool == null)
				{
					break;
				}
				var currentPack = MobPool.FirstOrDefault(b => b != null && b.Any(m => m.Alive));
				var currentIndex = MobPool.IndexOf(currentPack);
				if (WaveIndex != currentIndex)
				{
					WaveIndex = currentIndex;
					TickLogHelper.AddMapLogNewWave(this);
					WaveTickCount = 0;
					if (Players?.Any() == true)
					{
						foreach (var p in Players.Where(b => b.Alive).ToArray())
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
					if (Players?.Any() == true)
					{
						foreach (var p in Players.ToArray())
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
							await p.OnTick(ComponentTickParameter.New(p, Players, currentPack, this));
						}
					}

					foreach (var p in currentPack.ToArray())
					{
						if (!p.Alive)
						{
							continue;
						}
						if (p.Target == null)
						{
							p.SelectTarget(Players?.FirstOrDefault());
						}
						await p.OnTick(ComponentTickParameter.New(p, currentPack, Players, this));
					}
					if (Players?.All(b => b.Alive == false) == true)
					{
						break;
					}
				}
				catch
				{
					break;
				}

			}
			Playing = false;
		}

		public async Task<ContentResponse<bool>> OnTick(ComponentTickParameter parameter)
		{
			await Task.CompletedTask;
			return new ContentResponse<bool>();
		}

		public bool IsDisposed { get; protected set; }
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
