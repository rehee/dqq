﻿using DQQ.Commons.DTOs;
using DQQ.Components;
using DQQ.Components.Items;
using DQQ.Components.Parameters;
using DQQ.Components.Stages;
using DQQ.Components.Stages.Actors;
using DQQ.Components.Stages.Maps;
using DQQ.Drops;
using DQQ.Entities;
using DQQ.Enums;
using DQQ.Helper;
using DQQ.Profiles;
using DQQ.Profiles.Maps;
using DQQ.TickLogs;
using DQQ.UnitTest;
using ReheeCmf.Responses;
using System.Numerics;

namespace DQQ.Core.UnitTest.Helpers
{
  public class DropHelperTest : BaseTest
  {
    [Test]
    public void DropTestHappyPath()
    {
      var dropper = new TestDropper();
      var mapper = new TestMap();

      var dropList = dropper.Drop(mapper);
      Assert.That(dropList.Count(), Is.EqualTo(DropConst.MaxDropNumber));
    }
  }

  public class TestDropper : IDropper
  {
    public decimal DropRate { get; set; } = 0.2m;
  }
  public class TestMap : IMap
  {
    public int MapLevel => 10;

    public int Tier => throw new NotImplementedException();

    public int SubTier => throw new NotImplementedException();

    public decimal DropQuality => 10000m;

    public decimal DropQuantity => 10000m;

    public int? limitMinute => throw new NotImplementedException();

    public IEnumerable<IActor>? Players => throw new NotImplementedException();

    public List<List<IActor>?>? MobPool => throw new NotImplementedException();
    public int WaveIndex { get; set; }
    public bool Playable => throw new NotImplementedException();

    public bool Playing => throw new NotImplementedException();

    public DateTime? PlayTime => throw new NotImplementedException();

    public decimal PlayMins => throw new NotImplementedException();

    public bool ReopenBlocked => throw new NotImplementedException();

    public IEnumerable<IItem>? ItemPool => throw new NotImplementedException();

    public List<TickLogItem> Logs => throw new NotImplementedException();

    public Guid? DisplayId => throw new NotImplementedException();

    public string? DisplayName => throw new NotImplementedException();

    public IDQQEntity? Entity { get; set; }

    public DQQProfile? Profile { get; set; }

    public List<ItemComponent>? Drops => throw new NotImplementedException();

    public decimal PlayingCurrentSecond => throw new NotImplementedException();

    public Int64 XP { get; set; }

    public int TickCount { get; set; }

    public int WaveTickCount { get; set; }
    public ComponentTickParameter? TickParameter { get; set; } = ComponentTickParameter.New(111);

		public int? limitSeconds => throw new NotImplementedException();

		public EnumMapNumber? MapNumber => throw new NotImplementedException();

		public bool MapClear => throw new NotImplementedException();

		public MapProfile? MapProfile => throw new NotImplementedException();

		public int TotalTick => throw new NotImplementedException();

		public List<TickLogTimeLineItem>? TimeLines => throw new NotImplementedException();

		List<IActor>? IMap.Players => throw new NotImplementedException();

		public Task<ContentResponse<bool>> AfterDealingDamage(ComponentTickParameter parameter)
		{
			throw new NotImplementedException();
		}

		public void AfterTakeDamage(ComponentTickParameter parameter)
    {
      throw new NotImplementedException();
    }

    public void BeforeDamageReduction(ComponentTickParameter parameter)
    {
      throw new NotImplementedException();
    }

    public void BeforeTakeDamage(ComponentTickParameter parameter)
    {
      throw new NotImplementedException();
    }

    public void DamageReduction(ComponentTickParameter parameter)
    {
      throw new NotImplementedException();
    }

    public void Dispose()
    {
      throw new NotImplementedException();
    }

    public ValueTask DisposeAsync()
    {
      throw new NotImplementedException();
    }

    public Task Initialize(IDQQComponent creator, int mapTier, int mapSubTier, Guid? randomSeed = null)
    {
      throw new NotImplementedException();
    }

    public void Initialize(IDQQEntity entity, DQQComponent? parent)
    {
      throw new NotImplementedException();
    }

		public Task Initialize(CombatRequestDTO dto)
		{
			throw new NotImplementedException();
		}

		public Task<ContentResponse<bool>> OnTick(ComponentTickParameter parameter)
    {
      throw new NotImplementedException();
    }

    public Task Play()
    {
      throw new NotImplementedException();
    }
  }
}
