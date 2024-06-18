using DQQ.Components;
using DQQ.Components.Stages.Actors.Characters;
using DQQ.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DQQ.Commons.DTOs
{
	public class CombatRequestDTO
	{
		public Guid? ActorId { get; set; }
		public int MapLevel { get; set; }
		public int SubMapLevel { get; set; }
		public Guid? RandomGuid { get; set; }
		public EnumMapNumber MapNumber { get; set; }
		[JsonIgnore]
		public IDQQComponent? Creator { get; set; }
	}
}
