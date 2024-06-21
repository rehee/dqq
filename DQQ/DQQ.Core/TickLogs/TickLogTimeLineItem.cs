using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DQQ.TickLogs
{
	public class TickLogTimeLineItem
	{

		public int? ActionTick { get; set; }
		public bool IsStart {  get; set; }
		public TickLogActor[]? Players { get; set; }
		public TickLogActor[]? Enemies { get; set; }
	}
}
