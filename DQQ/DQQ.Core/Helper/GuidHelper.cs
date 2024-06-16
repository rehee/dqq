using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DQQ.Helper
{
	public static class GuidHelper
	{
		public static string GetShortString(this Guid input)
		{
			return input.ToString().Split("-").FirstOrDefault() ?? "n/a";
		}
	}
}
