using DQQ.Drops;
using DQQ.Enums;
using DQQ.Profiles.Skills;
using DQQ.XPs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace DQQ.Profiles.Mobs
{
	public abstract class MobProfile : DQQProfile<EnumMob>, IDropper, IXP
	{
		public virtual decimal AttackSpeedModify => 0;
		public abstract bool IsBoss { get; }
		public virtual double DamagePercentage => 1;
		public virtual double HPPercentage => 1;
		public abstract IEnumerable<MobSkill> Skills { get; }
		public abstract decimal DropRate { get; }
		public abstract decimal RarityRaRate { get; }
		public virtual double XPRate => 1;

		public virtual int QueuePosition => 0;
		public virtual string? ExtureDiscription2 { get; set; }
		public virtual string? ExtureDiscription3 { get; set; }
	}
}
